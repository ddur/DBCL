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
	/// <summary>Produces ICodeSet's
	/// <para>If OutputDictionary is not null, methods in this class never return duplicate ICodeSet</para>
	/// <para>(for all factored ICodeSet =&gt; ReferenceEquals=&gt;(Value)Equals=&gt;SetEquals=&gt;SequenceEqual)</para>
	/// <para>If InputDictionary is not null, set arguments must be members of that dictionary</para>
	/// <para>OutputDictionary and InputDictionary can be same instance</para>
	/// </summary>
	public static class ICodeSetStaticFactory
	{
		public static ICodeSetDictionary OutputDictionary = null;
		
		#region From items To ICodeSet

		public static ICodeSet From (this string utf16) {
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return string.IsNullOrEmpty(utf16) ? CodeSetNull.Singleton : From(utf16.Decode());
		}

		public static ICodeSet From (char req, params char[] opt)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			List<Code> codeList;
			if (opt.Length > 0) { // keyword "params" never returns null
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
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return chars.IsNullOrEmpty() ? CodeSetNull.Singleton : From(chars.ToCodes());
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
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return codes.IsNullOrEmpty() ? CodeSetNull.Singleton : From (BitSetArray.From (codes.ToValues()));
		}

		public static ICodeSet From (this BitSetArray bits)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			if (bits.IsNullOrEmpty()) {
				return CodeSetNull.Singleton;
			}
			if (OutputDictionary.Is(null)) {
				return bits.Reduce();
			}
			ICodeSet key = new CodeSetWrap(bits);
			if (!OutputDictionary.Find(ref key)) {
				key = bits.Reduce();
				OutputDictionary.Add(key);
			}
			return key;
			
		}

		#endregion

		#region Operations

		
		#region Union bit.Or(a,b,c...)

		public static ICodeSet Union (this ICodeSet self, ICodeSet that, params ICodeSet[] more)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From (self.BitUnion(that, more)); 
		}

		public static ICodeSet Union (this IEnumerable<ICodeSet> sets)
		{
			Contract.Requires (!sets.IsNull());
			Contract.Requires (sets.Count() >= 2);

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From (sets.BitUnion());
		}

		#endregion
		
		#region Intersection bit.And(((a,b),c),d...)
		
		public static ICodeSet Intersection (this ICodeSet self, ICodeSet that, params ICodeSet[] more)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From (self.BitIntersection(that, more));
		}

		public static ICodeSet Intersection (this IEnumerable<ICodeSet> sets)
		{
			Contract.Requires (!sets.IsNull());
			Contract.Requires (sets.Count() >= 2);

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From (sets.BitIntersection());
		}

		#endregion
		
		#region Disjunction xor(((a,b),c),d...)

		public static ICodeSet Disjunction (this ICodeSet self, ICodeSet that, params ICodeSet[] more)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From (self.BitDisjunction(that, more)); 
		}

		public static ICodeSet Disjunction (this IEnumerable<ICodeSet> sets)
		{
			Contract.Requires (!sets.IsNull());
			Contract.Requires (sets.Count() >= 2);

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From (sets.BitDisjunction());
		}

		#endregion

		#region Difference (((a-b)-c)-d...)

		public static ICodeSet Difference (this ICodeSet self, ICodeSet that, params ICodeSet[] more)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From (self.BitDifference(that, more));
		}

		public static ICodeSet Difference (this IEnumerable<ICodeSet> sets)
		{
			Contract.Requires (!sets.IsNull());
			Contract.Requires (sets.Count() >= 2);

			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From (sets.BitDifference());
		}

		#endregion
		
		#region Complement
		
		public static ICodeSet Complement (this ICodeSet self)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

			return From (self.BitComplement());
		}
		
		#endregion
		
		#endregion
		
	}
}
