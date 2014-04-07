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
	/// <summary>Provides conversion, relation and predicate services
	/// <remarks>All public except methods returning ICodeSet</remarks>
	/// </summary>
	public static class ICodeSetService {
		
		private const bool ThisMethodHandlesNull = true;

		public const int UnitCount = 1;
		public const int PairCount = 2;
		public const int ListMaxCount = 16; // NOTE: log(16) == 4 tests, TODO: check speed on 32 and 64

		public const int NullStart = -1;
		public const int NullFinal = -2;

		#region To Service

		public static IEnumerable<Code> ToCodes (this IEnumerable<char> chars) {
			if (chars.IsNot(null)) {
				foreach (Code code in chars) {
					yield return code; 
				}
			}
		}

		public static IEnumerable<Code> ToCodes (this IEnumerable<int> ints, int offset = 0) {
			if (ints.IsNot(null)) {
				if (offset == 0) {
					foreach (var code in ints) {
						if (code.HasCodeValue()) {
							yield return (Code)code;
						}
					}
				}
				else {
					int shifted;
					foreach (var code in ints) {
						shifted = code + offset;
						if (shifted.HasCodeValue()) {
							yield return (Code)shifted;
						}
					}
				}
			}
		}

		public static IEnumerable<int> ToValues (this IEnumerable<Code> codes) {
			if (codes.IsNot(null)) {
				foreach (Code code in codes) {
					yield return code.Value; 
				}
			}
		}

		public static IEnumerable<int> ToValues (this IEnumerable<char> codes) {
			if (codes.IsNot(null)) {
				foreach (Code code in codes) {
					yield return code.Value; 
				}
			}
		}

		[Pure] public static bool IsCompact(this BitSetArray self)
		{
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
		[Pure] public static BitSetArray ToBitSetArray(this ICodeSet self)
		{
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
		[Pure] internal static BitSetArray ToCompact(this ICodeSet self)
		{
			Contract.Ensures (Contract.Result<BitSetArray>().IsNot(null));
			Contract.Ensures (Contract.Result<BitSetArray>().IsCompact());

			if (self.IsNull() || self.Count == 0) return new BitSetArray();
			
			var bits = self as CodeSetPage;
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
		
		#endregion

		#region Properties

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
