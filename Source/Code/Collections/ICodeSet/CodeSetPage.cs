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

    /// <summary>CodeSetPage contains only codes within same unicode plane/page</summary>
    /// <remarks>Cannot be empty, cannot be full, contains at least 3, up to <see cref="char.MaxValue">char.MaxValue-1</see> codes</remarks>
    [Serializable]
    public sealed class CodeSetPage : CodeSet {

        #region Ctor

        public static CodeSetPage From (params Code[] codes) {
            Contract.Requires<ArgumentNullException> (codes.IsNot (null));
            Contract.Requires<InvalidOperationException> (codes.Distinct ().Count ().InRange (Service.PairCount + 1, codes.Span () - 1));	// not Null/Code/Pair/Full
            Contract.Requires<InvalidOperationException> (codes.Min ().UnicodePlane () == codes.Max ().UnicodePlane ()); // one Page
            Contract.Ensures (Contract.Result<CodeSetPage> ().IsNot (null));

            return new CodeSetPage ((IEnumerable<Code>)codes);
        }

        public static CodeSetPage From (IEnumerable<Code> codes) {
            Contract.Requires<ArgumentNullException> (codes.IsNot (null));
            Contract.Requires<InvalidOperationException> (codes.Distinct ().Count ().InRange (Service.PairCount + 1, codes.Span () - 1));	// not Null/Code/Pair/Full
            Contract.Requires<InvalidOperationException> (codes.Min ().UnicodePlane () == codes.Max ().UnicodePlane ()); // one Page
            Contract.Ensures (Contract.Result<CodeSetPage> ().IsNot (null));

            return new CodeSetPage (codes);
        }

        public static CodeSetPage From (BitSetArray bits, int offset = 0) {
            Contract.Requires<ArgumentNullException> (bits.IsNot (null));
            Contract.Requires<InvalidOperationException> (bits.Count.InRange (Service.PairCount + 1, bits.Span () - 1));	// not Null/Code/Pair/Full
            Contract.Requires<IndexOutOfRangeException> (offset.InRange (0, Code.MaxValue - (int)bits.Last));
            Contract.Requires<InvalidOperationException> ((bits.First + offset).UnicodePlane () == (bits.Last + offset).UnicodePlane ()); // one Page
            Contract.Ensures (Contract.Result<CodeSetPage> ().IsNot (null));

            return new CodeSetPage (bits, offset);
        }

        private CodeSetPage (IEnumerable<Code> codes) {
            Contract.Requires<ArgumentNullException> (codes.IsNot (null));
            Contract.Requires<InvalidOperationException> (codes.Distinct ().Count ().InRange (Service.PairCount + 1, codes.Span () - 1));	// not Null/Code/Pair/Full
            Contract.Requires<InvalidOperationException> (codes.Min ().UnicodePlane () == codes.Max ().UnicodePlane ()); // one Page

            Contract.Ensures (Theory.Construct (codes, this));

            var iCodeSet = codes as ICodeSet;
            if (iCodeSet.IsNot (null)) {
                this.start = iCodeSet.First;
                this.final = iCodeSet.Last;
            }
            else {
                this.start = Code.MaxValue;
                this.final = Code.MinValue;
                foreach (Code code in codes) {
                    if (code < this.start)
                        this.start = code;
                    if (code > this.final)
                        this.final = code;
                }
            }
            Contract.Assume (start <= final);
            if (codes is CodeSetPage) {
                // ICodeSet is ReadOnly => can share
                this.sorted = ((CodeSetPage)codes).sorted;
            }
            else {
                this.sorted = BitSetArray.Size (1 + this.final - this.start);
                foreach (var code in codes) {
                    this.sorted._Set (code - this.start, true);
                }
            }
        }

        private CodeSetPage (BitSetArray bits, int offset = 0) {
            Contract.Requires<ArgumentNullException> (bits.IsNot (null));
            Contract.Requires<InvalidOperationException> (bits.Count.InRange (Service.PairCount + 1, bits.Span () - 1));	// not Null/Code/Pair/Full
            Contract.Requires<IndexOutOfRangeException> (offset.InRange (0, Code.MaxValue - (int)bits.Last));
            Contract.Requires<InvalidOperationException> ((bits.First + offset).UnicodePlane () == (bits.Last + offset).UnicodePlane ()); // one Page

            Contract.Ensures (Theory.Construct (bits, offset, this));

            Contract.Assume (bits.First.HasValue);
            Contract.Assume (bits.Last.HasValue);

            this.start = ((int)bits.First) + offset;
            this.final = ((int)bits.Last) + offset;
            this.sorted = BitSetArray.Size (bits.Span ());
            foreach (var item in bits) {
                this.sorted._Set (item + offset - this.start, true);
            }
        }

        #endregion

        #region Fields

        private readonly BitSetArray sorted;
        private readonly Code start;
        private readonly Code final;

        #endregion

        #region ICodeSet

        [Pure]
        public override bool this[Code code] {
            get {
                return sorted[code - this.start];
            }
        }

        [Pure]
        public override int Count {
            get {
                return sorted.Count;
            }
        }

        [Pure]
        public override int Length {
            get {
                return sorted.Length;
            }
        }

        [Pure]
        public override Code First {
            get {
                return this.start;
            }
        }

        [Pure]
        public override Code Last {
            get {
                return this.final;
            }
        }

        [Pure]
        public override bool IsReduced {
            get {
        		return Count > Service.ListMaxCount && First.UnicodePlane() == Last.UnicodePlane();
            }
        }

        [Pure]
        public override IEnumerator<Code> GetEnumerator () {
            foreach (Code code in this.sorted) {
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

        public BitSetArray ToCompact () {
            Contract.Ensures (Contract.Result<BitSetArray> ().IsNot (null));
            return BitSetArray.Copy (this.sorted);
        }

        internal static class Theory {

            [Pure]
            public static bool Construct (IEnumerable<Code> codes, CodeSetPage self) {
                Success success = true;

                var distinctItems = codes.Distinct ().OrderBy (item => (item));
                var distinctCount = distinctItems.Count ();
                Code distinctMin = distinctItems.Min ();
                Code distinctMax = distinctItems.Max ();

                // input -> private
                success.Assert (self.sorted.IsNot (null));
                success.Assert (self.sorted.Count == distinctCount);
                var e = self.sorted.GetEnumerator ();
                foreach (var item in distinctItems) {
                    e.MoveNext ();
                    success.Assert (item == e.Current + self.start);// => SequenceEqual
                    success.Assert (self.sorted[item - self.start]);
                }

                success.Assert (self.start == distinctItems.Min ());
                success.Assert (self.final == distinctItems.Max ());

                return success;
            }

            [Pure]
            public static bool Construct (BitSetArray bits, int offset, CodeSetPage self) {
                Success success = true;

                // input -> private
                success.Assert (self.sorted.IsNot (null));
                success.Assert (self.sorted.Count == bits.Count);
                var e = self.sorted.GetEnumerator ();
                foreach (var item in bits) {
                    e.MoveNext ();
                    success.Assert (item == e.Current - offset + self.start);// => SequenceEqual
                    success.Assert (self.sorted[item + offset - self.start]);
                }

                Contract.Assume (bits.First.HasValue);
                Contract.Assume (bits.Last.HasValue);
                success.Assert (self.start == (int)bits.First + offset);
                success.Assert (self.final == (int)bits.Last + offset);

                return success;
            }

            [Pure]
            public static bool Invariant (CodeSetPage self) {
                Success success = true;

                // private
                success.Assert (self.sorted.IsNot (null));
                success.Assert (self.sorted.IsCompact ());
                success.Assert (self.sorted.Count > Service.PairCount);	// not Null-Pair
                success.Assert (self.sorted.Count < self.sorted.Length);		// not Full
                success.Assert (self.start.UnicodePlane () == self.final.UnicodePlane ());

                // public <- private
                success.Assert (self.Length == self.sorted.Length);
                success.Assert (self.Count == self.sorted.Count);
                success.Assert (self.First == self.start);
                success.Assert (self.Last == self.final);

                // constraints
                success.Assert (self.Count > Service.PairCount);// not Unit-Pair
                success.Assert (self.Count < self.Length);				// not Full
                success.Assert (self.First.UnicodePlane () == self.Last.UnicodePlane ()); // one Page

                return success;
            }
        }
    }
}
