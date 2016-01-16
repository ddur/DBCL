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
using DD.Text;

namespace DD.Collections.ICodeSet {

    public class Distinct {

        #region Embeds

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

        #endregion

        #region Fields

        private readonly C5.HashSet<ICodeSet> distinct = new C5.HashSet<ICodeSet> ();

        #endregion

        #region From items

        public ICodeSet From (string utf16) {
            Contract.Requires<ArgumentException> (utf16.IsNullOrEmpty() || utf16.CanDecode ());
            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));

            return string.IsNullOrEmpty (utf16) ? CodeSetNone.Singleton : From (utf16.Decode ());
        }

        public ICodeSet From (char req, params char[] opt) {
            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));

            var args = new List<char>() {req};
            if (!opt.IsNullOrEmpty()) { args.AddRange(opt); }
            return From (args);
        }

        public ICodeSet From (Code req, params Code[] opt) {
            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));

            var args = new List<Code>() {req};
            if (!opt.IsNullOrEmpty()) { args.AddRange(opt); }
            return From (args);
        }

        public ICodeSet From (IEnumerable<char> chars) {
            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));
            Contract.Ensures (Theory.From (chars, this, Contract.Result<ICodeSet> ()));

            if (chars.IsNullOrEmpty()) {
                return CodeSetNone.Singleton;
            } else {
                var args = new List<Code>();
                foreach (var item in chars) {
                    args.Add(item);
                }
                return From (args);
            }
        }

        public ICodeSet From (IEnumerable<Code> codes) {
            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));
            Contract.Ensures (Theory.From (codes, this, Contract.Result<ICodeSet> ()));

            return codes.IsNullOrEmpty () ? CodeSetNone.Singleton : QuickFrom (BitSetArray.From (codes.ToValues ()));
        }

        public ICodeSet From (BitSetArray bits) {
            Contract.Requires<ArgumentException> (bits.IsNullOrEmpty() || (int)bits.Last <= Code.MaxValue, "Last bit is larger than Code.MaxValue");

            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));
            Contract.Ensures (Theory.From (bits, this, Contract.Result<ICodeSet> ()));

            // bits is created external to this class, it is not transient. Use safe copy of bits!
            return bits.IsNullOrEmpty () ? CodeSetNone.Singleton : From ((ICodeSet)QuickWrap.From(BitSetArray.Copy(bits)));
        }

        /// <summary>
        /// Quick creates ICodeSet for dictionary key
        /// </summary>
        /// <param name="bits"></param>
        /// <returns></returns>
        internal ICodeSet QuickFrom (BitSetArray bits) {
            Contract.Requires<ArgumentException> (bits.IsNullOrEmpty() || (int)bits.Last <= Code.MaxValue, "Last bit is larger than Code.MaxValue");

            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));
            Contract.Ensures (Theory.From (bits, this, Contract.Result<ICodeSet> ()));

            return bits.IsNullOrEmpty () ? CodeSetNone.Singleton : From ((ICodeSet)QuickWrap.From (bits));
        }

        public ICodeSet From (ICodeSet iset) {
            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));
            Contract.Ensures (Theory.From (iset, this, Contract.Result<ICodeSet> ()));

            if (iset.IsNullOrEmpty ()) {
                return CodeSetNone.Singleton;
            }
            ICodeSet key = iset;
            if (!distinct.Find (ref key)) {
                var qset = iset as QuickWrap;
                key = qset.IsNull () ? iset.Reduce () : qset.ToBitSetArray ().Reduce ();
                distinct.Add (key);
            }
            return key;
        }

        #endregion

        #region Distinct Collection

        public IEnumerable<ICodeSet> Collection {
            get {
                Contract.Ensures (Contract.Result<IEnumerable<ICodeSet>> ().IsNot (null));
                foreach (var item in distinct) {
                    yield return item;
                }
            }
        }

        public bool Contains (ICodeSet set) {
            return distinct.Contains (set);
        }

        public int Count {
            get {
                return distinct.Count;
            }
        }
        #endregion

        #region Set Operations

        /*  ICodeSet operation names differ from ISet<T> names because
            ICodeSet operation does not mutate any operand and returns new set.
            (like string, unlike ISet<T> operations)
        */

        #region Union bits.Or(a,b,c...)

        public ICodeSet Union (ICodeSet lhs, ICodeSet rhs, params ICodeSet[] opt) {
            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));

            var args = new List<ICodeSet>() {lhs, rhs};
            if (!opt.IsNullOrEmpty()) { args.AddRange(opt); }
            return Union(args);
        }

        public ICodeSet Union (IEnumerable<ICodeSet> sets) {
            Contract.Requires<ArgumentNullException> (sets.IsNot (null));
            Contract.Requires<ArgumentException> (sets.Count () >= 2);

            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));

            return QuickFrom (sets.BitUnion ());
        }

        #endregion

        #region Intersection bits.And(((a,b),c),d...)

        public ICodeSet Intersection (ICodeSet lhs, ICodeSet rhs, params ICodeSet[] opt) {
            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));

            var args = new List<ICodeSet>() {lhs, rhs};
            if (!opt.IsNullOrEmpty()) { args.AddRange(opt); }
            return Intersection(args);
        }

        public ICodeSet Intersection (IEnumerable<ICodeSet> sets) {
            Contract.Requires<ArgumentNullException> (sets.IsNot (null));
            Contract.Requires<ArgumentException> (sets.Count () >= 2);

            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));

            return QuickFrom (sets.BitIntersection ());
        }

        #endregion

        #region Disjunction bits.Xor(((a,b),c),d...)

        public ICodeSet Disjunction (ICodeSet lhs, ICodeSet rhs, params ICodeSet[] opt) {
            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));

            var args = new List<ICodeSet>() {lhs, rhs};
            if (!opt.IsNullOrEmpty()) { args.AddRange(opt); }
            return Disjunction(args);
        }

        public ICodeSet Disjunction (IEnumerable<ICodeSet> sets) {
            Contract.Requires<ArgumentNullException> (sets.IsNot (null));
            Contract.Requires<ArgumentException> (sets.Count () >= 2);

            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));

            return QuickFrom (sets.BitDisjunction ());
        }

        #endregion

        #region Difference bits.Not(((a-b)-c)-d...)

        public ICodeSet Difference (ICodeSet lhs, ICodeSet rhs, params ICodeSet[] opt) {
            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));

            var args = new List<ICodeSet>() {lhs, rhs};
            if (!opt.IsNullOrEmpty()) { args.AddRange(opt); }
            return Difference(args);
        }

        public ICodeSet Difference (IEnumerable<ICodeSet> sets) {
            Contract.Requires<ArgumentNullException> (sets.IsNot (null));
            Contract.Requires<ArgumentException> (sets.Count () >= 2);

            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));

            return QuickFrom (sets.BitDifference ());
        }

        #endregion

        #region Complement bits.Not()

        public ICodeSet Complement (ICodeSet that) {
            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));

            return QuickFrom (that.BitComplement ());
        }

        #endregion

        #endregion

        #region Invariant

        [ContractInvariantMethod]
        private void Invariant () {
            Contract.Invariant (distinct.IsNot (null));
            Contract.Invariant (!distinct.Contains (CodeSetNone.Singleton));
        }

        #endregion

        #region Theory

        private static class Theory {

            [Pure]
            public static bool Result (Distinct self, ICodeSet result) {
                Success success = true;

                success.Assert (result.IsNot (null));
                success.Assert (result.IsReduced ());

                if (result.Count != 0) {
                    var key = result;
                    success.Assert (self.distinct.Find (ref key));
                    success.Assert (key.Is (result));
                }
                return success;
            }

            [Pure]
            public static bool From (IEnumerable<char> chars, Distinct self, ICodeSet result) {
                Success success = true;

                if (!chars.IsNullOrEmpty ()) {
                    var codes = new List<Code>();
                    foreach (var item in chars) { codes.Add(item); }
                    success.Assert (codes.Distinct ().OrderBy (item => (item)).SequenceEqual (result));
                }
                else {
                    success.Assert (result.Count == 0);
                }
                return success;
            }

            [Pure]
            public static bool From (IEnumerable<Code> codes, Distinct self, ICodeSet result) {
                Success success = true;

                if (!codes.IsNullOrEmpty ()) {
                    success.Assert (codes.Distinct ().OrderBy (item => (item)).SequenceEqual (result));
                }
                else {
                    success.Assert (result.Count == 0);
                }
                return success;
            }

            [Pure]
            public static bool From (BitSetArray bits, Distinct self, ICodeSet result) {
                Success success = true;

                if (!bits.IsNullOrEmpty ()) {
                    success.Assert (bits.ToCodes ().Distinct ().OrderBy (item => (item)).SequenceEqual (result));
                }
                else {
                    success.Assert (result.Count == 0);
                }
                return success;
            }
        }

        #endregion
    }
}
