// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using DD.Collections;
using DD.Text;

using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeTest
{

    [TestFixture]
    public class EncodeToString
    {
        
        static IEnumerable<TestCaseData> EncodeToStringOrThrow {
            get {
                return DataSource.CodeToStringOrThrow;
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

        [Test, TestCaseSource("EncodeToStringOrThrow")]
        public string Encode (int code) {
            return code.Encode();
        }

	    [Test, TestCaseSource("InvalidCode")]
        public void EncodeInvalidCode(int code)
        {
            Assert.Throws<ArgumentOutOfRangeException>(delegate {code.Encode();});
        }

	    [Test, TestCaseSource("ValidCode")]
        public void EncodeValidCode(int code) 
        {
            Code C = code;
			if (C.HasCharValue()) {
				if (C.IsSurrogate()) {
                    Assert.That(code.Encode() == "" + (char)0xFFFD);
                }
                else {
                    Assert.That(code.Encode() == "" + (char)code);
                }
            }
            else {
                Assert.That(code.Encode().Length == 2);
                Assert.That((code.Encode()).Decode().First() == code);
            }
        }
        
        [Test]
        public void EncodeAllValidCodes() {
            List<Code> codeList = null;
            Assert.DoesNotThrow ( delegate {codeList.Encode();});
            codeList = new List<Code>();
            Assert.DoesNotThrow ( delegate {codeList.Encode();});
			foreach (Code code in ValidCode) { if(!code.IsSurrogate()) codeList.Add(code); }
            string result = codeList.Encode();
            Assert.True (codeList.SequenceEqual(result.Decode()));
            codeList.Clear();
			foreach (Code code in ValidCode) { codeList.Add(code.IsSurrogate()?(Code)0xFFFD:code); }
            result = codeList.Encode();
            Assert.True (codeList.SequenceEqual(result.Decode()));
            codeList.Clear();
            foreach (Code code in ValidCode) { codeList.Add(code); }
            result = codeList.Encode();
            Assert.True (!codeList.SequenceEqual(result.Decode()));
        }

    }

}
