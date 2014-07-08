// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetWrapTest
{
	[TestFixture]
	public class Members
	{
		[Test]
		public void Indexer()
		{
			var r = new Random();
			Code C = r.Next(Code.MinValue, Code.MaxValue);
			Code D = C;
			while (D == C) {
				D = r.Next(Code.MinValue, Code.MaxValue);
			}

			var csw = CodeSetWrap.From();
			Assert.False ( csw[C] );
			Assert.False ( csw[D] );
			
			csw  = CodeSetWrap.From(BitSetArray.Size());
			Assert.False ( csw[C] );
			Assert.False ( csw[D] );
			
			csw  = CodeSetWrap.From(BitSetArray.From (C));
			Assert.True ( csw[C] );
			Assert.False ( csw[D] );
			
			csw  = CodeSetWrap.From(BitSetArray.From (C,D));
			Assert.True ( csw[C] );
			Assert.True ( csw[D] );
			
		}
		
		[Test]
		public void Properties() {

			CodeSetWrap csw;
			Code C;

			csw = CodeSetWrap.From();
			Assert.Throws<InvalidOperationException> (delegate{C = csw.First;});
			Assert.Throws<InvalidOperationException> (delegate{C = csw.Last;});
			
			csw = CodeSetWrap.From (new List<Code>() {12});
			Assert.True (csw.Count == 1);
			Assert.True (csw.Length == 1);
			Assert.True (csw.First == 12);
			Assert.True (csw.Last == 12);
			
			csw = CodeSetWrap.From (new List<Code>() {1,12,33,20});
			Assert.True (csw.Count == 4);
			Assert.True (csw.Length == 33);
			Assert.True (csw.First == 1);
			Assert.True (csw.Last == 33);
			
		}
		
		[Test]
		public void ToBitSetArray() {
			CodeSetWrap csw;
			BitSetArray bsa;

			csw = CodeSetWrap.From();
			bsa = csw.ToBitSetArray();
			Assert.True (bsa.Count == 0);
			Assert.True (bsa.Length == 0);
			Assert.True (bsa.First == null);
			Assert.True (bsa.Last == null);

			csw = CodeSetWrap.From (new List<Code>() {12});
			bsa = csw.ToBitSetArray();
			Assert.True (bsa.Count == 1);
			Assert.True (bsa.First == 12);
			Assert.True (bsa.Last == 12);
			Assert.True (bsa[12]);
			Assert.True (bsa.SequenceEqual(csw.ToValues()));

			csw = CodeSetWrap.From (new List<Code>() {1,12,33,20});
			bsa = csw.ToBitSetArray();
			Assert.True (bsa.Count == 4);
			Assert.True (bsa.First == 1);
			Assert.True (bsa.Last == 33);
			Assert.True (bsa.SequenceEqual(csw.ToValues()));
		}

	}
}
