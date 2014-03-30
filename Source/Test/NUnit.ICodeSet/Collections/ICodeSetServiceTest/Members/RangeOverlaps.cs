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
	public class RangeOverlaps
	{
		[Test]
		public void Null()
		{
			ICodeSet a = null;
			ICodeSet b = null;
			
			Assert.IsFalse (a.RangeOverlaps(b));
			Assert.IsFalse (b.RangeOverlaps(a));

			b = new Code(7);

			Assert.IsFalse (a.RangeOverlaps(b));
			Assert.IsFalse (b.RangeOverlaps(a));
		}

		[Test]
		public void Empty()
		{
			ICodeSet a = new CodeSetBits();
			ICodeSet b = CodeSetNull.Singleton;;
			
			Assert.IsFalse (a.RangeOverlaps(b));
			Assert.IsFalse (b.RangeOverlaps(a));

			b = new CodeSetPair(7,12);

			Assert.IsFalse (a.RangeOverlaps(b));
			Assert.IsFalse (b.RangeOverlaps(a));
		}

		[Test]
		public void NullOrEmpty()
		{
			ICodeSet a = null;
			ICodeSet b = CodeSetNull.Singleton;;
			
			Assert.IsFalse (a.RangeOverlaps(b));
			Assert.IsFalse (b.RangeOverlaps(a));
		}

		[Test]
		public void ReferenceEqual()
		{
			ICodeSet a = null;
			
			Assert.IsFalse (a.RangeOverlaps(a));

			a = new CodeSetList(1,3,7,8,9);

			Assert.IsTrue (a.RangeOverlaps(a));
		}

		[Test]
		public void SetEqual()
		{
			ICodeSet a = new CodeSetBits(6,9,28);
			ICodeSet b = new CodeSetList(6,9,28);
			
			Assert.IsTrue (a.RangeOverlaps(b));
			Assert.IsTrue (b.RangeOverlaps(a));
		}

		[Test]
		public void RangeEqual()
		{
			ICodeSet a = new CodeSetList(6,9,28);
			ICodeSet b = new CodeSetPair(6,28);
			
			Assert.IsTrue (a.RangeOverlaps(b));
			Assert.IsTrue (b.RangeOverlaps(a));
		}

		[Test]
		public void RangeOverlaps_IsTrue()
		{
			ICodeSet a = new CodeSetList(6,9,28);
			ICodeSet b = new CodeSetPair(6,28);
			
			Assert.IsTrue (a.RangeOverlaps(b));
			Assert.IsTrue (b.RangeOverlaps(a));

			b = new CodeSetPair(0,10);

			Assert.IsTrue (a.RangeOverlaps(b));
			Assert.IsTrue (b.RangeOverlaps(a));

			b = new CodeSetPair(10,30);

			Assert.IsTrue (a.RangeOverlaps(b));
			Assert.IsTrue (b.RangeOverlaps(a));

			b = new CodeSetPair(0,6);

			Assert.IsTrue (a.RangeOverlaps(b));
			Assert.IsTrue (b.RangeOverlaps(a));

			b = new CodeSetPair(28,29);

			Assert.IsTrue (a.RangeOverlaps(b));
			Assert.IsTrue (b.RangeOverlaps(a));

			b = new Code(11);

			Assert.IsTrue (a.RangeOverlaps(b));
			Assert.IsTrue (b.RangeOverlaps(a));
		}

		[Test]
		public void RangeOverlaps_IsFalse()
		{
			ICodeSet a = new CodeSetList(6,9,28);
			ICodeSet b = new CodeSetPair(29,37);
			
			Assert.IsFalse (a.RangeOverlaps(b));
			Assert.IsFalse (b.RangeOverlaps(a));

			b = new CodeSetPair (0,5);
			
			Assert.IsFalse (a.RangeOverlaps(b));
			Assert.IsFalse (b.RangeOverlaps(a));

			b = new Code (4);
			
			Assert.IsFalse (a.RangeOverlaps(b));
			Assert.IsFalse (b.RangeOverlaps(a));
		}
	}
}
