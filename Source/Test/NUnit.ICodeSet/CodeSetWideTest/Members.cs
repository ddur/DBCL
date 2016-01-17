// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetWideTest {

    [TestFixture]
    public class Members {
        private static List<Code> list1 = new List<Code> () { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 32768, 65536, 1114111 };
        private static List<Code> list2 = new List<Code> () { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 32768, 65536, 65537 };

        private readonly CodeSetWide csw1 = CodeSetWide.From (list1);
        private readonly CodeSetWide csw2 = CodeSetWide.From (list2);

        [Test]
        public void AsEnumerable () {
            Assert.True (csw1.SequenceEqual (list1));
            Assert.True (csw2.SequenceEqual (list2));
        }

        [Test]
        public void Indexer () {
            Assert.False (csw1[-1]);
            Assert.False (csw1[int.MinValue]);

            Assert.True (csw1[0]);
            Assert.False (csw1[100]);

            Assert.True (csw1[(Code)0]);
            Assert.False (csw1[(Code)100]);

            Assert.False (csw1[65535]);
            Assert.True (csw1[65536]);
            Assert.False (csw1[65537]);

            Assert.False (csw1[(Code)65535]);
            Assert.True (csw1[(Code)65536]);
            Assert.False (csw1[(Code)65537]);

            Assert.False (csw1[Code.MaxValue - 1]);
            Assert.True (csw1[Code.MaxValue]);

            Assert.False (csw1[(Code)Code.MaxValue - 1]);
            Assert.True (csw1[(Code)Code.MaxValue]);

            Assert.False (csw2[65535]);
            Assert.True (csw2[65536]);
            Assert.True (csw2[65537]);

            Assert.False (csw2[(Code)65535]);
            Assert.True (csw2[(Code)65536]);
            Assert.True (csw2[(Code)65537]);

            Assert.False (csw2[Code.MaxValue - 1]);
            Assert.False (csw2[Code.MaxValue]);

            Assert.False (csw2[(Code)Code.MaxValue - 1]);
            Assert.False (csw2[(Code)Code.MaxValue]);
        }

        [Test]
        public void Properties () {
            Assert.True (csw1.Count == 20);
            Assert.True (csw1.Length == Code.MaxCount);
            Assert.True (csw1.First.Value == 0);
            Assert.True (csw1.Last.Value == Code.MaxValue);

            Assert.True (csw2.Count == 20);
            Assert.True (csw2.Length == 1 + 65537);
            Assert.True (csw2.First.Value == 0);
            Assert.True (csw2.Last.Value == 65537);
        }
    }
}
