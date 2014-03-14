// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DD.Collections.ICodeSetTest
{
	[TestFixture]
	public class Dictionary
	{
		ICodeSetDictionary icsDict;

		[Test]
		public void Construct() {
			icsDict = new ICodeSetDictionary();
		}
		
		[Test]
		public void AddICodeSet() {
			icsDict = new ICodeSetDictionary();
			icsDict.Add (new CodeSetFull(0,5));
			Assert.True (icsDict[new CodeSetFull(0,5)] == 0);
			Assert.True (icsDict[new CodeSetBits(new Code[]{0,1,2,3,4,5})] == 0);
		}
		
		[Test]
		public void AddICodeSetValue() {
			icsDict = new ICodeSetDictionary();
			Assert.Throws<NotSupportedException> (
				delegate { icsDict.Add (new CodeSetFull(0,5),8);}
			);
		}

		[Test]
		public void AddKeyValuePair() {
			icsDict = new ICodeSetDictionary();
			Assert.Throws<NotSupportedException> (
				delegate { icsDict.Add (new KeyValuePair<ICodeSet, int>(new CodeSetFull(0,5),8));}
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
		public void ItemSet() {
			icsDict = new ICodeSetDictionary();
			icsDict.Add (new CodeSetFull(0,4));
			Assert.Throws<NotSupportedException> (
				delegate {
					icsDict[new CodeSetFull(0,4)] = 5;
				}
			);
		}
		
		[Test]
		public void Keys() {
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
		public void Remove() {
			icsDict = new ICodeSetDictionary();
			icsDict.Add (new CodeSetFull(0,5));
			icsDict.Add (new CodeSetFull(0,4));
			Assert.False (icsDict.Remove(new CodeSetFull(0,6)));
			Assert.False (icsDict.Remove(new CodeSetBits(new Code[]{0,1,2,3,4,5,6})));
			Assert.True (icsDict.Remove(new CodeSetBits(new Code[]{0,1,2,3,4})));
			Assert.True (icsDict.Remove(new CodeSetBits(new Code[]{0,1,2,3,4,5})));
		}
	}
}
