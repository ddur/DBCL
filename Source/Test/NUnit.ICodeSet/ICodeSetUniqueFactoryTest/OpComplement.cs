﻿// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetUniqueFactoryTest
{
    [TestFixture]
    public class OpComplement
    {
        readonly DistinctICodeSet distinct = new DistinctICodeSet();

        [Test]
        public void OfNull()
        {
            var ics = (ICodeSet)null;
            var icsComplement = distinct.Complement(ics);
            Assert.True (ReferenceEquals (CodeSetNone.Singleton, icsComplement));
        }

        [Test]
        public void OfEmpty()
        {
            var ics = (ICodeSet)CodeSetNone.Singleton;
            var icsComplement = distinct.Complement(ics);
            Assert.True (ReferenceEquals (CodeSetNone.Singleton, icsComplement));
        }

        [Test]
        public void OfOne()
        {
            var ics = (ICodeSet)(Code)('x');
            var icsComplement = distinct.Complement(ics);
            Assert.True (ReferenceEquals (CodeSetNone.Singleton, icsComplement));
        }

        [Test]
        public void OfPairXY()
        {
            var ics = (ICodeSet)CodeSetPair.From('x','y');
            var icsComplement = distinct.Complement(ics);
            Assert.True (ReferenceEquals (CodeSetNone.Singleton, icsComplement));
        }

        [Test]
        public void OfPairXZ()
        {
            var ics = (ICodeSet)CodeSetPair.From('x','z');
            var icsComplement = distinct.Complement(ics);
            Assert.False (ReferenceEquals (CodeSetNone.Singleton, icsComplement));
            Assert.True (icsComplement.Count == 1);
        }

        [Test]
        public void OfFullSet()
        {
            var ics = (ICodeSet)CodeSetFull.From('x','z');
            var icsComplement = distinct.Complement(ics);
            Assert.True (ReferenceEquals (CodeSetNone.Singleton, icsComplement));
        }

        [Test]
        public void OfOther()
        {
            var ics = distinct.From('a', 'b', 'e', 'f', 'g', 'h', 'j');
            var icsComplement = distinct.Complement(ics);
            Assert.False (ReferenceEquals (CodeSetNone.Singleton, icsComplement));
            Assert.True (icsComplement.SequenceEqual("cdi".ToICodeSet()));
        }
    }
}
