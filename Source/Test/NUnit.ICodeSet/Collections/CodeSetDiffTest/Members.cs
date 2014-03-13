// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Linq;
using NUnit.Framework;

namespace DD.Collections.CodeSetDiffTest
{
	[TestFixture]
	public class Members
	{
		static CodeSetDiff csd1 = new CodeSetDiff(new CodeSetFull(Code.MinValue, Code.MaxValue), new Code(Code.MaxValue/2));
		static CodeSetDiff csd2 = new CodeSetDiff(new CodeSetFull(90, 203), new CodeSetFull(100, 200));
		
		[Test]
		public void AsEnumerable()
		{
			Assert.True ( csd2.SequenceEqual(new Code[] {90,91,92,93,94,95,96,97,98,99,201,202,203}));
		}

		[Test]
		public void FirstLast()
		{
			Assert.True (csd1.First.Value == Code.MinValue);
			Assert.True (csd1.Last.Value == Code.MaxValue);
			
			Assert.True (csd2.First.Value == 90);
			Assert.True (csd2.Last.Value == 203);
			
		}
		
		[Test]
		public void Indexer()
		{
			Code[] codes = new Code[] {90,91,92,93,94,95,96,97,98,99,201,202,203};
			foreach (var code in codes) {
				Assert.True (csd2[code]);
			}
			codes = new Code[] {0,100,101,102,103,104,105,106,107,108,109,110,120,130,140,150,160,170,180,190,199,200};
			foreach (var code in codes) {
				Assert.False (csd2[code]);
			}
		}
	}
}
