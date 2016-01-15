// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using DD.Diagnostics;

namespace DD.Collections.ICodeSet {

    /// <summary>Difference of two sets (Range-Reduce(Set))</summary>
    /// <remarks>Cannot be empty, cannot be full, contains at least 3, up to <see cref="Code.MaxCodeCount">Code.MaxCodeCount-1</see> codes</remarks>
    [Serializable]
    public sealed class CodeSetDiff : CodeSet {

        #region Ctor

        public static CodeSetDiff From (ICodeSet a, ICodeSet b) {
            Contract.Requires<ArgumentNullException> (a.IsNot (null));
            Contract.Requires<ArgumentNullException> (b.IsNot (null));

            Contract.Requires<ArgumentEmptyException> (!a.IsEmpty ());
            Contract.Requires<ArgumentEmptyException> (!b.IsEmpty ());

            // a is full range
            Contract.Requires<InvalidOperationException> (a.Count == a.Length);
            Contract.Requires<InvalidOperationException> (a is CodeSetFull);

            // b is proper-inner range subset of a
            Contract.Requires<InvalidOperationException> (a.First < b.First);
            Contract.Requires<InvalidOperationException> (b.Last < a.Last);

            // Has more members than Service.ListMaxCount (>CodeSetList.MaxCount)
            Contract.Requires<InvalidOperationException> ((a.Count - b.Count) > Service.ListMaxCount);

            Contract.Ensures (Contract.Result<CodeSetDiff> ().IsNot (null));

            return new CodeSetDiff (a, b);
        }

        /// <summary>Internal Constructor for Reduction</summary>
        /// <param name="a">CodeRange</param>
        /// <param name="b">CodeSet</param>
        internal CodeSetDiff (ICodeSet a, ICodeSet b) {
            Contract.Requires<ArgumentNullException> (a.IsNot (null));
            Contract.Requires<ArgumentNullException> (b.IsNot (null));

            Contract.Requires<ArgumentException> (!a.IsEmpty ());
            Contract.Requires<ArgumentException> (!b.IsEmpty ());

            // a is full range
            Contract.Requires<ArgumentException> (a.Count == a.Length);
            Contract.Requires<ArgumentException> (a is CodeSetFull);

            // b is proper-inner range subset of a
            Contract.Requires<ArgumentException> (a.First < b.First);
            Contract.Requires<ArgumentException> (b.Last < a.Last);

            // this.count > Service.ListMaxCount (>CodeSetList.MaxCount)
            Contract.Requires<ArgumentException> ((a.Count - b.Count) > Service.ListMaxCount);

            Contract.Ensures (Theory.Construct (a, b, this));

            this.aSet = a;
            this.bSet = b;
        }

        #endregion

        #region Fields

        private readonly ICodeSet aSet;
        private readonly ICodeSet bSet;

        #endregion

        #region ICodeSet

        [Pure]
        public override bool this[Code code] {
            get {
                return !this.bSet[code] && this.aSet[code];
            }
        }

        [Pure]
        public override int Count {
            get {
                return aSet.Count - bSet.Count;
            }
        }

        [Pure]
        public override int Length {
            get {
                return this.aSet.Length;
            }
        }

        [Pure]
        public override Code First {
            get {
                return this.aSet.First;
            }
        }

        [Pure]
        public override Code Last {
            get {
                return this.aSet.Last;
            }
        }

        [Pure]
        public override bool IsReduced {
            get {
              	return true; // reduced by Constructor Code-Contracts
            }
        }

        [Pure]
        public override IEnumerator<Code> GetEnumerator () {
            foreach (Code code in this.aSet) {
                if (!this.bSet[code]) {
                    yield return code;
                }
            }
        }

        #endregion

        #region Invariant

        [ContractInvariantMethod]
        private void Invariant () {
            Contract.Invariant (Theory.Invariant (this));
        }

        #endregion

        private static class Theory {

            [Pure]
            public static bool Construct (ICodeSet a, ICodeSet b, CodeSetDiff self) {
                Success success = true;

                // input
                success.Assert (!a.Is (null));
                success.Assert (!b.Is (null));
                success.Assert (!a.IsEmpty ());
                success.Assert (!b.IsEmpty ());
                success.Assert (a.Count == a.Length);
                success.Assert (a.First < b.First);
                success.Assert (b.Last < a.Last);
                success.Assert ((a.Count - b.Count) > Service.PairCount);

                // input -> private
                success.Assert (self.aSet.Is (a));
                success.Assert (self.bSet.Is (b));

                return success;
            }

            [Pure]
            public static bool Invariant (CodeSetDiff self) {
                Success success = true;

                // private
                success.Assert (!self.aSet.Is (null));
                success.Assert (!self.bSet.Is (null));
                success.Assert (!self.aSet.IsEmpty ());
                success.Assert (!self.bSet.IsEmpty ());
                success.Assert (self.aSet.Count == self.aSet.Length);
                success.Assert (self.aSet.First < self.bSet.First);
                success.Assert (self.bSet.Last < self.aSet.Last);
                success.Assert ((self.aSet.Count - self.bSet.Count) > Service.PairCount);

                // public <- private
                success.Assert (self.Count == self.aSet.Count - self.bSet.Count);
                success.Assert (self.Length == self.aSet.Length);
                success.Assert (self.First == self.aSet.First);
                success.Assert (self.Last == self.aSet.Last);

                // constraints
                success.Assert (self.Count > Service.PairCount);// not Pair
                success.Assert (self.Count < self.Length);				// not Full

                return success;
            }
        }
    }
}
