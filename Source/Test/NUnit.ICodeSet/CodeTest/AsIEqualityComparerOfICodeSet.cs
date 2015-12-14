// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeTest {

    [TestFixture]
    public class AsIEqualityComparerOfICodeSet {

        [Test]
        public void Compare () {
            Random r = new Random ();
            Code C = r.Next (Code.MinValue, Code.MaxValue);
            Code D = C;
            while (D == C) {
                D = r.Next (Code.MinValue, Code.MaxValue);
            }
            ICodeSet iC = C;
            ICodeSet iD = D;

            Assert.True (iC.Equals (iC, iC));
            Assert.True (iC.Equals (iD, iD));
            Assert.True (iD.Equals (iC, iC));
            Assert.True (iD.Equals (iD, iD));

            Assert.True (iC.Equals (iC, iD) == iC.Equals (iD, iC));
            Assert.True (iD.Equals (iC, iD) == iD.Equals (iD, iC));
            Assert.True (iC.Equals (iC, iD) == iD.Equals (iD, iC));
            Assert.True (iC.Equals (iC, iD) == iC.Equals (iD, iC));

            Assert.True (iC.GetHashCode (iC) == iC.GetHashCode ());
            Assert.True (iC.GetHashCode (iD) == iD.GetHashCode ());
            Assert.True (iD.GetHashCode (iC) == iC.GetHashCode ());
            Assert.True (iD.GetHashCode (iD) == iD.GetHashCode ());

            Assert.True (iC.GetHashCode (iC) == C.GetHashCode ());
            Assert.True (iC.GetHashCode (iD) == D.GetHashCode ());
            Assert.True (iD.GetHashCode (iC) == C.GetHashCode ());
            Assert.True (iD.GetHashCode (iD) == D.GetHashCode ());
        }
    }
}