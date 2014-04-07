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
	public class IsProperRangeSubSuperOf
	{
		[Test]
		public void Null() {
			ICodeSet a = null;
			ICodeSet b = null;

			Assert.False (a.IsProperRangeSubsetOf(b));
			Assert.False (b.IsProperRangeSubsetOf(a));

			Assert.True (a.IsProperRangeSubsetOf(b) == b.IsProperRangeSupersetOf(a));

			b = new CodeSetPair(1,2);

			Assert.False (a.IsProperRangeSubsetOf(b));
			Assert.False (b.IsProperRangeSubsetOf(a));

			Assert.True (a.IsProperRangeSubsetOf(b) == b.IsProperRangeSupersetOf(a));
		}

		[Test]
		public void Empty() {
			ICodeSet a = CodeSetNull.Singleton;
			ICodeSet b = CodeSetNull.Singleton;

			Assert.False (a.IsProperRangeSubsetOf(b));
			Assert.False (b.IsProperRangeSubsetOf(a));

			Assert.True (a.IsProperRangeSubsetOf(b) == b.IsProperRangeSupersetOf(a));

			b = new Code(7);

			Assert.False (a.IsProperRangeSubsetOf(b));
			Assert.False (b.IsProperRangeSubsetOf(a));

			Assert.True (a.IsProperRangeSubsetOf(b) == b.IsProperRangeSupersetOf(a));
		}

		[Test]
		public void NullOrEmpty() {
			ICodeSet a = null;
			ICodeSet b = CodeSetNull.Singleton;

			Assert.False (a.IsProperRangeSubsetOf(b));
			Assert.False (b.IsProperRangeSubsetOf(a));

			Assert.True (a.IsProperRangeSubsetOf(b) == b.IsProperRangeSupersetOf(a));
		}

		[Test]
		public void ReferenceEqual() {
			ICodeSet a = new CodeSetList(1,2,4);

			Assert.False (a.IsProperRangeSubsetOf(a));

			Assert.True (a.IsProperRangeSubsetOf(a) == a.IsProperRangeSupersetOf(a));
		}

		[Test]
		public void RangeEqual() {
			ICodeSet a = new CodeSetPage(1,2,5);
			ICodeSet b = new CodeSetList(1,2,5);

			Assert.False (a.IsProperRangeSubsetOf(b));
			Assert.False (b.IsProperRangeSubsetOf(a));

			Assert.True (a.IsProperRangeSubsetOf(b) == b.IsProperRangeSupersetOf(a));

			b = new CodeSetPair(1,5); 
			Assert.False (a.IsProperRangeSubsetOf(b));
			Assert.False (b.IsProperRangeSubsetOf(a));

			Assert.True (a.IsProperRangeSubsetOf(b) == b.IsProperRangeSupersetOf(a));
		}

		[Test]
		public void ProperRangeSubset() {
			ICodeSet a = new CodeSetPage(0,1,2,9);
			ICodeSet b = new CodeSetList(1,2,9);

			Assert.True (b.IsProperRangeSubsetOf(a));
			Assert.False (a.IsProperRangeSubsetOf(b));

			Assert.True (a.IsProperRangeSubsetOf(b) == b.IsProperRangeSupersetOf(a));

			b = new CodeSetPage(0,1,8); 
			Assert.True (b.IsProperRangeSubsetOf(a));
			Assert.False (a.IsProperRangeSubsetOf(b));

			Assert.True (a.IsProperRangeSubsetOf(b) == b.IsProperRangeSupersetOf(a));

			b = new Code(3);
			Assert.True (b.IsProperRangeSubsetOf(a));
			Assert.False (a.IsProperRangeSubsetOf(b));

			Assert.True (a.IsProperRangeSubsetOf(b) == b.IsProperRangeSupersetOf(a));
		}

		[Test]
		public void NotRangeSubset() {
			ICodeSet a = new CodeSetPage(0,1,2,5);
			ICodeSet b = new CodeSetList(1,2,3,4,7);
			
			Assert.False (a.IsProperRangeSubsetOf(b));
			Assert.False (b.IsProperRangeSubsetOf(a));

			Assert.True (a.IsProperRangeSubsetOf(b) == b.IsProperRangeSupersetOf(a));

			b = new Code(6);
			Assert.False (b.IsProperRangeSubsetOf(a));
			Assert.False (a.IsProperRangeSubsetOf(b));

			Assert.True (a.IsProperRangeSubsetOf(b) == b.IsProperRangeSupersetOf(a));
		}
	}
}
