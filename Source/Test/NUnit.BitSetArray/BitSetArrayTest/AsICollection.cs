// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace DD.Collections.BitSetArrayTest.Interfaces {

    [TestFixture]
    public class AsICollection {
        Random r = new Random ();

        [Test]
        public void Add () {
            BitSetArray bs = new BitSetArray ();
            Assert.That (delegate {
                ((ICollection<int>)bs).Add (int.MinValue);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                ((ICollection<int>)bs).Add (-1);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

            int item;
            for ( int i = 0; i < 100; i++ ) {
                item = r.Next (0, 1000);
                ((ICollection<int>)bs).Add (item);
                Assert.That (bs[item]);
            }
        }

        [Test]
        public void Remove () {
            BitSetArray bs = new BitSetArray (100, true);

            int item;
            for ( int i = 0; i < 100; i++ ) {
                item = r.Next (-100, 100);
                Assert.That (bs[item] == bs.Remove (item));
                Assert.That (!bs[item]);
            }
        }

        [Test]
        public void Clear () {
            int len;
            int ver;
            BitSetArray bs = new BitSetArray ();
            ver = bs.Version;
            bs.Clear ();
            Assert.That (bs.Count == 0);
            Assert.That (bs.Length == 0);
            Assert.That (bs.Version == ver);
            for ( int i = 0; i < 10; i++ ) {
                len = r.Next (1, 100);
                bs = new BitSetArray (len, true);
                ver = bs.Version;
                Assert.That (bs.Count == len);
                bs.Clear ();
                Assert.That (bs.Count == 0);
                Assert.That (bs.Length == len);
                Assert.That (bs.Version != ver);
            }
        }

        [Test]
        public void Contains () {
            BitSetArray test = BitSetArray.From (0, 5, 10);
            Assert.That (test.Contains (int.MinValue) == false);
            Assert.That (test.Contains (-1) == false);
            Assert.That (test.Contains (0) == true);
            Assert.That (test.Contains (1) == false);
            Assert.That (test.Contains (2) == false);
            Assert.That (test.Contains (3) == false);
            Assert.That (test.Contains (4) == false);
            Assert.That (test.Contains (5) == true);
            Assert.That (test.Contains (6) == false);
            Assert.That (test.Contains (7) == false);
            Assert.That (test.Contains (8) == false);
            Assert.That (test.Contains (9) == false);
            Assert.That (test.Contains (10) == true);
            Assert.That (test.Contains (11) == false);
            Assert.That (test.Contains (int.MaxValue) == false);

            test = new BitSetArray ();
            Assert.That (test.Contains (int.MinValue) == false);
            Assert.That (test.Contains (-1) == false);
            Assert.That (test.Contains (0) == false);
            Assert.That (test.Contains (1) == false);
            Assert.That (test.Contains (int.MaxValue) == false);
        }

        [Test]
        public void CopyToArrayClass () {
            BitSetArray bs = BitSetArray.From (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20);
            Array failArray;
            failArray = null; // Null reference
            Assert.That (delegate {
                bs.CopyTo (failArray, 0);
            }, Throws.TypeOf<ArgumentNullException> ());
            failArray = new int[10, 10]; // Invalid Rank
            Assert.That (delegate {
                bs.CopyTo (failArray, 0);
            }, Throws.TypeOf<ArgumentException> ());
            failArray = new byte[10]; // Invalid Type
            Assert.That (delegate {
                bs.CopyTo (failArray, 0);
            }, Throws.TypeOf<ArgumentException> ());

            Array copyArray;
            copyArray = new int[100];
            Assert.That (delegate {
                bs.CopyTo (copyArray, -1);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                bs.CopyTo (copyArray, int.MinValue);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                bs.CopyTo (copyArray, copyArray.Length - bs.Count + 1);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                bs.CopyTo (copyArray, copyArray.Length);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                bs.CopyTo (copyArray, copyArray.Length - 1);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                bs.CopyTo (copyArray, copyArray.Length + 1);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

            Assert.That (delegate {
                bs.CopyTo (copyArray, 0);
            }, Throws.Nothing);
            Assert.That (delegate {
                bs.CopyTo (copyArray, copyArray.Length - bs.Count);
            }, Throws.Nothing);
            Assert.That ((int)copyArray.GetValue (0) == 1);
            Assert.That ((int)copyArray.GetValue (19) == 20);
            Assert.That ((int)copyArray.GetValue (80) == 1);
            Assert.That ((int)copyArray.GetValue (99) == 20);

        }

        [Test]
        public void CopyToArrayOfInt () {
            BitSetArray bs = BitSetArray.From (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20);
            int[] nullArray;
            int[] copyArray;
            nullArray = null;
            Assert.Throws<ArgumentNullException> (delegate {
                bs.CopyTo (nullArray, 0);
            });
            Assert.Throws<ArgumentNullException> (delegate {
                bs.CopyTo (null, 0);
            });

            copyArray = new int[100];
            Assert.That (delegate {
                bs.CopyTo (copyArray, -1);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                bs.CopyTo (copyArray, int.MinValue);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                bs.CopyTo (copyArray, copyArray.Length - bs.Count + 1);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                bs.CopyTo (copyArray, copyArray.Length);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                bs.CopyTo (copyArray, copyArray.Length - 1);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                bs.CopyTo (copyArray, copyArray.Length + 1);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

            Assert.That (delegate {
                bs.CopyTo (copyArray, 0);
            }, Throws.Nothing);
            Assert.That (delegate {
                bs.CopyTo (copyArray, copyArray.Length - bs.Count);
            }, Throws.Nothing);
            Assert.That ((int)copyArray.GetValue (0) == 1);
            Assert.That ((int)copyArray.GetValue (19) == 20);
            Assert.That ((int)copyArray.GetValue (80) == 1);
            Assert.That ((int)copyArray.GetValue (99) == 20);

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
