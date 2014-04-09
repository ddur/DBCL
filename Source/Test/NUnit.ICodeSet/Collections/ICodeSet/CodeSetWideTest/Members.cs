// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetWideTest
{
	[TestFixture]
	public class Members
	{
		static List<Code> list1 = new List<Code>() {0,65536,1114111};
		static List<Code> list2 = new List<Code>() {32768,65536,65537};

		CodeSetWide csw1 = new CodeSetWide(list1);
		CodeSetWide csw2 = new CodeSetWide(list2);

		[Test] public void AsEnumerable() {
			
			Assert.True (csw1.SequenceEqual(list1));
			Assert.True (csw2.SequenceEqual(list2));
		}

		[Test] public void FirstLast() {

			Assert.True (csw1.First.Value == 0);
			Assert.True (csw1.Last.Value == Code.MaxValue);

			Assert.True (csw2.First.Value == 32768);
			Assert.True (csw2.Last.Value == 65537);
		}
		
		[Test]
		public void Indexer()
		{
			Assert.False (csw1[-1]);
			Assert.False (csw1[int.MinValue]);

			Assert.True (csw1[0]);
			Assert.False (csw1[1]);

			Assert.True (csw1[(Code)0]);
			Assert.False (csw1[(Code)1]);

			Assert.False (csw1[65535]);
			Assert.True (csw1[65536]);
			Assert.False (csw1[65537]);

			Assert.False (csw1[(Code)65535]);
			Assert.True (csw1[(Code)65536]);
			Assert.False (csw1[(Code)65537]);

			Assert.False (csw1[Code.MaxValue-1]);
			Assert.True (csw1[Code.MaxValue]);
			
			Assert.False (csw1[(Code)Code.MaxValue-1]);
			Assert.True (csw1[(Code)Code.MaxValue]);


			Assert.False (csw2[65535]);
			Assert.True (csw2[65536]);
			Assert.True (csw2[65537]);

			Assert.False (csw2[(Code)65535]);
			Assert.True (csw2[(Code)65536]);
			Assert.True (csw2[(Code)65537]);

			Assert.False (csw2[Code.MaxValue-1]);
			Assert.False (csw2[Code.MaxValue]);
			
			Assert.False (csw2[(Code)Code.MaxValue-1]);
			Assert.False (csw2[(Code)Code.MaxValue]);

		}
		
	}
}
