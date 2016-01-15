﻿/*
 * Created by SharpDevelop.
 * User: ddur
 * Date: 15.1.2016.
 * Time: 10:28
 * 
 */
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetUniqueFactoryTest
{
    [TestFixture]
    public class OpDifference
    {
        [Test]
        public void OfNullSets()
        {
            var distinct = new Distinct();

            var ics_a = distinct.From ((ICodeSet) null);
            var ics_b = distinct.From ((ICodeSet) null);
            var ics_difference = distinct.Difference(ics_a, ics_b);

            Assert.False (distinct.Contains(ics_difference));
            Assert.False (distinct.Contains(CodeSetNone.Singleton));

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_difference));
            Assert.True (distinct.Count == 0);
        }

        [Test]
        public void EmptySets()
        {
            var distinct = new Distinct();

            var ics_a = distinct.From (CodeSetNone.Singleton);
            var ics_b = distinct.From (CodeSetNone.Singleton);
            var ics_difference = distinct.Difference(ics_a, ics_b);

            Assert.False (distinct.Contains(ics_difference));
            Assert.False (distinct.Contains(CodeSetNone.Singleton));

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_difference));
            Assert.True (distinct.Count == 0);
        }

        [Test]
        public void Equal2Sets()
        {
            var distinct = new Distinct();

            var ics_a = distinct.From('a');
            var ics_b = distinct.From('a');
            var ics_difference = distinct.Difference(ics_a, ics_b);

            Assert.False (distinct.Contains(ics_difference));
            Assert.False (distinct.Contains(CodeSetNone.Singleton));

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_difference));
            Assert.True (distinct.Contains(ics_a));
            Assert.True (distinct.Contains(ics_b));
            Assert.True (distinct.Count == 1);
        }

        [Test]
        public void Equal3Sets()
        {
            var distinct = new Distinct();

            var ics_a = distinct.From('a');
            var ics_b = distinct.From('a');
            var ics_c = distinct.From('a');
            var ics_difference = distinct.Difference(ics_a, ics_b, ics_c);

            Assert.False (distinct.Contains(ics_difference));
            Assert.False (distinct.Contains(CodeSetNone.Singleton));

            Assert.True (distinct.Contains(ics_a));
            Assert.True (distinct.Contains(ics_b));
            Assert.True (distinct.Contains(ics_c));
            Assert.True (distinct.Count == 1);

            Assert.True (ReferenceEquals (CodeSetNone.Singleton, ics_difference));
        }

        [Test]
        public void Overlaps_Not()
        {
            var distinct = new Distinct();

            var ics_a = distinct.From('a');
            var ics_b = distinct.From('b');
            var ics_c = distinct.From('d');
            var ics_difference = distinct.Difference(ics_a, ics_b, ics_c);

            Assert.False (distinct.Contains(CodeSetNone.Singleton));

            Assert.True (distinct.Contains(ics_difference));
            Assert.True (distinct.Contains(ics_a));
            Assert.True (distinct.Contains(ics_b));
            Assert.True (distinct.Contains(ics_c));
            Assert.True (distinct.Count == 3);

            Assert.True (ics_difference.Equals(ics_a));
            Assert.True (ReferenceEquals (ics_difference, ics_a));
        }

        [Test]
        public void Overlaps_Yes()
        {
            var distinct = new Distinct();

            var ics_a = distinct.From('a', 'b', 'c', 'd', 'e', 'f' );
            var ics_b = distinct.From(          'c', 'd', 'e', 'f', 'g', 'h' );
            var ics_c = distinct.From(               'd', 'e');
            var ics_difference = distinct.Difference(ics_a, ics_b, ics_c);

            Assert.False (distinct.Contains(CodeSetNone.Singleton));

            Assert.True (distinct.Contains(ics_difference));
            Assert.True (distinct.Contains(ics_a));
            Assert.True (distinct.Contains(ics_b));
            Assert.True (distinct.Contains(ics_c));
            Assert.True (distinct.Count == 4);

            Assert.True (ics_difference.Equals(distinct.From ('a', 'b')));
            Assert.True (ReferenceEquals (ics_difference, distinct.From ('a', 'b')));
        }

        [Test]
        public void Throws_ArgumentNullException_WhenNullOperands()
        {
            var distinct = new Distinct();

            Assert.Throws ( typeof(ArgumentNullException),
                    delegate {
                               distinct.Difference ((List<ICodeSet>)null);
                    });
        }

        [Test]
        public void Throws_ArgumentException_WhenNoOperands()
        {
            var distinct = new Distinct();

            Assert.Throws ( typeof(ArgumentException),
                    delegate {
                               distinct.Difference (new List<ICodeSet>());
                    });
        }

        [Test]
        public void Throws_ArgumentException_WhenOneOperand()
        {
            var distinct = new Distinct();
            var ics_a = distinct.From('a', 'b', 'c', 'd', 'e', 'f' );

            Assert.Throws ( typeof(ArgumentException),
                    delegate {
                               distinct.Difference (new List<ICodeSet>() {ics_a});
                    });
        }
    }
}
