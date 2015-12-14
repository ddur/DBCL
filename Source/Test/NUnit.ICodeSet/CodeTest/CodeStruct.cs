// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using DD.Collections;

namespace DD.Collections.ICodeSet.CodeTest {

    [TestFixture]
    public class CodeStruct {

        private static IEnumerable<int> ValidByte {
            get {
                return DataSource.ValidByteValue;
            }
        }

        private static IEnumerable<int> ValidChar {
            get {
                return DataSource.ValidCharValue;
            }
        }

        private static IEnumerable<int> ValidCode {
            get {
                return DataSource.ValidCodeValue;
            }
        }

        private static IEnumerable<int> InvalidCode {
            get {
                return DataSource.InvalidCodeValue;
            }
        }

        private Random r = new Random ();

        [Test]
        public void Constants () {
            Assert.True (Code.MinValue == DataSource.MinCodeValue);
            Assert.True (Code.MaxValue == DataSource.MaxCodeValue);
            Assert.True (Code.MinCount == DataSource.MinCodeCount);
            Assert.True (Code.MaxCount == DataSource.MaxCodeCount);
        }

        [Test]
        public void CodeSeq () {
            List<Code> cs = new List<Code> ();
            cs = new List<Code> (new Code[] { 100, 200 });
        }

        [Test, TestCaseSource ("ValidByte")]
        public void ConstructCastFromByte (int code) {
            byte b = (byte)code;
            Assert.DoesNotThrow (delegate { new Code (b); });
            Assert.DoesNotThrow (delegate { Code C = b; });
        }

        [Test, TestCaseSource ("ValidChar")]
        public void ConstructCastFromChar (int code) {
            char c = (char)code;
            Assert.DoesNotThrow (delegate { new Code (c); });
            Assert.DoesNotThrow (delegate { Code C = c; });
        }

        [Test, TestCaseSource ("ValidCode")]
        public void ConstructCastFromValidInt (int code) {
            Assert.DoesNotThrow (delegate { new Code (code); });
            Assert.DoesNotThrow (delegate { Code C = code; });
        }

        [Test, TestCaseSource ("InvalidCode")]
        public void ConstructCastFromInvalidInt (int code) {
            Assert.Throws<InvalidCastException> (delegate { new Code (code); });
            Assert.Throws<InvalidCastException> (delegate { Code C = code; });
        }

        [Test, TestCaseSource ("ValidCode")]
        public void CastToInt (int code) {
            Code C = code;
            int cast = C;
            Assert.True (C.Value == code);
            Assert.True (cast == C.Value);
            Assert.True (cast == code);
        }

        [Test]
        public void IsEqual () {
            Code a = 2;
            Code b = 2;
            Code c = 3;
            object x = new object ();

            Assert.True (a == b);
            Assert.False (a == c);

            Assert.True (a.Equals (b));
            Assert.False (a.Equals (c));

            Assert.True (a.Equals (b as object));
            Assert.False (a.Equals (c as object));

            Assert.False (a.Equals (x));
        }

        [Test, TestCaseSource ("ValidCode")]
        public void ToStringOverride (int code) {
            Code C = code;
            if ((code & 0xFF) == code) {
                if (char.IsControl ((char)code)) {
                    Assert.True (C.ToString () == "\\x" + code.ToString ("X"));
                }
                else {
                    Assert.True (C.ToString () == "" + (char)code);
                }
            }
            else {
                Assert.True (C.ToString () == "\\x" + C.Value.ToString ("X"));
            }
        }
    }
}
