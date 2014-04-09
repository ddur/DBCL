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
	public class IsNullOrEmpty_BitSetArray
	{
		[Test]
		public void IsTrue()
		{
			Assert.True (((BitSetArray)null).IsNullOrEmpty());
			Assert.True ((BitSetArray.Size()).IsNullOrEmpty());
			Assert.True ((BitSetArray.Size(100)).IsNullOrEmpty());
		}

		[Test]
		public void IsFalse()
		{
			Assert.False ((BitSetArray.From (0)).IsNullOrEmpty());
			Assert.False ((BitSetArray.From (1,2,3)).IsNullOrEmpty());
			Assert.False ((BitSetArray.Size (100, true)).IsNullOrEmpty());
		}
	}
}
