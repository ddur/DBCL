// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Diagnostics.Contracts;

namespace DD.Collections.ICodeSet {

    /// <summary>
    /// Description of CodeExtensions.
    /// </summary>
    public static class CodeExtended {

        #region Category

        [Pure]
        public static bool HasCharValue (this Code self) {
            Contract.Ensures (Contract.Result<bool> () == (self.Value.InRange (0, 0xFFFF)));
            return (self & 0xFFFF) == self;
        }

        [Pure]
        public static bool IsSurrogate (this Code self) {
            Contract.Ensures (Contract.Result<bool> () == (self.Value.InRange (0xD800, 0xDFFF)));
            return (self.HasCharValue () && char.IsSurrogate ((char)self));
        }

        [Pure]
        public static bool IsHighSurrogate (this Code self) {
            Contract.Ensures (Contract.Result<bool> () == (self.Value.InRange (0xD800, 0xDBFF)));
            return (self.HasCharValue () && char.IsHighSurrogate ((char)self));
        }

        [Pure]
        public static bool IsLowSurrogate (this Code self) {
            Contract.Ensures (Contract.Result<bool> () == (self.Value.InRange (0xDC00, 0xDFFF)));
            return (self.HasCharValue () && char.IsLowSurrogate ((char)self));
        }

        [Pure]
        public static bool IsPermanentlyUndefined (this Code self) {
            Contract.Ensures (
                Contract.Result<bool> () ==
                (self.Value.InRange (0xFDD0, 0xFDDF)) ||
                (self > 0xFF && ((self & 0xFF) == 0xFE ||
                    (self & 0xFF) == 0xFF)));
            return (self.Value.InRange (0xFDD0, 0xFDDF) ||
                (self > 0xFF &&
                    ((self & 0xFF) == 0xFE || (self & 0xFF) == 0xFF)));
        }

        [Pure]
        public static int UnicodePlane (this Code self) {
            return self.Value >> 16;
        }

        [Pure]
        public static string ToXmlEntity (this Code self) {
            Contract.Ensures (Contract.Result<string> ().IsNot (null));

            if ((self.Value & 0xFF) == self.Value) {
                switch (self.Value) { // XML predefined entities
                    case ((int)'>'):
                        return "&gt;";
                    case ((int)'<'):
                        return "&lt;";
                    case ((int)'&'):
                        return "&amp;";
                    case ((int)'\''):
                        return "&apos;";
                    case ((int)'"'):
                        return "&quot;";
                }
            }
            return "&#x" + self.Value.ToString ("X") + ";";
        }

        /// <summary>http://www.w3.org/TR/2008/REC-xml-20081126/#charsets</summary>
        [Pure]
        public static bool IsXml10Char (this Code self) {
            return (self.Value == 0x9 || self.Value == 0xA || self.Value == 0xD ||
                    self.Value.InRange (0x20, 0xD7FF) ||
                    self.Value.InRange (0xE000, 0xFFFD) ||
                    self.Value.InRange (0x10000, 0x10FFFF));
        }

        /// <summary>http://www.w3.org/TR/2008/REC-xml-20081126/#charsets</summary>
        [Pure]
        public static bool IsXml10Discouraged (this Code self) {
            return (self.Value.InRange (0x7F, 0x84) ||
                self.Value.InRange (0x86, 0x9F) ||
                self.Value.IsPermanentlyUndefined ());
        }

        /// <summary>http://www.w3.org/TR/2006/REC-xml11-20060816/#charsets</summary>
        [Pure]
        public static bool IsXml11Char (this Code self) {
            return ((self.Value.InRange (0x1, 0xD7FF) ||
                     self.Value.InRange (0xE000, 0xFFFD) ||
                     self.Value.InRange (0x10000, 0x10FFFF)));
        }

        /// <summary>http://www.w3.org/TR/2006/REC-xml11-20060816/#charsets</summary>
        [Pure]
        public static bool IsXml11Restricted (this Code self) {
            return (self.Value.InRange (0x1, 0x8) ||
                    self.Value.InRange (0xB, 0xC) ||
                    self.Value.InRange (0xE, 0x1F) ||
                    self.Value.InRange (0x7F, 0x84) ||
                    self.Value.InRange (0x86, 0x9F));
        }

        /// <summary>http://www.w3.org/TR/2006/REC-xml11-20060816/#charsets</summary>
        [Pure]
        public static bool IsXml11Discouraged (this Code self) {
            return (self.Value.IsPermanentlyUndefined () || self.IsXml11Restricted ());
        }

        #endregion

    }
}
