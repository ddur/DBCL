// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DD.Collections.ICodeSet
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
			try {
				this.unique.Add (key, id++);
			}
			catch (C5.DuplicateNotAllowedException) {
				throw new ArgumentException("Duplicate not allowed");
			}
		}
		
		public bool Find(ref ICodeSet key)
		{
			int val = 0;
			return this.unique.Find (ref key, out val);
		}
		
		#endregion

		#region Interfaces

		#region IDictionary<ICodeSet, int>

		public int this [ICodeSet codeSet] {
			get {
				return this.unique[codeSet];
			}
			set {
				throw new NotSupportedException("Value is auto-numbered");
			}
		}
		
		public ICollection<ICodeSet> Keys {
			get {
				return this.unique.Keys.ToList();
			}
		}
		
		public ICollection<int> Values {
			get {
				return this.unique.Values.ToList();
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
				return this.unique.IsReadOnly;
			}
		}
		
		void IDictionary<ICodeSet, int>.Add(ICodeSet key, int value)
		{
			throw new NotSupportedException("Value is auto-numbered");
		}
		
		public bool Remove(ICodeSet key)
		{
			return this.unique.Remove (key);
		}
		
		public void Clear()
		{
			this.unique.Clear();
		}
		
		void ICollection<KeyValuePair<ICodeSet, int>>.Add(KeyValuePair<ICodeSet, int> item)
		{
			throw new NotSupportedException("Value is auto-numbered");
		}
		
		bool ICollection<KeyValuePair<ICodeSet, int>>.Remove(KeyValuePair<ICodeSet, int> item)
		{
			throw new NotSupportedException();
		}
		
		#endregion

		#region Inherited Interface Methods

		public bool Contains(KeyValuePair<ICodeSet, int> item)
		{
			return unique.Contains(new C5.KeyValuePair<ICodeSet,int>(item.Key,item.Value));
		}
		
		[SuppressMessage("Microsoft.Contracts", "CC1033", Justification = "Not same exceptions")]
		public void CopyTo(KeyValuePair<ICodeSet, int>[] array, int arrayIndex)
		{
			Contract.Requires<ArgumentNullException>(!array.Is(null));
			Contract.Requires<ArgumentOutOfRangeException>(arrayIndex >= 0);
			Contract.Requires<ArgumentOutOfRangeException>(arrayIndex <= (array.Length - this.Count));
			int index = arrayIndex;
			foreach (var item in unique) {
				array[index] = new KeyValuePair<ICodeSet, int> (item.Key, item.Value);
 				++index;
			}
		}
		
		public IEnumerator<KeyValuePair<ICodeSet, int>> GetEnumerator()
		{
			foreach (var item in unique) {
				yield return new KeyValuePair<ICodeSet, int> (item.Key, item.Value);
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<ICodeSet,int>>)this).GetEnumerator();
		}
		
		#endregion

		#endregion

	}
}
