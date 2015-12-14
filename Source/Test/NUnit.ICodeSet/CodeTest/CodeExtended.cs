// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeTest {

    [TestFixture]
    public class CodeExtended {

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

        [Test, TestCaseSource ("ValidCode")]
        public void HasCharValue (int code) {
            Code C = code;
            Assert.True (C.HasCharValue () == ((code & 0xFFFF) == code));
        }

        [Test, TestCaseSource ("ValidCode")]
        public void IsHighLowSurrogate (int code) {
            Code C = code;
            if (C.HasCharValue ()) {
                Assert.True (C.IsSurrogate () == char.IsSurrogate ((char)C));
                Assert.True (C.IsLowSurrogate () == char.IsLowSurrogate ((char)C));
                Assert.True (C.IsHighSurrogate () == char.IsHighSurrogate ((char)C));
            }
            else {
                Assert.False (C.IsSurrogate ());
                Assert.False (C.IsLowSurrogate ());
                Assert.False (C.IsHighSurrogate ());
            }
        }

        [Test, TestCaseSource ("ValidCode")]
        public void IsPermanentlyUndefined (int code) {
            Code C = code;
            if (code.InRange (0xFDD0, 0xFDDF)) {
                Assert.True (C.IsPermanentlyUndefined ());
            }
            else if (code.InRange (0x0, 0xFF)) {
                Assert.False (C.IsPermanentlyUndefined ());
            }
            else if ((code & 0xFF) == 0xFF || (code & 0xFF) == 0xFE) {
                Assert.True (C.IsPermanentlyUndefined ());
            }
            else {
                Assert.False (C.IsPermanentlyUndefined ());
            }
        }

        [Test, TestCaseSource ("ValidCode")]
        public void UnicodePlane (int code) {
            Code C = code;
            Assert.True (C.UnicodePlane () == (code >> 16));
        }

        [Test, TestCaseSource ("ValidCode")]
        public void ToXmlEntity (int code) {
            Code C = code;
            switch (C) {
                case '>':
                    Assert.True (C.ToXmlEntity () == "&gt;");
                    break;

                case '<':
                    Assert.True (C.ToXmlEntity () == "&lt;");
                    break;

                case '&':
                    Assert.True (C.ToXmlEntity () == "&amp;");
                    break;

                case '\'':
                    Assert.True (C.ToXmlEntity () == "&apos;");
                    break;

                case '"':
                    Assert.True (C.ToXmlEntity () == "&quot;");
                    break;

                default:
                    Assert.True (C.ToXmlEntity () == "&#x" + C.Value.ToString ("X") + ";");
                    break;
            }
        }

        [Test, TestCaseSource ("ValidCode")]
        public void IsXml10Char (int code) {
            Code C = code;
            if (C.IsSurrogate () || C == 0xFFFE || C == 0xFFFF) {
                Assert.False (C.IsXml10Char ());
            }
            else if (C < 0x20 && C != 0x9 && C != 0xA && C != 0xD) {
                Assert.False (C.IsXml10Char ());
            }
            else {
                Assert.True (C.IsXml10Char ());
            }
        }

        [Test, TestCaseSource ("ValidCode")]
        public void IsXml10Discouraged (int code) {
            Code C = code;
            if (C.IsPermanentlyUndefined ()) {
                Assert.True (C.IsXml10Discouraged ());
            }
            // is in range [127:159] (control character) except 0x85-NEL (control character)
            else if (code.InRange (0x7F, 0x9F) && C != 0x85) {
                Assert.True (C.IsXml10Discouraged ());
            }
            else {
                Assert.False (C.IsXml10Discouraged ());
            }
        }

        [Test, TestCaseSource ("ValidCode")]
        public void IsXml11Char (int code) {
            Code C = code;
            if (C.IsSurrogate () || C == 0xFFFE || C == 0xFFFF) {
                Assert.False (C.IsXml11Char ());
            }
            else if (C == 0) {
                Assert.False (C.IsXml11Char ());
            }
            else {
                Assert.True (C.IsXml11Char ());
            }
        }

        [Test, TestCaseSource ("ValidCode")]
        public void IsXml11Restricted (int code) {
            Code C = code;
            if (C.HasCharValue () && char.IsControl ((char)C) && C != 0x0 && C != 0x9 && C != 0xA && C != 0xD && C != 0x85) {
                Assert.True (C.IsXml11Restricted ());
            }
            else {
                Assert.False (C.IsXml11Restricted ());
            }
        }

        [Test, TestCaseSource ("ValidCode")]
        public void IsXml11Discouraged (int code) {
            Code C = code;
            if (C.IsPermanentlyUndefined () || C.IsXml11Restricted ()) {
                Assert.True (C.IsXml11Discouraged ());
            }
            else {
                Assert.False (C.IsXml11Discouraged ());
            }
        }
    }
}