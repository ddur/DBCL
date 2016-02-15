// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeTest {

    [TestFixture]
    public class AsIReadOnlyCollectionOfCode {

        [Test]
        public void CountIsOne () {
            var r = new Random ();
            Code C = r.Next (Code.MinValue, Code.MaxValue);
            IReadOnlyCollection<Code> iC = C;

            // always 1 member
            Assert.True (iC.Count == 1);
        }

    }
}
