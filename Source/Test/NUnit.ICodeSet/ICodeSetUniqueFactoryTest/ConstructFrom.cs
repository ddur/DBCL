/*
 * Created by SharpDevelop.
 * User: ddur
 * Date: 14.1.2016.
 * Time: 23:05
 * 
 */
using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using DD.Collections.ICodeSet;

namespace DD.Collections.ICodeSet.ICodeSetUniqueFactoryTest
{
    [TestFixture]
    public static class ConstructFrom
    {
        public class CharAndParamsChar {

            readonly Distinct distinct = new Distinct();

            [Test]
            public void Valid() {
                const char req = 'a';
                var opt_null = (char[])null;
                var opt_none = new char[0];
                var opt_one = new char[] {'b'};

                var result = distinct.From(req);
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced());
                Assert.True (result.Count == 1);

                result = distinct.From(req, opt_null);
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced());
                Assert.True (result.Count == 1);

                result = distinct.From(req, opt_none);
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced());
                Assert.True (result.Count == 1);

                result = distinct.From(req, opt_one);
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced());
                Assert.True (result.Count == 2);
            }
            
            [Test]
            public void ValidAndDoesNotDecode() {
                const char req = 'a';
                var opt = new char[] {'\uD800', '\uDC07'};

                var result = distinct.From(req, opt);
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced());
                Assert.True (result.Count == 3);
            }
        }

        public class ParamsCode {

            readonly Distinct distinct = new Distinct();

            [Test]
            public void Valid() {
                var result = distinct.From(2);
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced());
                Assert.True (result.Count == 1);

                result = distinct.From(2, null);
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced());
                Assert.True (result.Count == 1);

                result = distinct.From(2, new Code[0]);
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced());
                Assert.True (result.Count == 1);

                result = distinct.From(2, new Code[] {3});
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced());
                Assert.True (result.Count == 2);
            }
            
            [Test]
            public void ValidAndDoesNotDecode() {
                var result = distinct.From(2, new Code[] {0xD800, 0xDC07});
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced());
                Assert.True (result.Count == 3);
            }
        }

        public class StringUtf16 {

            readonly Distinct distinct = new Distinct();

            [Test]
            public void Null() {
                const string Utf16 = null;
                Assert.True (ReferenceEquals (CodeSetNone.Singleton, distinct.From(Utf16)));
            }

            [Test]
            public void Empty() {
                const string Utf16 = "";
                Assert.True (ReferenceEquals (CodeSetNone.Singleton, distinct.From(Utf16)));
            }

            [Test]
            public void Invalid_Throws() {
                const string Utf16 = "abc\uDC00\uD800def";
                Assert.Throws ( typeof(ArgumentException),
                        delegate {
                            distinct.From(Utf16);
                        });
            }

            [Test]
            public void ValidAndDoesDecode() {
                const string Utf16 = "abc\uFFFF\u0000\uD801\uDC01def";
                Assert.IsInstanceOf (typeof(ICodeSet), distinct.From(Utf16));
                Assert.True (distinct.From(Utf16).IsReduced());
                Assert.True (distinct.From(Utf16).Count == 9);
            }
        }

        public class IEnumerableOfChar {

            [Test]
            public void Null() {
                var distinct = new Distinct();
                Assert.True (ReferenceEquals (CodeSetNone.Singleton, distinct.From((List<char>)null)));

                Assert.False (distinct.Contains(CodeSetNone.Singleton));
                Assert.True (distinct.Count == 0);
            }

            [Test]
            public void Empty() {
                var distinct = new Distinct();
                Assert.True (ReferenceEquals (CodeSetNone.Singleton, distinct.From(new List<char>())));

                Assert.False (distinct.Contains(CodeSetNone.Singleton));
                Assert.True (distinct.Count == 0);
            }
        }


        public class IEnumerableOfCode {

            [Test]
            public void Null() {
                var distinct = new Distinct();
                Assert.True (ReferenceEquals (CodeSetNone.Singleton, distinct.From((List<Code>)null)));

                Assert.False (distinct.Contains(CodeSetNone.Singleton));
                Assert.True (distinct.Count == 0);
            }

            [Test]
            public void Empty() {
                var distinct = new Distinct();
                Assert.True (ReferenceEquals (CodeSetNone.Singleton, distinct.From(new List<Code>())));

                Assert.False (distinct.Contains(CodeSetNone.Singleton));
                Assert.True (distinct.Count == 0);
            }
        }

        public class ICodeSetArgument {

            [Test]
            public void Null() {
                var distinct = new Distinct();
                Assert.True (ReferenceEquals (CodeSetNone.Singleton, distinct.From((ICodeSet)null)));

                Assert.False (distinct.Contains(CodeSetNone.Singleton));
                Assert.True (distinct.Count == 0);
            }

            [Test]
            public void Empty() {
                var distinct = new Distinct();
                Assert.True (ReferenceEquals (CodeSetNone.Singleton, distinct.From(CodeSetWrap.From ())));
                Assert.True (ReferenceEquals (CodeSetNone.Singleton, distinct.From(CodeSetNone.Singleton)));

                Assert.False (distinct.Contains(CodeSetNone.Singleton));
                Assert.True (distinct.Count == 0);
            }

            [Test]
            public void Valid() {

                var distinct = new Distinct();

                var result = distinct.From(2, new Code[] {0xD800, 0xDC07});
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced());
                Assert.True (result.Count == 3);

                Assert.True (distinct.Contains (result));
                Assert.True (distinct.Count == 1);

                var result2 = Factory.From(2, new Code[] {0xD800, 0xDC07});
                var result3 = distinct.From (result2);

                Assert.True (distinct.Contains (result));
                Assert.True (distinct.Contains (result2));
                Assert.True (distinct.Contains (result3));
                Assert.True (distinct.Count == 1);

                Assert.False (ReferenceEquals (result, result2));
                Assert.True (ReferenceEquals (result, result3));
            }
        }

        public class BitSetArrayArgument {

            [Test]
            public void Null() {
                var distinct = new Distinct();
                Assert.True (ReferenceEquals (CodeSetNone.Singleton, distinct.From((BitSetArray)null)));

                Assert.False (distinct.Contains(CodeSetNone.Singleton));
                Assert.True (distinct.Count == 0);
            }

            [Test]
            public void Empty() {
                var distinct = new Distinct();
                Assert.True (ReferenceEquals (CodeSetNone.Singleton, distinct.From(BitSetArray.Empty ())));

                Assert.False (distinct.Contains(CodeSetNone.Singleton));
                Assert.True (distinct.Count == 0);
            }

            [Test]
            public void Valid() {

                var distinct = new Distinct();

                var result = distinct.From(BitSetArray.From (2, 3, 4, 5));
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced());
                Assert.True (result.Count == 4);

                Assert.True (distinct.Contains (result));
                Assert.True (distinct.Count == 1);

                var result2 = (BitSetArray.From (2, 3, 4, 5)).ToICodeSet();
                var result3 = distinct.From (result2);

                Assert.True (distinct.Contains (result));
                Assert.True (distinct.Contains (result2));
                Assert.True (distinct.Contains (result3));
                Assert.True (distinct.Count == 1);

                Assert.False (ReferenceEquals (result, result2));
                Assert.True (ReferenceEquals (result, result3));
                
                List<ICodeSet> collection = distinct.Collection.ToList();
                Assert.True (collection.Count == 1);
            }
        }
    }
}
