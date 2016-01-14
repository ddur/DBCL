/*
 * Created by SharpDevelop.
 * User: ddur
 * Date: 14.1.2016.
 * Time: 12:13
 * 
 */
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetStaticFactoryTest
{
    [TestFixture]
    public class Intersection
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
            var ics_a = Factory.From('a');
            var ics_b = Factory.From('a');
            var ics_c = Factory.From('a');
            var ics_intersection = ics_a.Intersection(ics_b, ics_c);

            Assert.True (ics_intersection.SequenceEqual(new Code('a')));
        }

        [Test]
        public void Overlaps_Not()
        {
            var ics_a = Factory.From('a');
            var ics_b = Factory.From('b');
            var ics_c = Factory.From('d');
            var ics_intersection = ics_a.Intersection(ics_b, ics_c);

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_intersection));
        }

        [Test]
        public void Overlaps_Yes()
        {
            var ics_a = Factory.From('a', 'b', 'c', 'd', 'e', 'f' );
            var ics_b = Factory.From(          'c', 'd', 'e', 'f', 'g', 'h' );
            var ics_c = Factory.From(               'd', 'e');
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
