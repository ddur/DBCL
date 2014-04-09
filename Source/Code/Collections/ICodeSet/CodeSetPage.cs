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

namespace DD.Collections.ICodeSet {

	/// <summary>CodeSetPage contains only codes within same unicode plane/page
	/// </summary>
	public sealed class CodeSetPage : CodeSet {

		#region Ctor

		internal CodeSetPage (params Code[] codes) : this ((IEnumerable<Code>)codes) {}

		internal CodeSetPage (IEnumerable<Code> codes) {

			Contract.Requires<ArgumentNullException> (!codes.Is(null));
			Contract.Requires<ArgumentException> (!codes.IsEmpty());
			Contract.Requires<ArgumentException> (codes.Distinct().Count() > ICodeSetService.PairCount); // not Null-Pair
			Contract.Requires<ArgumentException> (codes.Distinct().Count() < (codes.Max() - codes.Min())); // not Full-Pair
			Contract.Requires<ArgumentException> (codes.Min().UnicodePlane() == codes.Max().UnicodePlane()); // one Page

			Contract.Ensures (Theory.Construct(codes, this));

			var iCodeSet = codes as ICodeSet;
			if (iCodeSet.IsNot(null)) {
				this.start = iCodeSet.First;
				this.final = iCodeSet.Last;
			}
			else {
				foreach ( Code code in codes ) {
					if ( code < this.start )
						this.start = code;
					if ( code > this.final )
						this.final = code;
				}
			}
			if (codes is CodeSetPage) {
				// ICodeSet is ReadOnly => can share
				this.sorted = ((CodeSetPage)codes).sorted;
			}
			else {
				this.sorted = BitSetArray.Size (1 + this.final - this.start);
				foreach ( Code code in codes ) {
					this.sorted.Set (code - this.start, true);
				}
			}
		}

		internal CodeSetPage (BitSetArray bits, int offset = 0) {

			Contract.Requires<ArgumentNullException> (!bits.Is(null));
			Contract.Requires<ArgumentException> (!bits.IsEmpty());
			Contract.Requires<ArgumentOutOfRangeException> (bits.Length <= Code.MaxCount || bits.Last <= Code.MaxValue);
			Contract.Requires<ArgumentException> (((int)bits.First + offset).HasCodeValue() && ((int)bits.Last + offset).HasCodeValue());
			Contract.Requires<ArgumentException> (bits.Count > ICodeSetService.PairCount);	// not Null-Pair
			Contract.Requires<ArgumentException> (bits.Count < (bits.Last - bits.First));	// not Full-Pair
			Contract.Requires<ArgumentException> (((Code)(bits.First + offset)).UnicodePlane() == ((Code)(bits.Last + offset)).UnicodePlane()); // one Page

			Contract.Ensures (Theory.Construct(bits, offset, this));

			this.start = (int)bits.First + offset;
			this.final = (int)bits.Last + offset;
			this.sorted = BitSetArray.Size (this.final - this.start + 1);
			foreach ( Code code in bits ) {
				this.sorted.Set (code + offset - this.start, true);
			}
		}

		#endregion

		#region Fields

		private readonly BitSetArray sorted;
		private readonly Code start = Code.MaxValue;
		private readonly Code final = Code.MinValue;

		#endregion

		#region ICodeSet

		[Pure] public override bool this[Code code] {
			get {
				return sorted[code - this.start];
			}
		}

		[Pure] public override int Count {
			get {
				return sorted.Count;
			}
		}

		[Pure] public override int Length {
			get {
				return sorted.Length;
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

		[Pure] public override IEnumerator<Code> GetEnumerator () {
			foreach ( Code code in this.sorted ) {
				yield return this.start + code;
			}
		}

		#endregion

		#region Invariant

		[ContractInvariantMethod]
		private void Invariant () {
			Contract.Invariant (Theory.Invariant (this));
		}

		#endregion

		internal BitSetArray ToCompact () {
			return BitSetArray.Copy (this.sorted);
		}

		internal static class Theory {

			[Pure] public static bool Construct (IEnumerable<Code> codes, CodeSetPage self) {

				// disable once ConvertToConstant.Local
				Success success = true;
				
				var distinctItems = codes.Distinct().OrderBy(item => (item));
				var distinctCount = distinctItems.Count();
				Code distinctMin = distinctItems.Min();
				Code distinctMax = distinctItems.Max();

				// input -> private
				success.Assert (self.sorted.IsNot (null));
				success.Assert (self.sorted.Count == distinctCount);
				var e = self.sorted.GetEnumerator();
				foreach (var item in distinctItems) {
					e.MoveNext();
					success.Assert (item == e.Current + self.start);// => SequenceEqual
					success.Assert (self.sorted[item-self.start]);
				}

				success.Assert (self.start == distinctItems.Min());
				success.Assert (self.final == distinctItems.Max());
				
				return success;
			}
			
			[Pure] public static bool Construct (BitSetArray bits, int offset, CodeSetPage self) {

				// disable once ConvertToConstant.Local
				Success success = true;

				// input -> private
				success.Assert (self.sorted.IsNot (null));
				success.Assert (self.sorted.Count == bits.Count);
				var e = self.sorted.GetEnumerator();
				foreach (var item in bits) {
					e.MoveNext();
					success.Assert (item == e.Current - offset + self.start);// => SequenceEqual
					success.Assert (self.sorted[item + offset - self.start]);
				}

				success.Assert (self.start == (int)bits.First + offset);
				success.Assert (self.final == (int)bits.Last + offset);
				
				return success;
			}

			[Pure] public static bool Invariant(CodeSetPage self) {

				// disable once ConvertToConstant.Local
				Success success = true;
				
				// private
				success.Assert (self.sorted.IsNot (null));
				success.Assert (self.sorted.IsCompact());
				success.Assert (self.sorted.Count > ICodeSetService.PairCount);	// not Null-Pair
				success.Assert (self.sorted.Count < (self.sorted.Length - 1));	// not Full-Pair
				success.Assert (self.start.UnicodePlane() == self.final.UnicodePlane());

				// public <- private
				success.Assert (self.Length == self.sorted.Length);
				success.Assert (self.Count == self.sorted.Count);
				success.Assert (self.First == self.start);
				success.Assert (self.Last == self.final);
				
				// constraints
				success.Assert (self.Count > ICodeSetService.PairCount);// not Unit-Pair
				success.Assert (self.Count < (self.Length - 1));		// not Full-Pair
				success.Assert (self.First.UnicodePlane() == self.Last.UnicodePlane()); // one Page

				return success;
			}
		}
	}
}
