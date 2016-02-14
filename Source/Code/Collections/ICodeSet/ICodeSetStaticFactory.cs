// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using DD.Text;

namespace DD.Collections.ICodeSet {

    /// <summary>Produces Reduced ICodeSet's</summary>
    /// <remarks>By default do not allow producing empty set</remarks>
    /// <remarks>Empty set is required evil, produced by some of set operations</remarks>
    public static class Factory {

        #region Range To ICodeSet

        public static ICodeSet Range (this Code start, Code final) {
            Contract.Requires<ArgumentException> (start <= final);

            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            switch (final - start + 1) {
                case 1:
                    return (Code)start;
                case 2:
                    return CodeSetPair.From (start, final);
                default:
                    return CodeSetFull.From (start, final);
            }
        }

        #endregion

        #region From items To ICodeSet

        public static ICodeSet From (this Predicate<Code> func) {
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            if (func.Is(null) || func.ToCodes().IsEmpty()) {
                return CodeSetNone.Singleton;
            }
            var bits = BitSetArray.Size (Code.MaxCount);
            bits._SetMembers (func.ToIntCodes());
            return bits.ToICodeSet();
        }

        public static ICodeSet From (this string utf16) {
            Contract.Requires<ArgumentException> (utf16.IsNullOrEmpty () || utf16.CanDecode ());

            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            return utf16.IsNullOrEmpty () ? CodeSetNone.Singleton : utf16.ToICodeSet ();
        }

        public static ICodeSet From (this char req, params char[] opt) {
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            var args = new List<char>() {req};
            if (!opt.IsNullOrEmpty()) { args.AddRange(opt); }
            return args.ToICodeSet ();
        }

        public static ICodeSet From (this Code req, params Code[] opt) {
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            var args = new List<Code>() {req};
            if (!opt.IsNullOrEmpty()) { args.AddRange(opt); }
            return args.ToICodeSet() ;
        }

        public static ICodeSet From (ICodeSet iCodeSet) {
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            return iCodeSet.Reduce() ;
        }

        public static ICodeSet ToICodeSet (this string utf16) {
            Contract.Requires<ArgumentException>(utf16.IsNullOrEmpty() || utf16.CanDecode());
        	
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            return utf16.IsNullOrEmpty () ? CodeSetNone.Singleton : utf16.Decode().ToICodeSet ();
        }

        public static ICodeSet ToICodeSet (this IEnumerable<char> chars) {
        	Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            return chars.IsNullOrEmpty () ? CodeSetNone.Singleton : BitSetArray.From (chars.ToValues ()).ToICodeSet ();
        }

        public static ICodeSet ToICodeSet (this IEnumerable<Code> codes) {
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            return codes.IsNullOrEmpty () ? CodeSetNone.Singleton : BitSetArray.From (codes.ToValues ()).ToICodeSet ();
        }

        public static ICodeSet ToICodeSet (this BitSetArray bits) {
            Contract.Requires<ArgumentException> (bits.IsNullOrEmpty() || (int)bits.Last <= Code.MaxValue, "Last bit is larger than Code.MaxValue");

            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            return bits.IsNullOrEmpty () ? CodeSetNone.Singleton : bits.Reduce ();
        }

        #endregion

        #region Operations

        #region Union bit.Or(a,b,c...)

        public static ICodeSet Union (this ICodeSet self, ICodeSet that, params ICodeSet[] more) {
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            var args = new List<ICodeSet>() {self, that};
            if (!more.IsNullOrEmpty()) { args.AddRange(more); }
            return args.Union();
        }

        public static ICodeSet Union (this IEnumerable<ICodeSet> sets) {
            Contract.Requires<ArgumentNullException> (!sets.IsNull ());
            Contract.Requires<ArgumentException> (sets.Count () >= 2, "At least two sets required for any binary operation");

            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            return sets.BitUnion ().ToICodeSet ();
        }

        #endregion

        #region Intersection bit.And(((a,b),c),d...)

        public static ICodeSet Intersection (this ICodeSet self, ICodeSet that, params ICodeSet[] more) {
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            var args = new List<ICodeSet>() {self, that};
            if (!more.IsNullOrEmpty()) { args.AddRange(more); }
            return args.Intersection();
        }

        public static ICodeSet Intersection (this IEnumerable<ICodeSet> sets) {
            Contract.Requires<ArgumentNullException> (!sets.IsNull ());
            Contract.Requires<ArgumentException> (sets.Count () >= 2, "At least two sets required for any binary operation");

            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            return sets.BitIntersection ().ToICodeSet ();
        }

        #endregion

        #region Disjunction xor(((a,b),c),d...)

        public static ICodeSet Disjunction (this ICodeSet self, ICodeSet that, params ICodeSet[] more) {
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            var args = new List<ICodeSet>() {self, that};
            if (!more.IsNullOrEmpty()) { args.AddRange(more); }
            return args.Disjunction();
        }

        public static ICodeSet Disjunction (this IEnumerable<ICodeSet> sets) {
            Contract.Requires<ArgumentNullException> (!sets.IsNull ());
            Contract.Requires<ArgumentException> (sets.Count () >= 2, "At least two sets required for any binary operation");

            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            return sets.BitDisjunction ().ToICodeSet ();
        }

        #endregion

        #region Difference (((a-b)-c)-d...)

        public static ICodeSet Difference (this ICodeSet self, ICodeSet that, params ICodeSet[] more) {
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            var args = new List<ICodeSet>() {self, that};
            if (!more.IsNullOrEmpty()) { args.AddRange(more); }
            return args.Difference();
        }

        public static ICodeSet Difference (this IEnumerable<ICodeSet> sets) {
            Contract.Requires<ArgumentNullException> (!sets.IsNull ());
            Contract.Requires<ArgumentException> (sets.Count () >= 2, "At least two sets required for any binary operation");

            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            return sets.BitDifference ().ToICodeSet ();
        }

        #endregion

        #region Complement

        public static ICodeSet Complement (this ICodeSet self) {
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced);

            return self.BitComplement ().ToICodeSet();
        }

        #endregion

        #endregion
    }
}
