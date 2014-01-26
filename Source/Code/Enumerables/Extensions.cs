// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System.Diagnostics.Contracts;

namespace DD.Enumerables {

    /// <summary>Extends Loop and Range</summary>
    public static class Extensions {
        [Pure]
        public static Loop Times (this int self) {
            return new Loop (self);
        }

        [Pure]
        public static Range To (this int start, int final) {
            return new Range (start, final);
        }

    }
}
