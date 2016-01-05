/*
 * Created by SharpDevelop.
 * User: ddur
 * Date: 3.1.2016.
 * Time: 12:59
 * 
 */
using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetStaticFactoryTest
{
	public static class ToICodeSet {

		public class FromIEnumerableOfChar {

			[Test]
			public void Null () {
				const char[] chars = null;
				Assert.Throws ( typeof(ArgumentNullException),
				        delegate {
				            chars.ToICodeSet();
				        });
			}
			
			[Test]
			public void Empty () {
				var chars = new char[0];
				Assert.Throws ( typeof(ArgumentEmptyException),
				        delegate {
				            chars.ToICodeSet();
				        });
			}
			
			[Test]
			public void Valid () {
				var chars = new char[] {'a', '\uD800', '\uDC07'};

				var result = chars.ToICodeSet();
				Assert.IsInstanceOf (typeof(ICodeSet), result);
				Assert.True (result.IsReduced());
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
	public static class From
	{
		public class CharAndParamsChar {

			[Test]
			public void Valid() {
				const char req = 'a';
				var opt_null = (char[])null;
				var opt_none = new char[0];
				var opt_one = new char[] {'b'};

				var result = Factory.From(req);
				Assert.IsInstanceOf (typeof(ICodeSet), result);
				Assert.True (result.IsReduced());
				Assert.True (result.Count == 1);

				result = Factory.From(req, opt_null);
				Assert.IsInstanceOf (typeof(ICodeSet), result);
				Assert.True (result.IsReduced());
				Assert.True (result.Count == 1);

				result = Factory.From(req, opt_none);
				Assert.IsInstanceOf (typeof(ICodeSet), result);
				Assert.True (result.IsReduced());
				Assert.True (result.Count == 1);

				result = Factory.From(req, opt_one);
				Assert.IsInstanceOf (typeof(ICodeSet), result);
				Assert.True (result.IsReduced());
				Assert.True (result.Count == 2);
			}
			
			[Test]
			public void ValidAndDoesNotDecode() {
				const char req = 'a';
				var opt = new char[] {'\uD800', '\uDC07'};

				var result = Factory.From(req, opt);
				Assert.IsInstanceOf (typeof(ICodeSet), result);
				Assert.True (result.IsReduced());
				Assert.True (result.Count == 3);
			}
		}

		public class ParamsCode {

			[Test]
			public void Valid() {
				var result = Factory.From(2);
				Assert.IsInstanceOf (typeof(ICodeSet), result);
				Assert.True (result.IsReduced());
				Assert.True (result.Count == 1);

				result = Factory.From(2, null);
				Assert.IsInstanceOf (typeof(ICodeSet), result);
				Assert.True (result.IsReduced());
				Assert.True (result.Count == 1);

				result = Factory.From(2, new Code[0]);
				Assert.IsInstanceOf (typeof(ICodeSet), result);
				Assert.True (result.IsReduced());
				Assert.True (result.Count == 1);

				result = Factory.From(2, new Code[] {3});
				Assert.IsInstanceOf (typeof(ICodeSet), result);
				Assert.True (result.IsReduced());
				Assert.True (result.Count == 2);
			}
			
			[Test]
			public void ValidAndDoesNotDecode() {
				var result = Factory.From(2, new Code[] {0xD800, 0xDC07});
				Assert.IsInstanceOf (typeof(ICodeSet), result);
				Assert.True (result.IsReduced());
				Assert.True (result.Count == 3);
			}
		}

		public class StringUtf16 {

			[Test]
			public void Null() {
				const string Utf16 = null;
				Assert.Throws ( typeof(ArgumentNullException),
				        delegate {
						    Factory.From(Utf16);
				        });
			}

			[Test]
			public void Empty() {
				const string Utf16 = "";
				Assert.Throws ( typeof(ArgumentEmptyException),
				        delegate {
						    Factory.From(Utf16);
				        });
			}

			[Test]
			public void Invalid() {
				const string Utf16 = "abc\uDC00\uD800def";
				Assert.Throws ( typeof(ArgumentException),
				        delegate {
						    Factory.From(Utf16);
				        });
			}

			[Test]
			public void ValidAndDoesDecode() {
				const string Utf16 = "abc\uFFFF\u0000\uD801\uDC01def";
				Assert.IsInstanceOf (typeof(ICodeSet), Factory.From(Utf16));
				Assert.True (Factory.From(Utf16).IsReduced());
				Assert.True (Factory.From(Utf16).Count == 9);
			}
			
		}
	}
}
