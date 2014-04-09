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
    public class AsIEqualityComparerOfCode
    {
        [Test]
        public void Compare()
        {
            Random r = new Random();
            Code C = r.Next(Code.MinValue, Code.MaxValue);
            Code D = C;
            while (D == C) {
                D = r.Next(Code.MinValue, Code.MaxValue);
            }
            
            Assert.True (C.Equals(C, C));
            Assert.True (C.Equals(D, D));
            Assert.True (D.Equals(C, C));
            Assert.True (D.Equals(D, D));

            Assert.True (C.Equals(C, D) == C.Equals(D, C));
            Assert.True (D.Equals(C, D) == D.Equals(D, C));
            Assert.True (C.Equals(C, D) == D.Equals(D, C));
            Assert.True (C.Equals(C, D) == C.Equals(D, C));
            
            Assert.True (C.GetHashCode(C) == C.GetHashCode());
            Assert.True (C.GetHashCode(D) == D.GetHashCode());
            Assert.True (D.GetHashCode(C) == C.GetHashCode());
            Assert.True (D.GetHashCode(D) == D.GetHashCode());
        }
    }
}
