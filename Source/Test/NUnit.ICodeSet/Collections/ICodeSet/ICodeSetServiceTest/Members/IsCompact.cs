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
	public class IsCompact
	{
		[Test]
		public void Null_ReturnsFalse()
		{
			BitSetArray isNull = null;
			Assert.False (isNull.IsCompact());
		}

		[Test]
		public void Empty_ReturnsTrue()
		{
			var isEmpty = BitSetArray.Size ();
			Assert.True (isEmpty.IsCompact());
		}

		[Test]
		public void NotEmpty_ReturnsCompact()
		{
			var notEmpty = BitSetArray.From (0);
			Assert.True (notEmpty.IsCompact());

			notEmpty = BitSetArray.From (1);
			Assert.False (notEmpty.IsCompact());

			notEmpty = BitSetArray.From (1,10);
			Assert.False (notEmpty.IsCompact());

			notEmpty = BitSetArray.From (0,10);
			Assert.True (notEmpty.IsCompact());

			notEmpty = BitSetArray.From (0,10,100);
			Assert.True (notEmpty.IsCompact());

			notEmpty = BitSetArray.Size (200);
			notEmpty.Add (0);
			notEmpty.Add (10);
			notEmpty.Add (100);
			Assert.False (notEmpty.IsCompact());

			notEmpty.TrimExcess();
			Assert.True (notEmpty.IsCompact());
		}
	}
}
