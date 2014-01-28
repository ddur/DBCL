// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DD.Collections.CodeSetBitsTest
{
    [TestFixture]
    public class Members
    {
        [Test]
        public void Indexer()
        {
            Random r = new Random();
            Code C = r.Next(Code.MinValue, Code.MaxValue);
            Code D = C;
            while (D == C) {
                D = r.Next(Code.MinValue, Code.MaxValue);
            }

            CodeSetBits csb = new CodeSetBits();
            Assert.False ( csb[C] );
            Assert.False ( csb[D] );
            
            csb  = new CodeSetBits(new List<Code>());
            Assert.False ( csb[C] );
            Assert.False ( csb[D] );
            
            csb  = new CodeSetBits(new List<Code>() {C});
            Assert.True ( csb[C] );
            Assert.False ( csb[D] );
            
            csb  = new CodeSetBits(new List<Code>() {C,D});
            Assert.True ( csb[C] );
            Assert.True ( csb[D] );
            
        }
        
        [Test]
        public void FirstLast() {

            CodeSetBits csb;
            Code C;

            csb = new CodeSetBits();
            Assert.Throws<InvalidOperationException> (delegate{C = csb.First;});
            Assert.Throws<InvalidOperationException> (delegate{C = csb.Last;});
            
            csb = new CodeSetBits (new List<Code>() {12});
            Assert.True (csb.First == 12);
            Assert.True (csb.Last == 12);
            
            csb = new CodeSetBits (new List<Code>() {1,12,33,20});
            Assert.True (csb.First == 1);
            Assert.True (csb.Last == 33);
            
        }
        
        [Test]
        public void ToCompact() {
            CodeSetBits csb;
            BitSetArray compact;

            csb = new CodeSetBits();
            compact = csb.ToCompact();
            Assert.True (compact.Count == 0);
            Assert.True (compact.Length == 0);
            
            csb = new CodeSetBits (new List<Code>() {12});
            compact = csb.ToCompact();
            Assert.True (compact.Count == 1);
            Assert.True (compact.Length == 1);
            Assert.True (compact.Length == 1 + compact.Last - compact.First);
            Assert.True (compact.First == 0);
            Assert.True (compact.Last == 0);
            Assert.True (compact[0]);
            Assert.True (compact[compact.Length-1]);
            
            csb = new CodeSetBits (new List<Code>() {1,12,33,20});
            compact = csb.ToCompact();
            Assert.True (compact.Count == 4);
            Assert.True (compact.Length == 33);
            Assert.True (compact.Length == 1 + compact.Last - compact.First);
            Assert.True (compact.First == 0);
            Assert.True (compact.Last == compact.Length-1);
            Assert.True (compact[0]);
            Assert.True (compact[compact.Length-1]);
            
        }

    }
}
