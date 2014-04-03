// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Diagnostics.Contracts;

namespace DD.Collections
{
	/// <summary>
	/// Description of ICodeSetReduction.
	/// </summary>
	public static class ICodeSetReduction
	{
		#region Reduction

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
			Contract.Ensures (!(Contract.Result<ICodeSet>() is CodeSetBits));

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
			Contract.Ensures(!(Contract.Result<ICodeSet>() is CodeSetBits));
			
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
