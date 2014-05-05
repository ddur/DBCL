// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

using DD.Diagnostics;

namespace DD.Collections
{
	/// <summary>Set of positive (0 included) Int32 integers with capacity of Int32.MaxValue
	/// <para>Like BitArray, with ISet&lt;int&gt; interface, on-bits counting and on-bits enumeration.</para>
	/// <para>On-bit represents positive integer set member</para>
	/// <remarks>Instance with maximum capacity takes 256MB</remarks>
	/// </summary>
	// TODO? BitShift
	// TODO? Parallel.ForEach
	[Serializable]
	public sealed partial class BitSetArray :
		ISet<int>, ICollection<int>, IEnumerable<int>,
		IEquatable<BitSetArray>, IComparable<BitSetArray>,
		ICollection, IEnumerable, ICloneable
	{

		#region Ctor

		/// <summary>
		/// Create empty set
		/// </summary>
		private BitSetArray()
		{
			Contract.Ensures(Theory.Construct(this));
		}

		/// <summary>
		/// Create empty set with size/capacity of length items
		/// </summary>
		/// <param name="length"></param>
		private BitSetArray(int length)
		{
			Contract.Requires<ArgumentOutOfRangeException>(
				ValidLength(length));

			Contract.Ensures(Theory.Construct(this, length));

			this.range = length;
			this.array = new long[BitSetArray.GetLongArrayLength(this.range)];

		}

		/// <summary>
		/// Create empty set with size/capacity of length items and with default value if true
		/// </summary>
		/// <param name="length"></param>
		/// <param name = "value"></param>
		private BitSetArray(int length, bool value = false)
			: this(length)
		{
			Contract.Requires<ArgumentOutOfRangeException>(ValidLength(length));
			Contract.Requires<ArgumentException>(!value.Bool() || (length > 0));

			Contract.Ensures(Theory.Construct(this, length, value));

			if (value.Bool()
			    && this.range > 0) {
				Contract.Assert(this.array.Length > 0);
				for (int i = 0; i < array.Length; i++) {
					this.array[i] = -1;
				}
				this.ClearTail();
				this.count = this.range;
				this.FirstSet(0);
				this.LastSet(this.range - 1);
			}
		}

		/// <summary>
		/// Create set from another set
		/// </summary>
		/// <param name="that"></param>
		private BitSetArray(BitSetArray that)
		{
			Contract.Requires<ArgumentNullException>(that.IsNot(null));

			Contract.Ensures(Theory.Construct(this, that));

			lock (that.SyncRoot) {
				this.array = new long[that.array.Length];
				this.version = that.version;
				this.range = that.range;
				this.count = that.count;
				this.startVersion = that.startVersion;
				this.startMemoize = that.startMemoize;
				this.finalVersion = that.finalVersion;
				this.finalMemoize = that.finalMemoize;
				if (this.array.Length != 0 && this.count != 0) {
					Array.Copy(that.array, this.array, BitSetArray.GetLongArrayLength(this.range)); // do not copy exccess bit-blocks
				}
			}
		}

		/// <summary>
		/// Create set from another set using new range Length
		/// </summary>
		/// <param name="that"></param>
		/// <param name="length"></param>
		private BitSetArray(BitSetArray that, int length)
		{
			Contract.Requires<ArgumentNullException>(that.IsNot(null));
			Contract.Requires<ArgumentOutOfRangeException>(ValidLength(length));

			Contract.Ensures(Theory.Construct(this, that, length));

			lock (that.SyncRoot) {
				this.range = length;
				this.array = new long[BitSetArray.GetLongArrayLength(this.range)];
				if (this.array.Length != 0
				    && that.array.Length != 0
				    && that.Count != 0) {
					int thatRangeArrayLength = BitSetArray.GetLongArrayLength(that.range);
					if (this.array.Length >= thatRangeArrayLength) {
						Array.Copy(that.array, this.array, thatRangeArrayLength);
						if (this.range >= that.range || (int)that.Last < this.range) {
							this.count = that.count;
							this.startVersion = that.startVersion;
							this.startMemoize = that.startMemoize;
							this.finalVersion = that.finalVersion;
							this.finalMemoize = that.finalMemoize;
						} else {
							this.ClearTail();
							this.count = BitSetArray.CountOnBits(this.array);
						}
					} else {
						Array.Copy(that.array, this.array, this.array.Length);
						this.ClearTail();
						this.count = BitSetArray.CountOnBits(this.array);
					}
				}
			}
		}

		#endregion

		#region Embed

		public abstract class EnumeratorForwardAbstract : IEnumerator<int>, IEnumerator, IDisposable
		{
			protected readonly BitSetArray enumerated;
			private readonly int version;
			private readonly int arrayLen;
			private int arrIndex;
			private int bitStart;
			private int bitIndex;
			private ulong bitItems;
			private bool doNext;
			private bool invalid = true;

			public EnumeratorForwardAbstract(BitSetArray that)
			{
				Contract.Requires<ArgumentNullException>(that.IsNot(null));
				Contract.Ensures(Theory.Construct(this, that));

				this.init(that, ref this.version, ref this.enumerated, ref this.arrayLen);
			}

			protected EnumeratorForwardAbstract(BitSetArray that, object thatSyncRoot)
			{
				Contract.Requires<ArgumentNullException>(!that.IsNull());
				Contract.Requires<ArgumentNullException>(!thatSyncRoot.IsNull());
				Contract.Ensures(Theory.Construct(this, that));

				lock (thatSyncRoot) {
					this.init(that, ref this.version, ref this.enumerated, ref this.arrayLen);
				}
			}

			private void init(BitSetArray that, ref int this_version, ref BitSetArray this_enumerated, ref int this_arrayLen)
			{

				this_version = that.version;
				this_enumerated = that;
				this_arrayLen = BitSetArray.GetLongArrayLength(this.enumerated.range);

				this.arrIndex = 0;
				this.doNext = this.enumerated.count != 0;
				if (this.doNext) {
					Contract.Assert(this.arrayLen != 0);
					this.bitItems = unchecked ((ulong)this.enumerated.array[this.arrIndex]);
				}
				this.bitStart = 0;
				this.bitIndex = -1;
			}

			public void Reset()
			{
				// do the same as compiler generated enumerator
				throw new NotSupportedException();
			}

			public void Dispose()
			{
			}

			public virtual bool MoveNext()
			{
				Contract.Ensures(Theory.MoveNext(this, Contract.Result<bool>()));
				Contract.EnsuresOnThrow<InvalidOperationException>(Theory.MoveNextOnThrow(this));

				if (this.doNext && (this.version == this.enumerated.version)) {
					if (this.bitItems == 0) {
						// read/skip empty block(s)
						while (true) {
							++this.arrIndex;
							if (this.arrIndex == this.arrayLen)
								break;
							if (this.enumerated.array[this.arrIndex] != 0)
								break;
						}
						if (this.arrIndex != this.arrayLen) {
							this.bitStart = this.arrIndex * longBits; // compute offset
							this.bitIndex = -1; // reset bit index
							this.bitItems = unchecked ((ulong)this.enumerated.array[this.arrIndex]);
						}

					}
					if (this.bitItems != 0) {
						if (this.invalid) {
							this.invalid = false;
						}
						++this.bitIndex;
						while ((this.bitItems & 1ul) == 0) {
							++this.bitIndex;
							this.bitItems >>= 1;
						}
						this.bitItems >>= 1;
					} else {
						this.doNext = false; // break&stop .MoveNext
						this.invalid = true; // invalidate .Current
					}
				} else if (this.version != this.enumerated.version) {
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				return this.doNext;
			}

			int IEnumerator<int>.Current {
				get {
					Contract.Ensures(Theory.Current(this, Contract.Result<int>()));
					Contract.EnsuresOnThrow<InvalidOperationException>(Theory.CurrentOnThrow(this));

					if (!this.invalid && this.version == this.enumerated.version) {
						return (this.bitStart + this.bitIndex);
					}
					if (this.invalid) {
						throw new InvalidOperationException("The enumerator is not positioned within collection.");
					}
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
			}

			object IEnumerator.Current {
				get {
					return ((IEnumerator<int>)this).Current;
				}
			}

			private static class Theory
			{
				[Pure] public static bool Construct(EnumeratorForwardAbstract me, BitSetArray that)
				{
					Success success = true;

					success.Assert(that.IsNot(null));

					success.Assert(me.doNext == (that.count != 0));
					success.Assert(me.invalid == true);

					return success;
				}

				[Pure] public static bool MoveNext(EnumeratorForwardAbstract me, bool result)
				{
					Success success = true;
					if (result) {
						success.Assert(!me.invalid);
						success.Assert(me.enumerated[me.bitStart + me.bitIndex]);
					} else {
						success.Assert(me.invalid);
					}
					return success;
				}

				[Pure] public static bool MoveNextOnThrow(EnumeratorForwardAbstract me)
				{
					Success success = true;

					success.Assert(me.version != me.enumerated.version);

					return success;
				}

				[Pure] public static bool Current(EnumeratorForwardAbstract me, int retValue)
				{
					Success success = true;

					success.Assert(!me.invalid);
					success.Assert(me.version == me.enumerated.version);
					success.Assert(me.enumerated[retValue]);
					success.Assert(retValue.InRange(0, me.enumerated.range - 1));
					success.Assert(retValue == me.bitStart + me.bitIndex);

					return success;
				}

				[Pure] public static bool CurrentOnThrow(EnumeratorForwardAbstract me)
				{
					Success success = true;

					success.Assert(me.invalid || (me.version != me.enumerated.version));

					return success;
				}

			}

		}

		public sealed class EnumeratorForward : EnumeratorForwardAbstract
		{
			public EnumeratorForward(BitSetArray that)
				: base(that)
			{
			}
		}

		public sealed class EnumeratorForwardSynchronized : EnumeratorForwardAbstract
		{
			public EnumeratorForwardSynchronized(BitSetArray that)
				: base(that, that.SyncRoot)
			{
			}
			public override bool MoveNext()
			{
				lock (enumerated.SyncRoot) {
					return base.MoveNext();
				}
			}
		}

		public sealed class EnumeratorForwardReadOnly : EnumeratorForwardAbstract
		{
			public EnumeratorForwardReadOnly(BitSetArray that)
				: base(BitSetArray.Copy(that))
			{
			}
		}

		public abstract class EnumeratorComplementAbstract : IEnumerator<int>, IEnumerator, IDisposable
		{
			protected readonly BitSetArray enumerated;
			private readonly int version;
			private readonly int arrayLen;
			private int arrIndex;
			private int bitStart;
			private int bitIndex;
			private ulong bitItems;
			private bool doNext;
			private bool invalid = true;

			protected EnumeratorComplementAbstract(BitSetArray that)
			{
				Contract.Requires<ArgumentNullException>(!that.IsNull());
				Contract.Ensures(Theory.Construct(this, that));

				this.init(that, ref this.version, ref this.enumerated, ref this.arrayLen);
			}

			protected EnumeratorComplementAbstract(BitSetArray that, object thatSyncRoot)
			{
				Contract.Requires<ArgumentNullException>(!that.IsNull());
				Contract.Requires<ArgumentNullException>(!thatSyncRoot.IsNull());
				Contract.Ensures(Theory.Construct(this, that));

				lock (thatSyncRoot) {
					this.init(that, ref this.version, ref this.enumerated, ref this.arrayLen);
				}
			}

			private void init(BitSetArray that, ref int this_version, ref BitSetArray this_enumerated, ref int this_arrayLen)
			{
				this_version = that.version;
				this_enumerated = that;
				this_arrayLen = BitSetArray.GetLongArrayLength(this.enumerated.range);

				this.arrIndex = 0;
				this.doNext = this.enumerated.count != this.enumerated.range;
				if (this.doNext) {
					Contract.Assert(this.arrayLen != 0);
					this.bitItems = unchecked ((ulong)this.enumerated.array[this.arrIndex]);
					this.bitItems = ~this.bitItems;
					if (this.arrIndex == (this.arrayLen - 1)) { // clear tail
						this.bitItems &= ulong.MaxValue >> (longBits - (this.enumerated.range & mask0x3F));
					}
				}
				this.bitStart = 0;
				this.bitIndex = -1;
			}

			public void Reset()
			{
				// do the same as compiler generated enumerator
				throw new NotSupportedException();
			}

			public void Dispose()
			{
			}

			public virtual bool MoveNext()
			{
				Contract.Ensures(Theory.MoveNext(this, Contract.Result<bool>()));
				Contract.EnsuresOnThrow<InvalidOperationException>(Theory.MoveNextOnThrow(this));

				if (this.doNext && (this.version == this.enumerated.version)) {
					if (this.bitItems == 0) {
						// skip empty block(s)
						while (true) {
							++this.arrIndex;
							if (this.arrIndex == this.arrayLen) {
								break;
							}
							if (this.enumerated.array[this.arrIndex] != -1) {
								break;
							}
						}
						if (this.arrIndex != this.arrayLen) {
							this.bitStart = this.arrIndex * longBits; // compute offset
							this.bitIndex = -1; // reset bit index
							this.bitItems = unchecked ((ulong)this.enumerated.array[this.arrIndex]);
							this.bitItems = ~this.bitItems;
							if (this.arrIndex == (this.arrayLen - 1)) { // clear tail
								this.bitItems &= ulong.MaxValue >> (longBits - (this.enumerated.range & mask0x3F));
							}
						}

					}
					if (this.bitItems != 0) {
						if (this.invalid) {
							this.invalid = false;
						}
						++this.bitIndex;
						while ((this.bitItems & 1ul) == 0) {
							++this.bitIndex;
							this.bitItems >>= 1;
						}
						this.bitItems >>= 1;
					} else {
						this.doNext = false; // break&stop .MoveNext
						this.invalid = true; // invalidate .Current
					}
				} else if (this.version != this.enumerated.version) {
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				return this.doNext;
			}

			int IEnumerator<int>.Current {
				get {
					Contract.Ensures(Theory.Current(this, Contract.Result<int>()));
					Contract.EnsuresOnThrow<InvalidOperationException>(Theory.CurrentOnThrow(this));

					if (!this.invalid && this.version == this.enumerated.version) {
						return (this.bitStart + this.bitIndex);
					}
					if (this.invalid) {
						throw new InvalidOperationException("The enumerator is not positioned within collection.");
					}
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
			}

			object IEnumerator.Current {
				get {
					return ((IEnumerator<int>)this).Current;
				}
			}

			private static class Theory
			{
				[Pure] public static bool Construct(EnumeratorComplementAbstract me, BitSetArray that)
				{
					Success success = true;

					success.Assert(that.IsNot(null));

					success.Assert(me.doNext == (that.count != that.range));
					success.Assert(me.invalid == true);

					return success;
				}

				[Pure] public static bool MoveNext(EnumeratorComplementAbstract me, bool result)
				{
					Success success = true;
					if (result) {
						success.Assert(!me.invalid);
						success.Assert(!me.enumerated[me.bitStart + me.bitIndex]);
					} else {
						success.Assert(me.invalid);
					}
					return success;
				}

				[Pure] public static bool MoveNextOnThrow(EnumeratorComplementAbstract me)
				{
					Success success = true;

					success.Assert(me.version != me.enumerated.version);

					return success;
				}

				[Pure] public static bool Current(EnumeratorComplementAbstract me, int retValue)
				{
					Success success = true;

					success.Assert(!me.invalid);
					success.Assert(me.version == me.enumerated.version);
					success.Assert(!me.enumerated[retValue]);
					success.Assert(retValue.InRange(0, me.enumerated.range - 1));
					success.Assert(retValue == me.bitStart + me.bitIndex);

					return success;
				}

				[Pure] public static bool CurrentOnThrow(EnumeratorComplementAbstract me)
				{
					Success success = true;

					success.Assert(me.invalid || (me.version != me.enumerated.version));

					return success;
				}

			}

		}

		public sealed class EnumeratorComplement : EnumeratorComplementAbstract
		{
			public EnumeratorComplement(BitSetArray that)
				: base(that)
			{
			}
		}
		
		public sealed class EnumeratorComplementSynchronized : EnumeratorComplementAbstract
		{
			public EnumeratorComplementSynchronized(BitSetArray that)
				: base(that, that.SyncRoot)
			{
			}

			public override bool MoveNext()
			{
				lock (this.enumerated.SyncRoot) {
					return base.MoveNext();
				}
			}
		}

		public sealed class EnumeratorComplementReadOnly : EnumeratorComplementAbstract
		{
			public EnumeratorComplementReadOnly(BitSetArray that)
				: base(BitSetArray.Copy(that))
			{
			}
		}

		public abstract class EnumeratorReverseAbstract : IEnumerator<int>, IEnumerator, IDisposable
		{
			protected readonly BitSetArray enumerated;
			private readonly int version;
			private readonly int arrayLen;
			private int arrIndex;
			private int bitStart;
			private int bitIndex;
			private long bitItems;
			private bool doNext;
			private bool invalid = true;

			protected EnumeratorReverseAbstract(BitSetArray that)
			{
				Contract.Requires<ArgumentNullException>(!that.IsNull());
				Contract.Ensures(Theory.Construct(this, that));

				this.init(that, ref this.version, ref this.enumerated, ref this.arrayLen);
			}

			protected EnumeratorReverseAbstract(BitSetArray that, object thatSyncRoot)
			{
				Contract.Requires<ArgumentNullException>(!that.IsNull());
				Contract.Requires<ArgumentNullException>(!thatSyncRoot.IsNull());
				Contract.Ensures(Theory.Construct(this, that));

				lock (thatSyncRoot) {
					this.init(that, ref this.version, ref this.enumerated, ref this.arrayLen);
				}
			}

			private void init(BitSetArray that, ref int this_version, ref BitSetArray this_enumerated, ref int this_arrayLen)
			{
				this_version = that.version;
				this_enumerated = that;
				this_arrayLen = BitSetArray.GetLongArrayLength(this.enumerated.range);

				this.arrIndex = this.arrayLen - 1;
				this.doNext = this.enumerated.count != 0;
				if (this.doNext) {
					Contract.Assert(this.arrayLen != 0);
					this.bitItems = this.enumerated.array[this.arrIndex];
				}
				this.bitStart = this.arrIndex * longBits; // set offset
				this.bitIndex = longBits; // reset bit index counter
			}

			public void Reset()
			{
				// do the same as compiler generated enumerator
				throw new NotSupportedException();
			}

			public void Dispose()
			{
			}

			public virtual bool MoveNext()
			{
				Contract.Ensures(Theory.MoveNext(this, Contract.Result<bool>()));
				Contract.EnsuresOnThrow<InvalidOperationException>(Theory.MoveNextOnThrow(this));

				if (this.doNext && (this.version == this.enumerated.version)) {
					if (this.bitItems == 0) {
						// skip empty block(s)
						while (true) {
							--this.arrIndex;
							if (this.arrIndex < 0)
								break;
							if (this.enumerated.array[this.arrIndex] != 0)
								break;
						}
						if (this.arrIndex >= 0) {
							this.bitStart = this.arrIndex * longBits; // compute offset
							this.bitIndex = longBits; // reset bit index
							this.bitItems = this.enumerated.array[this.arrIndex];
						}

					}
					if (this.bitItems != 0) {
						if (this.invalid)
							this.invalid = false;
						--this.bitIndex;
						while (this.bitItems > 0) {
							--this.bitIndex;
							this.bitItems <<= 1;
						}
						this.bitItems <<= 1;
					} else {
						this.doNext = false; // break&stop .MoveNext
						this.invalid = true; // invalidate .Current
					}
				} else if (this.version != this.enumerated.version) {
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				return this.doNext;
			}

			int IEnumerator<int>.Current {
				get {
					Contract.Ensures(Theory.Current(this, Contract.Result<int>()));
					Contract.EnsuresOnThrow<InvalidOperationException>(Theory.CurrentOnThrow(this));

					if (!this.invalid && this.version == this.enumerated.version) {
						return (this.bitStart + this.bitIndex);
					}
					if (this.invalid) {
						throw new InvalidOperationException("The enumerator is not positioned within collection.");
					}
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
			}

			object IEnumerator.Current {
				get {
					return ((IEnumerator<int>)this).Current;
				}
			}

			private static class Theory
			{
				[Pure] public static bool Construct(EnumeratorReverseAbstract me, BitSetArray that)
				{
					Success success = true;

					success.Assert(that.IsNot(null));

					success.Assert(me.doNext == (that.count != 0));
					success.Assert(me.invalid == true);

					return success;
				}

				[Pure] public static bool MoveNext(EnumeratorReverseAbstract me, bool result)
				{
					Success success = true;

					if (result) {
						success.Assert(!me.invalid);
						success.Assert(me.enumerated[me.bitStart + me.bitIndex]);
					} else {
						success.Assert(me.invalid);
					}

					return success;
				}

				[Pure] public static bool MoveNextOnThrow(EnumeratorReverseAbstract me)
				{
					Success success = true;

					success.Assert(me.version != me.enumerated.version);

					return success;
				}

				[Pure] public static bool Current(EnumeratorReverseAbstract me, int retValue)
				{
					Success success = true;

					success.Assert(!me.invalid);
					success.Assert(me.version == me.enumerated.version);
					success.Assert(me.enumerated[retValue]);
					success.Assert(retValue.InRange(0, me.enumerated.range - 1));
					success.Assert(retValue == me.bitStart + me.bitIndex);

					return success;
				}

				[Pure] public static bool CurrentOnThrow(EnumeratorReverseAbstract me)
				{
					Success success = true;

					success.Assert(me.invalid || (me.version != me.enumerated.version));

					return success;
				}

			}
		}

		public sealed class EnumeratorReverse : EnumeratorReverseAbstract
		{
			public EnumeratorReverse(BitSetArray that)
				: base(that)
			{
			}

		}

		public sealed class EnumeratorReverseSynchronized : EnumeratorReverseAbstract
		{
			public EnumeratorReverseSynchronized(BitSetArray that)
				: base(that, that.sRoot)
			{
			}

			public override bool MoveNext()
			{
				lock (this.enumerated.SyncRoot) {
					return base.MoveNext();
				}
			}
		}

		public sealed class EnumeratorReverseReadOnly : EnumeratorReverseAbstract
		{
			public EnumeratorReverseReadOnly(BitSetArray that)
				: base(BitSetArray.Copy(that))
			{
			}
		}

		#endregion

		#region Fields

		private int range = 0;
		private int count = 0;
		private int version = int.MaxValue;
		private long[] array = new long[0];

		[NonSerialized]
		private readonly object sRoot = new object();

		[NonSerialized]
		public const int MinCount = 0;
		[NonSerialized]
		public const int MaxCount = int.MaxValue;
		[NonSerialized]
		public const int MinValue = 0;
		[NonSerialized]
		public const int MaxValue = BitSetArray.MaxCount - 1;

		[NonSerialized]
		private const int byteBits = 8;
		[NonSerialized]
		private const int shortBits = sizeof(short) * byteBits;
		[NonSerialized]
		private const int int32Bits = sizeof(int) * byteBits;
		[NonSerialized]
		private const int longBits = sizeof(long) * byteBits;
		[NonSerialized]
		private const int log2of64 = 6;
		[NonSerialized]
		private const int mask0x3F = 0x3F;
		[NonSerialized]
		private const ulong table = 0x4332322132212110;
		// bit counter table		  FEDCBA9876543210

		[NonSerialized]
		private int? startVersion = null;
		[NonSerialized]
		private int? finalVersion = null;
		[NonSerialized]
		private int? startMemoize = null;
		[NonSerialized]
		private int? finalMemoize = null;

		#if DEBUG // enable read-only access to test private members state
		public int? StartVersion { get { return startVersion; } }
		public int? StartMemoize { get { return startMemoize; } }
		public int? FinalVersion { get { return finalVersion; } }
		public int? FinalMemoize { get { return finalMemoize; } }
		#endif

		#endregion

		#region Methods

		#region Invariant

		[ContractInvariantMethod]
		private void BitSetArrayInvariant()
		{
			Contract.Invariant(Theory.Invariant(this));
		}

		#endregion

		#region Public Static

		#region Member&Length Validation

		[Pure] public static bool ValidMembers(IEnumerable<int> items)
		{
			return Theory.IsValidMembers(items);
		}

		[Pure] public static bool ValidMember(int item)
		{
			return Theory.IsValidMember(item);
		}

		[Pure] public static bool ValidLength(int item)
		{
			return Theory.IsValidLength(item);
		}

		#endregion

		#region Factory

		[Pure] public static BitSetArray From(IEnumerable<int> items)
		{
			Contract.Requires<ArgumentNullException>(items != null);
			Contract.Requires<ArgumentOutOfRangeException>(ValidMembers(items));

			Contract.Ensures(Theory.From(Contract.Result<BitSetArray>(), items));

			BitSetArray retValue;
			if (items.IsEmpty()) {
				retValue = new BitSetArray();
			} else {
				int maxValue = int.MinValue;
				foreach (int item in items) {
					if (item > maxValue)
						maxValue = item;
				}
				Contract.Assert(maxValue != int.MinValue);
				retValue = new BitSetArray(maxValue + 1);
				retValue._SetItems(items);
			}
			return retValue;
		}

		[Pure] public static BitSetArray From(int required, params int[] optional)
		{
			Contract.Requires<ArgumentOutOfRangeException>(
				ValidMember(required));
			Contract.Requires<ArgumentOutOfRangeException>(
				optional == null || optional.Length == 0 ||
				ValidMembers(optional));

			Contract.Ensures(
				Theory.From(Contract.Result<BitSetArray>(), required, optional));

			var items = new List<int>();
			items.Add(required); // value type (int) is never null
			if (optional.Length != 0) { // params is never null, cannot produce null test
				items.AddRange(optional);
			}
			return BitSetArray.From(items); // Redirect to From(IEnumerable<int>)

		}

		[Pure] public static BitSetArray From(BitSetArray that)
		{
			Contract.Requires<ArgumentNullException>(that.IsNot(null));

			return new BitSetArray(that);
		}

		[Pure] public static BitSetArray From(BitSetArray that, int length)
		{
			Contract.Requires<ArgumentNullException>(that.IsNot(null));
			Contract.Requires<ArgumentOutOfRangeException>(ValidLength(length));

			return new BitSetArray(that, length);
		}

		[Pure] public static BitSetArray Copy(BitSetArray that)
		{
			Contract.Requires<ArgumentNullException>(that.IsNot(null));

			return new BitSetArray(that);
		}

		[Pure] public static BitSetArray Size(int length = 0, bool value = false)
		{
			Contract.Requires<ArgumentOutOfRangeException>(ValidLength(length));
			Contract.Requires<ArgumentException>(
				(
				    value.Bool() == true
				    && length > 0
				)
				||
				(
				    value.Bool() == false
				)
			);

			return new BitSetArray(length, value);
		}

		[Pure] public static BitSetArray Mask(BitArray mask, int length)
		{
			Contract.Requires<ArgumentNullException>(mask != null);
			Contract.Requires<ArgumentOutOfRangeException>(ValidLength(length));

			Contract.Ensures(Theory.Mask(Contract.Result<BitSetArray>(), mask, length));

			var retValue = new BitSetArray(length);

			if (retValue.array.Length > 0) {
				IEnumerator maskEnumerator = mask.GetEnumerator();
				bool cycle = maskEnumerator.MoveNext();
				if (cycle) {
					long value = 0;
					int i = 0, j = 0;
					int maskCount = 1;
					for (i = 0; i < retValue.array.Length; i++) {
						value = 0;
						for (j = 0; j < longBits; j++) {
							if ((bool)maskEnumerator.Current) {
								value |= (1L << j);
								++retValue.count;
							}
							if (maskCount == retValue.range) {
								break;
							}
							cycle = maskEnumerator.MoveNext();
							if (!cycle) {
								break;
							}
							++maskCount;
						}
						retValue.array[i] = value;
						if (!cycle) {
							break;
						}
					}
				}
			}
			return retValue;
		}

		[Pure] public static BitSetArray Mask(IEnumerable<bool> mask, int length)
		{
			Contract.Requires<ArgumentNullException>(mask != null);
			Contract.Requires<ArgumentOutOfRangeException>(ValidLength(length));

			Contract.Ensures(Theory.Mask(Contract.Result<BitSetArray>(), mask, length));

			var retValue = new BitSetArray(length);

			if (retValue.range > 0) {
				Contract.Assert(retValue.array.Length > 0);

				IEnumerator<bool> maskEnumerator = mask.GetEnumerator();
				bool cycle = maskEnumerator.MoveNext();
				if (cycle) {
					long value = 0;
					int i = 0, j = 0;
					int maskCount = 1;
					for (i = 0; i < retValue.array.Length; i++) {
						value = 0;
						for (j = 0; j < longBits; j++) {
							if (maskEnumerator.Current) {
								value |= (1L << j);
								++retValue.count;
							}
							if (maskCount == retValue.range) {
								break;
							}
							cycle = maskEnumerator.MoveNext();
							if (!cycle) {
								break;
							}
							++maskCount;
						}
						retValue.array[i] = value;
						if (!cycle) {
							break;
						}
					}
				}
			}
			return retValue;
		}

		[Pure] public static BitSetArray Mask(IEnumerable<byte> mask, int length)
		{
			Contract.Requires<ArgumentNullException>(mask != null);
			Contract.Requires<ArgumentOutOfRangeException>(ValidLength(length));

			Contract.Ensures(Theory.Mask(Contract.Result<BitSetArray>(), mask, length));

			var retValue = new BitSetArray(length);

			if (retValue.range > 0) {
				Contract.Assert(retValue.array.Length > 0);

				IEnumerator<byte> maskEnumerator = mask.GetEnumerator();
				bool cycle = maskEnumerator.MoveNext();
				if (cycle) {
					int maskCount = 1;
					int i = 0, j = 0;
					long value = 0;
					for (i = 0; i < retValue.array.Length; i++) {
						value = 0;
						for (j = 0; j < (sizeof(long) / sizeof(byte)); j++) {
							if (maskEnumerator.Current != 0) {
								value |= ((long)(maskEnumerator.Current)) << (j << 3);
							}
							cycle = maskEnumerator.MoveNext();
							if (!cycle) {
								break;
							}
							++maskCount;
						}
						retValue.count += BitSetArray.CountOnBits(value);
						retValue.array[i] = value;
						if (!cycle) {
							break;
						}
					}
					if ((maskCount << 3) > retValue.range) {
						// mask is longer than range, cleanup tail
						int lastBitsCount = BitSetArray.CountOnBits(retValue.array[retValue.array.Length - 1]);
						if (lastBitsCount != 0) {
							retValue.count -= lastBitsCount;
							retValue.ClearTail();
							retValue.count += BitSetArray.CountOnBits(retValue.array[retValue.array.Length - 1]);
						}
					}
				}
			}
			return retValue;
		}

		[Pure] public static BitSetArray Mask(IEnumerable<short> mask, int length)
		{
			Contract.Requires<ArgumentNullException>(mask != null);
			Contract.Requires<ArgumentOutOfRangeException>(ValidLength(length));

			Contract.Ensures(Theory.Mask(Contract.Result<BitSetArray>(), mask, length));

			var retValue = new BitSetArray(length);

			if (retValue.range > 0) {
				Contract.Assert(retValue.array.Length > 0);

				IEnumerator<short> maskEnumerator = mask.GetEnumerator();
				bool cycle = maskEnumerator.MoveNext();
				if (cycle) {
					int maskCount = 1;
					int i = 0, j = 0;
					long value = 0;
					for (i = 0; i < retValue.array.Length; i++) {
						value = 0;
						for (j = 0; j < (sizeof(long) / sizeof(short)); j++) {
							if (maskEnumerator.Current != 0) {
								value |= (((long)(unchecked ((ushort)maskEnumerator.Current))) << (j << 4));
							}
							cycle = maskEnumerator.MoveNext();
							if (!cycle) {
								break;
							}
							++maskCount;
						}
						retValue.count += BitSetArray.CountOnBits(value);
						retValue.array[i] = value;
						if (!cycle) {
							break;
						}
					}
					if ((maskCount << 4) > retValue.range) {
						// mask is longer than range, cleanup tail
						int lastBitsCount = BitSetArray.CountOnBits(retValue.array[retValue.array.Length - 1]);
						if (lastBitsCount != 0) {
							retValue.count -= lastBitsCount;
							retValue.ClearTail();
							retValue.count += BitSetArray.CountOnBits(retValue.array[retValue.array.Length - 1]);
						}
					}
				}
			}
			return retValue;
		}

		[Pure] public static BitSetArray Mask(IEnumerable<int> mask, int length)
		{
			Contract.Requires<ArgumentNullException>(mask != null);
			Contract.Requires<ArgumentOutOfRangeException>(ValidLength(length));

			Contract.Ensures(Theory.Mask(Contract.Result<BitSetArray>(), mask, length));

			var retValue = new BitSetArray(length);

			if (retValue.range > 0) {
				Contract.Assert(retValue.array.Length > 0);

				IEnumerator<int> maskEnumerator = mask.GetEnumerator();
				bool cycle = maskEnumerator.MoveNext();
				if (cycle) {
					int maskCount = 1;
					int i = 0, j = 0;
					long value = 0;
					for (i = 0; i < retValue.array.Length; i++) {
						value = 0;
						for (j = 0; j < (sizeof(long) / sizeof(int)); j++) {
							if (maskEnumerator.Current != 0) {
								value |= (((long)(unchecked ((uint)maskEnumerator.Current))) << (j << 5));
							}
							cycle = maskEnumerator.MoveNext();
							if (!cycle) {
								break;
							}
							++maskCount;
						}
						retValue.count += BitSetArray.CountOnBits(value);
						retValue.array[i] = value;
						if (!cycle) {
							break;
						}
					}
					if ((maskCount << 5) > retValue.range) {
						// mask is longer than range, cleanup tail
						int lastBitsCount = BitSetArray.CountOnBits(retValue.array[retValue.array.Length - 1]);
						if (lastBitsCount != 0) {
							retValue.count -= lastBitsCount;
							retValue.ClearTail();
							retValue.count += BitSetArray.CountOnBits(retValue.array[retValue.array.Length - 1]);
						}
					}
				}
			}
			return retValue;
		}

		[Pure] public static BitSetArray Mask(IEnumerable<long> mask, int length)
		{
			Contract.Requires<ArgumentNullException>(mask != null);
			Contract.Requires<ArgumentOutOfRangeException>(ValidLength(length));

			Contract.Ensures(Theory.Mask(Contract.Result<BitSetArray>(), mask, length));

			var retValue = new BitSetArray(length);

			if (retValue.range > 0) {
				Contract.Assert(retValue.array.Length > 0);

				IEnumerator<long> maskEnumerator = mask.GetEnumerator();
				bool cycle = maskEnumerator.MoveNext();
				if (cycle) {
					int maskCount = 1;
					for (int i = 0; i < retValue.array.Length; i++) {
						if (maskEnumerator.Current != 0) {
							retValue.array[i] = maskEnumerator.Current;
							retValue.count += BitSetArray.CountOnBits(maskEnumerator.Current);
						}
						cycle = maskEnumerator.MoveNext();
						if (!cycle) {
							break;
						}
						++maskCount;
					}
					if ((maskCount << 6) > retValue.range) {
						// mask is longer than range, cleanup tail
						int lastBitsCount = BitSetArray.CountOnBits(retValue.array[retValue.array.Length - 1]);
						if (lastBitsCount != 0) {
							retValue.count -= lastBitsCount;
							retValue.ClearTail();
							retValue.count += BitSetArray.CountOnBits(retValue.array[retValue.array.Length - 1]);
						}
					}
				}
			}
			return retValue;
		}

		#endregion

		#region Ctor/To... Helpers

		[Pure] public static int GetLongArrayLength(int length)
		{
			Contract.Requires<ArgumentOutOfRangeException>(ValidLength(length));
			Contract.Ensures(
				(length == 0 &&
				Contract.Result<int>() == 0) ||
				(length > 0 &&
				(((long)Contract.Result<int>() * longBits) >= length) &&
				((((long)Contract.Result<int>() - 1) * longBits) < length) &&
				(Contract.Result<int>() == (((length - 1) / longBits) + 1)))
			);

			return length == 0 ? 0 : ((--length) >> log2of64) + 1;
		}

		[Pure] public static int GetIntArrayLength(int length)
		{
			Contract.Requires<ArgumentOutOfRangeException>(ValidLength(length));
			Contract.Ensures(
				(length == 0 &&
				Contract.Result<int>() == 0) ||
				(length > 0 &&
				(((long)Contract.Result<int>() * int32Bits) >= length) &&
				((((long)Contract.Result<int>() - 1) * int32Bits) < length) &&
				(Contract.Result<int>() == (((length - 1) / int32Bits) + 1)))
			);

			return length == 0 ? 0 : ((--length) >> 5) + 1;
		}

		[Pure] public static int GetShortArrayLength(int length)
		{
			Contract.Requires<ArgumentOutOfRangeException>(ValidLength(length));
			Contract.Ensures(
				(length == 0 &&
				Contract.Result<int>() == 0) ||
				(length > 0 &&
				(((long)Contract.Result<int>() * shortBits) >= length) &&
				((((long)Contract.Result<int>() - 1) * shortBits) < length) &&
				(Contract.Result<int>() == (((length - 1) / shortBits) + 1)))
			);

			return length == 0 ? 0 : ((--length) >> 4) + 1;
		}

		[Pure] public static int GetByteArrayLength(int length)
		{
			Contract.Requires<ArgumentOutOfRangeException>(ValidLength(length));
			Contract.Ensures(
				(length == 0 &&
				Contract.Result<int>() == 0) ||
				(length > 0 &&
				(((long)Contract.Result<int>() * byteBits) >= length) &&
				((((long)Contract.Result<int>() - 1) * byteBits) < length) &&
				(Contract.Result<int>() == (((length - 1) / byteBits) + 1)))
			);

			return length == 0 ? 0 : ((--length) >> 3) + 1;
		}

		#endregion

		#region Count "On" Bits

		[Pure] public static int CountOnBits(byte mask)
		{
			Contract.Ensures(Contract.Result<int>().InRange(0, byteBits));
			Contract.Ensures(Theory.CountOnBits(Contract.Result<int>(), mask));

			return BitSetArray.CountOnBits((ulong)mask);
		}

		[Pure] public static int CountOnBits(short mask)
		{
			Contract.Ensures(Contract.Result<int>().InRange(0, 16));
			Contract.Ensures(Theory.CountOnBits(Contract.Result<int>(), mask));

			return BitSetArray.CountOnBits(unchecked ((ulong)((ushort)mask)));
		}

		[Pure] public static int CountOnBits(int mask)
		{
			Contract.Ensures(Contract.Result<int>().InRange(0, 32));
			Contract.Ensures(Theory.CountOnBits(Contract.Result<int>(), mask));

			return BitSetArray.CountOnBits(unchecked ((ulong)((uint)mask)));
		}

		[Pure] public static int CountOnBits(long mask)
		{
			Contract.Ensures(Contract.Result<int>().InRange(0, longBits));
			Contract.Ensures(Theory.CountOnBits(Contract.Result<int>(), mask));

			return BitSetArray.CountOnBits(unchecked ((ulong)mask));
		}

		[CLSCompliant(false)]
		[Pure] public static int CountOnBits(ulong mask)
		{
			Contract.Ensures(Contract.Result<int>().InRange(0, longBits));
			Contract.Ensures(Theory.CountOnBits(Contract.Result<int>(), mask));

			int retCount = 0;
			while (mask != 0) {
				retCount += (int)((table >> (int)((mask & 0xFul) << 2)) & 0xFul);
				mask >>= 4;
			}
			return retCount;
		}

		[Pure] public static int CountOnBits(BitArray mask)
		{
			Contract.Ensures(Theory.CountOnBits(Contract.Result<int>(), mask));

			int retCount = 0;
			if (mask != null
			    && mask.Length != 0) {

				// Counting BitArray.CopyTo(int[]) without length(range limit) does not count correctly because bits above BitArray.length are copied
				// => bits above BitArray.Length are not cleared when BitArray.Length is reduced!
				var array = new int[(mask.Length + 31) / 32];
				mask.CopyTo(array, 0);
				retCount = BitSetArray.CountOnBits(array, mask.Length);

			}
			return retCount;
		}


		/// <summary>
		/// <remarks>If bits on count is &gt; int.MaxValue, method will throw <exception cref="System.OverflowException">exception</exception></remarks>
		/// </summary>
		/// <param name="mask"></param>
		/// <returns></returns>
		[Pure] public static int CountOnBits(bool[] mask)
		{
			Contract.Ensures(Theory.CountOnBits(Contract.Result<int>(), mask));

			int retCount = 0;
			if (mask != null && mask.Length != 0) {
				foreach (bool bitBlock in mask) {
					if (bitBlock) {
						++retCount;
					}
				}
			}
			return retCount;
		}

		/// <summary>
		/// <remarks>If bits on count is &gt; int.MaxValue, method will throw <exception cref="System.OverflowException">exception</exception></remarks>
		/// </summary>
		/// <param name="mask"></param>
		/// <returns></returns>
		[Pure] public static int CountOnBits(byte[] mask)
		{
			Contract.Ensures(Theory.CountOnBits(Contract.Result<int>(), mask));

			int retCount = 0;
			if (mask != null && mask.Length != 0) {
				byte bitBlock = 0;
				for (int arrIndex = 0; arrIndex < mask.Length; arrIndex++) {
					bitBlock = mask[arrIndex];
					switch (bitBlock) {
						case 0:
							break;
						case byte.MaxValue:
							retCount += byteBits;
							break;
						default:
							do {
								retCount += (int)((table >> ((bitBlock & 0xF) << 2)) & 0xFul);
								bitBlock >>= 4;
							} while (bitBlock != 0);
							break;
					}
				}
			}
			return retCount;
		}

		/// <summary>
		/// <remarks>If bits on count is &gt; int.MaxValue, method will throw <exception cref="System.OverflowException">exception</exception></remarks>
		/// </summary>
		/// <param name="mask"></param>
		/// <returns></returns>
		[Pure] public static int CountOnBits(short[] mask)
		{
			Contract.Ensures(Theory.CountOnBits(Contract.Result<int>(), mask));

			int retCount = 0;
			if (mask != null && mask.Length != 0) {
				ushort bitBlock = 0; // unsigned for logical right shift
				for (int arrIndex = 0; arrIndex < mask.Length; arrIndex++) {
					bitBlock = unchecked ((ushort)mask[arrIndex]);
					switch (bitBlock) {
						case 0:
							break;
						case ushort.MaxValue:
							retCount += shortBits;
							break;
						default:
							do {
								retCount += (int)((table >> ((bitBlock & 0xF) << 2)) & 0xFul);
								bitBlock >>= 4;
							} while (bitBlock != 0);
							break;
					}
				}
			}
			return retCount;
		}

		/// <summary>
		/// <remarks>If bits on count is &gt; int.MaxValue, method will throw <exception cref="System.OverflowException">exception</exception></remarks>
		/// </summary>
		/// <param name="mask"></param>
		/// <returns></returns>
		[Pure] public static int CountOnBits(int[] mask)
		{
			Contract.Ensures(Theory.CountOnBits(Contract.Result<int>(), mask));

			int retCount = 0;
			if (mask != null && mask.Length != 0) {
				uint bitBlock = 0; // unsigned for logical right shift
				for (int arrIndex = 0; arrIndex < mask.Length; arrIndex++) {
					bitBlock = unchecked ((uint)mask[arrIndex]);
					switch (bitBlock) {
						case 0:
							break;
						case uint.MaxValue:
							retCount += int32Bits;
							break;
						default:
							do {
								retCount += (int)((table >> (int)((bitBlock & 0xF) << 2)) & 0xFul);
								bitBlock >>= 4;
							} while (bitBlock != 0);
							break;
					}
				}
			}
			return retCount;
		}

		[Pure] public static int CountOnBits(int[] mask, int length)
		{
			Contract.Requires<ArgumentOutOfRangeException>(ValidLength(length));
			Contract.Ensures(Theory.CountOnBits(Contract.Result<int>(), mask, length));

			int retCount = 0;
			if (mask != null && mask.Length != 0 && length != 0) {
				int arrLength = BitSetArray.GetIntArrayLength(length);
				if (arrLength > mask.Length) {
					arrLength = mask.Length;
				}
				uint bitBlock = 0; // unsigned for logical right shift
				for (int arrIndex = 0; arrIndex < arrLength - 1; arrIndex++) {
					bitBlock = unchecked ((uint)mask[arrIndex]);
					switch (bitBlock) {
						case 0:
							break;
						case uint.MaxValue:
							retCount += int32Bits;
							break;
						default:
							do {
								retCount += (int)((table >> (int)((bitBlock & 0xF) << 2)) & 0xFul);
								bitBlock >>= 4;
							} while (bitBlock != 0);
							break;
					}
				}
				// mask last bit-block up to range and count
				bitBlock = unchecked ((uint)mask[arrLength - 1]) & (uint.MaxValue >> (int32Bits - (length & 0x1F)));
				while (bitBlock != 0) {
					retCount += (int)((table >> (int)((bitBlock & 0xF) << 2)) & 0xFul);
					bitBlock >>= 4;
				}
			}
			return retCount;
		}

		/// <summary>
		/// <remarks>If bitsOn count is &gt; int.MaxValue, method will throw <exception cref="System.OverflowException">exception</exception></remarks>
		/// </summary>
		/// <param name="mask"></param>
		/// <returns></returns>
		[Pure] public static int CountOnBits(long[] mask)
		{
			Contract.Ensures(Theory.CountOnBits(Contract.Result<int>(), mask));

			int retCount = 0;
			if (mask != null && mask.Length != 0) {
				ulong bitBlock = 0; // unsigned for logical right shift
				for (int arrIndex = 0; arrIndex < mask.Length; arrIndex++) {
					bitBlock = unchecked ((ulong)mask[arrIndex]);
					switch (bitBlock) {
						case 0:
							break;
						case ulong.MaxValue:
							retCount += longBits;
							break;
						default:
							do {
								retCount += (int)((table >> (int)((bitBlock & 0xFul) << 2)) & 0xFul);
								bitBlock >>= 4;
							} while (bitBlock != 0);
							break;
					}
				}
			}
			return retCount;
		}

		/// <summary>
		/// </summary>
		/// <param name="mask"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		[Pure] public static int CountOnBits(long[] mask, int length)
		{
			Contract.Requires<ArgumentOutOfRangeException>(ValidLength(length));
			Contract.Ensures(Theory.CountOnBits(Contract.Result<int>(), mask, length));

			int retCount = 0;
			if (mask != null && mask.Length != 0 && length != 0) {
				int arrLength = BitSetArray.GetLongArrayLength(length);
				if (arrLength > mask.Length) {
					arrLength = mask.Length;
				}
				ulong bitBlock = 0; // unsigned for logical right shift
				// count all but last
				for (int arrIndex = 0; arrIndex < (arrLength - 1); arrIndex++) {
					bitBlock = unchecked ((ulong)mask[arrIndex]);
					switch (bitBlock) {
						case 0:
							break;
						case ulong.MaxValue:
							retCount += longBits;
							break;
						default:
							do {
								retCount += (int)((table >> (int)((bitBlock & 0xFul) << 2)) & 0xFul);
								bitBlock >>= 4;
							} while (bitBlock != 0);
							break;
					}
				}
				// mask last bit-block up to range and count
				bitBlock = unchecked ((ulong)mask[arrLength - 1]) & (ulong.MaxValue >> (longBits - (length & mask0x3F)));
				while (bitBlock != 0) {
					retCount += (int)((table >> (int)((bitBlock & 0xFul) << 2)) & 0xFul);
					bitBlock >>= 4;
				}
			}
			return retCount;
		}

		/// <summary>
		/// <remarks>If bits on count is &gt; int.MaxValue, method will throw <exception cref="System.OverflowException">exception</exception></remarks>
		/// </summary>
		/// <param name="mask"></param>
		/// <returns></returns>
		[Pure] public static int CountOnBits(IEnumerable<bool> mask)
		{
			Contract.Ensures(ValidLength(Contract.Result<int>()));

			int retCount = 0;
			if (mask != null) {
				foreach (bool bit in mask) {
					if (bit) {
						++retCount;
					}
				}
			}
			return retCount;
		}

		/// <summary>
		/// <remarks>If bitsOn count is &gt; int.MaxValue, method will throw <exception cref="System.OverflowException">exception</exception></remarks>
		/// </summary>
		/// <param name="mask"></param>
		/// <returns></returns>
		[Pure] public static int CountOnBits(IEnumerable<byte> mask)
		{
			Contract.Ensures(Theory.CountOnBits(Contract.Result<int>(), mask));

			int retCount = 0;
			if (mask != null) {
				byte bitBlock = 0;
				foreach (byte item in mask) {
					bitBlock = item;
					switch (bitBlock) {
						case 0:
							break;
						case byte.MaxValue:
							retCount += byteBits;
							break;
						default:
							do {
								retCount += (int)((table >> ((bitBlock & 0xF) << 2)) & 0xFul);
								bitBlock >>= 4;
							} while (bitBlock != 0);
							break;
					}
				}
			}
			return retCount;
		}

		/// <summary>
		/// <remarks>If bitsOn count is &gt; int.MaxValue, method will throw <exception cref="System.OverflowException">exception</exception></remarks>
		/// </summary>
		/// <param name="mask"></param>
		/// <returns></returns>
		[Pure] public static int CountOnBits(IEnumerable<short> mask)
		{
			Contract.Ensures(Theory.CountOnBits(Contract.Result<int>(), mask));

			int retCount = 0;
			if (mask != null) {
				ushort bitBlock = 0;
				foreach (short item in mask) {
					bitBlock = unchecked ((ushort)item);
					switch (bitBlock) {
						case 0:
							break;
						case ushort.MaxValue:
							retCount += shortBits;
							break;
						default:
							do {
								retCount += (int)((table >> ((bitBlock & 0xF) << 2)) & 0xFul);
								bitBlock >>= 4;
							} while (bitBlock != 0);
							break;
					}
				}
			}
			return retCount;
		}

		/// <summary>
		/// <remarks>If bitsOn count is &gt; int.MaxValue, method will throw <exception cref="System.OverflowException">exception</exception></remarks>
		/// </summary>
		/// <param name="mask"></param>
		/// <returns></returns>
		[Pure] public static int CountOnBits(IEnumerable<int> mask)
		{
			Contract.Ensures(Theory.CountOnBits(Contract.Result<int>(), mask));

			int retCount = 0;
			if (mask != null) {
				uint bitBlock = 0; // unsigned for logical right shift
				foreach (int item in mask) {
					bitBlock = unchecked ((uint)item);
					switch (bitBlock) {
						case 0:
							break;
						case uint.MaxValue:
							retCount += int32Bits;
							break;
						default:
							do {
								retCount += (int)((table >> (int)((bitBlock & 0xF) << 2)) & 0xFul);
								bitBlock >>= 4;
							} while (bitBlock != 0);
							break;
					}
				}
			}
			return retCount;
		}

		/// <summary>
		/// <remarks>If bitsOn count is &gt; int.MaxValue, method will throw <exception cref="System.OverflowException">exception</exception></remarks>
		/// </summary>
		/// <param name="mask"></param>
		/// <returns></returns>
		[Pure] public static int CountOnBits(IEnumerable<long> mask)
		{
			Contract.Ensures(Theory.CountOnBits(Contract.Result<int>(), mask));

			int retCount = 0;
			if (mask != null) {
				ulong bitBlock = 0; // unsigned for logical right shift
				foreach (long item in mask) {
					bitBlock = unchecked ((ulong)item);
					switch (bitBlock) {
						case 0:
							break;
						case ulong.MaxValue:
							retCount += longBits;
							break;
						default:
							do {
								retCount += (int)((table >> (int)((bitBlock & 0xFul) << 2)) & 0xFul);
								bitBlock >>= 4;
							} while (bitBlock != 0);
							break;
					}
				}
			}
			return retCount;
		}

		#endregion

		#endregion

		#region Public Instance

		#region Indexer

		/// <summary>Indexer
		/// <para>Get: Return true if item is member of collection</para>
		/// <para>Get: No exceptions thrown</para>
		/// <para>Set: Depending on value Add(item) to or Remove(item) from collection.</para>
		/// <para>Set: Throws ArgumentOutOfRangeException if value == true && (item &lt; 0 || item &gt;= this.Length)</para>
		/// </summary>
		/// <param name="item"></param>
		/// <returns>Get: bool. True if this contains item.</returns>
		public bool this[int item] {
			[Pure]
			get {
				Contract.Ensures(Theory.IndexerGetItemValue(this, item, Contract.Result<bool>()));

				if (this.InRange(item)) {
					Contract.Assert((item >> log2of64).InRange(0, this.array.Length - 1));
					return this._Get(item);
				} else {
					return false;
				}
			}
			set {
				Contract.Requires<ArgumentOutOfRangeException>(
					(value.Bool() == true && this.InRange(item)) || value.Bool() == false);
				Contract.Ensures(Theory.IndexerSetItemValue(Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)), item, value, this));

				if (!value.Bool() && (!this.InRange(item))) {
					// if index is outside of BitSetArray range accept false value
				} else {
					this._Set(item, value);
				}
			}
		}

		#endregion

		#region Mutable Operations

		public BitSetArray And(BitSetArray that)
		{

			Contract.Ensures(
				Theory.And(Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)), that, this, Contract.Result<BitSetArray>())
			);

			if (this.Is(that)
			    || this.count == 0) {
				// A ∩ A = A
				// A == ∅ => (∅ ∩ B = ∅) == A
			} else if (that.IsNull() || that.Count == 0) {
				// A ∩ ∅ = ∅
				Contract.Assert(this.count != 0);
				this.SetAll(false); // .SetAll(false) will change version

			} else {

				lock (SyncRoot) {

					int this_version = this.version;
					long bits_result = 0;

					// And ∧ => IntersectWith ∩
					int thisArrLen = BitSetArray.GetLongArrayLength(this.range);
					int thatArrLen = BitSetArray.GetLongArrayLength(that.range);
	
					int temp_count = 0;
					int index = 0;
					int opLength = thisArrLen <= thatArrLen ?
						thisArrLen :
						thatArrLen;
					for (index = 0; index < opLength; index++) {
						if (this.array[index] != 0 || that.array[index] != 0) {
							bits_result = this.array[index] & that.array[index];
							if (bits_result != this.array[index]) {
								if (this_version == this.version) {
									this.AddVersion();
								}
								this.array[index] = bits_result;
							}
						}
						temp_count += BitSetArray.CountOnBits(this.array[index]);
					}
					for (index = opLength; index < thisArrLen; index++) {
						this.array[index] = 0;
					}
					// This operation only can (optionally) decrease number of members
					if (this.count != temp_count) {
						this.count = temp_count;
					}
				}
				// no false bit above result.Length will ever change into true
				// Positive logic -> no need to ClearTail()
			}

			return this;
		}

		public BitSetArray Or(BitSetArray that)
		{
			Contract.Ensures(
				Theory.Or(Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)), that, this, Contract.Result<BitSetArray>())
			);

			if (this.Is(that)
			    || that.Is(null)
			    || that.count == 0) {
				// A ∪ A = A
				// A ∪ ∅ = A
			} else { // Or => UnionWith B

				lock (SyncRoot) {

					int this_version = this.version;
					long bits_result = 0;

					if (this.Length < that.Length) {
						this.Length = that.Length;
					}
	
					int thisArrLen = BitSetArray.GetLongArrayLength(this.range);
					int thatArrLen = BitSetArray.GetLongArrayLength(that.range);
					Contract.Assert(thisArrLen >= thatArrLen);
	
					int index = 0;
					int temp_count = 0;
					int opLength = thisArrLen <= thatArrLen ?
						thisArrLen :
						thatArrLen;
	
					// thisArrLen == thatArrLen
					for (index = 0; index < opLength; index++) {
						if (this.array[index] != 0 || that.array[index] != 0) {
							bits_result = this.array[index] | that.array[index];
							if (bits_result != this.array[index]) {
								if (this_version == this.version) {
									this.AddVersion();
								}
								this.array[index] = bits_result;
							}
						}
						temp_count += BitSetArray.CountOnBits(this.array[index]);
					}
					// thisArrLen > thatArrLen
					for (index = opLength; index < thisArrLen; index++) {
						temp_count += BitSetArray.CountOnBits(this.array[index]);
					}
					// This operation can only optionally increase number of members
					if (this.count != temp_count) {
						this.count = temp_count;
					}
					// no false bit above result.Length will ever change into true
					// Positive logic -> no need to ClearTail()
				}
			}
			return this;
		}

		public BitSetArray Xor(BitSetArray that)
		{
			Contract.Ensures(
				Theory.Xor(Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)), that, this, Contract.Result<BitSetArray>())
			);

			if (this.Is(that)) {
				// A.Xor(A)=∅
				this.SetAll(false); // that will change version too
			} else if (that.IsNull() || that.Count == 0) {
				// A.Xor(∅)=A
				// no change
			} else if (this.count == 0) {
				// ∅.Xor(B)=B
				// copy that to this
				Contract.Assert(that.count != 0);

				lock (SyncRoot) {

					this.AddVersion();
					this.count = that.count;
					this.range = this.range < that.range ?
						that.range :
						this.range;
					if (this.array.Length < BitSetArray.GetLongArrayLength(this.range)) {
						this.array = new long[BitSetArray.GetLongArrayLength(this.range)];
					}
					Array.Copy(that.array, this.array, that.array.Length);
				}
			}
			// Xor ^ => SymmetricExceptWith ⊻
			else {

				Contract.Assert(this.count != 0);
				Contract.Assert(that.count != 0);

				lock (SyncRoot) {

					int this_version = this.version;
					long bits_result = 0;
	
					//if ((int)this.Last < (int)that.Last) { this.Length = (int)that.Last+1; }
					if (this.Length < that.Length) {
						this.Length = that.Length;
					}
	
					int thisArrLen = BitSetArray.GetLongArrayLength(this.range);
					int thatArrLen = BitSetArray.GetLongArrayLength(that.range);
					Contract.Assert(thisArrLen >= thatArrLen);
	
					int index = 0;
					int temp_count = 0;
					int opLength = thisArrLen <= thatArrLen ?
						thisArrLen :
						thatArrLen;
	
					// for thisArrLen <= thatArrLen
					for (index = 0; index < opLength; index++) {
						if (this.array[index] != 0 || that.array[index] != 0) {
							bits_result = this.array[index] ^ that.array[index];
							if (bits_result != this.array[index]) {
								if (this_version == this.version) {
									this.AddVersion();
								}
								this.array[index] = bits_result;
							}
						}
						temp_count += BitSetArray.CountOnBits(this.array[index]);
					}
					// for thisArrLen > thatArrLen
					for (index = opLength; index < thisArrLen; index++) {
						temp_count += BitSetArray.CountOnBits(this.array[index]);
					}
					// This operation can change bit sequence leaving same number of bits set
					if (this.count != temp_count) {
						this.count = temp_count;
					}
					// no false bit above result.Length will ever change into true
					// Positive logic -> no need to ClearTail()
				}
			}

			return this;
		}

		/// <summary>this.XOR(this.AND(that)) == this.AND(that.NOT())
		/// <para>NOT(B) == this.XOR(this.AND(B)) == this.except.that</para>
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		public BitSetArray Not(BitSetArray that)
		{
			Contract.Ensures(
				Theory.Not(Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)), that, this, Contract.Result<BitSetArray>())
			);

			// A-B ==> A.Xor(A.And(B)) ==> A.ExceptWith(B)
			if (this.Is(that)) {
				// A - A == ∅
				this.SetAll(false); // => this.AddVerson();
			} else if (this.count == 0
			           || that.IsNull()
			           || that.IsEmpty()) {
				// ∅ - B == ∅
				// A - ∅ == A
			} else {

				lock (SyncRoot) {
					// A - B == C

					int this_version = this.version;
					long bits_result = 0;

					int thisArrLen = BitSetArray.GetLongArrayLength(this.range);
					int thatArrLen = BitSetArray.GetLongArrayLength(that.range);
	
					int index = 0;
					int temp_count = 0;
					int opLength = thisArrLen <= thatArrLen ?
						thisArrLen :
						thatArrLen;
	
					// thisArrLen <= thatArrLen
					for (index = 0; index < opLength; index++) {
						if (this.array[index] != 0 && that.array[index] != 0) {
							//AND.XOR ==> (A.Xor(B)).And(A)
							//0 0 =& 0 =^ 0
							//1 0 =& 0 =^ 1
							//0 1 =& 0 =^ 0
							//1 1 =& 1 =^ 0
							//bits_result = this.array[index] ^ (this.array[index] & that.array[index]);
		
							//NOT.AND ==> (A.And(B.Not())
							//0 0 ~1 =& 0
							//1 0 ~1 =& 1
							//0 1 ~0 =& 0
							//1 1 ~0 =& 0
							bits_result = this.array[index] & ~(that.array[index]);
							if (bits_result != this.array[index]) {
								if (this_version == this.version) {
									this.AddVersion();
								}
								this.array[index] = bits_result;
							}
						}
						temp_count += BitSetArray.CountOnBits(this.array[index]);
					}
					// thisArrLen > thatArrLen
					for (index = opLength; index < thisArrLen; index++) {
						temp_count += BitSetArray.CountOnBits(this.array[index]);
					}
					// this operation only can optionally decrease number of members
					if (this.count != temp_count) {
						this.count = temp_count;
					}
					// no false bit above result.Length will ever change into true
					// Positive logic -> no need to ClearTail()
				}
			}

			return this;
		}

		/// <summary>Complement within this domain range (Length)
		/// <para>NOT == this.XOR(this.Clone().SetAll(true))</para>
		/// </summary>
		/// <returns></returns>
		public BitSetArray Not()
		{
			Contract.Ensures(
				Theory.Not(
					Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)),
					this,
					Contract.Result<BitSetArray>()
				)
			);

			if (this.range == 0) {
				// nothing to complement - no change
			} else {

				lock (SyncRoot) {

					// This operation will always change bit sequence
					this.AddVersion();

					int opLength = BitSetArray.GetLongArrayLength(this.range);
	
					this.count = this.range - this.count;
//				if (System.Environment.ProcessorCount > 1 && opLength > 64) {
//					Parallel.For(0, opLength, index => {
//						this.array[index] = ~this.array[index];
//					});
//				}
//				else {
					for (int index = 0; index < opLength; index++) {
						this.array[index] = ~this.array[index];
					}
//				}
					// false bit beyond .Length can change into true
					// Not == Negative logic -> ClearTail
					this.ClearTail();
				}
			}
			return this;
		}

		#region ToDo?
		// ToDo? other logic Nand Nor
		// equivalence x≡y = ¬(x⊕y) = ~(a.Xor(b)) => Nxor
		// implication x→y = ¬x∨y
		// http://en.wikipedia.org/wiki/Logical_connective
		// http://en.wikipedia.org/wiki/Logic_operation
		// http://en.wikipedia.org/wiki/Table_of_logic_symbols
		// http://en.wikipedia.org/wiki/Set_%28mathematics%29
		//
		//		public BitSetArray Nand(BitSetArray that) {
		//			Contract.Ensures(
		//				BitSetArray.Ensures.Nand(
		//					Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)),
		//					this,
		//					Contract.Result<BitSetArray>(),
		//					that
		//				)
		//			);
		//
		//			if (this.Is(that)) {
		//				// A.Nand(A)=~A
		//				this.Not(); // .Not will change version
		//			}
		//			else if (that.IsNull() that.Count == 0) {
		//				// A.Nand(∅)=~A
		//				this.Not(); // .Not will change version
		//			}
		//			else if (this.count == 0) {
		//				// ∅.Nand(B)=~B
		//				// copy that to this
		//				Contract.Assert(that.count != 0);
		//				this.count = that.count;
		//				this.range = this.range < that.range ?
		//					  that.range :
		//					  this.range;
		//				if (this.array.Length < BitSetArray.GetLongArrayLength(this.range)) {
		//					this.array = new long[BitSetArray.GetLongArrayLength(this.range)];
		//				}
		//				Array.Copy(that.array, this.array, that.array.Length);
		//				this.Not();
		//			}
		//			// Nand
		//			else {
		//				Contract.Assert(this.count != 0 && that.count != 0);
		//				int init_version = this.version;
		//
		//				if (this.Length < that.Length) { this.Length = that.Length; }
		//
		//				int thisArrLen = BitSetArray.GetLongArrayLength(this.range);
		//				int thatArrLen = BitSetArray.GetLongArrayLength(that.range);
		//				Contract.Assert(thisArrLen >= thatArrLen);
		//
		//				int index = 0;
		//				int temp_count = 0;
		//				int opLength = thisArrLen <= thatArrLen ?
		//					  thisArrLen :
		//					  thatArrLen;
		//				long temp_bitmask = 0;
		//
		//				// for thisArrLen <= thatArrLen
		//				for (index = 0; index < opLength; index++) {
		//					if (init_version == this.version) {
		//						temp_bitmask = this.array[index];
		//					}
		//					if (this.array[index] != -1 || that.array[index] != -1) {
		//						this.array[index] = ~(this.array[index] & that.array[index]);
		//					}
		//					temp_count += BitSetArray.CountOnBits(this.array[index]);
		//					if (init_version == this.version && temp_bitmask != this.array[index]) {
		//						this.AddVerson();
		//					}
		//				}
		//				  // same version and bit sequence will change?
		//				  if ( init_version == this.version && thisArrLen > thatArrLen ) {
		//					  this.AddVerson(); // if (thisArrLen > thatArrLen) ~ will change sequence
		//				}
		//				// loop if thisArrLen > thatArrLen
		//				for (index = opLength; index < thisArrLen; index++) {
		//					  this.array[index] = ~this.array[index]
		//					temp_count += BitSetArray.CountOnBits(this.array[index]);
		//				}
		//				// This operation can change bit sequence leaving same number of bits set
		//				if (this.count != temp_count) {
		//					this.count = temp_count;
		//				}
		//				// false bit beyond .Length can change into true
		//				// Nand == Negative logic -> ClearTail
		//				this.ClearTail();
		//			}
		//
		//			return this;
		//		}
		#endregion

		#endregion

		#region Pure Set Relations

		[Pure]
		public bool SequenceEqual(BitSetArray that)
		{
			return SetEquals(that);
		}

		[Pure]
		public bool SetEquals(BitSetArray that)
		{
			Contract.Ensures(
				Theory.SetEquals(this, Contract.Result<bool>(), that));

			bool isSetEqual;

			if (that.Is(null) || that.Count == 0) {
				if (this.count == 0)
					isSetEqual = true; // both empty
				else
					isSetEqual = false; // that empty
			} else if (this.Count == 0) {
				isSetEqual = false; // this empty
			} else if (this.Is(that)) {
				isSetEqual = true; // same instance, none null/empty
			} else { // none empty
				if (this.count != that.count) {
					isSetEqual = false; // not same count
				} else {
					Contract.Assert(this.count != 0);
					Contract.Assert(this.count == that.count);
					Contract.Assert(this.IsNot(that)); // ATTN! using "this != that" will create infinite loop

					isSetEqual = true;

					int thisArrLen = BitSetArray.GetLongArrayLength(this.range);
					Contract.Assume(thisArrLen <= this.array.Length); // for static checker

					int thatArrLen = BitSetArray.GetLongArrayLength(that.range);
					Contract.Assume(thatArrLen <= that.array.Length); // for static checker

					int shorterArrLen = thisArrLen <= thatArrLen ?
						thisArrLen :
						thatArrLen;
					long[] shorter = thisArrLen <= thatArrLen ?
						this.array :
						that.array;
					long[] longer = thisArrLen > thatArrLen ?
						this.array :
						that.array;

					// to be set-equal, bits must be equal within shorter (overlaped) array
					for (int i = 0; i < shorterArrLen; i++) {
						if (shorter[i] != longer[i]) {
							isSetEqual = false;
							break;
						}
					}
				}
			}
			return isSetEqual;
		}

		[Pure]
		public bool Overlaps(BitSetArray that)
		{
			Contract.Ensures(Theory.Overlaps(this, Contract.Result<bool>(), that));

			bool hasOverlap;

			if (this.count == 0
			    || that.IsNull()
			    || that.IsEmpty()) {
				hasOverlap = false; // one empty
			} else if (this.Is(that)) {
				hasOverlap = true; // same instance - not empty
			} else { // none empty, not same instance
				hasOverlap = false;
				// to have overlap, bits must overlap within shorter array
				int thisArrayLength = BitSetArray.GetLongArrayLength(this.range);
				int thatArrayLength = BitSetArray.GetLongArrayLength(that.range);
				int overlapedLength = thisArrayLength <= thatArrayLength ?
					thisArrayLength : thatArrayLength;
				for (int i = 0; i < overlapedLength; i++) {
					if ((this.array[i] & that.array[i]) != 0) {
						hasOverlap = true;
						break;
					}
				}
			}
			return hasOverlap;
		}

		[Pure]
		public bool IsSupersetOf(BitSetArray that)
		{
			Contract.Ensures(Theory.IsSupersetOf(this, Contract.Result<bool>(), that));

			bool isSuperset;

			if (this.count == 0
			    || that.IsNull()
			    || that.count == 0) {
				isSuperset = false; // empty is never subset nor superset
			} else if (this.Is(that)) {
				isSuperset = true; // same instance - not empty
			} else if (this.count < that.count) {
				isSuperset = false;
			} else {
				// condition this.count >= that.count is satisfied
				isSuperset = true; // assume true
				int thisArrLen = BitSetArray.GetLongArrayLength(this.range);
				int thatArrLen = BitSetArray.GetLongArrayLength(that.range);
				int testLength = thisArrLen <= thatArrLen ?
					thisArrLen :
					thatArrLen;
				int i;
				for (i = 0; i < (testLength); i++) {
					// if any that.member & this.not-member then this.IsNotSupersetOf(that)
					if (((~this.array[i]) & (that.array[i])) != 0) {
						isSuperset = false;
						break;
					}
				}
				if (isSuperset) {
					for (i = testLength; i < thatArrLen; i++) {
						// if any that.member (& this.not-member) then this.IsNotSupersetOf(that)
						if (that.array[i] != 0) {
							isSuperset = false;
							break;
						}
					}
				}
			}
			return isSuperset;
		}

		[Pure]
		public bool IsProperSupersetOf(BitSetArray that)
		{
			Contract.Ensures(Theory.IsProperSupersetOf(this, Contract.Result<bool>(), that));

			if (that.IsNot(null)) {
				if (this.count <= that.count) {
					return false;
				}
				return this.IsSupersetOf(that);
			}
			return false;
		}

		[Pure]
		public bool IsSubsetOf(BitSetArray that)
		{
			Contract.Ensures(Theory.IsSubsetOf(this, Contract.Result<bool>(), that));

			if (that.IsNot(null)) {
				return that.IsSupersetOf(this);
			}
			return false;
		}

		[Pure]
		public bool IsProperSubsetOf(BitSetArray that)
		{
			Contract.Ensures(Theory.IsProperSubsetOf(this, Contract.Result<bool>(), that));

			if (that.IsNot(null)) {
				return that.IsProperSupersetOf(this);
			}
			return false;
		}

		#endregion

		#region Convert To...

		[Pure]
		public long[] To64BitMask()
		{
			Contract.Ensures(Contract.Result<long[]>() != null);
			Contract.Ensures(Contract.Result<long[]>().Length == BitSetArray.GetLongArrayLength(this.range));
			Contract.Ensures(BitSetArray.CountOnBits(Contract.Result<long[]>()) == this.count);

			return (long[])this.array.Clone();
		}

		[Pure]
		public int[] To32BitMask()
		{
			Contract.Ensures(Contract.Result<int[]>() != null);
			Contract.Ensures(Contract.Result<int[]>().Length == BitSetArray.GetIntArrayLength(this.range));
			Contract.Ensures(BitSetArray.CountOnBits(Contract.Result<int[]>()) == this.count);

			var intArray = new int[BitSetArray.GetIntArrayLength(this.range)];
			for (int i = 0; i < intArray.Length; i++) {
				if ((i & 1) == 0) {
					intArray[i] = unchecked ((int)(this.array[(i >> 1)] & 0xFFFFFFFFL)); // even - takes low 32 bits
				} else {
					intArray[i] = unchecked ((int)(((ulong)this.array[(i >> 1)]) >> 32)); // odd - takes high 32 bits
				}
			}
			return intArray;
		}

		[Pure]
		public short[] To16BitMask()
		{
			Contract.Ensures(Contract.Result<short[]>() != null);
			Contract.Ensures(Contract.Result<short[]>().Length == BitSetArray.GetShortArrayLength(this.range));
			Contract.Ensures(BitSetArray.CountOnBits(Contract.Result<short[]>()) == this.count);

			var shortArray = new short[BitSetArray.GetShortArrayLength(this.range)];
			for (int i = 0; i < shortArray.Length; i++) {
				shortArray[i] = unchecked ((short)((((ulong)this.array[(i >> 2)]) >> ((i & 3) * shortBits)) & 0xFFFFul));
			}
			return shortArray;
		}

		[Pure]
		public byte[] To8BitMask()
		{
			Contract.Ensures(Contract.Result<byte[]>() != null);
			Contract.Ensures(Contract.Result<byte[]>().Length == BitSetArray.GetByteArrayLength(this.range));
			Contract.Ensures(BitSetArray.CountOnBits(Contract.Result<byte[]>()) == this.count);

			var byteArray = new byte[BitSetArray.GetByteArrayLength(this.range)];
			for (int i = 0; i < byteArray.Length; i++) {
				byteArray[i] = (byte)(((this.array[(i >> 3)]) >> ((i & 7) * byteBits)) & 0xFF);
			}
			return byteArray;
		}

		[Pure]
		public bool[] ToBoolMask()
		{
			Contract.Ensures(Contract.Result<bool[]>() != null);
			Contract.Ensures(Contract.Result<bool[]>().Length == this.range);
			Contract.Ensures(BitSetArray.CountOnBits(Contract.Result<bool[]>()) == this.count);

			var boolArray = new bool[this.range];
			for (int i = 0; i < boolArray.Length; i++) {
				if (this[i]) {
					boolArray[i] = true;
				}
			}
			return boolArray;
		}

		[Pure]
		public BitArray ToBitArray()
		{
			Contract.Ensures(Contract.Result<BitArray>() != null);
			Contract.Ensures(Contract.Result<BitArray>().Length == this.range);
			Contract.Ensures(BitSetArray.CountOnBits(Contract.Result<BitArray>()) == this.count);

			var bitArray = new BitArray(this.To32BitMask());
			Contract.Assert(bitArray.Length >= this.range);
			Contract.Assert(bitArray.Length <= this.array.Length * longBits);
			if (bitArray.Length != this.range) {
				bitArray.Length = this.range;
			}
			return bitArray;
		}

		[Pure]
		public int[] ToItems()
		{
			Contract.Ensures(Contract.Result<int[]>() != null);
			Contract.Ensures(Contract.Result<int[]>().Length == this.count);

			var itemsArray = new int[this.count];
			int counter = 0;
			foreach (var item in this) {
				itemsArray[counter] = item;
				++counter;
			}
			return itemsArray;
		}

		#endregion

		#region Other

		[Pure]
		public bool Get(int item)
		{
			Contract.Requires<ArgumentOutOfRangeException>(ValidMember(item));
			Contract.Requires<IndexOutOfRangeException>(this.InRange(item));
			Contract.Ensures(Theory.Get(this, item, Contract.Result<bool>()));

			return _Get(item);
		}

		[Pure]
		internal bool _Get(int item)
		{
			Contract.Requires<ArgumentOutOfRangeException>(ValidMember(item));
			Contract.Requires<IndexOutOfRangeException>(this.InRange(item));
			Contract.Ensures(Theory.Get(this, item, Contract.Result<bool>()));

			bool value;
			lock (SyncRoot) {
				Contract.Assert(this.InRange(item), "Race condition");
				value = 0 != (array[item >> log2of64] & 1L << (item & mask0x3F));
			}
			return value;
		}

		public bool Set(int item, bool value = true)
		{
			Contract.Requires<ArgumentOutOfRangeException>(ValidMember(item));
			Contract.Requires<IndexOutOfRangeException>(this.InRange(item));
			Contract.Ensures(Theory.Set(Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)), item, value, this));

			return _Set(item, value);
		}

		internal bool _Set(int item, bool value = true)
		{
			Contract.Requires<ArgumentOutOfRangeException>(ValidMember(item));
			Contract.Requires<IndexOutOfRangeException>(this.InRange(item));
			Contract.Ensures(Theory.Set(Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)), item, value, this));

			value = value.Bool();
			bool isChanged = false;

			lock (SyncRoot) {

				Contract.Assert(this.InRange(item), "Race condition");
				if (((array[item >> log2of64] & 1L << (item & mask0x3F)) != 0) == value) {
					// no change
				} else {
					// flip bit value
					int live_version = this.Version;
					this.AddVersion();
					array[item >> log2of64] ^= 1L << (item & mask0x3F);

					if (value) {
						Contract.Assert(this.count < this.range);
						++this.count; // count cannot be 0 after this line
						if (this.count == 1) {
							this.FirstSet(item);
							this.LastSet(item);
						} else {
							Contract.Assert(this.count > 1);
							if (this.startVersion == live_version) {
								// cache is not expired
								if (item < (int)this.startMemoize) {
									this.startMemoize = item;
								}
								this.startVersion = this.version;
							}
							if (this.finalVersion == live_version) {
								// cache is not expired
								if (item > (int)this.finalMemoize) {
									this.finalMemoize = item;
								}
								this.finalVersion = this.version;
							}
						}
					} else {
						Contract.Assert(this.count > 0);
						--this.count;
						if (this.startVersion == live_version) {
							// cache is not expired
							if (item != (int)this.startMemoize) {
								// cache is alive
								this.startVersion = this.version;
							}
						}
						if (this.finalVersion == live_version) {
							// cache is not expired
							if (item != (int)this.finalMemoize) {
								// cache is alive 
								this.finalVersion = this.version;
							}
						}
					}
					Contract.Assert(this[item] == value);
					isChanged = true;
				}
			}
			return isChanged;
		}

		#if DEBUG
		[CLSCompliant(false)]
		public
		
#else
		private
		#endif
		void _SetItems(IEnumerable<int> items)
		{
			Contract.Requires<ArgumentNullException>(items != null);
			Contract.Requires<ArgumentEmptyException>(!items.IsEmpty());
			Contract.Requires<ArgumentOutOfRangeException>(ValidMembers(items));
			Contract.Requires<InvalidOperationException>(Contract.ForAll(items, item => item < this.Length));
			Contract.Requires<InvalidOperationException>(this.Count == 0);

			lock (SyncRoot) {

				this.AddVersion(); // (!items.IsEmpty() && this.Count == 0)
				int minValue = int.MaxValue;
				int maxValue = int.MinValue;
				foreach (var item in items) {
					if ((array[item >> log2of64] & 1L << (item & mask0x3F)) != 0) {
						// no bit change
					} else {
						// set bit value
						array[item >> log2of64] ^= 1L << (item & mask0x3F);
						Contract.Assert(this.count < this.range);
						this.count += 1;
						if (item < minValue) {
							minValue = item;
						}
						if (item > maxValue) {
							maxValue = item;
						}
					}
				}
				Contract.Assert(minValue != int.MaxValue);
				Contract.Assert(maxValue != int.MinValue);
				this.FirstSet(minValue);
				this.LastSet(maxValue);
			}
		}

		public void SetAll(bool value)
		{
			Contract.Requires<ArgumentOutOfRangeException>(value == value.Bool());

			Contract.Ensures(Theory.SetAll(Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)), value, this));

			if (this.array.Length != 0) {
				value = value.Bool();

				lock (SyncRoot) {

					int rangeLength = BitSetArray.GetLongArrayLength(this.range);
					if (value) {
						if (this.count != this.range) {
							this.AddVersion();
							this.count = this.range;
							for (int i = 0; i < rangeLength; i++) {
								if (this.array[i] != -1L)
									this.array[i] = -1L;
							}
							this.ClearTail();
							this.FirstSet(0);
							this.LastSet(this.range - 1);
						}
					} else {
						if (this.count != 0) {
							this.AddVersion();
							this.count = 0;
							for (int i = 0; i < rangeLength; i++) {
								if (this.array[i] != 0)
									this.array[i] = 0;
							}
						}
					}
				}
			}
		}

		public void TrimExcess()
		{
			Contract.Ensures(Theory.TrimExcess(Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)), this));

			lock (SyncRoot) {
				int trimmedRange = 0;
				if (this.count != 0) {
					int? last = this.Last;
					Contract.Assert(last != null);
					trimmedRange = (int)last;
					++trimmedRange;
				}
				int newArrayLength = BitSetArray.GetLongArrayLength(trimmedRange);
				if (this.array.Length != newArrayLength) {
					Contract.Assert(this.array.Length > newArrayLength);
					long[] newArray = new long[newArrayLength];
					Array.Copy(this.array, newArray, newArrayLength);
					this.array = newArray;
				}
				this.range = trimmedRange;
			}
		}

		/// <summary>Check if zero &lt;= item &lt; this.Length</summary>
		/// <remarks>If this.InRange(item) then item can be set or cleared withour resizing</remarks>
		/// <param name="item">value to test</param>
		/// <returns>true if in range</returns>
		[Pure] public bool InRange(int item)
		{
			return (item >= 0)
			&& (item < this.range);
		}

		#endregion

		#endregion

		#region Interfaces

		#region ICloneable

		/// <summary>Implements ICloneable
		/// <para>No exception is thrown.</para>
		/// </summary>
		/// <returns>Object. Deep copy of current instance</returns>
		[Pure]
		public object Clone()
		{
			Contract.Ensures(Contract.Result<object>() != null);
			Contract.Ensures(Theory.IsCopy((BitSetArray)Contract.Result<object>(), this));

			return new BitSetArray(this);
		}

		#endregion

		#region IEnumerable<int>

		/// <summary>Implements IEnumerable.GetEnumerator
		/// </summary>
		/// <returns>IEnumerable</returns>
		[Pure]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Implements IEnumerable{int}.GetEnumerator
		/// </summary>
		/// <returns>IEnumerable{int}</returns>
		[Pure]
		public IEnumerator<int> GetEnumerator()
		{
			return new BitSetArray.EnumeratorForwardSynchronized(this);
		}

		[Pure]
		public IEnumerator<int> GetEnumeratorReverse()
		{
			return new BitSetArray.EnumeratorReverseSynchronized(this);
		}

		[Pure]
		public IEnumerator<int> GetEnumeratorComplement()
		{
			return new BitSetArray.EnumeratorComplementSynchronized(this);
		}

		[Pure]
		public IEnumerable<int> Complement()
		{
			IEnumerator<int> enumerator = this.GetEnumeratorComplement();
			while (enumerator.MoveNext()) {
				yield return enumerator.Current;
			}
			enumerator.Dispose();
		}

		[Pure]
		public IEnumerable<int> Reverse()
		{
			IEnumerator<int> enumerator = this.GetEnumeratorReverse();
			while (enumerator.MoveNext()) {
				yield return enumerator.Current;
			}
			enumerator.Dispose();
		}

		#endregion

		#region IEquatable<Self>

		/// <summary>Implements IEquatable.Equals
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		[Pure]
		public override bool Equals(Object that)
		{

			if (this.Is(that)) {
				return true;
			}
			if (this.count == 0
			    && that.Is(null)) {
				return true;
			}

			var thatSet = that as BitSetArray;
			if (thatSet.IsNot(null)) {
				return this.SetEquals(thatSet);
			}
			var thatIEnum = that as IEnumerable<int>;
			if (thatIEnum != null) {
				return this.SetEquals(thatIEnum);
			}
			return false;
		}

		/// <summary>Implements IEquatable&lt;BitSetArray&gt;
		/// <para>BitSetArray equality is defined by Set/Value equality, not only by reference.</para>
		/// </summary>
		/// <param name="that"></param>
		/// <returns>Bool. True if SetEquals</returns>
		[Pure]
		public bool Equals(BitSetArray that)
		{

			return this.SetEquals(that);
		}

		/// <summary>Implements IEquatable.GetHashCode
		/// <para>No exception is thrown.</para>
		/// </summary>
		/// <returns>int</returns>
		[Pure]
		public override int GetHashCode()
		{
			if (this.Count == 0) {
				return 0;
			} else {
				return unchecked ((int)this.First << 2) ^ unchecked (this.Count << 1) ^ ((int)this.Last);
			}
		}

		#endregion

		#region IComparable<Self>

		/// <summary>Implements IComparable&lt;BitSetArray&gt;
		/// <para>Greater contains larger binary value (defined by value of bit sequence)</para>
		/// <para>I.e. ulong value is defined by 64 bits, BitSetArray value is defined by up to int.MaxValue bits</para>
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		public int CompareTo(BitSetArray that)
		{
			int compare = 0;

			if (that.IsNull() || that.IsEmpty()) {
				compare = this.count > 0 ? 1 : 0;
			} else {
				// Untrimmed array.Length can be equal or (much) larger than Length required by range
				// shorter	 = <-range.Length----->|------------------>|array.Length
				// longer	  = <-range.Length----------->|---->|array.Length
				// first loop:						<----|
				// second loop:  <-------------------|
				int thisArrLen = BitSetArray.GetLongArrayLength(this.range);
				int thatArrLen = BitSetArray.GetLongArrayLength(that.range);

				int longerArrLen = thisArrLen > thatArrLen ? thisArrLen : thatArrLen;
				int shorterArrLen = thisArrLen <= thatArrLen ? thisArrLen : thatArrLen;

				long[] longer = thisArrLen > thatArrLen ? this.array : that.array;

				// not overlaping part
				Contract.Assume(longer != null);
				Contract.Assume(longerArrLen <= longer.Length);
				Contract.Assume(shorterArrLen >= 0);
				for (int i = longerArrLen - 1; i >= shorterArrLen; i--) {
					if (longer[i] != 0) {
						compare = longer.Is(this.array) ? 1 : -1;
						break;
					}
				}
				// overlaping part
				if (compare == 0) {
					for (int i = shorterArrLen - 1; i >= 0; i--) {
						if (this.array[i] != that.array[i]) {
							compare = unchecked ((ulong)this.array[i] > (ulong)that.array[i]) ? 1 : -1;
							break;
						}
					}
				}
			}

			return compare;
		}

		#endregion

		#region ICollection<int>

		/// <summary>Implements ICollection{int}.Add
		/// Throws <exception cref="System.ArgumentOutOfRangeException"/> if item &lt; 0 || item == int.MaxValue
		/// <remarks>
		/// <para>ICollection.Add return value is void, so only way to return error to caller is exception.</para>
		/// </remarks>
		/// </summary>
		/// <param name="item"></param>
		public void Add(int item)
		{
			Contract.Ensures(Theory.ICollectionAdd(Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)), item, this));

			if (!ValidMember(item)) {
				throw new ArgumentOutOfRangeException();
			}
			if (item >= this.range) {
				this.Length = item + 1;
			}
			this._Set(item, true);
		}

		/// <summary>Implements ICollection{int}.Remove
		/// <para>No exceptions thrown.</para>
		/// </summary>
		/// <param name="item">int</param>
		/// <returns>bool. True if item is found and removed.</returns>
		public bool Remove(int item)
		{
			Contract.Ensures(Theory.Remove(Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)), item, this, Contract.Result<bool>()));

			if (this[item]) {
				return this._Set(item, false);
			}
			return false; // not found
		}

		/// <summary>Implements ICollection{int}.Clear
		/// </summary>
		public void Clear()
		{
			Contract.Ensures(Theory.Clear(Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)), this));

			this.SetAll(false);
		}

		/// <summary>Implements ICollection{int}.Contains
		/// <para>No exceptions thrown</para>
		/// </summary>
		/// <param name="item">int</param>
		/// <returns>bool. True if item is found.</returns>
		[Pure]
		public bool Contains(int item)
		{
			Contract.Ensures(Theory.Contains(this, Contract.Result<bool>(), item));

			Contract.Assume(this[item] == false || this.Count > 0); // assume inherited Contract.Ensures
			return this[item];
		}

		/// <summary>Implements ICollection{int}.CopyTo
		/// <para>Throws ArgumentNullException if array.Is(null)</para>
		/// <para>Throws ArgumentOutOfRangeException if index &lt; 0 || index &gt;= (array.Length - this.Count)</para>
		/// </summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		[Pure]
		[SuppressMessage("Microsoft.Contracts", "CC1033", Justification = "Debug/Release exceptions not same")]
		public void CopyTo(int[] array, int arrayIndex)
		{
			Contract.Requires<ArgumentNullException>(array != null);
			Contract.Requires<ArgumentOutOfRangeException>(arrayIndex >= 0);
			Contract.Requires<ArgumentOutOfRangeException>(arrayIndex <= (array.Length - this.Count));

			Contract.Ensures(Theory.CopyToArrayOfInt(this, array, arrayIndex));
			int index = arrayIndex;
			foreach (int item in this) {
				Contract.Assume(index < array.Length);
				array[index] = item;
				++index;
			}
		}

		/// <summary>Implements ICollection.CopyTo
		/// <para>Throws ArgumentNullException if array.Is(null)</para>
		/// <para>Throws ArgumentException if array.Rank != 1</para>
		/// <para>Throws ArgumentException if !(array is int[])</para>
		/// <para>Throws ArgumentOutOfRangeException if index &lt; 0 || index &gt;= (array.Length - this.Count)</para>
		/// </summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		[Pure]
		[SuppressMessage("Microsoft.Contracts", "CC1033", Justification = "Debug/Release exceptions not same")]
		public void CopyTo(Array array, int arrayIndex)
		{
			Contract.Requires<ArgumentNullException>(array != null);
			Contract.Requires<ArgumentException>(array.Rank == 1);
			Contract.Requires<ArgumentException>(array is int[]);
			Contract.Requires<ArgumentOutOfRangeException>(arrayIndex >= 0);
			Contract.Requires<ArgumentOutOfRangeException>(arrayIndex <= (array.Length - this.Count));

			Contract.Ensures(Theory.CopyToArrayOfObj(this, array, arrayIndex));

			int index = arrayIndex;
			foreach (int item in this) {
				Contract.Assume(index < array.Length);
				array.SetValue((object)item, index);
				++index;
			}
		}

		#endregion

		#region ISet<int>

		#region Operations

		/// <summary>Implements ISet{int}.Add
		/// </summary>
		/// <param name="item"></param>
		/// <returns>bool, true if item added.</returns>
		bool ISet<int>.Add(int item)
		{
			Contract.Ensures(
				Theory.Add(Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)), item, this, Contract.Result<bool>()));

			if (ValidMember(item)) {
				if (!this[item]) {
					if (item >= this.range) {
						this.Length = item + 1;
					}
					Contract.Assert(this.InRange(item));
					return this._Set(item, true);
				}
			}
			return false;
		}

		/// <summary>Implements ISet{int}.ExceptWith
		/// <remarks>
		/// <para>For this operation is irrelevant wheter IEnumerable{int} is a set (contains no duplicates) or not.</para>
		/// <para>For this operation argument set IEnumerableOf{int} is not required to be within BitSetArray.domain.</para></remarks>
		/// </summary>
		/// <param name="that"></param>
		public void ExceptWith(IEnumerable<int> that)
		{

			Contract.Ensures(
				Theory.ExceptWith(Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)), that, this)
			);

			var other = that as BitSetArray;
			if (other.IsNot(null)) {
				this.Not(other);
			} else if (this.count == 0
			           || that.IsNull()
			           || that.IsEmpty()) {
				// nothing to ExceptWith
			} else {
				// this will never ExceptWith(that) outside this.Length
				foreach (var item in that) {
					// ExceptWith can simply ignore any value otside this.Length
					if (this.InRange(item)) {
						this._Set(item, false); // -> this.AddVersion
					}
				}
			}
		}

		/// <summary>Implements ISet{int}.IntersectWith
		/// <remarks>
		/// <para>For this operation is irrelevant wheter IEnumerable{int} is a set (contains no duplicates) or not.</para>
		/// <para>For this operation argument set IEnumerableOf{int} is not required to be within BitSetArray.domain.</para></remarks>
		/// </summary>
		/// <param name="that"></param>
		public void IntersectWith(IEnumerable<int> that)
		{

			Contract.Ensures(
				Theory.IntersectWith(Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)), that, this)
			);

			var other = that as BitSetArray;
			if (other.IsNot(null)) {
				this.And(other);
			} else if (this.count == 0) {
				// nothing to IntersectWith
			} else if (that.IsNull() || that.IsEmpty()) {
				Contract.Assert(this.count != 0);
				this.Clear(); // .Clear() will change version
			} else {

				lock (SyncRoot) {

					other = BitSetArray.Size(this.range);
					foreach (var item in that) {
						if (this[item]) {
							other._Set(item, true);
						}
					}
					if (other.count != this.count) {
						this.AddVersion();
						this.array = other.array;
						this.count = other.count;
					}
				}
			}
		}

		/// <summary>Implements ISet{int}.SymmetricExceptWith
		/// <remarks>
		/// <para>For this operation is irrelevant wheter IEnumerable{int} is a set (contains no duplicates) or not.</para>
		/// <para>For this operation argument set IEnumerableOf{int} is not required to be within BitSetArray.domain.</para></remarks>
		/// <para>Since resulting set of SymmetricExceptWith operation MUST be within BitSetArray.domain,</para>
		/// <para>IEnumerableOf{int} argument members outside BitSetArray.domain are ignored.</para></remarks>
		/// </summary>
		/// <param name="that"></param>
		public void SymmetricExceptWith(IEnumerable<int> that)
		{
			Contract.Ensures(
				Theory.SymmetricExceptWith(Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)), that, this)
			);

			var other = that as BitSetArray;
			if (other.IsNot(null)) {
				this.Xor(other);
			} else if (that.IsNull() || that.IsEmpty()) {
				// nothing to SymmetricExceptWith
			} else {
				other = BitSetArray.From(that.Where(ValidMember));
				this.Xor(other);
			}
		}

		/// <summary>Implements ISet{int}.UnionWith
		/// <remarks>
		/// <para>For this operation is irrelevant wheter IEnumerable{int} is a set (contains no duplicates) or not.</para>
		/// <para>For this operation argument set IEnumerableOf{int} is not required to be within BitSetArray.domain.</para></remarks>
		/// <para>Since resulting set of UnionWith operation MUST be within BitSetArray.domain,</para>
		/// <para>IEnumerableOf{int} argument members outside BitSetArray.domain are ignored.</para></remarks>
		/// </summary>
		/// <param name="that"></param>
		public void UnionWith(IEnumerable<int> that)
		{
			Contract.Ensures(
				Theory.UnionWith(Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)), that, this)
			);

			var other = that as BitSetArray;
			if (other.IsNot(null)) {
				this.Or(other);
			} else if (that.IsNull() || that.IsEmpty()) {
				// nothing to UnionWith
			} else {
				other = BitSetArray.From(that.Where(ValidMember));
				this.Or(other);
			}
		}

		#endregion

		#region Relations

		/// <summary>Implements ISet{int}.SetEquals
		/// <para>Returns true if that argument set is set-equal to this instance.</para>
		/// <remarks>
		/// <para>For this operation is irrelevant wheter IEnumerable{int} is a set (contains no duplicates) or not.</para>
		/// <para>For this operation argument set IEnumerableOf{int} MUST NOT be within BitSetArray.domain.</para></remarks>
		/// </summary>
		/// <param name="that">IEnumerable{int}</param>
		/// <returns>bool</returns>
		[Pure]
		public bool SetEquals(IEnumerable<int> that)
		{
			Contract.Ensures(Theory.SetEquals(this, Contract.Result<bool>(), that));

			var other = that as BitSetArray;
			if (other.IsNot(null)) {
				return this.SetEquals(other);
			} else if (this.count == 0
			           &&
			           (
			               that.Is(null)
			               || that.IsEmpty()
			           )) {
				return true; // both empty
			} else if (this.count == 0
			           || (that.Is(null)
			           || that.IsEmpty())) {
				return false; // one empty
			} else {
				// If any member of "that" is outside BitSetArray.domain then two sets cannot be equal.
				foreach (var item in that) {
					if (!ValidMember(item)) {
						return false;
					}
				}
				return this.SetEquals(BitSetArray.From(that));
			}
		}

		/// <summary>Implements ISet{int}.Overlaps
		/// <para>Returns true if at least one item in that argument is member of this instance (overlaps).</para>
		/// <remarks>
		/// <para>For this operation is irrelevant wheter IEnumerable{int} is a set (contains no duplicates) or not.</para>
		/// <para>For this operation argument set IEnumerableOf{int} MUST NOT be within BitSetArray.domain.</para></remarks>
		/// </summary>
		/// <param name="that">IEnumerable{int}</param>
		/// <returns>bool</returns>
		[Pure]
		public bool Overlaps(IEnumerable<int> that)
		{
			Contract.Ensures(Theory.Overlaps(this, Contract.Result<bool>(), that));

			var other = that as BitSetArray;
			if (other.IsNot(null)) {
				return this.Overlaps(other);
			} else if (this.count == 0
			           || that.IsNull()
			           || that.IsEmpty()) {
				return false; // nothing to Overlap
			} else {
				foreach (var item in that) {
					if (this[item]) {
						return true; // at least one member overlaps
					}
				}
				return false;
			}
		}

		/// <summary>Implements ISet{int}.IsSuperSetOf
		/// <para>Return true if "that" argument set is a subset of this instance.</para>
		/// <remarks>
		/// <para>For this operation is irrelevant wheter IEnumerable{int} is a set (contains no duplicates) or not.</para>
		/// <para>For this operation argument set IEnumerableOf{int} MUST NOT be within BitSetArray.domain.</para></remarks>
		/// </summary>
		/// <param name="that">IEnumerable{int}</param>
		/// <returns>bool</returns>
		[Pure]
		public bool IsSupersetOf(IEnumerable<int> that)
		{
			Contract.Ensures(Theory.IsSupersetOf(this, Contract.Result<bool>(), that));

			var other = that as BitSetArray;
			if (other.IsNot(null)) {
				return this.IsSupersetOf(other);
			} else if (this.count == 0
			           || that.Is(null)
			           || that.IsEmpty()) {
				return false; // empty is never subset nor superset
			} else {
				bool isSuperset = true;
				foreach (var item in that) {
					if (!this[item]) {
						isSuperset = false;
						break;
					}
				}
				return isSuperset;
			}
		}

		/// <summary>Implements ISet{int}.IsProperSupersetOf
		/// <para>Returns true if argument is a subset but not equal to this.</para>
		/// <remarks>
		/// <para>For this operation is irrelevant wheter IEnumerable{int} is a set (contains no duplicates) or not.</para>
		/// <para>For this operation argument set IEnumerableOf{int} MUST NOT be within BitSetArray.domain.</para></remarks>
		/// </summary>
		/// <param name="that">IEnumerable{int}</param>
		/// <returns>bool</returns>
		[Pure]
		public bool IsProperSupersetOf(IEnumerable<int> that)
		{
			Contract.Ensures(Theory.IsProperSupersetOf(this, Contract.Result<bool>(), that));

			var other = that as BitSetArray;
			if (other.IsNot(null)) {
				return this.IsProperSupersetOf(other);
			} else if (this.count == 0
			           || that.IsNull()
			           || that.IsEmpty()) {
				return false; // empty is never subset nor superset
			} else {
				// (this)BitSetArray.IsProperSupersetOf(that) only if "that.domain" is same as "BitSetArray.domain"
				// If "that.domain" is not subset of BitSetArray.domain, creation of BitSetArray will fail with exception
				try {
					return this.IsProperSupersetOf(BitSetArray.From(that));
				} catch (ArgumentOutOfRangeException) {
					return false;
				}
			}
		}

		/// <summary>Implements ISet{int}.IsSubsetOf
		/// <para>Returns true if "that" argument is superset of this</para>
		/// <remarks>
		/// <para>For this operation is irrelevant wheter IEnumerable{int} is a set (contains no duplicates) or not.</para>
		/// <para>For this operation argument set IEnumerableOf{int} MUST NOT be within BitSetArray.domain.</para></remarks>
		/// </summary>
		/// <param name="that">IEnumerable{int}</param>
		/// <returns>bool</returns>
		[Pure]
		public bool IsSubsetOf(IEnumerable<int> that)
		{
			Contract.Ensures(Theory.IsSubsetOf(this, Contract.Result<bool>(), that));

			var other = that as BitSetArray;
			if (other.IsNot(null)) {
				return this.IsSubsetOf(other);
			} else if (this.count == 0
			           || that.IsNull()
			           || that.IsEmpty()) {
				return false; // empty is never subset nor superset
			} else {
				// that.domain can be wider/larger than BitSetArray.domain
				// but test is required only within BitSetArray.domain

				// find last member to prevent multiple resizing of testSet
				int maxMember = int.MinValue;
				foreach (int item in that) {
					if (item > maxMember)
						maxMember = item;
				}
				if (ValidMember(maxMember)) {
					other = BitSetArray.From(maxMember);
					// add members to test set
					foreach (var item in that) {
						if (ValidMember(item)
						    && !other[item]) {
							other._Set(item, true);
						}
					}

					return this.IsSubsetOf(other);
				}
				return false;
			}

		}

		/// <summary>Member of ISet{int} interface
		/// <para></para>
		/// <remarks>
		/// <para>Empty set is never subset or superset</para>
		/// <para>For this operation is irrelevant wheter IEnumerable{int} is a set (contains no duplicates) or not.</para>
		/// <para>For this operation argument set IEnumerableOf{int} MUST NOT be within BitSetArray.domain.</para></remarks>
		/// </summary>
		/// <param name="that">IEnumerable{int}</param>
		/// <returns>bool</returns>
		[Pure]
		public bool IsProperSubsetOf(IEnumerable<int> that)
		{
			Contract.Ensures(Theory.IsProperSubsetOf(this, Contract.Result<bool>(), that));

			var other = that as BitSetArray;
			if (other.IsNot(null)) {
				return this.IsProperSubsetOf(other);
			} else if (this.count == 0
			           || that.IsNull()
			           || that.IsEmpty()) {
				return false; // empty is never subset nor superset
			} else {
				try {
					return (BitSetArray.From(that)).IsProperSupersetOf(this);
				} catch (ArgumentOutOfRangeException) {
					// "that" contains members outside BitSetArray.domain
					// consequently, if this.IsSubsetOf(that) => that.IsProperSupersetOf(this)
					return this.IsSubsetOf(that);
				}
			}
		}

		#endregion

		#endregion

		#endregion

		#region Local

		/// <summary>
		/// int
		/// </summary>
		void ClearTail()
		{
			Contract.Ensures(
				Theory.IsTailCleared(this)
			);

			lock (SyncRoot) {

				int rangeLength = BitSetArray.GetLongArrayLength(this.range);
	
				// clear tail bits
				// if this.array.Length==0 => this.range==0 => !((this.range&longMask)!= 0)
				if (((this.range & mask0x3F) != 0) && ((this.array[rangeLength - 1] & (-1L << (this.range & mask0x3F))) != 0)) {
					// ATTN: -1L (0xFFFFFFFFFFFFFFFF) >> 63 !=  0x0000000000000001
					// ATTN: -1L (0xFFFFFFFFFFFFFFFF) >> 63 ==  0xFFFFFFFFFFFFFFFF (-1L)
					this.array[rangeLength - 1] &= unchecked ((long)(ulong.MaxValue >> (longBits - (this.range & mask0x3F))));
					// checked by Ensures: Contract.Assert((this.array[rangeLength - 1] & (-1L << (this.range & longMask))) == 0);
				}
				// clear tail words(long)
				// if this.array.Length==0 => !(this.array.Length>rangeLength)
				if (this.array.Length > rangeLength) { // tail exists
					for (int i = rangeLength; i < this.array.Length; i++) {
						if (this.array[i] != 0) {
							this.array[i] = 0;
						}
					}
				}
			}
		}

		private void AddVersion()
		{
			Contract.Ensures(this.version != Contract.OldValue<int>(this.version));
			lock (SyncRoot) {
				unchecked {
					++this.version;
				}
			}
		}

		#endregion

		#endregion

		#region Operators

		#region Cast Operators

		public static explicit operator BitArray(BitSetArray a)
		{
			return a.IsNull() ? new BitArray(0) : a.ToBitArray();
		}

		public static explicit operator BitSetArray(BitArray a)
		{
			return a.IsNull() ? new BitSetArray() : BitSetArray.Mask(a, a.Count);
		}

		#endregion

		#region Set Operators

		public static BitSetArray operator |(BitSetArray a, BitSetArray b)
		{
			return a.IsNull() ? b : (BitSetArray.Copy(a)).Or(b);
		}

		public static BitSetArray operator &(BitSetArray a, BitSetArray b)
		{
			return a.IsNull() ? new BitSetArray() : (BitSetArray.Copy(a)).And(b);
		}

		public static BitSetArray operator ^(BitSetArray a, BitSetArray b)
		{
			return a.IsNull() ? b : (BitSetArray.Copy(a)).Xor(b);
		}

		public static BitSetArray operator -(BitSetArray a, BitSetArray b)
		{
			return a.IsNull() ? new BitSetArray() : (BitSetArray.Copy(a)).Not(b);
		}

		public static BitSetArray operator ~(BitSetArray a)
		{
			return a.IsNull() ? new BitSetArray() : (BitSetArray.Copy(a)).Not();
		}

		#endregion

		#region Comparators

		public static bool operator ==(BitSetArray a, BitSetArray b)
		{
			if (a.IsNull()) {
				if (b.IsNull() || b.Count == 0) {
					return true;
				} else {
					return false;
				}
			} else {
				return a.SetEquals(b);
			}
		}

		public static bool operator !=(BitSetArray a, BitSetArray b)
		{
			return !(a == b);
		}

		public static bool operator <=(BitSetArray a, BitSetArray b)
		{
			if (a.IsNull()) {
				if (b.IsNull() || b.Count == 0) {
					return true;
				} else {
					return true;
				}
			} else {
				switch (a.CompareTo(b)) {
					case 0:
						return true;
					case -1:
						return true;
					default:
						return false;
				}
			}
		}

		public static bool operator >=(BitSetArray a, BitSetArray b)
		{
			if (a.IsNull()) {
				if (b.IsNull() || b.Count == 0) {
					return true;
				} else {
					return false;
				}
			} else {
				switch (a.CompareTo(b)) {
					case 0:
						return true;
					case 1:
						return true;
					default:
						return false;
				}
			}
		}

		public static bool operator >(BitSetArray a, BitSetArray b)
		{
			return !(a <= b);
		}

		public static bool operator <(BitSetArray a, BitSetArray b)
		{
			return !(a >= b);
		}

		#endregion

		#endregion

		#region Properties

		/// <summary>Get/Set Length
		/// <remarks>Setting .Length smaller will not deallocate array space. Use TrimmExccess to dealocate</remarks>
		/// </summary>
		public int Length {
			[Pure]
			get {
				Contract.Ensures(Theory.LengthGet(this, Contract.Result<int>()));
				return this.range;
			}
			set {
				Contract.Requires<ArgumentOutOfRangeException>(ValidLength(value));

				Contract.Ensures(Theory.LengthSet(Contract.OldValue<BitSetArray>(BitSetArray.Copy(this)), value, this));

				if (this.range != value) {

					lock (SyncRoot) {

						if (this.count != 0) {
							if (value <= (int)this.Last) {
								this.AddVersion();
							}
						}
	
						if (value == 0) {
							this.range = value;
							if (this.count != 0) {
								this.count = 0;
								this.ClearTail();
							}
	
						} else {
	
							int newRangeArrayLength = BitSetArray.GetLongArrayLength(value);
							if (newRangeArrayLength > this.array.Length) {
								if (newRangeArrayLength < (this.array.Length * 2)) {
									if (((long)(this.array.Length) * 2) <= BitSetArray.GetLongArrayLength(int.MaxValue)) {
										// reserve double space
										newRangeArrayLength = this.array.Length * 2;
									} else {
										// up to max required space
										newRangeArrayLength = BitSetArray.GetLongArrayLength(int.MaxValue);
									}
								}
								var newArray = new long[newRangeArrayLength];
								if (this.count != 0) {
									Array.Copy(this.array, newArray, BitSetArray.GetLongArrayLength(this.range));
								}
								this.array = newArray;
								this.range = value;
							} else {
								Contract.Assert(newRangeArrayLength <= this.array.Length);
								this.range = value;
								this.ClearTail();
								this.count = BitSetArray.CountOnBits(this.array, this.range);
							}
						}
					}
				}
			}
		}

		public int Capacity {
			[Pure]
			get {
				Contract.Ensures(Theory.CapacityGet(this, Contract.Result<int>()));
				return
					(this.array.LongLength * longBits) <= int.MaxValue ?
					(int)(this.array.LongLength * longBits) :
					int.MaxValue;
			}
		}

		public int Count {
			[Pure]
			get {
				Contract.Ensures(
					ValidLength(Contract.Result<int>()));
				Contract.Ensures(Contract.Result<int>() == this.count);
				return count;
			}
		}

		public int? First {
			[Pure]
			get {
				Contract.Ensures(Theory.FirstGet(this, Contract.Result<int?>()));

				int? start = null;
				if (this.count != 0) {
					if (this.startVersion == this.version) {
						start = this.startMemoize;
					} else {
						foreach (int item in this) {
							start = item;
							break;
						}
						this.startMemoize = start;
						this.startVersion = this.version;
					}
				}

				Contract.Assume((this.Count == 0 && start == null) ||
				(this.Count > 0 && start != null && this[(int)start]));
				return start;
			}
		}

		void FirstSet(int value)
		{
			Contract.Requires<ArgumentException>(this.InRange((int)value));
			Contract.Requires<InvalidOperationException>(Count != 0);

			Contract.Ensures(Theory.FirstSet(this, value));
					

			lock (SyncRoot) {
				this.startMemoize = value;
				this.startVersion = this.version;
			}
		}

		public int? Last {
			[Pure]
			get {
				Contract.Ensures(Theory.LastGet(this, Contract.Result<int?>()));

				int? final = null;
				if (this.count != 0) {
					if (this.finalVersion == this.version) {
						final = this.finalMemoize;
					} else {
						foreach (int item in this.Reverse ()) {
							final = item;
							break;
						}
						this.finalMemoize = final;
						this.finalVersion = this.version;
					}
				}

				Contract.Assume((this.Count == 0 && final == null) ||
				(this.Count > 0 && final != null && this[(int)final]));
				return final;
			}
		}

		void LastSet(int value)
		{
			Contract.Requires<ArgumentException>(this.InRange((int)value));
			Contract.Requires<InvalidOperationException>(Count != 0);

			Contract.Ensures(Theory.LastSet(this, value));

			lock (SyncRoot) {
				this.finalMemoize = value;
				this.finalVersion = this.version;
			}
		}

		public bool IsReadOnly {
			[Pure]
			get {
				Contract.Ensures(Contract.Result<bool>() == false);
				return false;
			}
		}

		public bool IsSynchronized {
			[Pure]
			get {
				Contract.Ensures(Contract.Result<bool>() == true);
				return true;
			}
		}

		public object SyncRoot {
			[Pure]
			get {
				Contract.Ensures(this.sRoot != null);
				return this.sRoot;
			}
		}

		public int Version {
			[Pure] get {
				return this.version;
			}
		}

		#endregion

	}
}
