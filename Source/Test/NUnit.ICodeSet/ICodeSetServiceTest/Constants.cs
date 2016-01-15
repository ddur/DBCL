// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetServiceTest {

    [TestFixture]
    public class Constants {

        [Test]
        public void AreEqual () {
            Assert.AreEqual (Service.UnitCount, 1);
            Assert.AreEqual (Service.PairCount, 2);
            Assert.AreEqual (Service.NoneStart, -1);
            Assert.AreEqual (Service.NoneFinal, -2);
        }
    }
}
