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
	/// <summary>
	/// Description of CodeSetCode.
	/// </summary>
	public class CodeSetCode : CodeSet
	{
		#region Ctor

		public CodeSetCode(string utf16)
			: this(utf16.Decode())
		{
			Contract.Requires<ArgumentEmptyException>(string.IsNullOrEmpty(utf16));
			Contract.Requires<ArgumentException>(utf16.CanDecode());
			
		}
		public CodeSetCode(params Code[] codes)
			: this(codes as IEnumerable<Code>)
		{
			Contract.Requires<ArgumentEmptyException>(codes.Length > 0);
			
		}
		public CodeSetCode(params char[] chars)
			: this(chars.Cast<Code>())
		{
			Contract.Requires<ArgumentEmptyException>(chars.Length > 0);
			
		}
		public CodeSetCode(IEnumerable<Code> codes)
		{
			Contract.Requires<ArgumentNullException>(!codes.Is(null));
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
				mask = 1u << (item & maskFive);
				index = item >> moveFive;
				if ((sorted[index] & mask) == 0) {
					sorted[index] ^= mask;
					++count;
				}
			}
		}

		internal CodeSetCode(uint[] mask, Code offset)
		{
			Contract.Requires<ArgumentNullException>(mask != null);
			Contract.Requires<ArgumentEmptyException>(mask.Length > 0);
			Contract.Requires<ArgumentException>((mask[0] & -1) != 0); // first bit set
			Contract.Requires<ArgumentException>(mask[mask.Length - 1] != 0); // last block is not empty
			// compute last bit Code.Value and check if larger than Code.MaxValue

			start = offset;
			sorted = new uint[mask.Length];
			Array.Copy(mask, sorted, sorted.Length); 
		}

		#endregion

		#region Fields

		private readonly Code start = Code.MaxValue;
		private readonly Code final = Code.MinValue;
		private readonly int count = 0;
		private readonly uint[] sorted;
		const int moveFive = 5;
		const int maskFive = 0x1F;

		#endregion

		#region ICodeSet

		[Pure] public override bool this[Code code] {
			get {
				if (code.Value.InRange(start.Value, final.Value)) {
					int item = code.Value - start.Value;
					int index = item >> moveFive;
					uint mask = 1u << (item & maskFive);
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
