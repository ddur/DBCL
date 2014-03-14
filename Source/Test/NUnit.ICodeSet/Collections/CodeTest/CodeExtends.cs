// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using NUnit.Framework;

using DD.Collections;
using DD.Text;

namespace DD.Collections.CodeTest
{
	[TestFixture]
	public class CodeExtends
	{
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
	    
	    [Test, TestCaseSource("ValidChar"), TestCaseSource("ValidCode")]
	    public void IsSurrogate_ValidCode(int intCodeValue)
	    {
			Assert.True (intCodeValue.IsSurrogate() == ((Code)intCodeValue).IsSurrogate());
			Assert.True (intCodeValue.IsLowSurrogate() == ((Code)intCodeValue).IsLowSurrogate());
			Assert.True (intCodeValue.IsHighSurrogate() == ((Code)intCodeValue).IsHighSurrogate());
	    }
	    [Test, TestCaseSource("InvalidCode")]
	    public void IsSurrogate_InvalidCode(int intCodeValue)
	    {
			Assert.False (intCodeValue.IsSurrogate());
			Assert.False (intCodeValue.IsLowSurrogate());
			Assert.False (intCodeValue.IsHighSurrogate());
	    }

		[Test, TestCaseSource("ValidChar"), TestCaseSource("ValidCode")]
	    public void IsPermanentlyUndefined_ValidCode(int intCodeValue)
	    {
			Assert.True (intCodeValue.IsPermanentlyUndefined() == ((Code)intCodeValue).IsPermanentlyUndefined());
			if (intCodeValue <= 0xFF) 
				Assert.False (intCodeValue.IsPermanentlyUndefined());
			else {
				if (((intCodeValue&0xFF)==0xFE) || ((intCodeValue&0xFF)==0xFF))  
					Assert.True (intCodeValue.IsPermanentlyUndefined());
			}
			if (intCodeValue.InRange(0xFDD0, 0xFDDF)) 
				Assert.True (intCodeValue.IsPermanentlyUndefined());
	    }
	    [Test, TestCaseSource("InvalidCode")]
	    public void IsPermanentlyUndefined_InvalidCode(int intCodeValue)
	    {
			Assert.True (intCodeValue.IsPermanentlyUndefined());
	    }
	
	    [Test, TestCaseSource("ValidChar")]
	    public void HasCharValue_ValidChar(int code)
	    {
			Assert.True(code.HasCharValue());
	    }
	    [Test, TestCaseSource("ValidCode")]
	    public void HasCharValue_ValidCode(int code)
	    {
			Assert.IsTrue(code.HasCharValue() == ((Code)code).HasCharValue());
	    }
	    [Test, TestCaseSource("InvalidCode")]
	    public void HasCharValue_InvalidCode(int code)
	    {
	        Assert.False(code.HasCharValue());
	    }
	
	    [Test, TestCaseSource("ValidChar"), TestCaseSource("ValidCode")]
	    public void HasCodeValue_ValidCode(int code)
	    {
	        Assert.True(code.HasCodeValue());
	    }
	    [Test, TestCaseSource("InvalidCode")]
	    public void HasCodeValue_InvalidCode(int code)
	    {
	        Assert.False(code.HasCodeValue());
	    }
	
	    [Test, TestCaseSource("ValidChar"), TestCaseSource("ValidCode")]
	    public void CastToCodeValue_ValidCode(int i) {
	        Assert.True (i.CastToCodeValue().HasCodeValue());
	        Assert.True (i.CastToCodeValue() == i);
	    }

	    [Test, TestCaseSource("InvalidCode")]
	    public void CastToCodeValue_InvalidCode(int i) {
	        Assert.True (i.CastToCodeValue().HasCodeValue());
	        Assert.True (i.CastToCodeValue() != i);
	    }

	    [Test, TestCaseSource("ValidChar"), TestCaseSource("ValidCode")]
	    public void IsCodeCount_ValidCode(int code)
	    {
	        Assert.True(code.IsCodesCount());
	        Assert.True((code+1).IsCodesCount());
	        
	    }
	    [Test, TestCaseSource("InvalidCode")]
	    public void IsCodeCount_InvalidCode(int code)
	    {
	        if (code == Code.MaxValue + 1)
    	        Assert.True (code.IsCodesCount());
	        else 
	           Assert.False (code.IsCodesCount());
	        
	    }
	    
	    [Test, TestCaseSource("ValidChar"), TestCaseSource("ValidCode")]
	    public void Encode_ValidCode(int i) {
			Assert.True (i.Encode() == ((Code)i).Encode());
	    }

	    [Test, TestCaseSource("InvalidCode")]
	    public void Encode_InvalidCode(int i) {
			Assert.Throws<ArgumentOutOfRangeException>(delegate{i.Encode();});
	    }

	    [Test]
		public void IEnumerableIntContainsAll()
		{
			BitSetArray testRange = null;

			Assert.That ( testRange.ContainsAll ( null ), Is.False );
			Assert.That ( testRange.ContainsAll ( (IEnumerable<char>)null ), Is.False );
			Assert.That ( testRange.ContainsAll ( new List<char>() ), Is.False );
			Assert.That ( testRange.ContainsAll ( (String)null ), Is.False );
			Assert.That ( testRange.ContainsAll ( (string)null ), Is.False );
			Assert.That ( testRange.ContainsAll ( "" ), Is.False );

			// define test range
			testRange = BitSetArray.Size (0x60, true);

			// Test with null or empty argument
			Assert.That ( testRange.ContainsAll ( null ), Is.False );
			Assert.That ( testRange.ContainsAll ( (IEnumerable<char>)null ), Is.False );
			Assert.That ( testRange.ContainsAll ( new List<char>() ), Is.False );
			Assert.That ( testRange.ContainsAll ( (String)null ), Is.False );
			Assert.That ( testRange.ContainsAll ( (string)null ), Is.False );
			Assert.That ( testRange.ContainsAll ( "" ), Is.False );

			// Argument IEnumerable<char> as List<char>
			Assert.That ( testRange.ContainsAll ( new List<char> { 'A','B','C','D','E','F','G','H' } ), Is.True );
			Assert.That ( testRange.ContainsAll ( new List<char> { 'A','B','C','D','E','F','G','H','a' } ), Is.False );
			Assert.That ( testRange.ContainsAll ( new List<char> { 'a','b','c','d','e','f','g','h' } ), Is.False );

			// Argument IEnumerable<char> as HashSet<char>
			Assert.That ( testRange.ContainsAll ( new HashSet<char> { 'A','B','C','D','E','F','G','H' } ), Is.True );
			Assert.That ( testRange.ContainsAll ( new HashSet<char> { 'A','B','C','D','E','F','G','H','a' } ), Is.False );
			Assert.That ( testRange.ContainsAll ( new HashSet<char> { 'a','b','c','d','e','f','g','h' } ), Is.False );

			// Argument IEnumerable<char> as String
			Assert.That ( testRange.ContainsAll ( "ABCDEFGH" ), Is.True );
			Assert.That ( testRange.ContainsAll ( "ABCDEFGHa" ), Is.False );
			Assert.That ( testRange.ContainsAll ( "abcdefgh" ), Is.False );

		}
		
	}
}
