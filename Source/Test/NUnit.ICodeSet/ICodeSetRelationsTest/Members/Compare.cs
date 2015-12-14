// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetRelationsTest.Members {

    [TestFixture]
    public class Compare {

        [Test]
        public void NullOrEmpty () {
            ICodeSet a = null;
            ICodeSet b = null;
            Assert.True (a.Compare (b) == 0);

            a = CodeSetNone.Singleton;
            b = null;
            Assert.True (a.Compare (b) == 0);

            a = CodeSetNone.Singleton;
            b = CodeSetNone.Singleton;
            Assert.True (a.Compare (b) == 0);

            a = null;
            b = CodeSetNone.Singleton;
            Assert.True (a.Compare (b) == 0);
        }

        [Test]
        public void NullOrEmpty_with_NotEmpty () {
            ICodeSet a = null;
            ICodeSet b = new Code (0);
            Assert.True (a.Compare (b) == -1);

            a = CodeSetNone.Singleton;
            b = CodeSetPair.From (0, 100);
            Assert.True (a.Compare (b) == -1);

            a = CodeSetNone.Singleton;
            b = CodeSetList.From (0, 100, 1000);
            Assert.True (a.Compare (b) == -1);

            a = new Code (0);
            b = null;
            Assert.True (a.Compare (b) == 1);

            a = CodeSetPair.From (0, 100);
            b = CodeSetNone.Singleton;
            Assert.True (a.Compare (b) == 1);

            a = CodeSetList.From (0, 100, 1000);
            b = CodeSetNone.Singleton;
            Assert.True (a.Compare (b) == 1);
        }

        [Test]
        public void NotEmpty_CompareMostSignificantBit () {
            ICodeSet a = new Code (0);
            ICodeSet b = new Code (1);
            Assert.True (a.Compare (b) == -1);

            a = new Code (100);
            b = new Code (1);
            Assert.True (a.Compare (b) == 1);

            a = CodeSetList.From (0, 1, 2, 3, 4, 5, 6, 8);
            b = CodeSetList.From (0, 1, 2, 3, 4, 5, 6, 9);
            Assert.True (a.Compare (b) == -1);

            a = CodeSetList.From (0, 1, 2, 3, 4, 5, 6, 90);
            b = CodeSetList.From (0, 1, 2, 3, 4, 5, 6, 8);
            Assert.True (a.Compare (b) == 1);
        }

        [Test]
        public void NotEmpty_CompareBits () {
            ICodeSet a = new Code (0);
            ICodeSet b = new Code (0);
            Assert.True (a.Compare (b) == 0);

            a = CodeSetList.From (0, 1, 2, 3, 4, 5, 6, 9);
            b = CodeSetList.From (0, 1, 2, 3, 4, 5, 6, 9);
            Assert.True (a.Compare (b) == 0);

            a = CodeSetList.From (0, 2, 3, 4, 5, 6, 9);
            b = CodeSetList.From (0, 1, 2, 3, 4, 5, 6, 9);
            Assert.True (a.Compare (b) == -1);

            a = CodeSetList.From (0, 1, 2, 3, 4, 5, 6, 90);
            b = CodeSetList.From (0, 2, 3, 4, 5, 6, 90);
            Assert.True (a.Compare (b) == 1);
        }
    }
}