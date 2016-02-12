// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;
using DD.Collections;

namespace DD.Collections.ICodeSet.CodeSetMaskTest {

    [TestFixture]
    public static class Construction {

        #region From ParamsCode

        public class FromParamsCode {

            [Test]
            public void NullArray () {
                Assert.That (
                    delegate {
                        CodeSetMask.From ((Code[])null);
                    }, Throws.TypeOf<ArgumentNullException> ()
                );
            }

            [Test]
            public void EmptyArray () {
                Assert.That (
                    delegate {
                        var x = CodeSetMask.From (new Code[0]);
                    }, Throws.TypeOf<ArgumentEmptyException> ()
                );
            }

            [Test]
            public void ValidArray () {
                Assert.That (
                    delegate {
                        CodeSetMask.From (new Code[] { 0, 1, 2, 3 });
                    }, Throws.Nothing
                );
            }

            [Test]
            public void ValidParams () {
                Assert.That (
                    delegate {
                        CodeSetMask.From (0, 1, 2, 3, 50);
                    }, Throws.Nothing
                );
            }
        }

        #endregion

        #region From IEumerable<Code>

        public class FromIEnumerableOfCode {

            [Test]
            public void Null () {
                Assert.That (
                    delegate {
                        CodeSetMask.From ((IEnumerable<Code>)null);
                    }, Throws.TypeOf<ArgumentNullException> ()
                );
            }

            [Test]
            public void Empty () {
                Assert.That (
                    delegate {
                        var x = CodeSetMask.From ((IEnumerable<Code>)new Code[0]);
                    }, Throws.TypeOf<ArgumentEmptyException> ()
                );
            }

            [Test]
            public void Valid () {
                Assert.That (
                    delegate {
                        CodeSetMask.From ((IEnumerable<Code>)new Code[] { 0, 1, 3, 3 });
                    }, Throws.Nothing
                );
            }

            [Test]
            public void CastFromICodeSet () {
                Assert.That (
                    delegate {
                        CodeSetMask.From ((IEnumerable<Code>)CodeSetList.From (0, 3, 254, 255, 256));
                    }, Throws.Nothing
                );
            }
        }

        #endregion

        #region From int[]

        public class FromArrayOfInt {

            [Test]
            public void Null () {
                int[] arg = null;
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 1, 10, 5);
                    }, Throws.TypeOf<ArgumentNullException> ().And.Message.EqualTo ("Precondition failed: array.IsNot(null)")
                );
            }

            [Test]
            public void Empty () {
                var arg = new int[0];
                Assert.That (
                    delegate {
                        var x = CodeSetMask.From (arg, 1, 10, 5);
                    }, Throws.TypeOf<ArgumentEmptyException> ().And.Message.EqualTo ("Precondition failed: array.Length != 0")
                );
            }

            [Test]
            public void InvalidBitsToLargeArray () {
                var arg = new int[(Code.MaxValue >> 5) + 2];
                arg[0] = 1;
                arg[arg.Length - 1] = 1; // 1114112
                Assert.That (
                    delegate {
                        var x = CodeSetMask.From (arg, 0, Code.MaxValue, 2);
                    }, Throws.TypeOf<ArgumentException> ().And.Message.EqualTo ("Precondition failed: ((Code.MaxValue >> 5) + 1) >= array.Length")
                );
            }

            [Test]
            public void InvalidBitsNoBits () {
                var arg = new int[] { 0 };
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 1, 10, 5);
                    }, Throws.TypeOf<ArgumentException> ().And.Message.EqualTo ("Precondition failed: (array[0] & 1) != 0")
                );
            }

            [Test]
            public void InvalidBitsFirstBit () {
                var arg = new int[] { 2 };
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 1, 10, 5);
                    }, Throws.TypeOf<ArgumentException> ().And.Message.EqualTo ("Precondition failed: (array[0] & 1) != 0")
                );
            }

            [Test]
            public void InvalidBitsLastBitMask () {
                var arg = new int[] { 1, 1, 0 };
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 0, 10, 2);
                    }, Throws.TypeOf<ArgumentException> ().And.Message.EqualTo ("Precondition failed: array[array.Length - 1] != 0")
                );
            }

            [Test]
            public void InvalidStartValue () {
                var arg = new int[] { 1 };
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 0, 0, 1);
                    }, Throws.Nothing
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, Code.MinValue - 1, 0, 1);
                    }, Throws.TypeOf<ArgumentException> ().And.Message.EqualTo ("Precondition failed: start.HasCodeValue()")
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, Code.MaxValue + 1, 0, 1);
                    }, Throws.TypeOf<ArgumentException> ().And.Message.EqualTo ("Precondition failed: start.HasCodeValue()")
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, int.MinValue, 0, 1);
                    }, Throws.TypeOf<ArgumentException> ().And.Message.EqualTo ("Precondition failed: start.HasCodeValue()")
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, int.MaxValue, 0, 1);
                    }, Throws.TypeOf<ArgumentException> ().And.Message.EqualTo ("Precondition failed: start.HasCodeValue()")
                );
            }

            [Test]
            public void InvalidFinalValue () {
                var arg = new int[] { 1 };
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 0, 0, 1);
                    }, Throws.Nothing
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 0, Code.MinValue - 1, 1);
                    }, Throws.TypeOf<ArgumentException> ().And.Message.EqualTo ("Precondition failed: final.HasCodeValue()")
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 0, Code.MaxValue + 1, 1);
                    }, Throws.TypeOf<ArgumentException> ().And.Message.EqualTo ("Precondition failed: final.HasCodeValue()")
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 0, int.MinValue, 1);
                    }, Throws.TypeOf<ArgumentException> ().And.Message.EqualTo ("Precondition failed: final.HasCodeValue()")
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 0, int.MaxValue, 1);
                    }, Throws.TypeOf<ArgumentException> ().And.Message.EqualTo ("Precondition failed: final.HasCodeValue()")
                );
            }

            [Test]
            public void InvalidStartValueGreaterThanFinal () {
                var arg = new int[(Code.MaxValue >> 5) + 1];
                arg[0] = 1;
                arg[arg.Length - 1] = -1;
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 1114111, 1114110, 33);
                    }, Throws.TypeOf<ArgumentException> ().And.Message.EqualTo ("Precondition failed: start <= final")
                );
            }

            [Test]
            public void InvalidCountValue () {
                var arg = new int[] { 1 };
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 0, 0, 0);
                    }, Throws.TypeOf<ArgumentException> ().And.Message.EqualTo ("Precondition failed: count > 0")
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 0, 0, 2);
                    }, Throws.TypeOf<ArgumentException> ().And.Message.EqualTo ("Precondition failed: count <= (final - start + 1)")
                );
            }

            [Test]
            public void InvalidBitsLastBitItemPlusStart () {
                var arg = new int[(Code.MaxValue >> 5) + 1];
                arg[0] = 1;
                arg[arg.Length - 1] = -1;
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 0, Code.MaxValue, 33);
                    }, Throws.Nothing
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 1, Code.MaxValue, 33); // 1114112
                    }, Throws.TypeOf<ArgumentException> ().And.Message.EqualTo ("Precondition failed: final == array.IsCompactLast () + start")
                );

                arg = new int[] { 9 };
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, Code.MaxValue - 1, Code.MaxValue, 2);
                    }, Throws.TypeOf<ArgumentException> ().And.Message.EqualTo ("Precondition failed: final == array.IsCompactLast () + start")
                );
            }

            [Test]
            public void InvalidBitsLastBitItem () {
                var arg = new int[(Code.MaxValue >> 5) + 1];
                arg[0] = 1;
                arg[arg.Length - 1] = 1;
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 0, Code.MaxValue, 2);
                    }, Throws.TypeOf<ArgumentException> ().And.Message.EqualTo ("Precondition failed: final == array.IsCompactLast () + start")
                );
            }

            [Test]
            public void InvalidBitsCount () {
                var arg = new int[(Code.MaxValue >> 5) + 1];
                arg[0] = 1;
                arg[arg.Length - 1] = -1;
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 0, 1114111, 33);
                    }, Throws.Nothing
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 0, 1114111, 34);
                    }, Throws.TypeOf<ArgumentException> ().And.Message.EqualTo ("Precondition failed: count == BitSetArray.CountOnBits(array)")
                );
            }

            [Test]
            public void Valid () {
                var arg = new int[(Code.MaxValue >> 5) + 1];
                arg[0] = 1;
                arg[arg.Length - 1] = -1;
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 0, 1114111, 33);
                    }, Throws.Nothing
                );

                arg = new int[] { 1 };
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 0, 0, 1);
                    }, Throws.Nothing
                );
            }
        }

        #endregion

        #region From CodeSetMask

        public class FromCodeSetMask {

            [Test]
            public void Null () {
                CodeSetMask arg = null;
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg);
                    }, Throws.TypeOf<ArgumentNullException> ()
                );
            }

            [Test]
            public void InvalidOffsetExtreme () {
                var arg = CodeSetMask.From (1);
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, int.MinValue);
                    }, Throws.TypeOf<ArgumentException> ()
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, Code.MinValue - 1);
                    }, Throws.TypeOf<ArgumentException> ()
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, Code.MaxValue + 1);
                    }, Throws.TypeOf<ArgumentException> ()
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, int.MaxValue);
                    }, Throws.TypeOf<ArgumentException> ()
                );
            }

            [Test]
            public void InvalidOffsetMinimal () {
                var arg = CodeSetMask.From (CodeSetPair.From (1, 1114111));
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, -1);
                    }, Throws.TypeOf<ArgumentException> ()
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 1);
                    }, Throws.TypeOf<ArgumentException> ()
                );
            }

            [Test]
            public void Valid () {
                var arg = CodeSetMask.From (CodeSetPair.From (1, 1114111));
                CodeSetMask clone = null;
                Assert.That (
                    delegate {
                        clone = CodeSetMask.From (arg);
                    }, Throws.Nothing
                );
                Assert.True (clone.SequenceEqual(arg));
                Assert.True (clone.First == arg.First);
                Assert.True (clone.Last == arg.Last);
                Assert.True (clone.Count == arg.Count);
            }
        }

        #endregion

        #region From BitSetArray

        public class FromBitSetArray {

            [Test]
            public void InvalidItemsPlusMinusOffset () {
                var arg = BitSetArray.From(0);
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, Code.MinValue - 1);
                    }, Throws.TypeOf<ArgumentException> ()
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, Code.MaxValue + 1);
                    }, Throws.TypeOf<ArgumentException> ()
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, int.MinValue);
                    }, Throws.TypeOf<ArgumentException> ()
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, int.MaxValue);
                    }, Throws.TypeOf<ArgumentException> ()
                );

                arg = BitSetArray.From (0, Code.MaxValue);

                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 1); // 1114112
                    }, Throws.TypeOf<ArgumentException> ()
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, -1); // -1
                    }, Throws.TypeOf<ArgumentException> ()
                );
            }

            [Test]
            public void ValidItemsPlusMinusOffset () {
                var arg = BitSetArray.From(0);
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, Code.MinValue);
                    }, Throws.Nothing
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, Code.MaxValue);
                    }, Throws.Nothing
                );

                arg = BitSetArray.From (Code.MaxValue/2);

                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 100000);
                    }, Throws.Nothing
                );
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, -100000);
                    }, Throws.Nothing
                );
            }

        }
        #endregion

        #region From CompactBitMask

        public class FromCompactBitMask {

            // CompactBitMask is struct, cannot be null

            [Test]
            public void Valid () {
                var arg = CodeSetMask.From (CodeSetPair.From (1, 1114111));
                CodeSetMask clone = null;
                Assert.That (
                    delegate {
                        clone = CodeSetMask.From (CodeSetMask.From (arg).ToCompactBitMask());
                    }, Throws.Nothing
                );
                Assert.True (clone.SequenceEqual(arg));
                Assert.True (clone.First == arg.First);
                Assert.True (clone.Last == arg.Last);
                Assert.True (clone.Count == arg.Count);
            }
        }

        #endregion
    }
}
