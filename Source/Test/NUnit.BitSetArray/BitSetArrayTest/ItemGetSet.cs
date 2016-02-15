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
    public class ItemGetSetSetAllTrimExcess {

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
        public void GetMember () {
            BitSetArray test = BitSetArray.Mask (new int[] { 1057 }, 11);
            Assert.That (delegate {
                test.GetMember (int.MinValue);
            }, Throws.Nothing);
            Assert.That (test.GetMember (int.MinValue) == false);
            Assert.That (delegate {
                test.GetMember (-1);
            }, Throws.Nothing);
            Assert.That (test.GetMember (-1) == false);

            Assert.That (test.GetMember (0) == true);
            Assert.That (test.GetMember (1) == false);
            Assert.That (test.GetMember (2) == false);
            Assert.That (test.GetMember (3) == false);
            Assert.That (test.GetMember (4) == false);
            Assert.That (test.GetMember (5) == true);
            Assert.That (test.GetMember (6) == false);
            Assert.That (test.GetMember (7) == false);
            Assert.That (test.GetMember (8) == false);
            Assert.That (test.GetMember (9) == false);
            Assert.That (test.GetMember (10) == true);
            Assert.That (delegate {
                test.GetMember (11);
            }, Throws.Nothing);
            Assert.That (test.GetMember (11) == false);
            Assert.That (delegate {
                test.GetMember (int.MaxValue);
            }, Throws.Nothing);
            Assert.That (test.GetMember (int.MaxValue) == false);

            test = BitSetArray.Empty ();
            Assert.That (delegate {
                test.GetMember (int.MinValue);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.GetMember (-1);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.GetMember (0);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.GetMember (1);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.GetMember (int.MaxValue);
            }, Throws.Nothing);
            Assert.That (test.GetMember (int.MinValue) == false);
            Assert.That (test.GetMember (-1) == false);
            Assert.That (test.GetMember (0) == false);
            Assert.That (test.GetMember (1) == false);
            Assert.That (test.GetMember (int.MaxValue) == false);
        }

        [Test]
        public void SetMember_CheckVersion () {

            var test = BitSetArray.Size (10);

            test.SetMember (5); // first/last cached
            int expected_version = int.MinValue;
            Assert.AreEqual (expected_version, test.Version);

            Assert.AreEqual (test.Version, test.CachedFirstItem_Version);
            Assert.AreEqual (test.Version, test.CachedLastItem_Version);

            Assert.AreEqual (5, test.CachedFirstItem_Value);
            Assert.AreEqual (5, test.CachedLastItem_Value);

            // update cache.First
            test.SetMember (3);
            ++expected_version;

            Assert.AreEqual (expected_version, test.Version);

            Assert.AreEqual (test.Version, test.CachedFirstItem_Version);
            Assert.AreEqual (test.Version, test.CachedLastItem_Version);

            Assert.AreEqual (3, test.CachedFirstItem_Value);
            Assert.AreEqual (5, test.CachedLastItem_Value);

            // update cache.Last
            test.SetMember (7);
            ++expected_version;

            Assert.AreEqual (expected_version, test.Version);

            Assert.AreEqual (test.Version, test.CachedFirstItem_Version);
            Assert.AreEqual (test.Version, test.CachedLastItem_Version);

            Assert.AreEqual (3, test.CachedFirstItem_Value);
            Assert.AreEqual (7, test.CachedLastItem_Value);

            // cache is alive (version updated)
            test.SetMember (4);
            ++expected_version;

            Assert.AreEqual (expected_version, test.Version);

            Assert.AreEqual (test.Version, test.CachedFirstItem_Version);
            Assert.AreEqual (test.Version, test.CachedLastItem_Version);

            Assert.AreEqual (3, test.CachedFirstItem_Value);
            Assert.AreEqual (7, test.CachedLastItem_Value);

            // invalidate cache.First
            test.SetMember (3, false);
            ++expected_version;

            Assert.AreEqual (expected_version, test.Version);

            Assert.AreNotEqual (test.Version, test.CachedFirstItem_Version);
            Assert.AreEqual (test.Version, test.CachedLastItem_Version);

            Assert.AreEqual (3, test.CachedFirstItem_Value);
            Assert.AreEqual (7, test.CachedLastItem_Value);

            // cache.First is invalidated (dead)
            test.SetMember (3);
            ++expected_version;

            Assert.AreEqual (expected_version, test.Version);

            Assert.AreNotEqual (test.Version, test.CachedFirstItem_Version);
            Assert.AreEqual (test.Version, test.CachedLastItem_Version);

            Assert.AreEqual (3, test.CachedFirstItem_Value);
            Assert.AreEqual (7, test.CachedLastItem_Value);

            // cache.First is invalidated (dead)
            test.SetMember (3, false);
            ++expected_version;

            Assert.AreEqual (expected_version, test.Version);

            Assert.AreNotEqual (test.Version, test.CachedFirstItem_Version);
            Assert.AreEqual (test.Version, test.CachedLastItem_Version);

            Assert.AreEqual (3, test.CachedFirstItem_Value);
            Assert.AreEqual (7, test.CachedLastItem_Value);

            // invalidate cache.Last
            test.SetMember (7, false);
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
                test.SetMember (item, false);
            }
            Assert.AreEqual (0, test.Count);

        }

        [Test]
        public void SetMember_Random () {
            var r = new Random ();
            int random_item = 0;
            var test = BitSetArray.Size (1000);
            for (int i = 0; i < 100; i++) {
                test.SetMember (r.Next (int.MinValue, int.MaxValue));
                test.SetMember (r.Next (int.MinValue, int.MaxValue), false);
            }
            for (int i = 0; i < 100; i++) {
                random_item = r.Next (int.MinValue, int.MaxValue);
                test.SetMember (random_item);
                test.SetMember (random_item, false);
                test.SetMember (random_item);
            }
        }

        [Test]
        public void SetMember_Throws () {
            BitSetArray test = BitSetArray.From (5, 0, 10);
            Assert.That (delegate {
                test.SetMember (int.MinValue, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (int.MinValue, true);
            }, Throws.Nothing);
            Assert.False (test.SetMember (int.MinValue, false));
            Assert.False (test.SetMember (int.MinValue, true));

            Assert.That (delegate {
                test.SetMember (-1, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (-1, true);
            }, Throws.Nothing);
            Assert.False (test.SetMember (-1, false));
            Assert.False (test.SetMember (-1, true));

            Assert.That (delegate {
                test.SetMember (0, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (1, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (2, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (3, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (4, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (5, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (6, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (7, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (8, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (9, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (10, false);
            }, Throws.Nothing);

            test = BitSetArray.From (5, 0, 10);
            Assert.True (test.SetMember (0, false));
            Assert.True (test.SetMember (1, true));
            Assert.False (test.SetMember (2, false));
            Assert.True (test.SetMember (3, true));
            Assert.False (test.SetMember (4, false));
            Assert.False (test.SetMember (5, true));
            Assert.False (test.SetMember (6, false));
            Assert.True (test.SetMember (7, true));
            Assert.False (test.SetMember (8, false));
            Assert.True (test.SetMember (9, true));
            Assert.True (test.SetMember (10, false));

            Assert.That (delegate {
                test.SetMember (11, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (11, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (int.MaxValue, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (int.MaxValue, true);
            }, Throws.Nothing);

            test = BitSetArray.Empty ();
            Assert.That (delegate {
                test.SetMember (int.MinValue, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (-1, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (0, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (1, true);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (int.MaxValue, true);
            }, Throws.Nothing);

            Assert.False (test.SetMember (int.MinValue, true));
            Assert.False (test.SetMember (-1, true));
            Assert.False (test.SetMember (0, true));
            Assert.False (test.SetMember (1, true));
            Assert.False (test.SetMember (int.MaxValue, true));

            Assert.That (delegate {
                test.SetMember (int.MinValue, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (-1, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (0, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (1, false);
            }, Throws.Nothing);
            Assert.That (delegate {
                test.SetMember (int.MaxValue, false);
            }, Throws.Nothing);

            Assert.False (test.SetMember (int.MinValue, false));
            Assert.False (test.SetMember (-1, false));
            Assert.False (test.SetMember (0, false));
            Assert.False (test.SetMember (1, false));
            Assert.False (test.SetMember (int.MaxValue, false));
        }

        private IEnumerable<BitSetArray> BitSetAllSource {
            get {
                yield return BitSetArray.Empty ();
                yield return BitSetArray.Size (100, true);
            }
        }

        [Test, TestCaseSource ("BitSetAllSource")]
        public void SetAll (BitSetArray test) {

            test.SetAll (true);
            Assert.That (test.Count == test.Length);

            test.Length += 100;
            test.SetAll (true);
            Assert.That (test.Count == test.Length);

            test.SetAll (false);
            Assert.That (test.Count == 0);

            test.SetAll (false);
            Assert.That (test.Count == 0);

            test.SetMember (0, true);
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
