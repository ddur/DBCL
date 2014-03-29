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
	public class IsCompact
	{
		[Test]
		public void IsCompact_Null_ReturnsFalse()
		{
			BitSetArray isNull = null;
			Assert.False (isNull.IsCompact());
		}

		[Test]
		public void IsCompact_Empty_ReturnsTrue()
		{
			var isEmpty = new BitSetArray();
			Assert.True (isEmpty.IsCompact());
		}

		[Test]
		public void IsCompact_NotEmpty()
		{
			var notEmpty = new BitSetArray() {0};
			Assert.True (notEmpty.IsCompact());

			notEmpty = new BitSetArray() {1};
			Assert.False (notEmpty.IsCompact());

			notEmpty = new BitSetArray() {1,10};
			Assert.False (notEmpty.IsCompact());

			notEmpty = new BitSetArray() {0,10};
			Assert.True (notEmpty.IsCompact());

			notEmpty = new BitSetArray() {0,10,100};
			Assert.True (notEmpty.IsCompact());

			notEmpty = new BitSetArray(200) {0,10,100};
			Assert.False (notEmpty.IsCompact());

			notEmpty.TrimExcess();
			Assert.True (notEmpty.IsCompact());
		}
	}
}
