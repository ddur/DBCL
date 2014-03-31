﻿// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSetServiceTest.Members
{
	[TestFixture]
	public class ToCompact
	{
		[Test]
		public void FromNullOrEmpty()
		{
			BitSetArray compact;
			ICodeSet a = null;
			
			compact = a.ToCompact();
			Assert.IsTrue (compact.Count == 0);
			Assert.IsTrue (compact.IsCompact());

			a = new CodeSetBits();
			compact = a.ToCompact();
			Assert.IsTrue (compact.Count == 0);
			Assert.IsTrue (compact.IsCompact());

			a = CodeSetNull.Singleton;
			compact = a.ToCompact();
			Assert.IsTrue (compact.Count == 0);
			Assert.IsTrue (compact.IsCompact());
		}

		[Test]
		public void FromCodeSetBits()
		{
			BitSetArray compact;
			ICodeSet a = new Code(20);
			
			compact = a.ToCompact();
			Assert.IsTrue (compact.Count == 1);
			Assert.IsTrue (compact.IsCompact());

			a = new CodeSetPair(22,65);
			compact = a.ToCompact();
			Assert.IsTrue (compact.Count == 2);
			Assert.IsTrue (compact.IsCompact());

			a = new CodeSetList(22,65,77);
			compact = a.ToCompact();
			Assert.IsTrue (compact.Count == 3);
			Assert.IsTrue (compact.IsCompact());

			a = new CodeSetFull(20,29);
			compact = a.ToCompact();
			Assert.IsTrue (compact.Count == 10);
			Assert.IsTrue (compact.IsCompact());
		}
	}
}
