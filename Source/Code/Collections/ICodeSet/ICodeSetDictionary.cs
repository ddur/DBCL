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

namespace DD.Collections.ICodeSet {

    /// <summary>Stores ICodeSet's where (Value)Equals == SetEquals == SequenceEqual == ReferenceEquals
    /// </summary>
    public class Dictionary<T> : IDictionary<ICodeSet, T> {

        #region Ctor

        public Dictionary () {
            unique = new C5.HashDictionary<ICodeSet, T> ();
        }

        #endregion

        #region Fields

        /// <summary>C5.Hash(Set/Dictionary) has ability to return stored key reference</summary>
        private readonly C5.HashDictionary<ICodeSet, T> unique;

        #endregion

        #region C5 Find Wrapper

        public bool Find (ref ICodeSet key) {
            T val = default (T);
            return this.unique.Find (ref key, out val);
        }

        #endregion

        #region Interfaces

        #region IDictionary<ICodeSet, T>

        public virtual T this[ICodeSet codeSet] {
            get {
                return this.unique[codeSet];
            }
            set {
                this.unique[codeSet] = value;
            }
        }

        public ICollection<ICodeSet> Keys {
            get {
                return this.unique.Keys.ToList ();
            }
        }

        public ICollection<T> Values {
            get {
                return this.unique.Values.ToList ();
            }
        }

        public bool ContainsKey (ICodeSet key) {
            return this.unique.Contains (key);
        }

        public virtual void Add (ICodeSet key, T value) {
            try {
                this.unique.Add (key, value);
            }
            catch (C5.DuplicateNotAllowedException) {
                throw new ArgumentException ("Duplicate not allowed");
            }
        }

        public bool Remove (ICodeSet key) {
            return this.unique.Remove (key);
        }

        public bool TryGetValue (ICodeSet key, out T value) {
            return this.unique.Find (ref key, out value);
        }

        #endregion

        #region Inherited Interface Methods

        #region ICollection<KeyValuePair<ICodeSet, T>>

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

        public virtual void Add (KeyValuePair<ICodeSet, T> item) {
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

        public bool Contains (KeyValuePair<ICodeSet, T> item) {
            return unique.Contains (new C5.KeyValuePair<ICodeSet, T> (item.Key, item.Value));
        }

        [Pure]
        [SuppressMessage ("Microsoft.Contracts", "CC1033", Justification = "Debug/Release exceptions not same")]
        public void CopyTo (KeyValuePair<ICodeSet, T>[] array, int arrayIndex) {
            Contract.Requires<ArgumentNullException> (array.IsNot (null));
            Contract.Requires<IndexOutOfRangeException> (arrayIndex >= 0);
            Contract.Requires<IndexOutOfRangeException> (arrayIndex <= (array.Length - this.Count));
            int index = arrayIndex;
            foreach (var item in unique) {
                array[index] = new KeyValuePair<ICodeSet, T> (item.Key, item.Value);
                ++index;
            }
        }

        public virtual bool Remove (KeyValuePair<ICodeSet, T> item) {
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

        #region IEnumerable<KeyValuePair<ICodeSet, T>>

        public IEnumerator<KeyValuePair<ICodeSet, T>> GetEnumerator () {
            foreach (var item in unique) {
                yield return new KeyValuePair<ICodeSet, T> (item.Key, item.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator () {
            return ((IEnumerable<KeyValuePair<ICodeSet, T>>)this).GetEnumerator ();
        }

        #endregion

        #endregion

        #endregion
    }

    public sealed class ICodeSetDictionary : Dictionary<int> {

        #region Fields

        private int ID = 0;

        #endregion

        #region new members

        public void Add (ICodeSet iset) {
            Contract.Requires (iset.IsNot (null));
            base.Add (iset, ID);
            ++ID;
        }

        #endregion

        #region Overrides

        public override int this[ICodeSet codeSet] {
            get {
                return base[codeSet];
            }
            set {
                throw new NotSupportedException ();
            }
        }

        public override void Add (ICodeSet key, int value) {
            throw new NotSupportedException ();
        }

        public override void Add (KeyValuePair<ICodeSet, int> item) {
            Contract.Requires (!item.IsNull ());
            throw new NotSupportedException ();
        }

        public override bool Remove (KeyValuePair<ICodeSet, int> item) {
            throw new NotSupportedException ();
        }

        #endregion
    }
}
