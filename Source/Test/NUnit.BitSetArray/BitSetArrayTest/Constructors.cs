// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;

using NUnit.Framework;

namespace DD.Collections.BitSetArrayTest {

    [TestFixture]
    public class Constructors {
        [Test]
        public void ConstructEmptyEmpty () {
            BitSetArray test;
            test = new BitSetArray ();
            Assert.That (test.Count == 0, Is.True);
            Assert.That (test.Length == 0, Is.True);
            Assert.That (test.First == null, Is.True);
            Assert.That (test.Last == null, Is.True);
        }

        [Test]
        public void ConstructEmptyWithLength () {

            // Exceptions
            Assert.That (delegate {
                new BitSetArray (-1);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                new BitSetArray (int.MinValue);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                new BitSetArray (new Random ().Next (-1, int.MinValue));
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                new BitSetArray (0, true);
            }, Throws.TypeOf<ArgumentException> ());

            Assert.That (delegate {
                new BitSetArray () { -1 };
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                new BitSetArray () { int.MinValue };
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                new BitSetArray () { new Random ().Next (-1, int.MinValue) };
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());

            BitSetArray test;

            test = new BitSetArray (0);
            Assert.That (test.Count == 0, Is.True);
            Assert.That (test.Length == 0, Is.True);
            Assert.That (test.First == null, Is.True);
            Assert.That (test.Last == null, Is.True);

            test = new BitSetArray (0x110000);
            Assert.That (test.Count == 0, Is.True);
            Assert.That (test.Length == 0x110000, Is.True);
            Assert.That (test.First == null, Is.True);
            Assert.That (test.Last == null, Is.True);

#if MAXCOVERAGE
            test = null;
            GC.Collect ();

            test = new BitSetArray (int.MaxValue);
            Assert.That (test.Count == 0, Is.True);
            Assert.That (test.Length == int.MaxValue, Is.True);
            Assert.That (test.First == null, Is.True);
            Assert.That (test.Last == null, Is.True);

            test = null;
            GC.Collect ();
#endif

        }

        [Test]
        public void ConstructWithDefaultValue () {
            // Exceptions
            Assert.That (delegate {
                new BitSetArray (-1, true);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                new BitSetArray (int.MinValue, true);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());
            Assert.That (delegate {
                new BitSetArray (new Random ().Next (-1, int.MinValue), true);
            }, Throws.TypeOf<ArgumentOutOfRangeException> ());


            BitSetArray test;

            test = new BitSetArray (0, false);
            Assert.That (test.Count == 0, Is.True);
            Assert.That (test.Length == 0, Is.True);
            Assert.That (test.First == null, Is.True);
            Assert.That (test.Last == null, Is.True);

            test = new BitSetArray (0, false);
            Assert.That (test.Count == 0, Is.True);
            Assert.That (test.Length == 0, Is.True);
            Assert.That (test.First == null, Is.True);
            Assert.That (test.Last == null, Is.True);

            test = new BitSetArray (0x110000, true);
            Assert.That (test.Count == 0x110000, Is.True);
            Assert.That (test.Length == test.Count, Is.True);
            Assert.That (test.First == 0, Is.True);
            Assert.That (test.Last == test.Length - 1, Is.True);

            test = new BitSetArray (0x110000, false);
            Assert.That (test.Count == 0, Is.True);
            Assert.That (test.Length == 0x110000, Is.True);
            Assert.That (test.First == null, Is.True);
            Assert.That (test.Last == null, Is.True);

#if MAXCOVERAGE
            test = null;
            GC.Collect ();

            test = new BitSetArray (int.MaxValue, true);
            Assert.That (test.Count == int.MaxValue, Is.True);
            Assert.That (test.Length == test.Count, Is.True);
            Assert.That (test.First == 0, Is.True);
            Assert.That (test.Last == test.Length - 1, Is.True);

            test = null;
            GC.Collect ();
#endif

        }

        [Test]
        public void ConstructCopy () {
            // Exceptions (null reference)
            Assert.That (delegate {
                new BitSetArray ((BitSetArray)null, 0);
            }, Throws.TypeOf<ArgumentNullException> ());
            Assert.That (delegate {
                new BitSetArray ((BitSetArray)null);
            }, Throws.TypeOf<ArgumentNullException> ());

            BitSetArray test;
            BitSetArray copy;

            // copy empty
            test = new BitSetArray (0, false);
            copy = new BitSetArray (test);
            Assert.That (copy.IsNot (test));
            Assert.That (copy.Count == test.Count);
            Assert.That (copy.Length == test.Length);
            Assert.That (copy.First == test.First);
            Assert.That (copy.Last == test.Last);
            Assert.That (copy.IsNot (test));
            copy.Length = 1;
            copy.Set (0, true);
            Assert.That (copy.Count != test.Count);
            Assert.That (copy.Length != test.Length);
            Assert.That (copy.First != test.First);
            Assert.That (copy.Last != test.Last);

            int largeLen = 0x10000;
            int smallLen = 0xFF;

            // copy empty to larger space
            test = new BitSetArray (0, false);
            copy = new BitSetArray (test, largeLen);
            Assert.That (copy.IsNot (test));
            Assert.That (copy.Count == test.Count);
            Assert.That (copy.Length != test.Length);
            Assert.That (copy.Length == largeLen);
            Assert.That (copy.First == test.First);
            Assert.That (copy.Last == test.Last);
            Assert.That (copy.IsNot (test));
            copy.Set (0, true);
            Assert.That (copy.Count != test.Count);
            Assert.That (copy.Length != test.Length);
            Assert.That (copy.First != test.First);
            Assert.That (copy.Last != test.Last);

            // copy full
            test = new BitSetArray (largeLen, true);
            copy = new BitSetArray (test);
            Assert.That (copy.IsNot (test));
            Assert.That (copy.Count == test.Count);
            Assert.That (copy.Length == test.Length);
            Assert.That (copy.First == test.First);
            Assert.That (copy.Last == test.Last);
            Assert.That (copy.IsNot (test));
            copy.Set (0, false);
            Assert.That (copy.Count == test.Count - 1);
            Assert.That (copy.Length == test.Length);
            Assert.That (copy.First == test.First + 1);
            Assert.That (copy.Last == test.Last);
            copy.Set (copy.Length - 1, false);
            Assert.That (copy.Last == test.Last - 1);


            // copy full to empty space
            test = new BitSetArray (largeLen, true);
            copy = new BitSetArray (test, 0);
            Assert.That (copy.IsNot (test));
            Assert.That (copy.Count != test.Count);
            Assert.That (copy.Count == 0);
            Assert.That (copy.Length != test.Length);
            Assert.That (copy.Length == 0);
            Assert.That (copy.First == null);
            Assert.That (copy.Last == null);


            // copy full to smaller space
            test = new BitSetArray (largeLen, true);
            copy = new BitSetArray (test, largeLen - 1);
            Assert.That (copy.IsNot (test));
            Assert.That (copy.Count == test.Count - 1);
            Assert.That (copy.Length == test.Length - 1);
            Assert.That (copy.First == test.First);
            Assert.That (copy.Last == test.Last - 1);


            // copy full to smal space
            test = new BitSetArray (largeLen, true);
            copy = new BitSetArray (test, smallLen);
            Assert.That (copy.IsNot (test));
            Assert.That (copy.Count == smallLen);
            Assert.That (copy.Length == smallLen);
            Assert.That (copy.First == test.First);
            Assert.That (copy.Last == smallLen - 1);


            // copy small to larger space
            test = new BitSetArray (smallLen, true);
            copy = new BitSetArray (test, largeLen);
            Assert.That (copy.IsNot (test));
            Assert.That (copy.Count == test.Count);
            Assert.That (copy.Length == largeLen);
            Assert.That (copy.First == test.First);
            Assert.That (copy.Last == test.Last);

#if MAXCOVERAGE
            test = null;
            copy = null;
            GC.Collect ();

            test = new BitSetArray (int.MaxValue, true);
            copy = new BitSetArray (test);
            Assert.That (copy.IsNot (test));
            Assert.That (copy.Count == test.Count);
            Assert.That (copy.Length == test.Length);
            Assert.That (copy.First == test.First);
            Assert.That (copy.Last == test.Last);

            copy.Set (0, false);
            Assert.That (copy.Count == test.Count - 1);
            Assert.That (copy.Length == test.Length);
            Assert.That (copy.First == test.First + 1);
            Assert.That (copy.Last == test.Last);
            copy.Set ((int)copy.Last, false);
            Assert.That (copy.Count == test.Count - 2);
            Assert.That (copy.Last == test.Last - 1);

            test = null;
            copy = null;
            GC.Collect ();
#endif
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
