// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DD.Collections {

	/// <summary>Space efficient ICodeSet, limited number of codes (items)
	/// <remarks>Space efficient, O(log(n))</remarks>
	/// </summary>
	public sealed class CodeSetList : CodeSet {

		#region Ctor

		internal CodeSetList (IEnumerable<Code> codes) {
			Contract.Requires<ArgumentNullException> (!codes.IsNull ());
			Contract.Requires<ArgumentException> (!codes.IsEmpty ());

			Contract.Requires<ArgumentException> (codes.Distinct ().Count () > ICodeSetService.PairCount);
			Contract.Requires<ArgumentException> (codes.Distinct ().Count () <= ICodeSetService.ListMaxCount);

			// Input -> private
			Contract.Ensures (this.sorted.IsNot (null));
			Contract.Ensures (Contract.ForAll (codes, item => this.sorted.Contains(item)));
			Contract.Ensures (this.sorted.Count == codes.Distinct().Count());

			if (codes is ICodeSet) {
				this.sorted = new List<Code> (codes); // creates sorted&distinct list from sorted set
			}
			else {
				this.sorted = new List<Code> (codes.Distinct().OrderBy (item => (item)));
			}
			this.sorted.TrimExcess();
		}

		#endregion

		#region Fields

		private readonly List<Code> sorted;

		#endregion

		#region ICodeSet

		[Pure] public override bool this[Code code] {
			get {
				return (this.sorted.BinarySearch (code) >= 0);
			}
		}

		[Pure] public override int Count {
			get {
				return this.sorted.Count;
			}
		}

		[Pure] public override Code First {
			get {
				return this.sorted[0];
			}
		}

		[Pure] public override Code Last {
			get {
				return this.sorted[this.sorted.Count - 1];
			}
		}

		[Pure] public override IEnumerator<Code> GetEnumerator () {
			return this.sorted.GetEnumerator ();
		}

		#endregion

		#region Invariant

		[ContractInvariantMethod]
		private void Invariant () {
			// private
			Contract.Invariant (this.sorted.IsNot (null));
			Contract.Invariant (this.sorted.Count > ICodeSetService.PairCount);
			Contract.Invariant (this.sorted.Count <= ICodeSetService.ListMaxCount);
			Contract.Invariant (this.sorted.SequenceEqual(this.sorted.Distinct().OrderBy(item => (item))));

			// public <- private
			Contract.Invariant (this.First == this.sorted[0]);
			Contract.Invariant (this.Last == this.sorted.Last());
			Contract.Invariant (this.Count == this.sorted.Count);

			// constraints
			Contract.Invariant (this.Count > ICodeSetService.PairCount);
			Contract.Invariant (this.Count <= ICodeSetService.ListMaxCount);
		}

		#endregion
		
	}
}
