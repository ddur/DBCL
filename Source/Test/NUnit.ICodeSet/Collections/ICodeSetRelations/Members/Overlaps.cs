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
	public class Overlaps
	{
		[Test]
		public void Null()
		{
			ICodeSet a = null;
			ICodeSet b = null;

			Assert.IsFalse (a.Overlaps(b)); 
			Assert.IsFalse (b.Overlaps(a)); 

			Assert.IsTrue (a.QuickSetOverlaps(b) == false); // null/empty never overlaps
			Assert.IsTrue (b.QuickSetOverlaps(a) == false); // null/empty never overlaps

			Assert.IsTrue (a.Overlaps(b) == b.Overlaps(a));

			b = new Code(0); 

			Assert.IsFalse (a.Overlaps(b)); 
			Assert.IsFalse (b.Overlaps(a)); 

			Assert.IsTrue (a.QuickSetOverlaps(b) == false); // null/empty never overlaps
			Assert.IsTrue (b.QuickSetOverlaps(a) == false); // null/empty never overlaps

			Assert.IsTrue (a.Overlaps(b) == b.Overlaps(a));
		}

		[Test]
		public void Empty()
		{
			ICodeSet a = CodeSetNull.Singleton;
			ICodeSet b = CodeSetNull.Singleton;

			Assert.IsFalse (a.Overlaps(b)); 
			Assert.IsFalse (b.Overlaps(a)); 

			Assert.IsTrue (a.QuickSetOverlaps(b) == false); // null/empty never overlaps
			Assert.IsTrue (b.QuickSetOverlaps(a) == false); // null/empty never overlaps

			Assert.IsTrue (a.Overlaps(b) == b.Overlaps(a)); 

			b = new CodeSetPair(0,7); 

			Assert.IsTrue (a.QuickSetOverlaps(b) == false); // null/empty never overlaps
			Assert.IsTrue (b.QuickSetOverlaps(a) == false); // null/empty never overlaps

			Assert.IsFalse (a.Overlaps(b)); 
			Assert.IsFalse (b.Overlaps(a)); 

			Assert.IsTrue (a.Overlaps(b) == b.Overlaps(a));
		}

		[Test]
		public void NullOrEmpty()
		{
			ICodeSet a = null;
			ICodeSet b = CodeSetNull.Singleton;

			Assert.IsFalse (a.Overlaps(b)); 
			Assert.IsFalse (b.Overlaps(a)); 

			Assert.IsTrue (a.QuickSetOverlaps(b) == false); // null/empty never overlaps
			Assert.IsTrue (b.QuickSetOverlaps(a) == false); // null/empty never overlaps

			Assert.IsTrue (a.Overlaps(b) == b.Overlaps(a)); 
		}

		[Test]
		public void ReferenceEqual()
		{
			ICodeSet a = new CodeSetList(0,2,4);

			Assert.IsTrue (a.Overlaps(a));

			Assert.IsTrue (a.QuickSetOverlaps(a) == true); // self allways overlaps
		}

		[Test]
		public void SetEqual()
		{
			ICodeSet a = new CodeSetPage(1,2,5);
			ICodeSet b = new CodeSetList(1,2,5);

			Assert.IsTrue (a.Overlaps(b));
			Assert.IsTrue (b.Overlaps(a));

			Assert.IsTrue (a.QuickSetOverlaps(b) == true); // edge overlaps
			Assert.IsTrue (b.QuickSetOverlaps(a) == true); // edge overlaps

			Assert.IsTrue (a.Overlaps(b) == b.Overlaps(a));
		}

		[Test]
		public void Overlaps_IsTrue()
		{
			ICodeSet a = new CodeSetPage(1,2,3,4,5,7,10);
			ICodeSet b = new CodeSetPage(0,4,9);

			Assert.IsTrue (a.Overlaps(b));
			Assert.IsTrue (b.Overlaps(a));

			Assert.IsTrue (a.QuickSetOverlaps(b) == null); // no quick answer
			Assert.IsTrue (b.QuickSetOverlaps(a) == null); // no quick answer

			Assert.IsTrue (a.Overlaps(b) == b.Overlaps(a));

			b = new CodeSetPair(2,5);

			Assert.IsTrue (a.Overlaps(b));
			Assert.IsTrue (b.Overlaps(a));

			Assert.IsTrue (a.QuickSetOverlaps(b) == true); // edge overlaps
			Assert.IsTrue (b.QuickSetOverlaps(a) == true); // edge overlaps

			Assert.IsTrue (a.Overlaps(b) == b.Overlaps(a));

			b = new Code(2);

			Assert.IsTrue (a.Overlaps(b));
			Assert.IsTrue (b.Overlaps(a));

			Assert.IsTrue (a.QuickSetOverlaps(b) == true); // edge overlaps
			Assert.IsTrue (b.QuickSetOverlaps(a) == true); // edge overlaps

			Assert.IsTrue (a.Overlaps(b) == b.Overlaps(a));
		}

		[Test]
		public void Overlaps_IsFalse()
		{
			ICodeSet a = new CodeSetPage(1,3,5);
			ICodeSet b = new CodeSetPage(0,2,4);

			Assert.IsFalse (a.Overlaps(b));
			Assert.IsFalse (b.Overlaps(a));

			Assert.IsTrue (a.QuickSetOverlaps(b) == null); // no quick answer
			Assert.IsTrue (b.QuickSetOverlaps(a) == null); // no quick answer

			Assert.IsTrue (a.Overlaps(b) == b.Overlaps(a));

			b = new CodeSetPair(0,7);

			Assert.IsFalse (a.Overlaps(b));
			Assert.IsFalse (b.Overlaps(a));

			Assert.IsTrue (a.QuickSetOverlaps(b) == null); // no quick answer
			Assert.IsTrue (b.QuickSetOverlaps(a) == null); // no quick answer

			Assert.IsTrue (a.Overlaps(b) == b.Overlaps(a));

			b = new Code(2);

			Assert.IsFalse (a.Overlaps(b));
			Assert.IsFalse (b.Overlaps(a));

			Assert.IsTrue (a.QuickSetOverlaps(b) == null); // no quick answer
			Assert.IsTrue (b.QuickSetOverlaps(a) == null); // no quick answer

			Assert.IsTrue (a.Overlaps(b) == b.Overlaps(a));

			b = new Code(22);

			Assert.IsFalse (a.Overlaps(b));
			Assert.IsFalse (b.Overlaps(a));

			Assert.IsTrue (a.QuickSetOverlaps(b) == false); // one does not overlap
			Assert.IsTrue (b.QuickSetOverlaps(a) == false); // one does not overlap

			Assert.IsTrue (a.Overlaps(b) == b.Overlaps(a));
		}
	}
}
