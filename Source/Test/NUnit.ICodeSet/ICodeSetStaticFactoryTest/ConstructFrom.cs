// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;

using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetStaticFactoryTest
{
    public static class ConstructFrom
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
                Assert.True (result.IsReduced);
                Assert.True (result.Count == 1);

                result = Factory.From(req, opt_null);
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced);
                Assert.True (result.Count == 1);

                result = Factory.From(req, opt_none);
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced);
                Assert.True (result.Count == 1);

                result = Factory.From(req, opt_one);
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced);
                Assert.True (result.Count == 2);
            }
            
            [Test]
            public void ValidAndDoesNotDecode() {
                const char req = 'a';
                var opt = new char[] {'\uD800', '\uDC07'};

                var result = Factory.From(req, opt);
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced);
                Assert.True (result.Count == 3);
            }
        }

        public class ParamsCode {

            [Test]
            public void Valid() {
                var result = Factory.From(2);
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced);
                Assert.True (result.Count == 1);

                result = Factory.From(2, null);
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced);
                Assert.True (result.Count == 1);

                result = Factory.From(2, new Code[0]);
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced);
                Assert.True (result.Count == 1);

                result = Factory.From(2, new Code[] {3});
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced);
                Assert.True (result.Count == 2);
            }
            
            [Test]
            public void ValidAndDoesNotDecode() {
                var result = Factory.From(2, new Code[] {0xD800, 0xDC07});
                Assert.IsInstanceOf (typeof(ICodeSet), result);
                Assert.True (result.IsReduced);
                Assert.True (result.Count == 3);
            }
        }

        public class StringUtf16 {

            [Test]
            public void Null() {
                const string Utf16 = null;
                Assert.True (ReferenceEquals (CodeSetNone.Singleton, Factory.From(Utf16)));
            }

            [Test]
            public void Empty() {
                const string Utf16 = "";
                Assert.True (ReferenceEquals (CodeSetNone.Singleton, Factory.From(Utf16)));
            }

            [Test]
            public void Invalid_Throws() {
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
                Assert.True (Factory.From(Utf16).IsReduced);
                Assert.True (Factory.From(Utf16).Count == 9);
            }
        }

        public class Range {

            [Test]
            public void OneMember () {
                // arrange
                int start;
                var random = new Random ();

                // act
                start = random.Next (Code.MinValue, Code.MaxValue);

                // assert
                Assert.True (((Code)start).Range(start) is Code);

                // act
                start = Code.MinValue;

                // assert
                Assert.True (((Code)start).Range(start) is Code);

                // act
                start = Code.MaxValue;

                // assert
                Assert.True (((Code)start).Range(start) is Code);
            }

            [Test]
            public void TwoMembers () {
                // arrange
                int start;
                int final;
                var random = new Random ();

                // act
                start = random.Next (Code.MinValue, Code.MaxValue-1);
                final = start + 1;

                // assert
                Assert.True (((Code)start).Range(final) is CodeSetPair);

                // act
                start = Code.MinValue;
                final = start + 1;

                // assert
                Assert.True (((Code)start).Range(final) is CodeSetPair);

                // act
                final = Code.MaxValue;
                start = final - 1;

                // assert
                Assert.True (((Code)start).Range(final) is CodeSetPair);
            }

            [Test]
            public void FullRange () {
                // arrange
                int start;
                int final;
                var random = new Random ();

                // act
                start = random.Next (Code.MinValue, (Code.MaxValue/2)-1);
                final = random.Next ((Code.MaxValue/2)+1, Code.MaxValue);

                // assert
                Assert.True (((Code)start).Range(final) is CodeSetFull);
            }

            [Test]
            public void InvalidRange () {
                // arrange
                int start;
                int final;
                var random = new Random ();

                // act
                final = random.Next (Code.MinValue, (Code.MaxValue/2)-1);
                start = random.Next ((Code.MaxValue/2)+1, Code.MaxValue);

                // assert
                Assert.Throws (typeof(ArgumentException),
                        delegate {
                            ((Code)start).Range(final);
                        });
            }
        }
    }
}
