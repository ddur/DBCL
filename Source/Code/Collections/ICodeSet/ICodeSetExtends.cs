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
	/// <summary>
	/// Description of ICodeSetExtends.
	/// </summary>
	public static class ICodeSetExtends
	{
        private const bool ThisMethodHandlesNull = true;

		[Pure] public static bool IsCompact(this BitSetArray bits) {
            Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

			if (bits.IsNull()) return false;
			
			if (bits.Count == 0) {
				return bits.Length == 0; 
			}
			else {
				return bits[0] && bits[bits.Length-1];
			}
        }
        
        [Pure] public static BitSetArray ToCompact(this BitSetArray self) {
            Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

            Contract.Ensures (Contract.Result<BitSetArray>().IsNot(null));
            Contract.Ensures (((self.IsNull() || self.Count == 0)
                               && Contract.Result<BitSetArray>().Count == 0)
                              ||
                              ( Contract.Result<BitSetArray>().Count == self.Count()
                               && Contract.ForAll(self, item => Contract.Result<BitSetArray>()[item-(int)self.First])
                              ));

            if (self.IsNull() || self.Count == 0) return new BitSetArray();
            int selfFirst = (int)self.First;
            BitSetArray ret = new BitSetArray(1 + (int)self.Last - selfFirst);
            foreach (int item in self) {
                ret.Set (item - selfFirst, true);
            }

            // first and last bit set == compact
            Contract.Assert (ret[0]);
            Contract.Assert (ret[ret.Length-1]);

            return ret;
        }

        [Pure] public static IEnumerable<Code> FromCompact (this BitSetArray compact, int start) {
            Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);
            Contract.Requires<ArgumentException> (start.HasCodeValue());
            Contract.Requires<ArgumentException> ((compact.IsNull()) || ((int)compact.First+start).HasCodeValue());
            Contract.Requires<ArgumentException> ((compact.IsNull()) || ((int)compact.Last+start).HasCodeValue());

            Contract.Ensures (Contract.Result<IEnumerable<Code>>().IsNot(null));
            Contract.Ensures (Contract.Result<IEnumerable<Code>>().Count() == compact.Count);

            if (!compact.IsNull() && compact.Count != 0) {
                foreach (int item in compact) {
                    yield return (Code)(item+start);
                }
            }
        }

	}
}
