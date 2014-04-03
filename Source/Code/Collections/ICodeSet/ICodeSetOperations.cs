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
	/// <summary>
	/// Description of ICodeSetOperations.
	/// </summary>
	internal static class ICodeSetOperations
	{
		private static BitSetArray NoBits {
			get { return new BitSetArray(); }
		}

		#region Union or(a,b,c...)

		public static BitSetArray BitUnion (this ICodeSet req, params ICodeSet[] opt)
		{
			Contract.Ensures (Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures (Contract.Result<BitSetArray>().Length <= Code.MaxCount);

			var setList = new List<ICodeSet> (1 + opt.Length);
			setList.Add (req);
			setList.AddRange (opt);
			return setList.Count == 0 ? NoBits : BitUnion(setList);
		}

		public static BitSetArray BitUnion (this IEnumerable<ICodeSet> sets) {

			Contract.Ensures (Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures (Contract.Result<BitSetArray>().Length <= Code.MaxCount);

			if (sets.IsNull()) { return NoBits; }
			var e = sets.GetEnumerator();
			BitSetArray result = null;
			while (e.MoveNext()) {
				if (result.IsNull()) {
					result = e.Current.ToBitSetArray();
				}
				else if (!e.Current.IsNullOrEmpty()){
					result.Or(e.Current.ToBitSetArray());
				}
			}
			return result; 
		}

		#endregion
		
		#region Intersection and(((a,b),c),d...)
		
		public static BitSetArray BitIntersection (this ICodeSet req, params ICodeSet[] opt)
		{
			Contract.Ensures (Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures (Contract.Result<BitSetArray>().Length <= Code.MaxCount);

			var setList = new List<ICodeSet> (1 + opt.Length);
			setList.Add (req);
			setList.AddRange (opt);
			return BitIntersection (setList);
		}

		public static BitSetArray BitIntersection (this IEnumerable<ICodeSet> sets)
		{
			Contract.Ensures (Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures (Contract.Result<BitSetArray>().Length <= Code.MaxCount);

			if (sets.IsNull()) { return NoBits; }
			if (sets.Count() < 2) { return NoBits; } // intersection with empty == empty

			var e = sets.GetEnumerator();
			BitSetArray result = null;
			while (e.MoveNext()) {
				if (e.Current.IsNullOrEmpty()) { return NoBits; } // no intersection possible
				if (result.IsNull()) {
					result = e.Current.ToBitSetArray();
				}
				else {
					result.And(e.Current.ToBitSetArray());
					if (result.Count == 0) { break; } // no more intersection possible
				}
			}
			return result; 
		}

		#endregion
		
		#region Disjunction xor(((a,b),c),d...)

		public static BitSetArray BitDisjunction (this ICodeSet req, params ICodeSet[] opt)
		{
			Contract.Ensures (Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures (Contract.Result<BitSetArray>().Length <= Code.MaxCount);

			var setList = new List<ICodeSet> (1 + opt.Length);
			setList.Add (req);
			setList.AddRange (opt);
			return BitDisjunction (setList);
		}

		public static BitSetArray BitDisjunction (this IEnumerable<ICodeSet> sets) {

			Contract.Ensures (Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures (Contract.Result<BitSetArray>().Length <= Code.MaxCount);

			if (sets.IsNull()) { return NoBits; }
			var e = sets.GetEnumerator();
			BitSetArray result = null;
			while (e.MoveNext()) {
				if (result.IsNull()) {
					result = e.Current.ToBitSetArray();
				}
				else if (!e.Current.IsNullOrEmpty()){
					result.Xor(e.Current.ToBitSetArray());
				}
			}
			return result; 
		}

		#endregion

		#region Difference (((a-b)-c)-d...)

		public static BitSetArray BitDifference (this ICodeSet req, params ICodeSet[] opt)
		{
			Contract.Ensures (Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures (Contract.Result<BitSetArray>().Length <= Code.MaxCount);

			var setList = new List<ICodeSet> (1 + opt.Length);
			setList.Add (req);
			setList.AddRange (opt);
			return BitDifference (setList);
		}

		public static BitSetArray BitDifference (this IEnumerable<ICodeSet> sets)
		{
			Contract.Ensures (Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures (Contract.Result<BitSetArray>().Length <= Code.MaxCount);

			if (sets.IsNull()) { return NoBits; }

			var e = sets.GetEnumerator();
			BitSetArray result = null;
			while (e.MoveNext()) {
				if (result.IsNull()) {
					if (e.Current.IsNullOrEmpty()) { return NoBits; } // no difference possible
					result = e.Current.ToBitSetArray();
				}
				else {
					result.Not(e.Current.ToBitSetArray());
					if (result.Count == 0) { break; } // no more difference possible
				}
			}
			return result; 
		}

		#endregion
		
		#region Complement
		
		public static BitSetArray BitComplement (this ICodeSet self)
		{
			Contract.Ensures (Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures (Contract.Result<BitSetArray>().Length <= Code.MaxCount);

			if (self.Is(null) || ((self.Length - self.Count) == 0)) return NoBits;

			BitSetArray compact = self.ToCompact();
			compact.Not();
			Contract.Assert (compact.Count != 0);
			var complement = new BitSetArray(self.Last+1);
			foreach (var item in compact) {
				complement.Set(item + self.First);
			}
			return complement;
		}
		
		#endregion
		
	}
}
