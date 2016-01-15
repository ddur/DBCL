// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace DD.Collections.BitSetArrayTest.Interfaces.AsISet {

    [TestFixture]
    public class Operations {
        private Random r = new Random ();
        public static IEnumerable<TestCaseData> ISetTestCases = TestData.ISetTestCases;

        [Test]
        public void Add () {
            ISet<int> bs = BitSetArray.Empty ();
            Assert.That (delegate {
                bs.Add (int.MinValue);
            }, Throws.Nothing);
            Assert.That (delegate {
                bs.Add (-1);
            }, Throws.Nothing);

            int item;
            for (int i = 0; i < 100; i++) {
                item = r.Next (-100, 100);
                if (BitSetArray.ValidMember (item)) {
                    Assert.That (!bs.Contains (item) == bs.Add (item));
                    Assert.That (bs.Contains (item));
                }
                else {
                    Assert.That (bs.Add (item) == false);
                }
            }
        }

        [Test, TestCaseSource ("ISetTestCases")]
        public void ExceptWith (BitSetArray thisSet, IEnumerable<int> thatSet) {
            HashSet<int> hashResult = new HashSet<int> (thisSet);
            hashResult.ExceptWith (new HashSet<int> (thatSet));
            thisSet.ExceptWith (thatSet);
            Assert.That (hashResult.SetEquals (thisSet));
            thisSet.ExceptWith ((IEnumerable<int>)thisSet);
            Assert.That (thisSet.IsEmpty ());
        }

        [Test]
        public void ExceptWithNull () {
            BitSetArray testSet = BitSetArray.From (1, 2, 3);
            Assert.That (delegate {
                testSet.ExceptWith ((int[])null);
            }, Throws.Nothing);
            Assert.That (testSet.SequenceEqual (new int[] { 1, 2, 3 }));
        }

        [Test, TestCaseSource ("ISetTestCases")]
        public void IntersectWith (BitSetArray thisSet, IEnumerable<int> thatSet) {
            HashSet<int> hashResult = new HashSet<int> (thisSet);
            hashResult.IntersectWith (new HashSet<int> (thatSet));
            thisSet.IntersectWith (thatSet);
            Assert.That (hashResult.SetEquals (thisSet));
            thisSet.IntersectWith ((IEnumerable<int>)thisSet);
            Assert.That (hashResult.SetEquals (thisSet));
        }

        [Test]
        public void IntersectWithNull () {
            BitSetArray testSet = BitSetArray.From (1, 2, 3);
            Assert.That (delegate {
                testSet.IntersectWith ((int[])null);
            }, Throws.Nothing);
            Assert.That (testSet.Count == 0);
        }

        [Test, TestCaseSource ("ISetTestCases")]
        public void SymmetricExceptWith (BitSetArray thisSet, IEnumerable<int> thatSet) {
            HashSet<int> hashResult = new HashSet<int> (thisSet);
            hashResult.SymmetricExceptWith (new HashSet<int> (thatSet.Where (item => BitSetArray.ValidMember (item))));
            thisSet.SymmetricExceptWith (thatSet);
            Assert.That (hashResult.SetEquals (thisSet));
            thisSet.SymmetricExceptWith ((IEnumerable<int>)thisSet);
            Assert.That (thisSet.IsEmpty ());
        }

        [Test]
        public void SymmetricExceptWithNull () {
            BitSetArray testSet = BitSetArray.From (1, 2, 3);
            Assert.That (delegate {
                testSet.SymmetricExceptWith ((int[])null);
            }, Throws.Nothing);
            Assert.That (testSet.SequenceEqual (new int[] { 1, 2, 3 }));
        }

        [Test, TestCaseSource ("ISetTestCases")]
        public void UnionWith (BitSetArray thisSet, IEnumerable<int> thatSet) {
            HashSet<int> hashResult = new HashSet<int> (thisSet);
            hashResult.UnionWith (new HashSet<int> (thatSet.Where (item => BitSetArray.ValidMember (item))));
            thisSet.UnionWith (thatSet);
            Assert.That (hashResult.SetEquals (thisSet));
            BitSetArray saveSet = BitSetArray.Copy (thisSet);
            thisSet.UnionWith ((IEnumerable<int>)thisSet);
            Assert.That (thisSet.SetEquals (saveSet));
        }

        [Test]
        public void UnionWithNull () {
            BitSetArray testSet = BitSetArray.From (1, 2, 3);
            Assert.That (delegate {
                testSet.UnionWith ((int[])null);
            }, Throws.Nothing);
            Assert.That (testSet.SequenceEqual (new int[] { 1, 2, 3 }));
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

    [TestFixture]
    public class Relations {
        public static IEnumerable<TestCaseData> ISetTestCases = TestData.ISetTestCases;

        [Test, TestCaseSource ("ISetTestCases")]
        public void SetEquals (BitSetArray thisSet, IEnumerable<int> thatSet) {
            HashSet<int> hashThis = new HashSet<int> (thisSet);
            HashSet<int> hashThat = new HashSet<int> (thatSet);
            Assert.That (hashThis.SetEquals (hashThat) == thisSet.SetEquals (thatSet));
            Assert.That (thisSet.SetEquals ((IEnumerable<int>)thisSet));
        }

        [Test]
        public void SetEqualsNull () {
            BitSetArray testSet = BitSetArray.From (1, 2, 3);
            Assert.That (!testSet.SetEquals ((int[])null));
            testSet = BitSetArray.Empty ();
            Assert.That (testSet.SetEquals ((int[])null));
        }

        [Test, TestCaseSource ("ISetTestCases")]
        public void Overlaps (BitSetArray thisSet, IEnumerable<int> thatSet) {
            HashSet<int> hashThis = new HashSet<int> (thisSet);
            HashSet<int> hashThat = new HashSet<int> (thatSet);
            Assert.That (hashThis.Overlaps (hashThat) == thisSet.Overlaps (thatSet));
            if (thisSet.Count != 0) {
                Assert.That (thisSet.Overlaps ((IEnumerable<int>)thisSet));
            }
            else {
                Assert.That (!thisSet.Overlaps ((IEnumerable<int>)thisSet));
            }
        }

        [Test]
        public void OverlapsNull () {
            BitSetArray testSet = BitSetArray.From (1, 2, 3);
            Assert.That (!testSet.Overlaps ((int[])null));
            testSet = BitSetArray.Empty ();
            Assert.That (!testSet.Overlaps ((int[])null));
        }

        [Test, TestCaseSource ("ISetTestCases")]
        public void IsSupersetOf (BitSetArray thisSet, IEnumerable<int> thatSet) {
            HashSet<int> hashThis = new HashSet<int> (thisSet);
            HashSet<int> hashThat = new HashSet<int> (thatSet);
            if (thatSet.Count () != 0) {
                Assert.That (hashThis.IsSupersetOf (hashThat) == thisSet.IsSupersetOf (thatSet));
            }
            else {
                Assert.That (hashThis.IsSupersetOf (hashThat) != thisSet.IsSupersetOf (thatSet));
            }
            if (thisSet.Count != 0) {
                Assert.That (thisSet.IsSupersetOf ((IEnumerable<int>)thisSet));
            }
            else {
                Assert.That (!thisSet.IsSupersetOf ((IEnumerable<int>)thisSet));
            }
        }

        [Test]
        public void IsSupersetOfNull () {
            BitSetArray testSet = BitSetArray.From (1, 2, 3);
            Assert.That (!testSet.IsSupersetOf ((int[])null));
            testSet = BitSetArray.Empty ();
            Assert.That (!testSet.IsSupersetOf ((int[])null));
        }

        [Test, TestCaseSource ("ISetTestCases")]
        public void IsProperSupersetOf (BitSetArray thisSet, IEnumerable<int> thatSet) {
            HashSet<int> hashThis = new HashSet<int> (thisSet);
            HashSet<int> hashThat = new HashSet<int> (thatSet);
            if (thisSet.Count != 0 && thatSet.Count () == 0) {
                Assert.That (hashThis.IsProperSupersetOf (hashThat) != thisSet.IsProperSupersetOf (thatSet));
            }
            else {
                Assert.That (hashThis.IsProperSupersetOf (hashThat) == thisSet.IsProperSupersetOf (thatSet));
            }
            Assert.That (!thisSet.IsProperSupersetOf ((IEnumerable<int>)thisSet));
        }

        [Test]
        public void IsProperSupersetOfNull () {
            BitSetArray testSet = BitSetArray.From (1, 2, 3);
            Assert.That (!testSet.IsProperSupersetOf ((int[])null));
            testSet = BitSetArray.Empty ();
            Assert.That (!testSet.IsProperSupersetOf ((int[])null));
        }

        [Test, TestCaseSource ("ISetTestCases")]
        public void IsSubsetOf (BitSetArray thisSet, IEnumerable<int> thatSet) {
            HashSet<int> hashThis = new HashSet<int> (thisSet);
            HashSet<int> hashThat = new HashSet<int> (thatSet);
            if (thisSet.Count == 0) {
                Assert.That (hashThis.IsSubsetOf (hashThat) != thisSet.IsSubsetOf (thatSet));
            }
            else {
                Assert.That (hashThis.IsSubsetOf (hashThat) == thisSet.IsSubsetOf (thatSet));
            }
            if (thisSet.Count != 0) {
                Assert.That (thisSet.IsSubsetOf ((IEnumerable<int>)thisSet));
            }
            else {
                Assert.That (!thisSet.IsSubsetOf ((IEnumerable<int>)thisSet));
            }
        }

        [Test]
        public void IsSubsetOfNull () {
            BitSetArray testSet = BitSetArray.From (1, 2, 3);
            Assert.That (!testSet.IsSubsetOf ((int[])null));
            testSet = BitSetArray.Empty ();
            Assert.That (!testSet.IsSubsetOf ((int[])null));
        }

        [Test, TestCaseSource ("ISetTestCases")]
        public void IsProperSubsetOf (BitSetArray thisSet, IEnumerable<int> thatSet) {
            HashSet<int> hashThis = new HashSet<int> (thisSet);
            HashSet<int> hashThat = new HashSet<int> (thatSet);
            if (thisSet.Count == 0 && thatSet.Count () != 0) {
                Assert.That (hashThis.IsProperSubsetOf (hashThat) != thisSet.IsProperSubsetOf (thatSet));
            }
            else {
                Assert.That (hashThis.IsProperSubsetOf (hashThat) == thisSet.IsProperSubsetOf (thatSet));
            }
            Assert.That (!thisSet.IsProperSubsetOf ((IEnumerable<int>)thisSet));
        }

        [Test]
        public void IsProperSubsetOfNull () {
            BitSetArray testSet = BitSetArray.From (1, 2, 3);
            Assert.That (!testSet.IsProperSubsetOf ((int[])null));
            testSet = BitSetArray.Empty ();
            Assert.That (!testSet.IsProperSubsetOf ((int[])null));
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

    public static class TestData {

        public static IEnumerable<TestCaseData> ISetTestCases {
            get {
                // empty set
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { }),
                                        new int[] { }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { }),
                                        new int[] { int.MinValue, -1, int.MaxValue }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { }),
                                        new int[] { 3, 4, 5, 6, 7, 8, 9, 65 }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { }),
                                        new int[] { int.MinValue, -1, 3, 4, 5, 6, 7, 8, 9, 65, int.MaxValue }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6, 65 }),
                                        new int[] { }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6, 65 }),
                                        new int[] { int.MinValue, -1, int.MaxValue }
                );

                // partial intersection (not subset)
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 }),
                                        new int[] { 3, 4, 5, 6, 7, 8, 9, 65 }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 }),
                                        new int[] { 3, 4, 5, 6, 7, 8, 9, 65, int.MaxValue }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 3, 4, 5, 6, 7, 8, 9, 65 }),
                                        new int[] { 0, 1, 2, 3, 4, 5, 6 }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 3, 4, 5, 6, 7, 8, 9, 65 }),
                                        new int[] { -1, 0, 1, 2, 3, 4, 5, 6 }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 }),
                                        new int[] { 3, 7, 8, 9, 65 }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 }),
                                        new int[] { 3, 7, 8, 9, 65, int.MaxValue }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 3, 7, 8, 9, 65 }),
                                        new int[] { 0, 1, 2, 3, 4, 5, 6 }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 3, 7, 8, 9, 65 }),
                                        new int[] { int.MinValue, 0, 1, 2, 3, 4, 5, 6 }
                );

                // partial intersection (subset)
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 }),
                                        new int[] { 3, 4, 5, 6 }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 }),
                                        new int[] { 3, 4, 5, 6, int.MaxValue }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 3, 4, 5, 6, 7, 8, 9, 65 }),
                                        new int[] { 3, 4, 5, 6 }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 3, 4, 5, 6, 7, 8, 9, 65 }),
                                        new int[] { int.MinValue, -1, 3, 4, 5, 6 }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 }),
                                        new int[] { 3, 5 }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 }),
                                        new int[] { int.MinValue, -1, 3, 5, int.MaxValue }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 3, 7, 8, 9, 65 }),
                                        new int[] { 3 }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 3, 7, 8, 9, 65 }),
                                        new int[] { int.MinValue, 3 }
                );

                // full intersection (equals)
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6, 66 }),
                                        new int[] { 0, 1, 2, 3, 4, 5, 6, 66 }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 100, 200, 300, 400, 500, 600 }),
                                        new int[] { 0, 100, 200, 300, 400, 500, 600 }
                );

                // no intersection
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 }),
                                        new int[] { 7, 8, 9 }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 }),
                                        new int[] { int.MinValue, -1, 7, 8, 9 }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 }),
                                        new int[] { 7, 8, 9, int.MaxValue }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 }),
                                        new int[] { int.MinValue, -1, 7, 8, 9, int.MaxValue }
                );

                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 7, 8, 9 }),
                                        new int[] { 0, 1, 2, 3, 4, 5, 6 }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 }),
                                        new int[] { 8, 9, 150 }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 8, 9, 150 }),
                                        new int[] { 0, 1, 2, 3, 4, 5, 6 }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 2, 4, 6 }),
                                        new int[] { 1, 3, 5, 7, 9, 200 }
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 1, 3, 5, 7, 9, 200 }),
                                        new int[] { 0, 2, 4, 6 }
                );
            }
        }
    }
}
