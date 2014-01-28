// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.CodeSetPairTest
{
    [TestFixture]
    public class Constructors
    {
        [Test]
        public void FromPair()
        {
            CodeSetPair csp;
            Assert.Throws<ArgumentException> (delegate{csp = new CodeSetPair(9,3);});
            Assert.Throws<InvalidCastException> (delegate{csp = new CodeSetPair(-20,3);});
            Assert.Throws<InvalidCastException> (delegate{csp = new CodeSetPair(0,-32);});
            csp = new CodeSetPair(1, 2);
            csp = new CodeSetPair(Code.MinValue, Code.MaxValue);
        }
    }
}
