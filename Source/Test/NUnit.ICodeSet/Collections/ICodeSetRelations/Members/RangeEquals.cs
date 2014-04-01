// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSetRelationsTest.Members
{
	[TestFixture]
	public class RangeEquals
	{
		[Test]
		public void Null()
		{
			ICodeSet a = null;
			ICodeSet b = null;
			
			Assert.IsTrue (a.RangeEquals(b));
			Assert.IsTrue (b.RangeEquals(a));

			b = new Code(7);

			Assert.IsFalse (a.RangeEquals(b));
			Assert.IsFalse (b.RangeEquals(a));
		}

		[Test]
		public void Empty()
		{
			ICodeSet a = new CodeSetBits();
			ICodeSet b = CodeSetNull.Singleton;;
			
			Assert.IsTrue (a.RangeEquals(b));
			Assert.IsTrue (b.RangeEquals(a));

			b = new CodeSetPair(7,12);

			Assert.IsFalse (a.RangeEquals(b));
			Assert.IsFalse (b.RangeEquals(a));
		}

		[Test]
		public void NullOrEmpty()
		{
			ICodeSet a = null;
			ICodeSet b = CodeSetNull.Singleton;;
			
			Assert.IsTrue (a.RangeEquals(b));
			Assert.IsTrue (b.RangeEquals(a));
		}

		[Test]
		public void ReferenceEqual()
		{
			ICodeSet a = null;
			
			Assert.IsTrue (a.RangeEquals(a));

			a = new CodeSetList(1,3,7,8,9);

			Assert.IsTrue (a.RangeEquals(a));
		}

		[Test]
		public void SetEqual()
		{
			ICodeSet a = new CodeSetBits(6,9,28);
			ICodeSet b = new CodeSetList(6,9,28);
			
			Assert.IsTrue (a.RangeEquals(b));
			Assert.IsTrue (b.RangeEquals(a));
		}

		[Test]
		public void RangeEquals_IsTrue()
		{
			ICodeSet a = new CodeSetList(6,9,28);
			ICodeSet b = new CodeSetPair(6,28);
			
			Assert.IsTrue (a.RangeEquals(b));
			Assert.IsTrue (b.RangeEquals(a));
		}

		[Test]
		public void RangeEquals_IsFalse()
		{
			ICodeSet a = new CodeSetList(6,9,28);
			ICodeSet b = new CodeSetPair(6,27);
			
			Assert.IsFalse (a.RangeEquals(b));
			Assert.IsFalse (b.RangeEquals(a));

			b = new CodeSetPair (9,28);
			
			Assert.IsFalse (a.RangeEquals(b));
			Assert.IsFalse (b.RangeEquals(a));

			b = new CodeSetPair (28,29);
			
			Assert.IsFalse (a.RangeEquals(b));
			Assert.IsFalse (b.RangeEquals(a));

			b = new Code (6);
			
			Assert.IsFalse (a.RangeEquals(b));
			Assert.IsFalse (b.RangeEquals(a));
		}
	}
}
