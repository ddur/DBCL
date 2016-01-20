// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetUniqueFactoryTest
{
    [TestFixture]
    public class OpUnion
    {
        [Test]
        public void OfNullSets()
        {
            var distinct = new Distinct();

            var ics_a = distinct.From ((ICodeSet) null);
            var ics_b = distinct.From ((ICodeSet) null);
            var ics_union = distinct.Union(ics_a, ics_b);

            Assert.False (distinct.Contains(ics_union));
            Assert.False (distinct.Contains(CodeSetNone.Singleton));

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_union));
            Assert.True (distinct.Count == 0);
        }

        [Test]
        public void EmptySets()
        {
            var distinct = new Distinct();

            var ics_a = distinct.From (CodeSetNone.Singleton);
            var ics_b = distinct.From (CodeSetNone.Singleton);
            var ics_union = distinct.Union(ics_a, ics_b);

            Assert.False (distinct.Contains(ics_union));
            Assert.False (distinct.Contains(CodeSetNone.Singleton));

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_union));
            Assert.True (distinct.Count == 0);
        }

        [Test]
        public void Equal2Sets()
        {
            var distinct = new Distinct();

            var ics_a = distinct.From('a');
            var ics_b = distinct.From('a');
            var ics_union = distinct.Union(ics_a, ics_b);

            Assert.True (distinct.Contains(ics_union));
            Assert.False (distinct.Contains(CodeSetNone.Singleton));

            Assert.True (distinct.Contains(ics_a));
            Assert.True (distinct.Contains(ics_b));
            Assert.True (distinct.Contains(ics_union));
            Assert.True (distinct.Count == 1);

            Assert.True (ReferenceEquals (ics_a, ics_union));
            Assert.True (ReferenceEquals (ics_b, ics_union));
        }

        [Test]
        public void Equal3Sets()
        {
            var distinct = new Distinct();

            var ics_a = distinct.From('a');
            var ics_b = distinct.From('a');
            var ics_c = distinct.From('a');
            var ics_union = distinct.Union(ics_a, ics_b, ics_c); // evaluates (((a and b) and c) and d ...

            Assert.True (distinct.Contains(ics_union));
            Assert.False (distinct.Contains(CodeSetNone.Singleton));

            Assert.True (distinct.Contains(ics_a));
            Assert.True (distinct.Contains(ics_b));
            Assert.True (distinct.Contains(ics_c));
            Assert.True (distinct.Contains(ics_union));
            Assert.True (distinct.Count == 1);

            Assert.True (ReferenceEquals (ics_a, ics_union));
            Assert.True (ReferenceEquals (ics_b, ics_union));
            Assert.True (ReferenceEquals (ics_c, ics_union));
        }

        [Test]
        public void Overlaps_Not()
        {
            var distinct = new Distinct();

            var ics_a = distinct.From('a');
            var ics_b = distinct.From('b');
            var ics_c = distinct.From('d');
            var ics_union = distinct.Union(ics_a, ics_b, ics_c); // evaluates (((a and b) and c) and d ...

            Assert.False (distinct.Contains(CodeSetNone.Singleton));

            Assert.True (distinct.Contains(ics_union));
            Assert.True (distinct.Contains(ics_a));
            Assert.True (distinct.Contains(ics_b));
            Assert.True (distinct.Contains(ics_c));
            Assert.True (distinct.Count == 4);

        }

        [Test]
        public void Overlaps_Yes()
        {
            var distinct = new Distinct();

            var ics_a = distinct.From('a', 'b', 'c', 'd', 'e', 'f' );
            var ics_b = distinct.From(          'c', 'd', 'e', 'f', 'g', 'h' );
            var ics_c = distinct.From(               'd', 'e');
            var ics_union = distinct.Union(ics_a, ics_b, ics_c); // evaluates (((a and b) and c) and d ...

            Assert.False (distinct.Contains(CodeSetNone.Singleton));

            Assert.True (distinct.Contains(ics_union));
            Assert.True (distinct.Contains(ics_a));
            Assert.True (distinct.Contains(ics_b));
            Assert.True (distinct.Contains(ics_c));
            Assert.True (distinct.Count == 4);

            Assert.True (ics_union.Equals(distinct.From ('a', 'b', 'c', 'd', 'e', 'f', 'g', 'h')));
            Assert.True (ReferenceEquals (ics_union, distinct.From ('a', 'b', 'c', 'd', 'e', 'f', 'g', 'h')));
        }

        [Test]
        public void Throws_ArgumentNullException_WhenNullOperands()
        {
            var distinct = new Distinct();

            Assert.Throws ( typeof(ArgumentNullException),
                    delegate {
                               distinct.Union ((List<ICodeSet>)null);
                    });
        }

        [Test]
        public void Throws_ArgumentException_WhenNoOperands()
        {
            var distinct = new Distinct();

            Assert.Throws ( typeof(ArgumentException),
                    delegate {
                               distinct.Union (new List<ICodeSet>());
                    });
        }

        [Test]
        public void Throws_ArgumentException_WhenOneOperand()
        {
            var distinct = new Distinct();
            var ics_a = distinct.From('a', 'b', 'c', 'd', 'e', 'f' );

            Assert.Throws ( typeof(ArgumentException),
                    delegate {
                               distinct.Union (new List<ICodeSet>() {ics_a});
                    });
        }
    }
}
