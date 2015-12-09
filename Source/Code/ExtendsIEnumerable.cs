// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DD {

    public static class ExtendsIEnumerable {

        /// <summary>Extends IEnumerable&lt;T&gt;.
        /// <para>Returns true if IEnumerable&lt;T&gt; is empty (no items/count==0).</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">Extended IEnumerable&lt;T&gt;</param>
        /// <returns>True if IEnumerable&lt;T&gt; is empty</returns>
        [Pure]
        public static bool IsEmpty<T> (this IEnumerable<T> self) {
            Contract.Requires<ArgumentNullException>(self != null);

            var collection = self as ICollection<T>;
            if ( !collection.IsNull () ) {
                return collection.Count == 0;
            }

            bool isEmpty = true;
            IEnumerator<T> e = self.GetEnumerator ();
            if ( e.MoveNext () ) {
                isEmpty = false;
            }
            return isEmpty;
        }

        /// <summary>Extends IEnumerable&lt;T&gt;.
        /// <para>Returns true if IEnumerable&lt;T&gt; is empty (no items/count==0).</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="me">Extended IEnumerable&lt;T&gt;</param>
        /// <returns>True if IEnumerable&lt;T&gt; is empty</returns>
        [Pure]
        public static bool IsEmpty (this IEnumerable self) {
            Contract.Requires<ArgumentNullException>(self != null);

            ICollection collection = self as ICollection;
            if ( !collection.IsNull () ) {
                return collection.Count == 0;
            }

            bool isEmpty = true;
            IEnumerator e = self.GetEnumerator ();
            if ( e.MoveNext () ) {
                isEmpty = false;
            }
            return isEmpty;
        }

        /// <summary>Extends any object implementing IEnumerable&lt;T&gt;.
        /// <para>Returns true if object is reference equals to null value or is empty IEnumerable(Of T).</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="me"></param>
        /// <returns></returns>
        [Pure]
        public static bool IsNullOrEmpty<T> (this IEnumerable<T> self) {
            if ( !self.IsNull () ) {
                return self.IsEmpty ();
            }
            return true;
        }

        /// <summary>Extends any object implementing IEnumerable&lt;T&gt;.
        /// <para>Returns true if object is reference equals to null value or is empty IEnumerable(Of T).</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="me"></param>
        /// <returns></returns>
        [Pure]
        public static bool IsNullOrEmpty (this IEnumerable self) {
            if ( !self.IsNull () ) {
                return self.IsEmpty ();
            }
            return true;
        }
    }
}
