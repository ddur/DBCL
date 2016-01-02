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

namespace DD.Collections.ICodeSet {

    /// <summary>Wraps around compact bitmask-array</summary>
    /// <remarks>Cannot be empty, contains at least 1, up to <see cref="Code.MaxCodeCount">Code.MaxCodeCount-1</see> codes</remarks>
    [Serializable]
    public sealed class CodeSetMask : CodeSet {

        #region Ctor

        /// <summary>
        /// Mask from not-null-or-empty params Code[]
        /// </summary>
        /// <param name="codes"></param>
        /// <returns>CodeSetMask</returns>
        public static CodeSetMask From (params Code[] codes) {
            Contract.Requires<ArgumentNullException> (codes.IsNot (null));
            Contract.Requires<ArgumentEmptyException> (codes.Length > 0);
            Contract.Ensures (Contract.Result<CodeSetMask> ().IsNot (null));

            return new CodeSetMask ((IEnumerable<Code>)codes);
        }

        /// <summary>
        /// Mask from not-null-or-empty IEnumerable&lt;Code&gt;
        /// </summary>
        /// <param name="codes">IEnumerable&lt;Code&gt;</param>
        /// <returns>CodeSetMask</returns>
        public static CodeSetMask From (IEnumerable<Code> codes) {
            Contract.Requires<ArgumentNullException> (codes.IsNot (null));
            Contract.Requires<ArgumentEmptyException> (!codes.IsEmpty ());
            Contract.Ensures (Contract.Result<CodeSetMask> ().IsNot (null));

            return new CodeSetMask (codes);
        }

        /// <summary>
        /// Clones CodeSetMask at same or another offset
        /// </summary>
        /// <param name="mask"></param>
        /// <param name="offset"></param>
        /// <returns>CodeSetMask</returns>
        public static CodeSetMask From (CodeSetMask mask, int offset = 0) {
            Contract.Requires<ArgumentNullException> (mask.IsNot (null));
            Contract.Requires<ArgumentException> (offset.HasCodeValue());
            Contract.Requires<ArgumentException> (((long)mask.Last + offset) <= Code.MaxValue);
            Contract.Ensures (Contract.Result<CodeSetMask> ().IsNot (null));

            return new CodeSetMask (mask, offset);
        }

        internal static CodeSetMask From (int[] mask, int offset = 0) {
            Contract.Requires<ArgumentNullException> (mask.IsNot (null));
            Contract.Requires<ArgumentEmptyException> (mask.Length != 0, "Empty Mask Array");
            Contract.Requires<ArgumentException> (((Code.MaxValue >> 5) + 1) >= mask.Length, "Too large Mask Array");
            Contract.Requires<ArgumentException> ((mask[0] & 1) != 0, "First bit must be set");
            Contract.Requires<ArgumentException> (mask[mask.Length-1] != 0, "mask.Last() must be != 0");
            Contract.Requires<ArgumentException> (mask.IsCodeCompactLast().HasCodeValue(), "Last bit has no Code.Value");
            Contract.Requires<ArgumentException> (offset.HasCodeValue());
            Contract.Requires<ArgumentException> ((mask.IsCodeCompactLast() + (long)offset) <= Code.MaxValue, "Last bit + offset has no Code.Value");
            Contract.Ensures (Contract.Result<CodeSetMask> ().IsNot (null));

            return new CodeSetMask (mask, offset);
        }

        private CodeSetMask (IEnumerable<Code> codes) {
            Contract.Requires<ArgumentNullException> (codes.IsNot (null));
            Contract.Requires<ArgumentEmptyException> (!codes.IsEmpty ());

            var iCodeSet = codes as ICodeSet;
            if (!iCodeSet.Is (null)) {
                start = iCodeSet.First;
                final = iCodeSet.Last;
            }
            else {
                foreach (var code in codes) {
                    if (start > code) {
                        start = code;
                    }
                    if (final < code) {
                        final = code;
                    }
                }
            }
            Contract.Assert (start <= final);
            this.sorted = new int[BitSetArray.GetIntArrayLength (1 + this.final - this.start)];
            int item = 0;
            int index = 0;
            int mask = 0;
            foreach (var code in codes) {
                item = code.Value - start.Value;
                index = item >> ShiftRightBits;
                mask = 1 << (item & ShiftLeftMask);
                   if ((sorted[index] & mask) == 0) {
                    sorted[index] ^= mask;
                    ++count;
                }
            }
        }

        private CodeSetMask (CodeSetMask mask, int offset = 0) {
            Contract.Requires<ArgumentNullException> (mask.IsNot (null));
            Contract.Requires<ArgumentException> (offset.HasCodeValue());
            Contract.Requires<ArgumentException> (((long)mask.Last + offset) <= Code.MaxValue);

            start = offset;
            final = offset + mask.Last;
            sorted = new int[mask.sorted.Length];
            Array.Copy (mask.sorted, sorted, sorted.Length);
        }


        private CodeSetMask (int[] mask, int offset = 0) {
            Contract.Requires<ArgumentNullException> (mask.IsNot (null));
            Contract.Requires<ArgumentEmptyException> (mask.Length != 0, "Empty Mask Array");
            Contract.Requires<ArgumentException> (((Code.MaxValue >> 5) + 1) >= mask.Length, "Too large Mask Array");
            Contract.Requires<ArgumentException> ((mask[0] & 1) != 0, "First bit must be set");
            Contract.Requires<ArgumentException> (mask[mask.Length-1] != 0, "mask.Last() must be != 0");
            Contract.Requires<ArgumentException> (mask.IsCodeCompactLast().HasCodeValue(), "Last bit has no Code.Value");
            Contract.Requires<ArgumentException> (offset.HasCodeValue());
            Contract.Requires<ArgumentException> ((mask.IsCodeCompactLast() + (long)offset) <= Code.MaxValue, "Last bit + offset has no Code.Value");

            start = offset;
            final = offset + mask.IsCodeCompactLast();
            sorted = new int[mask.Length];
            Array.Copy (mask, sorted, sorted.Length);
        }

        #endregion

        #region Fields

        private readonly Code start = Code.MaxValue;
        private readonly Code final = Code.MinValue;
        private readonly int count = 0;
        private readonly int[] sorted;
        private const int ShiftRightBits = 5;
        private const int ShiftLeftMask = 0x1F;

        #endregion

        #region ICodeSet

        [Pure]
        public override bool this[Code code] {
            get {
                if (code.Value.InRange (start.Value, final.Value)) {
                    int item = code.Value - start.Value;
                    return (sorted[item >> ShiftRightBits] & 1u << (item & ShiftLeftMask)) != 0;
                }
                return false;
            }
        }

        [Pure]
        public override int Count {
            get {
                return count;
            }
        }

        [Pure]
        public override int Length {
            get {
                return 1 + final - start;
            }
        }

        [Pure]
        public override Code First {
            get {
                return this.start;
            }
        }

        [Pure]
        public override Code Last {
            get {
                return this.final;
            }
        }

        [Pure]
        public override IEnumerator<Code> GetEnumerator () {
            for (int i = start; i <= final; i++) {
                if (this[i]) {
                    yield return i;
                }
            }
        }

        #endregion
    }
}
