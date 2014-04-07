﻿// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using DD.Text;

namespace DD.Collections
{
	public class Factory {

		#region Ctor

		public Factory() {
			outputDictionary = new ICodeSetDictionary();
		}

		#endregion

		#region Fields

		private readonly ICodeSetDictionary outputDictionary;

		#endregion

		#region From items

		public ICodeSet From (string utf16)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return string.IsNullOrEmpty(utf16) ? CodeSetNull.Singleton : From(utf16.Decode()); 
		}

		public ICodeSet From (params char[] chars)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return chars.IsNullOrEmpty() ? CodeSetNull.Singleton : From(chars.ToValues());
		}

		public ICodeSet From (IEnumerable<char> chars)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return chars.IsNullOrEmpty() ? CodeSetNull.Singleton : From(chars.ToValues());
		}

		public ICodeSet From (IEnumerable<Code> codes)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return codes.IsNullOrEmpty() ? CodeSetNull.Singleton : From(codes.ToValues());
		}

		public ICodeSet From (params int[] values)
		{
			Contract.Requires<ArgumentException> (values.IsNullOrEmpty() || Contract.ForAll(values, value => value.HasCodeValue()));

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return values.IsNullOrEmpty() ? CodeSetNull.Singleton : From(BitSetArray.From(values));
		}

		public ICodeSet From (IEnumerable<int> values)
		{
			Contract.Requires<ArgumentException> (values.IsNullOrEmpty() || Contract.ForAll(values, value => value.HasCodeValue()));

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return values.IsNullOrEmpty() ? CodeSetNull.Singleton : From(BitSetArray.From(values));
		}

		public ICodeSet From (BitSetArray bits)
		{
			Contract.Requires<ArgumentException> (bits.Is(null) || bits.Length <= Code.MaxCount || bits.Last <= Code.MaxValue);

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			ICodeSet key = bits.Reduce();
			if (!outputDictionary.Find(ref key)) {
				outputDictionary.Add (key);
			}
			return key;
		}

		#endregion

		#region Union bits.Or(a,b,c...)

		public ICodeSet Union (params ICodeSet[] sets)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From(sets.BitUnion());
		}

		public ICodeSet Union (IEnumerable<ICodeSet> sets)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From(sets.BitUnion());
		}

		#endregion
		
		#region Intersection bits.And(((a,b),c),d...)
		
		public ICodeSet Intersection (params ICodeSet[] sets)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From(sets.BitIntersection());
		}

		public ICodeSet Intersection (IEnumerable<ICodeSet> sets)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From(sets.BitIntersection());
		}

		#endregion
		
		#region Disjunction bits.Xor(((a,b),c),d...)

		public ICodeSet Disjunction (params ICodeSet[] sets)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From(sets.BitDisjunction());
		}

		public ICodeSet Disjunction (IEnumerable<ICodeSet> sets)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From(sets.BitDisjunction());
		}

		#endregion

		#region Difference bits.Not(((a-b)-c)-d...)

		public ICodeSet Difference (params ICodeSet[] sets)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From(sets.BitDifference());
		}

		public ICodeSet Difference (IEnumerable<ICodeSet> sets)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From(sets.BitDifference());
		}

		#endregion
		
		#region Complement
		
		public ICodeSet Complement (ICodeSet self)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (outputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From(self.BitComplement());
		}
		
		#endregion
	}


	/// <summary>Produces ICodeSet's
	/// <para>If OutputDictionary is not null, methods in this class never return duplicate ICodeSet</para>
	/// <para>(for all factored ICodeSet =&gt; ReferenceEquals=&gt;(Value)Equals=&gt;SetEquals=&gt;SequenceEqual)</para>
	/// <para>If InputDictionary is not null, set arguments must be members of that dictionary</para>
	/// <para>OutputDictionary and InputDictionary can be same instance</para>
	/// </summary>
	public static class ICodeSetFactory
	{
		public static ICodeSetDictionary OutputDictionary = null;
		public static ICodeSetDictionary InputDictionary = null;
		
		#region From items To ICodeSet Factory

		public static ICodeSet From (this string utf16) {
			Contract.Requires<ArgumentNullException> (!utf16.Is(null));
			Contract.Requires<ArgumentException> (utf16 != string.Empty);

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From (utf16.Decode());
		}

		public static ICodeSet From (char req, params char[] opt)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			List<Code> codeList;
			if (opt.Length > 0) { // keyword "params" is never null
				codeList = new List<Code>(1 + opt.Length);
				codeList.Add (req);
				foreach (Code code in opt) {
					codeList.Add (code);
				}
			} else { // type char is never null
				codeList = new List<Code>(1);
				codeList.Add (req);
			}
			return From (codeList);
		}

		public static ICodeSet From (this IEnumerable<char> chars)
		{
			Contract.Requires<ArgumentNullException> (!chars.Is(null));
			Contract.Requires<ArgumentException> (!chars.IsEmpty());

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			//return From (chars.Cast<Code>()); //InvalidCastException
			//return From (chars.OfType<Code>()); //ArgumentException
			var codeList = new List<Code>();
			foreach (Code code in chars) {
				codeList.Add(code);
			}
			return From (codeList);
		}

		public static ICodeSet From (Code req, params Code[] opt)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			List<Code> codeList;
			if (opt.Length > 0) { // array from keyword "params" is never null
				codeList = new List<Code>(1 + opt.Length);
				codeList.Add (req);
				codeList.AddRange (opt);
			} else { // type Code is never null
				codeList = new List<Code>(1);
				codeList.Add (req);
			}
			return From (codeList);
		}

		public static ICodeSet From (IEnumerable<Code> codes)
		{
			Contract.Requires<ArgumentNullException> (!codes.Is(null));
			Contract.Requires<ArgumentException> (!codes.IsEmpty());
			Contract.Requires<ArgumentOutOfRangeException> (InputDictionary.Is(null) || !(codes is ICodeSet) || InputDictionary.ContainsKey((ICodeSet)codes));

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From (new CodeSetBits (codes));
		}

		public static ICodeSet From (this BitSetArray bits)
		{
			Contract.Requires<ArgumentNullException> (!bits.Is(null));
			Contract.Requires<ArgumentException> (bits.Count != 0);
			Contract.Requires<ArgumentException> (bits.Last <= Code.MaxCount);

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From (new CodeSetBits(bits));;
		}

		private static ICodeSet From (CodeSetBits codeSet)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			if (OutputDictionary.Is(null)) {
				return codeSet.Reduce();
			}
			ICodeSet key = codeSet;
			if (!OutputDictionary.Find(ref key)) {
				key = codeSet.Reduce();
				OutputDictionary.Add (key);
			}
			return key;
		}

		#endregion

		#region Operations

		// ICodeSet operation names differ from ISet<T> names because
		// ICodeSet operation does not mutate any operand and returns new set,
		// unlike ISet<T> operations that mutate and return left operand.
		
		#region Union bit.Or(a,b,c...)

		public static ICodeSet Union (this ICodeSet req, params ICodeSet[] opt)
		{
			Contract.Requires<ArgumentNullException> (!req.Is(null));
			Contract.Requires<ArgumentNullException> (Contract.ForAll(opt, item => !item.Is(null)));

			Contract.Requires<ArgumentOutOfRangeException> (InputDictionary.Is(null) || InputDictionary.ContainsKey(req));
			Contract.Requires<ArgumentOutOfRangeException> (Contract.ForAll(opt, item => InputDictionary.Is(null) || InputDictionary.ContainsKey(item)));

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			List<ICodeSet> setList;
			if (opt.Length > 0) {
				setList = new List<ICodeSet>(1 + opt.Length);
				setList.Add (req);
				setList.AddRange ((IEnumerable<ICodeSet>)opt);
			} else {
				setList = new List<ICodeSet>(1);
				setList.Add (req);
			}
			return Union (setList);
		}

		public static ICodeSet Union (this IEnumerable<ICodeSet> sets)
		{
			Contract.Requires<ArgumentOutOfRangeException>
				(sets.Is(null) || Contract.ForAll(sets, item => InputDictionary.Is(null) || item.Is(null) || InputDictionary.ContainsKey(item)));

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			if (sets.Is(null) || sets.IsEmpty()) return Empty;
			int maxLength = int.MinValue;
			foreach (ICodeSet item in sets) {
				if (item.Is(null) || item.Count == 0) { continue; }
				if (item.Last > maxLength) { maxLength = item.Last; }
			}
			if (maxLength == int.MinValue) { return Empty; }
			++maxLength;
			var bits = BitSetArray.Size (maxLength);
			// TEST: foreach union takes same time as time to create another BitSetArray b (and then a.Or(b))
			foreach (ICodeSet item in sets) {
				if (item.Is(null) || item.IsEmpty()) { continue; }
				foreach (Code code in item) {
					bits.Set (code, true);
				}
			}
			return From (bits);
		}

		#endregion
		
		#region Intersection bit.And(((a,b),c),d...)
		
		public static ICodeSet Intersection (this ICodeSet req, params ICodeSet[] opt)
		{
			Contract.Requires<ArgumentNullException> (!req.Is(null));
			Contract.Requires<ArgumentNullException> (Contract.ForAll(opt, item => !item.Is(null)));

			Contract.Requires<ArgumentOutOfRangeException> (InputDictionary.Is(null) || InputDictionary.ContainsKey(req));
			Contract.Requires<ArgumentOutOfRangeException> (Contract.ForAll(opt, item => InputDictionary.Is(null) || InputDictionary.ContainsKey(item)));

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			List<ICodeSet> setList;
			if (opt.Length > 0) {
				setList = new List<ICodeSet> (1 + opt.Length);
				setList.Add (req);
				setList.AddRange (opt);
			} else {
				setList = new List<ICodeSet> (1);
				setList.Add (req);
			}
			return Intersection (setList);
		}

		public static ICodeSet Intersection (this IEnumerable<ICodeSet> sets)
		{
			Contract.Requires<ArgumentOutOfRangeException>
				(sets.Is(null) || Contract.ForAll(sets, item => InputDictionary.Is(null) || item.Is(null) || InputDictionary.ContainsKey(item)));

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			if (sets.Is(null) || sets.IsEmpty()) { return Empty; }

			int maxLength = int.MinValue;
			foreach (ICodeSet codeSet in sets) {
				if (codeSet.Is(null) || codeSet.Count == 0) { return Empty; }
				if (codeSet.Last > maxLength) { maxLength = codeSet.Last; }
			}
			Contract.Assert (maxLength != int.MinValue);
			++maxLength;

			var a = BitSetArray.Size (maxLength);
			var b = BitSetArray.Size (maxLength);
			bool first = true;
			foreach (ICodeSet codeSet in sets) {
				Contract.Assert (codeSet.Count != 0);
				if (first) {
					first = false;
					foreach (Code code in codeSet) {
						a.Set (code, true);
					}
				}
				else {
					b.Clear();
					foreach (Code code in codeSet) {
						b.Set (code, false);
					}
					a.IntersectWith(b);
					if (a.Count == 0) { return Empty; }
				}
			}
			return From (a);
		}

		#endregion
		
		#region Disjunction xor(((a,b),c),d...)

		public static ICodeSet Disjunction (this ICodeSet req, params ICodeSet[] opt)
		{
			Contract.Requires<ArgumentNullException> (!req.Is(null));
			Contract.Requires<ArgumentNullException> (Contract.ForAll(opt, item => !item.Is(null)));

			Contract.Requires<ArgumentOutOfRangeException> (InputDictionary.Is(null) || InputDictionary.ContainsKey(req));
			Contract.Requires<ArgumentOutOfRangeException> (Contract.ForAll(opt, item => InputDictionary.Is(null) || InputDictionary.ContainsKey(item)));

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			List<ICodeSet> setList;
			if (opt.Length > 0) {
				setList = new List<ICodeSet> (1 + opt.Length);
				setList.Add (req);
				setList.AddRange (opt);
			} else {
				setList = new List<ICodeSet> (1);
				setList.Add (req);
			}
			return Disjunction (setList);
		}

		public static ICodeSet Disjunction (this IEnumerable<ICodeSet> sets)
		{
			Contract.Requires<ArgumentOutOfRangeException>
				(sets.Is(null) || Contract.ForAll(sets, item => InputDictionary.Is(null) || item.Is(null) || InputDictionary.ContainsKey(item)));

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			if (sets.Is(null) || sets.IsEmpty()) { return Empty; }
			int maxLength = int.MinValue;
			foreach (ICodeSet codeSet in sets) {
				if (codeSet.Is(null) || codeSet.Count == 0) { continue; }
				if (codeSet.Last > maxLength) { maxLength = codeSet.Last; }
			}
			++maxLength;
			var a = BitSetArray.Size (maxLength);
			var b = BitSetArray.Size (maxLength);
			bool first = true;
			foreach (ICodeSet codeSet in sets) {
				if (first) {
					first = false;
					foreach (Code code in codeSet) {
						a.Set (code, true);
					}
				}
				else {
					b.Clear();
					foreach (Code code in codeSet) {
						b.Set (code, false);
					}
					a.SymmetricExceptWith(b);
				}
			}
			return From (a);
		}

		#endregion

		#region Difference (((a-b)-c)-d...)

		public static ICodeSet Difference (this ICodeSet req, params ICodeSet[] opt)
		{
			Contract.Requires<ArgumentNullException> (!req.Is(null));
			Contract.Requires<ArgumentNullException> (Contract.ForAll(opt, item => !item.Is(null)));

			Contract.Requires<ArgumentOutOfRangeException> (InputDictionary.Is(null) || InputDictionary.ContainsKey(req));
			Contract.Requires<ArgumentOutOfRangeException> (Contract.ForAll(opt, item => InputDictionary.Is(null) || InputDictionary.ContainsKey(item)));

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			List<ICodeSet> setList;
			if (opt.Length > 0) {
				setList = new List<ICodeSet> (1 + opt.Length);
				setList.Add (req);
				setList.AddRange (opt);
			} else {
				setList = new List<ICodeSet> (1);
				setList.Add (req);
			}
			return Difference (setList);
		}

		public static ICodeSet Difference (this IEnumerable<ICodeSet> sets)
		{
			Contract.Requires<ArgumentOutOfRangeException>
				(sets.Is(null) || Contract.ForAll(sets, item => InputDictionary.Is(null) || item.Is(null) || InputDictionary.ContainsKey(item)));

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			if (sets.Is(null) || sets.IsEmpty()) { return Empty; }
			if (sets.First().Is(null) || sets.First().Count == 0) { return Empty; }
			
			int maxLength = int.MinValue;
			foreach (ICodeSet codeSet in sets) {
				if (codeSet.Last > maxLength) { maxLength = codeSet.Last; }
			}
			++maxLength;
			var bits = BitSetArray.Size (maxLength);
			bool first = true;
			// TEST: foreach item difference takes same time as time to create BitSetArray b (and then a.Not(b))
			foreach (ICodeSet codeSet in sets) {
				if (first) {
					first = false;
					foreach (Code code in codeSet) {
						bits.Set (code, true);
					}
				}
				else {
					foreach (Code code in codeSet) {
						bits.Set (code, false);
						if (bits.Count == 0) { return Empty; }
					}
				}
			}
			return From (bits);
		}

		#endregion
		
		#region Complement
		
		public static ICodeSet Complement (this ICodeSet self)
		{
			Contract.Requires<ArgumentOutOfRangeException> (InputDictionary.Is(null) || InputDictionary.ContainsKey(self));

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			if (self.Is(null) || ((self.Length - self.Count) == 0)) return Empty;

			BitSetArray compact = self.ToCompact();
			compact.Not();
			Contract.Assert (compact.Count != 0);
			var complement = BitSetArray.Size (self.Last);
			foreach (var item in compact) {
				complement.Set(item + self.First);
			}
			return From (complement);
		}
		
		#endregion
		
		#endregion
		
		#region Empty ICodeSet

		[Pure] public static ICodeSet Empty {
			get {
				return CodeSetNull.Singleton;
			}
		}

		#endregion

	}
}
