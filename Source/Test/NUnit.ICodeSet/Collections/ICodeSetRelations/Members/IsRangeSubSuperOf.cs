﻿// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSetRelationsTest.Members
{
	[TestFixture]
	public class IsRangeSubSuperOf
	{
		[Test]
		public void Null() {
			ICodeSet a = null;
			ICodeSet b = null;

			Assert.False (a.IsRangeSubsetOf(b));
			Assert.False (b.IsRangeSubsetOf(a));

			Assert.True (a.IsRangeSubsetOf(b) == b.IsRangeSupersetOf(a));

			b = new CodeSetPair(1,2);

			Assert.False (a.IsRangeSubsetOf(b));
			Assert.False (b.IsRangeSubsetOf(a));

			Assert.True (a.IsRangeSubsetOf(b) == b.IsRangeSupersetOf(a));
		}

		[Test]
		public void Empty() {
			ICodeSet a = CodeSetNull.Singleton;
			ICodeSet b = CodeSetNull.Singleton;

			Assert.False (a.IsRangeSubsetOf(b));
			Assert.False (b.IsRangeSubsetOf(a));

			Assert.True (a.IsRangeSubsetOf(b) == b.IsRangeSupersetOf(a));

			b = new CodeSetList(1,2,3,7);

			Assert.False (a.IsRangeSubsetOf(b));
			Assert.False (b.IsRangeSubsetOf(a));

			Assert.True (a.IsRangeSubsetOf(b) == b.IsRangeSupersetOf(a));
		}

		[Test]
		public void NullOrEmpty() {
			ICodeSet a = null;
			ICodeSet b = CodeSetNull.Singleton;

			Assert.False (a.IsRangeSubsetOf(b));
			Assert.False (b.IsRangeSubsetOf(a));

			Assert.True (a.IsRangeSubsetOf(b) == b.IsRangeSupersetOf(a));
		}

		[Test]
		public void ReferenceEqual() {
			ICodeSet a = new CodeSetPage(1,2,7);

			Assert.True (a.IsRangeSubsetOf(a));

			Assert.True (a.IsRangeSubsetOf(a) == a.IsRangeSupersetOf(a));
		}

		[Test]
		public void RangeEqual() {
			ICodeSet a = new CodeSetPage(1,2,5);
			ICodeSet b = new CodeSetList(1,2,5);

			Assert.True (a.IsRangeSubsetOf(b));
			Assert.True (b.IsRangeSubsetOf(a));

			Assert.True (a.IsRangeSubsetOf(b) == b.IsRangeSupersetOf(a));

			b = new CodeSetPair(1,5); 
			Assert.True (a.IsRangeSubsetOf(b));
			Assert.True (b.IsRangeSubsetOf(a));

			Assert.True (a.IsRangeSubsetOf(b) == b.IsRangeSupersetOf(a));
		}

		[Test]
		public void ProperRangeSubset() {
			ICodeSet a = new CodeSetList(0,1,2,5);
			ICodeSet b = new CodeSetPage(1,2,5);

			Assert.True (b.IsRangeSubsetOf(a));
			Assert.False (a.IsRangeSubsetOf(b));

			Assert.True (a.IsRangeSubsetOf(b) == b.IsRangeSupersetOf(a));

			b = new CodeSetList(0,1,4); 
			Assert.True (b.IsRangeSubsetOf(a));
			Assert.False (a.IsRangeSubsetOf(b));

			Assert.True (a.IsRangeSubsetOf(b) == b.IsRangeSupersetOf(a));

			b = new Code(3);
			Assert.True (b.IsRangeSubsetOf(a));
			Assert.False (a.IsRangeSubsetOf(b));

			Assert.True (a.IsRangeSubsetOf(b) == b.IsRangeSupersetOf(a));
		}

		[Test]
		public void NotRangeSubset() {
			ICodeSet a = new CodeSetPage(0,1,2,5);
			ICodeSet b = new CodeSetList(1,2,3,4,7);
			
			Assert.False (a.IsRangeSubsetOf(b));
			Assert.False (b.IsRangeSubsetOf(a));

			Assert.True (a.IsRangeSubsetOf(b) == b.IsRangeSupersetOf(a));

			b = new Code(9);
			Assert.False (b.IsRangeSubsetOf(a));
			Assert.False (a.IsRangeSubsetOf(b));

			Assert.True (a.IsRangeSubsetOf(b) == b.IsRangeSupersetOf(a));
		}
	}
}
