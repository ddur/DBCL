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
        // index get is less restrictive than Get - no ArgumentOutOfRangeException thrown
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

            test = BitSetArray.Size ();
            Assert.That (test[int.MinValue] == false);
            Assert.That (test[-1] == false);
            Assert.That (test[0] == false);
            Assert.That (test[1] == false);
            Assert.That (test[int.MaxValue] == false);
        }

        [Test]
        // index set is less restrictive than Set - no ArgumentOutOfRangeException thrown if value is false
        public void IndexSet () {
            BitSetArray test = BitSetArray.From (0, 5, 10);
            Assert.That (delegate {
                test[int.MinValue] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[int.MinValue] = true;
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

            Assert.That (delegate {
                test[-1] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[-1] = true;
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

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
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                test[int.MaxValue] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[int.MaxValue] = true;
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

            test = BitSetArray.Size ();
            Assert.That (delegate {
                test[int.MinValue] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[int.MinValue] = true;
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

            Assert.That (delegate {
                test[-1] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[-1] = true;
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

            Assert.That (delegate {
                test[0] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[0] = true;
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

            Assert.That (delegate {
                test[1] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[1] = true;
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

            Assert.That (delegate {
                test[int.MaxValue] = false;
            }, Throws.Nothing);
            Assert.That (delegate {
                test[int.MaxValue] = true;
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
        }

        [Test]
        public void BitGet () {

            BitSetArray test = BitSetArray.Mask (new int[] { 1057 }, 11);
            Assert.That (delegate {
                test.Get (int.MinValue);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                test.Get (-1);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
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
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

            test = BitSetArray.Size ();
            Assert.That (delegate {
                test.Get (int.MinValue);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                test.Get (-1);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                test.Get (0);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Get (1);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Get (int.MaxValue);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
        }

        [Test]
        public void BitSet () {
            BitSetArray test = BitSetArray.From (0, 5, 10);
            Assert.That (delegate {
                test.Set (int.MinValue, false);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (int.MinValue, true);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

            Assert.That (delegate {
                test.Set (-1, false);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (-1, true);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

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
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (int.MaxValue, true);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

            test = BitSetArray.Size ();
            Assert.That (delegate {
                test.Set (int.MinValue, true);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (-1, true);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (0, true);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (1, true);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (int.MaxValue, true);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

            Assert.That (delegate {
                test.Set (int.MinValue, false);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (-1, false);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (0, false);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (1, false);
            }, Throws.TypeOf<IndexOutOfRangeException> ());
            Assert.That (delegate {
                test.Set (int.MaxValue, false);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
        }

        IEnumerable<BitSetArray> BitSetAllSource {
            get {
                yield return BitSetArray.Size ();
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
