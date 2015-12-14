// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Linq;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeTest {

    [TestFixture]
    public class Compiler {

        [Test]
        public void Configuration () {
            int testValue = UInt16.MinValue - 1;
#if DEBUG
            Assert.That ( delegate { int x = (char)testValue; }, Throws.TypeOf<OverflowException> () );
#else
	    	Assert.That ( delegate { int x = (char)testValue;}, Throws.Nothing );
#endif
            Assert.That ( delegate { int x = checked ( (char)testValue ); }, Throws.TypeOf<OverflowException> () );
            Assert.That ( delegate { int x = unchecked ( (char)testValue ); }, Throws.Nothing );
        }

        [Test]
        public void CharType () {
            Assert.True ( UInt16.MaxValue == 0xFFFF );
            Assert.True ( UInt16.MinValue == 0 );

            Assert.True ( UInt16.MaxValue == char.MaxValue );
            Assert.True ( UInt16.MinValue == char.MinValue );

            // prove that char type accepts all UInt16 values (including surrogates, invalid and unassigned)
            char c = (char)UInt16.MinValue;
            for (uint i = UInt16.MinValue; i <= UInt16.MaxValue; i++) {
                Assert.That ( delegate { c = (char)i; }, Throws.Nothing );
                Assert.That ( (int)c == i, Is.True );
            }
            Assert.That ( ((int)c) == UInt16.MaxValue, Is.True );

            // prove that char type cannot contain invalid value ( == throws exception on invalid value cast)
            int testValue;
            testValue = int.MinValue;
            Assert.That ( delegate { int x = checked ( (char)testValue ); }, Throws.Exception );
            testValue = UInt16.MinValue - 1;
            Assert.That ( delegate { int x = checked ( (char)testValue ); }, Throws.Exception );
            testValue = UInt16.MaxValue + 1;
            Assert.That ( delegate { int x = checked ( (char)testValue ); }, Throws.Exception );
            testValue = int.MaxValue;
            Assert.That ( delegate { int x = checked ( (char)testValue ); }, Throws.Exception );

            testValue = UInt16.MinValue - 1;
            Assert.That ( ((int)unchecked ( (char)testValue )).InRange ( UInt16.MinValue, UInt16.MaxValue ), Is.True );
            testValue = int.MinValue;
            Assert.That ( ((int)unchecked ( (char)testValue )).InRange ( UInt16.MinValue, UInt16.MaxValue ), Is.True );
            testValue = UInt16.MaxValue + 1;
            Assert.That ( ((int)unchecked ( (char)testValue )).InRange ( UInt16.MinValue, UInt16.MaxValue ), Is.True );
            testValue = int.MaxValue;
            Assert.That ( ((int)unchecked ( (char)testValue )).InRange ( UInt16.MinValue, UInt16.MaxValue ), Is.True );
        }
    }
}