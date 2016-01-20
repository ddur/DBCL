// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetStaticFactoryTest
{
	public static class ConstructToICodeSet {

		public class FromIEnumerableOfChar {

			[Test]
			public void Null () {
				const char[] chars = null;
                Assert.True (ReferenceEquals (CodeSetNone.Singleton, chars.ToICodeSet()));
			}
			
			[Test]
			public void Empty () {
				var chars = new char[0];
                Assert.True (ReferenceEquals (CodeSetNone.Singleton, chars.ToICodeSet()));
			}
			
			[Test]
			public void Valid () {
				var chars = new char[] {'a', '\uD800', '\uDC07'};

				var result = chars.ToICodeSet();
				Assert.IsInstanceOf (typeof(ICodeSet), result);
				Assert.True (result.IsReduced);
				Assert.True (result.Count == 3);
			}
			
		}

		public class FromIEnumerableOfCode {

			[Test]
			public void Null () {
				
			}
			
			[Test]
			public void Empty () {
				
			}
			
			[Test]
			public void Valid () {
				
			}
			
		}

		public class FromBitSetArray {

			[Test]
			public void Null () {
				
			}
			
			[Test]
			public void Empty () {
				
			}
			
			[Test]
			public void Valid () {
				
			}
			
		}
	}
}
