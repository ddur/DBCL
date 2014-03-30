// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSetServiceTest.Members
{
	[TestFixture]
	public class IsFull_BitSetArray
	{
		[Test]
		public void Null_IsFalse()
		{
			BitSetArray isNull = null;
			Assert.False (isNull.IsFull());
		}

		[Test]
		public void Empty_IsFalse()
		{
			var isEmpty = new BitSetArray();
			Assert.False (isEmpty.IsFull());
		}

		[Test]
		public void NotFull_IsFalse()
		{
			var isNotFull = new BitSetArray() {1,3};
			Assert.False (isNotFull.IsFull());

			isNotFull = new BitSetArray() {0,7};
			Assert.False (isNotFull.IsFull());
		}

		[Test]
		public void Full_IsTrue()
		{
			var isFull = new BitSetArray() {0};
			Assert.True (isFull.IsFull());

			isFull = new BitSetArray() {0,1};
			Assert.True (isFull.IsFull());

			isFull = new BitSetArray() {1,2};
			Assert.True (isFull.IsFull());

			isFull = new BitSetArray() {1,2,3};
			Assert.True (isFull.IsFull());

			isFull = new BitSetArray(10, true);
			Assert.True (isFull.IsFull());
		}
	}
}
