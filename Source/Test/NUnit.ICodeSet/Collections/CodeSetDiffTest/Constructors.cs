// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.CodeSetDiffTest
{
	[TestFixture]
	public class Constructors
	{
		[Test]
		public void FromTwoSets()
		{
			CodeSetDiff csd;
			
			csd = new CodeSetDiff(new CodeSetFull(Code.MinValue, Code.MaxValue), new Code(Code.MaxValue/2));
			csd = new CodeSetDiff(new CodeSetFull(Code.MinValue, Code.MaxValue), new CodeSetPair(100, 200));
			csd = new CodeSetDiff(new CodeSetFull(Code.MinValue, Code.MaxValue), new CodeSetFull(100, 200));

		}

		[Test]
		public void FromTwoSetsThrows()
		{
			CodeSetDiff csd;

			// requires no null
			Assert.Throws<ArgumentNullException> (
				delegate { csd = new CodeSetDiff((ICodeSet)null,new CodeSetPair(0,100)); }
			);
			Assert.Throws<ArgumentNullException> (
				delegate { csd = new CodeSetDiff(new CodeSetFull(0,100),(ICodeSet)null); }
			);

			// requires no empty
			Assert.Throws<ArgumentException> (
				delegate { csd = new CodeSetDiff(CodeSetNull.Singleton,new CodeSetPair(0,100)); }
			);
			Assert.Throws<ArgumentException> (
				delegate { csd = new CodeSetDiff(new CodeSetFull(0,100),CodeSetNull.Singleton); }
			);
			
			// requires a.Count = a.Length
			Assert.Throws<ArgumentException> (
				delegate { csd = new CodeSetDiff(new CodeSetPage(new Code[]{0,1,3,4}),new CodeSetPair(1,3)); }
			);
			// requires a is CodeSetFull
			Assert.Throws<ArgumentException> (
				delegate { csd = new CodeSetDiff(new CodeSetPage(new Code[]{0,1,2,3,4}),new CodeSetPair(1,3)); }
			);
			
			// requires a.Last > b.Last  
			Assert.Throws<ArgumentException> (
				delegate { csd = new CodeSetDiff(new CodeSetFull(0,100), new CodeSetPair(1,100)); }
			);
			Assert.Throws<ArgumentException> (
				delegate { csd = new CodeSetDiff(new CodeSetFull(0,100), new CodeSetPair(1,101)); }
			);

			// requires a.First < b.First  
			Assert.Throws<ArgumentException> (
				delegate { csd = new CodeSetDiff(new CodeSetFull(1,100), new CodeSetPair(1,99)); }
			);
			Assert.Throws<ArgumentException> (
				delegate { csd = new CodeSetDiff(new CodeSetFull(1,100), new CodeSetPair(0,99)); }
			);
			
			// requires !(b is CodeSetNull)
			Assert.Throws<ArgumentException> (
				delegate { csd = new CodeSetDiff(new CodeSetFull(0,100), CodeSetNull.Singleton); }
			);
			
			// requires .Count > 2
			Assert.Throws<ArgumentException> (
				delegate { csd = new CodeSetDiff(new CodeSetFull(1,4), new CodeSetPair(2,3)); }
			);
		}
	}
}
