// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetServiceTest.Members
{
	[TestFixture]
	public class IsFull_ICodeSet
	{
		[Test]
		public void Null_IsFalse()
		{
			ICodeSet isNull = null;
			Assert.False (isNull.IsFull());
		}

		[Test]
		public void Empty_IsFalse()
		{
			ICodeSet isEmpty = CodeSetNull.Singleton;
			Assert.False (isEmpty.IsFull());
		}

		[Test]
		public void NotFull_IsFalse()
		{
			ICodeSet isNotFull = new CodeSetList(1,2,3,5);
			Assert.False (isNotFull.IsFull());

			isNotFull = new CodeSetPair(0,7);
			Assert.False (isNotFull.IsFull());
		}

		[Test]
		public void Full_IsTrue()
		{
			ICodeSet isFull = new Code (9);
			Assert.True (isFull.IsFull());

			isFull = new CodeSetPair(1,2);
			Assert.True (isFull.IsFull());

			isFull = new CodeSetFull(0,10);
			Assert.True (isFull.IsFull());
		}
	}
}
