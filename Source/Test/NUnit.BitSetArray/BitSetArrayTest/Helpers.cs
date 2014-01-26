// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

namespace DD.Collections.BitSetArrayTest {

    [TestFixture]
    public class Helpers {
        [Test]
        public void CountOnBits () {
            Assert.That (BitSetArray.CountOnBits (0ul) == 0);
            Assert.That (BitSetArray.CountOnBits (18446744073709551615ul) == 64);
            Assert.That (BitSetArray.CountOnBits (0xFFFFFFFFFFFFFFFFul) == 64);

            Random r = new Random ();
            int[] intBits = new int[100];
            for ( int i = 0; i < intBits.Length; i++ ) {
                intBits[i] = r.Next (int.MinValue, int.MaxValue);
            }

            // CodeCover 0 & .MaxValue
            intBits[10] = 0;
            intBits[11] = 0;
            intBits[50] = -1;
            intBits[51] = -1;

            BitSetArray test = BitSetArray.Mask (intBits, intBits.Length * 32);
            BitSetArray read;
            Assert.That (test.Count >= 100);
            test.Length -= 4; // CodeCoverage: force range-end not equal to the end of last (64 bit) segment 

            int count = 0;
            foreach ( byte mask in test.To8BitMask () ) {
                count += BitSetArray.CountOnBits (mask);
            }
            Assert.That (count == test.Count);
            read = BitSetArray.Mask (test.To8BitMask (), test.Length);
            Assert.That (read.SetEquals (test));

            count = 0;
            foreach ( short mask in test.To16BitMask () ) {
                count += BitSetArray.CountOnBits (mask);
            }
            Assert.That (count == test.Count);
            read = BitSetArray.Mask (test.To16BitMask (), test.Length);
            Assert.That (read.SetEquals (test));

            count = 0;
            foreach ( int mask in test.To32BitMask () ) {
                count += BitSetArray.CountOnBits (mask);
            }
            Assert.That (count == test.Count);
            read = BitSetArray.Mask (test.To32BitMask (), test.Length);
            Assert.That (read.SetEquals (test));

            count = 0;
            foreach ( long mask in test.To64BitMask () ) {
                count += BitSetArray.CountOnBits (mask);
            }
            Assert.That (count == test.Count);
            read = BitSetArray.Mask (test.To64BitMask (), test.Length);
            Assert.That (read.SetEquals (test));

            count = 0;
            foreach ( int item in test.ToItems () ) {
                count += 1;
            }
            Assert.That (count == test.Count);
            read = BitSetArray.From (test.ToItems ());
            Assert.That (read.SetEquals (test));

            Assert.That (BitSetArray.CountOnBits (test.ToBitArray ()) == test.Count);
            read = BitSetArray.Mask (test.ToBitArray (), test.Length);
            Assert.That (read.SetEquals (test));

            // code cover ToBitArray where BitArray.Length matches read.Length
            read.Length = read.Capacity;
            var tmp = read.ToBitArray ();

            Assert.That (BitSetArray.CountOnBits (test.ToBoolMask ()) == test.Count);
            read = BitSetArray.Mask (test.ToBoolMask (), test.Length);
            Assert.That (read.SetEquals (test));

            Assert.That (delegate {
                BitSetArray.CountOnBits ((BitArray)null);
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.CountOnBits (new BitArray (0));
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.CountOnBits ((bool[])null);
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.CountOnBits (new bool[0]);
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.CountOnBits ((byte[])null);
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.CountOnBits (new byte[0]);
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.CountOnBits ((short[])null);
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.CountOnBits (new short[0]);
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.CountOnBits ((int[])null);
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.CountOnBits (new int[0]);
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.CountOnBits ((int[])null, 100);
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.CountOnBits (new int[0], 100);
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.CountOnBits (new int[0], -100);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                BitSetArray.CountOnBits ((long[])null);
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.CountOnBits (new long[0]);
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.CountOnBits ((long[])null, 200);
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.CountOnBits (new long[0], 200);
            }, Throws.Nothing);
            Assert.That (delegate {
                BitSetArray.CountOnBits (new long[0], -200);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

            Assert.That (BitSetArray.CountOnBits (test.To8BitMask ()) == test.Count);
            Assert.That (BitSetArray.CountOnBits (test.To16BitMask ()) == test.Count);
            Assert.That (BitSetArray.CountOnBits (test.To32BitMask ()) == test.Count);
            Assert.That (BitSetArray.CountOnBits (test.To32BitMask (), test.Length + 65) == test.Count);
            Assert.That (BitSetArray.CountOnBits (test.To32BitMask (), 32) == BitSetArray.CountOnBits (test.To32BitMask ()[0]));
            Assert.That (BitSetArray.CountOnBits (test.To64BitMask ()) == test.Count);
            Assert.That (BitSetArray.CountOnBits (test.To64BitMask (), test.Length + 64) == test.Count);
            Assert.That (BitSetArray.CountOnBits (test.To64BitMask (), 64) == BitSetArray.CountOnBits (test.To64BitMask ()[0]));

            Assert.That (BitSetArray.CountOnBits ((IEnumerable<bool>)test.ToBoolMask ()) == test.Count);
            Assert.That (BitSetArray.CountOnBits ((IEnumerable<byte>)test.To8BitMask ()) == test.Count);
            Assert.That (BitSetArray.CountOnBits ((IEnumerable<short>)test.To16BitMask ()) == test.Count);
            Assert.That (BitSetArray.CountOnBits ((IEnumerable<int>)test.To32BitMask ()) == test.Count);
            Assert.That (BitSetArray.CountOnBits ((IEnumerable<long>)test.To64BitMask ()) == test.Count);

            Assert.That (BitSetArray.CountOnBits ((IEnumerable<bool>)null) == 0);
            Assert.That (BitSetArray.CountOnBits ((IEnumerable<byte>)null) == 0);
            Assert.That (BitSetArray.CountOnBits ((IEnumerable<short>)null) == 0);
            Assert.That (BitSetArray.CountOnBits ((IEnumerable<int>)null) == 0);
            Assert.That (BitSetArray.CountOnBits ((IEnumerable<long>)null) == 0);

            Assert.That (BitSetArray.CountOnBits ((IEnumerable<bool>)new bool[0]) == 0);
            Assert.That (BitSetArray.CountOnBits ((IEnumerable<byte>)new byte[0]) == 0);
            Assert.That (BitSetArray.CountOnBits ((IEnumerable<byte>)new byte[3] { 0, 255, 0 }) == 8);
            Assert.That (BitSetArray.CountOnBits ((IEnumerable<short>)new short[0]) == 0);
            Assert.That (BitSetArray.CountOnBits ((IEnumerable<int>)new int[0]) == 0);
            Assert.That (BitSetArray.CountOnBits ((IEnumerable<long>)new long[0]) == 0);
        }

        [Test]
        public void ValidRangeAndItem () {
            Assert.That (BitSetArray.ValidMember (int.MinValue) == false);
            Assert.That (BitSetArray.ValidMember (int.MinValue / 2) == false);
            Assert.That (BitSetArray.ValidMember (-1) == false);
            Assert.That (BitSetArray.ValidMember (0) == true);
            Assert.That (BitSetArray.ValidMember (int.MaxValue / 2) == true);
            Assert.That (BitSetArray.ValidMember (int.MaxValue - 1) == true);
            Assert.That (BitSetArray.ValidMember (int.MaxValue) == false);

            Assert.That (BitSetArray.ValidLength (int.MinValue) == false);
            Assert.That (BitSetArray.ValidLength (int.MinValue / 2) == false);
            Assert.That (BitSetArray.ValidLength (-1) == false);
            Assert.That (BitSetArray.ValidLength (0) == true);
            Assert.That (BitSetArray.ValidLength (int.MaxValue / 2) == true);
            Assert.That (BitSetArray.ValidLength (int.MaxValue - 1) == true);
            Assert.That (BitSetArray.ValidLength (int.MaxValue) == true);

        }

        [Test]
        public void TestVersion () {
            // CodeCover: this.AddVersion
            BitSetArray test = BitSetArray.From (0);
            // set test.version to value close to int.MaxValue
            test.Version = int.MaxValue - 10;
            for ( int i = 0; i < 10; i++ ) {
                test.Set (0, true);
                test.Set (0, false);
            }
            // confirm version number looped back into int.MinValue
            Assert.That (test.Version == int.MinValue + 8, (test.Version - int.MinValue).ToString ());
        }

        [Test]
        public void GetArrayLength () {

            Assert.That (delegate {
                BitSetArray.GetByteArrayLength (-1);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                BitSetArray.GetByteArrayLength (int.MinValue);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

            Assert.That (BitSetArray.GetByteArrayLength (0) == 0);
            Assert.That (BitSetArray.GetByteArrayLength (1) == 1);
            Assert.That (BitSetArray.GetByteArrayLength (8) == 1);
            Assert.That (BitSetArray.GetByteArrayLength (9) == 2);
            Assert.That (BitSetArray.GetByteArrayLength (16) == 2);
            Assert.That (BitSetArray.GetByteArrayLength (17) == 3);
            Assert.That (BitSetArray.GetByteArrayLength (int.MaxValue) == (int.MaxValue / 8) + 1);

            Assert.That (delegate {
                BitSetArray.GetShortArrayLength (-1);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                BitSetArray.GetShortArrayLength (int.MinValue);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

            Assert.That (BitSetArray.GetShortArrayLength (0) == 0);
            Assert.That (BitSetArray.GetShortArrayLength (1) == 1);
            Assert.That (BitSetArray.GetShortArrayLength (16) == 1);
            Assert.That (BitSetArray.GetShortArrayLength (17) == 2);
            Assert.That (BitSetArray.GetShortArrayLength (32) == 2);
            Assert.That (BitSetArray.GetShortArrayLength (33) == 3);
            Assert.That (BitSetArray.GetShortArrayLength (int.MaxValue) == (int.MaxValue / 16) + 1);

            Assert.That (delegate {
                BitSetArray.GetIntArrayLength (-1);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                BitSetArray.GetIntArrayLength (int.MinValue);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

            Assert.That (BitSetArray.GetIntArrayLength (0) == 0);
            Assert.That (BitSetArray.GetIntArrayLength (1) == 1);
            Assert.That (BitSetArray.GetIntArrayLength (32) == 1);
            Assert.That (BitSetArray.GetIntArrayLength (33) == 2);
            Assert.That (BitSetArray.GetIntArrayLength (64) == 2);
            Assert.That (BitSetArray.GetIntArrayLength (65) == 3);
            Assert.That (BitSetArray.GetIntArrayLength (int.MaxValue) == (int.MaxValue / 32) + 1);

            Assert.That (delegate {
                BitSetArray.GetLongArrayLength (-1);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                BitSetArray.GetLongArrayLength (int.MinValue);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

            Assert.That (BitSetArray.GetLongArrayLength (0) == 0);
            Assert.That (BitSetArray.GetLongArrayLength (1) == 1);
            Assert.That (BitSetArray.GetLongArrayLength (64) == 1);
            Assert.That (BitSetArray.GetLongArrayLength (65) == 2);
            Assert.That (BitSetArray.GetLongArrayLength (128) == 2);
            Assert.That (BitSetArray.GetLongArrayLength (129) == 3);
            Assert.That (BitSetArray.GetLongArrayLength (int.MaxValue) == (int.MaxValue / 64) + 1);

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
