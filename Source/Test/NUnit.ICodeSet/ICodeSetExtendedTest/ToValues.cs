// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetExtendedTest.Members {

    [TestFixture]
    public class ToValues {

        [Test]
        public void When_NullCodes () {
            Assert.That (((IEnumerable<Code>)null).ToValues ().IsNot (null));
            Assert.That (!((IEnumerable<Code>)null).ToValues ().Any ());
            Assert.That (((IEnumerable<Code>)null).ToValues (0).IsNot (null));
            Assert.That (!((IEnumerable<Code>)null).ToValues (0).Any ());
        }

        [Test]
        public void When_EmptyCodes () {
            Assert.That ((new Code[0]).ToValues ().IsNot (null));
            Assert.That (!((IEnumerable<Code>)null).ToValues ().Any ());
            Assert.That ((new Code[0]).ToValues (0).IsNot (null));
            Assert.That (!((IEnumerable<Code>)null).ToValues (0).Any ());
        }

        [Test]
        public void When_ValidCodes () {
            IEnumerable<Code> test = new Code[] { 0, 100, 1000 };
            Assert.That (test.ToValues ().IsNot (null));
            Assert.That (test.ToValues ().SequenceEqual (new int[] { 0, 100, 1000 }));
            Assert.That (test.ToValues (10).IsNot (null));
            Assert.That (test.ToValues (10).SequenceEqual (new int[] { 10, 110, 1010 }));
        }

        [Test]
        public void When_NullChars () {
            Assert.That (((IEnumerable<Char>)null).ToValues ().IsNot (null));
            Assert.That (!((IEnumerable<Char>)null).ToValues ().Any ());
        }

        [Test]
        public void When_EmptyChars () {
            Assert.That ((new Char[0]).ToValues ().IsNot (null));
            Assert.That (!((IEnumerable<Char>)null).ToValues ().Any ());
        }

        [Test]
        public void When_ValidChars () {
            IEnumerable<char> test = new char[] { (char)0, (char)100, (char)1000 };
            Assert.That (test.ToValues ().IsNot (null));
            Assert.That (test.ToValues ().SequenceEqual (new int[] { 0, 100, 1000 }));
        }
    }
}
