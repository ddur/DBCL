﻿// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DD.Collections
{
	/// <summary>
	/// Description of CodeExtends.
	/// </summary>
	public static class CodeExtends
	{

		#region int

        [Pure]
		public static bool IsSurrogate (this int self) {
			return self.HasCodeValue() && ((Code)self).IsSurrogate();
        }

        [Pure]
        public static bool IsHighSurrogate (this int self) {
			return self.HasCodeValue() && ((Code)self).IsHighSurrogate();
        }

        [Pure]
        public static bool IsLowSurrogate (this int self) {
			return self.HasCodeValue() && ((Code)self).IsLowSurrogate();
        }

        [Pure]
        public static bool IsPermanentlyUndefined (this int self) {
			return !self.HasCodeValue() || ((Code)self).IsPermanentlyUndefined();
        }
        
		[Pure]
		public static bool HasCharValue (this int self) {
			return self.HasCodeValue() && ((Code)self).HasCharValue();
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
		public static bool IsCodesCount (this int self) {
            Contract.Ensures (Contract.Result<bool> () == (self.InRange (Code.MinCount, Code.MaxCount)));
	        if (self.HasCodeValue() || self == Code.MaxCount) { return true; }
			return false;
		}
	
		[Pure]
		public static string Encode (this int self)
		{
			Contract.Requires<ArgumentOutOfRangeException>(self.HasCodeValue());
	
			return ((Code)self).Encode();
		}
	
		#endregion

		#region IEnumerable<int>
	
		/// <summary>Returns True if collection{int} Contains All (IsSupersetOf) specified characters.
		/// <para>To treat characters as UTF16 encoded, use string argument.</para>
		/// </summary>
		/// <param name="chrs">IEnumerableOf(char)</param>
		/// <returns>bool</returns>
		public static bool ContainsAll (this ICollection<int> self, IEnumerable<char> chrs)
		{
			if (self == null || self.Count == 0) { return false; }
			if (chrs == null || chrs.IsEmpty()) { return false; }
	
			foreach (int item in chrs)
			{
			    if (!self.Contains(item))
				{
					return false;
				}
			}
			return true;
		}
	
		/// <summary>Returns True if collection{int} Contains All (IsSupersetOf) specified characters.
		/// <para>Characters are decoded from UTF16 encoding.</para>
		/// <para>To treat characters as items, cast string to IEnumerable&lt;char&gt;.</para>
		/// </summary>
		/// <param name="utf16">string</param>
		/// <returns>bool</returns>
		public static bool ContainsAll(this ICollection<int> self, string utf16)
		{
		    if (self.IsNull() || self.IsEmpty()) { return false; }
			if (string.IsNullOrEmpty(utf16)) { return false; }
	
			foreach (int item in utf16.Decode())
			{
			    if (!self.Contains(item))
				{
					return false;
				}
			}
			return true;
		}
	
		#endregion
	
	}
}