// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace DD.Collections
{
	/// <summary>
	/// Description of CodeExtends.
	/// </summary>
	public static class CodeExtends
	{

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
