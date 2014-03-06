﻿// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using DD.Diagnostics;

namespace DD.Collections {

	/// <summary>Difference set (Range-Set)</summary>
	public sealed class CodeSetDiff : CodeSet {

		#region Ctor

		/// <summary>Constructor</summary>
		/// <param name="a">CodeRange</param>
		/// <param name="b">CodeSet</param>
		internal CodeSetDiff (ICodeSet a, ICodeSet b) {
			Contract.Requires<ArgumentNullException> (!a.IsNull ());
			Contract.Requires<ArgumentNullException> (!b.IsNull ());

			Contract.Requires<ArgumentException> (!a.IsEmpty ());
			Contract.Requires<ArgumentException> (!b.IsEmpty ());

			// a is range
			Contract.Requires<ArgumentException> (a.Count == a.Length);

			// b is proper-inner range subset of a
			Contract.Requires<ArgumentException> (a.First < b.First);
			Contract.Requires<ArgumentException> (b.Last < a.Last);

			// this.count > ListMaxCount (two-sets-indexer/list-indexer)
			Contract.Requires<ArgumentException> ((a.Count - b.Count) > ICodeSetService.ListMaxCount);

			Contract.Ensures (Theory.Construct(a, b, this));

			this.aSet = a;
			this.bSet = b;
		}

		#endregion

		#region Fields

		private readonly ICodeSet aSet;
		private readonly ICodeSet bSet;

		#endregion

		#region ICodeSet

		[Pure] public override bool this[Code code] {
			get {
				return this.aSet[code] && !this.bSet[code];
			}
		}

		[Pure] public override int Count {
			get {
				return aSet.Count - bSet.Count;
			}
		}

		[Pure] public override int Length {
			get {
				return this.aSet.Length;
			}
		}

		[Pure] public override Code First {
			get {
				return this.aSet.First;
			}
		}

		[Pure] public override Code Last {
			get {
				return this.aSet.Last;
			}
		}

		[Pure] public override IEnumerator<Code> GetEnumerator () {
			foreach ( Code code in this.aSet ) {
				if ( !this.bSet[code] ) {
					yield return code;
				}
			}
		}

		#endregion

		#region Invariant

		[ContractInvariantMethod]
		private void Invariant () {
			Contract.Invariant (Theory.Invariant(this));
		}

		#endregion
		
		private static class Theory {
			
			[Pure] public static bool Construct (ICodeSet a, ICodeSet b, CodeSetDiff self) {
				// disable once ConvertToConstant.Local
				Success success = true;

				// input
				success.Assert (!a.Is(null));
				success.Assert (!b.Is(null));
				success.Assert (!a.IsEmpty());
				success.Assert (!b.IsEmpty());
				success.Assert (a.Count == a.Length);
				success.Assert (a.First < b.First);
				success.Assert (b.Last < a.Last);
				success.Assert ((a.Count - b.Count) > ICodeSetService.ListMaxCount);

				// input -> private
				success.Assert (self.aSet.Is (a));
				success.Assert (self.bSet.Is (b));

				return success;
			}

			[Pure] public static bool Invariant (CodeSetDiff self) {
				// disable once ConvertToConstant.Local
				Success success = true;

				// private
				success.Assert (!self.aSet.Is (null));
				success.Assert (!self.bSet.Is (null));
				success.Assert (!self.aSet.IsEmpty());
				success.Assert (!self.bSet.IsEmpty());
				success.Assert (self.aSet.Count == self.aSet.Length);
				success.Assert (self.aSet.First < self.bSet.First);
				success.Assert (self.bSet.Last < self.aSet.Last);
				success.Assert ((self.aSet.Count - self.bSet.Count) > ICodeSetService.ListMaxCount);

				// public <- private
				success.Assert (self.Count == self.aSet.Count - self.bSet.Count);
				success.Assert (self.Length == self.aSet.Length);
				success.Assert (self.First == self.aSet.First);
				success.Assert (self.Last == self.aSet.Last);

				// constraints
				success.Assert (self.Count > ICodeSetService.ListMaxCount);
				success.Assert (self.Count < Code.MaxCount);

				return success;
			}

		}
	}
}
