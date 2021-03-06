﻿// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DD.Collections.ICodeSet {

    /// <summary>
    /// Description of ICodeSetOperations.
    /// </summary>
    public static class Operations {

        private static BitSetArray NoBits {
            get { return BitSetArray.Empty (); }
        }

        #region Union or(a,b,c...)

        public static BitSetArray BitUnion (this ICodeSet self, ICodeSet that, params ICodeSet[] list) {
            Contract.Ensures (Contract.Result<BitSetArray> ().IsNot (null));
            Contract.Ensures (Contract.Result<BitSetArray> ().Length <= Code.MaxCount);

            var setList = new List<ICodeSet> ();
            setList.Add (self);
            setList.Add (that);
            if (!list.IsNull () && list.Length != 0)
                setList.AddRange (list);
            return setList.BitUnion ();
        }

        public static BitSetArray BitUnion (this IEnumerable<ICodeSet> sets) {
            Contract.Requires<ArgumentNullException> (sets.IsNot (null));
            Contract.Requires<ArgumentException> (sets.Count () >= 2);

            Contract.Ensures (Contract.Result<BitSetArray> ().IsNot (null));
            Contract.Ensures (Contract.Result<BitSetArray> ().Length <= Code.MaxCount);

            var e = sets.GetEnumerator ();
            e.MoveNext ();
            BitSetArray result = e.Current.ToBitSetArray ();
            while (e.MoveNext ()) {
                result.Or (e.Current.ToBitSetArray ());
            }
            return result;
        }

        #endregion

        #region Intersection and(((a,b),c),d...)

        public static BitSetArray BitIntersection (this ICodeSet self, ICodeSet that, params ICodeSet[] list) {
            Contract.Ensures (Contract.Result<BitSetArray> ().IsNot (null));
            Contract.Ensures (Contract.Result<BitSetArray> ().Length <= Code.MaxCount);

            var setList = new List<ICodeSet> ();
            setList.Add (self);
            setList.Add (that);
            if (!list.IsNull () && list.Length != 0)
                setList.AddRange (list);
            return setList.BitIntersection ();
        }

        public static BitSetArray BitIntersection (this IEnumerable<ICodeSet> sets) {
            Contract.Requires<ArgumentNullException> (sets.IsNot (null));
            Contract.Requires<ArgumentException> (sets.Count () >= 2);

            Contract.Ensures (Contract.Result<BitSetArray> ().IsNot (null));
            Contract.Ensures (Contract.Result<BitSetArray> ().Length <= Code.MaxCount);

            var e = sets.GetEnumerator ();
            e.MoveNext ();
            BitSetArray result = e.Current.ToBitSetArray ();
            while (e.MoveNext ()) {
                if (result.IsEmpty ()) {
                    break; // no intersection possible with empty
                }
                result.And (e.Current.ToBitSetArray ());
            }
            return result;
        }

        #endregion

        #region Disjunction xor(((a,b),c),d...)

        public static BitSetArray BitDisjunction (this ICodeSet self, ICodeSet that, params ICodeSet[] list) {
            Contract.Ensures (Contract.Result<BitSetArray> ().IsNot (null));
            Contract.Ensures (Contract.Result<BitSetArray> ().Length <= Code.MaxCount);

            var setList = new List<ICodeSet> ();
            setList.Add (self);
            setList.Add (that);
            if (!list.IsNull () && list.Length != 0)
                setList.AddRange (list);
            return setList.BitDisjunction ();
        }

        public static BitSetArray BitDisjunction (this IEnumerable<ICodeSet> sets) {
            Contract.Requires<ArgumentNullException> (sets.IsNot (null));
            Contract.Requires<ArgumentException> (sets.Count () >= 2);

            Contract.Ensures (Contract.Result<BitSetArray> ().IsNot (null));
            Contract.Ensures (Contract.Result<BitSetArray> ().Length <= Code.MaxCount);

            var e = sets.GetEnumerator ();
            e.MoveNext ();
            BitSetArray result = e.Current.ToBitSetArray ();
            while (e.MoveNext ()) {
                result.Xor (e.Current.ToBitSetArray ());
            }
            return result;
        }

        #endregion

        #region Difference (((a-b)-c)-d...)

        public static BitSetArray BitDifference (this ICodeSet self, ICodeSet that, params ICodeSet[] list) {
            Contract.Ensures (Contract.Result<BitSetArray> ().IsNot (null));
            Contract.Ensures (Contract.Result<BitSetArray> ().Length <= Code.MaxCount);

            var setList = new List<ICodeSet> ();
            setList.Add (self);
            setList.Add (that);
            if (!list.IsNull () && list.Length != 0)
                setList.AddRange (list);
            return setList.BitDifference ();
        }

        public static BitSetArray BitDifference (this IEnumerable<ICodeSet> sets) {
            Contract.Requires<ArgumentNullException> (sets.IsNot (null));
            Contract.Requires<ArgumentException> (sets.Count () >= 2);

            Contract.Ensures (Contract.Result<BitSetArray> ().IsNot (null));
            Contract.Ensures (Contract.Result<BitSetArray> ().Length <= Code.MaxCount);

            var e = sets.GetEnumerator ();
            e.MoveNext ();
            BitSetArray result = e.Current.ToBitSetArray ();
            while (e.MoveNext ()) {
                if (result.IsEmpty ()) {
                    break; // no difference possible from empty
                }
                result.Not (e.Current.ToBitSetArray ());
            }
            return result;
        }

        #endregion

        #region Complement

        public static BitSetArray BitComplement (this ICodeSet self) {
            Contract.Ensures (Contract.Result<BitSetArray> ().IsNot (null));
            Contract.Ensures (Contract.Result<BitSetArray> ().Length <= Code.MaxCount);

            if (self.Is (null) || ((self.Length - self.Count) == 0))
                return NoBits;

            var complement = self.ToBitSetArray().NotSpan();
            Contract.Assume (complement.Count != 0);
            return complement;
        }

        #endregion
    }
}
