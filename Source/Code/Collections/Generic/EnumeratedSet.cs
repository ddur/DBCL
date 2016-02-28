// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DD.Collections.Generic
{
    /// <summary>
    /// Enumerated Set
    /// <remarks>Contains and returns unique reference</remarks>
    /// </summary>
    public class EnumeratedSet<K> : HashDictionary<K, int>
    {

        #region Fields

        private int ID = 0;

        #endregion

        #region new members

        public K GetDistinct (K key) {
            Contract.Requires<ArgumentNullException> (key.IsNot (null));
            K reference = key;
            if (!Find (ref reference)) {
                Add (key);
                reference = key;
            }
            return reference;
        }

        public bool Contains (K key) {
            return base.ContainsKey (key);
        }

        public bool Add (K key) {
            Contract.Requires<ArgumentNullException> (key.IsNot (null));
            try {
                base.Add (key, ID);
                ++ID;
                return true;
            } catch {
                return false;
            }
        }

        #endregion

        #region Hide KeyValuePair methods

        public new int this[K key] {
            get {
                return base[key];
            }
        }

        // Hide KeyValuePair
        internal new bool Contains (KeyValuePair<K, int> item) {
            throw new NotSupportedException ();
        }

        internal new bool ContainsKey (K key) {
            throw new NotSupportedException ();
        }

        internal new void Add (K key, int value) {
            throw new NotSupportedException ();
        }

        internal new void Add (KeyValuePair<K, int> item) {
            throw new NotSupportedException ();
        }

        internal new bool Remove (KeyValuePair<K, int> item) {
            throw new NotSupportedException ();
        }

        #endregion
    }
}
