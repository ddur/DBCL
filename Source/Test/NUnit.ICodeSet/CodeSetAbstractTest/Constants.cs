// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetAbstractTest {

    [TestFixture]
    public class ConstantsValues {

        [Test]
        public void AreEqual () {
            Assert.AreEqual (CodeSet.UnitCount, 1);
            Assert.AreEqual (CodeSet.PairCount, 2);
            Assert.AreEqual (CodeSet.NoneStart, -1);
            Assert.AreEqual (CodeSet.NoneFinal, -2);
        }
    }
}
