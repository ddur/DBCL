﻿// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.CodeTest
{
    [TestFixture]
    public class AsICodeSet
    {
        
        [Test]
        public void Interface()
        {
            Random r = new Random();
            Code C = r.Next(Code.MinValue, Code.MaxValue);
            Code D = C;
            while (D == C) {
                D = r.Next(Code.MinValue, Code.MaxValue);
            }
            ICodeSet iC = C;
            Assert.True  (iC[C]);
            Assert.False  (iC[D]);

            Assert.True  (iC.Count == 1);
            Assert.That  (iC.Length == 1);
            Assert.True  (iC.Count == iC.Length);
            Assert.True  (iC.First == C);
            Assert.True  (iC.Last == iC.First);
            Assert.True  (iC.Last == iC.First);
            Assert.True  (iC.Count == iC.Last - iC.First + 1);
            
            ICodeSet ics2 = D;
            Assert.False (iC.Equals(ics2));
            Assert.False (iC.GetHashCode() == ics2.GetHashCode());
            Assert.True  (iC.GetHashCode() == ICodeSetService.GetHashCode(iC));
            Assert.True  (ics2.GetHashCode() == ICodeSetService.GetHashCode(ics2));

            ics2 = C;
            Assert.True  (iC.Equals(ics2));
            Assert.True  (iC.GetHashCode() == ics2.GetHashCode());
            Assert.True  (iC.GetHashCode() == ICodeSetService.GetHashCode(iC));
            Assert.True  (ics2.GetHashCode() == ICodeSetService.GetHashCode(ics2));

        }
        
    }
}
