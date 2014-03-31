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
	public class ToBitSetArray
	{
		[Test]
		public void NullOrEmpty()
		{
			BitSetArray output;

			ICodeSet a = null;
			output = a.ToBitSetArray();

			Assert.IsTrue (output.Count == 0);
			Assert.IsTrue (output.Length == 0);

			a = new CodeSetBits();
			output = a.ToBitSetArray();

			Assert.IsTrue (output.Count == 0);
			Assert.IsTrue (output.Length == 0);

			a = CodeSetNull.Singleton;
			output = a.ToBitSetArray();

			Assert.IsTrue (output.Count == 0);
			Assert.IsTrue (output.Length == 0);
		}

		[Test]
		public void NotEmpty()
		{
			BitSetArray output;

			ICodeSet a = new Code (11);
			output = a.ToBitSetArray();

			Assert.IsTrue (output.Count == a.Count);
			Assert.IsTrue (output.Length == a.Last+1);
			Assert.IsTrue (output.First == a.First);
			Assert.IsTrue (output.Last == a.Last);

			a = new CodeSetPair(25,90);
			output = a.ToBitSetArray();

			Assert.IsTrue (output.Count == a.Count);
			Assert.IsTrue (output.Length == a.Last+1);
			Assert.IsTrue (output.First == a.First);
			Assert.IsTrue (output.Last == a.Last);

			a = new CodeSetList(11,25,90,130);
			output = a.ToBitSetArray();

			Assert.IsTrue (output.Count == a.Count);
			Assert.IsTrue (output.Length == a.Last+1);
			Assert.IsTrue (output.First == a.First);
			Assert.IsTrue (output.Last == a.Last);

			a = new CodeSetBits(11,25,90,130,132,132,133,134,150,151,152,153,154,155,156,157,158,159,160);
			output = a.ToBitSetArray();

			Assert.IsTrue (output.Count == a.Count);
			Assert.IsTrue (output.Length == a.Last+1);
			Assert.IsTrue (output.First == a.First);
			Assert.IsTrue (output.Last == a.Last);
		}
	}
}
