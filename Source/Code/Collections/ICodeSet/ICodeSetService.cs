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

    /// <summary>Provides conversion, relation and predicate services
    /// <remarks>All public except methods returning ICodeSet</remarks>
    /// </summary>
    public static class ICodeSetService {
        public const int NullCount = 0;
        public const int UnitCount = 1;
        public const int PairCount = 2;

        // NOTE: log(16) == 4 tests, TODO: check speed on 32 and 64
        public const int ListMaxCount = 16;

        public const int MaskMaxSpan = 256;

        public const int NoneStart = -1;
        public const int NoneFinal = -2;

        #region To Service

        internal static IEnumerable<Code> ToCodes (this IEnumerable<int> ints, int offset = 0) {
            Contract.Requires<ArgumentException> (ints == null || Contract.ForAll (ints, x => (x + offset).HasCodeValue ()));
            Contract.Ensures (Contract.Result<IEnumerable<Code>> ().IsNot (null));
            if (ints.IsNot (null)) {
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
            else {
                yield break;
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
            var icsWrap = self as CodeSetWrap;
            if (!icsWrap.IsNull ()) {
                return icsWrap.ToBitSetArray ();
            }
            var ret = BitSetArray.Size (self.Last + 1);
            foreach (int code in self) {
                ret._Set (code, true);
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
            // TODO Ensures Theory
            Contract.Ensures (Contract.Result<BitSetArray> ().IsNot (null));
            Contract.Ensures (Contract.Result<BitSetArray> ().IsCompact ());

            if (self.IsNull () || self.Count == 0)
                return BitSetArray.Empty ();

            var bits = self as CodeSetPage;
            if (bits.IsNot (null))
                return bits.ToCompact ();

            var ret = BitSetArray.Size (self.Length);
            foreach (int code in self) {
                ret._Set (code - self.First, true);
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
