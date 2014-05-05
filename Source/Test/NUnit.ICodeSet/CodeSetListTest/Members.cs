// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Linq;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetListTest
{
    [TestFixture]
    public class Members
    {
        [Test]
        public void AllMembers()
        {
            CodeSetList csl;
            var input = new Code[] {1114111,2,2,22,50,100,200,500,1000,10000,100000,1000000,1,0,65536,128000,512000};
            csl = CodeSetList.From (input);
            
            // indexer
            foreach (var code in input) {
                Assert.True (csl[code]);
            }
            Assert.False (csl[40]);
            Assert.False (csl[60]);
            Assert.False (csl[80]);
            
            // GetEnumerator
            var e = csl.GetEnumerator();
            foreach (var item in input.Distinct().OrderBy(item => (item))) {
                Assert.True (e.MoveNext());
                Assert.True (item == e.Current);
            }
            Assert.False (e.MoveNext());

            // Count
            Assert.True (csl.Count == 16);
            
            // First
            Assert.True (csl.First == input.Min());
            
            // Last
            Assert.True (csl.Last == input.Max());
            
            // Length
            Assert.True (csl.Length == 1 + csl.Last - csl.First);
            
        }
    }
}
