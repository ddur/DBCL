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

        /// <summary>QuickWrap ICodeSet over BitSetArray</summary>
        class QuickWrap : CodeSet
		{
			#region Ctor

			internal QuickWrap(BitSetArray bits)
			{
				Contract.Requires<ArgumentNullException>(bits != null);
                Contract.Requires<ArgumentEmptyException> (bits.Count != 0);
                Contract.Requires<IndexOutOfRangeException> ((int)bits.Last <= Code.MaxValue);

				Contract.Ensures(Theory.Construct(bits, this));

                this.sorted = bits;

                Contract.Assume (sorted.Count > 0);
                Contract.Assume (sorted.First.HasValue);
                Contract.Assume (sorted.Last.HasValue);
                Contract.Assume (sorted.First.HasCodeValue ());
                Contract.Assume (sorted.Last.HasCodeValue ());

                this.count = sorted.Count;
                this.length = sorted.Span ();
                this.start = (Code)sorted.First;
                this.final = (Code)sorted.Last;
            }

			#endregion

			#region Fields

			readonly BitSetArray sorted;

            // stupid but so far this helps static checker to pass
            readonly int count;
            readonly int length;
            readonly Code start;
            readonly Code final;

            #endregion

			#region ICodeSet
	
			[Pure] public override bool this[Code code] {
				get {
					return sorted[code.Value];
				}
			}
	
			[Pure] public override int Count {
				get {
					return this.count;
				}
			}
	
			[Pure] public override int Length {
				get {
					return this.length;
				}
			}
	
			[Pure] public override Code First {
				get {
					return this.start;
				}
			}
	
			[Pure] public override Code Last {
				get {
                    return this.final;
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
				//Contract.Invariant(Theory.Invariant(this));
			}
	
			#endregion
	
			#region Theory
	
			static class Theory
			{
	
				[Pure] public static bool Construct(BitSetArray bits, QuickWrap self)
				{
					Success success = true;

                    success.Assert (!bits.IsNull ());
                    success.Assert (bits.Count > 0);

                    success.Assert (bits.First.HasValue);
                    success.Assert (bits.Last.HasValue);

                    success.Assert (self.sorted.First.HasCodeValue ());
                    success.Assert (self.sorted.Last.HasCodeValue ());

                    success.Assert (self.sorted.Is (bits));
	
					return success;
				}
				
				[Pure] public static bool Invariant(QuickWrap self)
				{
					Success success = true;
					
					// private
					success.Assert(!self.sorted.IsNull());
					success.Assert(self.sorted.Count > 0);

                    success.Assert (self.sorted.First.HasValue);
                    success.Assert (self.sorted.Last.HasValue);
                    Contract.Assume (self.sorted.First.HasValue);
                    Contract.Assume (self.sorted.Last.HasValue);

                    success.Assert (self.sorted.First.HasCodeValue ());
                    success.Assert (self.sorted.Last.HasCodeValue ());

					// public <- private
                    success.Assert (self.Length == self.sorted.Span ());
                    success.Assert (self.Count == self.sorted.Count);
                    success.Assert (self.First == (Code)self.sorted.First);
                    success.Assert (self.Last == (Code)self.sorted.Last);
					
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

			return string.IsNullOrEmpty(utf16) ? CodeSetNone.Singleton : From(utf16.Decode());
		}

		public ICodeSet From(params char[] chars)
		{
			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));
			Contract.Ensures(Theory.From(chars, this, Contract.Result<ICodeSet>()));

			return chars.IsNullOrEmpty() ? CodeSetNone.Singleton : From(chars.ToValues());
		}

		public ICodeSet From(IEnumerable<char> chars)
		{
			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));
			Contract.Ensures(Theory.From(chars, this, Contract.Result<ICodeSet>()));

			return chars.IsNullOrEmpty() ? CodeSetNone.Singleton : From(chars.ToValues());
		}

		public ICodeSet From(IEnumerable<Code> codes)
		{
			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));
			Contract.Ensures(Theory.From(codes, this, Contract.Result<ICodeSet>()));

			return codes.IsNullOrEmpty() ? CodeSetNone.Singleton : From(codes.ToValues());
		}

		public ICodeSet From(params int[] values)
		{
			Contract.Requires<ArgumentException>(values.IsNullOrEmpty() || Contract.ForAll(values, value => value.HasCodeValue()));

			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));
			Contract.Ensures(Theory.From(values, this, Contract.Result<ICodeSet>()));

			return values.IsNullOrEmpty() ? CodeSetNone.Singleton : QuickFrom(BitSetArray.From(values));
		}

		public ICodeSet From(IEnumerable<int> values)
		{
			Contract.Requires<ArgumentException>(values.IsNullOrEmpty() || Contract.ForAll(values, value => value.HasCodeValue()));

			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));
			Contract.Ensures(Theory.From(values, this, Contract.Result<ICodeSet>()));

			return values.IsNullOrEmpty() ? CodeSetNone.Singleton : QuickFrom(BitSetArray.From(values));
		}

        public ICodeSet From (BitSetArray bits) {
            Contract.Requires<ArgumentException> (bits.IsNullOrEmpty () || bits.Length <= Code.MaxCount || (int)bits.Last <= Code.MaxValue);

            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));
            Contract.Ensures (Theory.From (bits, this, Contract.Result<ICodeSet> ()));

            return bits.IsNullOrEmpty () ? CodeSetNone.Singleton : From ((ICodeSet)CodeSetBits.From (bits, 0));
        }

        ICodeSet QuickFrom (BitSetArray bits)
		{
            Contract.Requires<ArgumentException> (bits.IsNullOrEmpty() || bits.Length <= Code.MaxCount || (int)bits.Last <= Code.MaxValue);

			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));
			Contract.Ensures(Theory.From(bits, this, Contract.Result<ICodeSet>()));

            return bits.IsNullOrEmpty () ? CodeSetNone.Singleton : From ((ICodeSet)new QuickWrap(bits));
        }

		public ICodeSet From(ICodeSet iset)
		{
			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));
			Contract.Ensures(Theory.From(iset, this, Contract.Result<ICodeSet>()));

			if (iset.IsNull()) {
				return CodeSetNone.Singleton;
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
                Contract.Ensures(Contract.Result<IEnumerable<ICodeSet>>() != null);
                foreach (var item in dictionary)
                {
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

			return QuickFrom(lhs.BitUnion(rhs, opt));
		}

		public ICodeSet Union(IEnumerable<ICodeSet> sets)
		{
            Contract.Requires<ArgumentNullException>(sets != null);
			Contract.Requires<ArgumentException>(sets.Count() >= 2);

			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));

			return QuickFrom(sets.BitUnion());
		}

		#endregion

		#region Intersection bits.And(((a,b),c),d...)

		public ICodeSet Intersection(ICodeSet lhs, ICodeSet rhs, params ICodeSet[] opt)
		{
			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));

			return QuickFrom(lhs.BitIntersection(rhs, opt));
		}

		public ICodeSet Intersection(IEnumerable<ICodeSet> sets)
		{
            Contract.Requires<ArgumentNullException>(sets != null);
			Contract.Requires<ArgumentException>(sets.Count() >= 2);

			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));

			return QuickFrom(sets.BitIntersection());
		}

		#endregion

		#region Disjunction bits.Xor(((a,b),c),d...)

		public ICodeSet Disjunction(ICodeSet lhs, ICodeSet rhs, params ICodeSet[] opt)
		{
			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));

			return QuickFrom(lhs.BitDisjunction(rhs, opt));
		}

		public ICodeSet Disjunction(IEnumerable<ICodeSet> sets)
		{
            Contract.Requires<ArgumentNullException>(sets != null);
			Contract.Requires<ArgumentException>(sets.Count() >= 2);

			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));

			return QuickFrom(sets.BitDisjunction());
		}

		#endregion

		#region Difference bits.Not(((a-b)-c)-d...)

		public ICodeSet Difference(ICodeSet lhs, ICodeSet rhs, params ICodeSet[] opt)
		{
            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));

			return QuickFrom(lhs.BitDifference(rhs, opt));
		}

		public ICodeSet Difference(IEnumerable<ICodeSet> sets)
		{
			Contract.Requires<ArgumentNullException>(sets != null);
			Contract.Requires<ArgumentException>(sets.Count() >= 2);

			Contract.Ensures(Theory.Result(this, Contract.Result<ICodeSet>()));

			return QuickFrom(sets.BitDifference());
		}

		#endregion

		#region Complement bits.Not()

		public ICodeSet Complement(ICodeSet that)
		{
            Contract.Requires<ArgumentNullException>(that != null);
            Contract.Ensures (Theory.Result (this, Contract.Result<ICodeSet> ()));

			return QuickFrom(that.BitComplement());
		}

		#endregion

		#endregion

		#region Invariant

		[ContractInvariantMethod]
		private void Invariant()
		{
			Contract.Invariant(dictionary.IsNot(null)); 
			Contract.Invariant(!dictionary.Contains(CodeSetNone.Singleton)); 
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
					success.Assert(chars.Distinct().OrderBy(item => (item)).Cast<Code>().SequenceEqual(result));
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


