// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetPairTest {

    [TestFixture]
    public class Constructors {

        [Test]
        public void FromPair () {
            CodeSetPair csp;

            csp = CodeSetPair.From (1, 2);
            csp = CodeSetPair.From (1, 3);
            csp = CodeSetPair.From (Code.MinValue, Code.MaxValue);
        }

        [Test]
        public void FromPairThrows () {
            CodeSetPair csp;
            Assert.Throws<ArgumentException> (delegate { csp = CodeSetPair.From (9, 3); });
            Assert.Throws<ArgumentException> (delegate { csp = CodeSetPair.From (2, 2); });

            Assert.Throws<InvalidCastException> (delegate { csp = CodeSetPair.From (-20, 3); });
            Assert.Throws<InvalidCastException> (delegate { csp = CodeSetPair.From (0, -32); });
        }
    }
}
