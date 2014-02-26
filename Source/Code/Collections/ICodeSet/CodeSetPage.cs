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

    /// <summary>CodeSetPage contains only codes within same unicode plane/page
    /// </summary>
    public sealed class CodeSetPage : CodeSet {

        #region Ctor

        // TODO Theory.Compact(BitSetArray, offset)
        /// <summary>From Compact BitSetArray
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="offset"></param>
        internal CodeSetPage (BitSetArray bits, int offset) {
            Contract.Requires<ArgumentNullException> (bits.IsNot(null));
            Contract.Requires<ArgumentException> (bits.Count > ICodeSetService.PairCount);
            Contract.Requires<ArgumentException> (bits[0] == true);
            Contract.Requires<ArgumentException> (bits[bits.Length-1] == true);
            Contract.Requires<ArgumentException> (offset >= 0);
            Contract.Requires<ArgumentException> (offset <= Code.MaxCount - bits.Length);
			Contract.Requires<ArgumentException> (((Code)(bits.First+offset)).UnicodePlane() == ((Code)(bits.Last+offset)).UnicodePlane());

            // Input -> Output
            Contract.Ensures (this.Count == bits.Count);
            Contract.Ensures (this.First == offset);
            Contract.Ensures (this.Last == offset + bits.Length - 1);

            this.sorted = BitSetArray.Copy(bits);
            Contract.Assume (this.sorted.Equals(bits));

            this.start = offset;
            this.final = offset + this.sorted.Length - 1;
        }

        internal CodeSetPage (BitSetArray bits) {
            Contract.Requires<ArgumentNullException> (!bits.Is(null));
            Contract.Requires<ArgumentException> (bits.Count > ICodeSetService.PairCount);
            Contract.Requires<ArgumentOutOfRangeException> (bits.Last <= Code.MaxValue);
            Contract.Requires<ArgumentException> (((Code)bits.First).UnicodePlane() == ((Code)bits.Last).UnicodePlane());

            // Internal
            Contract.Ensures (this.sorted.IsNot (null));

            // Input -> Output
            Contract.Ensures (this.Count == bits.Count);
            Contract.Ensures (Contract.ForAll (bits, item => this[item]));
            Contract.Ensures (this.First.UnicodePlane() == this.Last.UnicodePlane());

            this.start = (int)bits.First;
            this.final = (int)bits.Last;
            this.sorted = new BitSetArray (this.final - this.start + 1);
            foreach ( Code code in bits ) {
                this.sorted.Set (code - this.start, true);
            }
        }

        internal CodeSetPage (IEnumerable<Code> codes) {

            Contract.Requires<ArgumentNullException> (!codes.Is(null));
            Contract.Requires<ArgumentException> (codes.Distinct().Count() > ICodeSetService.PairCount);
            Contract.Requires<ArgumentException> (codes.Min().UnicodePlane() == codes.Max().UnicodePlane());

            // Internal
            Contract.Ensures (this.sorted.IsNot (null));

            // Input -> Output
            Contract.Ensures (Contract.ForAll (codes, item => this[item]));
            Contract.Ensures (this.sorted.Count == codes.Distinct().Count());
            Contract.Ensures (this.First.UnicodePlane() == this.Last.UnicodePlane());

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

            // private
            Contract.Invariant (this.sorted.IsNot (null));
            Contract.Invariant (this.sorted.Length == 1 + this.final - this.start);
            Contract.Invariant (this.sorted.Count > ICodeSetService.PairCount);
            Contract.Invariant (this.sorted[0]);
            Contract.Invariant (this.sorted[this.final - this.start]);

            // public <- private
            Contract.Invariant (this.Length == this.sorted.Length);
            Contract.Invariant (this.Count == this.sorted.Count);
            Contract.Invariant (this.First == this.start);
            Contract.Invariant (this.Last == this.final);

            // public
            Contract.Invariant (this.First.UnicodePlane() == this.Last.UnicodePlane());
            Contract.Invariant (this.Length <= (char.MaxValue + 1));
            Contract.Invariant (this.Count > ICodeSetService.PairCount);

        }

        #endregion

        internal BitSetArray ToCompact () {
            return BitSetArray.Copy (this.sorted);
        }
        
    }
}
