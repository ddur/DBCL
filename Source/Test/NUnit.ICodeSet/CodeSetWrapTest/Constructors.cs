// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetWrapTest {

    [TestFixture]
    public class Constructors {

        [Test]
        public void From_Nothing () {
            var x = CodeSetWrap.From ();
        }

        [Test]
        public void From_CodeSetWrap () {
            var x = CodeSetWrap.From (new Code[] { 1, 2, 3 });
            var y = CodeSetWrap.From (x);
        }

        [Test]
        public void From_BitSetArray () {
            var x = CodeSetWrap.From (BitSetArray.Empty ());
            var y = CodeSetWrap.From (BitSetArray.From (1, 2, 3));
        }

        [Test]
        public void From_IEnumerableOfCode () {
            var x = CodeSetWrap.From (new Code[0]);
            var y = CodeSetWrap.From (new Code[] { 1, 2, 3 });
        }

        [Test]
        public void From_CodeSetWrap_ThrowsIfNull () {
            Assert.Throws<ArgumentNullException> (
                delegate {
                    var x = CodeSetWrap.From ((CodeSetWrap)null);
                }
            );
        }

        [Test]
        public void From_BitSetArray_ThrowsIfNull () {
            Assert.Throws<ArgumentNullException> (
                delegate {
                    var x = CodeSetWrap.From ((BitSetArray)null);
                }
            );
        }

        [Test]
        public void From_BitSetArray_ThrowsIfNotCode () {
            Assert.Throws<IndexOutOfRangeException> (
                delegate {
                    var x = CodeSetWrap.From (BitSetArray.Size (Code.MaxCount + 1, true));
                }
            );
        }

        [Test]
        public void From_IEnumerableOfCode_ThrowsIfNull () {
            Assert.Throws<ArgumentNullException> (
                delegate {
                    var x = CodeSetWrap.From ((IEnumerable<Code>)null);
                }
            );
        }
    }
}