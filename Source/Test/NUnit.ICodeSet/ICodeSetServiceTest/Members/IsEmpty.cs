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
    public class IsEmpty {

        [Test]
        public void BitSetArray_Null_Throws () {
            Assert.Throws<ArgumentNullException> (
                delegate {
                    ((BitSetArray)null).IsEmpty ();
                }
            );
        }

        [Test]
        public void BitSetArray_Empty () {
            Assert.True (BitSetArray.Empty ().IsEmpty ());
            Assert.True (BitSetArray.Size (10).IsEmpty ());
        }

        [Test]
        public void BitSetArray_NotEmpty () {
            Assert.False (BitSetArray.Size (1, true).IsEmpty ());
            Assert.False (BitSetArray.From (10, 11, 20).IsEmpty ());
        }

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
