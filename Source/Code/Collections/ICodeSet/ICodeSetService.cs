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

    /// <summary>Provides conversion, relation and predicate services
    /// <remarks>All public except methods returning ICodeSet</remarks>
    /// </summary>
    public static class Service {
        public const int NullCount = 0;
        public const int UnitCount = 1;
        public const int PairCount = 2;

        // NOTE: log(16) == 4 tests, TODO: check speed on 32 and 64
        public const int ListMaxCount = 16;

        public const int MaskMaxSpan = 256;

        public const int NoneStart = -1;
        public const int NoneFinal = -2;

        #region To Service

        public static IEnumerable<Code> ToCodes (this IEnumerable<int> ints, int offset = 0) {
            Contract.Requires<ArgumentNullException> (ints.IsNot (null));
            Contract.Requires<ArgumentException> (Contract.ForAll (ints, x => (x + offset).HasCodeValue ()));
            Contract.Ensures (Contract.Result<IEnumerable<Code>> ().IsNot (null));

            if (offset == 0) {
                foreach (var code in ints) {
                    yield return (Code)code;
                }
            }
            else {
                int shifted;
                foreach (var code in ints) {
                    shifted = code + offset;
                    yield return (Code)shifted;
                }
            }
        }

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

        public static IEnumerable<int> ToValues (this IEnumerable<char> codes) {
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

        [Pure]
        public static int Span (this BitSetArray self) {
            if (self.IsNull ())
                return 0;
            if (self.Count == 0)
                return 0;
            return 1 + (int)self.Last - (int)self.First;
        }

        [Pure]
        public static int Span (this IEnumerable<Code> self) {
            if (self.IsNull ())
                return 0;
            if (!self.Any ())
                return 0;
            return 1 + self.Max () - self.Min ();
        }

        /// <summary>IsCompact if:
        /// <para>1) is not null</para>
        /// <para>2) is not empty</para>
        /// <para>3) has first[0] and last[length-1] bit set</para></summary>
        /// <param name="self">BitSetArray</param>
        /// <returns>bool</returns>
        [Pure]
        public static bool IsCompact (this BitSetArray self) {
            if (self.IsNull ()) {
                return false;
            }
            if (self.Count == 0) {
                return self.Length == 0;
            }
            return self[0] && self[self.Length - 1] && self.Length.IsCodesCount ();
        }

        /// <summary>IsCompact if:
        /// <para>1) is not null</para>
        /// <para>2) is not empty</para>
        /// <para>3) Has at least first bit set</para>
        /// <para>4) Last self.Last() is not 0 (no empty tail)</para>
        /// </summary>
        /// <param name="self">int[]</param>
        /// <returns>bool</returns>
        [Pure]
        internal static bool IsCodeCompact (this int[] self) {
            if (self.IsCodeCompactLast () < 0) {
                return false;
            }
            return true;
        }

        /// <summary>IsCompact if:
        /// <para>1) is not null</para>
        /// <para>2) is not empty</para>
        /// <para>3) Has at least first bit set</para>
        /// <para>4) Last self.Last() is not 0 (no empty tail)</para>
        /// </summary>
        /// <param name="self">int[]</param>
        /// <returns>-1 if not compact else index of last bit set</returns>
        [Pure]
        internal static int IsCodeCompactLast (this int[] self) {
            int lastBitIndex = -1;
            if (self.IsNull ()) {
                return lastBitIndex;
            }
            if (0 == self.Length) {
                return lastBitIndex;
            }
            if (((Code.MaxValue >> 5) + 1) < self.Length) {
                return lastBitIndex;
            }
            if (0 == (self[0] & 1)) { // first bit must be set
                return lastBitIndex;
            }
            if (0 == (self[self.Length - 1])) { // last item = 0, does not contains bits
                return lastBitIndex;
            }
            lastBitIndex = (self.Length - 1) << 5;
            uint bitMask = unchecked ((uint)self[self.Length - 1]);
            while (bitMask != 0) {
                bitMask >>= 1;
                ++lastBitIndex;
            }
            --lastBitIndex; // zero indexed bits
            return lastBitIndex;
        }

        /// <summary>Returns BitSetArray(self.Last+1) where BitSetArray.Item == self.Item
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        [Pure]
        public static BitSetArray ToBitSetArray (this ICodeSet self) {
            Contract.Ensures (
                (self.IsNull () && Contract.Result<BitSetArray> ().Count == 0) ||
                (self.Count == Contract.Result<DD.Collections.BitSetArray> ().Count)
            );
            Contract.Ensures (Contract.Result<BitSetArray> ().IsNot (null));

            if (self.IsNull () || self.Count == 0) {
                return BitSetArray.Empty ();
            }
            var quick = self as Distinct.QuickWrap;
            if (quick.IsNot (null)) {
                return quick.ToBitSetArray(); // O(1)
            }
            var ret = BitSetArray.Size (self.Last + 1);
            foreach (int code in self) {
                ret._Set (code);
            }
            return ret;
        }

        /// <summary>Returns compact BitSetArray(self.Length) where BitSetArray.Item == self.Item-self.First (offset)
        /// <remarks>Use to serialize ICodeSet</remarks>
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        [Pure]
        public static BitSetArray ToCompact (this ICodeSet self) {
            Contract.Ensures (Contract.Result<BitSetArray> ().IsNot (null));
            Contract.Ensures (Contract.Result<BitSetArray> ().IsCompact ());

            if (self.IsNull () || self.Count == 0)
                return BitSetArray.Empty ();

            var ret = BitSetArray.Size (self.Length);
            foreach (int code in self) {
                ret._Set (code - self.First);
            }

            // first and last bit set == compact
            Contract.Assert (ret[0]);
            Contract.Assert (ret[ret.Length - 1]);

            return ret;
        }

        #endregion

        #region Properties

        [Pure]
        public static bool IsEmpty (this ICodeSet self) {
            Contract.Requires<ArgumentNullException> (self.IsNot (null));
            return self.Count == 0;
        }

        [Pure]
        public static bool IsEmpty (this BitSetArray self) {
            Contract.Requires<ArgumentNullException> (self.IsNot (null));
            return self.Count == 0;
        }

        [Pure]
        public static bool IsNullOrEmpty (this ICodeSet self) {
            return self.IsNull () || self.Count == 0;
        }

        [Pure]
        public static bool IsNullOrEmpty (this BitSetArray self) {
            return self.IsNull () || self.Count == 0;
        }

        [Pure]
        public static bool IsFull (this ICodeSet self) {
            return !self.IsNull () && self.Count != 0 && self.Count == self.Length;
        }

        [Pure]
        public static bool IsFull (this BitSetArray self) {
            return !self.IsNull () && self.Count != 0 && (self.Count == self.Span ());
        }

        #endregion
    }
}
