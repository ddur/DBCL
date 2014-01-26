// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System.Diagnostics.Contracts;

namespace DD {

    /// <summary>Object Extensions</summary>
    public static class ExtendsObject {

        /// <summary>Shortcut for "object.ReferenceEquals(this, that)".
        /// <para>Extends all object instances (and null reference).</para>
        /// <para>If '==' operator is overloaded, expression 'me == that' can possibly return other result than 'object.ReferenceEquals( me, that )'.</para>
        /// </summary>
        /// <param name="me"></param>
        /// <param name="that"></param>
        /// <returns>bool object.ReferenceEquals(this, that)</returns>
        [Pure]
        public static bool Is (this object self, object that) {
            Contract.Ensures (Contract.Result<System.Boolean> () == object.ReferenceEquals (self, that));

            return object.ReferenceEquals (self, that);
        }

        /// <summary>Shortcut for "!object.ReferenceEquals(this, that)".
        /// <para>Extends all object instances (and null reference).</para>
        /// <para>If '!=' operator is overloaded, expression 'me != that' can possibly return other result than 'object.ReferenceEquals( me, that )'.</para>
        /// </summary>
        /// <param name="me"></param>
        /// <param name="that"></param>
        /// <returns>bool object.ReferenceEquals(this, that)</returns>
        [Pure]
        public static bool IsNot (this object self, object that) {
            Contract.Ensures (Contract.Result<System.Boolean> () == !object.ReferenceEquals (self, that));

            return !self.Is (that);
        }

        /// <summary>Shortcut for "object.ReferenceEquals(this, null)".
        /// <para>Extends all object instances (and null reference).</para>
        /// <para>If '==' operator is overloaded, expression 'self == that' can possibly return other result than 'object.ReferenceEquals( me, that )'.</para>
        /// </summary>
        /// <param name="me"></param>
        /// <returns>bool object.ReferenceEquals(this, null)</returns>
        [Pure]
        public static bool IsNull (this object self) {
            Contract.Ensures (Contract.Result<System.Boolean> () == object.ReferenceEquals (self, null));

            return self.Is (null);
        }

    }
}
