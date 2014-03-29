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
	public class GetHashCode
	{
		[Test]
		public void GetHashCode_of_NullOrEmpty()
		{
			ICodeSet nullOrEmpty = null;
			Assert.True (ICodeSetService.GetHashCode(nullOrEmpty) == 0);
			nullOrEmpty = new CodeSetBits();
			Assert.True (ICodeSetService.GetHashCode(nullOrEmpty) == 0);
			nullOrEmpty = CodeSetNull.Singleton;
			Assert.True (ICodeSetService.GetHashCode(nullOrEmpty) == 0);
		}

		[Test]
		public void GetHashCode_of_NotEmpty()
		{
			ICodeSet notEmpty;
			int hashCode;

			notEmpty = new CodeSetBits(new Code[]{6});
			Assert.True (ICodeSetService.GetHashCode(notEmpty) != 0);
			hashCode = ICodeSetService.GetHashCode(notEmpty);
			Assert.True (ICodeSetService.GetHashCode(new Code(6)) == hashCode);

			notEmpty = new CodeSetBits(new Code[]{1,7});
			Assert.True (ICodeSetService.GetHashCode(notEmpty) != 0);
			hashCode = ICodeSetService.GetHashCode(notEmpty);
			Assert.True (ICodeSetService.GetHashCode(new CodeSetPair(1,7)) == hashCode);
			
			notEmpty = new CodeSetBits(new Code[]{1,7,80});
			Assert.True (ICodeSetService.GetHashCode(notEmpty) != 0);
			hashCode = ICodeSetService.GetHashCode(notEmpty);
			Assert.True (ICodeSetService.GetHashCode(new CodeSetList(1,7,80)) == hashCode);
			
		}
	}
}
