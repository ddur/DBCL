// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetRelationsTest.Members {

    [TestFixture]
    public class RangeEquals {

        [Test]
        public void Null () {
            ICodeSet a = null;
            ICodeSet b = null;

            Assert.IsTrue ( a.RangeEquals ( b ) );
            Assert.IsTrue ( b.RangeEquals ( a ) );

            b = new Code ( 7 );

            Assert.IsFalse ( a.RangeEquals ( b ) );
            Assert.IsFalse ( b.RangeEquals ( a ) );
        }

        [Test]
        public void Empty () {
            ICodeSet a = CodeSetNone.Singleton;
            ICodeSet b = CodeSetNone.Singleton;

            Assert.IsTrue ( a.RangeEquals ( b ) );
            Assert.IsTrue ( b.RangeEquals ( a ) );

            b = CodeSetPair.From ( 7, 12 );

            Assert.IsFalse ( a.RangeEquals ( b ) );
            Assert.IsFalse ( b.RangeEquals ( a ) );
        }

        [Test]
        public void NullOrEmpty () {
            ICodeSet a = null;
            ICodeSet b = CodeSetNone.Singleton;
            ;

            Assert.IsTrue ( a.RangeEquals ( b ) );
            Assert.IsTrue ( b.RangeEquals ( a ) );
        }

        [Test]
        public void ReferenceEqual () {
            ICodeSet a = null;

            Assert.IsTrue ( a.RangeEquals ( a ) );

            a = CodeSetList.From ( 1, 3, 7, 8, 9 );

            Assert.IsTrue ( a.RangeEquals ( a ) );
        }

        [Test]
        public void SetEqual () {
            ICodeSet a = CodeSetPage.From ( 6, 9, 28 );
            ICodeSet b = CodeSetList.From ( 6, 9, 28 );

            Assert.IsTrue ( a.RangeEquals ( b ) );
            Assert.IsTrue ( b.RangeEquals ( a ) );
        }

        [Test]
        public void RangeEquals_IsTrue () {
            ICodeSet a = CodeSetList.From ( 6, 9, 28 );
            ICodeSet b = CodeSetPair.From ( 6, 28 );

            Assert.IsTrue ( a.RangeEquals ( b ) );
            Assert.IsTrue ( b.RangeEquals ( a ) );
        }

        [Test]
        public void RangeEquals_IsFalse () {
            ICodeSet a = CodeSetList.From ( 6, 9, 28 );
            ICodeSet b = CodeSetPair.From ( 6, 27 );

            Assert.IsFalse ( a.RangeEquals ( b ) );
            Assert.IsFalse ( b.RangeEquals ( a ) );

            b = CodeSetPair.From ( 9, 28 );

            Assert.IsFalse ( a.RangeEquals ( b ) );
            Assert.IsFalse ( b.RangeEquals ( a ) );

            b = CodeSetPair.From ( 28, 29 );

            Assert.IsFalse ( a.RangeEquals ( b ) );
            Assert.IsFalse ( b.RangeEquals ( a ) );

            b = new Code ( 6 );

            Assert.IsFalse ( a.RangeEquals ( b ) );
            Assert.IsFalse ( b.RangeEquals ( a ) );
        }
    }
}