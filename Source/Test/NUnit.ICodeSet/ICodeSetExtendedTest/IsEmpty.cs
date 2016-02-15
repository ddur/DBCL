// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using DD.Collections;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetExtendedTest.Members {

    [TestFixture]
    public class IsEmpty {

        [Test]
        public void ICodeSet_Null_Throws () {
            Assert.Throws<ArgumentNullException> (
                delegate {
                    ((BitSetArray)null).IsEmpty ();
                }
            );
        }

        [Test]
        public void ICodeSet_Empty () {
            Assert.True (CodeSetNone.Singleton.IsEmpty ());
        }

        [Test]
        public void ICodeSet_NotEmpty () {
            Assert.False (CodeSetMask.From (1).IsEmpty ());
            Assert.False (CodeSetMask.From (10, 11, 20).IsEmpty ());
        }
    }
}
