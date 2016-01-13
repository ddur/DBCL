// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetPageTest {

    [TestFixture]
    public class Members {

        [Test]
        public void IndexerAndEnumerator () {

            CodeSetPage csp;
            List<Code> arg;

            Random r = new Random ();

            Code C = r.Next (char.MinValue + 10, char.MaxValue);
            Code D = C;
            while (D == C) {
                D = r.Next (char.MinValue + 10, char.MaxValue);
            }

            arg = new List<Code> () { 1, 2, 2, 40 };
            csp = CodeSetPage.From (arg);
            Assert.False (csp[C]);
            Assert.False (csp[D]);
            Assert.True (csp.SequenceEqual(arg.OrderBy (x => x).Distinct()));

            arg = new List<Code> () { 1, 3, 2, 50, C };
            csp = CodeSetPage.From (arg);
            Assert.True (csp[C]);
            Assert.False (csp[D]);
            Assert.True (csp.SequenceEqual(arg.OrderBy (x => x).Distinct()));

            arg = new List<Code> () { 3, 2, 1, 1, C, D };
            csp = CodeSetPage.From (arg);
            Assert.True (csp[C]);
            Assert.True (csp[D]);
            Assert.True (csp.SequenceEqual(arg.OrderBy (x => x).Distinct()));
        }

        [Test]
        public void Properties () {
            CodeSetPage csp;

            csp = CodeSetPage.From (new List<Code> () { 1, 12, 33, 20 });
            Assert.True (csp.Count == 4);
            Assert.True (csp.Length == 33);
            Assert.True (csp.First == 1);
            Assert.True (csp.Last == 33);
            Assert.False (csp.IsReduced);

            csp = CodeSetPage.From (new List<Code> () { 1, 12, 33, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 15, 16, 17 });
            Assert.True (csp.Count == 17);
            Assert.True (csp.Length == 33);
            Assert.True (csp.First == 1);
            Assert.True (csp.Last == 33);
            Assert.True (csp.IsReduced);
        }

        [Test]
        public void ToCompact () {
            CodeSetPage csp;
            BitSetArray compact;

            csp = CodeSetPage.From (new List<Code> () { 1, 12, 33, 20 });
            compact = csp.ToCompact ();
            Assert.True (compact.Count == 4);
            Assert.True (compact.Length == 33);
            Assert.True (compact.Length == 1 + compact.Last - compact.First);
            Assert.True (compact.First == 0);
            Assert.True (compact.Last == compact.Length - 1);
            Assert.True (compact[0]);
            Assert.True (compact[compact.Length - 1]);
        }
    }
}
