// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DD.Collections.ICodeSet.CodeSetMaskTest
{
    [TestFixture]
    public class Members
    {
        [Test]
        public void AllMembers()
        {
            var arg = (IEnumerable<Code>)new Code[] { 10, 11, 13, 13 };
            var csm = CodeSetMask.From (arg);
            Assert.True (csm[10]);
            Assert.True (csm[11]);
            Assert.False (csm[12]);
            Assert.True (csm[13]);
            Assert.False (csm[4]);
            Assert.True (csm.Count == 3);
            Assert.True (csm.Length == 4);
            Assert.True (csm.First == 10);
            Assert.True (csm.Last == 13);
            Assert.False (csm.IsReduced);
            Assert.True (csm.SequenceEqual(arg.OrderBy(x => x).Distinct())); // covers getEnumerator
            
            csm = CodeSetMask.From ((IEnumerable<Code>)new Code[] { 0, 1, 3, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 });
            Assert.True (csm.IsReduced);
            Assert.False (csm.IsEmpty);

            csm = CodeSetMask.From ((IEnumerable<Code>)new Code[] { 0, 1, 3, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 1024 });
            Assert.True (csm.IsReduced);
            Assert.False (csm.IsEmpty);

            csm = CodeSetMask.From ((IEnumerable<Code>)new Code[] { 0, 1, 3, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 1024, 65536 });
            Assert.False (csm.IsReduced);
            Assert.False (csm.IsEmpty);

            csm = CodeSetMask.From ((IEnumerable<Code>)new Code[] { 0, 1, 3, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
            Assert.False (csm.IsReduced);
            Assert.False (csm.IsEmpty);
        }
    }
}
