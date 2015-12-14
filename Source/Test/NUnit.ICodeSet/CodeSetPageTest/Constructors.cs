// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetPageTest {

    [TestFixture]
    public class Constructors {

        [Test]
        public void FromBitSetArray () {
            CodeSetPage csp;
            csp = CodeSetPage.From ( BitSetArray.From ( 1, 12, 33 ) );
        }

        [Test]
        public void FromBitSetArrayThrows () {
            CodeSetPage csp;

            // requires no null
            Assert.Throws<ArgumentNullException> ( delegate {
                csp = CodeSetPage.From ( (BitSetArray)null );
            } );

            // requires at least 3 members (> ICodeSetService.PairCount)
            Assert.Throws<InvalidOperationException> ( delegate {
                csp = CodeSetPage.From ( BitSetArray.Empty () );
            } );
            Assert.Throws<InvalidOperationException> ( delegate {
                csp = CodeSetPage.From ( BitSetArray.From ( 21 ) );
            } );
            Assert.Throws<InvalidOperationException> ( delegate {
                csp = CodeSetPage.From ( BitSetArray.From ( 12, 5 ) );
            } );

            // requires at least one NOT member
            Assert.Throws<InvalidOperationException> ( delegate {
                csp = CodeSetPage.From ( BitSetArray.From ( 1, 2, 3, 4, 5 ) );
            } );
            // as above, does not except range of codes (full)
            Assert.Throws<InvalidOperationException> ( delegate {
                csp = CodeSetPage.From ( BitSetArray.Size ( 10, true ) );
            } );

            // requires all codes within same unicode plane
            Assert.Throws<InvalidOperationException> ( delegate {
                csp = CodeSetPage.From ( BitSetArray.From ( 12, 66000 ) );
            } );
        }

        [Test]
        public void FromBitSetArrayAtOffsetThrows () {
            CodeSetPage csp;

            // requires valid offset
            Assert.Throws<IndexOutOfRangeException> ( delegate {
                csp = CodeSetPage.From ( BitSetArray.From ( 1, 2, 3, 4, 7 ), -1 );
            } );

            // requires valid offset
            Assert.Throws<IndexOutOfRangeException> ( delegate {
                csp = CodeSetPage.From ( BitSetArray.From ( Code.MaxValue - 10, Code.MaxValue - 7, Code.MaxValue - 5 ), 10 );
            } );
        }

        [Test]
        public void FromCodes () {
            CodeSetPage csp;
            csp = CodeSetPage.From ( new List<Code> () { 0, 1, 12, 33, 65535 } );
        }

        [Test]
        public void FromCodesThrows () {
            CodeSetPage csp;

            // requires no null
            Assert.Throws<ArgumentNullException> ( delegate {
                csp = CodeSetPage.From ( (IEnumerable<Code>)null );
            } );

            // requires at least 3 members (> ICodeSetService.PairCount)
            Assert.Throws<InvalidOperationException> ( delegate {
                csp = CodeSetPage.From ( new Code[0] );
            } );
            Assert.Throws<InvalidOperationException> ( delegate {
                csp = CodeSetPage.From ( new List<Code> () { 21 } );
            } );
            Assert.Throws<InvalidOperationException> ( delegate {
                csp = CodeSetPage.From ( new List<Code> () { 1, 25 } );
            } );

            // requires more than ICodeSetService.ListMaxCount NOT members
            Assert.Throws<InvalidOperationException> ( delegate {
                csp = CodeSetPage.From ( new List<Code> () { 1, 2, 3, 4, 5 } );
            } );

            // requires all codes within same unicode plane
            Assert.Throws<InvalidOperationException> ( delegate {
                csp = CodeSetPage.From ( new List<Code> () { 12, 25, 66, 66000 } );
            } );

            // does not except full-range of codes
            Assert.Throws<InvalidOperationException> ( delegate {
                csp = CodeSetPage.From ( Enumerable.Range ( 1, 80 ).Select ( item => (Code)item ) );
            } );
            Assert.Throws<InvalidOperationException> ( delegate {
                csp = CodeSetPage.From ( Enumerable.Range ( Code.MaxValue - 10, 10 ).Select ( item => (Code)item ) );
            } );
        }

        [Test]
        public void FromICodeSet () {
            ICodeSet icsp;
            icsp = CodeSetPage.From ( BitSetArray.From ( 0, 1, 12, 33, 65535 ) );
            var clone = CodeSetPage.From ( icsp );
        }

        [Test]
        public void FromICodeSetThrows () {
            ICodeSet icsp;

            // requires no null
            Assert.Throws<ArgumentNullException> ( delegate {
                icsp = CodeSetPage.From ( (ICodeSet)null );
            } );

            // requires at least 3 members (> ICodeSetService.PairCount)
            Assert.Throws<InvalidOperationException> ( delegate {
                icsp = CodeSetPage.From ( new Code[0] );
            } );
            Assert.Throws<InvalidOperationException> ( delegate {
                icsp = CodeSetPage.From ( new List<Code> () { 21 } );
            } );
            Assert.Throws<InvalidOperationException> ( delegate {
                icsp = CodeSetPage.From ( new List<Code> () { 1, 25 } );
            } );

            // requires at least one NOT member
            Assert.Throws<InvalidOperationException> ( delegate {
                icsp = CodeSetPage.From ( new List<Code> () { 1, 2, 3, 4, 5 } );
            } );

            // requires all codes within same unicode plane
            Assert.Throws<InvalidOperationException> ( delegate {
                icsp = CodeSetPage.From ( new List<Code> () { 12, 66000 } );
            } );
        }
    }
}