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
    public class Members
    {
        [Test]
        public void All()
        {
                // arrange
                Predicate<Code> func = x => x == 1 || x == 10 || x == 100;

                // act
                var result = CodeSetFunc.From(func);

                // assert
                Assert.True (result.First == 1);
                Assert.True (result.Last == 100);
                Assert.True (result.Count == 3);
                Assert.True (result.Length == 100);

                Assert.That (result[1]);
                Assert.That (result[10]);
                Assert.That (result[100]);

                Assert.That (!result[0]);
                Assert.That (!result[50]);

                Assert.That (result[(Code)1]);
                Assert.That (result[(Code)10]);
                Assert.That (result[(Code)100]);

                Assert.That (!result[(Code)0]);
                Assert.That (!result[(Code)50]);

                Assert.That (!result.IsEmpty);
                Assert.That (!result.IsReduced);

                foreach (var item in result) {
                    Assert.That (func(item));
                }
        }
    }
}
