// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------


using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetFuncTest
{
    [TestFixture]
    public class Construct
    {
        [Test]
        public void Null()
        {
            // assert
            Assert.Throws (typeof(ArgumentNullException),
                    delegate {
                        CodeSetFunc.From ((Predicate<Code>)null);
                    });
        }

        [Test]
        public void Empty()
        {
            // assert
            Assert.Throws (typeof(ArgumentEmptyException),
                    delegate {
                               CodeSetFunc.From ((Predicate<Code>)(x => false));
                    });
        }

        [Test]
        public void Valid()
        {
                // arrange
                int count = 0;
                // disable once ConvertClosureToMethodGroup
                Predicate<Code> func = x => x.IsHighSurrogate();

                // act
                var result = CodeSetFunc.From(func);

                // assert
                count = 0;
                for (int index = Code.MinValue; index <= Code.MaxValue; index++) {
                    Assert.True (index.IsHighSurrogate() == result[index]);
                    if (index.IsHighSurrogate()) count += 1;
                }
                Assert.True (count == result.Count);

                count = 0;
                for (int index = result.First; index <= result.Last; index++) {
                    Assert.True (index.IsHighSurrogate() == result[index]);
                    if (index.IsHighSurrogate()) count += 1;
                }
                Assert.True (count == result.Count);

                var compare = func.From();
                Assert.That (compare.SequenceEqual (result));
        }
    }
}
