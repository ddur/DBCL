// --------------------------------------------------------------------------------
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
	/// <summary>ICodeSet service &amp; extension methods
	/// <remarks>All public except methods returning ICodeSet</remarks>
	/// </summary>
	public static class ICodeSetService {
		
		private const bool ThisMethodHandlesNull = true;

		public const int UnitCount = 1;
		public const int PairCount = 2;
		public const int ListMaxCount = 16; // NOTE: log(16) == 4 tests, TODO: check speed on 32 and 64

		public const int NullStart = -1;
		public const int NullFinal = -2;

		#region To/From(Compact)BitSetArray Service

		[Pure] public static bool IsCompact(this BitSetArray self) {
			Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

			if (self.IsNull()) return false;
			
			if (self.Count == 0) {
				return self.Length == 0;
			}
			else {
				return self.Length.IsCodesCount() && self[0] && self[self.Length-1];
			}
		}
		
		/// <summary>Returns BitSetArray(self.Last+1) where BitSetArray.Item == self.Item
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		[Pure] public static BitSetArray ToBitSetArray(this ICodeSet self) {
			Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

			Contract.Ensures (Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures (((self.IsNull() || self.Count == 0)
							   && Contract.Result<BitSetArray>().Count == 0 )
							  ||
							  (Contract.Result<BitSetArray>().Count == self.Count()
							   && Contract.ForAll(self, item => Contract.Result<BitSetArray>()[item])
							  ));

			if (self.IsNull() || self.Count == 0) return new BitSetArray();
			
			var ret = new BitSetArray (self.Last+1);
			foreach (int code in self) {
				ret.Set (code, true);
			}
			return ret;
		}
		
		/// <summary>Returns compact BitSetArray(self.Length) where BitSetArray.Item == self.Item-self.First (offset)
		/// <remarks>Use to serialize ICodeSet</remarks>
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		[Pure] internal static BitSetArray ToCompact(this ICodeSet self) {
			Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

			Contract.Ensures (Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures (Contract.Result<BitSetArray>().IsCompact());

			if (self.IsNull() || self.Count == 0) return new BitSetArray();
			
			var bits = self as CodeSetBits;
			if (bits.IsNot(null)) return bits.ToCompact();
			
			var ret = new BitSetArray (self.Length);
			foreach (int code in self) {
				ret.Set (code-self.First, true);
			}

			// first and last bit set == compact
			Contract.Assert (ret[0]);
			Contract.Assert (ret[ret.Length-1]);

			return ret;
		}
		
		/// <summary>Returns compact BitSetArray(range.Length)
		/// <para>where self items.RangeOverlaps(range)</para>
		/// <para>and BitSetArray.Item == self.Item-range.First (offset)</para>
		/// <remarks>Use to map self.Items to range and perform set operations on two BitSetArray's</remarks>
		/// </summary>
		/// <param name="self"></param>
		/// <param name="range"></param>
		/// <returns></returns>
		[Pure] public static BitSetArray ToCompactOverlaps(this ICodeSet self, ICodeSet range) {
			Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

			Contract.Ensures (Contract.Result<BitSetArray> ().IsNot (null));
			Contract.Ensures (((range.IsNull () || range.Length == 0)
							   && Contract.Result<BitSetArray> ().Count == 0)
							  ||
							  (Contract.ForAll (self, item => ((int)item).InRange((int)range.First, (int)range.Last)
												&& Contract.Result<BitSetArray> ()[item - self.First])
							  ));

			if (self.IsNull() || self.Count == 0) return new BitSetArray();
			if (range.IsNull() || range.Length == 0) return new BitSetArray();

			var ret = new BitSetArray (range.Length);

			if (!self.RangeOverlaps(range)) return ret;
			
			foreach (int code in self) {
				if (code < range.First) continue;
				if (code > range.Last) break;
				ret.Set (code-range.First, true);
			}
			ret.TrimExcess();
			return ret;
		}

		#endregion

		#region ICodeSet Relations Service

		[Pure] public static bool Overlaps (this ICodeSet self, ICodeSet that) {

			switch (self.QuickSetEquals(that)) {
					case true: return true;
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
		
		[Pure] public static bool IsSubsetOf (this ICodeSet self, ICodeSet that) {
			return
				self.IsRangeSubsetOf(that) && // null/empty excluded
				self.Count <= that.Count &&
				self.All(item => that[item]);
		}
		
		[Pure] public static bool IsProperSubsetOf (this ICodeSet self, ICodeSet that) {
			return
				self.IsRangeSubsetOf(that) && // null/empty excluded
				self.Count < that.Count &&
				self.All(item => that[item]);
		}

		[Pure] public static bool IsSupersetOf (this ICodeSet self, ICodeSet that) {
			return that.IsSubsetOf(self);
		}
		
		[Pure] public static bool IsProperSupersetOf (this ICodeSet self, ICodeSet that) {
			return that.IsProperSubsetOf(self);
		}
		
		/// <summary>Compared as two BigIntegers (BitSetArray)
		/// </summary>
		/// <param name="self"></param>
		/// <param name="that"></param>
		/// <returns></returns>
		[Pure] public static int CompareTo (ICodeSet self, ICodeSet that) {
			
			if (self.QuickSetEquals(that) == true) { return 0; }
			Contract.Assert (!(self.IsNullOrEmpty() && that.IsNullOrEmpty()));

			if (self.IsNullOrEmpty()) { return -1; }
			if (that.IsNullOrEmpty()) { return 1; }
			if (self.Last < that.Last) { return -1; }
			if (self.Last > that.Last) { return 1; }

			return self.ToBitSetArray().CompareTo(that.ToBitSetArray());
		}

		#endregion

		#region ICodeSet Equality Service

		/// <summary>True if self.Equals(that)
		/// <para>ICodeSet is sorted/ordered set, Equals == SetEquals == SequenceEqual</para>
		/// </summary>
		/// <param name="self"></param>
		/// <param name="that"></param>
		/// <returns></returns>
		[Pure] public static bool Equals (ICodeSet self, ICodeSet that) {
			switch (self.QuickSetEquals(that)) {
				case false:
					return false; // one NullOrEmpty or both not QuickSetEquals
				case true:
					return true; // both NullOrEmpty or both QuickSetEquals
			}

			// none empty, same count, same first, same last
			Contract.Assume (!self.IsNullOrEmpty());
			Contract.Assume (!that.IsNullOrEmpty());
			Contract.Assume (self.Count == that.Count);
			Contract.Assume (self.First == that.First);
			Contract.Assume (self.Last == that.Last);

			var e = that.GetEnumerator();
			foreach (var code in self) {
				e.MoveNext();
				if (code != e.Current) return false;
			}
			return true;
		}
		
		[Pure] public static int GetHashCode (ICodeSet self) {
			if (self.IsNull() || self.Count == 0) return 0;
			return unchecked(self.First<<2) ^ unchecked(self.Count<<1) ^ (self.Last);
		}

		[Pure] public static bool SequenceEqual (this ICodeSet self, ICodeSet that) {
			return ICodeSetService.Equals(self, that);
		}

		#endregion
		
		#region Range Relations Service

		[Pure] public static bool RangeEquals (this ICodeSet self, ICodeSet that) {
			Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

			if (self.IsNull() || self.Count == 0) {
				if (that.IsNullOrEmpty()) return true;
				return false;
			}
			if (that.IsNull() || that.Count == 0) return false;

			return self.First == that.First && self.Last == that.Last;
		}

		[Pure] public static bool RangeOverlaps (this ICodeSet self, ICodeSet that) {
			Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

			if (self.IsNull() || self.Count == 0 || that.IsNull() || that.Count == 0) {
				return false; // empty ranges never overlap
			}
			if ( self.First > that.Last || self.Last < that.First ) {
				return false; // ranges do not overlap
			}
			return true;
		}

		[Pure] public static bool IsRangeSubsetOf (this ICodeSet self, ICodeSet that) {
			Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

			if (self.IsNull() || self.Count == 0 || that.IsNull() || that.Count == 0) {
				return false; // empty range is never subset/superset
			}
			if ( self.First >= that.First && self.Last <= that.Last ) {
				return true; // self.range.IsSubsetOf(that.range)
			}
			return false;
		}

		[Pure] public static bool IsProperRangeSubsetOf (this ICodeSet self, ICodeSet that) {
			Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

			return self.IsRangeSubsetOf(that) && self.Length < that.Length;
		}

		[Pure] public static bool IsRangeSupersetOf (this ICodeSet self, ICodeSet that) {
			Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

			return that.IsRangeSubsetOf(self);
		}
		
		[Pure] public static bool IsProperRangeSupersetOf (this ICodeSet self, ICodeSet that) {
			Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

			return that.IsProperRangeSubsetOf (self);
		}

		#endregion
		
		#region Quick Set Relations

		/// <summary>Equals check in constant time (no iteration)</summary>
		/// <param name="self"></param>
		/// <param name="that"></param>
		/// <returns>true/false/maybe</returns>
		[Pure] private static bool? QuickSetEquals(this ICodeSet self, ICodeSet that) {
			Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

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
				if (self.Count == PairCount || self.Count == self.Length) { return true; }
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
		[Pure] private static bool? QuickSetOverlaps(this ICodeSet self, ICodeSet that) {
			Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

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
		
		#region Properties Service

		[Pure] public static bool IsNullOrEmpty (this ICodeSet self) {
			Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

			return self.IsNull() || self.Count == 0;
		}

		[Pure] public static bool IsNullOrEmpty (this BitSetArray self) {
			Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

			return self.IsNull() || self.Count == 0;
		}
		
		[Pure] internal static bool IsFull (this ICodeSet self) {
			Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

			return !self.IsNull() && self.Count != 0 && self.Count == self.Length;
		}

		[Pure] internal static bool IsFull (this BitSetArray self) {
			Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

			return !self.IsNull() && self.Count != 0 && (self.Count == (1 + (int)self.Last - (int)self.First));
		}

		#endregion

	}
}
