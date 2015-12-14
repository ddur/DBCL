// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Diagnostics.Contracts;
using DD.Diagnostics;

namespace DD.Collections.ICodeSet {

    /// <summary>
    /// Description of ICodeSetReduction.
    /// </summary>
    public static class ICodeSetReduction {

        [Pure]
        private static ICodeSet ReducePartOne ( this BitSetArray self, int offset ) {
            Contract.Requires<IndexOutOfRangeException>
            (
                self.IsNullOrEmpty () ||
                (
                    (self.First + offset).HasCodeValue () &&
                    (self.Last + offset).HasCodeValue ()
                )
            );

            Contract.Ensures ( Contract.Result<ICodeSet> ().Is ( null ) || !(Contract.Result<ICodeSet> () is CodeSetBits) );

            #region Reduction

            #region Null

            if (self.IsNullOrEmpty ()) {
                return CodeSetNone.Singleton;
            }

            #endregion

            Contract.Assume ( self.First.HasValue );
            Contract.Assume ( self.Last.HasValue );

            #region Unit

            if (self.Count == ICodeSetService.UnitCount) {
                return (Code)((int)self.First + offset);
            }

            #endregion

            #region Pair

            if (self.Count == ICodeSetService.PairCount) {
                return CodeSetPair.From ( (int)self.First + offset, (int)self.Last + offset );
            }

            #endregion

            #region Full

            if (self.Count == self.Span ()) {
                return CodeSetFull.From ( (int)self.First + offset, (int)self.Last + offset );
            }

            #endregion

            #region List

            if (self.Count <= ICodeSetService.ListMaxCount) {
                Contract.Assert ( self.Count > ICodeSetService.PairCount );

                // only if spans wider than ICodeSetService.MaskMaxSpan?
                // if (self.Span() > ICodeSetService.MaskMaxSpan) {
                return CodeSetList.From ( self.ToCodes ( offset ) );
                //}
            }

            #endregion

            #region Mask

            if (self.Span () <= ICodeSetService.MaskMaxSpan) {
                return CodeSetMask.From ( self.ToCodes ( offset ) );
            }

            #endregion

            #endregion

            Contract.Assert ( self.Span () > ICodeSetService.MaskMaxSpan );
            Contract.Assert ( self.Count > ICodeSetService.ListMaxCount );

            return null;
        }

        [Pure]
        private static ICodeSet ReducePartTwo ( this BitSetArray self, int offset ) {
            Contract.Requires<ArgumentNullException> ( self.IsNot ( null ) );
            Contract.Requires<InvalidOperationException> ( self.Count.InRange ( ICodeSetService.PairCount + 1, self.Span () - 1 ) );	// not Null/Code/Pair/Full
            Contract.Requires<IndexOutOfRangeException> ( self.Length <= Code.MaxCount || self.Last <= Code.MaxValue );
            Contract.Requires<IndexOutOfRangeException> ( offset.InRange ( 0, Code.MaxValue - (int)self.Last ) );

            Contract.Ensures ( Contract.Result<ICodeSet> ().IsNot ( null ) );
            Contract.Ensures ( Contract.Result<ICodeSet> () is CodeSetPage || Contract.Result<ICodeSet> () is CodeSetWide );

            Contract.Assume ( self.First.HasValue );
            Contract.Assume ( self.Last.HasValue );

            Code start = (int)self.First + offset;
            Code final = (int)self.Last + offset;
            if (start.UnicodePlane () == final.UnicodePlane ()) {
                return CodeSetPage.From ( self, offset );
            }
            return CodeSetWide.From ( self, offset );
        }

        [Pure]
        public static ICodeSet Reduce ( this ICodeSet self ) {
            Contract.Ensures ( Contract.Result<ICodeSet> ().IsNot ( null ) );
            Contract.Ensures ( Contract.Result<ICodeSet> ().Theory () );

            if (self.IsNullOrEmpty ()) {
                return CodeSetNone.Singleton;
            }
            return self.IsReduced () ? self : self.ToBitSetArray ().Reduce ();
        }

        [Pure]
        public static bool IsReduced ( this ICodeSet self ) {
            return (
                self is Code ||
                self is CodeSetNone ||
                self is CodeSetPair ||
                self is CodeSetFull ||
                self is CodeSetList ||
                self is CodeSetMask ||
                self is CodeSetDiff ||
                self is CodeSetPage ||
                self is CodeSetWide
            );
        }

        [Pure]
        public static ICodeSet Reduce ( this BitSetArray self, int offset = 0 ) {
            Contract.Requires<IndexOutOfRangeException>
            (
                self.IsNullOrEmpty () ||
                (
                    (self.First + offset).HasCodeValue () &&
                    (self.Last + offset).HasCodeValue ()
                )
            );

            Contract.Ensures ( Contract.Result<ICodeSet> ().IsNot ( null ) );
            Contract.Ensures ( Contract.Result<ICodeSet> ().Theory () );

            var retSet = self.ReducePartOne ( offset );

            if (retSet.Is ( null )) {
                Contract.Assume ( self.IsNot ( null ) ); // not null
                Contract.Assume ( self.Span () != self.Count ); // not Full
                Contract.Assume ( self.Count > ICodeSetService.ListMaxCount ); // not Code, not Pair, not List
                Contract.Assume ( self.Span () > ICodeSetService.MaskMaxSpan ); // not Mask

                Contract.Assume ( self.First.HasValue );
                Contract.Assume ( self.Last.HasValue );

                // create complement
                int start = (int)self.First;
                int final = (int)self.Last;
                var complement = BitSetArray.Size ( self.Length );
                foreach (var item in self.Complement ()) {
                    if (item.InRange ( start, final )) {
                        complement._Set ( item );
                    }
                }

                Contract.Assume ( complement.Count != 0 );

                var notSet = complement.ReducePartOne ( offset );
                if (notSet.IsNot ( null )) {
                    // if reduced to Code/Pair/Full/List/Mask, return DiffSet
                    retSet = CodeSetDiff.From (
                        CodeSetFull.From ( (int)self.First + offset, (int)self.Last + offset ),
                        notSet );
                }
                else {
                    // not reduced, check size
                    if (complement.Span () < (self.Span () / 4)) {
                        // can save at least 3/4 of space
                        retSet = CodeSetDiff.From (
                            CodeSetFull.From ( (int)self.First + offset, (int)self.Last + offset ),
                            complement.ReducePartTwo ( offset ) );
                    }
                    else {
                        // final choice Page/Wide
                        retSet = self.ReducePartTwo ( offset );
                    }
                }
            }

            return retSet;
        }

        [Pure]
        private static bool Theory ( this ICodeSet self ) {
            Success success = true;

            success.Assert ( !self.IsNull () );

            switch (self.Count) {
                case ICodeSetService.NullCount:
                    success.Assert ( self is CodeSetNone );
                    break;

                case ICodeSetService.UnitCount:
                    success.Assert ( self is Code );
                    break;

                case ICodeSetService.PairCount:
                    success.Assert ( self is CodeSetPair );
                    break;

                default:
                    success.Assert ( self.Count > ICodeSetService.PairCount );

                    if (self.Count == self.Length) {
                        success.Assert ( self is CodeSetFull );
                    }
                    else if (self is CodeSetList) {
                        success.Assert ( self.Count <= ICodeSetService.ListMaxCount );
                    }
                    else if (self is CodeSetMask) {
                        success.Assert ( self.Span () <= ICodeSetService.MaskMaxSpan );
                    }
                    else if (self is CodeSetDiff) {
                        success.Assert ( self.Length > ICodeSetService.MaskMaxSpan );
                    }
                    else {
                        success.Assert ( self is CodeSetPage || self is CodeSetWide );
                    }
                    break;
            }
            return success;
        }
    }
}