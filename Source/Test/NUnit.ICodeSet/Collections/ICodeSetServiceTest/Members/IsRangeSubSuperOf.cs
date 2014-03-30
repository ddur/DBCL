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
	public class IsRangeSubSuperOf
	{
		[Test]
		public void Null() {
			ICodeSet a = null;
			ICodeSet b = null;

			Assert.False (a.IsRangeSubsetOf(b));
			Assert.False (b.IsRangeSubsetOf(a));

			Assert.True (a.IsRangeSubsetOf(b) == b.IsRangeSupersetOf(a));
		}

		[Test]
		public void Empty() {
			ICodeSet a = new CodeSetBits();
			ICodeSet b = CodeSetNull.Singleton;

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
		public void Self() {
			ICodeSet a = new CodeSetBits(1,2,3);

			Assert.True (a.IsRangeSubsetOf(a));

			Assert.True (a.IsRangeSubsetOf(a) == a.IsRangeSupersetOf(a));
		}

		[Test]
		public void RangeEqual() {
			ICodeSet a = new CodeSetBits(1,2,3);
			ICodeSet b = new CodeSetBits(1,2,3);

			Assert.True (a.IsRangeSubsetOf(b));
			Assert.True (b.IsRangeSubsetOf(a));

			Assert.True (a.IsRangeSubsetOf(b) == b.IsRangeSupersetOf(a));

			b = new CodeSetPair(1,3); 
			Assert.True (a.IsRangeSubsetOf(b));
			Assert.True (b.IsRangeSubsetOf(a));

			Assert.True (a.IsRangeSubsetOf(b) == b.IsRangeSupersetOf(a));
		}

		[Test]
		public void ProperRangeSubset() {
			ICodeSet a = new CodeSetBits(0,1,2,3);
			ICodeSet b = new CodeSetBits(1,2,3);

			Assert.True (b.IsRangeSubsetOf(a));
			Assert.False (a.IsRangeSubsetOf(b));

			Assert.True (a.IsRangeSubsetOf(b) == b.IsRangeSupersetOf(a));

			b = new CodeSetBits(0,1,2); 
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
			ICodeSet a = new CodeSetBits(0,1,2,3);
			ICodeSet b = new CodeSetBits(1,2,3,4,5);
			
			Assert.False (a.IsRangeSubsetOf(b));
			Assert.False (b.IsRangeSubsetOf(a));

			Assert.True (a.IsRangeSubsetOf(b) == b.IsRangeSupersetOf(a));

			b = new Code(5);
			Assert.False (b.IsRangeSubsetOf(a));
			Assert.False (a.IsRangeSubsetOf(b));

			Assert.True (a.IsRangeSubsetOf(b) == b.IsRangeSupersetOf(a));
		}
	}
}
