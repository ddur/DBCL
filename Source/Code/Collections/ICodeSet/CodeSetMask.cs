// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using DD.Diagnostics;

namespace DD.Collections.ICodeSet {

    /// <summary>Wraps around compact bitmask-array</summary>
    /// <remarks><para>Cannot be empty, contains at least 1, up to <see cref="Code.MaxCodeCount">Code.MaxCodeCount</see> codes</para>
    /// <para>Quick creates ICodeSet from compact int[] mask</para>
    /// <para>Quick clones from anoter CodeSetWrap</para></remarks>
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
            Contract.Ensures (Contract.Result<CodeSetMask> ().Count != 0);

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
            Contract.Ensures (Contract.Result<CodeSetMask> ().Count != 0);

            return new CodeSetMask (codes);
        }

        /// <summary>
        /// Clones CodeSetMask at offset
        /// </summary>
        /// <param name="mask"></param>
        /// <param name="offset"></param>
        /// <returns>CodeSetMask</returns>
        public static CodeSetMask From (CodeSetMask mask, int offset = 0) {
            Contract.Requires<ArgumentNullException> (mask.IsNot (null));
            Contract.Requires<ArgumentException> (offset.HasCodeValue ());
            Contract.Requires<ArgumentException> (((long)mask.Last + offset) <= Code.MaxValue);
            Contract.Ensures (Contract.Result<CodeSetMask> ().IsNot (null));
            Contract.Ensures (Contract.Result<CodeSetMask> ().Count != 0);

            return new CodeSetMask (mask, offset);
        }

        public static CodeSetMask From (int[] mask, int offset = 0) {
            Contract.Requires<ArgumentNullException> (mask.IsNot (null));
            Contract.Requires<ArgumentEmptyException> (mask.Length != 0, "Empty Mask Array");
            Contract.Requires<ArgumentException> (((Code.MaxValue >> 5) + 1) >= mask.Length, "Too large Mask Array");
            Contract.Requires<ArgumentException> ((mask[0] & 1) != 0, "First bit must be set");
            Contract.Requires<ArgumentException> (mask[mask.Length - 1] != 0, "mask[Last] must be != 0");
            Contract.Requires<ArgumentException> (offset.HasCodeValue (), "mask first bit + offset has no Code.Value");
            Contract.Requires<ArgumentException> ((mask.IsCompactLast () + offset).HasCodeValue (), "mask last bit + offset has no Code.Value");

            Contract.Ensures (Contract.Result<CodeSetMask> ().IsNot (null));

            return new CodeSetMask (mask, offset);
        }

        public static CodeSetMask From (BitSetArray bits, int offset = 0) {
            Contract.Requires<ArgumentNullException> (bits.IsNot (null));
            Contract.Requires<ArgumentEmptyException> (bits.Count != 0, "Empty BitSetArray");
            Contract.Requires<ArgumentException> ((bits.First + offset).HasCodeValue(), "First item + offset has no Code value");
            Contract.Requires<ArgumentException> ((bits.Last + offset).HasCodeValue(), "Last item + offset has no Code value");

            Contract.Ensures (Contract.Result<CodeSetMask> ().IsNot (null));

            return new CodeSetMask (bits, offset);
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
            Contract.Requires<ArgumentException> (offset.HasCodeValue ());
            Contract.Requires<ArgumentException> (((long)mask.Last + offset) <= Code.MaxValue);

            start = mask.First + offset;
            final = mask.Last + offset;
            sorted = mask.sorted;
            count = mask.Count;
        }

        private CodeSetMask (int[] mask, int offset = 0) {
            Contract.Requires<ArgumentNullException> (mask.IsNot (null));
            Contract.Requires<ArgumentEmptyException> (mask.Length != 0, "Empty Mask Array");
            Contract.Requires<ArgumentException> (((Code.MaxValue >> 5) + 1) >= mask.Length, "Too large Mask Array");
            Contract.Requires<ArgumentException> ((mask[0] & 1) != 0, "First bit must be set");
            Contract.Requires<ArgumentException> (mask[mask.Length - 1] != 0, "mask.Last() must be != 0");
            Contract.Requires<ArgumentException> (mask.IsCompactLast ().HasCodeValue (), "Last bit has no Code.Value");
            Contract.Requires<ArgumentException> (offset.HasCodeValue ());
            Contract.Requires<ArgumentException> ((mask.IsCompactLast () + (long)offset) <= Code.MaxValue, "Last bit + offset has no Code.Value");

            start = offset;
            final = offset + mask.IsCompactLast ();
            sorted = new int[mask.Length];
            Array.Copy (mask, sorted, sorted.Length);
            count = BitSetArray.CountOnBits(mask);
        }

        private CodeSetMask (BitSetArray bits, int offset = 0) {
            Contract.Requires<ArgumentNullException> (bits.IsNot (null));
            Contract.Requires<ArgumentEmptyException> (bits.Count != 0, "Empty Mask Array");
            Contract.Requires<ArgumentException> ((bits.First + offset).HasCodeValue(), "First item + offset has no Code value");
            Contract.Requires<ArgumentException> ((bits.Last + offset).HasCodeValue(), "Last item + offset has no Code value");

            Contract.Assume (bits.First.HasValue);
            Contract.Assume (bits.Last.HasValue);
            Contract.Assume (bits.First <= bits.Last);

            this.start = ((int)bits.First) + offset;
            this.final = ((int)bits.Last) + offset;
            this.sorted = new int[BitSetArray.GetIntArrayLength (1 + this.final - this.start)];

            int item = 0;
            int index = 0;
            int mask = 0;
            foreach (var bit in bits) {
                item = bit + offset - start;
                index = item >> ShiftRightBits;
                mask = 1 << (item & ShiftLeftMask);
                sorted[index] ^= mask;
                ++count;
            }
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
        public override bool IsReduced {
            get { // When more members than list, when not full and not wider than character range (16 bits/8KB)
        		return Count > Service.ListMaxCount && Length > Count && Length <= char.MaxValue;
            }
        }

        [Pure]
        public override IEnumerator<Code> GetEnumerator () {
            return new Enumerator(this);
        }

        #endregion

        #region Extended

        [ContractInvariantMethod] [Pure]
        private void CodeSetMaskInvariant () {
            Contract.Invariant (this.Count > Code.MinCount && this.Count <= Code.MaxCount);
        }

        
        public class Enumerator : IEnumerator<Code>, IEnumerator, IDisposable {
            private const int int32Bits = sizeof (int) * 8;
            protected readonly CodeSetMask enumerated;
            private uint bitItems;
            private int arrIndex;
            private int bitStart;
            private int bitIndex = -1; // Initial position
            private bool doNext = true; // CodeSetMask is never empty
            private bool invalid = true; // Invalidate .Current

            public Enumerator (CodeSetMask that) {
                Contract.Requires<ArgumentNullException> (that.IsNot (null));
                Contract.Ensures (Theory.Construct (this, that));

                this.enumerated = that;
                Contract.Assume (this.enumerated.count != 0);
                Contract.Assume (this.enumerated.sorted.Length != 0);
                this.bitItems = unchecked ((uint)this.enumerated.sorted[0]);
            }

            public void Reset () {
                // do the same as compiler generated enumerator
                throw new NotSupportedException ();
            }

            void IDisposable.Dispose () {
            }

            public virtual bool MoveNext () {
                Contract.Ensures (Theory.MoveNext (this, Contract.Result<bool> ()));

                if (this.doNext) {
                    if (this.bitItems == 0) {
                        // read/skip empty block(s)
                        while (true) {
                            ++this.arrIndex;
                            if (this.arrIndex == this.enumerated.sorted.Length)
                                break;
                            if (this.enumerated.sorted[this.arrIndex] != 0)
                                break;
                        }
                        if (this.arrIndex != this.enumerated.sorted.Length) {
                            this.bitStart = this.arrIndex * int32Bits; // compute offset
                            this.bitIndex = -1; // reset bit index
                            this.bitItems = unchecked ((uint)this.enumerated.sorted[this.arrIndex]);
                        }
                    }
                    if (this.bitItems != 0) {
                        if (this.invalid) {
                            this.invalid = false;
                        }
                        ++this.bitIndex;
                        while ((this.bitItems & 1ul) == 0) {
                            ++this.bitIndex;
                            this.bitItems >>= 1;
                        }
                        this.bitItems >>= 1;
                    }
                    else {
                        this.doNext = false; // break&stop .MoveNext
                        this.invalid = true; // invalidate .Current
                    }
                }
                return this.doNext;
            }

            public Code Current {
                get {
                    Contract.Ensures (Theory.Current (this, Contract.Result<Code> ()));
                    Contract.EnsuresOnThrow<InvalidOperationException> (Theory.CurrentOnThrow (this));

                    if (!this.invalid) {
                        return (this.bitStart + this.bitIndex + this.enumerated.start);
                    }
                    throw new InvalidOperationException ("The enumerator is not positioned within collection.");
                }
            }

            object IEnumerator.Current {
                get {
                    return Current;
                }
            }

            private static class Theory {

                [Pure]
                public static bool Construct (Enumerator me, CodeSetMask that) {
                    Success success = true;

                    success.Assert (that.IsNot (null));

                    success.Assert (me.doNext == (that.count != 0));
                    success.Assert (me.invalid == true);

                    return success;
                }

                [Pure]
                public static bool MoveNext (Enumerator me, bool result) {
                    Success success = true;
                    if (result) {
                        success.Assert (!me.invalid);
                        success.Assert (me.enumerated[me.bitStart + me.bitIndex + me.enumerated.start]);
                    }
                    else {
                        success.Assert (me.invalid);
                    }
                    return success;
                }

                [Pure]
                public static bool Current (Enumerator me, Code retValue) {
                    Success success = true;

                    success.Assert (!me.invalid);
                    success.Assert (me.enumerated[retValue]);
                    success.Assert (retValue >= me.enumerated.start && retValue <= me.enumerated.final);
                    success.Assert (retValue == me.bitStart + me.bitIndex + me.enumerated.start);

                    return success;
                }

                [Pure]
                public static bool CurrentOnThrow (Enumerator me) {
                    Success success = true;

                    success.Assert (me.invalid);

                    return success;
                }
            }
        }

        #endregion
    }
}
