// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using DD.Collections;
using NUnit.Framework;

namespace DD.Collections.CodeTest
{
	[TestFixture]
	public class CodeStruct
	{
		static IEnumerable<int> ValidByte {
			get {
				return DataSource.ValidByteValue;
			}
		}
		static IEnumerable<int> ValidChar {
			get {
				return DataSource.ValidCharValue;
			}
		}
		static IEnumerable<int> ValidCode {
			get {
				return DataSource.ValidCodeValue;
			}
		}
		static IEnumerable<int> InvalidCode {
			get {
				return DataSource.InvalidCodeValue;
			}
		}
		
		Random r = new Random();

		[Test]
		public void Constants()
		{
			Assert.True (Code.MinValue == DataSource.MinCodeValue);
			Assert.True (Code.MaxValue == DataSource.MaxCodeValue);
			Assert.True (Code.MinCount == DataSource.MinCodeCount);
			Assert.True (Code.MaxCount == DataSource.MaxCodeCount);
		}

		[Test]
		public void CodeSeq() {
			List<Code> cs = new List<Code>();
			cs = new List<Code> (new Code[] {100,200});
		}
		[Test, TestCaseSource("ValidByte")]
		public void ConstructCastFromByte(int code) {
			byte b = (byte)code;
			Assert.DoesNotThrow ( delegate { new Code(b); });
			Assert.DoesNotThrow ( delegate { Code C = b; });
		}
		
		[Test, TestCaseSource("ValidChar")]
		public void ConstructCastFromChar(int code) {
			char c = (char)code;
			Assert.DoesNotThrow ( delegate { new Code(c); });
			Assert.DoesNotThrow ( delegate { Code C = c; });
		}

		[Test, TestCaseSource("ValidCode")]
		public void ConstructCastFromValidInt(int code) {
			Assert.DoesNotThrow ( delegate { new Code(code); });
			Assert.DoesNotThrow ( delegate { Code C = code; });
		}

		[Test, TestCaseSource("InvalidCode")]
		public void ConstructCastFromInvalidInt(int code) {
			Assert.Throws<ArgumentOutOfRangeException> ( delegate { new Code(code); });
			Assert.Throws<InvalidCastException> ( delegate { Code C = code; });
		}

		[Test, TestCaseSource("ValidCode")]
		public void CastToCharArray(int code) {
			Code C = code;
			char[] chrs = C;

			Assert.NotNull (chrs);
			
			if (code.InRange(char.MinValue, char.MaxValue)) {

				Assert.True (chrs.Length == 1);
				if (C.IsSurrogate) {
					Assert.False (char.IsSurrogate(chrs[0]));
					Assert.True (chrs[0] == 0xFFFD);
				}
				else {
					Assert.True (chrs[0] == C);
				}
			} else {
				Assert.True (chrs.Length == 2);
				Assert.True (char.IsHighSurrogate(chrs[0]));
				Assert.True (char.IsLowSurrogate(chrs[1]));
				Assert.True ((""+chrs[0]+chrs[1]).Decode().First() == C);
			}
		}

		[Test, TestCaseSource("ValidCode")]
		public void CastToInt(int code) {
			Code C = code;
			int cast = C;
			Assert.True (C.Value == code);
			Assert.True (cast == C.Value);
			Assert.True (cast == code);
		}  

		[Test]
		public void IsEqual() {
			Code a = 2;
			Code b = 2;
			Code c = 3;
			object x = new object();

			Assert.True (a == b);
			Assert.False (a == c);
			
			Assert.True (a.Equals(b));
			Assert.False (a.Equals(c));
			
			Assert.True (a.Equals(b as object));
			Assert.False (a.Equals(c as object));

			Assert.False (a.Equals(x));
			
		}

		[Test, TestCaseSource("ValidCode")]
		public void IsHighLowSurrogate (int code) {
			Code C = code;
			if (C.HasCharValue) {
				Assert.True (C.IsSurrogate == char.IsSurrogate((char)C));
				Assert.True (C.IsLowSurrogate == char.IsLowSurrogate((char)C));
				Assert.True (C.IsHighSurrogate == char.IsHighSurrogate((char)C));
			}
			else {
				Assert.False (C.IsSurrogate);
				Assert.False (C.IsLowSurrogate);
				Assert.False (C.IsHighSurrogate);
			}
		}

		[Test, TestCaseSource("ValidCode")]
		public void IsPremanentlyUndefined (int code) {
			Code C = code;
			if (code.InRange (0xFDD0, 0xFDDF)) {
				Assert.True (C.IsPermanentlyUndefined);
			}
			else if ((code&0xFFFF) == 0xFFFF | (code&0xFFFF) == 0xFFFE) {
				Assert.True (C.IsPermanentlyUndefined);
			}
			else {
				Assert.False (C.IsPermanentlyUndefined);
			}
		}

		[Test, TestCaseSource("ValidCode")]
		public void UnicodePlane (int code) {
			Code C = code;
			Assert.True (C.UnicodePlane == (code >> 16));
		}

		[Test, TestCaseSource("ValidCode")]
		public void ToStringOverride(int code) {
			Code C = code;
			if ((code&0xFF) == code) {
				if (char.IsControl((char)code)) {
					Assert.True (C.ToString() == "\\x" + code.ToString("X"));
				}
				else {
					Assert.True (C.ToString() == "" + (char)code);
				}
			}
			else {
				Assert.True (C.ToString() == "\\x" + C.Value.ToString("X"));
			}
		}
		
		[Test, TestCaseSource("ValidCode")]
		public void ToXmlEntity(int code) {
			Code C = code;
			switch (C) {
				case '>':
					Assert.True (C.ToXmlEntity() == "&gt;"); break;
				case '<':
					Assert.True (C.ToXmlEntity() == "&lt;"); break;
				case '&':
					Assert.True (C.ToXmlEntity() == "&amp;"); break;
				case '\'':
					Assert.True (C.ToXmlEntity() == "&apos;"); break;
				case '"':
					Assert.True (C.ToXmlEntity() == "&quot;"); break;
				default:
					Assert.True (C.ToXmlEntity() == "&#x" + C.Value.ToString("X") + ";"); break;
			}
		}

		[Test, TestCaseSource("ValidCode")]
		public void IsXml10Char (int code) {
			Code C = code;
			if (C.IsSurrogate||C==0xFFFE||C==0xFFFF) {
				Assert.False (C.IsXml10Char());
			}
			else if (C<0x20 && C!=0x9 && C!=0xA && C!=0xD ) {
				Assert.False (C.IsXml10Char());
			}
			else {
				Assert.True (C.IsXml10Char());
			}
		}
	
	
		[Test, TestCaseSource("ValidCode")]
		public void IsXml10Discouraged (int code) {
			Code C = code;
			if (C.IsPermanentlyUndefined) {
				Assert.True (C.IsXml10Discouraged());
			}
   			// is in range [127:159] (control character) except 0x85-NEL (control character)
			else if (code.InRange(0x7F,0x9F) && C!=0x85) {
				Assert.True (C.IsXml10Discouraged());
			}
			else {
				Assert.False (C.IsXml10Discouraged());
			}
		}
	
		[Test, TestCaseSource("ValidCode")]
		public void IsXml11Char (int code) {
			Code C = code;
			if (C.IsSurrogate||C==0xFFFE||C==0xFFFF) {
				Assert.False (C.IsXml11Char());
			}
			else if (C==0) {
				Assert.False (C.IsXml11Char());
			}
			else {
				Assert.True (C.IsXml11Char());
			}
		}
	
		[Test, TestCaseSource("ValidCode")]
		public void IsXml11Restricted (int code) {
			Code C = code;
			if (C.HasCharValue && char.IsControl((char)C) && C!=0x0 && C!=0x9 && C!=0xA && C!=0xD && C!=0x85) {
				Assert.True (C.IsXml11Restricted());
			}
			else {
				Assert.False (C.IsXml11Restricted());
			}
		}

		[Test, TestCaseSource("ValidCode")]
		public void IsXml11Discouraged (int code) {
			Code C = code;
			if (C.IsPermanentlyUndefined || C.IsXml11Restricted()) {
				Assert.True (C.IsXml11Discouraged());
			}
			else {
				Assert.False (C.IsXml11Discouraged());
			}
		}

	}
}
