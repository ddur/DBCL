﻿// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSetServiceTest.Members
{
	[TestFixture]
	public class SuperSubSet
	{
		[Test]
		public void Null()
		{
			ICodeSet a = null;
			ICodeSet b = null;

			Assert.IsFalse (a.IsSubsetOf(b)); 
			Assert.IsFalse (b.IsSubsetOf(a)); 

			Assert.IsFalse (a.IsSupersetOf(b)); 
			Assert.IsFalse (b.IsSupersetOf(a)); 

			Assert.IsFalse (a.IsProperSubsetOf(b)); 
			Assert.IsFalse (b.IsProperSubsetOf(a)); 

			Assert.IsFalse (a.IsProperSupersetOf(b)); 
			Assert.IsFalse (b.IsProperSupersetOf(a)); 

			Assert.IsTrue (a.IsSubsetOf(b) == b.IsSupersetOf(a)); 
			Assert.IsTrue (b.IsSubsetOf(a) == a.IsSupersetOf(b)); 

			Assert.IsTrue (a.IsProperSubsetOf(b) == b.IsProperSupersetOf(a)); 
			Assert.IsTrue (b.IsProperSubsetOf(a) == a.IsProperSupersetOf(b)); 

			b = new Code(5);

			Assert.IsFalse (a.IsSubsetOf(b)); 
			Assert.IsFalse (b.IsSubsetOf(a)); 

			Assert.IsFalse (a.IsSupersetOf(b)); 
			Assert.IsFalse (b.IsSupersetOf(a)); 

			Assert.IsFalse (a.IsProperSubsetOf(b)); 
			Assert.IsFalse (b.IsProperSubsetOf(a)); 

			Assert.IsFalse (a.IsProperSupersetOf(b)); 
			Assert.IsFalse (b.IsProperSupersetOf(a)); 

			Assert.IsTrue (a.IsSubsetOf(b) == b.IsSupersetOf(a)); 
			Assert.IsTrue (b.IsSubsetOf(a) == a.IsSupersetOf(b)); 

			Assert.IsTrue (a.IsProperSubsetOf(b) == b.IsProperSupersetOf(a)); 
			Assert.IsTrue (b.IsProperSubsetOf(a) == a.IsProperSupersetOf(b)); 
		}

		[Test]
		public void Empty()
		{
			ICodeSet a = new CodeSetBits();
			ICodeSet b = CodeSetNull.Singleton;

			Assert.IsFalse (a.IsSubsetOf(b)); 
			Assert.IsFalse (b.IsSubsetOf(a)); 

			Assert.IsFalse (a.IsSupersetOf(b)); 
			Assert.IsFalse (b.IsSupersetOf(a)); 

			Assert.IsFalse (a.IsProperSubsetOf(b)); 
			Assert.IsFalse (b.IsProperSubsetOf(a)); 

			Assert.IsFalse (a.IsProperSupersetOf(b)); 
			Assert.IsFalse (b.IsProperSupersetOf(a)); 

			Assert.IsTrue (a.IsSubsetOf(b) == b.IsSupersetOf(a)); 
			Assert.IsTrue (b.IsSubsetOf(a) == a.IsSupersetOf(b)); 

			Assert.IsTrue (a.IsProperSubsetOf(b) == b.IsProperSupersetOf(a)); 
			Assert.IsTrue (b.IsProperSubsetOf(a) == a.IsProperSupersetOf(b)); 

			b = new CodeSetBits(1,5);

			Assert.IsFalse (a.IsSubsetOf(b)); 
			Assert.IsFalse (b.IsSubsetOf(a)); 

			Assert.IsFalse (a.IsSupersetOf(b)); 
			Assert.IsFalse (b.IsSupersetOf(a)); 

			Assert.IsFalse (a.IsProperSubsetOf(b)); 
			Assert.IsFalse (b.IsProperSubsetOf(a)); 

			Assert.IsFalse (a.IsProperSupersetOf(b)); 
			Assert.IsFalse (b.IsProperSupersetOf(a)); 

			Assert.IsTrue (a.IsSubsetOf(b) == b.IsSupersetOf(a)); 
			Assert.IsTrue (b.IsSubsetOf(a) == a.IsSupersetOf(b)); 

			Assert.IsTrue (a.IsProperSubsetOf(b) == b.IsProperSupersetOf(a)); 
			Assert.IsTrue (b.IsProperSubsetOf(a) == a.IsProperSupersetOf(b)); 
		}

		[Test]
		public void NullOrEmpty()
		{
			ICodeSet a = null;
			ICodeSet b = CodeSetNull.Singleton;

			Assert.IsFalse (a.IsSubsetOf(b)); 
			Assert.IsFalse (b.IsSubsetOf(a)); 

			Assert.IsFalse (a.IsSupersetOf(b)); 
			Assert.IsFalse (b.IsSupersetOf(a)); 

			Assert.IsFalse (a.IsProperSubsetOf(b)); 
			Assert.IsFalse (b.IsProperSubsetOf(a)); 

			Assert.IsFalse (a.IsProperSupersetOf(b)); 
			Assert.IsFalse (b.IsProperSupersetOf(a)); 

			Assert.IsTrue (a.IsSubsetOf(b) == b.IsSupersetOf(a)); 
			Assert.IsTrue (b.IsSubsetOf(a) == a.IsSupersetOf(b)); 

			Assert.IsTrue (a.IsProperSubsetOf(b) == b.IsProperSupersetOf(a)); 
			Assert.IsTrue (b.IsProperSubsetOf(a) == a.IsProperSupersetOf(b)); 
		}

		[Test]
		public void ReferenceEqual()
		{
			ICodeSet a = new CodeSetBits(1,2,3);

			Assert.IsTrue (a.IsSubsetOf(a));
			Assert.IsTrue (a.IsSupersetOf(a));

			Assert.IsFalse (a.IsProperSubsetOf(a));
			Assert.IsFalse (a.IsProperSupersetOf(a));

			Assert.IsTrue (a.IsSubsetOf(a) == a.IsSupersetOf(a));
			Assert.IsTrue (a.IsProperSubsetOf(a) == a.IsProperSupersetOf(a)); 
		}

		[Test]
		public void SetEqual()
		{
			ICodeSet a = new CodeSetBits(1,2,3);
			ICodeSet b = new CodeSetBits(1,2,3);

			Assert.IsTrue (a.IsSubsetOf(b));
			Assert.IsTrue (b.IsSubsetOf(a));

			Assert.IsTrue (a.IsSupersetOf(b));
			Assert.IsTrue (b.IsSupersetOf(a));

			Assert.IsFalse (a.IsProperSubsetOf(b));
			Assert.IsFalse (b.IsProperSubsetOf(a));

			Assert.IsFalse (a.IsProperSupersetOf(b));
			Assert.IsFalse (b.IsProperSupersetOf(a));

			Assert.IsTrue (a.IsSubsetOf(b) == b.IsSupersetOf(a));
			Assert.IsTrue (b.IsSubsetOf(a) == a.IsSupersetOf(b));

			Assert.IsTrue (a.IsProperSubsetOf(b) == b.IsProperSupersetOf(a));
			Assert.IsTrue (b.IsProperSubsetOf(a) == a.IsProperSupersetOf(b));
		}

		[Test]
		public void ProperSubset()
		{
			ICodeSet a = new CodeSetBits(1,2,3);
			ICodeSet b = new CodeSetBits(1,3);

			Assert.IsFalse (a.IsSubsetOf(b));
			Assert.IsTrue (b.IsSubsetOf(a));

			Assert.IsTrue (a.IsSupersetOf(b));
			Assert.IsFalse (b.IsSupersetOf(a));

			Assert.IsFalse (a.IsProperSubsetOf(b));
			Assert.IsTrue (b.IsProperSubsetOf(a));

			Assert.IsTrue (a.IsProperSupersetOf(b));
			Assert.IsFalse (b.IsProperSupersetOf(a));

			Assert.IsTrue (a.IsSubsetOf(b) == b.IsSupersetOf(a));
			Assert.IsTrue (b.IsSubsetOf(a) == a.IsSupersetOf(b));

			Assert.IsTrue (a.IsProperSubsetOf(b) == b.IsProperSupersetOf(a));
			Assert.IsTrue (b.IsProperSubsetOf(a) == a.IsProperSupersetOf(b));
		}
	}
}
