// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetExtendsTest.Members {

    [TestFixture]
    public class IsCodeCompactLast {

        [Test]
        public void ArgIsNull () {
            int[] arg = null;
            Assert.That (arg.IsCompactLast () == -1);
            Assert.False (arg.IsCompact ());
        }

        [Test]
        public void ArgIsEmpty () {
            var arg = new int[0];
            Assert.That (arg.IsCompactLast () == -1);
            Assert.False (arg.IsCompact ());
        }

        [Test]
        public void ArgIsTooLarge () {
            var arg = new int[(Code.MaxValue >> 5) + 2];
            Assert.That (arg.IsCompactLast () == -1);
            Assert.False (arg.IsCompact ());
        }

        [Test]
        public void ArgFirstBitIsNotSet () {
            var arg = new int[] { 0 };
            Assert.That (arg.IsCompactLast () == -1);
            Assert.False (arg.IsCompact ());
        }

        [Test]
        public void ArgLastByteIsNotSet () {
            var arg = new int[] { 1, 0 };
            Assert.That (arg.IsCompactLast () == -1);
            Assert.False (arg.IsCompact ());
        }

        [Test]
        public void ArgIsValid () {
            var arg = new int[] { 1, 1 };
            Assert.That (arg.IsCompactLast () == 32);
            Assert.True (arg.IsCompact ());
        }

        [Test]
        public void ArgIsValidAgain () {
            var arg = new int[] { 1, -1 };
            Assert.That (arg.IsCompactLast () == 63);
            Assert.True (arg.IsCompact ());
        }
    }
}
