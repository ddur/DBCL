﻿// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using DD.Enumerables;
using NUnit.Framework;

namespace DD.Collections.CodeSetPageTest
{
	[TestFixture]
	public class Constructors
	{

		[Test]
		public void FromBitSetArray()
		{
			CodeSetPage csp;

			// requires no null
			Assert.Throws <ArgumentNullException> (delegate{csp = new CodeSetPage ((BitSetArray)null);});

			// requires at least 3 members (> ICodeSetService.PairCount)
			Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new BitSetArray());});
			Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new BitSetArray() {21});});
			Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new BitSetArray() {12,5});});

			// requires all codes within same unicode plane
			Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new BitSetArray() {12,66000});});

            // does not except full-range of codes
            Assert.Throws<ArgumentException> (delegate{csp = new CodeSetPage(new BitSetArray(10, true));});

			csp = new CodeSetPage (new BitSetArray() {1,12,33});
		}
		
		[Test]
		public void FromCodes()
		{
			CodeSetPage csp;

			// requires no null
			Assert.Throws <ArgumentNullException> (delegate{csp = new CodeSetPage ((IEnumerable<Code>)null);});

			// requires at least 3 members (> ICodeSetService.PairCount)
			Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new Code[0]);});
			Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new List<Code>() {21});});
			Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new List<Code>() {1,25});});

			// requires all codes within same unicode plane
			Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new List<Code>() {12,25,66,66000});});

            // does not except full-range of codes
            Assert.Throws<ArgumentException> (delegate{csp = new CodeSetPage(1.To(80).Select(item => (Code)item));});
            Assert.Throws<ArgumentException> (delegate{csp = new CodeSetPage((Code.MaxValue-10).To(Code.MaxValue).Select(item => (Code)item));});

			csp = new CodeSetPage (new List<Code>() {0,1,12,33,65535});

		}
		
		[Test]
		public void FromICodeSet()
		{
			CodeSetPage csp;

			// requires no null
			Assert.Throws <ArgumentNullException> (delegate{csp = new CodeSetPage ((ICodeSet)null);});

			// requires at least 3 members (> ICodeSetService.PairCount)
			Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new CodeSetBits(new Code[0]));});
			Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new CodeSetBits(new List<Code>() {21}));});
			Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new CodeSetBits(new List<Code>() {1,25}));});

			// requires all codes within same unicode plane
			Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new CodeSetBits(new List<Code>() {12,66000}));});

			csp = new CodeSetPage (new CodeSetBits(new BitSetArray() {0,1,12,33,65535}));
			CodeSetPage clone = new CodeSetPage (csp);
		}
		
	}
}
