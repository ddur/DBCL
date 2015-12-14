// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
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
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Get (-1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
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
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Get (int.MaxValue);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            test = BitSetArray.Empty ();
            Assert.That (delegate {
                test.Get (int.MinValue);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Get (-1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Get (0);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Get (1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Get (int.MaxValue);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
        }

        [Test]
        public void BitSet_Debug () {
#if DEBUG
            // On RELEASE there is no access to private members
            var test = BitSetArray.Size (10);

            test.Set (5); // first/last cached
            int expected_version = int.MinValue;
            Assert.AreEqual (expected_version, test.Version);

            Assert.AreEqual (test.Version, test.StartVersion);
            Assert.AreEqual (test.Version, test.FinalVersion);

            Assert.AreEqual (5, test.StartMemoize);
            Assert.AreEqual (5, test.FinalMemoize);

            // update cache.First
            test.Set (3);
            ++expected_version;

            Assert.AreEqual (expected_version, test.Version);

            Assert.AreEqual (test.Version, test.StartVersion);
            Assert.AreEqual (test.Version, test.FinalVersion);

            Assert.AreEqual (3, test.StartMemoize);
            Assert.AreEqual (5, test.FinalMemoize);

            // update cache.Last
            test.Set (7);
            ++expected_version;

            Assert.AreEqual (expected_version, test.Version);

            Assert.AreEqual (test.Version, test.StartVersion);
            Assert.AreEqual (test.Version, test.FinalVersion);

            Assert.AreEqual (3, test.StartMemoize);
            Assert.AreEqual (7, test.FinalMemoize);

            // cache is alive (version updated)
            test.Set (4);
            ++expected_version;

            Assert.AreEqual (expected_version, test.Version);

            Assert.AreEqual (test.Version, test.StartVersion);
            Assert.AreEqual (test.Version, test.FinalVersion);

            Assert.AreEqual (3, test.StartMemoize);
            Assert.AreEqual (7, test.FinalMemoize);

            // invalidate cache.First
            test.Set (3, false);
            ++expected_version;

            Assert.AreEqual (expected_version, test.Version);

            Assert.AreNotEqual (test.Version, test.StartVersion);
            Assert.AreEqual (test.Version, test.FinalVersion);

            Assert.AreEqual (3, test.StartMemoize);
            Assert.AreEqual (7, test.FinalMemoize);

            // cache.First is invalidated (dead)
            test.Set (3);
            ++expected_version;

            Assert.AreEqual (expected_version, test.Version);

            Assert.AreNotEqual (test.Version, test.StartVersion);
            Assert.AreEqual (test.Version, test.FinalVersion);

            Assert.AreEqual (3, test.StartMemoize);
            Assert.AreEqual (7, test.FinalMemoize);

            // cache.First is invalidated (dead)
            test.Set (3, false);
            ++expected_version;

            Assert.AreEqual (expected_version, test.Version);

            Assert.AreNotEqual (test.Version, test.StartVersion);
            Assert.AreEqual (test.Version, test.FinalVersion);

            Assert.AreEqual (3, test.StartMemoize);
            Assert.AreEqual (7, test.FinalMemoize);

            // invalidate cache.Last
            test.Set (7, false);
            ++expected_version;

            // cache.Last is invalidated (dead)
            Assert.AreEqual (expected_version, test.Version);

            Assert.AreNotEqual (test.Version, test.StartVersion);
            Assert.AreNotEqual (test.Version, test.FinalVersion);

            Assert.AreEqual (3, test.StartMemoize);
            Assert.AreEqual (7, test.FinalMemoize);

            // reanimate cache.First
            var ignore = test.First;
            Assert.AreEqual (expected_version, test.Version);

            Assert.AreEqual (test.Version, test.StartVersion);
            Assert.AreNotEqual (test.Version, test.FinalVersion);

            Assert.AreEqual (4, test.StartMemoize);
            Assert.AreEqual (7, test.FinalMemoize);

            // reanimate cache.Last
            var myLast = test.Last;
            Assert.AreEqual (expected_version, test.Version);

            Assert.AreEqual (test.Version, test.StartVersion);
            Assert.AreEqual (test.Version, test.FinalVersion);

            Assert.AreEqual (4, test.StartMemoize);
            Assert.AreEqual (5, test.FinalMemoize);

            var reset = new List<int> (test);
            foreach (var item in reset) {
                test.Set (item, false);
            }
            Assert.AreEqual (0, test.Count);

#endif
        }

        [Test]
        public void BitSet_Random () {
            var r = new Random ();
            int random_item = 0;
            var test = BitSetArray.Size (1000);
            for (int i = 0; i < 1000; i++) {
                test.Set (r.Next (0, 999));
                test.Set (r.Next (0, 999), false);
            }
            for (int i = 0; i < 1000; i++) {
                random_item = r.Next (0, 999);
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
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (int.MinValue, true);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            Assert.That (delegate {
                test.Set (-1, false);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (-1, true);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

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

            Assert.That (delegate {
                test.Set (11, false);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (11, true);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (int.MaxValue, false);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (int.MaxValue, true);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            test = BitSetArray.Empty ();
            Assert.That (delegate {
                test.Set (int.MinValue, true);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (-1, true);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (0, true);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (1, true);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (int.MaxValue, true);
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            Assert.That (delegate {
                test.Set (int.MinValue, false);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (-1, false);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (0, false);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (1, false);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (int.MaxValue, false);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
        }

        [Test]
        public void Bit_SetItems () {
#if DEBUG // _SetItems private to public.
            // Only public members contains Contract.Require<Exception>
            Assert.That (delegate {
                BitSetArray.Size (2)._SetItems (new int[] { 0 });
            }, Throws.Nothing);

            var test = BitSetArray.Size (2);
            Assert.That (delegate {
                test._SetItems (null);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                test._SetItems (new int[0]);
            }, Throws.TypeOf<ArgumentEmptyException> ());
            Assert.That (delegate {
                test._SetItems (new int[] { int.MinValue });
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test._SetItems (new int[] { -1 });
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test._SetItems (new int[] { int.MaxValue });
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            Assert.GreaterOrEqual (5, test.Length);
            Assert.That (delegate {
                test._SetItems (new int[] { 5 });
            }, Throws.TypeOf<IndexOutOfRangeException> ());

            test._SetItems (new int[] { 0 });
            Assert.AreNotEqual (0, test.Count);
            Assert.That (delegate {
                test._SetItems (new int[] { 0 });
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
