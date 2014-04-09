﻿// --------------------------------------------------------------------------------
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

	/// <summary>Space efficient ICodeSet, limited number of codes (items)
	/// <remarks>Space efficient, O(log(n))</remarks>
	/// </summary>
	public sealed class CodeSetList : CodeSet {

		#region Ctor

		internal CodeSetList (params Code[] codes) : this ((IEnumerable<Code>)codes) {}

		internal CodeSetList (IEnumerable<Code> codes) {
			Contract.Requires<ArgumentNullException> (!codes.IsNull ());
			Contract.Requires<ArgumentException> (!codes.IsEmpty ());
			Contract.Requires<ArgumentException> (codes.Distinct ().Count () > ICodeSetService.PairCount);
			Contract.Requires<ArgumentException> (codes.Distinct ().Count () <= ICodeSetService.ListMaxCount);
			Contract.Requires<ArgumentException> (1 + codes.Max() - codes.Min() != codes.Distinct().Count());

			Contract.Ensures (Theory.Construct(codes, this));

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

		[Pure] public override int Length {
			get {
				return 1 + this.sorted[this.sorted.Count - 1] - this.sorted[0];
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
			Contract.Invariant (Theory.Invariant(this));
		}

		#endregion
		
		private static class Theory {
			
			[Pure] public static bool Construct (IEnumerable<Code> codes, CodeSetList self) {
				
				// disable once ConvertToConstant.Local
				Success success = true;
				
				// Input -> private
				success.Assert (self.sorted.IsNot (null));
				foreach (var item in codes) {
					success.Assert (self.sorted.Contains(item));
				}
				success.Assert (self.sorted.Count == codes.Distinct().Count());
				success.Assert (self.sorted[0] == codes.Min());
				success.Assert (self.sorted[self.sorted.Count - 1] == codes.Max());
				success.Assert (self.sorted.SequenceEqual(codes.Distinct().OrderBy(item => (item))));

				return success;
			}
			
			[Pure] public static bool Invariant (CodeSetList self) {
				
				// disable once ConvertToConstant.Local
				Success success = true;
				
				// private
				success.Assert (self.sorted.IsNot (null));
				success.Assert (self.sorted.Count > ICodeSetService.PairCount);
				success.Assert (self.sorted.Count <= ICodeSetService.ListMaxCount);
	
				// public <- private
				success.Assert (self.First == self.sorted[0]);
				success.Assert (self.Last == self.sorted[self.sorted.Count - 1]);
				success.Assert (self.Count == self.sorted.Count);
	
				// constraints
				success.Assert (self.Count > ICodeSetService.PairCount);
				success.Assert (self.Count <= ICodeSetService.ListMaxCount);
	            success.Assert (self.Count != self.Length);

				return success;
			}
		}
	}
}
