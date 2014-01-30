// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DD.Collections.CodeSetPageTest
{
    [TestFixture]
    public class Constructors
    {

        [Test]
        public void FromCompact()
        {
            CodeSetPage csp;

            // requires no null
            Assert.Throws <ArgumentNullException> (delegate{csp = new CodeSetPage ((BitSetArray)null, 0);});

            // requires at least 3 members (> ICodeSetService.PairCount)
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new BitSetArray(), 0);});
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new BitSetArray() {0}, 0);});
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new BitSetArray() {0,12}, 0);});

            // requires compact bitSet
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new BitSetArray() {1,2,3}, 0);});
            
            // requires all members within same unicode plane
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new BitSetArray() {0,1,2,3,66000}, 0);});
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new BitSetArray() {0,1,2,3,65535}, 1);});
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new BitSetArray() {0,1,2,3}, 65533);});
            
            // requires valid offset 
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new BitSetArray() {0,1,2,3}, -1);});
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new BitSetArray() {0,1,2,3}, Code.MaxCount);});
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new BitSetArray() {0,1,2,3}, Code.MaxValue);});

            csp = new CodeSetPage (new BitSetArray() {0,1,12,33}, 128);
        }
        
        [Test]
        public void FromBitSetArray()
        {
            CodeSetPage csp;

            // requires no null
            Assert.Throws <ArgumentNullException> (delegate{csp = new CodeSetPage ((BitSetArray)null);});

            // requires at least 3 members (> ICodeSetService.PairCount)
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new BitSetArray());});
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new BitSetArray() {21});});
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new BitSetArray() {12,5});});

            // requires all codes within same unicode plane
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new BitSetArray() {12,66000});});

            csp = new CodeSetPage (new BitSetArray() {1,12,33});
        }
        
        [Test]
        public void FromCodes()
        {
            CodeSetPage csp;

            // requires no null
            Assert.Throws <ArgumentNullException> (delegate{csp = new CodeSetPage ((IEnumerable<Code>)null);});

            // requires at least 3 members (> ICodeSetService.PairCount)
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new Code[0]);});
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new List<Code>() {21});});
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new List<Code>() {1,25});});

            // requires all codes within same unicode plane
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new List<Code>() {12,66000});});

            csp = new CodeSetPage (new List<Code>() {0,1,12,33,65535});

        }
        
        [Test]
        public void FromICodeSet()
        {
            CodeSetPage csp;

            // requires no null
            Assert.Throws <ArgumentNullException> (delegate{csp = new CodeSetPage ((ICodeSet)null);});

            // requires at least 3 members (> ICodeSetService.PairCount)
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new CodeSetBits(new Code[0]));});
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new CodeSetBits(new List<Code>() {21}));});
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new CodeSetBits(new List<Code>() {1,25}));});

            // requires all codes within same unicode plane
            Assert.Throws <ArgumentException> (delegate{csp = new CodeSetPage (new CodeSetBits(new List<Code>() {12,66000}));});

            csp = new CodeSetPage (new CodeSetBits(new BitSetArray() {0,1,12,33,65535}));
            CodeSetPage clone = new CodeSetPage (csp);
        }
        
    }
}
