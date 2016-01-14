/*
 * Created by SharpDevelop.
 * User: ddur
 * Date: 14.1.2016.
 * Time: 11:29
 * 
 */
using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetStaticFactoryTest
{
    [TestFixture]
    public class Union
    {

        [Test]
        public void OfNullSets()
        {
            var ics_a = (ICodeSet) null;
            var ics_b = (ICodeSet) null;
            var ics_union = ics_a.Union(ics_b);

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_union));
        }

        [Test]
        public void EmptySets()
        {
            var ics_a = CodeSetNone.Singleton;
            var ics_b = CodeSetNone.Singleton;
            var ics_union = ics_a.Union(ics_b);

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_union));
        }

        [Test]
        public void EqualSets()
        {
            var ics_a = Factory.From('a');
            var ics_b = Factory.From('a');
            var ics_c = Factory.From('a');
            var ics_union = ics_a.Union(ics_b, ics_c);

            Assert.True (ics_union.SequenceEqual(new Code('a')));
        }

        [Test]
        public void Other()
        {
            var ics_a = Factory.From('a');
            var ics_b = Factory.From('b');
            var ics_c = Factory.From('d');
            var ics_union = ics_a.Union(ics_b, ics_c);

            Assert.True (ics_union.SequenceEqual("abd".ToICodeSet()));
        }
    }
}
