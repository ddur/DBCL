// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------


using System;
using NUnit.Framework;

namespace DD.Collections.BitSetArrayTest
{
    [TestFixture]
    public class ItemsAddRemoveSet
    {

        [Test]
        public void AddNullItems()
        {
            // arrange
            var bits = BitSetArray.From (0, 1, 2, 3, 4, 5, 6);

            // assert
            Assert.That (
                delegate {
                    bits.Add ((int[])null);
                }, Throws.TypeOf<ArgumentNullException>()
               );
        }

        [Test]
        public void AddEmptyItems()
        {
            // arrange
            var bits = BitSetArray.From (0, 1, 2, 3, 4, 5, 6);

            // assert
            Assert.That (
                delegate {
                    bits.Add (new int[0]);
                }, Throws.TypeOf<ArgumentEmptyException>()
               );
        }

        [Test]
        public void AddInvalidItems()
        {
            // arrange
            var bits = BitSetArray.From (0, 1, 2, 3, 4, 5, 6);

            // assert
            Assert.That (
                delegate {
                    bits.Add (new int[] {0, int.MinValue});
                }, Throws.TypeOf<IndexOutOfRangeException>()
               );
            Assert.That (
                delegate {
                    bits.Add (new int[] {0, -1});
                }, Throws.TypeOf<IndexOutOfRangeException>()
               );
            Assert.That (
                delegate {
                    bits.Add (new int[] {0, int.MaxValue});
                }, Throws.TypeOf<IndexOutOfRangeException>()
               );
        }

        [Test]
        public void AddValidItems()
        {
            // arrange
            var bits = BitSetArray.From (0, 1, 2, 3, 4, 5, 6);
            var oldCount = bits.Count;

            // act
            bits.Add (new int[] {0, 1, 2, 3, 4, 5, 6});

            // assert
            Assert.That (bits.Count == oldCount);

            // act
            bits.Add (new int[] {10, 11, 12, 13, 14, 15, 16});

            // assert
            Assert.That (bits.Count == oldCount * 2);
        }

        [Test]
        public void RemoveNullItems()
        {
            // arrange
            var bits = BitSetArray.From (0, 1, 2, 3, 4, 5, 6);

            // assert
            Assert.That (
                delegate {
                    bits.Remove ((int[])null);
                }, Throws.TypeOf<ArgumentNullException>()
               );
        }

        [Test]
        public void RemoveEmptyItems()
        {
            // arrange
            var bits = BitSetArray.From (0, 1, 2, 3, 4, 5, 6);

            // assert
            Assert.That (
                delegate {
                    bits.Remove (new int[0]);
                }, Throws.TypeOf<ArgumentEmptyException>()
               );
        }

        [Test]
        public void RemoveInvalidItems()
        {
            // arrange
            var bits = BitSetArray.From (0, 1, 2, 3, 4, 5, 6);

            // assert
            Assert.That (
                delegate {
                    bits.Remove (new int[] {0, int.MinValue});
                }, Throws.TypeOf<IndexOutOfRangeException>()
               );
            Assert.That (
                delegate {
                    bits.Remove (new int[] {0, -1});
                }, Throws.TypeOf<IndexOutOfRangeException>()
               );
            Assert.That (
                delegate {
                    bits.Remove (new int[] {0, int.MaxValue});
                }, Throws.TypeOf<IndexOutOfRangeException>()
               );
        }

        [Test]
        public void RemoveValidItems()
        {
            // arrange
            var bits = BitSetArray.From (0, 1, 2, 3, 4, 5, 6);
            var oldCount = bits.Count;

            // act
            var removed = bits.Remove (new int[] {10, 11, 12, 13, 14, 15, 16});

            // assert
            Assert.That (bits.Count == oldCount);
            Assert.That (removed == 0);

            // act
            removed = bits.Remove (new int[] {0, 1, 2, 3, 4, 5, 6});

            // assert
            Assert.That (bits.Count == 0);
            Assert.That (removed == oldCount);
        }

        [Test]
        public void SetMembers_Null () {

            // arrange
            var test = BitSetArray.Size (10);
            int[] items = null;

            // act & assert
            Assert.That (
                delegate {
                    test.SetMembers (items);
                }, Throws.TypeOf<ArgumentNullException>()
               );
        }


        [Test]
        public void SetMembers_Empty () {

            // arrange
            var test = BitSetArray.Size (10);
            var items = new int[0];;

            // act
            test.SetMembers (items);

            // assert
            Assert.That (test.Count == 0);
        }

        [Test]
        public void SetMembers_FromFalseToTrue_Version () {

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
            test.SetMembers (items);

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
            test.SetMembers (new int[] {4, 6}); // cache valid - no need to update value, update version

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
            test.SetMembers (new int[] {0, 9}); // cache valid - update cache value and version

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
            test.SetMembers (new int[] {0, 9}, false); // invalidates cache

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
            test.SetMembers (new int[] {0, 9}); // cache invalidated - does not update cache

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
        public void SetMembers_FromFalseToFalse () {

            // arrange
            var test = BitSetArray.Size (10);
            var items = new int [] {int.MinValue, -1, 0, 8, 0, 8, int.MaxValue};

            // act
            test.SetMembers (items, false);

            // assert
            Assert.That (test.Count == 0);
            Assert.False (test[0]);
            Assert.False (test[1]);
            Assert.False (test[7]);
            Assert.False (test[8]);
            Assert.False (test[9]);

        }

        [Test]
        public void SetMembers_FromTrueToFalse_Version () {

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
            test.SetMembers (items, false);

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
            test.SetMembers (new int[] {4, 6}, false); // cache valid - no need to update value, update version

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
            test.SetMembers (new int[] {0, 9}, false); // invalidates cache

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

            test.SetMembers (new int[] {3, 5}, false); // cache invalidated - does not update cache

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
            test.SetMembers (new int[] {0, 9}); // cache valid - update cache value and version

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
        public void SetMembers_FromTrueToTrue () {

            // arrange
            var test = BitSetArray.Size (10, true);
            var items = new int [] {int.MinValue, -1, 0, 8, 0, 8, int.MaxValue};

            // act
            test.SetMembers (items);

            // assert
            Assert.That (test.Count == 10);
            Assert.That (test[0]);
            Assert.That (test[1]);
            Assert.That (test[7]);
            Assert.That (test[8]);
            Assert.That (test[9]);
        }
    }
}
