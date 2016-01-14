/*
 * Created by SharpDevelop.
 * User: ddur
 * Date: 14.1.2016.
 * Time: 12:49
 * 
 */
using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetStaticFactoryTest
{
    [TestFixture]
    public class Disjunction
    {
 
        [Test]
        public void OfNullSets()
        {
            var ics_a = (ICodeSet) null;
            var ics_b = (ICodeSet) null;
            var ics_disjunction = ics_a.Disjunction(ics_b);

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_disjunction));
        }

        [Test]
        public void EmptySets()
        {
            var ics_a = CodeSetNone.Singleton;
            var ics_b = CodeSetNone.Singleton;
            var ics_disjunction = ics_a.Disjunction(ics_b);

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_disjunction));
        }

        [Test]
        public void Equal2Sets()
        {
            var ics_a = Factory.From('a');
            var ics_b = Factory.From('a');
            var ics_disjunction = ics_a.Disjunction(ics_b);

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_disjunction));
        }

        [Test]
        public void Equal3Sets()
        {
            var ics_a = Factory.From('a');
            var ics_b = Factory.From('a');
            var ics_c = Factory.From('a');
            var ics_disjunction = ics_a.Disjunction(ics_b, ics_c);

            Assert.True (ics_disjunction.Equals(ics_c));
        }

        [Test]
        public void Overlaps_Not()
        {
            var ics_a = Factory.From('a');
            var ics_b = Factory.From('b');
            var ics_c = Factory.From('d');
            var ics_disjunction = ics_a.Disjunction(ics_b, ics_c);

            Assert.True (ics_disjunction.Equals(ics_a.Union(ics_b, ics_c)));
        }

        [Test]
        public void Overlaps_Yes()
        {
            var ics_a = Factory.From('a', 'b', 'c', 'd', 'e', 'f' );
            var ics_b = Factory.From(          'c', 'd', 'e', 'f', 'g', 'h' );
            var ics_c = Factory.From(               'd', 'e');
            var ics_disjunction = ics_a.Disjunction(ics_b, ics_c);

            Assert.True (ics_disjunction.Equals(Factory.From ('a', 'b', 'd', 'e', 'g', 'h')));
        }
    }
}
