// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DD.Collections.Generic
{
    /// <summary>Stores K's where (Value)Equals == SetEquals == SequenceEqual == ReferenceEquals
    /// </summary>
    public class HashDictionary<K, T> : IDictionary<K, T> {

        #region Ctor

        public HashDictionary () {
            unique = new C5.HashDictionary<K, T> ();
        }

        #endregion

        #region Fields

        /// <summary>C5.Hash(Set/Dictionary) has ability to return stored key reference</summary>
        [CLSCompliant (false)]
        protected readonly C5.HashDictionary<K, T> unique;

        #endregion

        #region new-members (Use C5 extra functions)

        public bool Find (ref K key) {
            T val = default (T);
            return this.unique.Find (ref key, out val);
        }

        #endregion

        #region Interfaces

        #region IDictionary<K, T>

        public virtual T this[K key] {
            get {
                return this.unique[key];
            }
            set {
                this.unique[key] = value;
            }
        }

        public ICollection<K> Keys {
            get {
                return this.unique.Keys.ToList ();
            }
        }

        public ICollection<T> Values {
            get {
                return this.unique.Values.ToArray();
            }
        }

        public bool ContainsKey (K key) {
            return this.unique.Contains (key);
        }

        public virtual void Add (K key, T value) {
            try {
                this.unique.Add (key, value);
            }
            catch (C5.DuplicateNotAllowedException) {
                throw new ArgumentException ("Duplicate not allowed");
            }
        }

        public bool Remove (K key) {
            return this.unique.Remove (key);
        }

        public bool TryGetValue (K key, out T value) {
            return this.unique.Find (ref key, out value);
        }

        #endregion

        #region Inherited Interface Methods

        #region ICollection<KeyValuePair<K, T>>

        public int Count {
            get {
                return this.unique.Count;
            }
        }

        public virtual bool IsReadOnly {
            get {
                return this.unique.IsReadOnly;
            }
        }

        public virtual void Add (KeyValuePair<K, T> item) {
            try {
                this.unique.Add (item.Key, item.Value);
            }
            catch (C5.DuplicateNotAllowedException) {
                throw new ArgumentException ("Duplicate not allowed");
            }
        }

        public void Clear () {
            this.unique.Clear ();
        }

        public bool Contains (KeyValuePair<K, T> item) {
            return unique.Contains (new C5.KeyValuePair<K, T> (item.Key, item.Value));
        }

        [Pure]
        [SuppressMessage ("Microsoft.Contracts", "CC1033", Justification = "Debug/Release exceptions not same")]
        public void CopyTo (KeyValuePair<K, T>[] array, int arrayIndex) {
            Contract.Requires<ArgumentNullException> (array.IsNot (null));
            Contract.Requires<IndexOutOfRangeException> (arrayIndex >= 0);
            Contract.Requires<IndexOutOfRangeException> (arrayIndex <= (array.Length - this.Count));
            int index = arrayIndex;
            foreach (var item in unique) {
                array[index] = new KeyValuePair<K, T> (item.Key, item.Value);
                ++index;
            }
        }

        public virtual bool Remove (KeyValuePair<K, T> item) {
            var key = item.Key;
            var value = item.Value;
            if (this.unique.Find (ref key, out value)) {
                if (value.Equals (item.Value)) {
                    return this.unique.Remove (key);
                }
            }
            return false;
        }

        #endregion

        #region IEnumerable<KeyValuePair<K, T>>

        public IEnumerator<KeyValuePair<K, T>> GetEnumerator () {
            foreach (var item in unique) {
                yield return new KeyValuePair<K, T> (item.Key, item.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator () {
            return ((IEnumerable<KeyValuePair<K, T>>)this).GetEnumerator ();
        }

        #endregion

        #endregion

        #endregion
    }
}
