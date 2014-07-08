// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DD.Collections.ICodeSet
{
	/// <summary>
	/// Description of ICodeSetOperations.
	/// </summary>
	public static class ICodeSetOperations
	{
		private static BitSetArray NoBits {
			get { return BitSetArray.Size(); }
		}

		#region Union or(a,b,c...)

		public static BitSetArray BitUnion(this ICodeSet self, ICodeSet that, params ICodeSet[] list)
		{
			Contract.Ensures(Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures(Contract.Result<BitSetArray>().Length <= Code.MaxCount);

			var setList = new List<ICodeSet>();
			setList.Add(self);
			setList.Add(that);
			if (!list.IsNull() && list.Length != 0)
				setList.AddRange(list);
			return setList.BitUnion();
		}

		public static BitSetArray BitUnion(this IEnumerable<ICodeSet> sets)
		{
			Contract.Requires<ArgumentNullException>(!sets.IsNull());
			Contract.Requires<ArgumentException>(sets.Count() >= 2);

			Contract.Ensures(Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures(Contract.Result<BitSetArray>().Length <= Code.MaxCount);

			var e = sets.GetEnumerator();
			e.MoveNext();
			BitSetArray result = e.Current.ToBitSetArray();
			while (e.MoveNext()) {
				result.Or(e.Current.ToBitSetArray());
			}
			return result;
		}

		#endregion
		
		#region Intersection and(((a,b),c),d...)
		
		public static BitSetArray BitIntersection(this ICodeSet self, ICodeSet that, params ICodeSet[] list)
		{
			Contract.Ensures(Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures(Contract.Result<BitSetArray>().Length <= Code.MaxCount);

			var setList = new List<ICodeSet>();
			setList.Add(self);
			setList.Add(that);
			if (!list.IsNull() && list.Length != 0)
				setList.AddRange(list);
			return setList.BitIntersection();
		}

		public static BitSetArray BitIntersection(this IEnumerable<ICodeSet> sets)
		{
			Contract.Requires<ArgumentNullException>(!sets.IsNull());
			Contract.Requires<ArgumentException>(sets.Count() >= 2);

			Contract.Ensures(Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures(Contract.Result<BitSetArray>().Length <= Code.MaxCount);

			var e = sets.GetEnumerator();
			e.MoveNext();
			BitSetArray result = e.Current.ToBitSetArray();
			while (e.MoveNext()) {
				if (result.IsEmpty()) {
					break; // no intersection possible with empty
				}
				result.And(e.Current.ToBitSetArray());
			}
			return result;
		}

		#endregion
		
		#region Disjunction xor(((a,b),c),d...)

		public static BitSetArray BitDisjunction(this ICodeSet self, ICodeSet that, params ICodeSet[] list)
		{
			Contract.Ensures(Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures(Contract.Result<BitSetArray>().Length <= Code.MaxCount);

			var setList = new List<ICodeSet>();
			setList.Add(self);
			setList.Add(that);
			if (!list.IsNull() && list.Length != 0)
				setList.AddRange(list);
			return setList.BitDisjunction();
		}

		public static BitSetArray BitDisjunction(this IEnumerable<ICodeSet> sets)
		{
			Contract.Requires<ArgumentNullException>(!sets.Is(null));
			Contract.Requires<ArgumentException>(sets.Count() >= 2);

			Contract.Ensures(Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures(Contract.Result<BitSetArray>().Length <= Code.MaxCount);

			var e = sets.GetEnumerator();
			e.MoveNext();
			BitSetArray result = e.Current.ToBitSetArray();
			while (e.MoveNext()) {
				result.Xor(e.Current.ToBitSetArray());
			}
			return result;
		}

		#endregion

		#region Difference (((a-b)-c)-d...)

		public static BitSetArray BitDifference(this ICodeSet self, ICodeSet that, params ICodeSet[] list)
		{
			Contract.Ensures(Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures(Contract.Result<BitSetArray>().Length <= Code.MaxCount);

			var setList = new List<ICodeSet>();
			setList.Add(self);
			setList.Add(that);
			if (!list.IsNull() && list.Length != 0)
				setList.AddRange(list);
			return setList.BitDifference();
		}

		public static BitSetArray BitDifference(this IEnumerable<ICodeSet> sets)
		{
			Contract.Requires<ArgumentNullException>(!sets.IsNull());
			Contract.Requires<ArgumentException>(sets.Count() >= 2);

			Contract.Ensures(Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures(Contract.Result<BitSetArray>().Length <= Code.MaxCount);

			var e = sets.GetEnumerator();
			e.MoveNext();
			BitSetArray result = e.Current.ToBitSetArray();
			while (e.MoveNext()) {
				if (result.IsEmpty()) {
					break; // no difference possible from empty
				}
				result.Not(e.Current.ToBitSetArray());
			}
			return result;
		}

		#endregion
		
		#region Complement
		
		public static BitSetArray BitComplement(this ICodeSet self)
		{
			Contract.Ensures(Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures(Contract.Result<BitSetArray>().Length <= Code.MaxCount);

			if (self.Is(null) || ((self.Length - self.Count) == 0))
				return NoBits;

			// TODO? performance test between two options below
			var complement = BitSetArray.Size(self.Last);
			if ((self.Length - self.Count) < (self.Length / 2)) {
				BitSetArray compact = self.ToCompact();
				compact.Not();
				foreach (var item in compact) {
					complement._Set(item + self.First);
				}
			} else {
				for (int item = self.First + 1; item < self.Last; item++) {
					if (!self[item]) {
						complement._Set(item);
					}
				}
			}
			Contract.Assert(complement.Count != 0);
			return complement;
		}
		
		#endregion
		
	}
}
