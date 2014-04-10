// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using DD.Text;
namespace DD.Collections.ICodeSet
{
	public class ICodeSetUniqueFactory
	{
		#region Ctor

		public ICodeSetUniqueFactory()
		{
			outputDictionary = new ICodeSetDictionary();
		}

		#endregion

		#region Fields

		private readonly ICodeSetDictionary outputDictionary;

		#endregion

		#region From items

		public ICodeSet From(string utf16)
		{
			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures(outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return string.IsNullOrEmpty(utf16) ? CodeSetNull.Singleton : From(utf16.Decode());
		}

		public ICodeSet From(params char[] chars)
		{
			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures(outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return chars.IsNullOrEmpty() ? CodeSetNull.Singleton : From(chars.ToValues());
		}

		public ICodeSet From(IEnumerable<char> chars)
		{
			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures(outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return chars.IsNullOrEmpty() ? CodeSetNull.Singleton : From(chars.ToValues());
		}

		public ICodeSet From(IEnumerable<Code> codes)
		{
			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures(outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return codes.IsNullOrEmpty() ? CodeSetNull.Singleton : From(codes.ToValues());
		}

		public ICodeSet From(params int[] values)
		{
			Contract.Requires<ArgumentException>(values.IsNullOrEmpty() || Contract.ForAll(values, value => value.HasCodeValue()));

			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures(outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return values.IsNullOrEmpty() ? CodeSetNull.Singleton : From(BitSetArray.From(values));
		}

		public ICodeSet From(IEnumerable<int> values)
		{
			Contract.Requires<ArgumentException>(values.IsNullOrEmpty() || Contract.ForAll(values, value => value.HasCodeValue()));

			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures(outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return values.IsNullOrEmpty() ? CodeSetNull.Singleton : From(BitSetArray.From(values));
		}

		public ICodeSet From(BitSetArray bits)
		{
			Contract.Requires<ArgumentException>(bits.IsNullOrEmpty() || bits.Length.IsCodesCount() || bits.Last.HasCodeValue());

			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures(outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			if (bits.IsNullOrEmpty()) { return CodeSetNull.Singleton; }
			
			ICodeSet key = new CodeSetWrap(bits);
			if (!outputDictionary.Find(ref key)) {
				key = bits.Reduce();
				outputDictionary.Add(key);
			}
			return key;
		}

		#endregion

		#region Set Operations

		// ICodeSet operation names differ from ISet<T> names because
		// ICodeSet operation does not mutate any operand and returns new set.
		// (like string, unlike ISet<T> operations that mutate (and returns) left operand)

		#region Union bits.Or(a,b,c...)

		public ICodeSet Union(params ICodeSet[] sets)
		{
			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures(outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From(sets.BitUnion());
		}

		public ICodeSet Union(IEnumerable<ICodeSet> sets)
		{
			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures(outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From(sets.BitUnion());
		}

		#endregion

		#region Intersection bits.And(((a,b),c),d...)

		public ICodeSet Intersection(params ICodeSet[] sets)
		{
			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures(outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From(sets.BitIntersection());
		}

		public ICodeSet Intersection(IEnumerable<ICodeSet> sets)
		{
			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures(outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From(sets.BitIntersection());
		}

		#endregion

		#region Disjunction bits.Xor(((a,b),c),d...)

		public ICodeSet Disjunction(params ICodeSet[] sets)
		{
			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures(outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From(sets.BitDisjunction());
		}

		public ICodeSet Disjunction(IEnumerable<ICodeSet> sets)
		{
			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures(outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From(sets.BitDisjunction());
		}

		#endregion

		#region Difference bits.Not(((a-b)-c)-d...)

		public ICodeSet Difference(params ICodeSet[] sets)
		{
			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures(outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From(sets.BitDifference());
		}

		public ICodeSet Difference(IEnumerable<ICodeSet> sets)
		{
			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures(outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From(sets.BitDifference());
		}

		#endregion

		#region Complement bits.Not()

		public ICodeSet Complement(ICodeSet that)
		{
			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures(outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From(that.BitComplement());
		}

		#endregion

		#endregion

	}
}


