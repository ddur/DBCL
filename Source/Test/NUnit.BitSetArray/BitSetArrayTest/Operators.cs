// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;

using NUnit.Framework;

namespace DD.Collections.BitSetArrayTest {

    [TestFixture]
    public class Operators {
        private BitSetArray a;
        private BitSetArray b;
        private BitSetArray c;

        [Test]
        public void Cast () {
            a = null;
            BitArray casted = (BitArray)a;
            Assert.NotNull ( casted );
            a = BitSetArray.From ( 0, 1, 2, 3, 4, 5 );
            casted = (BitArray)a;
            Assert.True ( casted.Count == 6 );
            foreach (bool bit in casted) {
                Assert.True ( bit );
            }

            a = (BitSetArray)casted;
            Assert.True ( a.Count == 6 );
            Assert.True ( a.Length == 6 );

            casted = null;
            a = (BitSetArray)casted;
            Assert.NotNull ( a );
            Assert.True ( a.Length == 0 );
        }

        [Test]
        public void LeftOperandIsNull () {
            a = null;
            b = BitSetArray.From ( 1, 2, 3 );
            c = BitSetArray.Empty ();

            Assert.That ( (a | b).SetEquals ( b ) );
            Assert.That ( (a & b).SetEquals ( a ) );
            Assert.That ( (a ^ b).SetEquals ( b ) );
            Assert.That ( (a - b).SetEquals ( a ) );

            Assert.That ( !(a < c) );
            Assert.That ( (a == c) );
            Assert.That ( (a <= c) );

            Assert.That ( !(a > c) );
            Assert.That ( !(a != c) );
            Assert.That ( (a >= c) );

            Assert.That ( (a < b) );
            Assert.That ( !(a == b) );
            Assert.That ( (a <= b) );

            Assert.That ( !(a > b) );
            Assert.That ( (a != b) );
            Assert.That ( !(a >= b) );

            Assert.That ( ~a == c );
        }

        [Test]
        public void Equals () {
            a = BitSetArray.From ( 1, 2, 3 );
            b = BitSetArray.From ( 1, 2, 3 );
            c = BitSetArray.From ( new int[] { 1, 2, 3, 4 } );

            Assert.That ( a == b );
            Assert.That ( b == a );
            Assert.That ( a != c );
            Assert.That ( c != a );
            Assert.That ( b != c );
            Assert.That ( c != b );

            c = BitSetArray.Empty ();

            Assert.That ( a == b );
            Assert.That ( b == a );
            Assert.That ( a != c );
            Assert.That ( c != a );
            Assert.That ( b != c );
            Assert.That ( c != b );

            a = null;
            b = null;

            Assert.That ( a == b );
            Assert.That ( b == a );
            Assert.That ( a == c );
            Assert.That ( c == a );
            Assert.That ( b == c );
            Assert.That ( c == b );
        }

        [Test]
        public void Compare () {
            a = BitSetArray.From ( 1, 2, 3 );
            b = BitSetArray.From ( 1, 2, 3 );
            c = BitSetArray.From ( new int[] { 1, 2, 3, 4 } );

            Assert.That ( a == b );
            Assert.That ( b == a );
            Assert.That ( a >= b );
            Assert.That ( !(a < b) );
            Assert.That ( b >= a );
            Assert.That ( !(b < a) );
            Assert.That ( a <= b );
            Assert.That ( !(a > b) );
            Assert.That ( b <= a );
            Assert.That ( !(b > a) );

            Assert.That ( a < c );
            Assert.That ( a <= c );
            Assert.That ( !(a >= c) );
            Assert.That ( c > a );
            Assert.That ( c >= a );
            Assert.That ( !(c <= a) );

            Assert.That ( b < c );
            Assert.That ( b <= c );
            Assert.That ( !(b >= c) );
            Assert.That ( c > b );
            Assert.That ( c >= b );
            Assert.That ( !(c <= b) );

            a = null;
            b = null;

            Assert.That ( a == b );
            Assert.That ( b == a );
            Assert.That ( a >= b );
            Assert.That ( !(a < b) );
            Assert.That ( b >= a );
            Assert.That ( !(b < a) );
            Assert.That ( a <= b );
            Assert.That ( !(a > b) );
            Assert.That ( b <= a );
            Assert.That ( !(b > a) );

            Assert.That ( a < c );
            Assert.That ( a <= c );
            Assert.That ( !(a >= c) );
            Assert.That ( c > a );
            Assert.That ( c >= a );
            Assert.That ( !(c <= a) );

            Assert.That ( b < c );
            Assert.That ( b <= c );
            Assert.That ( !(b >= c) );
            Assert.That ( c > b );
            Assert.That ( c >= b );
            Assert.That ( !(c <= b) );

            b = BitSetArray.Empty ();

            Assert.That ( a == b );
            Assert.That ( b == a );
            Assert.That ( a >= b );
            Assert.That ( !(a < b) );
            Assert.That ( b >= a );
            Assert.That ( !(b < a) );
            Assert.That ( a <= b );
            Assert.That ( !(a > b) );
            Assert.That ( b <= a );
            Assert.That ( !(b > a) );

            Assert.That ( a < c );
            Assert.That ( a <= c );
            Assert.That ( !(a >= c) );
            Assert.That ( c > a );
            Assert.That ( c >= a );
            Assert.That ( !(c <= a) );

            Assert.That ( b < c );
            Assert.That ( b <= c );
            Assert.That ( !(b >= c) );
            Assert.That ( c > b );
            Assert.That ( c >= b );
            Assert.That ( !(c <= b) );
        }

        [Test]
        public void UnionOr () {
            a = BitSetArray.From ( 1, 2, 3 );
            b = BitSetArray.From ( 3, 4, 5 );
            c = BitSetArray.From ( new int[] { 1, 2, 3, 4, 5 } );

            Assert.That ( (a | b) == c );
            Assert.That ( (b | a) == c );
        }

        [Test]
        public void IntersectionAnd () {
            a = BitSetArray.From ( 1, 2, 3 );
            b = BitSetArray.From ( 3, 4, 5 );
            c = BitSetArray.From ( new int[] { 3 } );

            Assert.That ( (a & b) == c );
            Assert.That ( (b & a) == c );
        }

        [Test]
        public void SymmetricExceptXor () {
            a = BitSetArray.From ( 1, 2, 3 );
            b = BitSetArray.From ( 3, 4, 5 );
            c = BitSetArray.From ( new int[] { 1, 2, 4, 5 } );

            Assert.That ( (a ^ b) == c );
            Assert.That ( (b ^ a) == c );
        }

        [Test]
        public void ExceptNot () {
            a = BitSetArray.From ( 1, 2, 3 );
            b = BitSetArray.From ( 3, 4, 5 );
            c = BitSetArray.From ( new int[] { 1, 2 } );

            Assert.That ( (a - b) == c );
            Assert.That ( (b - a) == BitSetArray.From ( 4, 5 ) );
        }

        [Test]
        public void ComplementNot () {
            a = BitSetArray.From ( 0, 1, 2, 3 );
            b = BitSetArray.From ( 3, 4, 5 );
            c = BitSetArray.From ( new int[] { 0, 1, 2 } );

            Assert.That ( (~a) == BitSetArray.Empty () );
            Assert.That ( (~a) == BitSetArray.Size ( 10 ) );
            Assert.That ( (~b) == c );
        }

        [TestFixtureSetUp]
        public void Init () {
            GC.Collect ();
        }

        [TestFixtureTearDown]
        public void Dispose () {
            GC.Collect ();
        }
    }
}