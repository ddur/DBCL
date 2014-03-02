// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DD.Collections.CodeSetBitsTest
{
	[TestFixture]
	public class Constructors
	{
		
		[Test]
		public void Empty()
		{
			var csb = new CodeSetBits();

		}
		
		[Test]
		public void FromCodes()
		{
			CodeSetBits csb;

			// Requires no null
			Assert.Throws <ArgumentNullException> (delegate{csb = new CodeSetBits ((IEnumerable<Code>)null);});

			csb = new CodeSetBits (new List<Code>());
			csb = new CodeSetBits (new List<Code>() {1});
			csb = new CodeSetBits (new List<Code>() {1,12,33,20});
		}
		
		[Test]
		public void FromICodeSet()
		{
			CodeSetBits csb;

			// Requires no null
			Assert.Throws <ArgumentNullException> (delegate{csb = new CodeSetBits ((ICodeSet)null);});

			ICodeSet input;
			input = ICodeSetFactory.Empty;
			csb = new CodeSetBits (input);

			input = ICodeSetFactory.From(new List<Code>() {1,12,33,20});
			csb = new CodeSetBits (input);
			var clone = new CodeSetBits (csb);
		}
		
		[Test]
		public void FromBitSetArray()
		{
			CodeSetBits csb;

			// Requires no null
			Assert.Throws <ArgumentNullException> (delegate{csb = new CodeSetBits ((BitSetArray)null);});

			csb = new CodeSetBits (new BitSetArray());
			csb = new CodeSetBits (new BitSetArray() {33});
			csb = new CodeSetBits (new BitSetArray() {1,12,33});
		}
		
		[Test]
		public void FromCompact()
		{
			CodeSetBits csb;

			// Requires no null
			Assert.Throws <ArgumentNullException> (delegate{csb = new CodeSetBits ((BitSetArray)null, 0);});

			// Requires compact BitSetArray => at least one member
			Assert.Throws <ArgumentException> (delegate{csb = new CodeSetBits (new BitSetArray(), 0);});
			Assert.Throws <ArgumentException> (delegate{csb = new CodeSetBits (new BitSetArray() {0,1}, -1);});
			Assert.Throws <ArgumentException> (delegate{csb = new CodeSetBits (new BitSetArray() {0,1,12,33}, -1);});
			Assert.Throws <ArgumentException> (delegate{csb = new CodeSetBits (new BitSetArray() {0,1,12,Code.MaxCount}, 0);});
			Assert.Throws <ArgumentException> (delegate{csb = new CodeSetBits (new BitSetArray() {0,1,12,Code.MaxValue}, 1);});

			csb = new CodeSetBits (new BitSetArray() {0},Code.MaxValue);
			csb = new CodeSetBits (new BitSetArray() {0,1});
			csb = new CodeSetBits (new BitSetArray() {0,1,12,33});
			csb = new CodeSetBits (new BitSetArray() {0,1,12,33,Code.MaxValue});
		}
		
		
	}
}
