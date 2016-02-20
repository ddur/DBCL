// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetUniqueFactoryTest
{
    [TestFixture]
    public class IsDistinct
    {

        [Test]
        public void CodeReference()
        {
            var code1 = (Code)40;
            var code2 = (Code)40;
            var iCodeSet1 = (ICodeSet)code1;
            var iCodeSet2 = (ICodeSet)code2;

            Assert.False (ReferenceEquals (iCodeSet1, iCodeSet2));

            var distinct = new DistinctICodeSet();
            var iCodeSetDistinct1 = distinct.From (code1);
            var iCodeSetDistinct2 = distinct.From (code2);

            Assert.False (ReferenceEquals (iCodeSet1, iCodeSetDistinct1));
            Assert.False (ReferenceEquals (iCodeSet2, iCodeSetDistinct2));

            Assert.True (ReferenceEquals (iCodeSetDistinct1, iCodeSetDistinct2));
        }

        [Test]
        public void ReferenceEqualityTest()
        {
            // arrange
            var codeSetPair1 = CodeSetPair.From (40, 50);
            var codeSetPair2 = CodeSetPair.From (40, 50);

            // assert
            Assert.False (ReferenceEquals (codeSetPair1, codeSetPair2));

            // arrange
            var iCodeSet1 = (ICodeSet)codeSetPair1;
            var iCodeSet2 = (ICodeSet)codeSetPair2;

            // assert
            Assert.False (ReferenceEquals (iCodeSet1, iCodeSet2));

            Assert.True (ReferenceEquals (iCodeSet1, codeSetPair1));
            Assert.True (ReferenceEquals (iCodeSet2, codeSetPair2));
            Assert.False (ReferenceEquals (iCodeSet1, codeSetPair2));
            Assert.False (ReferenceEquals (iCodeSet2, codeSetPair1));

            // arrange
            var distinct = new DistinctICodeSet();
            var iCodeSetDistinct1 = distinct.From (codeSetPair1);
            var iCodeSetDistinct2 = distinct.From (codeSetPair2);

            // assert
            Assert.True (ReferenceEquals (iCodeSetDistinct1, iCodeSetDistinct2));

            Assert.True (ReferenceEquals (iCodeSetDistinct1, codeSetPair1));
            Assert.True (ReferenceEquals (iCodeSetDistinct2, codeSetPair1));
            
            Assert.False (ReferenceEquals (iCodeSetDistinct1, codeSetPair2));
            Assert.False (ReferenceEquals (iCodeSetDistinct2, codeSetPair2));

            Assert.True (ReferenceEquals (iCodeSet1, codeSetPair1));
            Assert.True (ReferenceEquals (iCodeSet1, iCodeSetDistinct1));
        }
    }
}
