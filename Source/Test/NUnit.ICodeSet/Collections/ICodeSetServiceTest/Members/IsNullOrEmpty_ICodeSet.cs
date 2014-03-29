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
	public class IsNullOrEmpty_ICodeSet
	{
		[Test]
		public void IsTrue()
		{
			Assert.True (((ICodeSet)null).IsNullOrEmpty());
			Assert.True (((CodeSetBits)null).IsNullOrEmpty());
			Assert.True (((CodeSetNull)null).IsNullOrEmpty());
			Assert.True (((CodeSetPair)null).IsNullOrEmpty());
			Assert.True (((CodeSetList)null).IsNullOrEmpty());
			Assert.True (((CodeSetFull)null).IsNullOrEmpty());
			Assert.True (((CodeSetDiff)null).IsNullOrEmpty());
			Assert.True (((CodeSetPage)null).IsNullOrEmpty());
			Assert.True (((CodeSetWide)null).IsNullOrEmpty());

			Assert.True ((CodeSetNull.Singleton).IsNullOrEmpty());
			Assert.True ((new CodeSetBits()).IsNullOrEmpty());
		}

		[Test]
		public void IsFalse() {
			Assert.False ((new CodeSetBits(new Code[] {0})).IsNullOrEmpty());
			Assert.False ((new Code(9)).IsNullOrEmpty());

			Assert.False ((new CodeSetBits(new Code[] {10,11})).IsNullOrEmpty());
			Assert.False ((new CodeSetPair(10,11)).IsNullOrEmpty());
		}
	}
}
