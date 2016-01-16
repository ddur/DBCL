/*
 * Created by SharpDevelop.
 * User: ddur
 * Date: 17.1.2016.
 * Time: 0:15
 * 
 */
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using DD.Diagnostics;

namespace DD.Collections.ICodeSet
{
    /// <summary>QuickWrap ICodeSet over BitSetArray</summary>
    /// <remarks><para>It is private because it is unsafe for public use</para>
    /// <para>It does not copy constructor argument! It holds reference to external BitSetArray</para>
    /// <para>Used to quick wrap ICodeSet over TRANSIENT-ONLY, within parent class created, BitSetArray</para></remarks>
    internal class QuickWrap : CodeSet {

        #region Ctor

        public static QuickWrap From(BitSetArray bits) {
            Contract.Requires<ArgumentNullException> (bits.IsNot (null));
            return new QuickWrap (bits);
        }

        private QuickWrap (BitSetArray bits) {
            Contract.Requires<ArgumentNullException> (bits.IsNot (null));
            Contract.Requires<ArgumentException> (bits.Count != 0);
            Contract.Requires<ArgumentException> ((int)bits.Last <= Code.MaxValue);

            Contract.Ensures (Theory.Construct (bits, this));

            this.sorted = bits;

            Contract.Assume (sorted.Count > 0);
            Contract.Assume (sorted.First.HasValue);
            Contract.Assume (sorted.Last.HasValue);
            Contract.Assume (sorted.First.HasCodeValue ());
            Contract.Assume (sorted.Last.HasCodeValue ());

            this.count = sorted.Count;
            this.length = sorted.Span ();
            this.start = (Code)sorted.First;
            this.final = (Code)sorted.Last;
        }

        #endregion

        #region Fields

        private readonly BitSetArray sorted;

        // stupid but so far this helps code-contracts static checker to pass
        private readonly int count;

        private readonly int length;
        private readonly Code start;
        private readonly Code final;

        #endregion

        #region ICodeSet

        [Pure]
        public override bool this[Code code] {
            get {
                return sorted[code.Value];
            }
        }

        [Pure]
        public override int Count {
            get {
                return this.count;
            }
        }

        [Pure]
        public override int Length {
            get {
                return this.length;
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

        /// <summary>
        /// This ICodeSet implementation is temporary, must be reduced
        /// </summary>
        [Pure]
        public override bool IsReduced {
            get {
                return false;
            }
        }

        [Pure]
        public override IEnumerator<Code> GetEnumerator () {
            foreach (var code in sorted) {
                yield return (Code)code;
            }
        }

        #endregion

        #region To BitSetArray

        public BitSetArray ToBitSetArray () {
            return sorted;
        }

        #endregion

        #region Invariant

        [ContractInvariantMethod]
        private void Invariant () {
            Contract.Invariant(Theory.Invariant(this));
        }

        #endregion

        #region Theory

        private static class Theory {

            [Pure]
            public static bool Construct (BitSetArray bits, QuickWrap self) {
                Success success = true;

                success.Assert (!bits.IsNull ());
                success.Assert (bits.Count > 0);

                success.Assert (bits.First.HasValue);
                success.Assert (bits.Last.HasValue);

                success.Assert (self.sorted.First.HasCodeValue ());
                success.Assert (self.sorted.Last.HasCodeValue ());

                success.Assert (self.sorted.Is (bits));

                return success;
            }

            [Pure]
            public static bool Invariant (QuickWrap self) {
                Success success = true;

                // private
                success.Assert (!self.sorted.IsNull ());
                success.Assert (self.sorted.Count > 0);

                success.Assert (self.sorted.First.HasValue);
                success.Assert (self.sorted.Last.HasValue);
                Contract.Assume (self.sorted.First.HasValue);
                Contract.Assume (self.sorted.Last.HasValue);

                success.Assert (self.sorted.First.HasCodeValue ());
                success.Assert (self.sorted.Last.HasCodeValue ());

                // public <- private
                success.Assert (self.Length == self.sorted.Span ());
                success.Assert (self.Count == self.sorted.Count);
                success.Assert (self.First == (Code)self.sorted.First);
                success.Assert (self.Last == (Code)self.sorted.Last);

                return success;
            }
        }
        #endregion
    }
}
