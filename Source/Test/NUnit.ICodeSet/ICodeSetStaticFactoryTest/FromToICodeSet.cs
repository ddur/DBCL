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
	public static class From
	{
		public class CharAndParamsChar {

			[Test]
			public void Valid() {
				const char req = 'a';
				var opt_null = (char[])null;
				var opt_none = new char[0];
				var opt_one = new char[] {'b'};

				Assert.IsInstanceOf (typeof(ICodeSet), Factory.From(req));
				Assert.True (Factory.From(req).IsReduced());
				Assert.True (Factory.From(req).Count == 1);

				Assert.IsInstanceOf (typeof(ICodeSet), Factory.From(req, opt_null));
				Assert.True (Factory.From(req, opt_null).IsReduced());
				Assert.True (Factory.From(req, opt_null).Count == 1);

				Assert.IsInstanceOf (typeof(ICodeSet), Factory.From(req, opt_none));
				Assert.True (Factory.From(req, opt_none).IsReduced());
				Assert.True (Factory.From(req, opt_none).Count == 1);

				Assert.IsInstanceOf (typeof(ICodeSet), Factory.From(req, opt_one));
				Assert.True (Factory.From(req, opt_one).IsReduced());
				Assert.True (Factory.From(req, opt_one).Count == 2);
			}
			
			[Test]
			public void ValidAndDoesNotDecode() {
				const char req = 'a';
				var opt_two = new char[] {'\uD800', '\uDC07'};

				Assert.IsInstanceOf (typeof(ICodeSet), Factory.From(req, opt_two));
				Assert.True (Factory.From(req, opt_two).IsReduced());
				Assert.True (Factory.From(req, opt_two).Count == 3);
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
