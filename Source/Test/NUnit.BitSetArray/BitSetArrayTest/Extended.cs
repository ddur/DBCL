// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.BitSetArrayTest.Extended {

    [TestFixture]
    public class Span_This_BitSetArray {

        [Test]
        public void IsTrue () {
            Assert.True (((BitSetArray)null).Span () == 0);
            Assert.True (BitSetArray.Empty ().Span () == 0);
            Assert.True (BitSetArray.Size (100).Span () == 0);
            Assert.True (BitSetArray.Size (100, true).Span () == 100);
            Assert.True (BitSetArray.From (100, 200, 300).Span () == 201);
        }

        [Test]
        public void BitSetArray_Null () {
            BitSetArray isNull = null;
            Assert.AreEqual (0, isNull.Span ());
        }

        [Test]
        public void BitSetArray_Empty () {
            Assert.AreEqual (0, BitSetArray.Empty ().Span ());
            Assert.AreEqual (0, BitSetArray.Size (1).Span ());
            Assert.AreEqual (0, BitSetArray.Size (1000).Span ());
        }

        [Test]
        public void BitSetArray_NotEmpty () {
            Assert.AreEqual (1, BitSetArray.From (1).Span ());
            Assert.AreEqual (10, BitSetArray.From (1, 10).Span ());
            Assert.AreEqual (101, BitSetArray.From (900, 1000).Span ());
        }
    }

    [TestFixture]
    public class IsEmpty_This_BitSetArray {

        [Test]
        public void BitSetArray_Null_Throws () {
            Assert.Throws<ArgumentNullException> (
                delegate {
                    ((BitSetArray)null).IsEmpty ();
                }
            );
        }

        [Test]
        public void IsTrue () {
            Assert.True ((BitSetArray.Empty ()).IsEmpty ());
            Assert.True ((BitSetArray.Size (100)).IsEmpty ());
        }

        [Test]
        public void IsFalse () {
            Assert.False ((BitSetArray.From (0)).IsEmpty ());
            Assert.False ((BitSetArray.From (1, 2, 3)).IsEmpty ());
            Assert.False ((BitSetArray.Size (100, true)).IsEmpty ());
        }
    }

    [TestFixture]
    public class IsNullOrEmpty_This_BitSetArray {

        [Test]
        public void IsTrue () {
            Assert.True (((BitSetArray)null).IsNullOrEmpty ());
            Assert.True ((BitSetArray.Empty ()).IsNullOrEmpty ());
            Assert.True ((BitSetArray.Size (100)).IsNullOrEmpty ());
        }

        [Test]
        public void IsFalse () {
            Assert.False ((BitSetArray.From (0)).IsNullOrEmpty ());
            Assert.False ((BitSetArray.From (1, 2, 3)).IsNullOrEmpty ());
            Assert.False ((BitSetArray.Size (100, true)).IsNullOrEmpty ());
        }
    }

    [TestFixture]
    public class IsFull_This_BitSetArray {

        [Test]
        public void Null_IsFalse () {
            BitSetArray isNull = null;
            Assert.False (isNull.IsFull ());
        }

        [Test]
        public void Empty_IsFalse () {
            var isEmpty = BitSetArray.Empty ();
            Assert.False (isEmpty.IsFull ());
        }

        [Test]
        public void NotFull_IsFalse () {
            var isNotFull = BitSetArray.From (1, 3);
            Assert.False (isNotFull.IsFull ());

            isNotFull = BitSetArray.From (0, 7);
            Assert.False (isNotFull.IsFull ());
        }

        [Test]
        public void Full_IsTrue () {
            var isFull = BitSetArray.From (0);
            Assert.True (isFull.IsFull ());

            isFull = BitSetArray.From (0, 1);
            Assert.True (isFull.IsFull ());

            isFull = BitSetArray.From (1, 2);
            Assert.True (isFull.IsFull ());

            isFull = BitSetArray.From (1, 2, 3);
            Assert.True (isFull.IsFull ());

            isFull = BitSetArray.Size (10, true);
            Assert.True (isFull.IsFull ());
        }
    }
}
