// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;

using NUnit.Framework;

namespace DD.Collections.BitSetArrayTest.Interfaces {

    [TestFixture]
    public class AsIComparable {

        [Test]
        public void CompareTo () {
            var bsA = BitSetArray.Empty ();
            var bsB = BitSetArray.Empty ();

            Assert.That (bsA.CompareTo ((BitSetArray)null) == 0);
            Assert.That (bsA.Equals ((BitSetArray)null));

            Assert.That (bsA.CompareTo (bsB) == 0);
            Assert.That (bsB.CompareTo (bsA) == 0);
            Assert.That (bsA.CompareTo (bsA) == 0);
            Assert.That (bsB.CompareTo (bsB) == 0);
            Assert.That (bsA.GetHashCode () == bsB.GetHashCode ());
            Assert.That (bsA.Equals (bsB));
            Assert.That (bsB.Equals (bsA));

            bsA.Add (10);
            Assert.That (bsA.CompareTo (bsB) > 0);
            Assert.That (bsB.CompareTo (bsA) < 0);
            Assert.That (bsA.GetHashCode () != bsB.GetHashCode ());
            Assert.That (!bsA.Equals (bsB));
            Assert.That (!bsB.Equals (bsA));

            bsB.Add (10);
            Assert.That (bsA.CompareTo (bsB) == 0);
            Assert.That (bsB.CompareTo (bsA) == 0);
            Assert.That (bsA.CompareTo (bsA) == 0);
            Assert.That (bsB.CompareTo (bsB) == 0);
            Assert.That (bsA.GetHashCode () == bsB.GetHashCode ());
            Assert.That (bsA.Equals (bsB));
            Assert.That (bsB.Equals (bsA));

            bsB.Add (11);
            Assert.That (bsA.CompareTo (bsB) < 0);
            Assert.That (bsB.CompareTo (bsA) > 0);
            Assert.That (bsA.GetHashCode () != bsB.GetHashCode ());
            Assert.That (!bsA.Equals (bsB));
            Assert.That (!bsB.Equals (bsA));

            bsA.Add (1000);
            Assert.That (bsA.CompareTo (bsB) > 0);
            Assert.That (bsB.CompareTo (bsA) < 0);
            Assert.That (bsA.GetHashCode () != bsB.GetHashCode ());
            Assert.That (!bsA.Equals (bsB));
            Assert.That (!bsB.Equals (bsA));

            bsA.Length = 2000;
            Assert.That (bsA.CompareTo (bsB) > 0);
            Assert.That (bsB.CompareTo (bsA) < 0);
            Assert.That (bsA.GetHashCode () != bsB.GetHashCode ());
            Assert.That (!bsA.Equals (bsB));
            Assert.That (!bsB.Equals (bsA));

        }

        [TestFixtureSetUp]
        public void Init () {
            GC.Collect ();
        }

        [TestFixtureTearDown]
        public void Kill () {
            GC.Collect ();
        }
    }
}
