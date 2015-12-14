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
    public class Relations {

        public static IEnumerable<BitSetArray> SetValueSource {
            get {
                yield return (BitSetArray)null;
                yield return BitSetArray.Empty ();
                yield return BitSetArray.From (1);
                yield return BitSetArray.From (2, 3, 4, 5, 6, 7, 8, 65);
                yield return BitSetArray.From (2, 3, 4, 5, 6, 7, 8, 1028);
                yield return BitSetArray.From (1, 2, 3, 4, 5, 0, 64, 65, 127, 128);
            }
        }

        [Test, Combinatorial]
        public void SetquenceEqual ([ValueSource ("SetValueSource")] BitSetArray thisSet, [ValueSource ("SetValueSource")] BitSetArray thatSet) {
            if (thisSet.IsNot (null)) {
                Assert.True (thisSet.SequenceEqual (thatSet) == thisSet.SetEquals (thatSet));
            }
            if (thatSet.IsNot (null)) {
                Assert.True (thatSet.SequenceEqual (thisSet) == thatSet.SetEquals (thisSet));
            }
        }

        [Test, Combinatorial]
        public void SetEquals ([ValueSource ("SetValueSource")] BitSetArray thisSet, [ValueSource ("SetValueSource")] BitSetArray thatSet) {
            if (thisSet.IsNot (null) && thatSet.IsNot (null)) {
                Assert.That (new HashSet<int> (thisSet).SetEquals (new HashSet<int> (thatSet)) == thisSet.SetEquals (thatSet));
            }
            if (!thisSet.IsNullOrEmpty () && thatSet.IsNullOrEmpty ()) {
                Assert.That (thisSet.SetEquals (thatSet) == false);
            }
            if (thisSet.IsNot (null) && thisSet.IsEmpty () && thatSet.IsNullOrEmpty ()) {
                Assert.That (thisSet.SetEquals (thatSet) == true);
            }
            if (thisSet.IsNot (null)) {
                Assert.That (thisSet.SetEquals (thisSet) == true);
            }
        }

        [Test, Combinatorial]
        public void Overlaps ([ValueSource ("SetValueSource")] BitSetArray thisSet, [ValueSource ("SetValueSource")] BitSetArray thatSet) {
            if (thisSet.IsNot (null) && thatSet.IsNot (null)) {
                Assert.That (new HashSet<int> (thisSet).Overlaps (new HashSet<int> (thatSet)) == thisSet.Overlaps (thatSet));
            }
            if (thisSet.IsNot (null) && thatSet.IsNullOrEmpty ()) {
                Assert.That (thisSet.Overlaps (thatSet) == false);
            }
            if (thisSet.IsNot (null)) {
                if (thisSet.IsEmpty ()) {
                    Assert.That (thisSet.Overlaps (thisSet) == false);
                }
                else {
                    Assert.That (thisSet.Overlaps (thisSet) == true);
                }
            }
        }

        [Test, Combinatorial]
        public void IsSupersetOf ([ValueSource ("SetValueSource")] BitSetArray thisSet, [ValueSource ("SetValueSource")] BitSetArray thatSet) {
            // in BitSetArray set system, empty set is never subset because it never overlaps
            if (thisSet.IsNot (null) && thatSet.IsNot (null)) {
                if (thatSet.IsEmpty ()) {
                    Assert.That (new HashSet<int> (thisSet).IsSupersetOf (new HashSet<int> (thatSet)) != thisSet.IsSupersetOf (thatSet));
                }
                else {
                    Assert.That (new HashSet<int> (thisSet).IsSupersetOf (new HashSet<int> (thatSet)) == thisSet.IsSupersetOf (thatSet));
                }
            }
            if (thisSet.IsNot (null) && thatSet.IsNullOrEmpty ()) {
                Assert.That (thisSet.IsSupersetOf (thatSet) == false);
            }
            if (thisSet.IsNot (null)) {
                if (thisSet.IsEmpty ()) {
                    Assert.That (thisSet.IsSupersetOf (thisSet) == false);
                }
                else {
                    Assert.That (thisSet.IsSupersetOf (thisSet) == true);
                }
            }
        }

        [Test, Combinatorial]
        public void IsSubsetOf ([ValueSource ("SetValueSource")] BitSetArray thisSet, [ValueSource ("SetValueSource")] BitSetArray thatSet) {
            // in BitSetArray set system, empty set is never subset because it never overlaps
            if (thisSet.IsNot (null) && thatSet.IsNot (null)) {
                if (thisSet.IsEmpty ()) {
                    Assert.That (new HashSet<int> (thisSet).IsSubsetOf (new HashSet<int> (thatSet)) != thisSet.IsSubsetOf (thatSet));
                }
                else {
                    Assert.That (new HashSet<int> (thisSet).IsSubsetOf (new HashSet<int> (thatSet)) == thisSet.IsSubsetOf (thatSet));
                }
            }
            if (thisSet.IsNot (null) && thatSet.IsNullOrEmpty ()) {
                Assert.That (thisSet.IsSubsetOf (thatSet) == false);
            }
            if (thisSet.IsNot (null)) {
                if (thisSet.IsEmpty ()) {
                    Assert.That (thisSet.IsSubsetOf (thisSet) == false);
                }
                else {
                    Assert.That (thisSet.IsSubsetOf (thisSet) == true);
                }
            }
        }

        [Test, Combinatorial]
        public void IsProperSupersetOf ([ValueSource ("SetValueSource")] BitSetArray thisSet, [ValueSource ("SetValueSource")] BitSetArray thatSet) {
            // in BitSetArray set system, empty set is never subset because it never overlaps
            if (thisSet.IsNot (null) && thatSet.IsNot (null)) {
                if (!thisSet.IsEmpty () && thatSet.IsEmpty ()) {
                    Assert.That (new HashSet<int> (thisSet).IsProperSupersetOf (new HashSet<int> (thatSet)) != thisSet.IsProperSupersetOf (thatSet));
                }
                else {
                    Assert.That (new HashSet<int> (thisSet).IsProperSupersetOf (new HashSet<int> (thatSet)) == thisSet.IsProperSupersetOf (thatSet));
                }
            }
            if (thisSet.IsNot (null) && thatSet.IsNullOrEmpty ()) {
                Assert.That (thisSet.IsProperSupersetOf (thatSet) == false);
            }
            if (thisSet.IsNot (null)) {
                Assert.That (thisSet.IsProperSupersetOf (thisSet) == false);
            }
        }

        [Test, Combinatorial]
        public void IsProperSubsetOf ([ValueSource ("SetValueSource")] BitSetArray thisSet, [ValueSource ("SetValueSource")] BitSetArray thatSet) {
            // in BitSetArray set system, empty set is never subset because it never overlaps
            if (thisSet.IsNot (null) && thatSet.IsNot (null)) {
                if (thisSet.IsEmpty () && !thatSet.IsEmpty ()) {
                    Assert.That (new HashSet<int> (thisSet).IsProperSubsetOf (new HashSet<int> (thatSet)) != thisSet.IsProperSubsetOf (thatSet));
                }
                else {
                    Assert.That (new HashSet<int> (thisSet).IsProperSubsetOf (new HashSet<int> (thatSet)) == thisSet.IsProperSubsetOf (thatSet));
                }
            }
            if (thisSet.IsNot (null) && thatSet.IsNullOrEmpty ()) {
                Assert.That (thisSet.IsProperSubsetOf (thatSet) == false);
            }
            if (thisSet.IsNot (null)) {
                Assert.That (thisSet.IsProperSubsetOf (thisSet) == false);
            }
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