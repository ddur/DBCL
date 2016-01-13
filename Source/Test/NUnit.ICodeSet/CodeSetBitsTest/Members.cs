// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetBitsTest {

    [TestFixture]
    public class Members {

        [Test]
        public void Indexer () {
            var r = new Random ();
            Code C = r.Next (Code.MinValue, Code.MaxValue);
            Code D = C;
            while (D == C) {
                D = r.Next (Code.MinValue, Code.MaxValue);
            }

            var csb = CodeSetBits.From ();
            Assert.False (csb[C]);
            Assert.False (csb[D]);

            csb = CodeSetBits.From (new List<Code> ());
            Assert.False (csb[C]);
            Assert.False (csb[D]);

            csb = CodeSetBits.From (new List<Code> () { C });
            Assert.True (csb[C]);
            Assert.False (csb[D]);

            csb = CodeSetBits.From (new List<Code> () { C, D });
            Assert.True (csb[C]);
            Assert.True (csb[D]);
        }

        [Test]
        public void Properties () {
            CodeSetBits csb;
            Code C;

            csb = CodeSetBits.From ();
            Assert.Throws<InvalidOperationException> (delegate {
                C = csb.First;
            });
            Assert.Throws<InvalidOperationException> (delegate {
                C = csb.Last;
            });

            csb = CodeSetBits.From (new List<Code> () { 12 });
            Assert.True (csb.Count == 1);
            Assert.True (csb.Length == 1);
            Assert.True (csb.First == 12);
            Assert.True (csb.Last == 12);
            Assert.False (csb.IsReduced);


            csb = CodeSetBits.From (new List<Code> () { 1, 12, 33, 20 });
            Assert.True (csb.Count == 4);
            Assert.True (csb.Length == 33);
            Assert.True (csb.First == 1);
            Assert.True (csb.Last == 33);
            Assert.False (csb.IsReduced);
        }

        [Test]
        public void ToCompact () {
            CodeSetBits csb;
            BitSetArray compact;

            csb = CodeSetBits.From ();
            compact = csb.ToCompact ();
            Assert.True (compact.Count == 0);
            Assert.True (compact.Length == 0);

            csb = CodeSetBits.From (new List<Code> () { 12 });
            compact = csb.ToCompact ();
            Assert.True (compact.Count == 1);
            Assert.True (compact.Length == 1);
            Assert.True (compact.Length == 1 + compact.Last - compact.First);
            Assert.True (compact.First == 0);
            Assert.True (compact.Last == 0);
            Assert.True (compact[0]);
            Assert.True (compact[compact.Length - 1]);

            csb = CodeSetBits.From (new List<Code> () { 1, 12, 33, 20 });
            compact = csb.ToCompact ();
            Assert.True (compact.Count == 4);
            Assert.True (compact.Length == 33);
            Assert.True (compact.Length == 1 + compact.Last - compact.First);
            Assert.True (compact.First == 0);
            Assert.True (compact.Last == compact.Length - 1);
            Assert.True (compact[0]);
            Assert.True (compact[compact.Length - 1]);
        }

        [Test]
        public void Complement () {
            BitSetArray bsa = BitSetArray.From (new List<int> () { 1, 2, 5, 7 });
            CodeSetBits csb = CodeSetBits.From (bsa);
            List<Code> complement = new List<Code> () { 3, 4, 6 };
            Assert.True (csb.Complement.SequenceEqual (complement));
        }
    }
}
