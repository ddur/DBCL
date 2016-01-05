// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
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

        #region From items To ICodeSet

        public static ICodeSet From (char req, params char[] opt) {
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced ());

            List<char> charList;
            if (!opt.IsNull () && opt.Length > 0) {
                charList = new List<char> (1 + opt.Length);
                charList.Add (req);
                foreach (char code in opt) {
                    charList.Add (code);
                }
            }
            else {
                charList = new List<char> () { req };
            }
            return charList.ToICodeSet ();
        }

        public static ICodeSet From (Code req, params Code[] opt) {
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced ());

            List<Code> codeList;
            if (!opt.IsNull () && opt.Length > 0) {
                codeList = new List<Code> (1 + opt.Length);
                codeList.Add (req);
                codeList.AddRange (opt);
            }
            else { // type Code is never null
                codeList = new List<Code> () { req };
            }
            return codeList.ToICodeSet() ;
        }

        public static ICodeSet From (string utf16) {
        	Contract.Requires<ArgumentNullException>(!utf16.Is (null));
        	Contract.Requires<ArgumentEmptyException>(!utf16.Is (string.Empty));
        	Contract.Requires<ArgumentException>(utf16.CanDecode());
        	
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced ());

            return utf16.ToICodeSet ();
        }

        public static ICodeSet ToICodeSet (this string utf16) {
        	Contract.Requires<ArgumentNullException>(!utf16.Is (null));
        	Contract.Requires<ArgumentEmptyException>(!utf16.Is (string.Empty));
        	Contract.Requires<ArgumentException>(utf16.CanDecode());
        	
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced ());

            return utf16.Decode().ToICodeSet ();
        }

        public static ICodeSet ToICodeSet (this IEnumerable<char> chars) {
        	Contract.Requires<ArgumentNullException>(!chars.Is (null));
        	Contract.Requires<ArgumentEmptyException>(!chars.IsEmpty());

        	Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced ());

            return BitSetArray.From (chars.ToValues ()).ToICodeSet ();
        }

        public static ICodeSet ToICodeSet (this IEnumerable<Code> codes) {
        	Contract.Requires<ArgumentNullException>(!codes.Is (null));
        	Contract.Requires<ArgumentEmptyException>(!codes.IsEmpty());

            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced ());

            return BitSetArray.From (codes.ToValues ()).ToICodeSet ();
        }

        public static ICodeSet ToICodeSet (this BitSetArray bits) {
            Contract.Requires<ArgumentNullException> (!bits.Is (null));
            Contract.Requires<ArgumentEmptyException> (!bits.IsEmpty());
            Contract.Requires<ArgumentException> ((int)bits.Last <= Code.MaxValue, "Last bit is larger than Code.MaxValue");

            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced ());

            return bits.IsNullOrEmpty () ? CodeSetNone.Singleton : bits.Reduce ();
        }

        #endregion

        #region Operations

        #region Union bit.Or(a,b,c...)

        public static ICodeSet Union (this ICodeSet self, ICodeSet that, params ICodeSet[] more) {
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced ());

            return ToICodeSet (self.BitUnion (that, more));
        }

        public static ICodeSet Union (this IEnumerable<ICodeSet> sets) {
            Contract.Requires (!sets.IsNull ());
            Contract.Requires (sets.Count () >= 2);

            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced ());

            return ToICodeSet (sets.BitUnion ());
        }

        #endregion

        #region Intersection bit.And(((a,b),c),d...)

        public static ICodeSet Intersection (this ICodeSet self, ICodeSet that, params ICodeSet[] more) {
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced ());

            return ToICodeSet (self.BitIntersection (that, more));
        }

        public static ICodeSet Intersection (this IEnumerable<ICodeSet> sets) {
            Contract.Requires (!sets.IsNull ());
            Contract.Requires (sets.Count () >= 2);

            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced ());

            return ToICodeSet (sets.BitIntersection ());
        }

        #endregion

        #region Disjunction xor(((a,b),c),d...)

        public static ICodeSet Disjunction (this ICodeSet self, ICodeSet that, params ICodeSet[] more) {
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced ());

            return ToICodeSet (self.BitDisjunction (that, more));
        }

        public static ICodeSet Disjunction (this IEnumerable<ICodeSet> sets) {
            Contract.Requires (!sets.IsNull ());
            Contract.Requires (sets.Count () >= 2);

            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced ());

            return ToICodeSet (sets.BitDisjunction ());
        }

        #endregion

        #region Difference (((a-b)-c)-d...)

        public static ICodeSet Difference (this ICodeSet self, ICodeSet that, params ICodeSet[] more) {
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced ());

            return ToICodeSet (self.BitDifference (that, more));
        }

        public static ICodeSet Difference (this IEnumerable<ICodeSet> sets) {
            Contract.Requires (!sets.IsNull ());
            Contract.Requires (sets.Count () >= 2);

            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced ());

            return ToICodeSet (sets.BitDifference ());
        }

        #endregion

        #region Complement

        public static ICodeSet Complement (this ICodeSet self) {
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().IsReduced ());

            return ToICodeSet (self.BitComplement ());
        }

        #endregion

        #endregion
    }
}
