// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetFullTest {

    [TestFixture]
    public class Constructors {

        [Test]
        public void FromRange () {
            CodeSetFull csf;
            csf = CodeSetFull.From (1, 3); // at least 3 members
            csf = CodeSetFull.From (Code.MinValue, Code.MaxValue);
        }

        [Test]
        public void FromRangeThrows () {
            CodeSetFull csf;
            Assert.Throws<InvalidOperationException> (
                delegate {
                    csf = CodeSetFull.From (9, 3);
                }
            );
            Assert.Throws<InvalidOperationException> (
                delegate {
                    csf = CodeSetFull.From (1, 2);
                }
            );
            Assert.Throws<InvalidCastException> (
                delegate {
                    csf = CodeSetFull.From (-20, 3);
                }
            );
            Assert.Throws<InvalidCastException> (
                delegate {
                    csf = CodeSetFull.From (0, -32);
                }
            );
        }
    }
}
