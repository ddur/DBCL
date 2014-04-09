// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Diagnostics.Contracts;

namespace DD.Collections.ICodeSet
{
	/// <summary>
	/// Description of ICodeSetReduction.
	/// </summary>
	public static class ICodeSetReduction
	{
		#region Reduction

		[Pure] private static ICodeSet ReducePartOne (this BitSetArray bitSet, int offset)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().Is(null)||!(Contract.Result<ICodeSet>() is CodeSetBits));

			#region Null
			if (bitSet.IsNullOrEmpty()) {
				return CodeSetNull.Singleton;
			}
			#endregion

			#region Unit
			else if (bitSet.Count == ICodeSetService.UnitCount) {
				return new Code ((int)bitSet.First + offset);
			}
			#endregion

			#region Pair
			else if (bitSet.Count == ICodeSetService.PairCount) {
				return new CodeSetPair ((int)bitSet.First + offset, (int)bitSet.Last + offset);
			}
			#endregion

			#region Full
			else if (bitSet.Count == bitSet.Span()) {
				return new CodeSetFull ((int)bitSet.First + offset, (int)bitSet.Last + offset);
			}
			#endregion

			#region List
			else if (bitSet.Count <= ICodeSetService.ListMaxCount) {
				// List space less than 1/4 Bits space (bitSet.Span/8)*4
				if ((bitSet.Count * sizeof(int)) < (bitSet.Span()/2)) {
					return new CodeSetList (bitSet.ToCodes(offset));
				}
				if (((Code)bitSet.First).UnicodePlane() != ((Code)bitSet.Last).UnicodePlane()) {
					return new CodeSetList (bitSet.ToCodes(offset));
				}
			}
			#endregion
			
			return null;

		}

		[Pure] private static ICodeSet ReducePartTwo(this BitSetArray self, int offset)
		{
			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));

			if (((Code)(self.First + offset)).UnicodePlane() == ((Code)(self.Last + offset)).UnicodePlane()) {
				return new CodeSetPage(self, offset);
			} else {
				Contract.Assert(((Code)self.First).UnicodePlane() != ((Code)self.Last).UnicodePlane());
				return new CodeSetWide(self, offset);
			}
		}
		
		[Pure] internal static ICodeSet Reduce (this BitSetArray self, int offset = 0)
		{
			Contract.Requires<ArgumentException> (self.IsNull() || self.Length <= Code.MaxCount || self.Last <= Code.MaxValue);

			Contract.Ensures(Contract.Result<ICodeSet>().Theory());
			
			ICodeSet retSet = self.ReducePartOne(offset);

			if (retSet.Is(null)) {

				Contract.Assert (self.IsNot(null)); // not null
				Contract.Assert (self.Count > ICodeSetService.PairCount); // not Code, not Pair
				Contract.Assert (self.Span() != self.Count); // not Full -> has complement items 
				
				// not reduced, create complement
				int start = (int)self.First;
				int final = (int)self.Last;
				var complement = BitSetArray.Size(self.Length);
				foreach (var item in self.Complement()) {
					if (item.InRange(start, final)) {
						complement.Set(item);
					}
				}

				Contract.Assert (complement.Count != 0); 

				var notSet = complement.ReducePartOne(offset);
				if (notSet.IsNot(null)) {
					// reduced, return DiffSet
					retSet = new CodeSetDiff(
						new CodeSetFull((int)self.First + offset, (int)self.Last + offset),
						notSet);
				}
				else {
					// not reduced, check size
					if (complement.Span() < (self.Span() / 4)) {
						// can save at least 3/4 of space
						retSet = new CodeSetDiff(
							new CodeSetFull((int)self.First + offset, (int)self.Last + offset),
							complement.ReducePartTwo(offset));
					}
					else {
						// final choice
						retSet = self.ReducePartTwo(offset);
					}
				}
			}
			
			return retSet;
		}

		[Pure] private static bool Theory (this ICodeSet self) {

			if (self.Is(null)) return false;
			switch (self.Count) {
				case ICodeSetService.NullCount:
					return (self is CodeSetNull);
				case ICodeSetService.UnitCount:
					return (self is Code);
				case ICodeSetService.PairCount:
					return (self is CodeSetPair);
				default:
					Contract.Assert (self.Count > ICodeSetService.PairCount); 

					if (self.Count == self.Length) {
						return (self is CodeSetFull);
					}
					if (self.Count <= ICodeSetService.ListMaxCount) {
						return (self is CodeSetList || self is CodeSetPage);
					}
					if (self is CodeSetDiff) {
						return true;
					}
					if (self is CodeSetPage) {
						return self.First.UnicodePlane() == self.Last.UnicodePlane();
					}
					if (self is CodeSetWide) {
						return self.First.UnicodePlane() != self.Last.UnicodePlane();
					}
					return false;
			}
		}

		[Pure] private static ICodeSet ReducePartOne (this CodeSetBits bitSet)
		{
			Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));

			#region Null
			if (bitSet.Is(null) || bitSet.Count == 0) {
				return CodeSetNull.Singleton;
			}
			#endregion

			#region Unit
			else if (bitSet.Count == ICodeSetService.UnitCount) {
				return new Code (bitSet.First);
			}
			#endregion

			#region Pair
			else if (bitSet.Count == ICodeSetService.PairCount) {
				return new CodeSetPair (bitSet.First, bitSet.Last);
			}
			#endregion

			#region Full
			else if (bitSet.Count == bitSet.Length) {
				return new CodeSetFull (bitSet.First, bitSet.Last);
			}
			#endregion

			#region List
			else if (bitSet.Count <= ICodeSetService.ListMaxCount) {
				// List space less than 1/4 Bits space (bitSet.Length/8)*4
				if ((bitSet.Count * sizeof(int)) < (bitSet.Length/2)) {
					return new CodeSetList (bitSet);
				}
				if (((Code)bitSet.First).UnicodePlane() != ((Code)bitSet.Last).UnicodePlane()) {
					 return new CodeSetList (bitSet);
				}
			}
			#endregion
			
			return bitSet;

		}

		[Pure] private static ICodeSet ReducePartTwo(this CodeSetBits bitSet)
		{
			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));

			if (bitSet.First.UnicodePlane() == bitSet.Last.UnicodePlane()) {
				return new CodeSetPage(bitSet);
			} else {
				Contract.Assert(bitSet.First.UnicodePlane() != bitSet.Last.UnicodePlane());
				return new CodeSetWide(bitSet);
			}
		}
		
		[Pure] internal static ICodeSet Reduce (this CodeSetBits bitSet)
		{
			Contract.Ensures(Contract.Result<ICodeSet>().IsNot(null));
			
			ICodeSet retSet = ReducePartOne(bitSet);

			if (retSet is CodeSetBits) {

				// not reduced, try DiffSet
				var complement = bitSet.ToCompact();
				complement.Not();
				ICodeSet notSet = ReducePartOne(new CodeSetBits(complement, (int)retSet.First));
	
				if (notSet is CodeSetBits) {

					// not reduced, check size
					if (notSet.Length < (bitSet.Length / 4)) {
						// can save at least 3/4 of space
						retSet = new CodeSetDiff(
							new CodeSetFull(bitSet.First, bitSet.Last),
							ReducePartTwo(notSet as CodeSetBits));
					}
					else {
						// final choice
						retSet = ReducePartTwo(bitSet);
					}
				}
				else {
					// notSet is optimal
					retSet = new CodeSetDiff(
						new CodeSetFull(bitSet.First, bitSet.Last),
						notSet);
				}
			}
			
			return retSet;
		}

		#endregion

	}
}
