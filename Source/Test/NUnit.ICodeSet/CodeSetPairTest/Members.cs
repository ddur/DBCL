// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Linq;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetPairTest
{
    [TestFixture]
    public class Members
    {
        [Test]
        public void AllMembers()
        {
            CodeSetPair csp;

            csp = CodeSetPair.From(1, 7);

            // enumerator
            Assert.True (csp.SequenceEqual(new Code[2] {1,7}));

            // count first last length
            Assert.True (csp.Count == 2);
            Assert.True (csp.First == 1);
            Assert.True (csp.Last == 7);
            Assert.True (csp.Length == 1 + csp.Last - csp.First);

            // indexer
            Assert.True (csp[1]);
            Assert.True (csp[7]);
            Assert.False (csp[5]);
            Assert.False (csp[9]);
            Assert.False (csp[Code.MinValue]);
            Assert.False (csp[Code.MaxValue]);

            
            csp = CodeSetPair.From(Code.MinValue, Code.MaxValue);

            // enumerator
            Assert.True (csp.SequenceEqual(new Code[2] {Code.MinValue,Code.MaxValue}));

            // count first last length
            Assert.True (csp.Count == 2);
            Assert.True (csp.First == Code.MinValue);
            Assert.True (csp.Last == Code.MaxValue);
            Assert.True (csp.Length == 1 + csp.Last - csp.First);

            // indexer
            Assert.True (csp[Code.MinValue]);
            Assert.True (csp[Code.MaxValue]);

            Assert.True (csp[0]);
            Assert.True (csp[1114111]);
            
            Assert.False (csp[1]);
            Assert.False (csp[7]);
            Assert.False (csp[5]);
            Assert.False (csp[9]);
            
        }
    }
}
