// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using DD.Collections;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetBitsTest {

    [TestFixture]
    public class Constructors {

        [Test]
        public void Empty () {
            var csb = CodeSetBits.From ();
        }

        [Test]
        public void FromParamsCodes () {
            CodeSetBits csb;

            csb = CodeSetBits.From (1);
            csb = CodeSetBits.From (1, 12, 33, 20);
        }

        [Test]
        public void FromParamsCodesThrows () {
            CodeSetBits csb;

            // (Code)int cast fails
            Assert.Throws<InvalidCastException> (
                delegate {
                    csb = CodeSetBits.From (-1);
                }
            );
            // (Code)int cast fails
            Assert.Throws<InvalidCastException> (
                delegate {
                    csb = CodeSetBits.From (1, 12, 33, -20);
                }
            );
        }

        [Test]
        public void FromCodes () {
            CodeSetBits csb;

            csb = CodeSetBits.From (new List<Code> ());
            csb = CodeSetBits.From (new List<Code> () { 1 });
            csb = CodeSetBits.From (new List<Code> () { 1, 12, 33, 20 });
        }

        [Test]
        public void FromCodesThrows () {
            CodeSetBits csb;

            // Requires no null
            Assert.Throws<ArgumentNullException> (
                delegate {
                    csb = CodeSetBits.From ((IEnumerable<Code>)null);
                }
            );
        }

        [Test]
        public void FromICodeSet () {
            CodeSetBits csb;

            ICodeSet input;
            input = CodeSetNone.Singleton;
            csb = CodeSetBits.From (input);

            input = CodeSetList.From (new List<Code> () { 1, 12, 33, 20 });
            csb = CodeSetBits.From (input);
            var clone = CodeSetBits.From (csb);
        }

        [Test]
        public void FromICodeSetThrows () {
            CodeSetBits csb;

            // Requires no null
            Assert.Throws<ArgumentNullException> (delegate { csb = CodeSetBits.From ((ICodeSet)null); });
        }

        [Test]
        public void FromBitSetArray () {
            CodeSetBits csb;

            csb = CodeSetBits.From (BitSetArray.Empty ());
            csb = CodeSetBits.From (BitSetArray.From (33));
            csb = CodeSetBits.From (BitSetArray.From (1, 12, 33));
            csb = CodeSetBits.From (BitSetArray.From (1, 12, 33));
        }

        [Test]
        public void FromBitSetArrayThrows () {
            CodeSetBits csb;

            // Requires no null
            Assert.Throws<ArgumentNullException> (
                delegate {
                    csb = CodeSetBits.From ((BitSetArray)null);
                });

            // Requires valid members
            Assert.Throws<IndexOutOfRangeException> (
                delegate {
                    csb = CodeSetBits.From (BitSetArray.From (0, 1, 12, Code.MaxCount));
                });

            Assert.Throws<IndexOutOfRangeException> (
                delegate {
                    csb = CodeSetBits.From (BitSetArray.From (0, 1, 12, int.MaxValue - 1));
                });
        }

        [Test]
        public void FromBitSetArrayAtOffset () {
            CodeSetBits csb;

            csb = CodeSetBits.From (BitSetArray.Empty (), Code.MinValue);
            csb = CodeSetBits.From (BitSetArray.Empty (), Code.MaxValue);
            csb = CodeSetBits.From (BitSetArray.Empty (), Code.MaxCount);

            csb = CodeSetBits.From (BitSetArray.From (0), Code.MaxValue);
            csb = CodeSetBits.From (BitSetArray.From (0), Code.MaxCount - 1);

            csb = CodeSetBits.From (BitSetArray.From (0, 1), Code.MinCount);
            csb = CodeSetBits.From (BitSetArray.From (0, 1), Code.MaxValue - 1);
            csb = CodeSetBits.From (BitSetArray.From (0, 1), Code.MaxCount - 2);

            csb = CodeSetBits.From (BitSetArray.From (0, 1, 12, 33), Code.MaxCount / 2);

            csb = CodeSetBits.From (BitSetArray.From (0, 1, 12, 33, Code.MaxValue), 0);
        }

        [Test]
        public void FromBitSetArrayAtOffsetThrows () {
            CodeSetBits csb;

            // Requires no null
            Assert.Throws<ArgumentNullException> (delegate { csb = CodeSetBits.From ((BitSetArray)null, 0); });

            // Requires valid members&offset
            Assert.Throws<IndexOutOfRangeException> (delegate { csb = CodeSetBits.From (BitSetArray.From (0, 1), -1); });
            Assert.Throws<IndexOutOfRangeException> (delegate { csb = CodeSetBits.From (BitSetArray.From (0, 1, 12, 33), -1); });
            Assert.Throws<IndexOutOfRangeException> (delegate { csb = CodeSetBits.From (BitSetArray.From (0, 1, 12, Code.MaxCount), 0); });
            Assert.Throws<IndexOutOfRangeException> (delegate { csb = CodeSetBits.From (BitSetArray.From (0, 1, 12, Code.MaxValue), 1); });
        }
    }
}
