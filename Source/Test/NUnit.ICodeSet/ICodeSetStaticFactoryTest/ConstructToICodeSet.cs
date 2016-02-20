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

        public class FromBitSetArray {

            [Test]
            public void Null () {
                Assert.True (ReferenceEquals (CodeSetNone.Singleton, ((BitSetArray)null).ToICodeSet()));
            }
            
            [Test]
            public void Empty () {
                Assert.True (ReferenceEquals (CodeSetNone.Singleton, BitSetArray.Empty().ToICodeSet()));
            }
            
            [Test]
            public void Valid () {
                Assert.False (ReferenceEquals (CodeSetNone.Singleton, BitSetArray.From(100).ToICodeSet()));
                Assert.True (BitSetArray.From(100).ToICodeSet() is Code);
                Assert.True (BitSetArray.From(100, 101).ToICodeSet() is CodeSetPair);
                Assert.True (BitSetArray.From(100, 101, 102).ToICodeSet() is CodeSetFull);
                Assert.True (BitSetArray.From(100, 101, 120).ToICodeSet() is CodeSetList);
            }
            
        }

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
			    Assert.True (ReferenceEquals (CodeSetNone.Singleton, ((Code[])null).ToICodeSet()));
			}
			
			[Test]
			public void Empty () {
                Assert.True (ReferenceEquals (CodeSetNone.Singleton, new Code[0].ToICodeSet()));
			}
			
			[Test]
			public void Valid () {
			    Assert.False (ReferenceEquals (CodeSetNone.Singleton, new Code[] {100}.ToICodeSet()));
                Assert.True (new Code[] {100}.ToICodeSet() is Code);
                Assert.True (new Code[] {100, 101}.ToICodeSet() is CodeSetPair);
                Assert.True (new Code[] {100, 101, 100, 101}.ToICodeSet() is CodeSetPair);
                Assert.True (new Code[] {100, 101, 102, 100, 101, 102}.ToICodeSet() is CodeSetFull);
                Assert.True (new Code[] {100, 101, 120, 100, 101, 120}.ToICodeSet() is CodeSetList);
			}
			
		}

        public class FromCode {

            [Test]
            public void Valid () {
                Assert.True (((Code)32).ToICodeSet() is Code);
            }
            
        }

        public class FromStringUtf16 {

            [Test]
            public void Null() {
                const string Utf16 = null;
                Assert.True (ReferenceEquals(CodeSetNone.Singleton, Utf16.ToICodeSet()));
            }

            [Test]
            public void Empty() {
                const string Utf16 = "";
                Assert.True (ReferenceEquals(CodeSetNone.Singleton, Utf16.ToICodeSet()));
            }

            [Test]
            public void Invalid_Throws() {
                const string Utf16 = "abc\uDC00\uD800def";
                Assert.Throws ( typeof(ArgumentException),
                        delegate {
                        Utf16.ToICodeSet();
                        });
            }

            [Test]
            public void ValidAndDoesDecode() {
                const string Utf16 = "abc\uFFFF\u0000\uD801\uDC01def";
                Assert.IsInstanceOf (typeof(ICodeSet), ICodeSetFactory.ToICodeSet(Utf16));
                Assert.True (Utf16.ToICodeSet().IsReduced);
                Assert.True (Utf16.ToICodeSet().Count == 9);
            }
        }

        public class FromPredicateOfCode {

            [Test]
            public void Null () {

                // arrange
                Predicate<Code> func = null;

                // act & assert
                Assert.That (func.ToICodeSet() is CodeSetNone);
            }

            [Test]
            public void Empty () {

                // arrange
                Predicate<Code> func = x => false;

                // act & assert
                Assert.That (func.ToICodeSet() is CodeSetNone);
            }

            [Test]
            public void Valid () {

                // arrange
                int count = 0;
                // disable once ConvertClosureToMethodGroup
                Predicate<Code> func = x => x.IsHighSurrogate();

                // act
                var result = func.ToICodeSet();

                // assert
                count = 0;
                for (int index = Code.MinValue; index <= Code.MaxValue; index++) {
                    Assert.True (((Code)index).IsHighSurrogate() == result[index]);
                    if (index.IsHighSurrogate()) count += 1;
                }
                Assert.True (count == result.Count);

                count = 0;
                for (Code index = result.First; index <= result.Last; index++) {
                    Assert.True (index.IsHighSurrogate() == result[index]);
                    if (index.IsHighSurrogate()) count += 1;
                }
                Assert.True (count == result.Count);
                Assert.That (result is CodeSetFull);

                var bits = BitSetArray.Size (result.Last+1);
                bits._SetMembers (func.ToIntCodes());
                var compare = bits.ToICodeSet();
                Assert.That (compare.SequenceEqual (result));
            }
        }
	}
}
