// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using DD.Collections;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetServiceTest.Members
{
	[TestFixture]
	public class Span
	{
		[Test]
		public void BitSetArray_Null()
		{
			BitSetArray isNull = null;
			Assert.AreEqual(0, isNull.Span());
		}

		[Test]
		public void BitSetArray_Empty()
		{
			Assert.AreEqual(0, BitSetArray.Empty ().Span());
			Assert.AreEqual(0, BitSetArray.Size(1).Span());
			Assert.AreEqual(0, BitSetArray.Size(1000).Span());
		}

		[Test]
		public void BitSetArray_NotEmpty()
		{
			Assert.AreEqual(1, BitSetArray.From(1).Span());
			Assert.AreEqual(10, BitSetArray.From(1,10).Span());
			Assert.AreEqual(101, BitSetArray.From(900, 1000).Span());
		}

		[Test]
		public void IEnumerableCode_Null()
		{
			Assert.AreEqual(0, ((CodeSet)null).Span());
		}

		[Test]
		public void IEnumerableCode_Empty()
		{
			Assert.AreEqual(0, CodeSetNone.Singleton.Span());
			Assert.AreEqual(0, CodeSetBits.From().Span());
			Assert.AreEqual(0, (new Code[0]).Span());
		}

		[Test]
		public void IEnumerableCode_NotEmpty()
		{
			Assert.AreEqual(10, CodeSetPair.From(1,10).Span());
			Assert.AreEqual(100, CodeSetBits.From(1,20,100).Span());
			Assert.AreEqual(1000, CodeSetList.From(901, 1000, 1111, 1900).Span());
		}

	}
}
