// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;
using DD.Collections.ICodeSet;

namespace DD.Collections.ICodeSet.ICodeSetStaticFactoryTest
{
    [TestFixture]
    public class OpComplement
    {

        [Test]
        public void OfNull()
        {
            var ics = (ICodeSet)null;
            var icsComplement = ics.Complement();
            Assert.True (ReferenceEquals (CodeSetNone.Singleton, icsComplement));
        }

        [Test]
        public void OfEmpty()
        {
            var ics = (ICodeSet)CodeSetNone.Singleton;
            var icsComplement = ics.Complement();
            Assert.True (ReferenceEquals (CodeSetNone.Singleton, icsComplement));
        }

        [Test]
        public void OfOne()
        {
            var ics = (ICodeSet)(Code)('x');
            var icsComplement = ics.Complement();
            Assert.True (ReferenceEquals (CodeSetNone.Singleton, icsComplement));
        }

        [Test]
        public void OfPairXY()
        {
            var ics = (ICodeSet)CodeSetPair.From('x','y');
            var icsComplement = ics.Complement();
            Assert.True (ReferenceEquals (CodeSetNone.Singleton, icsComplement));
        }

        [Test]
        public void OfPairXZ()
        {
            var ics = (ICodeSet)CodeSetPair.From('x','z');
            var icsComplement = ics.Complement();
            Assert.False (ReferenceEquals (CodeSetNone.Singleton, icsComplement));
            Assert.True (icsComplement.Count == 1);
        }

        [Test]
        public void OfFullSet()
        {
            var ics = (ICodeSet)CodeSetFull.From('x','z');
            var icsComplement = ics.Complement();
            Assert.True (ReferenceEquals (CodeSetNone.Singleton, icsComplement));
        }

        [Test]
        public void OfOther()
        {
            var ics = ICodeSetFactory.From('a', 'b', 'e', 'f', 'g', 'h', 'j');
            var icsComplement = ics.Complement();
            Assert.False (ReferenceEquals (CodeSetNone.Singleton, icsComplement));
            Assert.True (icsComplement.SequenceEqual("cdi".ToICodeSet()));
        }
    }
}
