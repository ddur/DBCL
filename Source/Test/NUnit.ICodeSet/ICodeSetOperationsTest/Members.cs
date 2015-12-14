// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetOperationsTest {

    [TestFixture]
    public class Members {

        [Test, TestCaseSource ("OperationTestCases")]
        public void BitComplement (BitSetArray a, BitSetArray b) {
            ICodeSet iCodeSetA = a.Reduce ();
            ICodeSet iCodeSetB = b.Reduce ();

            BitSetArray bitsA = iCodeSetA.BitComplement ();
            BitSetArray bitsB = iCodeSetB.BitComplement ();

            Assert.True (bitsA.Count == a.Span () - a.Count);
            Assert.True (bitsB.Count == b.Span () - b.Count);

            if (bitsA.Count != 0) {
                Assert.True (bitsA.First > a.First);
                Assert.True (bitsA.Last < a.Last);
                Assert.AreEqual (bitsA.Count + iCodeSetA.Count, iCodeSetA.Length);

                iCodeSetA = CodeSetWrap.From (bitsA);
                Assert.False (bitsA.SequenceEqual (iCodeSetA.BitComplement ()));

                bitsA.Or (a);
                Assert.True (bitsA.Count == bitsA.Span ());
                Assert.True (bitsA.Span () == a.Span ());
                Assert.True (bitsA.First == a.First);
                Assert.True (bitsA.Last == a.Last);
            }

            if (bitsB.Count != 0) {
                Assert.True (bitsB.First > b.First);
                Assert.True (bitsB.Last < b.Last);
                Assert.AreEqual (bitsB.Count + iCodeSetB.Count, iCodeSetB.Length);

                iCodeSetB = CodeSetWrap.From (bitsB);
                Assert.False (bitsB.SequenceEqual (iCodeSetB.BitComplement ()));

                bitsB.Or (b);
                Assert.True (bitsB.Count == bitsB.Span ());
                Assert.True (bitsB.Span () == b.Span ());
                Assert.True (bitsB.First == b.First);
                Assert.True (bitsB.Last == b.Last);
            }
        }

        [Test]
        public void BitComplement_of_null () {
            ICodeSet iCodeSetA = null;

            Assert.That (iCodeSetA.BitComplement ().Count == 0);
        }

        [Test, TestCaseSource ("OperationTestCases")]
        public void BitDifference (BitSetArray a, BitSetArray b) {
            ICodeSet iCodeSetA = a.Reduce ();
            ICodeSet iCodeSetB = b.Reduce ();

            Assert.That (iCodeSetA.BitDifference (iCodeSetB).SequenceEqual (a - b));
            Assert.That (iCodeSetA.BitDifference (iCodeSetB, iCodeSetB).SequenceEqual (a - b - b));

            Assert.That (iCodeSetA.BitDifference ((ICodeSet)null).SequenceEqual (a));
            Assert.That (((ICodeSet)null).BitDifference (iCodeSetA).IsEmpty ());

            Assert.That (iCodeSetA.BitDifference ((ICodeSet)null, (ICodeSet[])null).SequenceEqual (a));
            Assert.That (((ICodeSet)null).BitDifference (iCodeSetA, (ICodeSet[])null).IsEmpty ());

            Assert.That (iCodeSetB.BitDifference (iCodeSetA).SequenceEqual (b - a));
            Assert.That (iCodeSetB.BitDifference (iCodeSetA, iCodeSetA).SequenceEqual (b - a - a));

            Assert.That (iCodeSetB.BitDifference ((ICodeSet)null).SequenceEqual (b));
            Assert.That (((ICodeSet)null).BitDifference (iCodeSetB).IsEmpty ());
        }

        [Test]
        public void BitDifference_Throws () {
            var argument = OperationTestCases.First ();
            ICodeSet iCodeSetA = ((BitSetArray)(argument.Arguments[0])).Reduce ();
            ICodeSet iCodeSetB = ((BitSetArray)(argument.Arguments[1])).Reduce ();
            IEnumerable<ICodeSet> throws = null;

            Assert.Throws<ArgumentNullException> (
                delegate {
                    var x = throws.BitDifference ();
                }
            );
            Assert.Throws<ArgumentException> (
                delegate {
                    var x = new ICodeSet[0].BitDifference ();
                }
            );
            Assert.Throws<ArgumentException> (
                delegate {
                    var x = new[] {
						iCodeSetA
					}.BitDifference ();
                }
            );
            Assert.Throws<ArgumentException> (
                delegate {
                    var x = new[] {
						iCodeSetB
					}.BitDifference ();
                }
            );
            Assert.That (
                delegate {
                    var x = new ICodeSet[] {
						iCodeSetA,
						iCodeSetB
					}.BitDifference ();
                },
                Throws.Nothing
            );
            Assert.That (
                delegate {
                    var x = new ICodeSet[] {
						iCodeSetA,
						iCodeSetB,
						null
					}.BitDifference ();
                },
                Throws.Nothing
            );
        }

        [Test, TestCaseSource ("OperationTestCases")]
        public void BitDisjunction (BitSetArray a, BitSetArray b) {
            ICodeSet iCodeSetA = a.Reduce ();
            ICodeSet iCodeSetB = b.Reduce ();

            Assert.That (iCodeSetA.BitDisjunction (iCodeSetB).SequenceEqual (a ^ b));
            Assert.That (iCodeSetA.BitDisjunction (iCodeSetB, iCodeSetB).SequenceEqual (a ^ b ^ b));

            Assert.That (iCodeSetA.BitDisjunction ((ICodeSet)null).SequenceEqual (a));
            Assert.That (((ICodeSet)null).BitDisjunction (iCodeSetA).SequenceEqual (a));

            Assert.That (iCodeSetA.BitDisjunction ((ICodeSet)null, (ICodeSet[])null).SequenceEqual (a));
            Assert.That (((ICodeSet)null).BitDisjunction (iCodeSetA, (ICodeSet[])null).SequenceEqual (a));

            Assert.That (iCodeSetB.BitDisjunction (iCodeSetA).SequenceEqual (b ^ a));
            Assert.That (iCodeSetB.BitDisjunction (iCodeSetA, iCodeSetA).SequenceEqual (b ^ a ^ a));

            Assert.That (iCodeSetB.BitDisjunction ((ICodeSet)null).SequenceEqual (b));
            Assert.That (((ICodeSet)null).BitDisjunction (iCodeSetB).SequenceEqual (b));
        }

        [Test]
        public void BitDisjunction_Throws () {
            var argument = OperationTestCases.First ();
            ICodeSet iCodeSetA = ((BitSetArray)(argument.Arguments[0])).Reduce ();
            ICodeSet iCodeSetB = ((BitSetArray)(argument.Arguments[1])).Reduce ();

            Assert.Throws<ArgumentNullException> (
                delegate {
                    ((ICodeSet[])null).BitDisjunction ();
                }
            );
            Assert.Throws<ArgumentException> (
                delegate {
                    new ICodeSet[0].BitDisjunction ();
                }
            );
            Assert.Throws<ArgumentException> (
                delegate {
                    new[] {
						iCodeSetA
					}.BitDisjunction ();
                }
            );
            Assert.Throws<ArgumentException> (
                delegate {
                    new[] {
						iCodeSetB
					}.BitDisjunction ();
                }
            );
            Assert.That (
                delegate {
                    new ICodeSet[] {
						iCodeSetA,
						iCodeSetB
					}.BitDisjunction ();
                },
                Throws.Nothing
            );
            Assert.That (
                delegate {
                    new ICodeSet[] {
						iCodeSetA,
						iCodeSetB,
						null
					}.BitDisjunction ();
                },
                Throws.Nothing
            );
        }

        [Test, TestCaseSource ("OperationTestCases")]
        public void BitIntersection (BitSetArray a, BitSetArray b) {
            ICodeSet iCodeSetA = a.Reduce ();
            ICodeSet iCodeSetB = b.Reduce ();

            Assert.That (iCodeSetA.BitIntersection (iCodeSetB).SequenceEqual (a & b));
            Assert.That (iCodeSetA.BitIntersection (iCodeSetB, iCodeSetB).SequenceEqual (a & b & b));

            Assert.That (iCodeSetA.BitIntersection ((ICodeSet)null).IsEmpty ());
            Assert.That (((ICodeSet)null).BitIntersection (iCodeSetA).IsEmpty ());

            Assert.That (iCodeSetA.BitIntersection ((ICodeSet)null, (ICodeSet[])null).IsEmpty ());
            Assert.That (((ICodeSet)null).BitIntersection (iCodeSetA, (ICodeSet[])null).IsEmpty ());

            Assert.That (iCodeSetB.BitIntersection (iCodeSetA).SequenceEqual (b & a));
            Assert.That (iCodeSetB.BitIntersection (iCodeSetA, iCodeSetA).SequenceEqual (b & a & a));

            Assert.That (iCodeSetB.BitIntersection ((ICodeSet)null).IsEmpty ());
            Assert.That (((ICodeSet)null).BitIntersection (iCodeSetB).IsEmpty ());
        }

        [Test]
        public void BitIntersection_Throws () {
            var argument = OperationTestCases.First ();
            ICodeSet iCodeSetA = ((BitSetArray)(argument.Arguments[0])).Reduce ();
            ICodeSet iCodeSetB = ((BitSetArray)(argument.Arguments[1])).Reduce ();

            Assert.Throws<ArgumentNullException> (
                delegate {
                    ((ICodeSet[])null).BitIntersection ();
                }
            );
            Assert.Throws<ArgumentException> (
                delegate {
                    new ICodeSet[0].BitIntersection ();
                }
            );
            Assert.Throws<ArgumentException> (
                delegate {
                    new[] {
						iCodeSetA
					}.BitIntersection ();
                }
            );
            Assert.Throws<ArgumentException> (
                delegate {
                    new[] {
						iCodeSetB
					}.BitIntersection ();
                }
            );
            Assert.That (
                delegate {
                    new ICodeSet[] {
						iCodeSetA,
						iCodeSetB
					}.BitIntersection ();
                },
                Throws.Nothing
            );
            Assert.That (
                delegate {
                    new ICodeSet[] {
						iCodeSetA,
						iCodeSetB,
						null
					}.BitIntersection ();
                },
                Throws.Nothing
            );
        }

        [Test, TestCaseSource ("OperationTestCases")]
        public void BitUnion (BitSetArray a, BitSetArray b) {
            ICodeSet iCodeSetA = a.Reduce ();
            ICodeSet iCodeSetB = b.Reduce ();

            Assert.That (iCodeSetA.BitUnion (iCodeSetB).SequenceEqual (a | b));
            Assert.That (iCodeSetA.BitUnion (iCodeSetB, iCodeSetB).SequenceEqual (a | b | b));

            Assert.That (iCodeSetA.BitUnion ((ICodeSet)null).SequenceEqual (a));
            Assert.That (((ICodeSet)null).BitUnion (iCodeSetA).SequenceEqual (a));

            Assert.That (iCodeSetA.BitUnion ((ICodeSet)null, (ICodeSet[])null).SequenceEqual (a));
            Assert.That (((ICodeSet)null).BitUnion (iCodeSetA, (ICodeSet[])null).SequenceEqual (a));

            Assert.That (iCodeSetB.BitUnion (iCodeSetA).SequenceEqual (b | a));
            Assert.That (iCodeSetB.BitUnion (iCodeSetA, iCodeSetA).SequenceEqual (b | a | a));

            Assert.That (iCodeSetB.BitUnion ((ICodeSet)null).SequenceEqual (b));
            Assert.That (((ICodeSet)null).BitUnion (iCodeSetB).SequenceEqual (b));
        }

        [Test]
        public void BitUnion_Throws () {
            var argument = OperationTestCases.First ();
            ICodeSet iCodeSetA = ((BitSetArray)(argument.Arguments[0])).Reduce ();
            ICodeSet iCodeSetB = ((BitSetArray)(argument.Arguments[1])).Reduce ();

            Assert.Throws<ArgumentNullException> (
                delegate {
                    ((ICodeSet[])null).BitUnion ();
                }
            );
            Assert.Throws<ArgumentException> (
                delegate {
                    new ICodeSet[0].BitUnion ();
                }
            );
            Assert.Throws<ArgumentException> (
                delegate {
                    new[] {
						iCodeSetA
					}.BitUnion ();
                }
            );
            Assert.Throws<ArgumentException> (
                delegate {
                    new[] {
						iCodeSetB
					}.BitUnion ();
                }
            );
            Assert.That (
                delegate {
                    new ICodeSet[] {
						iCodeSetA,
						iCodeSetB
					}.BitUnion ();
                },
                Throws.Nothing
            );
            Assert.That (
                delegate {
                    new ICodeSet[] {
						iCodeSetA,
						iCodeSetB,
						null
					}.BitUnion ();
                },
                Throws.Nothing
            );
        }

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
            }
        }
    }
}
