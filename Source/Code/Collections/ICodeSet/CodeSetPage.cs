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

namespace DD.Collections {

	/// <summary>CodeSetPage contains only codes within same unicode plane/page
	/// </summary>
	public sealed class CodeSetPage : CodeSet {

		#region Ctor

		internal CodeSetPage (IEnumerable<Code> codes) {

			Contract.Requires<ArgumentNullException> (!codes.Is(null));
			Contract.Requires<ArgumentException> (codes.Distinct().Count() > ICodeSetService.PairCount);
			Contract.Requires<ArgumentException> (codes.Min().UnicodePlane() == codes.Max().UnicodePlane());
			Contract.Requires<ArgumentException> (1 + codes.Max() - codes.Min() != codes.Distinct().Count());

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
				// ICodeSet is ReadOnly => can share guts/internals
				this.sorted = ((CodeSetPage)codes).sorted;
			}
			else {
				this.sorted = new BitSetArray (1 + this.final - this.start);
				foreach ( Code code in codes ) {
					this.sorted.Set (code - this.start, true);
				}
			}
		}

		internal CodeSetPage (BitSetArray bits) {

			Contract.Requires<ArgumentNullException> (!bits.Is(null));
			Contract.Requires<ArgumentException> (bits.Count > ICodeSetService.PairCount);
			Contract.Requires<ArgumentOutOfRangeException> (bits.Last <= Code.MaxValue);
			Contract.Requires<ArgumentException> (((Code)bits.First).UnicodePlane() == ((Code)bits.Last).UnicodePlane());
			Contract.Requires<ArgumentException> (bits.Count != bits.Length);

			Contract.Ensures (Theory.Construct(bits, this));

			this.start = (int)bits.First;
			this.final = (int)bits.Last;
			this.sorted = new BitSetArray (this.final - this.start + 1);
			foreach ( Code code in bits ) {
				this.sorted.Set (code - this.start, true);
			}
		}

		#endregion

		#region Fields

		private readonly BitSetArray sorted;
		private readonly int start = Code.MaxValue;
		private readonly int final = Code.MinValue;

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

		private static class Theory {
		 
			[Pure] public static bool Construct (IEnumerable<Code> codes, CodeSetPage self) {

				// disable once ConvertToConstant.Local
				Success success = true;
				
				// input -> private
				success.Assert (self.sorted.IsNot (null));
				success.Assert (self.sorted.Count == codes.Distinct().Count());
				foreach (var item in codes) {
					success.Assert (self.sorted[item-self.start]);
				}

				success.Assert (self.start == codes.Min());
				success.Assert (self.final == codes.Max());
				
				return success;
			}
			
			[Pure] public static bool Construct (BitSetArray bits, CodeSetPage self) {

				// disable once ConvertToConstant.Local
				Success success = true;

				success.Assert (self.sorted.IsNot (null));
				success.Assert (self.sorted.Count == bits.Count);
				foreach (var item in bits) {
					success.Assert (self.sorted[item-self.start]);
				}

				success.Assert (self.start == (int)bits.First);
				success.Assert (self.final == (int)bits.Last);
				
				return success;
			}

			[Pure] public static bool Invariant(CodeSetPage self) {

				// disable once ConvertToConstant.Local
				Success success = true;
				
				// private
				success.Assert (self.sorted.IsNot (null));
				success.Assert (self.sorted.Length <= (1 + char.MaxValue));
				success.Assert (self.sorted.Count > ICodeSetService.PairCount);
				success.Assert (self.sorted.Count != self.sorted.Length);
				success.Assert (self.sorted[0]);
				success.Assert (self.sorted[self.sorted.Length - 1]);

				success.Assert (self.start.HasCodeValue ());
				success.Assert (self.final.HasCodeValue ());
				success.Assert (((Code)self.start).UnicodePlane() == ((Code)self.final).UnicodePlane());

				// public <- private
				success.Assert (self.Length == self.sorted.Length);
				success.Assert (self.Count == self.sorted.Count);
				success.Assert (self.First == self.start);
				success.Assert (self.Last == self.final);
				
				// constraints
				success.Assert (self.Count > ICodeSetService.PairCount);
				success.Assert (self.Count <= (1 + char.MaxValue));
				success.Assert (self.Length <= (1 + char.MaxValue));
				success.Assert (self.Count != self.Length);
				success.Assert (self.First.UnicodePlane() == self.Last.UnicodePlane());

				return success;
			}
		}
	}
}
