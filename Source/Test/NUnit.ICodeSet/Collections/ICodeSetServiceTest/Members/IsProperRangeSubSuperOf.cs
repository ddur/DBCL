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
	public class IsProperRangeSubSuperOf
	{
		[Test]
		public void Null() {
			ICodeSet a = null;
			ICodeSet b = null;

			Assert.False (a.IsProperRangeSubsetOf(b));
			Assert.False (b.IsProperRangeSubsetOf(a));

			Assert.True (a.IsProperRangeSubsetOf(b) == b.IsProperRangeSupersetOf(a));
		}

		[Test]
		public void Empty() {
			ICodeSet a = new CodeSetBits();
			ICodeSet b = CodeSetNull.Singleton;

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
			ICodeSet a = new CodeSetBits(1,2,3);

			Assert.False (a.IsProperRangeSubsetOf(a));

			Assert.True (a.IsProperRangeSubsetOf(a) == a.IsProperRangeSupersetOf(a));
		}

		[Test]
		public void RangeEqual() {
			ICodeSet a = new CodeSetBits(1,2,3);
			ICodeSet b = new CodeSetBits(1,2,3);

			Assert.False (a.IsProperRangeSubsetOf(b));
			Assert.False (b.IsProperRangeSubsetOf(a));

			Assert.True (a.IsProperRangeSubsetOf(b) == b.IsProperRangeSupersetOf(a));

			b = new CodeSetPair(1,3); 
			Assert.False (a.IsProperRangeSubsetOf(b));
			Assert.False (b.IsProperRangeSubsetOf(a));

			Assert.True (a.IsProperRangeSubsetOf(b) == b.IsProperRangeSupersetOf(a));
		}

		[Test]
		public void ProperRangeSubset() {
			ICodeSet a = new CodeSetBits(0,1,2,3);
			ICodeSet b = new CodeSetBits(1,2,3);

			Assert.True (b.IsProperRangeSubsetOf(a));
			Assert.False (a.IsProperRangeSubsetOf(b));

			Assert.True (a.IsProperRangeSubsetOf(b) == b.IsProperRangeSupersetOf(a));

			b = new CodeSetBits(0,1,2); 
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
			ICodeSet a = new CodeSetBits(0,1,2,3);
			ICodeSet b = new CodeSetBits(1,2,3,4,5);
			
			Assert.False (a.IsProperRangeSubsetOf(b));
			Assert.False (b.IsProperRangeSubsetOf(a));

			Assert.True (a.IsProperRangeSubsetOf(b) == b.IsProperRangeSupersetOf(a));

			b = new Code(5);
			Assert.False (b.IsProperRangeSubsetOf(a));
			Assert.False (a.IsProperRangeSubsetOf(b));

			Assert.True (a.IsProperRangeSubsetOf(b) == b.IsProperRangeSupersetOf(a));
		}
	}
}
