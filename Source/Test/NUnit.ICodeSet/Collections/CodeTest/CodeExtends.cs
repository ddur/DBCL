// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using DD.Collections;
using NUnit.Framework;

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
	    
	    [Test, TestCaseSource("ValidChar")]
	    public void CharHasCodeValue(int intCodeValue)
	    {
	        Assert.True (intCodeValue.HasCodeValue());
	    }
	
	    [Test, TestCaseSource("ValidCode")]
	    public void ValidIntHasCodeValue(int code)
	    {
	        Assert.True(code.HasCodeValue());
	    }
	
	    [Test, TestCaseSource("InvalidCode")]
	    public void InvalidIntHasNotCodeValue(int code)
	    {
	        Assert.False(code.HasCodeValue());
	    }
	
	    [Test, TestCaseSource("ValidCode")]
	    public void ValidIntIsCodeCount(int code)
	    {
	        Assert.True(code.IsCodeCount());
	        Assert.True((code+1).IsCodeCount());
	        
	    }
	    
	    [Test, TestCaseSource("InvalidCode")]
	    public void InvalidIntIsNotCodeCount(int code)
	    {
	        if (code == Code.MaxValue + 1)
    	        Assert.True (code.IsCodeCount());
	        else 
	           Assert.False (code.IsCodeCount());
	        
	    }
	    
	    [Test, TestCaseSource("ValidCode")]
	    public void ValidIntCastToCodeValue(int i) {
	        Assert.True (i.CastToCodeValue().HasCodeValue());
	        Assert.True (i.CastToCodeValue() == i);
	    }

	    [Test, TestCaseSource("InvalidCode")]
	    public void InvalidIntCastToCodeValue(int i) {
	        Assert.True (i.CastToCodeValue().HasCodeValue());
	        Assert.True (i.CastToCodeValue() != i);
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
