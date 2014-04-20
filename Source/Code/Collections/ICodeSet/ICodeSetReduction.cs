﻿// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Diagnostics.Contracts;
using DD.Diagnostics;

namespace DD.Collections.ICodeSet
{
	/// <summary>
	/// Description of ICodeSetReduction.
	/// </summary>
	public static class ICodeSetReduction
	{
		#region Reduction

		[Pure] private static ICodeSet ReducePartOne(this BitSetArray self, int offset)
		{
			Contract.Requires<ArgumentOutOfRangeException>(self.IsNullOrEmpty() || (self.First + offset).HasCodeValue());
			Contract.Requires<ArgumentOutOfRangeException>(self.IsNullOrEmpty() || (self.Last + offset).HasCodeValue());

			Contract.Ensures(Contract.Result<ICodeSet>().Is(null) || !(Contract.Result<ICodeSet>() is CodeSetBits));

			#region Null
			if (self.IsNullOrEmpty()) {
				return CodeSetNull.Singleton;
			}
			#endregion

			#region Unit
			if (self.Count == ICodeSetService.UnitCount) {
				return new Code((int)self.First + offset);
			}
			#endregion

			#region Pair
			if (self.Count == ICodeSetService.PairCount) {
				return new CodeSetPair((int)self.First + offset, (int)self.Last + offset);
			}
			#endregion

			#region Full
			if (self.Count == self.Span()) {
				return new CodeSetFull((int)self.First + offset, (int)self.Last + offset);
			}
			#endregion

			#region List
			if (self.Count <= ICodeSetService.ListMaxCount) {
				// List items spread over more than one unicode plane. (better than CodeSetWide)
				if (self.First.UnicodePlane() != self.Last.UnicodePlane()) {
					return new CodeSetList(self.ToCodes(offset));
				}
				// List space less than 1/4 of Bits space. (better than CodeSetPage)
				// list space in bytes == Count * sizeof(int)
				// bits space in bytes == BitSetArray.GetLongArrayLength(self.Span())*sizeof(long)
				if ((self.Count * sizeof(int)) < (BitSetArray.GetLongArrayLength(self.Span()) * sizeof(long) / 4)) {
					return new CodeSetList(self.ToCodes(offset));
				}
			}
			#endregion

			return null;

		}

		[Pure] private static ICodeSet ReducePartTwo(this BitSetArray self, int offset)
		{
			Contract.Requires<ArgumentNullException>(!self.IsNull());
			Contract.Requires<ArgumentEmptyException>(self.Count != 0);
			Contract.Requires<ArgumentOutOfRangeException>((self.First + offset).HasCodeValue());
			Contract.Requires<ArgumentOutOfRangeException>((self.Last + offset).HasCodeValue());

			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures(Contract.Result<ICodeSet>() is CodeSetPage || Contract.Result<ICodeSet>() is CodeSetWide);

			if (((Code)((int)self.First + offset)).UnicodePlane() == ((Code)((int)self.Last + offset)).UnicodePlane()) {
				return new CodeSetPage(self, offset);
			}
			return new CodeSetWide(self, offset);
		}
		
		[Pure] internal static ICodeSet Reduce(this BitSetArray self, int offset = 0)
		{
			Contract.Requires<ArgumentOutOfRangeException>(self.IsNullOrEmpty() || (self.First + offset).HasCodeValue());
			Contract.Requires<ArgumentOutOfRangeException>(self.IsNullOrEmpty() || (self.Last + offset).HasCodeValue());

			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));
			Contract.Ensures(Contract.Result<ICodeSet>().Theory());
			
			var retSet = self.ReducePartOne(offset);

			if (retSet.Is(null)) {

				Contract.Assert(self.IsNot(null)); // not null
				Contract.Assert(self.Count > ICodeSetService.PairCount); // not Code, not Pair
				Contract.Assert(self.Span() != self.Count); // not Full -> has complement items
				
				if (self.Span() <= ICodeSetService.NoDiffLength) {
					retSet = self.ReducePartTwo(offset);
				} else {
					// create complement
					int start = (int)self.First;
					int final = (int)self.Last;
					var complement = BitSetArray.Size(self.Length);
					foreach (var item in self.Complement()) {
						if (item.InRange(start, final)) {
							complement.Set(item);
						}
					}
	
					Contract.Assert(complement.Count != 0);
	
					var notSet = complement.ReducePartOne(offset);
					if (notSet.IsNot(null)) {
						// if reduced to Code/Pair/Full/List, return DiffSet
						retSet = new CodeSetDiff(
							new CodeSetFull((int)self.First + offset, (int)self.Last + offset),
							notSet);
					} else {
						// not reduced, check size
						if (complement.Span() < (self.Span() / 4)) {
							// can save at least 3/4 of space
							retSet = new CodeSetDiff(
								new CodeSetFull((int)self.First + offset, (int)self.Last + offset),
								complement.ReducePartTwo(offset));
						} else {
							// final choice
							retSet = self.ReducePartTwo(offset);
						}
					}
				}
			}
			
			return retSet;
		}

		[Pure] private static bool Theory(this ICodeSet self)
		{
			// disable once ConvertToConstant.Local
			Success success = true;
			success.Assert (!self.IsNull());
			switch (self.Count) {
				case ICodeSetService.NullCount:
					success.Assert (self is CodeSetNull);
					break;
				case ICodeSetService.UnitCount:
					success.Assert (self is Code);
					break;
				case ICodeSetService.PairCount:
					success.Assert (self is CodeSetPair);
					break;
				default:
					success.Assert (self.Count > ICodeSetService.PairCount);

					if (self.Count == self.Length) {
						success.Assert (self is CodeSetFull);
					}
					else if (self is CodeSetList) {
						success.Assert (self.Count <= ICodeSetService.ListMaxCount);
					}
					else if (self is CodeSetDiff) {
						success.Assert (self.Length > ICodeSetService.NoDiffLength);
					}
					else {
						success.Assert (self is CodeSetPage || self is CodeSetWide);
					}
					break;
			}
			return success;
		}

		#endregion

	}
}
