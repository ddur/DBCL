// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Diagnostics.Contracts;

namespace DD.Collections {

    /// <summary>
    /// Description of BitSetArrayExtended.
    /// </summary>
    public static class BitSetArrayExtended
    {

        /// <summary>
        /// If NullOrEmpty return 0
        /// Else return (1+Last-First)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        [Pure]
        public static int Span (this BitSetArray self) {
            return self.IsNull() ? 0 : (self.Count == 0 ? 0 : ((int)self.Last - (int)self.First + 1));
        }

        [Pure]
        public static bool IsEmpty (this BitSetArray self) {
            Contract.Requires<ArgumentNullException> (self.IsNot (null));
            return self.Count == 0;
        }

        [Pure]
        public static bool IsNullOrEmpty (this BitSetArray self) {
            return self.IsNull () || self.Count == 0;
        }

        [Pure]
        public static bool IsFull (this BitSetArray self) {
            return !self.IsNull () && self.Count != 0 && (self.Count == self.Span ());
        }
    }
}
