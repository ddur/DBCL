// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using DD.Diagnostics;

namespace DD.Collections.ICodeSet {

    /// <summary>Small set, limited number of codes (items)
    /// <para>Wraps around sorted list, binary search for this[item]</para></summary>
    /// <remarks>Cannot be empty, cannot be full, contains at least 3, up to <see cref="ICodeSetService.ListMaxCount">ICodeSetService.ListMaxCount</see> codes</remarks>
    [Serializable]
    public sealed class CodeSetList : CodeSet {

        #region Ctor

        public static CodeSetList From (params Code[] codes) {
            Contract.Requires<ArgumentNullException> (codes.IsNot (null));
            Contract.Requires<ArgumentEmptyException> (!codes.IsEmpty ());
            Contract.Requires<ArgumentException> (codes.Distinct ().Count () > CodeSet.PairCount); // at least 3 members
            Contract.Requires<ArgumentException> (codes.Distinct ().Count () <= CodeSet.ListMaxCount); // up to max members
            Contract.Requires<ArgumentException> (codes.Distinct ().Count () < (1 + codes.Max () - codes.Min ())); // not full
            Contract.Ensures (Contract.Result<CodeSetList> ().IsNot (null));

            return new CodeSetList ((IEnumerable<Code>)codes);
        }

        public static CodeSetList From (IEnumerable<Code> codes) {
            Contract.Requires<ArgumentNullException> (codes.IsNot (null));
            Contract.Requires<ArgumentEmptyException> (!codes.IsEmpty ());
            Contract.Requires<ArgumentException> (codes.Distinct ().Count () > CodeSet.PairCount); // at least 3 members
            Contract.Requires<ArgumentException> (codes.Distinct ().Count () <= CodeSet.ListMaxCount); // up to max list members
            Contract.Requires<ArgumentException> (codes.Distinct ().Count () < (1 + codes.Max () - codes.Min ())); // not full
            Contract.Ensures (Contract.Result<CodeSetList> ().IsNot (null));

            return new CodeSetList (codes);
        }

        internal CodeSetList (IEnumerable<Code> codes) {
            Contract.Requires<ArgumentNullException> (codes.IsNot (null));
            Contract.Requires<ArgumentEmptyException> (!codes.IsEmpty ());
            Contract.Requires<ArgumentException> (codes.Distinct ().Count () > CodeSet.PairCount); // not pair
            Contract.Requires<ArgumentException> (codes.Distinct ().Count () <= CodeSet.ListMaxCount); // up to max list members
            Contract.Requires<ArgumentException> (codes.Distinct ().Count () < (1 + codes.Max () - codes.Min ())); // not full

            Contract.Ensures (Theory.Construct (codes, this));

            if (codes is ICodeSet) {
                foreach (var item in codes) {
                    this.sorted.Add (item);
                }
            }
            else {
                foreach (var item in codes.Distinct ().OrderBy (item => (item))) {
                    this.sorted.Add (item);
                }
            }
            this.sorted.TrimExcess ();
        }

        #endregion

        #region Fields

        private readonly List<int> sorted = new List<int>(16);

        #endregion

        #region ICodeSet

        [Pure]
        public override bool this[Code code] {
            get {
                return (this.sorted.BinarySearch (code.Value) >= 0);
            }
        }

        [Pure]
        public override bool this[int value] {
            get {
                return (this.sorted.BinarySearch (value) >= 0);
            }
        }

        [Pure]
        public override bool IsEmpty {
            get {
                return false;
            }
        }

        [Pure]
        public override int Count {
            get {
                return this.sorted.Count;
            }
        }

        [Pure]
        public override int Length {
            get {
                return 1 + this.sorted[this.sorted.Count - 1] - this.sorted[0];
            }
        }

        [Pure]
        public override Code First {
            get {
                return this.sorted[0];
            }
        }

        [Pure]
        public override Code Last {
            get {
                return this.sorted[this.sorted.Count - 1];
            }
        }

        [Pure]
        public override bool IsReduced {
            get {
        		return Count > CodeSet.PairCount && Count <= CodeSet.ListMaxCount && Count < Length;
            }
        }

        [Pure]
        public override IEnumerator<Code> GetEnumerator () {
            foreach (Code item in this.sorted) {
                yield return item;
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
            public static bool Construct (IEnumerable<Code> codes, CodeSetList self) {
                Success success = true;

                // Input -> private
                success.Assert (self.sorted.IsNot (null));
                foreach (var item in codes) {
                    success.Assert (self.sorted.Contains (item));
                }
                success.Assert (self.sorted.Count == codes.Distinct ().Count ());
                success.Assert (self.sorted[0] == codes.Min ());
                success.Assert (self.sorted[self.sorted.Count - 1] == codes.Max ());
                int index = 0;
                foreach (int item in codes.Distinct ().OrderBy (code => (code.Value))) {
                    success.Assert (self.sorted[index] == item);
                    ++index;
                }

                return success;
            }

            [Pure]
            public static bool Invariant (CodeSetList self) {
                Success success = true;

                // private
                success.Assert (self.sorted.IsNot (null));
                success.Assert (self.sorted.Count > CodeSet.PairCount);
                success.Assert (self.sorted.Count <= CodeSet.ListMaxCount);

                // public <- private
                success.Assert (self.First == self.sorted[0]);
                success.Assert (self.Last == self.sorted[self.sorted.Count - 1]);
                success.Assert (self.Count == self.sorted.Count);

                // constraints
                success.Assert (self.Count > CodeSet.PairCount);
                success.Assert (self.Count <= CodeSet.ListMaxCount);
                success.Assert (self.Count != self.Length);

                return success;
            }
        }
    }
}
