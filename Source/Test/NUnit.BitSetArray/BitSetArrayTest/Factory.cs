// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using NUnit.Framework;

namespace DD.Collections.BitSetArrayTest {

    [TestFixture]
    public class Factory {

        [Test]
        public void From () {
            Assert.That (delegate {
                BitSetArray.From ((IEnumerable<int>)null);
            }, Throws.TypeOf<ArgumentNullException> ());

//            Assert.That (delegate {
//               BitSetArray.From (new int[0]);
//            }, Throws.TypeOf<ArgumentEmptyException> ());

            Assert.That (delegate {
                BitSetArray.From ((IEnumerable<int>)new int[] { 0, -1 });
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            Assert.That (delegate {
                BitSetArray.From (-1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                BitSetArray.From (-1, 0, 1, 2);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                BitSetArray.From (0, -1, -2, -3);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            BitSetArray test;
            List<int> list;

            list = new List<int> { 1, 2, 3, 4, 200, 300, 2000 };
            test = BitSetArray.From (list);
            Assert.That (test.SetEquals (BitSetArray.From (1, 2, 3, 4, 200, 300, 2000)));
            Assert.That (test.Count == list.Count);
            Assert.That (test.Length == 2001);
            Assert.That (test.First == 1);
            Assert.That (test.Last == 2000);

            test = BitSetArray.From (BitSetArray.From (list));
            Assert.That (test.Count == list.Count);
            Assert.That (test.Count == 7);
            Assert.That (test.Length == 2001);
            Assert.That (test.First == 1);
            Assert.That (test.Last == 2000);

            test = BitSetArray.From (BitSetArray.From (list), 3000);
            Assert.That (test.Count == list.Count);
            Assert.That (test.Count == 7);
            Assert.That (test.Length == 3000);
            Assert.That (test.First == 1);
            Assert.That (test.Last == 2000);

            test = BitSetArray.From (test, 1000);
            Assert.That (test.Count != list.Count);
            Assert.That (test.Count == 6);
            Assert.That (test.Length == 1000);
            Assert.That (test.First == 1);
            Assert.That (test.Last == 300);

            list = new List<int> { 1, 2, 3, 4, 1, 2, 3, 4 };
            test = BitSetArray.From (list);
            Assert.That (test.Count != list.Count);
            Assert.That (test.Count == 4);
            Assert.That (test.Length == 5);
            Assert.That (test.First == 1);
            Assert.That (test.Last == 4);

            test = BitSetArray.From (test, 5);
            Assert.That (test.Count == 4);
            Assert.That (test.Length == 5);
            Assert.That (test.First == 1);
            Assert.That (test.Last == 4);

            test = BitSetArray.From (test, 3);
            Assert.That (test.Count == 2);
            Assert.That (test.Length == 3);
            Assert.That (test.First == 1);
            Assert.That (test.Last == 2);
        }

        [Test]
        public void Copy () {
            Assert.That (delegate {
                BitSetArray.Copy ((BitSetArray)null);
            }, Throws.TypeOf<ArgumentNullException> ());

            BitSetArray test = BitSetArray.From (new List<int> { 1, 2, 3 });
            BitSetArray copy;

            // Here will do only code coverage as .Copy redirects to tested constructor
            copy = BitSetArray.Copy (test);
        }

        [Test]
        public void Size () {

            #region Size ( int length )

            // Here will do only code coverage as this .Mask redirects to tested constructor
            Assert.That (delegate {
                BitSetArray.Empty ();
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.Size (5);
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.Size (-1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            #endregion

            #region Size ( int length, bool value )

            // Here will do only code coverage as this .Mask redirects to tested constructor
            Assert.That (delegate {
                BitSetArray.Size (0, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.Size (0, true);
            }, Throws.TypeOf<ArgumentException> ());
            Assert.That (delegate {
                BitSetArray.Size (5, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.Size (0, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.Size (-1, true);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            #endregion
        }

        [Test]
        public void Mask () {
            bool[] bools = new bool[] { true, true, true, true, true, false, true, false, true, false };
            BitSetArray masked;

            #region Mask ( System.BitArray mask, int length )

            Assert.That (delegate {
                BitSetArray.Mask ((BitArray)null, -1);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (new BitArray (bools), -1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            masked = BitSetArray.Mask (new BitArray (bools), 0);
            Assert.That (masked.Count == 0);
            Assert.That (masked.Length == 0);

            masked = BitSetArray.Mask (new BitArray (0), 8);
            Assert.That (masked.Count == 0);
            Assert.That (masked.Length == 8);

            masked = BitSetArray.Mask (new BitArray (bools), bools.Length);
            Assert.That (masked.Count == 7);

            masked = BitSetArray.Mask (new BitArray (bools), 2);
            Assert.That (masked.Count == 2);

            masked = BitSetArray.Mask (new BitArray (bools), 7);
            Assert.That (masked.Count == 6);

            masked = BitSetArray.Mask (new BitArray (bools), 128);
            Assert.That (masked.Count == 7);

            #endregion

            #region Mask ( IEnumerable<bool> mask, int range )

            Assert.That (delegate {
                BitSetArray.Mask ((Collection<bool>)null, 1);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask ((IEnumerable<bool>)null, 0);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask ((bool[])null, int.MaxValue);
            }, Throws.TypeOf<ArgumentNullException> ());

            Assert.That (delegate {
                BitSetArray.Mask (new Collection<bool> (), -1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                BitSetArray.Mask (new TestEnumClass<bool> (new bool[0]), -1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                BitSetArray.Mask (new bool[0], int.MinValue);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            masked = BitSetArray.Mask (new bool[0], 8);
            Assert.That (masked.Count == 0);
            Assert.That (masked.Length == 8);

            masked = BitSetArray.Mask (bools, 0);
            Assert.That (masked.Count == 0);

            masked = BitSetArray.Mask (bools, 3);
            Assert.That (masked.Count == 3);

            masked = BitSetArray.Mask (bools, 8);
            Assert.That (masked.Count == 6);

            masked = BitSetArray.Mask (bools, bools.Length);
            Assert.That (masked.Count == 7);

            masked = BitSetArray.Mask (bools, bools.Length * 2);
            Assert.That (masked.Count == 7);

            Assert.That (delegate {
                BitSetArray.Mask ((Collection<bool>)null, 1);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask ((IEnumerable<bool>)null, 2);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask ((bool[])null, 0);
            }, Throws.TypeOf<ArgumentNullException> ());

            masked = BitSetArray.Mask (new Collection<bool> (bools), bools.Length);
            Assert.That (masked.Count == 7);

            masked = BitSetArray.Mask (new TestEnumClass<bool> (bools), bools.Length);
            Assert.That (masked.Count == 7);

            #endregion

            #region Mask ( IEnumerable<byte> mask, int length )

            byte[] bytes = null;

            Assert.That (delegate {
                BitSetArray.Mask (bytes, 0);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (bytes, -1);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (bytes, int.MaxValue);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (bytes, int.MinValue);
            }, Throws.TypeOf<ArgumentNullException> ());

            Assert.That (delegate {
                BitSetArray.Mask (new byte[0], -1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                BitSetArray.Mask (new byte[0], int.MinValue);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            Assert.That (delegate {
                BitSetArray.Mask ((Collection<byte>)null, -1);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (new Collection<byte> (), -1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            Assert.That (delegate {
                BitSetArray.Mask ((TestEnumClass<byte>)null, -1);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (new TestEnumClass<byte> (), -1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            masked = BitSetArray.Mask (new byte[0], 8);
            Assert.That (masked.Count == 0);
            Assert.That (masked.Length == 8);

            bytes = new byte[] { 0x00, 0x50, 0xFF, 0x50, 0xFF, 0x50, 0xFF, 0x50, 0xFF, 0x50, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00,
            					 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            Assert.That (BitSetArray.CountOnBits ((byte)0x00) == 0);
            Assert.That (BitSetArray.CountOnBits ((byte)0x01) == 1);
            Assert.That (BitSetArray.CountOnBits ((byte)0x02) == 1);
            Assert.That (BitSetArray.CountOnBits ((byte)0x04) == 1);
            Assert.That (BitSetArray.CountOnBits ((byte)0x08) == 1);
            Assert.That (BitSetArray.CountOnBits ((byte)0x50) == 2);
            Assert.That (BitSetArray.CountOnBits ((byte)0x0F) == 4);
            Assert.That (BitSetArray.CountOnBits ((byte)0xF0) == 4);
            Assert.That (BitSetArray.CountOnBits ((byte)0xFF) == 8);
            Assert.That (BitSetArray.CountOnBits (bytes) == 50);

            masked = BitSetArray.Mask (bytes, 0);
            Assert.That (masked.Count == 0);
            Assert.That (masked.Length == 0);

            masked = BitSetArray.Mask (bytes, 255);
            Assert.That (masked.Count == 50);
            Assert.That (masked.Length == 255);

            masked = BitSetArray.Mask (bytes, 88);
            Assert.That (masked.Count == 50);
            Assert.That (masked.Length == 88);

            masked = BitSetArray.Mask (bytes, 89);
            Assert.That (masked.Count == 50);
            Assert.That (masked.Length == 89);

            masked = BitSetArray.Mask (bytes, 87);
            Assert.That (masked.Count == 49);
            Assert.That (masked.Length == 87);

            masked = BitSetArray.Mask (bytes, 16);
            Assert.That (masked.Count == 2);

            masked = BitSetArray.Mask (new Collection<byte> (bytes), bytes.Length * 8);
            Assert.That (masked.Count == 50);

            masked = BitSetArray.Mask (new Collection<byte> (bytes), 24);
            Assert.That (masked.Count == 10);

            masked = BitSetArray.Mask (new Collection<byte> (bytes), 87);
            Assert.That (masked.Count == 49);

            masked = BitSetArray.Mask (new Collection<byte> (bytes), 89);
            Assert.That (masked.Count == 50);

            masked = BitSetArray.Mask (new TestEnumClass<byte> (bytes), bytes.Length * 8);
            Assert.That (masked.Count == 50);

            masked = BitSetArray.Mask (new TestEnumClass<byte> (bytes), 32);
            Assert.That (masked.Count == 12);

            masked = BitSetArray.Mask (new TestEnumClass<byte> (bytes), 87);
            Assert.That (masked.Count == 49);

            masked = BitSetArray.Mask (new TestEnumClass<byte> (bytes), 89);
            Assert.That (masked.Count == 50);

            #endregion

            #region Mask ( IEnumerable<short> mask, int length )

            short[] shorts = null;

            Assert.That (delegate {
                BitSetArray.Mask (shorts, 0);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (shorts, -1);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (shorts, int.MaxValue);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (shorts, int.MinValue);
            }, Throws.TypeOf<ArgumentNullException> ());

            Assert.That (delegate {
                BitSetArray.Mask (new short[0], -1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                BitSetArray.Mask (new short[0], int.MinValue);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            Assert.That (delegate {
                BitSetArray.Mask ((Collection<short>)null, -1);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (new Collection<short> (), -1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            Assert.That (delegate {
                BitSetArray.Mask ((TestEnumClass<short>)null, -1);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (new TestEnumClass<short> (), -1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            masked = BitSetArray.Mask (new short[0], 9);
            Assert.That (masked.Count == 0);
            Assert.That (masked.Length == 9);

            shorts = new short[] { 0x5050, -1, 0x5050, -1, 0x5050, -1, 0x5050, -1, 0x5050, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            Assert.That (BitSetArray.CountOnBits (shorts) == 100);
            Assert.That (BitSetArray.CountOnBits ((ushort)0xFFFF) == 16);
            Assert.That (BitSetArray.CountOnBits ((short)-1) == 16);

            masked = BitSetArray.Mask (shorts, 0);
            Assert.That (masked.Count == 0);
            Assert.That (masked.Length == 0);

            masked = BitSetArray.Mask (shorts, (shorts.Length * 16) - 1);
            Assert.That (masked.Count == 100);
            Assert.That (masked.Length == 319);

            masked = BitSetArray.Mask (shorts, shorts.Length * 16);
            Assert.That (masked.Count == 100);
            Assert.That (masked.Length == 320);

            masked = BitSetArray.Mask (shorts, (shorts.Length * 16) + 1);
            Assert.That (masked.Count == 100);
            Assert.That (masked.Length == 321);

            masked = BitSetArray.Mask (shorts, 16);
            Assert.That (masked.Count == 4);

            masked = BitSetArray.Mask (shorts, 159);
            Assert.That (masked.Count == 99);

            masked = BitSetArray.Mask (shorts, 161);
            Assert.That (masked.Count == 100);

            masked = BitSetArray.Mask (new Collection<short> (shorts), shorts.Length * 16);
            Assert.That (masked.Count == 100);

            masked = BitSetArray.Mask (new Collection<short> (shorts), 32);
            Assert.That (masked.Count == 20);

            masked = BitSetArray.Mask (new Collection<short> (shorts), 159);
            Assert.That (masked.Count == 99);

            masked = BitSetArray.Mask (new Collection<short> (shorts), 161);
            Assert.That (masked.Count == 100);

            masked = BitSetArray.Mask (new TestEnumClass<short> (shorts), shorts.Length * 16);
            Assert.That (masked.Count == 100);

            masked = BitSetArray.Mask (new TestEnumClass<short> (shorts), 64);
            Assert.That (masked.Count == 40);

            masked = BitSetArray.Mask (new TestEnumClass<short> (shorts), 159);
            Assert.That (masked.Count == 99);

            masked = BitSetArray.Mask (new TestEnumClass<short> (shorts), 161);
            Assert.That (masked.Count == 100);

            #endregion

            #region Mask ( IEnumerable<int> mask [, int length ] )

            int[] ints = null;

            Assert.That (delegate {
                BitSetArray.Mask (ints, 0);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (ints, -1);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (ints, int.MaxValue);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (ints, int.MinValue);
            }, Throws.TypeOf<ArgumentNullException> ());

            Assert.That (delegate {
                BitSetArray.Mask (new int[0], -1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                BitSetArray.Mask (new int[0], int.MinValue);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            Assert.That (delegate {
                BitSetArray.Mask ((Collection<int>)null, -1);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (new Collection<int> (), -1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            Assert.That (delegate {
                BitSetArray.Mask ((TestEnumClass<int>)null, -1);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (new TestEnumClass<int> (), -1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            masked = BitSetArray.Mask (new int[0], 20);
            Assert.That (masked.Count == 0);
            Assert.That (masked.Length == 20);

            ints = new int[] { 0x50505050, -1, 0x50505050, -1, 0x50505050, -1, 0x50505050, -1, 0x50505050, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            Assert.That (BitSetArray.CountOnBits (ints) == 200);
            Assert.That (BitSetArray.CountOnBits ((uint)0xFFFFFFFF) == 32);
            Assert.That (BitSetArray.CountOnBits ((int)-1) == 32);

            masked = BitSetArray.Mask (ints, 0);
            Assert.That (masked.Count == 0);
            Assert.That (masked.Length == 0);

            masked = BitSetArray.Mask (ints, (ints.Length * 32) - 1);
            Assert.That (masked.Count == 200);
            Assert.That (masked.Length == 639);

            masked = BitSetArray.Mask (ints, ints.Length * 32);
            Assert.That (masked.Count == 200);
            Assert.That (masked.Length == 640);

            masked = BitSetArray.Mask (ints, (ints.Length * 32) + 1);
            Assert.That (masked.Count == 200);
            Assert.That (masked.Length == 641);

            masked = BitSetArray.Mask (ints, 320);
            Assert.That (masked.Count == 200);
            Assert.That (masked.Length == 320);

            masked = BitSetArray.Mask (ints, 64);
            Assert.That (masked.Count == 40);

            masked = BitSetArray.Mask (ints, 319);
            Assert.That (masked.Count == 199);

            masked = BitSetArray.Mask (ints, 321);
            Assert.That (masked.Count == 200);

            masked = BitSetArray.Mask (new Collection<int> (ints), 320);
            Assert.That (masked.Count == 200);

            masked = BitSetArray.Mask (new Collection<int> (ints), 64);
            Assert.That (masked.Count == 40);

            masked = BitSetArray.Mask (new Collection<int> (ints), 319);
            Assert.That (masked.Count == 199);

            masked = BitSetArray.Mask (new Collection<int> (ints), 321);
            Assert.That (masked.Count == 200);

            masked = BitSetArray.Mask (new TestEnumClass<int> (ints), 320);
            Assert.That (masked.Count == 200);

            masked = BitSetArray.Mask (new TestEnumClass<int> (ints), 64);
            Assert.That (masked.Count == 40);

            masked = BitSetArray.Mask (new TestEnumClass<int> (ints), 319);
            Assert.That (masked.Count == 199);

            masked = BitSetArray.Mask (new TestEnumClass<int> (ints), 321);
            Assert.That (masked.Count == 200);

            #endregion

            #region Mask ( IEnumerable<long> mask [, int length ] )

            long[] longs = null;

            Assert.That (delegate {
                BitSetArray.Mask (longs, 0);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (longs, -1);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (longs, int.MaxValue);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (longs, int.MinValue);
            }, Throws.TypeOf<ArgumentNullException> ());

            Assert.That (delegate {
                BitSetArray.Mask (new long[0], -1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                BitSetArray.Mask (new long[0], int.MinValue);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            Assert.That (delegate {
                BitSetArray.Mask ((Collection<long>)null, -1);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (new Collection<long> (), -1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            Assert.That (delegate {
                BitSetArray.Mask ((TestEnumClass<long>)null, -1);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                BitSetArray.Mask (new TestEnumClass<long> (), -1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            masked = BitSetArray.Mask (new long[0], 20);
            Assert.That (masked.Count == 0);
            Assert.That (masked.Length == 20);

            longs = new long[] { 0x5050505050505050, -1, 0x5050505050505050, -1, 0x5050505050505050, -1, 0x5050505050505050, -1, 0x5050505050505050, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            Assert.That (BitSetArray.CountOnBits (longs) == 400);
            Assert.That (BitSetArray.CountOnBits ((ulong)0xFFFFFFFFFFFFFFFF) == 64);
            Assert.That (BitSetArray.CountOnBits ((long)-1) == 64);

            masked = BitSetArray.Mask (longs, 0);
            Assert.That (masked.Count == 0);
            Assert.That (masked.Length == 0);

            masked = BitSetArray.Mask (longs, (longs.Length * 64) - 1);
            Assert.That (masked.Count == 400);
            Assert.That (masked.Length == 1279);

            masked = BitSetArray.Mask (longs, longs.Length * 64);
            Assert.That (masked.Count == 400);
            Assert.That (masked.Length == 1280);

            masked = BitSetArray.Mask (longs, (longs.Length * 64) + 1);
            Assert.That (masked.Count == 400);
            Assert.That (masked.Length == 1281);

            masked = BitSetArray.Mask (longs, 640);
            Assert.That (masked.Count == 400);
            Assert.That (masked.Length == 640);

            masked = BitSetArray.Mask (longs, 128);
            Assert.That (masked.Count == 80);

            masked = BitSetArray.Mask (longs, 639);
            Assert.That (masked.Count == 399);

            masked = BitSetArray.Mask (longs, 641);
            Assert.That (masked.Count == 400);

            masked = BitSetArray.Mask (new Collection<long> (longs), 640);
            Assert.That (masked.Count == 400);

            masked = BitSetArray.Mask (new Collection<long> (longs), 128);
            Assert.That (masked.Count == 80);

            masked = BitSetArray.Mask (new Collection<long> (longs), 639);
            Assert.That (masked.Count == 399);

            masked = BitSetArray.Mask (new Collection<long> (longs), 641);
            Assert.That (masked.Count == 400);

            masked = BitSetArray.Mask (new TestEnumClass<long> (longs), 640);
            Assert.That (masked.Count == 400);

            masked = BitSetArray.Mask (new TestEnumClass<long> (longs), 128);
            Assert.That (masked.Count == 80);

            masked = BitSetArray.Mask (new TestEnumClass<long> (longs), 639);
            Assert.That (masked.Count == 399);

            masked = BitSetArray.Mask (new TestEnumClass<long> (longs), 641);
            Assert.That (masked.Count == 400);

            #endregion

            // Just to cover that last TestClass line :)
            TestEnumClass<bool> testClass = new TestEnumClass<bool> ();
            IEnumerator ie = ((IEnumerable)testClass).GetEnumerator ();
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

    // IEnumerable<T> class that is not Collection<T> nor T[]
    public class TestEnumClass<T> : IEnumerable<T> {
        private IEnumerable<T> items = new T[0];

        public TestEnumClass () {
        }

        public TestEnumClass (IEnumerable<T> items) {
            this.items = items;
        }

        IEnumerator IEnumerable.GetEnumerator () {
            return items.GetEnumerator ();
        }

        public IEnumerator<T> GetEnumerator () {
            return items.GetEnumerator ();
        }
    }
}
