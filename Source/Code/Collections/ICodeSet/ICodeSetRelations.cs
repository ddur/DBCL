// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DD.Collections.ICodeSet
{
	/// <summary>
	/// ICodeSet Set-Relations.
	/// </summary>
	public static class ICodeSetRelations
	{
		#region ICodeSet Set Relations

		[Pure] public static bool Overlaps (this ICodeSet self, ICodeSet that)
		{
			switch (self.QuickSetEquals(that)) {
				case true: return !self.IsNullOrEmpty();
			}
			switch (self.QuickSetOverlaps(that)) {
				case false: return false;
				case true: return true;
			}

			// Get overlap range
			Contract.Assert (self.RangeOverlaps (that)); // From QuickSetOverlaps
			Code overlapFirst = self.First >= that.First ? self.First : that.First;
			Code overlapLast = self.Last <= that.Last ? self.Last : that.Last;
			Contract.Assert (overlapFirst <= overlapLast);

			ICodeSet smaller = self.Count <= that.Count ? self : that;
			ICodeSet larger = self.Count > that.Count ? self : that;

			foreach (var code in smaller) {
				if (code < overlapFirst) continue;
				if (code > overlapLast) break;
				if (larger[code]) return true;
			}
			return false;
		}
		
		[Pure] public static bool IsSubsetOf (this ICodeSet self, ICodeSet that)
		{
			return
				self.IsRangeSubsetOf(that) && // null/empty excluded
				self.Count <= that.Count &&
				self.All(item => that[item]);
		}
		
		[Pure] public static bool IsProperSubsetOf (this ICodeSet self, ICodeSet that)
		{
			return
				self.IsRangeSubsetOf(that) && // null/empty excluded
				self.Count < that.Count &&
				self.All(item => that[item]);
		}

		[Pure] public static bool IsSupersetOf (this ICodeSet self, ICodeSet that)
		{
			return that.IsSubsetOf(self);
		}
		
		[Pure] public static bool IsProperSupersetOf (this ICodeSet self, ICodeSet that)
		{
			return that.IsProperSubsetOf(self);
		}
		
		/// <summary>ICodeSet.CompareTo() method implementation/semantics
		/// <para>Compared as two BigIntegers (BitSetArray)</para>
		/// </summary>
		/// <param name="self">ICodeSet</param>
		/// <param name="that">ICodeSet</param>
		/// <returns>Same as IComparable.CompareTo</returns>
		/// <remarks>Do not rename this extension method to "CompareTo"!</remarks>
		[Pure] internal static int Compare (this ICodeSet self, ICodeSet that)
		{
			if (self.QuickSetEquals(that) == true) { return 0; }
			Contract.Assert (!(self.IsNullOrEmpty() && that.IsNullOrEmpty()));

			if (self.IsNullOrEmpty()) { return -1; }
			if (that.IsNullOrEmpty()) { return 1; }
			if (self.Last < that.Last) { return -1; }
			if (self.Last > that.Last) { return 1; }

			return self.ToBitSetArray().CompareTo(that.ToBitSetArray());
		}

		#endregion

		#region ICodeSet Set/Sequence Equality Relation

		/// <summary>ICodeSet.Equals() method implementation/semantics
		/// <para>SetEquals == SequenceEqual == Equals</para>
		/// </summary>
		/// <param name="self">ICodeSet</param>
		/// <param name="that">ICodeSet</param>
		/// <returns>True on value(set&amp;sequence) equality</returns>
		/// <remarks>Do not rename this extension method to "Equals"!</remarks>
		[Pure] public static bool SetEquals (this ICodeSet self, ICodeSet that) {
			bool? IsEqual = self.QuickSetEquals(that); 
			switch (IsEqual) {
				case false:
					return false; // one NullOrEmpty or both not QuickSetEquals

				case true:
					return true; // both NullOrEmpty or both QuickSetEquals

				default:
					Contract.Assert (IsEqual == null);

					// none empty, same count, same first, same last
					Contract.Assume (!self.IsNullOrEmpty());
					Contract.Assume (!that.IsNullOrEmpty());
					Contract.Assume (self.Count == that.Count);
					Contract.Assume (self.First == that.First);
					Contract.Assume (self.Last == that.Last);
		
					bool move = true;
					var e = that.GetEnumerator();
					foreach (var code in self) {
						move = e.MoveNext();
						Contract.Assume (move);
						if (code != e.Current) return false;
					}
					move = e.MoveNext();
					Contract.Assume (!move);
					return true;
			}
		}
		
		/// <summary>SequenceEqual extension  
		/// </summary>
		/// <param name="self">ICodeSet</param>
		/// <param name="that">ICodeSet</param>
		/// <returns></returns>
		[Pure] public static bool SequenceEqual (this ICodeSet self, ICodeSet that) {
			return self.SetEquals(that);
		}

		/// <summary>ICodeSet.GetHashCode method implementation/semantics
		/// </summary>
		/// <param name="self">ICodeSet</param>
		/// <returns>int HashCode</returns>
		/// <remarks>Do not rename this extension method to "GetHashCode"!</remarks>
		[Pure] public static int HashCode (this ICodeSet self)
		{
			if (self.IsNull() || self.Count == 0) return 0;
			return unchecked(self.First<<2) ^ unchecked(self.Count<<1) ^ (self.Last);
		}

		#endregion
		
		#region Quick Set Relations

		/// <summary>Equals check in constant time (no iteration)</summary>
		/// <param name="self"></param>
		/// <param name="that"></param>
		/// <returns>true/false/maybe</returns>
		[Pure] internal static bool? QuickSetEquals(this ICodeSet self, ICodeSet that)
		{
			// ATTN! '==' operator can be overloaded to point indirectly here
			// so using "ICodeSet == ICodeSet" here will create endless loop

			// Compare .Count(s)
			if ( (self.IsNull () || self.Count == 0) && (that.IsNull () || that.Count == 0) ) {
				return true;
			}

			// Not Equal if any of two is Empty ( Empty.Count==0 => IntersectWith.Count == 0 ).
			// Both empty is excluded from here / checked by rule above.
			if ( (self.IsNull () || self.Count == 0) || (that.IsNull () || that.Count == 0) ) {
				return false;
			}

			// Reference equals? (Empty excluded by rules above)
			if (self.Is(that)) { return true; }

			// Maybe Equals if Range&Count equals
			if (self.First == that.First && self.Last == that.Last && self.Count == that.Count)
			{
				// 100% Equals if this is a couple of pairSet|fullSet(unitSet included)
				if (self.Count == ICodeSetService.PairCount || self.Count == self.Length) { return true; }
			}
			else {
				// 100% Not Equals if Range|Count is not equal.
				return false;
			}

			Contract.Assert (!self.IsNullOrEmpty());
			Contract.Assert (!that.IsNullOrEmpty());
			Contract.Assert (self.Count == that.Count);
			Contract.Assert (self.First == that.First);
			Contract.Assert (self.Last == that.Last);

			return null; // Maybe Equals
		}

		/// <summary>Overlaps check in constant time (no iteration)</summary>
		/// <param name="self"></param>
		/// <param name="that"></param>
		/// <returns>true/false/maybe</returns>
		[Pure] internal static bool? QuickSetOverlaps(this ICodeSet self, ICodeSet that)
		{
			// QuickEquals is allways checked before QuickOverlaps

			// Never overlaps if ranges do not overlap. (Checks  empty ICodeSet too)
			if (!self.RangeOverlaps(that)) {
				return false;
			}

			Contract.Assert (!self.IsNull() && self.Count != 0 && !that.IsNull() && that.Count != 0);
			// From here none empty and ranges overlap, but is unknown if any member overlaps.

			// Quick test if known members (start/final member) overlaps
			// Covers all cases where one of ranges is full and more ...
			if (self[that.First] || self[that.Last] || that[self.First] || that[self.Last]) {
				return true;
			}

			return null; // Maybe Overlaps (RangeOverlaps == true)
		}

		#endregion
		
		#region Range Relations

		[Pure] public static bool RangeEquals (this ICodeSet self, ICodeSet that)
		{
			if (self.IsNull() || self.Count == 0) {
				if (that.IsNullOrEmpty()) return true;
				return false;
			}
			if (that.IsNull() || that.Count == 0) return false;

			return self.First == that.First && self.Last == that.Last;
		}

		[Pure] public static bool RangeOverlaps (this ICodeSet self, ICodeSet that)
		{
			if (self.IsNull() || self.Count == 0 || that.IsNull() || that.Count == 0) {
				return false; // empty ranges never overlap
			}
			if ( self.First > that.Last || self.Last < that.First ) {
				return false; // ranges do not overlap
			}
			return true;
		}

		[Pure] public static bool IsRangeSubsetOf (this ICodeSet self, ICodeSet that)
		{
			if (self.IsNull() || self.Count == 0 || that.IsNull() || that.Count == 0) {
				return false; // empty range is never subset/superset
			}
			if ( self.First >= that.First && self.Last <= that.Last ) {
				return true; // self.range.IsSubsetOf(that.range)
			}
			return false;
		}

		[Pure] public static bool IsProperRangeSubsetOf (this ICodeSet self, ICodeSet that)
		{
			return self.IsRangeSubsetOf(that) && self.Length < that.Length;
		}

		[Pure] public static bool IsRangeSupersetOf (this ICodeSet self, ICodeSet that)
		{
			return that.IsRangeSubsetOf(self);
		}
		
		[Pure] public static bool IsProperRangeSupersetOf (this ICodeSet self, ICodeSet that)
		{
			return that.IsProperRangeSubsetOf (self);
		}

		#endregion
		
	}
}
