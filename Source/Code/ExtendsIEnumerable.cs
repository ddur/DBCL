// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
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
            Contract.Requires<ArgumentNullException> (self.IsNot (null));

            var collection = self as ICollection<T>;
            if (!collection.IsNull ()) {
                return collection.Count == 0;
            }
            return !self.GetEnumerator().MoveNext();
        }

        /// <summary>Extends IEnumerable.
        /// <para>Returns true if IEnumerable&lt;T&gt; is empty (no items/count==0).</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">Extended IEnumerable&lt;T&gt;</param>
        /// <returns>True if IEnumerable&lt;T&gt; is empty</returns>
        [Pure]
        public static bool IsEmpty (this IEnumerable self) {
            Contract.Requires<ArgumentNullException> (self.IsNot (null));

            var collection = self as ICollection;
            if (!collection.IsNull ()) {
                return collection.Count == 0;
            }
            return !self.GetEnumerator().MoveNext();
        }

        /// <summary>Extends any object implementing IEnumerable&lt;T&gt;.
        /// <para>Returns true if object is reference equals to null value or is empty IEnumerable(Of T).</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        [Pure]
        public static bool IsNullOrEmpty<T> (this IEnumerable<T> self) {
            return self.IsNull() || self.IsEmpty();
        }

        /// <summary>Extends any object implementing IEnumerable&lt;T&gt;.
        /// <para>Returns true if object is reference equals to null value or is empty IEnumerable(Of T).</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        [Pure]
        public static bool IsNullOrEmpty (this IEnumerable self) {
            return self.IsNull() || self.IsEmpty();
        }
    }
}
