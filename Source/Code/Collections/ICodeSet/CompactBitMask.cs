// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DD.Collections.ICodeSet
{
	public struct CompactBitMask
	{
		/// <summary>
		/// Int32 Bit Mask Array
		/// </summary>
		public readonly IReadOnlyCollection<int> Masks;

		public readonly int Start;

		public readonly int Final;

		public readonly int Count;

        /// <summary>
        /// For internal use only (Text.UniCode.Category.generated.cs)
        /// </summary>
        /// <param name="array"></param>
        /// <param name="start"></param>
        /// <param name="final"></param>
        /// <param name="count"></param>
		public CompactBitMask(int[] array, int start, int final, int count)
		{
            Contract.Requires<ArgumentNullException>(array.IsNot(null));
            Contract.Requires<ArgumentEmptyException> (array.Length != 0);

            Contract.Requires<ArgumentException> (((Code.MaxValue >> 5) + 1) >= array.Length);
            Contract.Requires<ArgumentException> ((array[0] & 1) != 0);
            Contract.Requires<ArgumentException> (array[array.Length - 1] != 0);

            Contract.Requires<ArgumentException>(start.HasCodeValue());
            Contract.Requires<ArgumentException>(final.HasCodeValue());
            Contract.Requires<ArgumentException>(start <= final);
            Contract.Requires<ArgumentException>(count > 0);
            Contract.Requires<ArgumentException>(count <= (final - start + 1));

            Contract.Requires<ArgumentException> (final == array.IsCompactLast () + start);
            Contract.Requires<ArgumentException> (count == BitSetArray.CountOnBits(array));

            Masks = ImmutableArray.Create<int>(array);
			Start = start;
			Final = final;
			Count = count;
		}
	}
}


