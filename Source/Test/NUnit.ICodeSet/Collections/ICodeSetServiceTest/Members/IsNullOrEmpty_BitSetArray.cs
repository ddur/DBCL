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
	public class IsNullOrEmpty_BitSetArray
	{
		[Test]
		public void IsTrue()
		{
			Assert.True (((BitSetArray)null).IsNullOrEmpty());
			Assert.True ((new BitSetArray()).IsNullOrEmpty());
			Assert.True ((new BitSetArray(100)).IsNullOrEmpty());
		}

		[Test]
		public void IsFalse()
		{
			Assert.False ((new BitSetArray(){0}).IsNullOrEmpty());
			Assert.False ((new BitSetArray(100){1,2,3}).IsNullOrEmpty());
			Assert.False ((new BitSetArray(100, true)).IsNullOrEmpty());
		}
	}
}
