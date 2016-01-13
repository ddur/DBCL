// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetRelationsTest.Members {

    [TestFixture]
    public class RangeOverlaps {

        [Test]
        public void Null () {
            ICodeSet a = null;
            ICodeSet b = null;

            Assert.IsFalse (a.RangeOverlaps (b));
            Assert.IsFalse (b.RangeOverlaps (a));

            b = new Code (7);

            Assert.IsFalse (a.RangeOverlaps (b));
            Assert.IsFalse (b.RangeOverlaps (a));
        }

        [Test]
        public void Empty () {
            ICodeSet a = CodeSetNone.Singleton;
            ICodeSet b = CodeSetNone.Singleton;

            Assert.IsFalse (a.RangeOverlaps (b));
            Assert.IsFalse (b.RangeOverlaps (a));

            b = CodeSetPair.From (7, 12);

            Assert.IsFalse (a.RangeOverlaps (b));
            Assert.IsFalse (b.RangeOverlaps (a));
        }

        [Test]
        public void NullOrEmpty () {
            ICodeSet a = null;
            ICodeSet b = CodeSetNone.Singleton;
            ;

            Assert.IsFalse (a.RangeOverlaps (b));
            Assert.IsFalse (b.RangeOverlaps (a));
        }

        [Test]
        public void ReferenceEqual () {
            ICodeSet a = null;

            Assert.IsFalse (a.RangeOverlaps (a));

            a = CodeSetList.From (1, 3, 7, 8, 9);

            Assert.IsTrue (a.RangeOverlaps (a));
        }

        [Test]
        public void SetEqual () {
            ICodeSet a = CodeSetMask.From (6, 9, 28);
            ICodeSet b = CodeSetList.From (6, 9, 28);

            Assert.IsTrue (a.RangeOverlaps (b));
            Assert.IsTrue (b.RangeOverlaps (a));
        }

        [Test]
        public void RangeEqual () {
            ICodeSet a = CodeSetList.From (6, 9, 28);
            ICodeSet b = CodeSetPair.From (6, 28);

            Assert.IsTrue (a.RangeOverlaps (b));
            Assert.IsTrue (b.RangeOverlaps (a));
        }

        [Test]
        public void RangeOverlaps_IsTrue () {
            ICodeSet a = CodeSetList.From (6, 9, 28);
            ICodeSet b = CodeSetPair.From (6, 28);

            Assert.IsTrue (a.RangeOverlaps (b));
            Assert.IsTrue (b.RangeOverlaps (a));

            b = CodeSetPair.From (0, 10);

            Assert.IsTrue (a.RangeOverlaps (b));
            Assert.IsTrue (b.RangeOverlaps (a));

            b = CodeSetPair.From (10, 30);

            Assert.IsTrue (a.RangeOverlaps (b));
            Assert.IsTrue (b.RangeOverlaps (a));

            b = CodeSetPair.From (0, 6);

            Assert.IsTrue (a.RangeOverlaps (b));
            Assert.IsTrue (b.RangeOverlaps (a));

            b = CodeSetPair.From (28, 29);

            Assert.IsTrue (a.RangeOverlaps (b));
            Assert.IsTrue (b.RangeOverlaps (a));

            b = new Code (11);

            Assert.IsTrue (a.RangeOverlaps (b));
            Assert.IsTrue (b.RangeOverlaps (a));
        }

        [Test]
        public void RangeOverlaps_IsFalse () {
            ICodeSet a = CodeSetList.From (6, 9, 28);
            ICodeSet b = CodeSetPair.From (29, 37);

            Assert.IsFalse (a.RangeOverlaps (b));
            Assert.IsFalse (b.RangeOverlaps (a));

            b = CodeSetPair.From (0, 5);

            Assert.IsFalse (a.RangeOverlaps (b));
            Assert.IsFalse (b.RangeOverlaps (a));

            b = new Code (4);

            Assert.IsFalse (a.RangeOverlaps (b));
            Assert.IsFalse (b.RangeOverlaps (a));
        }
    }
}
