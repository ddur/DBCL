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

        #region Fields

        private readonly C5.HashSet<ICodeSet> distinct = new C5.HashSet<ICodeSet> ();

        #endregion

        #region Range to ICodeSet

        public ICodeSet Range (Code start, Code final) {
            Contract.Requires<ArgumentException> (start <= final);

            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            switch (final - start + 1) {
                case 1:
                    return From (start);
                case 2:
                    return From (CodeSetPair.From (start, final));
                default:
                    return From (CodeSetFull.From (start, final));
            }
        }

        #endregion

        #region From items to ICodeSet

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
                success.Assert (result.IsReduced);

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
