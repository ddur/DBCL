// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeTest {

    [TestFixture]
    public class AsICollectionOfCode {

        [Test]
        public void NotSupported () {
            var r = new Random ();
            Code C = r.Next ( Code.MinValue, Code.MaxValue );
            Code D = r.Next ( Code.MinValue, Code.MaxValue );
            ICollection<Code> iC = C;

            Assert.That ( delegate { iC.Add ( C ); }, Throws.TypeOf<NotSupportedException> () );
            Assert.Throws<NotSupportedException> ( delegate { ((ICodeSet)D).Add ( C ); } );
            Assert.Throws<NotSupportedException> ( delegate { iC.Add ( D ); } );
            Assert.Throws<NotSupportedException> ( delegate { iC.Remove ( C ); } );
            Assert.Throws<NotSupportedException> ( delegate { iC.Remove ( D ); } );
            Assert.Throws<NotSupportedException> ( delegate { iC.Clear (); } );
        }

        [Test]
        public void ReadOnlyIsTrue () {
            var r = new Random ();
            Code C = r.Next ( Code.MinValue, Code.MaxValue );
            ICollection<Code> iC = C;

            // allways read only
            Assert.True ( iC.IsReadOnly );

            // always 1 member
            Assert.True ( iC.Count == 1 );
        }

        [Test]
        public void CountIsOne () {
            var r = new Random ();
            Code C = r.Next ( Code.MinValue, Code.MaxValue );
            ICollection<Code> iC = C;

            // always 1 member
            Assert.True ( iC.Count == 1 );
        }

        [Test]
        public void ContainsOneCode () {
            var r = new Random ();
            Code C = r.Next ( Code.MinValue, Code.MaxValue );
            Code D = r.Next ( Code.MinValue, Code.MaxValue );
            ICollection<Code> iC = C;

            // Contains
            Assert.True ( iC.Contains ( D ) == (C == D) );
        }

        [Test]
        public void CopyToArray () {
            var r = new Random ();
            Code C = r.Next ( Code.MinValue, Code.MaxValue );

            ICollection<Code> iC = C;

            // CopyTo
            var arrayC = new Code[2];
            iC.CopyTo ( arrayC, 0 );
            Assert.True ( arrayC[0] == C );
            iC.CopyTo ( arrayC, 1 );
            Assert.True ( arrayC[1] == C );

            Assert.Throws<IndexOutOfRangeException> ( delegate { iC.CopyTo ( arrayC, -1 ); } );
            Assert.Throws<IndexOutOfRangeException> ( delegate { iC.CopyTo ( arrayC, 2 ); } );

            arrayC = new Code[0];
            Assert.Throws<IndexOutOfRangeException> ( delegate { iC.CopyTo ( arrayC, -1 ); } );
            Assert.Throws<IndexOutOfRangeException> ( delegate { iC.CopyTo ( arrayC, 0 ); } );
            Assert.Throws<IndexOutOfRangeException> ( delegate { iC.CopyTo ( arrayC, 1 ); } );
            Assert.Throws<IndexOutOfRangeException> ( delegate { iC.CopyTo ( arrayC, 2 ); } );

            arrayC = null;
            Assert.Throws<ArgumentNullException> ( delegate { iC.CopyTo ( arrayC, 0 ); } );
        }
    }
}