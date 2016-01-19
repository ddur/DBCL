/*
 * Created by SharpDevelop.
 * User: ddur
 * Date: 19.1.2016.
 * Time: 16:22
 * 
 */
using System;
using System.Diagnostics.Contracts;

namespace DD.Collections {

    /// <summary>
    /// Description of BitSetArrayExtended.
    /// </summary>
    public static class BitSetArrayExtended
    {

        [Pure]
        public static int Span (this BitSetArray self) {
            if (self.IsNull ())
                return 0;
            if (self.Count == 0)
                return 0;
            return 1 + (int)self.Last - (int)self.First;
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
