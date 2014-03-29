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
	public class Equals
	{
		[Test]
		public void Equals_NullOrEmpty() {
			
			ICodeSet a = null;
			ICodeSet b = null;
			Assert.True (ICodeSetService.Equals(a, b));
			Assert.True (a.SequenceEqual(b));
			
			a = CodeSetNull.Singleton;
			b = null;
			Assert.True (ICodeSetService.Equals (a, b));
			Assert.True (a.SequenceEqual(b));

			a = CodeSetNull.Singleton;
			b = new CodeSetBits();
			Assert.True (ICodeSetService.Equals (a, b));
			Assert.True (a.SequenceEqual(b));

			a = new CodeSetBits();
			b = CodeSetNull.Singleton;
			Assert.True (ICodeSetService.Equals (a, b));
			Assert.True (a.SequenceEqual(b));

			a = null;
			b = new CodeSetBits();
			Assert.True (ICodeSetService.Equals (a, b));
			Assert.True (a.SequenceEqual(b));
		}

		[Test]
		public void Equals_NullOrEmpty_with_NotEmpty() {
			
			ICodeSet a = null;
			ICodeSet b = new Code(0);
			Assert.False (ICodeSetService.Equals (a, b));
			Assert.False (a.SequenceEqual(b));

			a = new CodeSetBits();
			b = new CodeSetPair(0,100);
			Assert.False (ICodeSetService.Equals (a, b));
			Assert.False (a.SequenceEqual(b));

			a = CodeSetNull.Singleton;
			b = new CodeSetList(0,100,1000);
			Assert.False (ICodeSetService.Equals (a, b));
			Assert.False (a.SequenceEqual(b));

			a = new Code(0);
			b = null; 
			Assert.False (ICodeSetService.Equals (a, b));
			Assert.False (a.SequenceEqual(b));

			a = new CodeSetPair(0,100);
			b = CodeSetNull.Singleton;
			Assert.False (ICodeSetService.Equals (a, b));
			Assert.False (a.SequenceEqual(b));

			a = new CodeSetList(0,100,1000);
			b = new CodeSetBits();
			Assert.False (ICodeSetService.Equals (a, b));
			Assert.False (a.SequenceEqual(b));
		}

		[Test]
		public void Equals_NotEmpty () {
			
			ICodeSet a = new Code(0);
			ICodeSet b = new Code(1);
			Assert.False (ICodeSetService.Equals (a, b));
			Assert.False (a.SequenceEqual(b));

			a = new Code(100);
			b = new Code(1);
			Assert.False (ICodeSetService.Equals (a, b));
			Assert.False (a.SequenceEqual(b));

			a = new CodeSetList(0,1,2,3,4,5,6,8);
			b = new CodeSetList(0,1,2,3,4,5,6,9);
			Assert.False (ICodeSetService.Equals (a, b));
			Assert.False (a.SequenceEqual(b));

			a = new CodeSetList(0,1,2,3,4,5,6,90);
			b = new CodeSetList(0,1,2,3,4,5,6,8);
			Assert.False (ICodeSetService.Equals (a, b));
			Assert.False (a.SequenceEqual(b));

			a = new Code(0);
			b = new Code(0);
			Assert.True (ICodeSetService.Equals (a, b));
			Assert.True (a.SequenceEqual(b));

			a = new CodeSetList(0,1,2,3,4,5,6,9);
			b = new CodeSetBits(new Code[] {0,1,2,3,4,5,6,9});
			Assert.True (ICodeSetService.Equals (a, b));
			Assert.True (a.SequenceEqual(b));

			a = new CodeSetList(0,2,3,4,5,6,7,9);
			b = new CodeSetList(0,1,2,3,4,5,6,9);
			Assert.False (ICodeSetService.Equals (a, b));
			Assert.False (a.SequenceEqual(b));

			a = new CodeSetList(0,1,2,3,4,5,6,90);
			b = new CodeSetList(0,2,3,4,5,6,90);
			Assert.False (ICodeSetService.Equals (a, b));
			Assert.False (a.SequenceEqual(b));
		}
	}
}
