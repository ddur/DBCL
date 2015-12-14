// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetServiceTest {

    [TestFixture]
    public class Constants {

        [Test]
        public void AreEqual () {
            Assert.AreEqual (ICodeSetService.UnitCount, 1);
            Assert.AreEqual (ICodeSetService.PairCount, 2);
            Assert.AreEqual (ICodeSetService.NoneStart, -1);
            Assert.AreEqual (ICodeSetService.NoneFinal, -2);
        }
    }
}