// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetWrapTest
{
	[TestFixture]
	public class Constructors
	{
		[Test]
		public void From_Nothing()
		{
			var x = new CodeSetWrap();
		}
		
		[Test]
		public void From_CodeSetWrap()
		{
			var x = new CodeSetWrap (new Code[] {1, 2, 3});
			var y = new CodeSetWrap (x);
		}
		
		[Test]
		public void From_BitSetArray()
		{
			var x = new CodeSetWrap (BitSetArray.Size ());
			var y = new CodeSetWrap (BitSetArray.From (1,2,3));
		}
		
		[Test]
		public void From_IEnumerableOfCode()
		{
			var x = new CodeSetWrap (new Code[0]);
			var y = new CodeSetWrap (new Code[] {1, 2, 3});
		}

		[Test]
		public void From_CodeSetWrap_ThrowsIfNull()
		{
			Assert.Throws <ArgumentNullException> (
				delegate {
					var x = new CodeSetWrap ((CodeSetWrap)null);
				}
			);
		}
		
		[Test]
		public void From_BitSetArray_ThrowsIfNull()
		{
			Assert.Throws <ArgumentNullException> (
				delegate {
					var x = new CodeSetWrap ((BitSetArray)null);
				}
			);
		}
		
		[Test]
		public void From_BitSetArray_ThrowsIfNotCode()
		{
			Assert.Throws <ArgumentOutOfRangeException> (
				delegate {
					var x = new CodeSetWrap (BitSetArray.Size(Code.MaxCount+1, true));
				}
			);
		}
		
		[Test]
		public void From_IEnumerableOfCode_ThrowsIfNull()
		{
			Assert.Throws <ArgumentNullException> (
				delegate {
					var x = new CodeSetWrap ((IEnumerable<Code>)null);
				}
			);
		}
	}
}
