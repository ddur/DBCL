﻿// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Linq;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetNullTest {

    [TestFixture]
    public class Members {

        [Test]
        public void AllMembers () {
            CodeSetNone csn = CodeSetNone.Singleton;

            Assert.Throws<InvalidOperationException> (delegate { Code c = csn.First; });
            Assert.Throws<InvalidOperationException> (delegate { Code c = csn.Last; });

            Assert.True (csn.IsEmpty);
            Assert.True (csn.IsReduced);
            Assert.True (csn.Count == 0);
            Assert.True (csn.Length == 0);
            Assert.True (csn.SequenceEqual (new Code[0]));

            Random r = new Random ();
            for (int i = 1; i <= 10; i++) {
                Assert.False (csn[r.Next (Code.MinValue, Code.MaxValue)]);
            }
            for (int i = 1; i <= 10; i++) {
                Assert.False (csn[(Code)r.Next (Code.MinValue, Code.MaxValue)]);
            }
        }
    }
}
