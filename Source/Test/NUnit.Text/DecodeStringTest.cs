// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using DD.Collections.ICodeSet;
using NUnit.Framework;

namespace DD.Text {

    [TestFixture]
    public class DecodeStringTest {

        private IEnumerable<TestCaseData> PrevNextToBool {
            get {
                return DataSource.TwoCharsToBool;
            }
        }

        private IEnumerable<TestCaseData> PrevNextToCodeOrThrow {
            get {
                return DataSource.TwoCharsToCodeOrThrow;
            }
        }

        private IEnumerable<TestCaseData> ValidString {
            get {
                return DataSource.ValidEncodedString;
            }
        }

        private IEnumerable<string> InvalidString {
            get {
                return DataSource.InvalidEncodedString;
            }
        }

        [Test, TestCaseSource ("ValidString")]
        public void DecodeValidString (string data, IEnumerable<Code> code) {
            Assert.True (data.CanDecode ());
            List<Code> decoded = null;
            Assert.True (data.TryDecode (out decoded));
            Assert.True (decoded.IsNot (null));
            Assert.True (decoded.SequenceEqual (code));
            Assert.That (delegate { decoded = data.Decode (); }, Throws.Nothing);
            Assert.True (decoded.IsNot (null));
            Assert.True (decoded.SequenceEqual (code));
            if (data.IsNot (null)) {
                Assert.True (decoded.Encode ().SequenceEqual (data));
            }
            else {
                Assert.True (decoded.Encode ().IsEmpty ());
            }
        }

        [Test, TestCaseSource ("InvalidString")]
        public void DecodeInvalidString (string data) {
            Assert.False (data.CanDecode ());
            List<Code> decoded = null;
            Assert.False (data.TryDecode (out decoded));
            Assert.True (decoded == null);
            if (data.IsNull ()) {
                Assert.Throws<ArgumentNullException> (delegate { data.Decode (); });
            }
            else {
                Assert.Throws<ArgumentException> (delegate { data.Decode (); });
            }
        }

        [Test, TestCaseSource ("PrevNextToBool")]
        public bool CanDecodeChar (char prev, char next) {
            return prev.CanDecode (next);
        }

        [Test, TestCaseSource ("PrevNextToCodeOrThrow")]
        public int DecodeChar (char prev, char next) {
            return prev.Decode (next);
        }
    }
}
