// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetFullTest
{
	[TestFixture]
	public class Constructors
	{
		[Test]
		public void FromRange()
		{
			CodeSetFull csf;
			csf = new CodeSetFull(1, 3); // at least 3 members
			csf = new CodeSetFull(Code.MinValue, Code.MaxValue);
		}

		[Test]
		public void FromRangeThrows()
		{
			CodeSetFull csf;
			Assert.Throws<ArgumentException>(
				delegate {
					csf = new CodeSetFull(9, 3);
				}
			);
			Assert.Throws<ArgumentException>(
				delegate {
					csf = new CodeSetFull(1, 2);
				}
			);
			Assert.Throws<InvalidCastException>(
				delegate {
					csf = new CodeSetFull(-20, 3);
				}
			);
			Assert.Throws<InvalidCastException>(
				delegate {
					csf = new CodeSetFull(0, -32);
				}
			);
		}
	}
}
