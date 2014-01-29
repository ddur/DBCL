// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Collections.Generic;

using DD.Enumerables;
using NUnit.Framework;

namespace DD.Collections.CodeSetFullTest
{
    [TestFixture]
    public class Members
    {
        [Test]
        public void AllMembers()
        {
            CodeSetFull csf;

            csf = new CodeSetFull(1, 7);

            // enumerator
            Assert.True (csf.SequenceEqual(new Code[7] {1,2,3,4,5,6,7}));

            // count first last length
            Assert.True (csf.Count == 7);
            Assert.True (csf.First == 1);
            Assert.True (csf.Last == 7);
            Assert.True (csf.Count == csf.Length);
            Assert.True (csf.Length == 1 + csf.Last - csf.First);

            // indexer
            Assert.True (csf[1]);
            Assert.True (csf[7]);
            Assert.True (csf[5]);
            Assert.False (csf[9]);
            Assert.False (csf[Code.MinValue]);
            Assert.False (csf[Code.MaxValue]);

            
            csf = new CodeSetFull(Code.MinValue, Code.MaxValue);

            // enumerator -> SequenceEqual
            Range range = Code.MinValue.To(Code.MaxValue);
            Assert.True (csf.Select(item => (int)item).SequenceEqual(range));

            // getEnumerator
            var eF = csf.GetEnumerator();
            foreach (int code in range) {
                if (eF.MoveNext() && (Code)code == eF.Current) {
                    // ok
                }
                else {
                    Assert.True (false);
                }
            }
            Assert.False (eF.MoveNext());

            // count first last length
            Assert.True (csf.Count == Code.MaxCount);
            Assert.True (csf.First == Code.MinValue);
            Assert.True (csf.Last == Code.MaxValue);
            Assert.True (csf.Length == 1 + csf.Last - csf.First);

            // indexer
            Assert.True (csf[Code.MinValue]);
            Assert.True (csf[Code.MaxValue]);

            Assert.True (csf[0]);
            Assert.True (csf[1114111]);
            
            Assert.True (csf[1]);
            Assert.True (csf[7]);
            Assert.True (csf[5]);
            Assert.True (csf[9]);
            
        }
    }
}
