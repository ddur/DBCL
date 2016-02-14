// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Diagnostics.Contracts;
using DD.Diagnostics;

namespace DD.Collections.ICodeSet {

    /// <summary>
    /// Description of ICodeSetReduction.
    /// </summary>
    public static class Reduction {

        [Pure]
        private static ICodeSet ReducePartOne (this BitSetArray self, int offset) {
            Contract.Requires<IndexOutOfRangeException>
            (
                self.IsNullOrEmpty () ||
                (
                    (self.First + offset).HasCodeValue () &&
                    (self.Last + offset).HasCodeValue ()
                )
            );

            Contract.Ensures (Contract.Result<ICodeSet> ().Is (null) || Contract.Result<ICodeSet> ().IsReduced);

            #region Reduction

            #region Null

            if (self.IsNullOrEmpty ()) {
                return CodeSetNone.Singleton;
            }

            #endregion

            Contract.Assume (self.First.HasValue);
            Contract.Assume (self.Last.HasValue);

            #region Unit

            if (self.Count == Service.UnitCount) {
                return (Code)((int)self.First + offset);
            }

            #endregion

            #region Pair

            if (self.Count == Service.PairCount) {
                return CodeSetPair.From ((int)self.First + offset, (int)self.Last + offset);
            }

            #endregion

            #region Full

            if (self.Count == self.Span ()) {
                return CodeSetFull.From ((int)self.First + offset, (int)self.Last + offset);
            }

            #endregion

            #region List

            if (self.Count <= Service.ListMaxCount) {
                Contract.Assert (self.Count > Service.PairCount);

                return CodeSetList.From (self.ToCodes (offset));
            }

            #endregion

            #region Mask

            if (self.Span () <= Service.MaskMaxSpan) {
                return CodeSetMask.From (self.ToCodes (offset));
            }

            #endregion

            #endregion

            Contract.Assert (self.Span () > Service.MaskMaxSpan);
            Contract.Assert (self.Count > Service.ListMaxCount);

            return null;
        }

        [Pure]
        private static ICodeSet ReducePartTwo (this BitSetArray self, int offset) {
            Contract.Requires<ArgumentNullException> (self.IsNot (null));
            Contract.Requires<ArgumentException> (self.Count.InRange (Service.PairCount + 1, self.Span () - 1));	// not Null/Code/Pair/Full
            Contract.Requires<ArgumentException> (self.Length <= Code.MaxCount || self.Last <= Code.MaxValue);
            Contract.Requires<ArgumentException> (offset.InRange (0, Code.MaxValue - (int)self.Last));

            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> () is CodeSetMask || Contract.Result<ICodeSet> () is CodeSetWide);

            Contract.Assume (self.First.HasValue);
            Contract.Assume (self.Last.HasValue);

            if (self.Span() <= char.MaxValue) {
            	return CodeSetMask.From (self, offset);
            }
            return CodeSetWide.From (self, offset);
        }

        [Pure]
        public static ICodeSet Reduce (this ICodeSet self) {
            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().Theory ());

            if (self.IsNullOrEmpty ()) {
                return CodeSetNone.Singleton;
            }
            return self.IsReduced ? self : self.ToBitSetArray ().Reduce ();
        }

        [Pure]
        public static ICodeSet Reduce (this BitSetArray self, int offset = 0) {
            Contract.Requires<IndexOutOfRangeException>
            (
                self.IsNullOrEmpty () ||
                (
                    (self.First + offset).HasCodeValue () &&
                    (self.Last + offset).HasCodeValue ()
                )
            );

            Contract.Ensures (Contract.Result<ICodeSet> ().IsNot (null));
            Contract.Ensures (Contract.Result<ICodeSet> ().Theory ());

            var retSet = self.ReducePartOne (offset);

            if (retSet.Is (null)) {
                Contract.Assume (self.IsNot (null)); // not null
                Contract.Assume (self.Span () != self.Count); // not Full
                Contract.Assume (self.Count > Service.ListMaxCount); // not Code, not Pair, not List
                Contract.Assume (self.Span () > Service.MaskMaxSpan); // not Mask

                Contract.Assume (self.First.HasValue);
                Contract.Assume (self.Last.HasValue);

                // create complement
                var complement = BitSetArray.Copy(self).NotSpan();

                Contract.Assume (complement.Count != 0);

                var complementSet = complement.ReducePartOne (offset);
                if (complementSet.IsNot (null)) {
                    // if reduced to Code/Pair/Full/List/Mask, return DiffSet
                    retSet = CodeSetDiff.From (
                        CodeSetFull.From ((int)self.First + offset, (int)self.Last + offset),
                        complementSet);
                }
                else {
                    // not reduced, check size
                    if (complement.Span () < (self.Span () / 4)) {
                        // can save at least 3/4 of space
                        retSet = CodeSetDiff.From (
                            CodeSetFull.From ((int)self.First + offset, (int)self.Last + offset),
                            complement.ReducePartTwo (offset));
                    }
                    else {
                        // final choice Mask/Wide
                        retSet = self.ReducePartTwo (offset);
                    }
                }
            }

            return retSet;
        }

        [Pure]
        private static bool Theory (this ICodeSet self) {
            Success success = true;

            success.Assert (!self.IsNull ());

            switch (self.Count) {
                case Service.NullCount:
                    success.Assert (self is CodeSetNone);
                    break;

                case Service.UnitCount:
                    success.Assert (self is Code);
                    break;

                case Service.PairCount:
                    success.Assert (self is CodeSetPair);
                    break;

                default:
                    success.Assert (self.Count > Service.PairCount);

                    if (self.Count == self.Length) {
                        success.Assert (self is CodeSetFull);
                    }
                    else if (self is CodeSetList) {
                        success.Assert (self.Count <= Service.ListMaxCount);
                    }
                    else if (self is CodeSetMask) {
                        success.Assert (self.Length <= char.MaxValue);
                    }
                    else if (self is CodeSetDiff) {
                        success.Assert (self.Length > Service.MaskMaxSpan);
                    }
                    else {
                        success.Assert (self is CodeSetWide);
                    }
                    break;
            }
            return success;
        }
    }
}
