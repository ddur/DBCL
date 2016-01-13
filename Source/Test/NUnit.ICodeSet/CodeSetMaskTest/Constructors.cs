/*
 * Created by SharpDevelop.
 * User: ddur
 * Date: 1.1.2016.
 * Time: 15:53
 *
 */

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
                        CodeSetMask.From (arg);
                    }, Throws.TypeOf<ArgumentNullException> ()
                );
            }

            [Test]
            public void Empty () {
                var arg = new int[0];
                Assert.That (
                    delegate {
                        var x = CodeSetMask.From (arg);
                    }, Throws.TypeOf<ArgumentEmptyException> ()
                );
            }

            [Test]
            public void InvalidNoBits () {
                var arg = new int[] { 0 };
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg);
                    }, Throws.TypeOf<ArgumentException> ()
                );
            }

            [Test]
            public void InvalidFirstBit () {
                var arg = new int[] { 2 };
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg);
                    }, Throws.TypeOf<ArgumentException> ()
                );
            }

            [Test]
            public void InvalidLastBitMask () {
                var arg = new int[] { 1, 1, 0 };
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg);
                    }, Throws.TypeOf<ArgumentException> ()
                );
            }

            [Test]
            public void InvalidLastBit () {
                var arg = new int[(Code.MaxValue >> 5) + 2];
                arg[0] = 1;
                arg[arg.Length - 1] = 1; // 1114112
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg);
                    }, Throws.TypeOf<ArgumentException> ()
                );
            }

            [Test]
            public void InvalidOffset () {
                var arg = new int[] { 1 };
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
            }

            [Test]
            public void InvalidBitsPlusOffset () {
                var arg = new int[(Code.MaxValue >> 5) + 1];
                arg[0] = 1;
                arg[arg.Length - 1] = -1;
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, 1); // 1114112
                    }, Throws.TypeOf<ArgumentException> ()
                );

                arg = new int[] { 3 };
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg, Code.MaxValue);
                    }, Throws.TypeOf<ArgumentException> ()
                );
            }

            [Test]
            public void Valid () {
                var arg = new int[(Code.MaxValue >> 5) + 1];
                arg[0] = 1;
                arg[arg.Length - 1] = -1;
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg);
                    }, Throws.Nothing
                );

                arg = new int[] { 1 };
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg);
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
                var arg = CodeSetMask.From (new int[] { 1 });
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
                Assert.That (
                    delegate {
                        CodeSetMask.From (arg);
                    }, Throws.Nothing
                );
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

    }
}
