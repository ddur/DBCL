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
	public class CompareTo
	{
		[Test]
		public void CompareTo_NullOrEmpty()
		{
			ICodeSet a = null;
			ICodeSet b = null;
			Assert.True (ICodeSetService.CompareTo (a, b) == 0);
			
			a = CodeSetNull.Singleton;
			b = null;
			Assert.True (ICodeSetService.CompareTo (a, b) == 0);

			a = CodeSetNull.Singleton;
			b = new CodeSetBits();
			Assert.True (ICodeSetService.CompareTo (a, b) == 0);

			a = new CodeSetBits();
			b = CodeSetNull.Singleton;
			Assert.True (ICodeSetService.CompareTo (a, b) == 0);

			a = null;
			b = new CodeSetBits();
			Assert.True (ICodeSetService.CompareTo (a, b) == 0);
		}

		[Test]
		public void CompareTo_NullOrEmpty_with_NotEmpty() {
			
			ICodeSet a = null;
			ICodeSet b = new Code(0);
			Assert.True (ICodeSetService.CompareTo (a, b) == -1);

			a = new CodeSetBits();
			b = new CodeSetPair(0,100);
			Assert.True (ICodeSetService.CompareTo (a, b) == -1);

			a = CodeSetNull.Singleton;
			b = new CodeSetList(0,100,1000);
			Assert.True (ICodeSetService.CompareTo (a, b) == -1);

			a = new Code(0);
			b = null; 
			Assert.True (ICodeSetService.CompareTo (a, b) == 1);

			a = new CodeSetPair(0,100);
			b = CodeSetNull.Singleton;
			Assert.True (ICodeSetService.CompareTo (a, b) == 1);

			a = new CodeSetList(0,100,1000);
			b = new CodeSetBits();
			Assert.True (ICodeSetService.CompareTo (a, b) == 1);

		}

		[Test]
		public void CompareTo_NotEmpty_MSB() {

			ICodeSet a = new Code(0);
			ICodeSet b = new Code(1);
			Assert.True (ICodeSetService.CompareTo (a, b) == -1);

			a = new Code(100);
			b = new Code(1);
			Assert.True (ICodeSetService.CompareTo (a, b) == 1);

			a = new CodeSetList(0,1,2,3,4,5,6,8);
			b = new CodeSetList(0,1,2,3,4,5,6,9);
			Assert.True (ICodeSetService.CompareTo (a, b) == -1);

			a = new CodeSetList(0,1,2,3,4,5,6,90);
			b = new CodeSetList(0,1,2,3,4,5,6,8);
			Assert.True (ICodeSetService.CompareTo (a, b) == 1);
		}			

		[Test]
		public void CompareTo_NotEmpty_BIT() {

			ICodeSet a = new Code(0);
			ICodeSet b = new Code(0);
			Assert.True (ICodeSetService.CompareTo (a, b) == 0);

			a = new CodeSetList(0,1,2,3,4,5,6,9);
			b = new CodeSetList(0,1,2,3,4,5,6,9);
			Assert.True (ICodeSetService.CompareTo (a, b) == 0);

			a = new CodeSetList(0,2,3,4,5,6,9);
			b = new CodeSetList(0,1,2,3,4,5,6,9);
			Assert.True (ICodeSetService.CompareTo (a, b) == -1);

			a = new CodeSetList(0,1,2,3,4,5,6,90);
			b = new CodeSetList(0,2,3,4,5,6,90);
			Assert.True (ICodeSetService.CompareTo (a, b) == 1);
		}			
	}
}
