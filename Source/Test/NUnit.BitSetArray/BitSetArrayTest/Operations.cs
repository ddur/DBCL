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
    public class Operations {

        public static IEnumerable<TestCaseData> OperationTestCases {
            get {
                // empty set
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { }),
                    BitSetArray.From (new int[] { })
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { }),
                    BitSetArray.Size (150)
                );
                yield return new TestCaseData (
                    BitSetArray.Size (100),
                    BitSetArray.From (new int[] { })
                );
                yield return new TestCaseData (
                    BitSetArray.Size (100),
                    BitSetArray.Size (150)
                );

                yield return new TestCaseData (
                    BitSetArray.From (new int[] { }),
                    BitSetArray.From (new int[] { 3, 4, 5, 6, 7, 8, 9, 65 })
                );
                yield return new TestCaseData (
                    BitSetArray.Size (200),
                    BitSetArray.From (new int[] { 3, 4, 5, 6, 7, 8, 9, 65 })
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6, 65 }),
                    BitSetArray.From (new int[] { })
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6, 65 }),
                    BitSetArray.Size (150)
                );

                // partial intersection
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 2, 3, 4, 5 }),
                    BitSetArray.From (new int[] { 0, 1, 2, 3 })
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3 }),
                    BitSetArray.From (new int[] { 2, 3, 4, 5 })
                );

                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 }),
                    BitSetArray.From (new int[] { 3, 4, 5, 6, 7, 8, 9, 65 })
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 3, 4, 5, 6, 7, 8, 9, 65 }),
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 })
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 }),
                    BitSetArray.From (new int[] { 3, 7, 8, 9, 65 })
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 3, 7, 8, 9, 65 }),
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 })
                );

                // full intersection (equals)
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6, 66 }),
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6, 66 })
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 100, 200, 300, 400, 500, 600 }),
                    BitSetArray.From (new int[] { 0, 100, 200, 300, 400, 500, 600 })
                );

                // no intersection
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 }),
                    BitSetArray.From (new int[] { 7, 8, 9 })
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 7, 8, 9 }),
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 })
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 }),
                    BitSetArray.From (new int[] { 8, 9, 150 })
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 8, 9, 150 }),
                    BitSetArray.From (new int[] { 0, 1, 2, 3, 4, 5, 6 })
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 2, 4, 6 }),
                    BitSetArray.From (new int[] { 1, 3, 5, 7, 9, 200 })
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 1, 3, 5, 7, 9, 200 }),
                    BitSetArray.From (new int[] { 0, 2, 4, 6 })
                );

                // covers branch bits_result == array[index]
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 2, 4, 6, 8, 201 }),
                    BitSetArray.From (new int[] { 1, 3, 5, 7, 9, 200 })
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 1, 3, 5, 7, 9, 200 }),
                    BitSetArray.From (new int[] { 0, 2, 4, 6, 8, 201 })
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 0, 2, 4, 6, 8, 120, 201 }),
                    BitSetArray.From (new int[] { 1, 3, 5, 7, 9, 200 })
                );
                yield return new TestCaseData (
                    BitSetArray.From (new int[] { 1, 3, 5, 7, 9, 200 }),
                    BitSetArray.From (new int[] { 0, 2, 4, 6, 8, 120, 201 })
                );
            }
        }

        [Test, TestCaseSource ("OperationTestCases")]
        public void And (BitSetArray thisSet, BitSetArray thatSet) {
            HashSet<int> hashResult = new HashSet<int> (thisSet);
            hashResult.IntersectWith (new HashSet<int> (thatSet));

            thisSet.And (thatSet);
            Assert.That (hashResult.SetEquals (thisSet));
            thisSet.And (thisSet);
            Assert.That (hashResult.SetEquals (thisSet));
        }

        [Test]
        public void AndNull () {
            BitSetArray testSet = BitSetArray.From (1, 2, 3);
            Assert.That (delegate {
                testSet.And ((BitSetArray)null);
            }, Throws.Nothing);
            Assert.That (testSet.Count == 0);
            Assert.That (delegate {
                testSet.And ((BitSetArray)null);
            }, Throws.Nothing);
            Assert.That (testSet.Count == 0);

            testSet = BitSetArray.Empty ();
            Assert.That (delegate {
                testSet.And ((BitSetArray)null);
            }, Throws.Nothing);
            Assert.That (testSet.Count == 0);
            Assert.That (delegate {
                testSet.And ((BitSetArray)null);
            }, Throws.Nothing);
            Assert.That (testSet.Count == 0);
        }

        [Test, TestCaseSource ("OperationTestCases")]
        public void Or (BitSetArray thisSet, BitSetArray thatSet) {
            HashSet<int> hashResult = new HashSet<int> (thisSet);
            hashResult.UnionWith (new HashSet<int> (thatSet));

            thisSet.Or (thatSet);
            Assert.That (hashResult.SetEquals (thisSet));
            thisSet.Or (thisSet);
            Assert.That (hashResult.SetEquals (thisSet));
        }

        [Test]
        public void OrNull () {
            BitSetArray testSet = BitSetArray.From (1, 2, 3);
            Assert.That (delegate {
                testSet.Or ((BitSetArray)null);
            }, Throws.Nothing);
            Assert.That (testSet.Count == 3);
            testSet = BitSetArray.Size (10);
            Assert.That (delegate {
                testSet.Or ((BitSetArray)null);
            }, Throws.Nothing);
            Assert.That (testSet.Count == 0);
        }

        [Test, TestCaseSource ("OperationTestCases")]
        public void Xor (BitSetArray thisSet, BitSetArray thatSet) {
            HashSet<int> hashResult = new HashSet<int> (thisSet);
            hashResult.SymmetricExceptWith (new HashSet<int> (thatSet));

            thisSet.Xor (thatSet);
            Assert.That (hashResult.SetEquals (thisSet));
            thisSet.Xor (thisSet);
            Assert.That (thisSet.IsEmpty ());
        }

        [Test]
        public void XorNull () {
            BitSetArray testSet = BitSetArray.From (1, 2, 3);
            Assert.That (delegate {
                testSet.Xor ((BitSetArray)null);
            }, Throws.Nothing);
            Assert.That (testSet.Count == 3);
            testSet = BitSetArray.Size (20);
            Assert.That (delegate {
                testSet.Xor ((BitSetArray)null);
            }, Throws.Nothing);
            Assert.That (testSet.Count == 0);
        }

        [Test, TestCaseSource ("OperationTestCases")]
        public void NotThat (BitSetArray thisSet, BitSetArray thatSet) {
            HashSet<int> hashResult = new HashSet<int> (thisSet);
            hashResult.ExceptWith (new HashSet<int> (thatSet));

            thisSet.Not (thatSet);
            Assert.That (hashResult.SetEquals (thisSet));
            thisSet.Not (thisSet);
            Assert.That (thisSet.IsEmpty ());
        }

        [Test]
        public void NotNull () {
            BitSetArray testSet = BitSetArray.From (1, 2, 3);
            Assert.That (delegate {
                testSet.Not ((BitSetArray)null);
            }, Throws.Nothing);
            Assert.That (testSet.Count == 3);
            testSet = BitSetArray.Size (5);
            Assert.That (delegate {
                testSet.Not ((BitSetArray)null);
            }, Throws.Nothing);
            Assert.That (testSet.Count == 0);
        }

        [Test, TestCaseSource ("OperationTestCases")]
        public void NotThis (BitSetArray thisSet, BitSetArray thatSet) {
            HashSet<int> hashResult;

            hashResult = new HashSet<int> ();
            for (int i = 0; i < thisSet.Length; i++) {
                if (!thisSet.Contains (i)) {
                    hashResult.Add (i);
                }
            }
            thisSet.Not ();
            Assert.That (hashResult.SetEquals (thisSet));

            hashResult = new HashSet<int> ();
            for (int i = 0; i < thatSet.Length; i++) {
                if (!thatSet.Contains (i)) {
                    hashResult.Add (i);
                }
            }
            thatSet.Not ();
            Assert.That (hashResult.SetEquals (thatSet));
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
