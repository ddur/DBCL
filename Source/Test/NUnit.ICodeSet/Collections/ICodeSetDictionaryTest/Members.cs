// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DD.Collections.ICodeSetDictionaryTest
{
	[TestFixture]
	public class Members
	{
		ICodeSetDictionary icsDict;

		[Test]
		public void AddICodeSet() {
			icsDict = new ICodeSetDictionary();
			icsDict.Add (new CodeSetFull(0,5));
			icsDict.Add (new CodeSetList(new Code[] {0,2,4,6,8,10}));
			Assert.True (icsDict[new CodeSetFull(0,5)] == 0);
			Assert.True (icsDict[new CodeSetBits(new Code[]{0,1,2,3,4,5})] == 0);
			Assert.True (icsDict[new CodeSetList(new Code[]{0,2,4,6,8,10})] == 1);
			Assert.True (icsDict[new CodeSetPage(new Code[]{0,2,4,6,8,10})] == 1);
			
			// add duplicate
			Assert.Throws<ArgumentException> (
				delegate {
					icsDict.Add (new CodeSetFull(0,5));
				}
			);
			Assert.Throws<ArgumentException> (
				delegate {
					icsDict.Add (new CodeSetPage(new Code[]{0,2,4,6,8,10}));
				}
			);
		}
		
		[Test]
		public void AddICodeSetAndValue_NotSupported() {
			icsDict = new ICodeSetDictionary();
			var icsIDictAsIDictionary = icsDict as IDictionary<ICodeSet, int>;
			Assert.NotNull(icsIDictAsIDictionary);
			Assert.Throws<NotSupportedException> (
				delegate { icsIDictAsIDictionary.Add (new CodeSetFull(0,5),8);}
			);
		}

		[Test]
		public void AddKeyValuePair_NotSupported() {
			icsDict = new ICodeSetDictionary();
			var icsDictAsKvpCollection = (ICollection<KeyValuePair<ICodeSet,int>>)icsDict;
			Assert.NotNull(icsDictAsKvpCollection);
			Assert.Throws<NotSupportedException> (
				delegate { icsDictAsKvpCollection.Add (new KeyValuePair<ICodeSet, int>(new CodeSetFull(0,5),8));}
			);
		}
		
		[Test]
		public void Clear() {
			icsDict = new ICodeSetDictionary();
			Assert.True (icsDict.Count == 0);
			icsDict.Add (new CodeSetFull(0,5));
			Assert.True (icsDict.Count == 1);
			icsDict.Add (new CodeSetFull(0,4));
			Assert.True (icsDict.Count == 2);
			icsDict.Clear();
			Assert.True (icsDict.Count == 0);
		}
		
		[Test]
		public void ContainsKey() {
			icsDict = new ICodeSetDictionary();
			icsDict.Add (new CodeSetFull(0,5));
			Assert.True (icsDict.ContainsKey(new CodeSetFull(0,5)));
			Assert.True (icsDict.ContainsKey(new CodeSetBits(new Code[]{0,1,2,3,4,5})));
		}

		[Test]
		public void Count() {
			icsDict = new ICodeSetDictionary();
			Assert.True (icsDict.Count == 0);
			icsDict.Add (new CodeSetFull(0,5));
			Assert.True (icsDict.Count == 1);
			icsDict.Add (new CodeSetFull(0,4));
			Assert.True (icsDict.Count == 2);
			icsDict.Remove(new CodeSetFull(0,5));
			Assert.True (icsDict.Count == 1);
			icsDict.Clear();
			Assert.True (icsDict.Count == 0);
		}

		[Test]
		public void Find() {
			icsDict = new ICodeSetDictionary();
			icsDict.Add (new CodeSetFull(0,5));
			icsDict.Add (new CodeSetFull(0,4));
			ICodeSet findSet = new CodeSetFull(0,5);
			Assert.True (icsDict.Find(ref findSet));
			findSet = new CodeSetBits(new Code[]{0,1,2,3,4,5});
			Assert.True (icsDict.Find(ref findSet));
		}

		[Test]
		public void IsReadOnly() {
			icsDict = new ICodeSetDictionary();
			Assert.False (icsDict.IsReadOnly);
		}
		
		[Test]
		public void ItemGet() {
			icsDict = new ICodeSetDictionary();
			icsDict.Add (new CodeSetFull(0,4));
			icsDict.Add (new CodeSetFull(0,5));
			Assert.True (icsDict[new CodeSetFull(0,4)] == 0);
			Assert.True (icsDict[new CodeSetFull(0,5)] == 1);
			Assert.True (icsDict[new CodeSetBits(new Code[]{0,1,2,3,4})] == 0);
			Assert.True (icsDict[new CodeSetBits(new Code[]{0,1,2,3,4,5})] == 1);
		}
		
		[Test]
		public void ItemSet_NotSupported() {
			icsDict = new ICodeSetDictionary();
			icsDict.Add (new CodeSetFull(0,4));
			Assert.Throws<NotSupportedException> (
				delegate {
					icsDict[new CodeSetFull(0,4)] = 5;
				}
			);
		}
		
		[Test]
		public void KeysGet() {
			icsDict = new ICodeSetDictionary();
			ICodeSet set1 = new CodeSetFull(0,4); 
			ICodeSet set2 = new CodeSetFull(0,5); 
			icsDict.Add (set1);
			icsDict.Add (set2);
			ICollection<ICodeSet> keys = icsDict.Keys;
			Assert.True (keys.Count == 2);
			Assert.True (keys.Contains(set1));
			Assert.True (keys.Contains(set2));
		}
		
		[Test]
		public void RemoveICodeSet() {
			icsDict = new ICodeSetDictionary();
			icsDict.Add (new CodeSetFull(0,5));
			icsDict.Add (new CodeSetFull(0,4));
			Assert.False (icsDict.Remove(new CodeSetFull(0,6)));
			Assert.False (icsDict.Remove(new CodeSetBits(new Code[]{0,1,2,3,4,5,6})));
			Assert.True (icsDict.Remove(new CodeSetBits(new Code[]{0,1,2,3,4})));
			Assert.True (icsDict.Remove(new CodeSetBits(new Code[]{0,1,2,3,4,5})));
		}
		
		[Test]
		public void RemoveKeyValuePair_NotSupported() {
			icsDict = new ICodeSetDictionary();
			icsDict.Add (new CodeSetFull(0,5));
			var kvp = new KeyValuePair<ICodeSet, int>(new CodeSetFull(0,5),0);
			Assert.Throws<NotSupportedException> (
				delegate {
					((ICollection<KeyValuePair<ICodeSet,int>>)icsDict).Remove(kvp);
				}
			);
		}
		
		[Test]
		public void Contains_NotSupported() {
			icsDict = new ICodeSetDictionary();
			icsDict.Add (new CodeSetFull(0,5));
			var icsDictAsKvpCollection = (ICollection<KeyValuePair<ICodeSet,int>>)icsDict;
			Assert.NotNull(icsDictAsKvpCollection);
			var kvp = new KeyValuePair<ICodeSet, int>(new CodeSetFull(0,5),0);
			Assert.Throws<NotSupportedException> (
				delegate {
					icsDictAsKvpCollection.Contains(kvp);
				}
			);
		}
		
		[Test]
		public void CopyTo_NotSupported() {
			icsDict = new ICodeSetDictionary();
			icsDict.Add (new CodeSetFull(0,5));
			var icsDictAsKvpCollection = (ICollection<KeyValuePair<ICodeSet,int>>)icsDict;
			Assert.NotNull(icsDictAsKvpCollection);
			KeyValuePair<ICodeSet,int>[] array;
			array = new KeyValuePair<ICodeSet, int>[1];
			Assert.Throws<NotSupportedException> (
				delegate {
					icsDictAsKvpCollection.CopyTo(array,0);
				}
			);
		}
		
		[Test]
		public void GetEnumeratorKeyValuePair_NotSupported () {
			icsDict = new ICodeSetDictionary();
			icsDict.Add (new CodeSetFull(0,5));
			var icsDictAsKvpCollection = (ICollection<KeyValuePair<ICodeSet,int>>)icsDict;
			Assert.NotNull(icsDictAsKvpCollection);
			Assert.Throws<NotSupportedException> (
				delegate {
					icsDictAsKvpCollection.GetEnumerator();
				}
			);
		}

		[Test]
		public void GetEnumerator_NotSupported () {
			icsDict = new ICodeSetDictionary();
			icsDict.Add (new CodeSetFull(0,5));
			var icsDictAsIEnumerable = (IEnumerable)icsDict;
			Assert.NotNull(icsDictAsIEnumerable);
			Assert.Throws<NotSupportedException> (
				delegate {
					icsDictAsIEnumerable.GetEnumerator();
				}
			);
		}

		[Test]
		public void TryGetValue() {
			icsDict = new ICodeSetDictionary();
			icsDict.Add (new CodeSetFull(0,5));
			icsDict.Add (new CodeSetFull(0,4));
			int value = int.MinValue;
			Assert.True (
				icsDict.TryGetValue(new CodeSetFull(0,4), out value)
			);
			Assert.True (value == 1);

			Assert.True (
				icsDict.TryGetValue(new CodeSetBits(new Code[] {0,1,2,3,4,5}), out value)
			);
			Assert.True (value == 0);

			Assert.True (
				icsDict.TryGetValue(new CodeSetBits(new Code[] {0,1,2,3,4}), out value)
			);
			Assert.True (value == 1);

			Assert.True (
				icsDict.TryGetValue(new CodeSetFull(0,5), out value)
			);
			Assert.True (value == 0);
			
			Assert.False (
				icsDict.TryGetValue(new CodeSetFull(0,6), out value)
			);
		}

		[Test]
		public void ValuesGet() {
			icsDict = new ICodeSetDictionary();
			icsDict.Add (new CodeSetFull(0,5));
			icsDict.Add (new CodeSetFull(0,4));
			icsDict.Add (new CodeSetFull(0,6));
			Assert.True (icsDict.Values.Distinct().Count() == 3);
			Assert.True (icsDict.Values.Contains(0));
			Assert.True (icsDict.Values.Contains(1));
			Assert.True (icsDict.Values.Contains(2));
		}
	}
}
