// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DD.Collections.ICodeSet
{
    /// <summary>
    /// ICodeSet Extensions
    /// </summary>
    public static class ICodeSetExtended
    {

        #region Properties

        [Pure]
        public static bool IsEmpty (this ICodeSet self) {
            Contract.Requires<ArgumentNullException> (self.IsNot (null));
            return self.Count == 0;
        }

        [Pure]
        public static bool IsNullOrEmpty (this ICodeSet self) {
            return self.IsNull () || self.Count == 0;
        }

        [Pure]
        public static bool IsFull (this ICodeSet self) {
            return !self.IsNull () && self.Count != 0 && self.Count == self.Length;
        }


        [Pure]
        public static int Span (this IEnumerable<Code> self) {
            if (self.IsNull ())
                return 0;
            if (self.IsEmpty ())
                return 0;
            int maxValue = int.MinValue;
            int minValue = int.MaxValue;
            foreach (var item in self) {
                if (item < minValue) minValue = item;
                if (item > maxValue) maxValue = item;
            }
            return maxValue - minValue + 1;
        }

        #endregion

        #region ToValues & ToBitSetArray

        public static IEnumerable<int> ToValues (this IEnumerable<Code> codes) {
            Contract.Ensures (Contract.Result<IEnumerable<int>> ().IsNot (null));
            if (codes.IsNot (null)) {
                foreach (Code code in codes) {
                    yield return code.Value;
                }
            }
            else {
                yield break;
            }
        }

        public static IEnumerable<int> ToValues (this IEnumerable<Code> codes, int offset) {
            Contract.Ensures (Contract.Result<IEnumerable<int>> ().IsNot (null));
            if (codes.IsNot (null)) {
                foreach (Code code in codes) {
                    yield return code.Value + offset;
                }
            }
            else {
                yield break;
            }
        }

        /// <summary>Returns BitSetArray(self.Last+1) where BitSetArray.Item == self.Item
        /// </summary>
        /// <param name="self"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [Pure]
        public static BitSetArray ToBitSetArray (this ICodeSet self, int offset = 0) {
            Contract.Ensures (
                (self.IsNull () && Contract.Result<BitSetArray> ().Count == 0) ||
                (self.Count == Contract.Result<DD.Collections.BitSetArray> ().Count)
            );
            Contract.Ensures (Contract.Result<BitSetArray> ().IsNot (null));

            if (self.IsNull () || self.Count == 0) {
                return BitSetArray.Empty ();
            }
            if (offset == 0) {
                var quick = self as QuickWrap;
                if (quick.IsNot (null)) {
                    return quick.ToBitSetArray(); // O(1)
                }
            }
            return BitSetArray.From (self.ToValues(offset)); // O(N) .. O(self.Enumerator)
        }

        #endregion

    }
}
