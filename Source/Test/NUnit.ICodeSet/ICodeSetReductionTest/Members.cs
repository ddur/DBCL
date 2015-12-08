// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetReductionTest
{
	[TestFixture]
	public class Members
	{
		[Test]
		public void IsReduced() {
			Assert.True(new Code(3).IsReduced());
			Assert.True(CodeSetNone.Singleton.IsReduced());
			Assert.True(CodeSetPair.From(3,4).IsReduced());
			Assert.True(CodeSetFull.From(3,44).IsReduced());
			Assert.True(CodeSetList.From(3,7,9,15,80).IsReduced());
			Assert.True(CodeSetPage.From(3,7,9,15,80).IsReduced());
			Assert.True(CodeSetWide.From(new Code[]{3,7,9,15,80,70000}).IsReduced());
			Assert.True(CodeSetDiff.From(CodeSetFull.From(1,80000),CodeSetWide.From(new Code[]{3,7,9,15,80,70000})).IsReduced());

			Assert.False(CodeSetBits.From(3,7,9,15,80).IsReduced());
			Assert.False(CodeSetWrap.From(BitSetArray.From(3,7,9,15,80)).IsReduced());
		}

		[Test, TestCaseSource("Expected")]
		public void Reduce_ICodeSet(Tuple<BitSetArray, Type> tuple)
		{
			ICodeSet input = null;
			if (tuple.Item1.IsNot(null)) {
				input = CodeSetWrap.From(tuple.Item1);
			}
			Type type = tuple.Item2;
			ICodeSet result = input.Reduce();
			Assert.NotNull(result);
			Assert.True(result.IsReduced());
			Assert.AreSame(type, result.GetType());
			if (input.IsNot(null)) {
				Assert.That (input.SequenceEqual(result));
			}
			result = result.Reduce(); // test already reduced
			Assert.NotNull(result);
		}

		[Test, TestCaseSource("Expected")]
		public void Reduce_BitSetArray(Tuple<BitSetArray, Type> tuple)
		{
			BitSetArray input = tuple.Item1;
			Type type = tuple.Item2;
			ICodeSet result = input.Reduce();
			Assert.NotNull(result);
			Assert.True(result.IsReduced());
			Assert.AreSame(type, result.GetType());
			if (input.IsNot(null)) {
				Assert.That (input.SequenceEqual(result.ToValues()));
			}
		}

		IEnumerable<Tuple<BitSetArray,Type>> ToNull {
			get {
				// null -> Null
				yield return new Tuple<BitSetArray, Type>((BitSetArray)null, typeof(CodeSetNone));

				// empty -> Null
				yield return new Tuple<BitSetArray, Type>(BitSetArray.Empty (), typeof(CodeSetNone));
				yield return new Tuple<BitSetArray, Type>(BitSetArray.Size(100), typeof(CodeSetNone));
			}
		}

		IEnumerable<Tuple<BitSetArray,Type>> ToCode {
			get {
				// one -> Code
				yield return new Tuple<BitSetArray, Type>(BitSetArray.From(0), typeof(Code));
				yield return new Tuple<BitSetArray, Type>(BitSetArray.From(66), typeof(Code));
			}
		}

		IEnumerable<Tuple<BitSetArray,Type>> ToPair {
			get {
				// pair -> Pair
				yield return new Tuple<BitSetArray, Type>(BitSetArray.From(0, 99), typeof(CodeSetPair));
				yield return new Tuple<BitSetArray, Type>(BitSetArray.From(66, 250), typeof(CodeSetPair));
				yield return new Tuple<BitSetArray, Type>(BitSetArray.From(66, 1000000), typeof(CodeSetPair));
			}
		}

		IEnumerable<Tuple<BitSetArray,Type>> ToFull {
			get {
				// range -> Full
				yield return new Tuple<BitSetArray, Type>(BitSetArray.Size(3, true), typeof(CodeSetFull));
				yield return new Tuple<BitSetArray, Type>(BitSetArray.Size(80, true), typeof(CodeSetFull));
				yield return new Tuple<BitSetArray, Type>(BitSetArray.Size(8000, true), typeof(CodeSetFull));
				yield return new Tuple<BitSetArray, Type>(BitSetArray.From(66, 67, 68), typeof(CodeSetFull));
				yield return new Tuple<BitSetArray, Type>(BitSetArray.From(Enumerable.Range(66, 75)), typeof(CodeSetFull));
				yield return new Tuple<BitSetArray, Type>(BitSetArray.From(Enumerable.Range(66000, 75000)), typeof(CodeSetFull));
			}
		}

		IEnumerable<Tuple<BitSetArray,Type>> ToList {
			get {
				// list -> List takes less space than 1/4 Page space
				yield return new Tuple<BitSetArray, Type>(BitSetArray.From(66, 250, 700), typeof(CodeSetList));
				yield return new Tuple<BitSetArray, Type>(BitSetArray.From(66, 250, 7000), typeof(CodeSetList));

				// list -> List wider than Page
				yield return new Tuple<BitSetArray, Type>(BitSetArray.From(66, 250, 70000), typeof(CodeSetList));
				yield return new Tuple<BitSetArray, Type>(BitSetArray.From(0, 1, 2, 3, 4, 9, 10, 12, 13, 16, 17, 18, 19, 20, 21, 70000), typeof(CodeSetList));
			}
		}

		IEnumerable<Tuple<BitSetArray,Type>> ToPage {
			get {
				// bits -> Page if length <= NoDiffLength
				yield return new Tuple<BitSetArray, Type>(BitSetArray.From(0, 99, 126), typeof(CodeSetPage));
				yield return new Tuple<BitSetArray, Type>(BitSetArray.From(66, 250, 254), typeof(CodeSetPage));
				yield return new Tuple<BitSetArray, Type>(BitSetArray.From(0, 1, 2, 3, 4, 9, 10, 12, 13, 16, 17, 18, 19, 20, 21, 22), typeof(CodeSetPage));

				// bits -> Diff attempt failed -> Page 
				yield return new Tuple<BitSetArray, Type>(BitSetArray.From(0, 1, 2, 3, 4, 9, 10, 12, 13, 16, 17, 18, 19, 20, 21, 700), typeof(CodeSetPage));
			}
		}

		IEnumerable<Tuple<BitSetArray,Type>> ToDiff {
			get {
				// bits -> Diff/Code
				BitSetArray bits = BitSetArray.From (Enumerable.Range(66000,5000));
				bits.Set (67000, false); // one not member
				yield return new Tuple<BitSetArray, Type>(bits, typeof(CodeSetDiff));

				// bits -> Diff/Pair
				bits.Set (67900, false); // two not members
				yield return new Tuple<BitSetArray, Type>(bits, typeof(CodeSetDiff));

				// bits -> Diff/List
				bits.Set (67100, false); // three not members (List)
				yield return new Tuple<BitSetArray, Type>(bits, typeof(CodeSetDiff));
				bits.Set (67500, false); // four not members (List)
				yield return new Tuple<BitSetArray, Type>(bits, typeof(CodeSetDiff));

				// bits -> Diff/Full
				foreach (var item in Enumerable.Range(67000,1000)) {
					bits.Set (item, false); // range of not members (Full)
				}
				yield return new Tuple<BitSetArray, Type>(bits, typeof(CodeSetDiff));

				// bits -> Diff/Page
				bits = BitSetArray.From (Enumerable.Range(60000,10000)); // between pages
				var r = new Random();
				for (var i = 0; i < 50; i++) {
					bits.Set(r.Next(65300, 65520), false);  
				}
				yield return new Tuple<BitSetArray, Type>(bits, typeof(CodeSetDiff));

				// bits -> Diff/Wide
				for (var i = 0; i < 50; i++) {
					bits.Set(r.Next(65530, 65700), false);  
				}
				yield return new Tuple<BitSetArray, Type>(bits, typeof(CodeSetDiff));
			}
		}

		IEnumerable<Tuple<BitSetArray,Type>> ToWide {
			get {
				// bits -> Wide if length <= NoDiffLength
				var bits = BitSetArray.Size (100000);
				var r = new Random();
				for (var i = 0; i < 100; i++) {
					bits.Set(r.Next(65530, 65700));  
				}
				yield return new Tuple<BitSetArray, Type>(bits, typeof(CodeSetWide));

				// bits -> Diff attempt failed -> Wide
				for (var i = 0; i < 500; i++) {
					bits.Set(r.Next(70000, 90000));  
				}
				yield return new Tuple<BitSetArray, Type>(bits, typeof(CodeSetWide));
			}
		}

		IEnumerable<Tuple<BitSetArray,Type>> Expected {
			get {
				foreach (var item in ToNull)
					yield return item;
				foreach (var item in ToCode)
					yield return item;
				foreach (var item in ToPair)
					yield return item;
				foreach (var item in ToFull)
					yield return item;
				foreach (var item in ToList)
					yield return item;
				foreach (var item in ToPage)
					yield return item;
				foreach (var item in ToDiff)
					yield return item;
				foreach (var item in ToWide)
					yield return item;
			}
		}
	}
}
