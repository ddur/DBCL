// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Linq;

using NUnit.Framework;

namespace DD.Collections.BitSetArrayTest.Interfaces {

    [TestFixture]
    public class AsIEquatable {
        [Test]
        public void Equals () {
            var bsA = BitSetArray.Empty ();

            Assert.That (bsA.Equals (bsA));
            Assert.That (bsA.Equals ((BitSetArray)bsA.Clone ()));
            Assert.That (bsA.Equals ((BitSetArray)null));
            Assert.That (bsA.Equals (BitSetArray.Empty ()));
            Assert.That (bsA.Equals (BitSetArray.Size (10)));

            bsA = BitSetArray.From (0, 5, 10);
            Assert.That (bsA.Equals (bsA));
            Assert.That (bsA.Equals ((BitSetArray)bsA.Clone ()));
            Assert.That (!bsA.Equals (BitSetArray.Empty ()));
            Assert.That (!bsA.Equals ((BitSetArray)null));

        }

        [Test]
        public void EqualsObject () {
            BitSetArray bsA = BitSetArray.Empty ();

            Assert.That (bsA.Equals ((object)bsA));
            Assert.That (bsA.Equals (bsA.Clone ()));
            Assert.That (bsA.Equals ((object)null));
            Assert.That (bsA.Equals (bsA.ToItems ()));
            Assert.That (bsA.Equals (bsA.ToList ()));

            bsA = BitSetArray.From (0, 5, 10);
            Assert.That (bsA.Equals ((object)bsA));
            Assert.That (bsA.Equals (bsA.Clone ()));
            Assert.That (!bsA.Equals ((object)BitSetArray.Empty ()));
            Assert.That (!bsA.Equals ((object)null));
            Assert.That (bsA.Equals (bsA.ToItems ()));
            Assert.That (bsA.Equals (bsA.ToList ()));

        }

        [Test]
        public void GetHashCode_ () {
            BitSetArray bsA = BitSetArray.Empty ();
            BitSetArray bsB = BitSetArray.Size (100);
            Assert.That (bsA.GetHashCode () == bsB.GetHashCode ());
            Assert.That (bsA.GetHashCode () == bsB.GetHashCode ()); // cover GetHashCode cache
            bsA.Add (1);
            bsB.Add (1);
            Assert.That (bsA.GetHashCode () == bsB.GetHashCode ());
            Assert.That (bsA.GetHashCode () == bsB.GetHashCode ()); // cover GetHashCode cache
            bsA.Add (20);
            Assert.That (bsA.GetHashCode () != bsB.GetHashCode ());
            bsB.Add (20);
            Assert.That (bsA.GetHashCode () == bsB.GetHashCode ());
            bsA.Add (1030);
            bsB.Add (1030);
            Assert.That (bsA.GetHashCode () == bsB.GetHashCode ());
            bsA.Length = 2000;
            Assert.That (bsA.GetHashCode () == bsB.GetHashCode ());

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
