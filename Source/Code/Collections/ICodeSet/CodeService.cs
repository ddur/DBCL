// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using DD.Diagnostics;

namespace DD.Collections
{
	/// <summary>
	/// Code Service
	/// </summary>
	public static class CodeService {
	
		[Pure]
		public static bool HasCharValue (this int self)
		{
			Contract.Ensures ( Contract.Result<bool>() == (self.InRange (0, 0xFFFF)));
	
			return (self & 0xFFFF) == self;
		}
	
		[Pure]
		public static bool HasCodeValue (this int self)
		{
            Contract.Ensures (Contract.Result<bool> () == (self.InRange (Code.MinValue, Code.MaxValue)));
            // self.InRange (0, 1114111)
			return ((self & 0xFFFFF) == self || (self & 0x10FFFF) == self);
		}
	
		[Pure]
		public static int CastToCodeValue (this int self) {
		    Contract.Ensures (Contract.Result<int>().HasCodeValue());
		    if (self.HasCodeValue()) {
		        return self;
		    } else if ((self & 0x100000) != 0) {
	                return (self & 0x10FFFF);
		    } else {
	                return (self & 0xFFFFF);
		    }
		}
		
		[Pure]
		public static bool IsCodeCount (this int self)
		{
            Contract.Ensures (Contract.Result<bool> () == (self.InRange (Code.MinCount, Code.MaxCount)));
	
	        if (self.HasCodeValue() || self == Code.MaxCount) { return true; }
			return false;
		}
	
	}
	
}
