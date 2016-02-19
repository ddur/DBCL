// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetStaticFactoryTest
{
    [TestFixture]
    public class OpDifference
    {

        [Test]
        public void OfNullSets()
        {
            var ics_a = (ICodeSet) null;
            var ics_b = (ICodeSet) null;
            var ics_difference = ics_a.Difference(ics_b);

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_difference));
        }

        [Test]
        public void EmptySets()
        {
            var ics_a = CodeSetNone.Singleton;
            var ics_b = CodeSetNone.Singleton;
            var ics_difference = ics_a.Difference(ics_b);

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_difference));
        }

        [Test]
        public void Equal2Sets()
        {
            var ics_a = ICodeSetFactory.From('a');
            var ics_b = ICodeSetFactory.From('a');
            var ics_difference = ics_a.Difference(ics_b);

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_difference));
        }

        [Test]
        public void Equal3Sets()
        {
            var ics_a = ICodeSetFactory.From('a');
            var ics_b = ICodeSetFactory.From('a');
            var ics_c = ICodeSetFactory.From('a');
            var ics_difference = ics_a.Difference(ics_b, ics_c);

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_difference));
        }

        [Test]
        public void Overlaps_Not()
        {
            var ics_a = ICodeSetFactory.From('a');
            var ics_b = ICodeSetFactory.From('b');
            var ics_c = ICodeSetFactory.From('d');
            var ics_difference = ics_a.Difference(ics_b, ics_c);

            Assert.True (ics_difference.Equals(ics_a));
        }

        [Test]
        public void Overlaps_Yes()
        {
            var ics_a = ICodeSetFactory.From('a', 'b', 'c', 'd', 'e', 'f' );
            var ics_b = ICodeSetFactory.From(          'c', 'd', 'e', 'f', 'g', 'h' );
            var ics_c = ICodeSetFactory.From(               'd', 'e');
            var ics_difference = ics_a.Difference(ics_b, ics_c);

            Assert.True (ics_difference.Equals(ICodeSetFactory.From ('a', 'b')));
        }

        [Test]
        public void Throws_ArgumentNullException_WhenNullOperands()
        {
            Assert.Throws ( typeof(ArgumentNullException),
                    delegate {
                               ((List<ICodeSet>)null).Difference();
                    });
        }

        [Test]
        public void Throws_ArgumentException_WhenNoOperands()
        {
            Assert.Throws ( typeof(ArgumentException),
                    delegate {
                               (new List<ICodeSet>()).Difference();
                    });
        }

        [Test]
        public void Throws_ArgumentException_WhenOneOperand()
        {
            var ics_a = ICodeSetFactory.From('a', 'b', 'c', 'd', 'e', 'f' );
            Assert.Throws ( typeof(ArgumentException),
                    delegate {
                               (new List<ICodeSet>() {ics_a}).Difference();
                    });
        }
    }
}
