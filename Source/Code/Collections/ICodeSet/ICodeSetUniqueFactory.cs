// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using DD.Diagnostics;
using DD.Text;

namespace DD.Collections.ICodeSet
{
	public class ICodeSetUniqueFactory
	{
		#region Embed

		/// <summary>ICodeSet QuickWrap over BitSetArray</summary>
		private class QuickWrap : CodeSet
		{

			#region Ctor

			public QuickWrap(BitSetArray bits)
			{
				Contract.Requires<ArgumentNullException>(bits.IsNot(null));
				Contract.Requires<ArgumentOutOfRangeException>(bits.Count == 0 || bits.Length.IsCodesCount() || bits.Last.HasCodeValue());

				Contract.Ensures(Theory.Construct(bits, this));
		
				sorted = bits;
			}

			#endregion

			#region Fields

			readonly BitSetArray sorted;

			#endregion

			#region ICodeSet
	
			[Pure] public override bool this[Code code] {
				get {
					return sorted[code.Value];
				}
			}
	
			[Pure] public override int Count {
				get {
					return sorted.Count;
				}
			}
	
			[Pure] public override int Length {
				get {
					return sorted.Span();
				}
			}
	
			[Pure] public override Code First {
				get {
					return (Code)sorted.First;
				}
			}
	
			[Pure] public override Code Last {
				get {
					return (Code)sorted.Last;
				}
			}
	
			[Pure] public override IEnumerator<Code> GetEnumerator()
			{
				foreach (var code in sorted) {
					yield return (Code)code;
				}
			}
	
			#endregion

			#region To BitSetArray

			public BitSetArray ToBitSetArray()
			{
				return sorted;
			}

			#endregion
	
			#region Invariant
	
			[ContractInvariantMethod]
			private void Invariant()
			{
				Contract.Invariant(Theory.Invariant(this));
			}
	
			#endregion
	
			#region Theory
	
			static class Theory
			{
	
				[Pure] public static bool Construct(BitSetArray bits, QuickWrap self)
				{
					Success success = true;
	
					success.Assert(!bits.IsNull());
					success.Assert(bits.Count != 0);
					success.Assert(
						bits.Length <= Code.MaxCount ||
						bits.Last <= Code.MaxCount
					);
					success.Assert(self.sorted.Is(bits));
	
					return success;
				}
				
				[Pure] public static bool Invariant(QuickWrap self)
				{
					Success success = true;
					
					// private
					success.Assert(!self.sorted.IsNull());
					success.Assert(self.sorted.Count != 0);
					success.Assert(
						self.sorted.Length <= Code.MaxCount ||
						self.sorted.Last <= Code.MaxCount
					);
		
					success.Assert(self.sorted.First.HasCodeValue());
					success.Assert(self.sorted.Last.HasCodeValue());
					
					// public <- private
					success.Assert(self.Length == self.sorted.Span());
					success.Assert(self.Count == self.sorted.Count);
					success.Assert(self.First == (Code)self.sorted.First);
					success.Assert(self.Last == (Code)self.sorted.Last);
					
					return success;
				}
			}
	
			#endregion
		}

		#endregion

		#region Fields

		private readonly C5.HashSet<ICodeSet> dictionary = new C5.HashSet<ICodeSet>(); 

		#endregion

		#region From items

		public ICodeSet From(string utf16)
		{
			Contract.Requires<ArgumentException>(utf16.CanDecode());
			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));
			Contract.Ensures(Theory.From(utf16, this, Contract.Result<ICodeSet>()));

			return string.IsNullOrEmpty(utf16) ? CodeSetNull.Singleton : From(utf16.Decode());
		}

		public ICodeSet From(params char[] chars)
		{
			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));
			Contract.Ensures(Theory.From(chars, this, Contract.Result<ICodeSet>()));

			return chars.IsNullOrEmpty() ? CodeSetNull.Singleton : From(chars.ToValues());
		}

		public ICodeSet From(IEnumerable<char> chars)
		{
			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));
			Contract.Ensures(Theory.From(chars, this, Contract.Result<ICodeSet>()));

			return chars.IsNullOrEmpty() ? CodeSetNull.Singleton : From(chars.ToValues());
		}

		public ICodeSet From(IEnumerable<Code> codes)
		{
			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));
			Contract.Ensures(Theory.From(codes, this, Contract.Result<ICodeSet>()));

			return codes.IsNullOrEmpty() ? CodeSetNull.Singleton : From(codes.ToValues());
		}

		public ICodeSet From(params int[] values)
		{
			Contract.Requires<ArgumentException>(values.IsNullOrEmpty() || Contract.ForAll(values, value => value.HasCodeValue()));

			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));
			Contract.Ensures(Theory.From(values, this, Contract.Result<ICodeSet>()));

			return values.IsNullOrEmpty() ? CodeSetNull.Singleton : From(BitSetArray.From(values));
		}

		public ICodeSet From(IEnumerable<int> values)
		{
			Contract.Requires<ArgumentException>(values.IsNullOrEmpty() || Contract.ForAll(values, value => value.HasCodeValue()));

			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));
			Contract.Ensures(Theory.From(values, this, Contract.Result<ICodeSet>()));

			return values.IsNullOrEmpty() ? CodeSetNull.Singleton : From(BitSetArray.From(values));
		}

		public ICodeSet From(BitSetArray bits)
		{
			Contract.Requires<ArgumentException>(bits.IsNullOrEmpty() || bits.Length.IsCodesCount() || bits.Last.HasCodeValue());

			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));
			Contract.Ensures(Theory.From(bits, this, Contract.Result<ICodeSet>()));

			return bits.IsNullOrEmpty() ? CodeSetNull.Singleton : From(new QuickWrap(bits)); 
		}

		public ICodeSet From(ICodeSet iset)
		{
			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));
			Contract.Ensures(Theory.From(iset, this, Contract.Result<ICodeSet>()));

			if (iset.IsNull()) {
				return CodeSetNull.Singleton;
			}
			ICodeSet key = iset;
			if (!dictionary.Find(ref key)) {
				var qset = iset as QuickWrap;
				key = !qset.IsNull() ? qset.ToBitSetArray().Reduce() : iset.Reduce();
				dictionary.Add(key);
			}
			return key;
		}

		#endregion

		#region Sets Factored

		public IEnumerable<ICodeSet> Factored {
			get {
				foreach (var item in dictionary) {
					yield return item;
				}
			}
		}

		#endregion

		#region Set Operations

		// ICodeSet operation names differ from ISet<T> names because
		// ICodeSet operation does not mutate any operand and returns new set.
		// (like string, unlike ISet<T> operations)

		#region Union bits.Or(a,b,c...)

		public ICodeSet Union(ICodeSet lhs, ICodeSet rhs, params ICodeSet[] opt)
		{
			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));

			return From(lhs.BitUnion(rhs, opt));
		}

		public ICodeSet Union(IEnumerable<ICodeSet> sets)
		{
			Contract.Requires<ArgumentNullException>(!sets.IsNull());
			Contract.Requires<ArgumentException>(sets.Count() >= 2);

			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));

			return From(sets.BitUnion());
		}

		#endregion

		#region Intersection bits.And(((a,b),c),d...)

		public ICodeSet Intersection(ICodeSet lhs, ICodeSet rhs, params ICodeSet[] opt)
		{
			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));

			return From(lhs.BitIntersection(rhs, opt));
		}

		public ICodeSet Intersection(IEnumerable<ICodeSet> sets)
		{
			Contract.Requires<ArgumentNullException>(!sets.IsNull());
			Contract.Requires<ArgumentException>(sets.Count() >= 2);

			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));

			return From(sets.BitIntersection());
		}

		#endregion

		#region Disjunction bits.Xor(((a,b),c),d...)

		public ICodeSet Disjunction(ICodeSet lhs, ICodeSet rhs, params ICodeSet[] opt)
		{
			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));

			return From(lhs.BitDisjunction(rhs, opt));
		}

		public ICodeSet Disjunction(IEnumerable<ICodeSet> sets)
		{
			Contract.Requires<ArgumentNullException>(!sets.IsNull());
			Contract.Requires<ArgumentException>(sets.Count() >= 2);

			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));

			return From(sets.BitDisjunction());
		}

		#endregion

		#region Difference bits.Not(((a-b)-c)-d...)

		public ICodeSet Difference(ICodeSet lhs, ICodeSet rhs, params ICodeSet[] opt)
		{
			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));

			return From(lhs.BitDifference(rhs, opt));
		}

		public ICodeSet Difference(IEnumerable<ICodeSet> sets)
		{
			Contract.Requires<ArgumentNullException>(!sets.IsNull());
			Contract.Requires<ArgumentException>(sets.Count() >= 2);

			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));

			return From(sets.BitDifference());
		}

		#endregion

		#region Complement bits.Not()

		public ICodeSet Complement(ICodeSet that)
		{
			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));

			return From(that.BitComplement());
		}

		#endregion

		#endregion

		#region Invariant

		[ContractInvariantMethod]
		private void Invariant()
		{
			Contract.Invariant(dictionary.IsNot(null)); 
			Contract.Invariant(!dictionary.Contains(CodeSetNull.Singleton)); 
		}

		#endregion

		#region Theory

		private static class Theory
		{
			[Pure] public static bool Result(ICodeSetUniqueFactory self, ICodeSet result)
			{
				Success success = true;

				success.Assert(result.IsNot(null));
				success.Assert(result.IsReduced());

				if (result.Count != 0) {
					var key = result;
					success.Assert(self.dictionary.Find(ref key));
					success.Assert(key.Is(result));
				}
				return success;
			}

			[Pure] public static bool From(string utf16, ICodeSetUniqueFactory self, ICodeSet result)
			{
				Success success = true;

				if (!utf16.IsNullOrEmpty()) {
					success.Assert(utf16.Decode().Distinct().OrderBy(item => (item)).SequenceEqual(result));
				} else {
					success.Assert(result.Count == 0);
				}
				return success;
			}

			[Pure] public static bool From(IEnumerable<char> chars, ICodeSetUniqueFactory self, ICodeSet result)
			{
				Success success = true;

				if (!chars.IsNullOrEmpty()) {
					success.Assert(chars.ToCodes().Distinct().OrderBy(item => (item)).SequenceEqual(result));
				} else {
					success.Assert(result.Count == 0);
				}
				return success;
			}

			[Pure] public static bool From(IEnumerable<Code> codes, ICodeSetUniqueFactory self, ICodeSet result)
			{
				Success success = true;

				if (!codes.IsNullOrEmpty()) {
					success.Assert(codes.Distinct().OrderBy(item => (item)).SequenceEqual(result));
				} else {
					success.Assert(result.Count == 0);
				}
				return success;
			}

			[Pure] public static bool From(IEnumerable<int> values, ICodeSetUniqueFactory self, ICodeSet result)
			{
				Success success = true;

				if (!values.IsNullOrEmpty()) {
					success.Assert(values.ToCodes().Distinct().OrderBy(item => (item)).SequenceEqual(result));
				} else {
					success.Assert(result.Count == 0);
				}
				return success;
			}

		}

		#endregion
	}
}


