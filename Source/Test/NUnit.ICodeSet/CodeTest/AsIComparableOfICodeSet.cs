// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeTest
{
    [TestFixture]
    public class AsIComparableOfICodeSet
    {
        [Test]
        public void Compare()
        {
            var r = new Random();
            Code C = r.Next(Code.MinValue, Code.MaxValue/2);
            Code D = C;
            while (D <= C) {
                D = r.Next(Code.MaxValue/2, Code.MaxValue);
            }
            ICodeSet iC = C;
            ICodeSet iD = D;
            ICodeSet iX = CodeSetPair.From (C, D);

            Assert.True (iC.CompareTo(iD) != 0); // not equal
            Assert.True (iD.CompareTo(iC) != 0); // not equal
            Assert.True (iC.CompareTo(iC) == 0); // equal
            Assert.True (iD.CompareTo(iD) == 0); // equal

            Assert.True (iC.CompareTo(iX) != 0); // never equal (!=count)
            Assert.True (iX.CompareTo(iC) != 0); // never equal (!=count)
            Assert.True (iC.CompareTo(iX) < 0);
            Assert.True (iX.CompareTo(iC) > 0);
            
            Assert.True (iC.CompareTo (iD) == C.CompareTo(D));
            Assert.True (iD.CompareTo (iC) == D.CompareTo(C));
            Assert.True (iC.CompareTo (iC) == C.CompareTo(C));
            Assert.True (iD.CompareTo (iD) == D.CompareTo(D));
        }
    }
}
