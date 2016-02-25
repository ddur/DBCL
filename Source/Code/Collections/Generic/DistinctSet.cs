// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Diagnostics.Contracts;

namespace DD.Collections.Generic
{
    /// <summary>
    /// Distinct T
    /// </summary>
    [CLSCompliant (false)]
    public class DistinctSet<T> : C5.HashSet<T>
    {

        public bool this [T item] {
            get {
                return Contains (item);
            }
        }

        /// <summary>
        /// If equal item exists then return that distinct set member, else add item and return it.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public T Distinct (T item) {
            Contract.Requires <ArgumentNullException> (!ReferenceEquals (item, null));

            T key = item;
            FindOrAdd (ref key);
            return key;
        }
    }
}
