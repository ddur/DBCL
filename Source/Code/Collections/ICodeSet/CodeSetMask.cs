// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using DD.Text;

namespace DD.Collections.ICodeSet
{
	/// <summary>Wraps around compact bitmask-array</summary>
	/// <remarks>Cannot be empty, contains at least 1, up to <see cref="Code.MaxCodeCount">Code.MaxCodeCount-1</see> codes</remarks>
	[Serializable]
	public sealed class CodeSetMask : CodeSet
	{
		#region Ctor

		public static CodeSetMask From(string utf16) {
            Contract.Requires<ArgumentNullException>(utf16 != null);
			Contract.Requires<ArgumentEmptyException>(!utf16.IsEmpty());
			Contract.Requires<ArgumentException>(utf16.CanDecode());
            Contract.Ensures(Contract.Result<CodeSetMask>() != null);

            return new CodeSetMask(utf16.Decode());
		}

		public static CodeSetMask From(params Code[] codes) {
            Contract.Requires<ArgumentNullException>(codes != null);
            Contract.Requires<ArgumentEmptyException> (codes.Length > 0);
            Contract.Ensures(Contract.Result<CodeSetMask>() != null);

			return new CodeSetMask(codes as IEnumerable<Code>);
		}

		public static CodeSetMask From(params char[] chars) {
            Contract.Requires<ArgumentNullException>(chars != null);
            Contract.Requires<ArgumentEmptyException> (chars.Length > 0);
            Contract.Ensures(Contract.Result<CodeSetMask>() != null);

			return new CodeSetMask(chars.Cast<Code>());
		}

		public static CodeSetMask From(IEnumerable<Code> codes) {
            Contract.Requires<ArgumentNullException>(codes != null);
			Contract.Requires<ArgumentEmptyException>(!codes.IsEmpty());
            Contract.Ensures(Contract.Result<CodeSetMask>() != null);

			return new CodeSetMask(codes);
		}

		private CodeSetMask(IEnumerable<Code> codes)
		{
            Contract.Requires<ArgumentNullException>(codes != null);
			Contract.Requires<ArgumentEmptyException>(!codes.IsEmpty());

			var iCodeSet = codes as ICodeSet;
			if (!iCodeSet.Is(null)) {
				start = iCodeSet.First;
				final = iCodeSet.Last;
			} else {
				foreach (var code in codes) {
					if (start > code) {
						start = code;
					}
					if (final < code) {
						final = code;
					}
				}
			}
			Contract.Assert(start <= final);
			this.sorted = new uint[BitSetArray.GetIntArrayLength(1 + this.final - this.start)];
			int item = 0;
			int index = 0;
			uint mask = 0;
			foreach (var code in codes) {
				item = code.Value - start.Value;
				mask = 1u << (item & mask0x1F);
				index = item >> log2of32;
				if ((sorted[index] & mask) == 0) {
					sorted[index] ^= mask;
					++count;
				}
			}
		}

		private CodeSetMask(uint[] mask, int offset = 0)
		{
			Contract.Requires<ArgumentNullException>(mask != null);
			Contract.Requires<ArgumentEmptyException>(mask.Length > 0);
			Contract.Requires<ArgumentException>(LastBitIndex(mask) != null);
			Contract.Requires<ArgumentException>(0 != (mask[0] & 1)); // first bit set
			Contract.Requires<ArgumentException>(0 != (mask[mask.Length - 1])); // last bits-item is not empty
			Contract.Requires<ArgumentException>(offset.HasCodeValue());
			// compute offset + last-bit-index and check if HasCodeValue
			Contract.Requires<ArgumentException>((offset + (int)LastBitIndex(mask)).HasCodeValue());

			start = offset;
			final = offset + (int)LastBitIndex(mask);
			sorted = new uint[mask.Length];
			Array.Copy(mask, sorted, sorted.Length); 
		}

		[Pure] private static uint? LastBitIndex (uint[] bitMaskArray) {
			if (bitMaskArray == null) {
				return null;
			}
			if (bitMaskArray.Length == 0) {
				return null;
			}
			for (int i = bitMaskArray.Length - 1; i >= 0; i--) {
				if (bitMaskArray[i] != 0) {
					return ((uint)(i) << log2of32) + LastBitIndex(bitMaskArray[i]);
				}
			}
			return null;
		}

		[Pure] private static byte? LastBitIndex (uint bitMask) {
			if (bitMask == 0) {
				return null;
			}
			byte lastBitIndex = 0;
			while ((bitMask & 1u) == 0) {
				bitMask >>= 1;
				++lastBitIndex;
			}
			return lastBitIndex;
		}

		#endregion

		#region Fields

		private readonly Code start = Code.MaxValue;
		private readonly Code final = Code.MinValue;
		private readonly int count = 0;
		private readonly uint[] sorted;
		const int log2of32 = 5;
		const int mask0x1F = 0x1F;

		#endregion

		#region ICodeSet

		[Pure] public override bool this[Code code] {
			get {
				if (code.Value.InRange(start.Value, final.Value)) {
					int item = code.Value - start.Value;
					int index = item >> log2of32;
					uint mask = 1u << (item & mask0x1F);
					return (sorted[index] & mask) != 0;
				}
				return false;
			}
		}

		[Pure] public override int Count {
			get {
				return count;
			}
		}

		[Pure] public override int Length {
			get {
				return 1 + final - start;
			}
		}

		[Pure] public override Code First {
			get {
				return this.start;
			}
		}

		[Pure] public override Code Last {
			get {
				return this.final;
			}
		}

		[Pure] public override IEnumerator<Code> GetEnumerator()
		{
			for (int i = start; i <= final; i++) {
				if (this[i]) {
					yield return i;
				}
			}						
		}

		#endregion
	}
}
