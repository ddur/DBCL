// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DD.Collections
{
    /// <summary>Stores ICodeSet's where (Value)Equals == SetEquals == SequenceEqual == ReferenceEquals
    /// </summary>
    public sealed class ICodeSetDictionary : IDictionary<ICodeSet, int>
    {
        /// <summary>
        /// C5.Hash(Set/Dictionary) has ability to return stored key reference
        /// </summary>
        private C5.HashDictionary<ICodeSet,int> unique = new C5.HashDictionary<ICodeSet, int>();
        private int id = 0;

        #region Ctor

        public ICodeSetDictionary() {}

        #endregion

        #region C5 Method Wrappers

        public void Add(ICodeSet key)
        {
            this.unique.Add (key, id++);
        }
        
        public bool Find(ref ICodeSet key)
        {
            int val = 0;
            return this.unique.Find (ref key, out val);
        }
        
        #endregion

        #region Interfaces

        #region IDictionary<ICodeSet, int>

        public int this [ICodeSet codes] {
            get {
                return this.unique[codes];
            }
            set {
                this.unique[codes] = value;
            }
        }
        
        public ICollection<ICodeSet> Keys {
            get {
                return (ICollection<ICodeSet>)this.unique.Keys;
            }
        }
        
        public ICollection<int> Values {
            get {
                return (ICollection<int>)this.unique.Values;
            }
        }
        
        public int Count {
            get {
                return this.unique.Count;
            }
        }
        
        public bool TryGetValue(ICodeSet key, out int value)
        {
            return this.unique.Find (ref key, out value);
        }
        
        public bool ContainsKey(ICodeSet key)
        {
            return this.unique.Contains (key);
        }
        
        public bool IsReadOnly {
            get {
                return ((ICollection<KeyValuePair<ICodeSet,int>>)this.unique).IsReadOnly;
            }
        }
        
        public void Add(ICodeSet key, int value)
        {
            this.unique.Add (key, value);
        }
        
        public bool Remove(ICodeSet key)
        {
            return this.unique.Remove (key);
        }
        
        public void Clear()
        {
            this.unique.Clear();
        }
        
        public void Add(KeyValuePair<ICodeSet, int> item)
        {
            ((ICollection<KeyValuePair<ICodeSet,int>>)this.unique).Add (item);
        }
        
        public bool Remove(KeyValuePair<ICodeSet, int> item)
        {
            return ((ICollection<KeyValuePair<ICodeSet,int>>)this.unique).Remove (item);
        }
        
        #endregion        

        #region Inherited Interface Methods

        bool ICollection<KeyValuePair<ICodeSet,int>>.Contains(KeyValuePair<ICodeSet, int> item)
        {
            return ((ICollection<KeyValuePair<ICodeSet,int>>)this.unique).Contains (item);
        }
        
        void ICollection<KeyValuePair<ICodeSet,int>>.CopyTo(KeyValuePair<ICodeSet, int>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<ICodeSet,int>>)this.unique).CopyTo (array, arrayIndex);
        }
        
        IEnumerator<KeyValuePair<ICodeSet, int>> IEnumerable<KeyValuePair<ICodeSet,int>>.GetEnumerator()
        {
            return ((ICollection<KeyValuePair<ICodeSet,int>>)this.unique).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)this.unique).GetEnumerator();
        }
        
        #endregion

        #endregion        

    }
}
