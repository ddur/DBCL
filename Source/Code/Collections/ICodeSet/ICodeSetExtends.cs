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
    /// ICodeSet Extends.
    /// </summary>
    public static class ICodeSetExtends
    {

        #region ToCodes

        /// <summary>
        /// Converts IEnumerable&lt;int&gt; into IEnumerable&lt;Code&gt; with offset
        /// <exception cref="System.ArgumentNullException">When self is null</exception>
        /// <exception cref="System.ArgumentException">When self contains non-Code value</exception>
        /// <exception cref="System.InvalidCastException">When self contains non-Code value</exception>
        /// </summary>
        /// <param name="self">IEnumerable&lt;int&gt;</param>
        /// <param name="offset">int</param>
        /// <returns>IEnumerable&lt;Code&gt;</returns>
        public static IEnumerable<Code> ToCodes (this IEnumerable<int> self, int offset = 0) {
            Contract.Requires<ArgumentNullException> (self.IsNot (null));
            Contract.Ensures (Contract.Result<IEnumerable<Code>> ().IsNot (null));

            var e = self.GetEnumerator();
            if (offset == 0) {
                while (e.MoveNext()) {
                    yield return e.Current;
                }
            } else {
                while (e.MoveNext()) {
                    yield return (long)e.Current + offset;
                }
            }
        }

        /// <summary>
        /// Converts Predicate&lt;Code&gt; into IEnumerable&lt;Code&gt;
        /// </summary>
        /// <remarks>Use pure (functional) predicate only!</remarks>
        /// <param name="extended">Predicate&lt;Code&gt;</param>
        /// <returns>IEnumerable&lt;Code&gt;</returns>
        [Pure]
        public static IEnumerable<Code> ToCodes (this Predicate<Code> extended) {
            if (extended != null) {
                for (int item = Code.MinValue; item <= Code.MaxValue; item++) {
                    if (extended (item)) {
                        yield return item;
                    }
                }
            }
            yield break;
        }

        /// <summary>
        /// Converts Predicate&lt;Code&gt; into IEnumerable&lt;Code&gt;
        /// </summary>
        /// <remarks>Use pure (functional) predicate only!</remarks>
        /// <param name="extended">Predicate&lt;Code&gt;</param>
        /// <returns>IEnumerable&lt;Code&gt;</returns>
        public static IEnumerable<int> ToIntCodes (this Predicate<Code> extended) {
            if (extended != null) {
                for (int item = Code.MinValue; item <= Code.MaxValue; item++) {
                    if (extended ((Code)item)) {
                        yield return item;
                    }
                }
            }
            yield break;
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
        internal static bool IsCompact (this int[] self) {
            if (self.IsCompactLast () < 0) {
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
        public static int IsCompactLast (this int[] self) {
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

        #endregion

    }
}
