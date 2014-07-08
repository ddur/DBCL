// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetRelationsTest.Members
{
	[TestFixture]
	public class HashCode
	{
		[Test]
		public void HashCode_of_NullOrEmpty()
		{
			ICodeSet nullOrEmpty = null;
			Assert.True (nullOrEmpty.HashCode() == 0);

			nullOrEmpty = CodeSetNone.Singleton;
			Assert.True (nullOrEmpty.HashCode() == 0);
		}

		[Test]
		public void HashCode_of_NotEmpty()
		{
			ICodeSet notEmpty;
			int hashCode;

			notEmpty = new Code(6);
			Assert.True (notEmpty.HashCode() != 0);
			hashCode = notEmpty.HashCode();
			Assert.True (new Code(6).HashCode() == hashCode);

			notEmpty = CodeSetPair.From(1,7);
			Assert.True (notEmpty.HashCode() != 0);
			hashCode = notEmpty.HashCode();
			Assert.True (CodeSetPair.From(1, 7).HashCode() == hashCode);
			
			notEmpty = CodeSetList.From(1, 7, 80);
			Assert.True (notEmpty.HashCode() != 0);
			hashCode = notEmpty.HashCode();
			Assert.True (CodeSetList.From(1, 7, 80).HashCode() == hashCode);
			
		}
	}
}
