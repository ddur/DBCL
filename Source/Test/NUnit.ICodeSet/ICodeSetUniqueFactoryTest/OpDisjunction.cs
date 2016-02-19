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
    public class OpDisjunction
    {
        [Test]
        public void OfNullSets()
        {
            var distinct = new DistinctICodeSet();

            var ics_a = distinct.From ((ICodeSet) null);
            var ics_b = distinct.From ((ICodeSet) null);
            var ics_disjunction = distinct.Disjunction(ics_a, ics_b);

            Assert.False (distinct.Contains(ics_disjunction));
            Assert.False (distinct.Contains(CodeSetNone.Singleton));

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_disjunction));
            Assert.True (distinct.Count == 0);
        }

        [Test]
        public void EmptySets()
        {
            var distinct = new DistinctICodeSet();

            var ics_a = distinct.From (CodeSetNone.Singleton);
            var ics_b = distinct.From (CodeSetNone.Singleton);
            var ics_disjunction = distinct.Disjunction(ics_a, ics_b);

            Assert.False (distinct.Contains(ics_disjunction));
            Assert.False (distinct.Contains(CodeSetNone.Singleton));

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_disjunction));
            Assert.True (distinct.Count == 0);
        }

        [Test]
        public void Equal2Sets()
        {
            var distinct = new DistinctICodeSet();

            var ics_a = distinct.From('a');
            var ics_b = distinct.From('a');
            var ics_disjunction = distinct.Disjunction(ics_a, ics_b);

            Assert.False (distinct.Contains(ics_disjunction));
            Assert.False (distinct.Contains(CodeSetNone.Singleton));

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_disjunction));
            Assert.True (distinct.Contains(ics_a));
            Assert.True (distinct.Contains(ics_b));
            Assert.True (distinct.Count == 1);
        }

        [Test]
        public void Equal3Sets()
        {
            var distinct = new DistinctICodeSet();

            var ics_a = distinct.From('a');
            var ics_b = distinct.From('a');
            var ics_c = distinct.From('a');
            var ics_disjunction = distinct.Disjunction(ics_a, ics_b, ics_c); // evaluates (((a xor b) xor c) xor d ...

            Assert.True (distinct.Contains(ics_disjunction));
            Assert.False (distinct.Contains(CodeSetNone.Singleton));

            Assert.True (distinct.Contains(ics_a));
            Assert.True (distinct.Contains(ics_b));
            Assert.True (distinct.Contains(ics_c));
            Assert.True (distinct.Contains(ics_disjunction));
            Assert.True (distinct.Count == 1);

            Assert.True (ReferenceEquals (ics_a, ics_disjunction));
            Assert.True (ReferenceEquals (ics_b, ics_disjunction));
            Assert.True (ReferenceEquals (ics_c, ics_disjunction));
        }

        [Test]
        public void Overlaps_Not()
        {
            var distinct = new DistinctICodeSet();

            var ics_a = distinct.From('a');
            var ics_b = distinct.From('b');
            var ics_c = distinct.From('d');
            var ics_disjunction = distinct.Disjunction(ics_a, ics_b, ics_c); // evaluates (((a xor b) xor c) xor d ...

            Assert.False (distinct.Contains(CodeSetNone.Singleton));

            Assert.True (distinct.Contains(ics_disjunction));
            Assert.True (distinct.Contains(ics_a));
            Assert.True (distinct.Contains(ics_b));
            Assert.True (distinct.Contains(ics_c));
            Assert.True (distinct.Count == 4);

            Assert.True (ReferenceEquals (ics_disjunction, distinct.From ("abd")));
        }

        [Test]
        public void Overlaps_Yes()
        {
            var distinct = new DistinctICodeSet();

            var ics_a = distinct.From('a', 'b', 'c', 'd', 'e', 'f' );
            var ics_b = distinct.From(          'c', 'd', 'e', 'f', 'g', 'h' ); // evaluates (((a xor b) xor c) xor d ...
            var ics_c = distinct.From(               'd', 'e');
            var ics_disjunction = distinct.Disjunction(ics_a, ics_b, ics_c);

            Assert.False (distinct.Contains(CodeSetNone.Singleton));

            Assert.True (distinct.Contains(ics_disjunction));
            Assert.True (distinct.Contains(ics_a));
            Assert.True (distinct.Contains(ics_b));
            Assert.True (distinct.Contains(ics_c));
            Assert.True (distinct.Count == 4);

            Assert.True (ics_disjunction.Equals(distinct.From ('a', 'b', 'd', 'e', 'g', 'h')));
            Assert.True (ReferenceEquals (ics_disjunction, distinct.From ('a', 'b', 'd', 'e', 'g', 'h')));
        }

        [Test]
        public void Throws_ArgumentNullException_WhenNullOperands()
        {
            var distinct = new DistinctICodeSet();

            Assert.Throws ( typeof(ArgumentNullException),
                    delegate {
                               distinct.Disjunction ((List<ICodeSet>)null);
                    });
        }

        [Test]
        public void Throws_ArgumentException_WhenNoOperands()
        {
            var distinct = new DistinctICodeSet();

            Assert.Throws ( typeof(ArgumentException),
                    delegate {
                               distinct.Disjunction (new List<ICodeSet>());
                    });
        }

        [Test]
        public void Throws_ArgumentException_WhenOneOperand()
        {
            var distinct = new DistinctICodeSet();
            var ics_a = distinct.From('a', 'b', 'c', 'd', 'e', 'f' );

            Assert.Throws ( typeof(ArgumentException),
                    delegate {
                               distinct.Disjunction (new List<ICodeSet>() {ics_a});
                    });
        }
    }
}
