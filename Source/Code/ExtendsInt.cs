// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System.Diagnostics.Contracts;

namespace DD {

    public static class ExtendsInt {

        /// <summary>Extends value type int(Int32).
        /// <para>Returns true if extended value is between or equal to both values.</para>
        /// </summary>
        /// <param name="self">int</param>
        /// <param name="lowValue">int</param>
        /// <param name="highValue">int</param>
        /// <returns>bool</returns>
        [Pure]
        public static bool InRange (this int self, int lowValue, int highValue) {
            Contract.Ensures (Contract.Result<bool> () == (self >= lowValue && self <= highValue));

            return self >= lowValue && self <= highValue;
        }
        /// <summary>Extends value type long.
        /// <para>Returns true if extended value is between or equal to both values.</para>
        /// </summary>
        /// <param name="self">int</param>
        /// <param name="lowValue">int</param>
        /// <param name="highValue">int</param>
        /// <returns>bool</returns>
        [Pure]
        public static bool InRange (this long self, long lowValue, long highValue) {
            Contract.Ensures (Contract.Result<bool> () == (self >= lowValue && self <= highValue));

            return self >= lowValue && self <= highValue;
        }
    }
}
