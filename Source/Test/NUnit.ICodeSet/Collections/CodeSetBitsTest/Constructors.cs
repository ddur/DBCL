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
    public class Constructors
    {
        
        [Test]
        public void Empty()
        {
            CodeSetBits csb = new CodeSetBits();

        }
        
        [Test]
        public void FromCodes()
        {
            Assert.Throws <ArgumentNullException> (delegate{new CodeSetBits ((IEnumerable<Code>)null);});
            CodeSetBits csb;
            csb = new CodeSetBits (new List<Code>());
            csb = new CodeSetBits (new List<Code>() {1,12,33,20});
        }
        
        [Test]
        public void FromICodeSet()
        {
            Assert.Throws <ArgumentNullException> (delegate{new CodeSetBits ((ICodeSet)null);});
            ICodeSet input;
            CodeSetBits csb;
            input = ICodeSetFactory.Empty;
            csb = new CodeSetBits (input);

            input = ICodeSetFactory.From(new List<Code>() {1,12,33,20});
            csb = new CodeSetBits (input);
            csb = new CodeSetBits (csb);
        }
        
        [Test]
        public void FromBitSetArray()
        {
            Assert.Throws <ArgumentNullException> (delegate{new CodeSetBits ((BitSetArray)null);});
            CodeSetBits csb;
            csb = new CodeSetBits (new BitSetArray());
            csb = new CodeSetBits (new BitSetArray() {33});
            csb = new CodeSetBits (new BitSetArray() {1,12,33});
        }
        
        [Test]
        public void FromCompact()
        {
            Assert.Throws <ArgumentNullException> (delegate{new CodeSetBits ((BitSetArray)null, 0);});
            Assert.Throws <ArgumentException> (delegate{new CodeSetBits (new BitSetArray(), 0);});
            Assert.Throws <ArgumentException> (delegate{new CodeSetBits (new BitSetArray() {0,1}, -1);});
            Assert.Throws <ArgumentException> (delegate{new CodeSetBits (new BitSetArray() {0,1,12,33}, -1);});
            Assert.Throws <ArgumentException> (delegate{new CodeSetBits (new BitSetArray() {0,1,12,Code.MaxCount}, 0);});
            Assert.Throws <ArgumentException> (delegate{new CodeSetBits (new BitSetArray() {0,1,12,Code.MaxValue}, 1);});

            CodeSetBits csb;
            csb = new CodeSetBits (new BitSetArray() {0},Code.MaxValue);
            csb = new CodeSetBits (new BitSetArray() {0,1});
            csb = new CodeSetBits (new BitSetArray() {0,1,12,33});
            csb = new CodeSetBits (new BitSetArray() {0,1,12,33,Code.MaxValue});
        }
        
        
    }
}
