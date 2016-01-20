// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetFullTest {

    [TestFixture]
    public class Members {

        [Test]
        public void AllMembers () {
            CodeSetFull codeSetFull;

            codeSetFull = CodeSetFull.From (1, 7);

            // enumerator
            Assert.True (codeSetFull.SequenceEqual (new Code[7] { 1, 2, 3, 4, 5, 6, 7 }));

            // count first last length
            Assert.True (codeSetFull.Count == 7);
            Assert.True (codeSetFull.First == 1);
            Assert.True (codeSetFull.Last == 7);
            Assert.True (codeSetFull.Count == codeSetFull.Length);
            Assert.True (codeSetFull.Length == 1 + codeSetFull.Last - codeSetFull.First);
            Assert.True (codeSetFull.IsReduced);
            Assert.False (codeSetFull.IsEmpty);

            // indexer
            Assert.True (codeSetFull[1]);
            Assert.True (codeSetFull[7]);
            Assert.True (codeSetFull[5]);
            Assert.False (codeSetFull[9]);
            Assert.False (codeSetFull[Code.MinValue]);
            Assert.False (codeSetFull[Code.MaxValue]);

            codeSetFull = CodeSetFull.From (Code.MinValue, Code.MaxValue);

            // enumerator -> SequenceEqual
            var range = Enumerable.Range (0, Code.MaxCount);
            Assert.True (codeSetFull.Select (item => (int)(item)).SequenceEqual (range));

            // getEnumerator
            var codeSetFullEnumerator = codeSetFull.GetEnumerator ();
            foreach (int code in range) {
                if (codeSetFullEnumerator.MoveNext () && (Code)code == codeSetFullEnumerator.Current) {
                    // ok
                }
                else {
                    Assert.True (false);
                }
            }
            Assert.False (codeSetFullEnumerator.MoveNext ());

            // count first last length
            Assert.True (codeSetFull.Count == Code.MaxCount);
            Assert.True (codeSetFull.First == Code.MinValue);
            Assert.True (codeSetFull.Last == Code.MaxValue);
            Assert.True (codeSetFull.Length == 1 + codeSetFull.Last - codeSetFull.First);

            // indexer
            Assert.True (codeSetFull[Code.MinValue]);
            Assert.True (codeSetFull[Code.MaxValue]);

            Assert.True (codeSetFull[0]);
            Assert.True (codeSetFull[1114111]);

            Assert.True (codeSetFull[1]);
            Assert.True (codeSetFull[7]);
            Assert.True (codeSetFull[5]);
            Assert.True (codeSetFull[9]);
        }
    }
}
