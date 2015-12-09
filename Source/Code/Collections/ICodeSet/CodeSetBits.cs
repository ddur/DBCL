// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using DD.Diagnostics;

namespace DD.Collections.ICodeSet
{
	/// <summary>Wraps over <see cref="ICodeSetService.IsCompact">Compact</see> BitSetArray</summary>
	/// <remarks>Can be empty, can be full, can contain up to <see cref="Code.MaxCount">Code.MaxCount</see> codes</remarks>
	[Serializable]
	public sealed class CodeSetBits : CodeSet
	{
		#region Ctor

		/// <summary>Empty CodeSetBits</summary>
		/// <remarks>Only CodeSetBits/Wrap and CodeSetNull can be Empty</remarks>
		public static CodeSetBits From()
		{
            Contract.Ensures(Contract.Result<CodeSetBits>() != null);

            return CodeSetBits.From(BitSetArray.Empty()); 
		}

		public static CodeSetBits From(params Code[] codes)
		{
			Contract.Requires<ArgumentNullException>(codes != null);
            Contract.Ensures(Contract.Result<CodeSetBits>() != null);

            return new CodeSetBits((IEnumerable<Code>)codes);
		}

		public static CodeSetBits From(IEnumerable<Code> codes)
		{
            Contract.Requires<ArgumentNullException>(codes != null);
            Contract.Ensures(Contract.Result<CodeSetBits>() != null);

            return new CodeSetBits(codes);
		}

		/// <summary>From BitSetArray at Offset</summary>
		/// <param name="bits">BitSetArray</param>
		/// <param name="offset">int</param>
		public static CodeSetBits From(BitSetArray bits, int offset = 0)
		{
			Contract.Requires<ArgumentNullException>(bits != null);
			Contract.Requires<IndexOutOfRangeException>(offset >= 0);
			Contract.Requires<IndexOutOfRangeException>(bits.Count == 0 || ((int)bits.Last + offset) <= Code.MaxValue);

            Contract.Ensures(Contract.Result<CodeSetBits>() != null);

			return new CodeSetBits(bits, offset);
		}

		private CodeSetBits(IEnumerable<Code> codes)
		{
            Contract.Requires<ArgumentNullException>(codes != null);

			Contract.Ensures(Theory.Construct(codes, this));

			if (!codes.IsEmpty()) {
				var codeSet = codes as ICodeSet;
				if (codeSet.IsNot(null)) {
					this.start = codeSet.First;
					this.final = codeSet.Last;
				} else {
					this.start = int.MaxValue;
					this.final = int.MinValue;
					foreach (Code code in codes) {
						if (code < this.start)
							this.start = code;
						if (code > this.final)
							this.final = code;
					}
				}

				// codes is same class?
				if (codes is CodeSetBits) {
					// ICodeSet is ReadOnly => can share internals
					this.sorted = ((CodeSetBits)codes).sorted;
				} else {
					this.sorted = BitSetArray.Size(this.final - this.start + 1);
					foreach (Code code in codes) {
						this.sorted._Set(code - this.start, true);
					}
				}
			} else {
				this.sorted = BitSetArray.Empty ();
			}
		}

		private CodeSetBits(BitSetArray bits, int offset = 0)
		{

            Contract.Requires<ArgumentNullException>(bits != null);
			Contract.Requires<IndexOutOfRangeException>(offset >= 0);
			Contract.Requires<IndexOutOfRangeException>(bits.Count == 0 || ((int)bits.Last + offset) <= Code.MaxValue);

			Contract.Ensures(Theory.Construct(bits, offset, this));

			if (bits.Count != 0) {
				Contract.Assume(bits.First.HasValue);
				Contract.Assume(bits.Last.HasValue);
				this.start = (int)bits.First + offset;
				this.final = (int)bits.Last + offset;
				this.sorted = BitSetArray.Size(this.final - this.start + 1);
				foreach (Code code in bits) {
					this.sorted._Set(code + offset - this.start, true);
				}
			} else {
				this.sorted = BitSetArray.Empty ();
			}
		}


		#endregion

		#region Fields

		private readonly BitSetArray sorted;
		private readonly int start = ICodeSetService.NoneStart;
		private readonly int final = ICodeSetService.NoneFinal;

		#endregion

		#region ICodeSet

		[Pure] public override bool this[Code code] {
			get {
				return this.sorted.Count != 0 && sorted[code - this.start];
			}
		}

		[Pure] public override int Count {
			get {
				return this.sorted.Count;
			}
		}

		[Pure] public override int Length {
			get {
				return this.sorted.Length;
			}
		}

		[Pure] public override Code First {
			get {
				if (this.sorted.Count != 0)
					return this.start;
				throw new InvalidOperationException();
			}
		}

		[Pure] public override Code Last {
			get {
				if (this.sorted.Count != 0)
					return this.final;
				throw new InvalidOperationException();
			}
		}

		[Pure] public override IEnumerator<Code> GetEnumerator()
		{
			foreach (var code in this.sorted) {
				yield return this.start + code;
			}
		}

		#endregion

		#region Extended

		public BitSetArray ToCompact()
		{
            Contract.Ensures(Contract.Result<BitSetArray>() != null);
            return BitSetArray.Copy(this.sorted);
		}
		
		public IEnumerable<Code> Complement {
			get {
                Contract.Ensures(Contract.Result<IEnumerable<Code>>() != null);
                foreach (var item in this.sorted.Complement())
                {
					yield return item + this.start;
				}
			}
		}

		#endregion

		#region Invariant

		[ContractInvariantMethod]
		private void Invariant()
		{
			Contract.Invariant(Theory.Invariant(this));
		}

		#endregion

		#region Theory

		static class Theory
		{

			[Pure] public static bool Construct(CodeSetBits self)
			{
				Success success = true;

				success.Assert(!self.sorted.IsNull());

				success.Assert(self.sorted.Count == 0);
				success.Assert(self.start == ICodeSetService.NoneStart);
				success.Assert(self.final == ICodeSetService.NoneFinal);

				return success;
			}
			
			[Pure] public static bool Construct(IEnumerable<Code> codes, CodeSetBits self)
			{
				Success success = true;
				
				success.Assert(!codes.IsNull());
				success.Assert(!self.sorted.IsNull());

				if (!codes.IsEmpty()) {
					success.Assert(self.sorted.Count == codes.Distinct().Count());
					success.Assert(self.start == codes.Min());
					success.Assert(self.final == codes.Max());
					foreach (var item in codes) {
						success.Assert(self.sorted[item - self.start]);
					}
				} else {
					success.Assert(Construct(self));
				}
				
				return success;
			}
			
			[Pure] public static bool Construct(BitSetArray bits, int offset, CodeSetBits self)
			{
				Success success = true;

				success.Assert(!bits.IsNull());
				success.Assert(!self.sorted.IsNull());

				success.Assert(self.sorted.Count == bits.Count);

				if (bits.Count != 0) {
					Contract.Assume(bits.First.HasValue);
					Contract.Assume(bits.Last.HasValue);
					success.Assert(self.start == (int)bits.First + offset);
					success.Assert(self.final == (int)bits.Last + offset);
					foreach (var item in bits) {
						success.Assert(self.sorted[item + offset - self.start]);
					}
				} else {
					success.Assert(Construct(self));
				}
				
				return success;
			}
			
			[Pure] public static bool Invariant(CodeSetBits self)
			{
				Success success = true;
				
				// private
				success.Assert(!self.sorted.IsNull());

				success.Assert(self.sorted.Length <= Code.MaxCount);
				success.Assert(self.sorted.Length == 1 + self.final - self.start);
				success.Assert(self.sorted.IsCompact());
	
				if (self.sorted.Count != 0) {
					success.Assert(self.start.HasCodeValue());
					success.Assert(self.final.HasCodeValue());
				} else {
					success.Assert(self.start == ICodeSetService.NoneStart);
					success.Assert(self.final == ICodeSetService.NoneFinal);
				}
				
				// public <- private
				success.Assert(self.Length == self.sorted.Length);
				success.Assert(self.Count == self.sorted.Count);
				if (self.Count != 0) {
					success.Assert(self.First == self.start);
					success.Assert(self.Last == self.final);
				}
				
				return success;
			}
		}

		#endregion
	}
}
