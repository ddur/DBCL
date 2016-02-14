// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace DD.Collections.BitSetArrayTest {

    [TestFixture]
    public class GetSet {

        [Test]
        // index get is less restrictive than Get - no IndexOutOfRangeException thrown
        public void IndexGet () {
            BitSetArray test = BitSetArray.From (0, 5, 10);
            Assert.That (test[int.MinValue] == false);
            Assert.That (test[-1] == false);
            Assert.That (test[0] == true);
            Assert.That (test[1] == false);
            Assert.That (test[2] == false);
            Assert.That (test[3] == false);
            Assert.That (test[4] == false);
            Assert.That (test[5] == true);
            Assert.That (test[6] == false);
            Assert.That (test[7] == false);
            Assert.That (test[8] == false);
            Assert.That (test[9] == false);
            Assert.That (test[10] == true);
            Assert.That (test[11] == false);
            Assert.That (test[int.MaxValue] == false);

            test = BitSetArray.Empty ();
            Assert.That (test[int.MinValue] == false);
            Assert.That (test[-1] == false);
            Assert.That (test[0] == false);
            Assert.That (test[1] == false);
            Assert.That (test[int.MaxValue] == false);
        }

        [Test]
        // index set is less restrictive than Set - no IndexOutOfRangeException thrown if value is false
        public void IndexSet () {
            BitSetArray test = BitSetArray.From (0, 5, 10);
            Assert.That (delegate {
                test[int.MinValue] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[int.MinValue] = true;
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            Assert.That (delegate {
                test[-1] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[-1] = true;
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            Assert.That (delegate {
                test[0] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[1] = true;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[2] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[3] = true;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[4] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[5] = true;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[6] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[7] = true;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[8] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[9] = true;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[10] = false;
            }, Throws.Nothing);

            Assert.That (delegate {
                test[11] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[11] = true;
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test[int.MaxValue] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[int.MaxValue] = true;
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            test = BitSetArray.Empty ();
            Assert.That (delegate {
                test[int.MinValue] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[int.MinValue] = true;
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            Assert.That (delegate {
                test[-1] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[-1] = true;
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            Assert.That (delegate {
                test[0] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[0] = true;
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            Assert.That (delegate {
                test[1] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[1] = true;
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            Assert.That (delegate {
                test[int.MaxValue] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[int.MaxValue] = true;
            }, Throws.TypeOf<IndexOutOfRangeException> ());
        }

        [Test]
        public void BitGet () {
            BitSetArray test = BitSetArray.Mask (new int[] { 1057 }, 11);
            Assert.That (delegate {
                test.Get (int.MinValue);
            }, Throws.Nothing);
            Assert.That (test.Get (int.MinValue) == false);
            Assert.That (delegate {
                test.Get (-1);
            }, Throws.Nothing);
            Assert.That (test.Get (-1) == false);

            Assert.That (test.Get (0) == true);
            Assert.That (test.Get (1) == false);
            Assert.That (test.Get (2) == false);
            Assert.That (test.Get (3) == false);
            Assert.That (test.Get (4) == false);
            Assert.That (test.Get (5) == true);
            Assert.That (test.Get (6) == false);
            Assert.That (test.Get (7) == false);
            Assert.That (test.Get (8) == false);
            Assert.That (test.Get (9) == false);
            Assert.That (test.Get (10) == true);
            Assert.That (delegate {
                test.Get (11);
            }, Throws.Nothing);
            Assert.That (test.Get (11) == false);
            Assert.That (delegate {
                test.Get (int.MaxValue);
            }, Throws.Nothing);
            Assert.That (test.Get (int.MaxValue) == false);

            test = BitSetArray.Empty ();
            Assert.That (delegate {
                test.Get (int.MinValue);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Get (-1);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Get (0);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Get (1);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Get (int.MaxValue);
            }, Throws.Nothing);
            Assert.That (test.Get (int.MinValue) == false);
            Assert.That (test.Get (-1) == false);
            Assert.That (test.Get (0) == false);
            Assert.That (test.Get (1) == false);
            Assert.That (test.Get (int.MaxValue) == false);
        }

        [Test]
        public void BitSet_Items_Null () {

            // arrange
            var test = BitSetArray.Size (10);
            int[] items = null;

            // act
            test.Set (items);

            // assert
            Assert.That (test.Count == 0);
        }


        [Test]
        public void BitSet_Items_Empty () {

            // arrange
            var test = BitSetArray.Size (10);
            var items = new int[0];;

            // act
            test.Set (items);

            // assert
            Assert.That (test.Count == 0);
        }

        [Test]
        public void BitSet_Items_Valid_FalseTrue () {

            // arrange
            var test = BitSetArray.Size (10);

            // assert preconditions
            Assert.That (test.Version == int.MaxValue);
            Assert.That (test.CachedFirstItem_Version == null);
            Assert.That (test.CachedLastItem_Version == null);
            Assert.That (test.CachedFirstItem_Value == null);
            Assert.That (test.CachedLastItem_Value == null);

            // act
            var items = new int [] {int.MinValue, -1, 2, 7, 2, 7, int.MaxValue};
            test.Set (items);

            // assert postconditions
            Assert.That (test.CachedFirstItem_Version == test.Version);
            Assert.That (test.CachedLastItem_Version == test.Version);
            Assert.That (test.CachedFirstItem_Value == 2);
            Assert.That (test.CachedLastItem_Value == 7);

            Assert.That (test.Count == 2);
            Assert.False (test[0]);
            Assert.False (test[1]);
            Assert.True (test[2]);
            Assert.False (test[3]);
            Assert.False (test[4]);
            Assert.False (test[5]);
            Assert.False (test[6]);
            Assert.True (test[7]);
            Assert.False (test[8]);
            Assert.False (test[9]);

            // act
            test.Set (new int[] {4, 6}); // cache valid - no need to update value, update version

            // assert postconditions
            Assert.That (test.CachedFirstItem_Version == test.Version);
            Assert.That (test.CachedLastItem_Version == test.Version);
            Assert.That (test.CachedFirstItem_Value == 2);
            Assert.That (test.CachedLastItem_Value == 7);

            Assert.That (test.Count == 4);
            Assert.False (test[0]);
            Assert.False (test[1]);
            Assert.True (test[2]);
            Assert.False (test[3]);
            Assert.True (test[4]);
            Assert.False (test[5]);
            Assert.True (test[6]);
            Assert.True (test[7]);
            Assert.False (test[8]);
            Assert.False (test[9]);

            // act
            test.Set (new int[] {0, 9}); // cache valid - update cache value and version

            // assert postconditions
            Assert.That (test.CachedFirstItem_Version == test.Version);
            Assert.That (test.CachedLastItem_Version == test.Version);
            Assert.That (test.CachedFirstItem_Value == 0);
            Assert.That (test.CachedLastItem_Value == 9);

            Assert.That (test.Count == 6);
            Assert.True (test[0]);
            Assert.False (test[1]);
            Assert.True (test[2]);
            Assert.False (test[3]);
            Assert.True (test[4]);
            Assert.False (test[5]);
            Assert.True (test[6]);
            Assert.True (test[7]);
            Assert.False (test[8]);
            Assert.True (test[9]);

            // act
            test.Set (new int[] {0, 9}, false); // invalidates cache

            // assert postconditions
            Assert.That (test.CachedFirstItem_Version != test.Version);
            Assert.That (test.CachedLastItem_Version != test.Version);
            Assert.That (test.CachedFirstItem_Value == 0);
            Assert.That (test.CachedLastItem_Value == 9);

            Assert.That (test.Count == 4);
            Assert.False (test[0]);
            Assert.False (test[1]);
            Assert.True (test[2]);
            Assert.False (test[3]);
            Assert.True (test[4]);
            Assert.False (test[5]);
            Assert.True (test[6]);
            Assert.True (test[7]);
            Assert.False (test[8]);
            Assert.False (test[9]);

            // act
            test.Set (new int[] {0, 9}); // cache invalidated - does not update cache

            // assert postconditions
            Assert.That (test.CachedFirstItem_Version != test.Version);
            Assert.That (test.CachedLastItem_Version != test.Version);
            Assert.That (test.CachedFirstItem_Value == 0);
            Assert.That (test.CachedLastItem_Value == 9);

            Assert.That (test.Count == 6);
            Assert.True (test[0]);
            Assert.False (test[1]);
            Assert.True (test[2]);
            Assert.False (test[3]);
            Assert.True (test[4]);
            Assert.False (test[5]);
            Assert.True (test[6]);
            Assert.True (test[7]);
            Assert.False (test[8]);
            Assert.True (test[9]);

        }


        [Test]
        public void BitSet_Items_Valid_FalseFalse () {

            // arrange
            var test = BitSetArray.Size (10);
            var items = new int [] {int.MinValue, -1, 0, 8, 0, 8, int.MaxValue};

            // act
            test.Set (items, false);

            // assert
            Assert.That (test.Count == 0);
            Assert.False (test[0]);
            Assert.False (test[1]);
            Assert.False (test[7]);
            Assert.False (test[8]);
            Assert.False (test[9]);

        }

        [Test]
        public void BitSet_Items_Valid_TrueFalse () {

            // arrange
            var test = BitSetArray.Size (10, true);

            // assert preconditions
            Assert.That (test.Version == int.MaxValue);
            Assert.That (test.CachedFirstItem_Version == int.MaxValue);
            Assert.That (test.CachedLastItem_Version == int.MaxValue);
            Assert.That (test.CachedFirstItem_Value == 0);
            Assert.That (test.CachedLastItem_Value == 9);

            // act
            var items = new int [] {int.MinValue, -1, 2, 7, 2, 7, int.MaxValue};
            test.Set (items, false);

            // assert postconditions
            Assert.That (test.CachedFirstItem_Version == test.Version);
            Assert.That (test.CachedLastItem_Version == test.Version);
            Assert.That (test.CachedFirstItem_Value == 0);
            Assert.That (test.CachedLastItem_Value == 9);

            Assert.That (test.Count == 8);
            Assert.That (test[0]);
            Assert.That (test[1]);
            Assert.False (test[2]);
            Assert.That (test[3]);
            Assert.That (test[4]);
            Assert.That (test[5]);
            Assert.That (test[6]);
            Assert.False (test[7]);
            Assert.That (test[8]);
            Assert.That (test[9]);

            // act
            test.Set (new int[] {4, 6}, false); // cache valid - no need to update value, update version

            // assert postconditions
            Assert.That (test.CachedFirstItem_Version == test.Version);
            Assert.That (test.CachedLastItem_Version == test.Version);
            Assert.That (test.CachedFirstItem_Value == 0);
            Assert.That (test.CachedLastItem_Value == 9);

            Assert.That (test.Count == 6);
            Assert.That (test[0]);
            Assert.That (test[1]);
            Assert.False (test[2]);
            Assert.That (test[3]);
            Assert.False (test[4]);
            Assert.That (test[5]);
            Assert.False (test[6]);
            Assert.False (test[7]);
            Assert.That (test[8]);
            Assert.That (test[9]);

            // act
            test.Set (new int[] {0, 9}, false); // invalidates cache

            // assert postconditions
            Assert.That (test.CachedFirstItem_Version != test.Version);
            Assert.That (test.CachedLastItem_Version != test.Version);
            Assert.That (test.CachedFirstItem_Value == 0);
            Assert.That (test.CachedLastItem_Value == 9);

            Assert.That (test.Count == 4);
            Assert.False (test[0]);
            Assert.That (test[1]);
            Assert.False (test[2]);
            Assert.That (test[3]);
            Assert.False (test[4]);
            Assert.That (test[5]);
            Assert.False (test[6]);
            Assert.False (test[7]);
            Assert.That (test[8]);
            Assert.False (test[9]);

            test.Set (new int[] {3, 5}, false); // cache invalidated - does not update cache

            // assert postconditions
            Assert.That (test.CachedFirstItem_Version != test.Version);
            Assert.That (test.CachedLastItem_Version != test.Version);
            Assert.That (test.CachedFirstItem_Value == 0);
            Assert.That (test.CachedLastItem_Value == 9);

            Assert.That (test.Count == 2);
            Assert.False (test[0]);
            Assert.That (test[1]);
            Assert.False (test[2]);
            Assert.False (test[3]);
            Assert.False (test[4]);
            Assert.False (test[5]);
            Assert.False (test[6]);
            Assert.False (test[7]);
            Assert.That (test[8]);
            Assert.False (test[9]);

            // set/update cache
            Assert.That (test.First == 1);
            Assert.That (test.Last == 8);

            // act
            test.Set (new int[] {0, 9}); // cache valid - update cache value and version

            // assert postconditions
            Assert.That (test.CachedFirstItem_Version == test.Version);
            Assert.That (test.CachedLastItem_Version == test.Version);
            Assert.That (test.CachedFirstItem_Value == 0);
            Assert.That (test.CachedLastItem_Value == 9);

            Assert.That (test.Count == 4);
            Assert.That (test[0]);
            Assert.That (test[1]);
            Assert.False (test[2]);
            Assert.False (test[3]);
            Assert.False (test[4]);
            Assert.False (test[5]);
            Assert.False (test[6]);
            Assert.False (test[7]);
            Assert.That (test[8]);
            Assert.That (test[9]);
        }

        [Test]
        public void BitSet_Items_Valid_TrueTrue () {

            // arrange
            var test = BitSetArray.Size (10, true);
            var items = new int [] {int.MinValue, -1, 0, 8, 0, 8, int.MaxValue};

            // act
            test.Set (items);

            // assert
            Assert.That (test.Count == 10);
            Assert.That (test[0]);
            Assert.That (test[1]);
            Assert.That (test[7]);
            Assert.That (test[8]);
            Assert.That (test[9]);
        }

        [Test]
        public void BitSet_Item () {

            var test = BitSetArray.Size (10);

            test.Set (5); // first/last cached
            int expected_version = int.MinValue;
            Assert.AreEqual (expected_version, test.Version);

            Assert.AreEqual (test.Version, test.CachedFirstItem_Version);
            Assert.AreEqual (test.Version, test.CachedLastItem_Version);

            Assert.AreEqual (5, test.CachedFirstItem_Value);
            Assert.AreEqual (5, test.CachedLastItem_Value);

            // update cache.First
            test.Set (3);
            ++expected_version;

            Assert.AreEqual (expected_version, test.Version);

            Assert.AreEqual (test.Version, test.CachedFirstItem_Version);
            Assert.AreEqual (test.Version, test.CachedLastItem_Version);

            Assert.AreEqual (3, test.CachedFirstItem_Value);
            Assert.AreEqual (5, test.CachedLastItem_Value);

            // update cache.Last
            test.Set (7);
            ++expected_version;

            Assert.AreEqual (expected_version, test.Version);

            Assert.AreEqual (test.Version, test.CachedFirstItem_Version);
            Assert.AreEqual (test.Version, test.CachedLastItem_Version);

            Assert.AreEqual (3, test.CachedFirstItem_Value);
            Assert.AreEqual (7, test.CachedLastItem_Value);

            // cache is alive (version updated)
            test.Set (4);
            ++expected_version;

            Assert.AreEqual (expected_version, test.Version);

            Assert.AreEqual (test.Version, test.CachedFirstItem_Version);
            Assert.AreEqual (test.Version, test.CachedLastItem_Version);

            Assert.AreEqual (3, test.CachedFirstItem_Value);
            Assert.AreEqual (7, test.CachedLastItem_Value);

            // invalidate cache.First
            test.Set (3, false);
            ++expected_version;

            Assert.AreEqual (expected_version, test.Version);

            Assert.AreNotEqual (test.Version, test.CachedFirstItem_Version);
            Assert.AreEqual (test.Version, test.CachedLastItem_Version);

            Assert.AreEqual (3, test.CachedFirstItem_Value);
            Assert.AreEqual (7, test.CachedLastItem_Value);

            // cache.First is invalidated (dead)
            test.Set (3);
            ++expected_version;

            Assert.AreEqual (expected_version, test.Version);

            Assert.AreNotEqual (test.Version, test.CachedFirstItem_Version);
            Assert.AreEqual (test.Version, test.CachedLastItem_Version);

            Assert.AreEqual (3, test.CachedFirstItem_Value);
            Assert.AreEqual (7, test.CachedLastItem_Value);

            // cache.First is invalidated (dead)
            test.Set (3, false);
            ++expected_version;

            Assert.AreEqual (expected_version, test.Version);

            Assert.AreNotEqual (test.Version, test.CachedFirstItem_Version);
            Assert.AreEqual (test.Version, test.CachedLastItem_Version);

            Assert.AreEqual (3, test.CachedFirstItem_Value);
            Assert.AreEqual (7, test.CachedLastItem_Value);

            // invalidate cache.Last
            test.Set (7, false);
            ++expected_version;

            // cache.Last is invalidated (dead)
            Assert.AreEqual (expected_version, test.Version);

            Assert.AreNotEqual (test.Version, test.CachedFirstItem_Version);
            Assert.AreNotEqual (test.Version, test.CachedLastItem_Version);

            Assert.AreEqual (3, test.CachedFirstItem_Value);
            Assert.AreEqual (7, test.CachedLastItem_Value);

            // reanimate cache.First
            var ignore = test.First;
            Assert.AreEqual (expected_version, test.Version);

            Assert.AreEqual (test.Version, test.CachedFirstItem_Version);
            Assert.AreNotEqual (test.Version, test.CachedLastItem_Version);

            Assert.AreEqual (4, test.CachedFirstItem_Value);
            Assert.AreEqual (7, test.CachedLastItem_Value);

            // reanimate cache.Last
            var myLast = test.Last;
            Assert.AreEqual (expected_version, test.Version);

            Assert.AreEqual (test.Version, test.CachedFirstItem_Version);
            Assert.AreEqual (test.Version, test.CachedLastItem_Version);

            Assert.AreEqual (4, test.CachedFirstItem_Value);
            Assert.AreEqual (5, test.CachedLastItem_Value);

            var reset = new List<int> (test);
            foreach (var item in reset) {
                test.Set (item, false);
            }
            Assert.AreEqual (0, test.Count);

        }

        [Test]
        public void BitSet_Random () {
            var r = new Random ();
            int random_item = 0;
            var test = BitSetArray.Size (1000);
            for (int i = 0; i < 100; i++) {
                test.Set (r.Next (int.MinValue, int.MaxValue));
                test.Set (r.Next (int.MinValue, int.MaxValue), false);
            }
            for (int i = 0; i < 100; i++) {
                random_item = r.Next (int.MinValue, int.MaxValue);
                test.Set (random_item);
                test.Set (random_item, false);
                test.Set (random_item);
            }
        }

        [Test]
        public void BitSet_Throws () {
            BitSetArray test = BitSetArray.From (5, 0, 10);
            Assert.That (delegate {
                test.Set (int.MinValue, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (int.MinValue, true);
            }, Throws.Nothing);
            Assert.False (test.Set (int.MinValue, false));
            Assert.False (test.Set (int.MinValue, true));

            Assert.That (delegate {
                test.Set (-1, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (-1, true);
            }, Throws.Nothing);
            Assert.False (test.Set (-1, false));
            Assert.False (test.Set (-1, true));

            Assert.That (delegate {
                test.Set (0, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (1, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (2, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (3, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (4, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (5, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (6, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (7, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (8, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (9, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (10, false);
            }, Throws.Nothing);

            test = BitSetArray.From (5, 0, 10);
            Assert.True (test.Set (0, false));
            Assert.True (test.Set (1, true));
            Assert.False (test.Set (2, false));
            Assert.True (test.Set (3, true));
            Assert.False (test.Set (4, false));
            Assert.False (test.Set (5, true));
            Assert.False (test.Set (6, false));
            Assert.True (test.Set (7, true));
            Assert.False (test.Set (8, false));
            Assert.True (test.Set (9, true));
            Assert.True (test.Set (10, false));

            Assert.That (delegate {
                test.Set (11, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (11, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (int.MaxValue, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (int.MaxValue, true);
            }, Throws.Nothing);

            test = BitSetArray.Empty ();
            Assert.That (delegate {
                test.Set (int.MinValue, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (-1, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (0, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (1, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (int.MaxValue, true);
            }, Throws.Nothing);

            Assert.False (test.Set (int.MinValue, true));
            Assert.False (test.Set (-1, true));
            Assert.False (test.Set (0, true));
            Assert.False (test.Set (1, true));
            Assert.False (test.Set (int.MaxValue, true));

            Assert.That (delegate {
                test.Set (int.MinValue, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (-1, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (0, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (1, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.Set (int.MaxValue, false);
            }, Throws.Nothing);

            Assert.False (test.Set (int.MinValue, false));
            Assert.False (test.Set (-1, false));
            Assert.False (test.Set (0, false));
            Assert.False (test.Set (1, false));
            Assert.False (test.Set (int.MaxValue, false));
        }

        [Test]
        public void Bit_InitItems () {

#if DEBUG // In Release build this internal method throws no contract exceptions
            var test = BitSetArray.Size (2);
            Assert.That (delegate {
                test.initItems (new int[] { 0 }, 0, 0);
            }, Throws.Nothing);

            test = BitSetArray.Size (2);
            Assert.That (delegate {
                test.initItems (new int[] { 0, 1 }, 0, 1);
            }, Throws.Nothing);

            test = BitSetArray.Size (2);
            Assert.That (delegate {
                test.initItems (new int[] { 1 }, 1, 1);
            }, Throws.Nothing);

            test = BitSetArray.Size (2);
            Assert.That (delegate {
                test.initItems (null, 0, 0);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                test.initItems (new int[0], 0, 0);
            }, Throws.TypeOf<ArgumentEmptyException> ());
            Assert.That (delegate {
                test.initItems (new int[] { int.MinValue }, 0, 0);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.initItems (new int[] { -1 }, -1, -1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.initItems (new int[] { 2 }, 2, 2);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.initItems (new int[] { int.MaxValue }, 0, 0);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            Assert.GreaterOrEqual (5, test.Length);
            Assert.That (delegate {
                test.initItems (new int[] { 5 }, 5, 5);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            test.initItems (new int[] { 0 }, 0, 0);
            Assert.AreNotEqual (0, test.Count);
            Assert.That (delegate {
                test.initItems (new int[] { 0 }, 0, 0);
            }, Throws.TypeOf<InvalidOperationException> ()); // Count != 0

#endif
        }

        private IEnumerable<BitSetArray> BitSetAllSource {
            get {
                yield return BitSetArray.Empty ();
                yield return BitSetArray.Size (100, true);
            }
        }

        [Test, TestCaseSource ("BitSetAllSource")]
        public void BitSetAll (BitSetArray test) {
            test.SetAll (true);
            Assert.That (test.Count == test.Length);

            test.Length += 100;
            test.SetAll (true);
            Assert.That (test.Count == test.Length);

            test.SetAll (false);
            Assert.That (test.Count == 0);

            test.SetAll (false);
            Assert.That (test.Count == 0);

            test.Set (0, true);
            test.SetAll (false);
            Assert.That (test.Count == 0);
        }

        [Test]
        public void TrimExcess () {
            BitSetArray test = BitSetArray.From (0, 5, 10, 200);
            Assert.That (test.Length == 201);
            Assert.That (test.Count == 4);

            test[200] = false;
            Assert.That (test.Length == 201);
            Assert.That (test.Count == 3);

            test.TrimExcess ();
            Assert.That (test.Length == 11);
            Assert.That (test.Count == 3);

            test.TrimExcess ();
            Assert.That (test.Length == 11);
            Assert.That (test.Count == 3);

            test.Length = 201;
            Assert.That (test.Length == 201);
            Assert.That (test.Count == 3);

            // .ClearTail codeCoverage
            test.Length = 11;
            Assert.That (test.Length == 11);
            Assert.That (test.Count == 3);
            // .ClearTail codeCoverage
            test.Length = 1;
            Assert.That (test.Length == 1);
            Assert.That (test.Count == 1);
            // .ClearTail codeCoverage
            test.Length = 0;
            Assert.That (test.Length == 0);
            Assert.That (test.Count == 0);

            test.TrimExcess ();
            Assert.That (test.Length == 0);
            Assert.That (test.Count == 0);
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
