// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using System.Linq;
using DD.Collections;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetServiceTest.Members
{
	[TestFixture]
	public class ToValues
	{
		[Test]
		public void When_NullCodes()
		{
			Assert.That ( ((IEnumerable<Code>)null).ToValues() != null );
			Assert.That ( !((IEnumerable<Code>)null).ToValues().Any() );
		}

		[Test]
		public void When_NullChars()
		{
			Assert.That ( ((IEnumerable<Char>)null).ToValues() != null );
			Assert.That ( !((IEnumerable<Char>)null).ToValues().Any() );
		}

		[Test]
		public void When_EmptyCodes()
		{
			Assert.That ( (new Code[0]).ToValues() != null );
			Assert.That ( !((IEnumerable<Code>)null).ToValues().Any() );
		}

		[Test]
		public void When_EmptyChars()
		{
			Assert.That ( (new Char[0]).ToValues() != null );
			Assert.That ( !((IEnumerable<Char>)null).ToValues().Any() );
		}
	}
}
