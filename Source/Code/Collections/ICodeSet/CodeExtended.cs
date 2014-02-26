// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Diagnostics.Contracts;

namespace DD.Collections
{
	/// <summary>
	/// Description of CodeExtensions.
	/// </summary>
	public static class CodeExtended
	{

		[Pure]
		public static string ToXmlEntity (this Code self) {
		    if ((self.Value&0xFF) == self.Value) {
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
			return "&#x" + self.Value.ToString("X") + ";";
		}

        /// <summary>http://www.w3.org/TR/2008/REC-xml-20081126/#charsets</summary>
        [Pure]
		public static bool IsXml10Char (this Code self) {
            return (self.Value == 0x9 || self.Value == 0xA || self.Value == 0xD ||   
               		self.Value.InRange(0x20, 0xD7FF) ||
                    self.Value.InRange(0xE000, 0xFFFD) || 
                    self.Value.InRange(0x10000, 0x10FFFF));
        }
        
        /// <summary>http://www.w3.org/TR/2008/REC-xml-20081126/#charsets</summary>
        [Pure]
        public static bool IsXml10Discouraged (this Code self) {
            return ( self.Value.InRange(0x7F, 0x84) ||
                     self.Value.InRange(0x86, 0x9F) ||
					 self.IsPermanentlyUndefined
                    );
        }
        
        /// <summary>http://www.w3.org/TR/2006/REC-xml11-20060816/#charsets</summary>
        [Pure]
        public static bool IsXml11Char (this Code self) {
            return ((self.Value.InRange(0x1, 0xD7FF) ||
                     self.Value.InRange(0xE000, 0xFFFD) || 
                     self.Value.InRange(0x10000, 0x10FFFF)));
        }
        
        /// <summary>http://www.w3.org/TR/2006/REC-xml11-20060816/#charsets</summary>
        [Pure]
        public static bool IsXml11Restricted (this Code self) {
            return (self.Value.InRange(0x1, 0x8) || 
                    self.Value.InRange(0xB, 0xC) || 
                    self.Value.InRange(0xE, 0x1F) || 
                    self.Value.InRange(0x7F, 0x84) || 
                    self.Value.InRange(0x86, 0x9F));
        }

        /// <summary>http://www.w3.org/TR/2006/REC-xml11-20060816/#charsets</summary>
        [Pure]
        public static bool IsXml11Discouraged (this Code self) {
			return (self.IsPermanentlyUndefined || self.IsXml11Restricted());
        }

	}
}
