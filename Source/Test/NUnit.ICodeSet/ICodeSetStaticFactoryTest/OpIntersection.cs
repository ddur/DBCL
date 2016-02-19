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
    public class OpIntersection
    {
 
        [Test]
        public void OfNullSets()
        {
            var ics_a = (ICodeSet) null;
            var ics_b = (ICodeSet) null;
            var ics_intersection = ics_a.Intersection(ics_b);

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_intersection));
        }

        [Test]
        public void EmptySets()
        {
            var ics_a = CodeSetNone.Singleton;
            var ics_b = CodeSetNone.Singleton;
            var ics_intersection = ics_a.Intersection(ics_b);

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_intersection));
        }

        [Test]
        public void EqualSets()
        {
            var ics_a = ICodeSetFactory.From('a');
            var ics_b = ICodeSetFactory.From('a');
            var ics_c = ICodeSetFactory.From('a');
            var ics_intersection = ics_a.Intersection(ics_b, ics_c);

            Assert.True (ics_intersection.SequenceEqual(new Code('a')));
        }

        [Test]
        public void Overlaps_Not()
        {
            var ics_a = ICodeSetFactory.From('a');
            var ics_b = ICodeSetFactory.From('b');
            var ics_c = ICodeSetFactory.From('d');
            var ics_intersection = ics_a.Intersection(ics_b, ics_c);

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_intersection));
        }

        [Test]
        public void Overlaps_Yes()
        {
            var ics_a = ICodeSetFactory.From('a', 'b', 'c', 'd', 'e', 'f' );
            var ics_b = ICodeSetFactory.From(          'c', 'd', 'e', 'f', 'g', 'h' );
            var ics_c = ICodeSetFactory.From(               'd', 'e');
            var ics_intersection = ics_a.Intersection(ics_b, ics_c);

            Assert.True (ics_intersection.Equals(ics_c));
        }

        [Test]
        public void Throws_ArgumentNullException_WhenNullOperands()
        {
            Assert.Throws ( typeof(ArgumentNullException),
                    delegate {
                               ((List<ICodeSet>)null).Intersection();
                    });
        }

        [Test]
        public void Throws_ArgumentException_WhenNoOperands()
        {
            Assert.Throws ( typeof(ArgumentException),
                    delegate {
                               (new List<ICodeSet>()).Intersection();
                    });
        }

        [Test]
        public void Throws_ArgumentException_WhenOneOperand()
        {
            Assert.Throws ( typeof(ArgumentException),
                    delegate {
                               (new List<ICodeSet>()).Intersection();
                    });
        }
    }
}
