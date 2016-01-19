// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using DD.Collections;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetServiceTest.Members {

    [TestFixture]
    public class Span {

        [Test]
        public void IEnumerableCode_Null () {
            Assert.AreEqual (0, ((CodeSet)null).Span ());
        }

        [Test]
        public void IEnumerableCode_Empty () {
            Assert.AreEqual (0, CodeSetNone.Singleton.Span ());
            Assert.AreEqual (0, (new Code[0]).Span ());
        }

        [Test]
        public void IEnumerableCode_NotEmpty () {
            Assert.AreEqual (10, CodeSetPair.From (1, 10).Span ());
            Assert.AreEqual (100, CodeSetMask.From (1, 20, 100).Span ());
            Assert.AreEqual (1000, CodeSetList.From (901, 1000, 1111, 1900).Span ());
        }
    }
}
