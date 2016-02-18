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

            var distinct = new Distinct();
            var iCodeSetDistinct1 = distinct.From (code1);
            var iCodeSetDistinct2 = distinct.From (code2);

            Assert.False (ReferenceEquals (iCodeSet1, iCodeSetDistinct1));
            Assert.False (ReferenceEquals (iCodeSet2, iCodeSetDistinct2));

            Assert.True (ReferenceEquals (iCodeSetDistinct1, iCodeSetDistinct2));
        }

        [Test]
        public void OtherReference()
        {
            var codeSetPair = CodeSetPair.From (40, 50);
            var iCodeSet = (ICodeSet)codeSetPair;
            Assert.True (ReferenceEquals (iCodeSet, codeSetPair));

            var distinct = new Distinct();
            var iCodeSetDistinct = distinct.From (codeSetPair);
            Assert.True (ReferenceEquals (iCodeSet, iCodeSetDistinct));
        }
    }
}
