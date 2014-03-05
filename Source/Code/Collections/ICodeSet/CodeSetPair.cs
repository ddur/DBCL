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

namespace DD.Collections
{
	/// <summary>Set of two items (codes)
	/// <remarks>Space efficient, O(k)</remarks>
	/// </summary>
	public sealed class CodeSetPair : CodeSet {

		#region Ctor

		internal CodeSetPair (Code low, Code high) {
			Contract.Requires<ArgumentException> (low < high);

			Contract.Ensures (Theory.Construct(low, high, this));

			this.start = low;
			this.final = high;
		}

		#endregion

		#region Fields

		private readonly int start;
		private readonly int final;

		#endregion

		#region ICodeSet

		[Pure] public override bool this [Code code] {
			get { return code == this.start || code == this.final; }
		}

		[Pure] public override int Count {
			get { return ICodeSetService.PairCount; }
		}

		[Pure] public override Code First {
			get { return this.start; }
		}

		[Pure] public override Code Last {
			get { return this.final; }
		}

		[Pure] public override IEnumerator<Code> GetEnumerator() {
			yield return this.start;
			yield return this.final;
		}

		#endregion
		
		#region Invariant
		
		[ContractInvariantMethod]
		private void Invariant () {
			Contract.Invariant (Theory.Invariant(this));
		}

		#endregion
		
		private static class Theory {

			[Pure] public static bool Construct(int low, int high, CodeSetPair self) {
				// disable once ConvertToConstant.Local
				Success success = true;
				
				// input -> private
				success.Assert (self.start == low);
				success.Assert (self.final == high);
				success.Assert (self[low]);
				success.Assert (self[high]);

				return success;
			}
			
			[Pure] public static bool Invariant (CodeSetPair self) {
				// disable once ConvertToConstant.Local
				Success success = true;
				
				// private
				success.Assert (self.start.HasCodeValue ());
				success.Assert (self.final.HasCodeValue ());
				success.Assert (self.start < self.final);

				// public <- private
				success.Assert (self.First == self.start);
				success.Assert (self.Last == self.final);
				
				// constraints
				success.Assert (self.Count == ICodeSetService.PairCount);

				return success;
			}
		}
	}
}
