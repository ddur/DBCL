// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;

using NUnit.Framework;

namespace DD.Collections.BitSetArrayTest {

    [TestFixture]
    public class Properties {
        [Test]
        public void LengthGet () {
            BitSetArray bs;
            bs = new BitSetArray (10);
            Assert.That (bs.Length == 10);
            bs = BitSetArray.Size (200, true);
            Assert.That (bs.Length == 200);
            bs.Length = 500;
            Assert.That (bs.Length == 500);
        }

        [Test]
        public void LengthSet () {
            BitSetArray bs;
            bs = new BitSetArray ();
            Assert.That (delegate {
                bs.Length = -1;
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                bs.Length = int.MinValue;
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            bs.Length = 0;
#if MAXCOVERAGE
			bs.Length = int.MaxValue;
#endif
            bs = new BitSetArray ();
            Assert.That (bs.Count == 0);
            Assert.That (bs.Length == 0);
            Assert.That (bs.Capacity == 0);
            bs.Length = 100;
            Assert.That (bs.Count == 0);
            Assert.That (bs.Length == 100);
            Assert.That (bs.Capacity == 128);
            bs.Length = 0;
            Assert.That (bs.Count == 0);
            Assert.That (bs.Length == 0);
            Assert.That (bs.Capacity == 128);
            bs.Length = 100;
            bs.SetAll (true);
            Assert.That (bs.Count == 100);
            Assert.That (bs.Length == 100);
            Assert.That (bs.Capacity == 128);
            bs.Length = 200;
            Assert.That (bs.Count == 100);
            Assert.That (bs.Length == 200);
            Assert.That (bs.Capacity == 256);
            bs.Length = 250;
            Assert.That (bs.Count == 100);
            Assert.That (bs.Length == 250);
            Assert.That (bs.Capacity == 256);
            bs.Length = 50;
            Assert.That (bs.Count == 50);
            Assert.That (bs.Length == 50);
            Assert.That (bs.Capacity == 256);
            bs.Length = 45;
            Assert.That (bs.Count == 45);
            Assert.That (bs.Length == 45);
            Assert.That (bs.Capacity == 256);
            bs.Length = 0;
            Assert.That (bs.Count == 0);
            Assert.That (bs.Length == 0);
            Assert.That (bs.Capacity == 256);
            bs.Length = 300;
            Assert.That (bs.Count == 0);
            Assert.That (bs.Length == 300);
            Assert.That (bs.Capacity == 512);

#if MAXCOVERAGE
			bs.Length = (int.MaxValue/4)*3;
			Assert.That ( bs.Count == 0 );
			Assert.That ( bs.Length == (int.MaxValue/4)*3 );
			Assert.That ( bs.Capacity == BitSetArray.GetLongArrayLength(bs.Length)*64 );
			bs.Length += 100;
			Assert.That ( bs.Count == 0 );
			Assert.That ( bs.Capacity == int.MaxValue );
#endif

        }

        [Test]
        public void CapacityGet () {
            BitSetArray bs = new BitSetArray (100, true);
            Assert.That (bs.Capacity == 128);
            bs.Add (500);
            Assert.That (bs.Capacity == 512);
            bs.Add (600);
            Assert.That (bs.Capacity == 1024);
        }

        [Test]
        public void CountGet () {
            BitSetArray bs = new BitSetArray (200, true);
            Assert.That (bs.Count == 200);
            bs.Add (250);
            Assert.That (bs.Count == 201);
        }

        [Test]
        public void FirstGet () {
            BitSetArray bs = new BitSetArray ();
            Assert.True (bs.First == null);
            Assert.True (bs.Last == null);
            bs.Add (12);
            Assert.True (bs.First == 12);
            Assert.True (bs.Last == 12);
            bs.Add (22);
            Assert.True (bs.First == 12);
            bs.Add (10);
            Assert.True (bs.First == 10);
            bs.Add (24);
            Assert.True (bs.First == 10);
        }

        [Test]
        public void LastGet () {
            BitSetArray bs = new BitSetArray ();
            Assert.True (bs.First == null);
            Assert.True (bs.Last == null);
            bs.Add (12);
            Assert.True (bs.First == 12);
            Assert.True (bs.Last == 12);
            bs.Add (22);
            Assert.True (bs.Last == 22);
            bs.Add (10);
            Assert.True (bs.Last == 22);
            bs.Add (44);
            Assert.True (bs.Last == 44);
        }

        [Test]
        public void IsReadonly () {
            BitSetArray bs = new BitSetArray ();
            Assert.That (!bs.IsReadOnly);
        }

        [Test]
        public void IsSynchronized () {
            BitSetArray bs = new BitSetArray ();
            Assert.That (!bs.IsSynchronized);
        }

        [Test]
        public void SyncRoot () {
            BitSetArray bs = new BitSetArray ();
            Assert.That (!bs.SyncRoot.IsNull ());
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
