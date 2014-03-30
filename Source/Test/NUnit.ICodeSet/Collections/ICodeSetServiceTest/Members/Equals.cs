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
	public class Equals
	{
		[Test]
		public void Equals_NullOrEmpty() {
			
			ICodeSet a = null;
			ICodeSet b = null;
			Assert.IsTrue (ICodeSetService.Equals(a, b));
			Assert.IsTrue (ICodeSetService.Equals(b, a));
			Assert.IsTrue (a.SequenceEqual(b));
			Assert.IsTrue (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == true);
			Assert.IsTrue (b.QuickSetEquals(a) == true);
			
			a = CodeSetNull.Singleton;
			b = null;
			Assert.IsTrue (ICodeSetService.Equals (a, b));
			Assert.IsTrue (ICodeSetService.Equals (b, a));
			Assert.IsTrue (a.SequenceEqual(b));
			Assert.IsTrue (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == true);
			Assert.IsTrue (b.QuickSetEquals(a) == true);

			a = CodeSetNull.Singleton;
			b = new CodeSetBits();
			Assert.IsTrue (ICodeSetService.Equals (a, b));
			Assert.IsTrue (ICodeSetService.Equals (b, a));
			Assert.IsTrue (a.SequenceEqual(b));
			Assert.IsTrue (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == true);
			Assert.IsTrue (b.QuickSetEquals(a) == true);

			a = new CodeSetBits();
			b = CodeSetNull.Singleton;
			Assert.IsTrue (ICodeSetService.Equals (a, b));
			Assert.IsTrue (ICodeSetService.Equals (b, a));
			Assert.IsTrue (a.SequenceEqual(b));
			Assert.IsTrue (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == true);
			Assert.IsTrue (b.QuickSetEquals(a) == true);

			a = null;
			b = new CodeSetBits();
			Assert.IsTrue (ICodeSetService.Equals (a, b));
			Assert.IsTrue (ICodeSetService.Equals (b, a));
			Assert.IsTrue (a.SequenceEqual(b));
			Assert.IsTrue (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == true);
			Assert.IsTrue (b.QuickSetEquals(a) == true);
		}

		[Test]
		public void Equals_NullOrEmpty_with_NotEmpty() {
			
			ICodeSet a = null;
			ICodeSet b = new Code(0);
			Assert.IsFalse (ICodeSetService.Equals (a, b));
			Assert.IsFalse (ICodeSetService.Equals (b, a));
			Assert.IsFalse (a.SequenceEqual(b));
			Assert.IsFalse (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == false);
			Assert.IsTrue (b.QuickSetEquals(a) == false);

			a = new CodeSetBits();
			b = new CodeSetPair(0,100);
			Assert.IsFalse (ICodeSetService.Equals (a, b));
			Assert.IsFalse (ICodeSetService.Equals (b, a));
			Assert.IsFalse (a.SequenceEqual(b));
			Assert.IsFalse (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == false);
			Assert.IsTrue (b.QuickSetEquals(a) == false);

			a = CodeSetNull.Singleton;
			b = new CodeSetList(0,100,1000);
			Assert.IsFalse (ICodeSetService.Equals (a, b));
			Assert.IsFalse (ICodeSetService.Equals (b, a));
			Assert.IsFalse (a.SequenceEqual(b));
			Assert.IsFalse (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == false);
			Assert.IsTrue (b.QuickSetEquals(a) == false);

			a = new Code(0);
			b = null; 
			Assert.IsFalse (ICodeSetService.Equals (a, b));
			Assert.IsFalse (ICodeSetService.Equals (b, a));
			Assert.IsFalse (a.SequenceEqual(b));
			Assert.IsFalse (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == false);
			Assert.IsTrue (b.QuickSetEquals(a) == false);

			a = new CodeSetPair(0,100);
			b = CodeSetNull.Singleton;
			Assert.IsFalse (ICodeSetService.Equals (a, b));
			Assert.IsFalse (ICodeSetService.Equals (b, a));
			Assert.IsFalse (a.SequenceEqual(b));
			Assert.IsFalse (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == false);
			Assert.IsTrue (b.QuickSetEquals(a) == false);

			a = new CodeSetList(0,100,1000);
			b = new CodeSetBits();
			Assert.IsFalse (ICodeSetService.Equals (a, b));
			Assert.IsFalse (ICodeSetService.Equals (b, a));
			Assert.IsFalse (a.SequenceEqual(b));
			Assert.IsFalse (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == false);
			Assert.IsTrue (b.QuickSetEquals(a) == false);
		}

		[Test]
		public void Equals_NotEmpty () {
			
			ICodeSet a = new Code(0);
			ICodeSet b = new Code(1);
			Assert.IsFalse (ICodeSetService.Equals (a, b));
			Assert.IsFalse (ICodeSetService.Equals (b, a));
			Assert.IsFalse (a.SequenceEqual(b));
			Assert.IsFalse (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == false);
			Assert.IsTrue (b.QuickSetEquals(a) == false);

			a = new Code(100);
			b = new Code(1);
			Assert.IsFalse (ICodeSetService.Equals (a, b));
			Assert.IsFalse (ICodeSetService.Equals (b, a));
			Assert.IsFalse (a.SequenceEqual(b));
			Assert.IsFalse (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == false);
			Assert.IsTrue (b.QuickSetEquals(a) == false);

			a = new CodeSetList(0,1,2,3,4,5,6,8);
			b = new CodeSetList(0,1,2,3,4,5,6,9);
			Assert.IsFalse (ICodeSetService.Equals (a, b));
			Assert.IsFalse (ICodeSetService.Equals (b, a));
			Assert.IsFalse (a.SequenceEqual(b));
			Assert.IsFalse (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == false);
			Assert.IsTrue (b.QuickSetEquals(a) == false);

			a = new CodeSetList(0,1,2,3,4,5,6,90);
			b = new CodeSetList(0,1,2,3,4,5,6,8);
			Assert.IsFalse (ICodeSetService.Equals (a, b));
			Assert.IsFalse (ICodeSetService.Equals (b, a));
			Assert.IsFalse (a.SequenceEqual(b));
			Assert.IsFalse (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == false);
			Assert.IsTrue (b.QuickSetEquals(a) == false);

			a = new Code(0);
			b = new Code(0);
			Assert.IsTrue (ICodeSetService.Equals (a, b));
			Assert.IsTrue (ICodeSetService.Equals (b, a));
			Assert.IsTrue (a.SequenceEqual(b));
			Assert.IsTrue (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == true);
			Assert.IsTrue (b.QuickSetEquals(a) == true);

			a = new CodeSetPair(0,6);
			b = new CodeSetBits(0,6);
			Assert.IsTrue (ICodeSetService.Equals (a, b));
			Assert.IsTrue (ICodeSetService.Equals (b, a));
			Assert.IsTrue (a.SequenceEqual(b));
			Assert.IsTrue (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == true);
			Assert.IsTrue (b.QuickSetEquals(a) == true);

			a = new CodeSetList(0,1,2,3,4,5,6,9);
			b = new CodeSetBits(0,1,2,3,4,5,6,9);
			Assert.IsTrue (ICodeSetService.Equals (a, b));
			Assert.IsTrue (ICodeSetService.Equals (b, a));
			Assert.IsTrue (a.SequenceEqual(b));
			Assert.IsTrue (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == null);
			Assert.IsTrue (b.QuickSetEquals(a) == null);

			a = new CodeSetList(0,2,3,4,5,6,7,9);
			b = new CodeSetList(0,1,2,3,4,5,6,9);
			Assert.IsFalse (ICodeSetService.Equals (a, b));
			Assert.IsFalse (ICodeSetService.Equals (b, a));
			Assert.IsFalse (a.SequenceEqual(b));
			Assert.IsFalse (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == null);
			Assert.IsTrue (b.QuickSetEquals(a) == null);

			a = new CodeSetList(0,1,2,3,4,5,6,90);
			b = new CodeSetList(0,2,3,4,5,6,90);
			Assert.IsFalse (ICodeSetService.Equals (a, b));
			Assert.IsFalse (ICodeSetService.Equals (b, a));
			Assert.IsFalse (a.SequenceEqual(b));
			Assert.IsFalse (b.SequenceEqual(a));
			Assert.IsTrue (a.QuickSetEquals(b) == false);
			Assert.IsTrue (b.QuickSetEquals(a) == false);
		}
	}
}
