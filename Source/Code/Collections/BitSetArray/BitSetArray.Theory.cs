// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using DD.Diagnostics;

namespace DD.Collections {

    public sealed partial class BitSetArray {

        /// <summary>Test Theory
        /// <remarks>Contract Ensures &amp; Requires</remarks>
        /// </summary>
        private static class Theory {

            #region Validation

            [Pure]
            public static bool IsValidMembers (IEnumerable<int> items) {
                if (items.IsNull ()) {
                    return false;
                }
                foreach (var item in items) {
                    if (!IsValidMember (item)) {
                        return false;
                    }
                }
                return true;
            }

            [Pure]
            public static bool IsValidMember (int item) {
                if (item >= 0 && item != int.MaxValue)
                    return true;
                return false;
            }

            [Pure]
            public static bool IsValidLength (int item) {
                if (item >= 0)
                    return true;
                return false;
            }

            [Pure]
            public static int BitSetArrayLength (int length) {
                return length == 0 ? 0 : ((--length) >> 6) + 1;
            }

            #endregion

            #region Invariant

            [Pure]
            public static bool Invariant (BitSetArray self) {
                Success success = true;

                success.Assert (self.IsReadOnly == false);
                success.Assert (self.SyncRoot.IsNot (null));
                success.Assert (self.array.IsNot (null));
                success.Assert (self.count >= 0);
                success.Assert (self.range >= 0);
                success.Assert (self.count <= self.range);
                success.Assert (BitSetArrayLength (self.range).InRange (0, self.array.Length)); // trimmed or not
                success.Assert (self.array.Length.InRange (BitSetArrayLength (self.range), BitSetArrayLength (int.MaxValue)));
                if (self.array.Length == 0) {
                    success.Assert (self.range == 0);
                }
                success.Assert (Theory.IsTailCleared (self));

#if TESTCOUNT
                // Too slow with CodeCoverage
                success.Assert (self.count == Theory.Count(self));
#endif

                return success;
            }

#if TESTCOUNT

            [Pure]
            public static long Count (BitSetArray mask) {
                return Count (mask.array, GetLongArrayLength (mask.range));
            }

            [Pure]
            public static long Count (long[] mask, int maxLen) {
                long counter = 0;
                ulong bitBlock = 0;
                if ( mask.Length < maxLen )
                    maxLen = mask.Length;
                for ( int i = 0; i < maxLen; i++ ) {
                    bitBlock = unchecked ((ulong)mask[i]);

                    if ( bitBlock == 0 ) {
                    }
                    else if ( bitBlock == ulong.MaxValue ) {
                        counter += longBits;
                    }
                    else {
                        counter += Theory.Count (bitBlock);
                    }
                }
                return counter;
            }
#endif

            #endregion

            #region Ctor

            [Pure]
            public static bool Construct (BitSetArray empty) {
                return Construct (empty, 0);
            }

            [Pure]
            public static bool Construct (BitSetArray created, int length) {
                Success success = true;

                success.Assert (created.IsNot (null));
                success.Assert (IsValidLength (length));

                success.Assert (created.count == 0);
                success.Assert (created.range == length);
                success.Assert (created.array.Length == BitSetArrayLength (length));
                success.Assert (created.version == int.MaxValue);

                return success;
            }

            [Pure]
            public static bool Construct (BitSetArray created, int length, bool defaultValue) {
                Success success = true;

                success.Assert (created.IsNot (null));
                success.Assert (IsValidLength (length));
                success.Assert (defaultValue == defaultValue.Bool ());

                if (defaultValue.Bool ()) {
                    success.Assert (created.range > 0);
                    success.Assert (created.count == length);
                    success.Assert (created.range == length);
                    success.Assert (created.array.Length == BitSetArrayLength (length));
                    success.Assert (created.version == int.MaxValue);

                    return success;
                }
                else {
                    return Construct (created, length);
                }
            }

            [Pure]
            public static bool Construct (BitSetArray created, BitSetArray from) {
                return IsCopy (created, from);
            }

            [Pure]
            public static bool Construct (BitSetArray created, BitSetArray from, int length) {
                Success success = true;

                success.Assert (created.IsNot (null));
                success.Assert (from.IsNot (null));
                success.Assert (IsValidLength (length));

                success.Assert (created.IsNot (from));

                success.Assert (created.array.IsNot (from.array));
                success.Assert (created.sRoot.IsNot (from.sRoot));
                success.Assert (created.range == length);
                success.Assert (created.array.Length == BitSetArrayLength (length));
                success.Assert (created.version == int.MaxValue);

                if (length >= from.Length) {
                    success.Assert (created.count == from.count);
                }
                else {
                    success.Assert (created.count <= from.count);
                    int count = 0;
                    foreach (int item in from) {
                        if (item >= length)
                            break;
                        ++count;
                    }
                    success.Assert (created.count == count);
                }

                return success;
            }

            [Pure]
            public static bool IsCopy (BitSetArray copy, BitSetArray from) {
                Success success = true;

                success.Assert (copy.IsNot (null));
                success.Assert (from.IsNot (null));
                success.Assert (copy.IsNot (from));

                success.Assert (copy.array.IsNot (from.array));
                success.Assert (copy.sRoot.IsNot (from.sRoot));
                success.Assert (copy.count == from.count);
                success.Assert (copy.range == from.range);
                success.Assert (copy.version == from.version);
                success.Assert (copy.array.Length == from.array.Length);
                success.Assert (copy.array.Length == 0 || copy.array.SequenceEqual (from.array));

                return success;
            }

            #endregion

            #region From

            [Pure]
            public static bool From (BitSetArray created, IEnumerable<int> fromItems) {
                Success success = true;

                success.Assert (created.IsNot (null));
                success.Assert (fromItems.IsNot (null));
                long index = 0;
                foreach (var item in fromItems) {
                    success.Assert (IsValidMember (item), "Invalid item at [" + index.ToString () + "]: " + item.ToString ());
                    ++index;
                }

                if (fromItems.IsEmpty ()) {
                    success.Assert (Construct (created));
                }
                else {
                    success.Assert (created.count == fromItems.Distinct ().Count ());
                    index = 0;
                    foreach (var item in fromItems) {
                        success.Assert (created[item], "False[" + item.ToString () + "] at items[" + index.ToString () + "]");
                        ++index;
                    }
                }

                return success;
            }

            [Pure]
            public static bool From (BitSetArray created, int fromRequired, IEnumerable<int> fromOptional) {
                Success success = true;

                success.Assert (created.IsNot (null));
                success.Assert (fromOptional.IsNot (null));
                success.Assert (IsValidMember (fromRequired));
                int counter = 0;
                foreach (var item in fromOptional) {
                    success.Assert (IsValidMember (item), "Invalid item at [" + counter.ToString () + "]: " + item.ToString ());
                    counter += 1;
                }

                if (fromOptional.IsEmpty ()) {
                    success.Assert (created[fromRequired]);
                    success.Assert (created.count == 1);
                }
                else {
                    success.Assert (created[fromRequired]);
                    counter = 0;
                    foreach (var item in fromOptional) {
                        success.Assert (created[item], "Failed ret[" + item.ToString () + "] at items[" + counter.ToString () + "]");
                        counter += 1;
                    }
                    success.Assert (created.count == fromOptional.Concat (new int[] { fromRequired }).Distinct ().Count ());
                }

                return success;
            }

            #endregion

            #region Mask

            [Pure]
            public static bool Mask (BitSetArray created, BitArray fromMask, int length) {
                Success success = true;

                success.Assert (created.IsNot (null));
                success.Assert (fromMask.IsNot (null));
                success.Assert (IsValidLength (length));

                success.Assert (created.range == length);

                if (length >= fromMask.Length) {
                    success.Assert (Theory.CountOnBits (created.count, fromMask));
                }
                else {
                    BitArray maskClone = (BitArray)fromMask.Clone ();
                    maskClone.Length = length; // reduce clone length
                    success.Assert (Theory.CountOnBits (created.count, maskClone));
                }

                return success;
            }

            [Pure]
            public static bool Mask (BitSetArray created, IEnumerable<bool> fromMask, int length) {
                Success success = true;

                success.Assert (created.IsNot (null));
                success.Assert (fromMask.IsNot (null));
                success.Assert (IsValidLength (length));

                success.Assert (created.range == length);

                if (length >= fromMask.Count ()) {
                    success.Assert (Theory.CountOnBits (created.count, fromMask));
                }
                else {
                    long index = 0;
                    long input = 0;
                    foreach (var item in fromMask) {
                        if (index >= length) {
                            break;
                        }
                        if (item) {
                            ++input;
                        }
                        ++index;
                    }
                    success.Assert (created.range == index);
                    success.Assert (created.count == input);
                }

                return success;
            }

            [Pure]
            public static bool Mask (BitSetArray created, IEnumerable<byte> fromMask, int length) {
                Success success = true;

                success.Assert (created.IsNot (null));
                success.Assert (fromMask.IsNot (null));
                success.Assert (IsValidLength (length));

                success.Assert (created.range == length);

                if (length >= fromMask.Count () * byteBits) {
                    success.Assert (Theory.CountOnBits (created.count, fromMask));
                }
                else {
                    long index = 0;
                    long count = 0;
                    byte store = 0;
                    foreach (var item in fromMask) {
                        if (index >= length) {
                            break;
                        }
                        store = item;
                        count += Theory.Count (store);
                        index += byteBits;
                    }
                    if (index != length && store != 0) {
                        count -= Theory.Count (store);
                        store = (byte)(store & (0xFF >> (byteBits - (length & 0x7))));
                        count += Theory.Count (store);
                    }
                    success.Assert (created.count == count);
                }

                return success;
            }

            [Pure]
            public static bool Mask (BitSetArray created, IEnumerable<short> fromMask, int length) {
                Success success = true;

                success.Assert (created.IsNot (null));
                success.Assert (fromMask.IsNot (null));
                success.Assert (IsValidLength (length));

                success.Assert (created.range == length);

                if (length >= fromMask.Count () * shortBits) {
                    success.Assert (Theory.CountOnBits (created.count, fromMask));
                }
                else {
                    long index = 0;
                    long count = 0;
                    short store = 0;
                    foreach (var item in fromMask) {
                        if (index >= length) {
                            break;
                        }
                        count += Theory.Count (unchecked ((ushort)item));
                        index += shortBits;
                        store = item;
                    }
                    if (index != length && store != 0) {
                        count -= Theory.Count (unchecked ((ushort)store));
                        store = (short)(store & (0xFFFF >> (shortBits - (length & 0xF))));
                        count += Theory.Count (unchecked ((ushort)store));
                    }
                    success.Assert (created.count == count);
                }

                return success;
            }

            [Pure]
            public static bool Mask (BitSetArray created, IEnumerable<int> fromMask, int length) {
                Success success = true;

                success.Assert (created.IsNot (null));
                success.Assert (fromMask.IsNot (null));
                success.Assert (IsValidLength (length));

                success.Assert (created.range == length);

                if (length >= fromMask.Count () * int32Bits) {
                    success.Assert (Theory.CountOnBits (created.count, fromMask));
                }
                else {
                    long index = 0;
                    long count = 0;
                    int store = 0;
                    foreach (var item in fromMask) {
                        if (index >= length) {
                            break;
                        }
                        count += Theory.Count (unchecked ((uint)item));
                        index += int32Bits;
                        store = item;
                    }
                    if (index != length && store != 0) {
                        count -= Theory.Count (unchecked ((uint)store));
                        store = (int)(store & (0xFFFFFFFF >> (int32Bits - (length & 0x1F))));
                        count += Theory.Count (unchecked ((uint)store));
                    }
                    success.Assert (created.count == count);
                }

                return success;
            }

            [Pure]
            public static bool Mask (BitSetArray created, IEnumerable<long> fromMask, int length) {
                Success success = true;

                success.Assert (created.IsNot (null));
                success.Assert (fromMask.IsNot (null));
                success.Assert (IsValidLength (length));

                success.Assert (created.range == length);

                if (length >= fromMask.Count () * longBits) {
                    success.Assert (Theory.CountOnBits (created.count, fromMask));
                }
                else {
                    long index = 0;
                    long count = 0;
                    long store = 0;
                    foreach (var item in fromMask) {
                        if (index >= length) {
                            break;
                        }
                        count += Theory.Count (unchecked ((ulong)item));
                        index += longBits;
                        store = item;
                    }
                    if (index != length && store != 0) {
                        count -= Theory.Count (unchecked ((ulong)store));
                        store = unchecked ((long)((ulong)store & (0xFFFFFFFFFFFFFFFF >> (longBits - (length & 0x3F)))));
                        count += Theory.Count (unchecked ((ulong)store));
                    }
                    success.Assert (created.count == count);
                }

                return success;
            }

            #endregion

            #region CountOnBits

            [Pure]
            public static long Count (ulong mask) {
                long counter = 0;
#if BITCOUNT
                while ( mask != 0 ) {
                    if ( (mask & 1) != 0 ) {
                        ++counter;
                    }
                    mask >>= 1;
                }
#else // TABLECOUNT
                while (mask != 0) {
                    counter += (int)((table >> (int)((mask & 0xFul) << 2)) & 0xFul);
                    mask >>= 4;
                }
#endif
                return counter;
            }

            [Pure]
            public static bool CountOnBits (int count, byte mask) {
                Success success = true;

                success.Assert (IsValidLength (count));
                success.Assert (count == Theory.Count (mask));

                return success;
            }

            [Pure]
            public static bool CountOnBits (int count, short mask) {
                Success success = true;

                success.Assert (IsValidLength (count));
                success.Assert (count == Theory.Count (unchecked ((ushort)mask)));

                return success;
            }

            [Pure]
            public static bool CountOnBits (int count, int mask) {
                Success success = true;

                success.Assert (IsValidLength (count));
                success.Assert (count == Theory.Count (unchecked ((uint)mask)));

                return success;
            }

            [Pure]
            public static bool CountOnBits (int count, long mask) {
                Success success = true;

                success.Assert (IsValidLength (count));
                success.Assert (count == Theory.Count (unchecked ((ulong)mask)));

                return success;
            }

            [Pure]
            public static bool CountOnBits (int count, ulong mask) {
                Success success = true;

                success.Assert (IsValidLength (count));
                success.Assert (count == Theory.Count (mask));

                return success;
            }

            [Pure]
            public static bool CountOnBits (int count, BitArray mask) {
                Success success = true;

                success.Assert (IsValidLength (count));
                int counter = 0;
                if (!mask.IsNull () && (mask.Length != 0)) {
                    foreach (bool item in mask) {
                        if (item)
                            ++counter;
                    }
                }
                else {
                    counter = 0;
                }
                success.Assert (count == counter);

                return success;
            }

            [Pure]
            public static bool CountOnBits (int count, IEnumerable<bool> mask) {
                Success success = true;

                success.Assert (IsValidLength (count));
                long counter = 0;
                if (!mask.IsNullOrEmpty ()) {
                    foreach (bool item in mask) {
                        if (item)
                            ++counter;
                    }
                }
                else {
                    counter = 0;
                }
                success.Assert (count == counter);

                return success;
            }

            [Pure]
            public static bool CountOnBits (int count, byte[] mask) {
                Success success = true;

                success.Assert (IsValidLength (count));
                long counter = 0;
                if (!mask.IsNullOrEmpty ()) {
                    foreach (byte item in mask) {
                        counter += Theory.Count (item);
                    }
                }
                else {
                    counter = 0;
                }
                success.Assert (count == counter);

                return success;
            }

            [Pure]
            public static bool CountOnBits (int count, short[] mask) {
                Success success = true;

                success.Assert (IsValidLength (count));
                long counter = 0;
                if (!mask.IsNullOrEmpty ()) {
                    foreach (short item in mask) {
                        counter += Theory.Count (unchecked ((ushort)item));
                    }
                }
                else {
                    counter = 0;
                }
                success.Assert (count == counter);

                return success;
            }

            [Pure]
            public static bool CountOnBits (int count, int[] mask) {
                Success success = true;

                success.Assert (IsValidLength (count));
                long counter = 0;
                if (!mask.IsNullOrEmpty ()) {
                    foreach (int item in mask) {
                        counter += Theory.Count (unchecked ((uint)item));
                    }
                }
                else {
                    counter = 0;
                }
                success.Assert (count == counter);

                return success;
            }

            [Pure]
            public static bool CountOnBits (int count, int[] mask, int length) {
                Success success = true;

                success.Assert (IsValidLength (count));

                const int tSize = sizeof (int) * 8;

                long counter = 0;
                if (!mask.IsNullOrEmpty ()) {
                    if (length >= mask.Count () * tSize) {
                        foreach (int item in mask) {
                            counter += Theory.Count (unchecked ((uint)item));
                        }
                    }
                    else {
                        long index = 0;
                        long store = 0;
                        foreach (var item in mask) {
                            if (index >= length) {
                                break;
                            }
                            store = item;
                            counter += Theory.Count (unchecked ((uint)store));
                            index += tSize;
                        }
                        if (index != length && store != 0) {
                            counter -= Theory.Count (unchecked ((uint)store));
                            store = (int)(store & (0xFFFFFFFF >> (tSize - (length & 0x1F))));
                            counter += Theory.Count (unchecked ((uint)store));
                        }
                    }
                }
                else {
                    counter = 0;
                }
                success.Assert (count == counter);

                return success;
            }

            [Pure]
            public static bool CountOnBits (int count, long[] mask) {
                Success success = true;

                success.Assert (IsValidLength (count));
                long counter = 0;
                if (!mask.IsNullOrEmpty ()) {
                    foreach (long item in mask) {
                        counter += Theory.Count (unchecked ((ulong)item));
                    }
                }
                else {
                    counter = 0;
                }
                success.Assert (count == counter);

                return success;
            }

            [Pure]
            public static bool CountOnBits (int count, long[] mask, int length) {
                Success success = true;

                success.Assert (IsValidLength (count));

                const int blockBits = sizeof (long) * 8;

                long counter = 0;

                if (!mask.IsNullOrEmpty ()) {
                    long arrayBits = mask.LongLength * blockBits;
                    if (length >= arrayBits) {
                        foreach (long item in mask) {
                            counter += Theory.Count (unchecked ((ulong)item));
                        }
                    }
                    else {
                        long lastBitIndex = 0;
                        long lastBitBlock = 0;
                        foreach (var item in mask) {
                            if (lastBitIndex >= length) {
                                break;
                            }
                            lastBitBlock = item;
                            counter += Theory.Count (unchecked ((ulong)lastBitBlock));
                            lastBitIndex += blockBits;
                        }
                        if (lastBitIndex != length && lastBitBlock != 0) {
                            counter -= Theory.Count (unchecked ((ulong)lastBitBlock));
                            lastBitBlock = unchecked ((long)((ulong)lastBitBlock & (0xFFFFFFFFFFFFFFFF >> (blockBits - (length & 0x3F)))));
                            counter += Theory.Count (unchecked ((ulong)lastBitBlock));
                        }
                    }
                }
                else {
                    counter = 0;
                }
                success.Assert (count == counter);

                return success;
            }

            [Pure]
            public static bool CountOnBits (int count, IEnumerable<byte> mask) {
                Success success = true;

                success.Assert (IsValidLength (count));
                long counter = 0;
                if (!mask.IsNullOrEmpty ()) {
                    foreach (byte item in mask) {
                        counter += Theory.Count (item);
                    }
                }
                else {
                    counter = 0;
                }
                success.Assert (count == counter);

                return success;
            }

            [Pure]
            public static bool CountOnBits (int count, IEnumerable<short> mask) {
                Success success = true;

                success.Assert (IsValidLength (count));
                long counter = 0;
                if (!mask.IsNullOrEmpty ()) {
                    foreach (short item in mask) {
                        counter += Theory.Count (unchecked ((ushort)item));
                    }
                }
                else {
                    counter = 0;
                }
                success.Assert (count == counter);

                return success;
            }

            [Pure]
            public static bool CountOnBits (int count, IEnumerable<int> mask) {
                Success success = true;

                success.Assert (IsValidLength (count));
                long counter = 0;
                if (!mask.IsNullOrEmpty ()) {
                    foreach (int item in mask) {
                        counter += Theory.Count (unchecked ((uint)item));
                    }
                }
                success.Assert (count == counter);

                return success;
            }

            [Pure]
            public static bool CountOnBits (int count, IEnumerable<long> mask) {
                Success success = true;

                success.Assert (IsValidLength (count));
                long counter = 0;
                if (!mask.IsNullOrEmpty ()) {
                    foreach (long item in mask) {
                        counter += Theory.Count (unchecked ((ulong)item));
                    }
                }
                success.Assert (count == counter);

                return success;
            }

            #endregion

            #region Operations

            [Pure]
            public static bool And (BitSetArray oldState, BitSetArray argument, BitSetArray newState, BitSetArray retValue) {
                Success success = true;

                success.Assert (retValue.IsNot (null));
                success.Assert (retValue.Is (newState));
                if (newState.SequenceEqual (oldState)) {
                    success.Assert (newState.version == oldState.version);
                }
                else {
                    success.Assert (newState.version != oldState.version);
                }

                success.Assert (newState.range == oldState.range);

                if (newState.Is (argument) || oldState.count == 0) {
                    // A == B => (A ∩ B) == A
                    // A == ∅ => (∅ ∩ B = ∅) == A
                    success.Assert (newState.SequenceEqual (oldState));
                    success.Assert (newState.version == oldState.version);
                }
                else if (argument.IsNullOrEmpty () && (oldState.count != 0)) {
                    // A ∩ ∅ == ∅
                    success.Assert (newState.count == 0);
                    success.Assert (newState.version != oldState.version);
                }
                else {
                    // A ∩ B == C
                    success.Assert (newState.SequenceEqual ((IEnumerable<int>)oldState.Intersect ((IEnumerable<int>)argument)));
                }

                return success;
            }

            [Pure]
            public static bool Or (BitSetArray oldState, BitSetArray argument, BitSetArray newState, BitSetArray retValue) {
                Success success = true;

                success.Assert (retValue.IsNot (null));
                success.Assert (retValue.Is (newState));
                if (newState.SequenceEqual (oldState)) {
                    success.Assert (newState.version == oldState.version);
                }
                else {
                    success.Assert (newState.version != oldState.version);
                }

                if (argument.IsNullOrEmpty ()) {
                    success.Assert (newState.range == oldState.range);
                }
                else {
                    success.Assert (newState.range == (oldState.range > argument.range ? oldState.range : argument.range));
                }

                if (newState.Is (argument) || argument.IsNullOrEmpty ()) {
                    // A ∪ ∅ == A
                    // A ∪ A == A
                    success.Assert (newState.SequenceEqual (oldState));
                    success.Assert (newState.version == oldState.version);
                }
                else if (oldState.count == 0) {
                    // ∅ ∪ B == B
                    success.Assert (newState.SequenceEqual (argument));
                    success.Assert (newState.version != oldState.version);
                }
                else {
                    // A ∪ B == C
                    success.Assert (newState.SequenceEqual (oldState.Union (argument).OrderBy (item => (item))));
                }

                return success;
            }

            [Pure]
            public static bool Xor (BitSetArray oldState, BitSetArray argument, BitSetArray newState, BitSetArray retValue) {
                Success success = true;

                success.Assert (retValue.IsNot (null));
                success.Assert (retValue.Is (newState));
                if (newState.SequenceEqual (oldState)) {
                    success.Assert (newState.version == oldState.version);
                }
                else {
                    success.Assert (newState.version != oldState.version);
                }

                if (argument.IsNullOrEmpty ()) {
                    success.Assert (newState.range == oldState.range);
                }
                else {
                    success.Assert (newState.range == (oldState.range > argument.range ? oldState.range : argument.range));
                }

                if (newState.Is (argument)) {
                    // A.Xor(A) == ∅
                    success.Assert (newState.count == 0);
                    if (oldState.count == 0) {
                        success.Assert (newState.version == oldState.version);
                    }
                    else {
                        success.Assert (newState.version != oldState.version);
                    }
                }
                else if (argument.IsNullOrEmpty ()) {
                    // A.Xor(∅)=A
                    success.Assert (newState.SequenceEqual (oldState));
                    success.Assert (newState.version == oldState.version);
                }
                else if (oldState.count == 0) {
                    // ∅.Xor(B) == B
                    success.Assert (newState.SequenceEqual (argument));
                    success.Assert (newState.version != oldState.version);
                }
                else {
                    // A.Xor(B) == C
                    success.Assert (newState.SequenceEqual (oldState.Union (argument).Except (oldState.Intersect (argument)).OrderBy (item => (item))));
                }

                return success;
            }

            [Pure]
            public static bool Not (BitSetArray oldState, BitSetArray argument, BitSetArray newState, BitSetArray retValue) {
                Success success = true;

                success.Assert (retValue.IsNot (null));
                success.Assert (retValue.Is (newState));

                success.Assert (newState.range == oldState.range);
                if (newState.SequenceEqual (oldState)) {
                    success.Assert (newState.version == oldState.version);
                }
                else {
                    success.Assert (newState.version != oldState.version);
                }

                if (newState.Is (argument)) {
                    // A.Not(A) == ∅
                    success.Assert (newState.count == 0);
                    if (oldState.count == 0) {
                        success.Assert (newState.version == oldState.version);
                    }
                    else {
                        success.Assert (newState.version != oldState.version);
                    }
                }
                else if (oldState.count == 0 || argument.IsNullOrEmpty ()) {
                    // A.Not(∅) == A
                    // A∅.Not(B) == A∅
                    success.Assert (newState.version == oldState.version);
                }
                else {
                    // A.Not(B) == C
                    success.Assert (newState.SequenceEqual ((oldState.Except (argument).Intersect (oldState)).OrderBy (item => (item))));
                }

                return success;
            }

            [Pure]
            public static bool Not (BitSetArray oldState, BitSetArray newState, BitSetArray retValue) {
                Success success = true;

                success.Assert (retValue.IsNot (null));
                success.Assert (retValue.Is (newState));

                success.Assert (newState.range == oldState.range);

                if (oldState.range == 0) {
                    // Empty range domain
                    // Nothing to change
                    success.Assert (newState.SequenceEqual (oldState));
                    success.Assert (newState.version == oldState.version);
                }
                else {
                    // A.Not() == ~A
                    success.Assert (newState.count == (newState.range - oldState.count));
                    success.Assert (!newState.Intersect (oldState).Any ());
                    success.Assert (newState.version != oldState.version);
                }

                return success;
            }

            #endregion

            #region Relations

            [Pure]
            public static bool SetEquals (BitSetArray self, bool equal, BitSetArray that) {
                Success success = true;

                if (that.IsNullOrEmpty ()) {
                    if (self.count == 0) {
                        success.Assert (equal == true);
                    }
                    else {
                        success.Assert (equal == false);
                    }
                }
                else {
                    // ATTN! Must use Linq.Enumerable.SequenceEqual here
                    // else SequenceEqual->SetEquals->Theory.SetEquals back here
                    success.Assert (self.SequenceEqual (that as IEnumerable<int>) == equal);
                }

                return success;
            }

            [Pure]
            public static bool Overlaps (BitSetArray self, bool overlaps, BitSetArray that) {
                Success success = true;

                if (self.count == 0 || that.IsNullOrEmpty ()) {
                    success.Assert (overlaps == false);
                }
                else {
                    success.Assert (overlaps == (self.Intersect (that).Any ()));
                }

                return success;
            }

            [Pure]
            public static bool IsSupersetOf (BitSetArray self, bool isSupersetOf, BitSetArray that) {
                Success success = true;

                if (self.count == 0 || that.IsNullOrEmpty ()) {
                    success.Assert (isSupersetOf == false);
                }
                else {
                    success.Assert (isSupersetOf == (self.Intersect (that).Count () == that.Count ()));
                }

                return success;
            }

            [Pure]
            public static bool IsProperSupersetOf (BitSetArray self, bool isProperSupersetOf, BitSetArray that) {
                Success success = true;

                if (self.count == 0 || that.IsNullOrEmpty ()) {
                    success.Assert (isProperSupersetOf == false);
                }
                else {
                    success.Assert (isProperSupersetOf == ((self.Intersect (that).Count () == that.Count ()) && (self.Count () > that.Count ())));
                }

                return success;
            }

            [Pure]
            public static bool IsSubsetOf (BitSetArray self, bool isSubsetOf, BitSetArray that) {
                Success success = true;

                if (self.count == 0 || that.IsNullOrEmpty ()) {
                    success.Assert (isSubsetOf == false);
                }
                else {
                    success.Assert (isSubsetOf == (self.Intersect (that).Count () == self.Count ()));
                }

                return success;
            }

            [Pure]
            public static bool IsProperSubsetOf (BitSetArray self, bool isProperSubsetOf, BitSetArray that) {
                Success success = true;

                if (self.count == 0 || that.IsNullOrEmpty ()) {
                    success.Assert (isProperSubsetOf == false);
                }
                else {
                    success.Assert (isProperSubsetOf == ((self.Intersect (that).Count () == self.Count ()) && (self.Count () < that.Count ())));
                }

                return success;
            }

            #endregion

            #region Get/Set/Trim...

            [Pure]
            public static bool IndexerGetItemValue (BitSetArray self, int item, bool getValue) {
                Success success = true;
                if (item.InRange (0, self.range - 1)) {
                    success.Assert (getValue == self.Get (item));
                }
                else {
                    success.Assert (getValue == false);
                }
                return success;
            }

            [Pure]
            public static bool IndexerSetItemValue (BitSetArray oldState, int item, bool setValue, BitSetArray newState) {
                Success success = true;
                if (setValue.Bool () && item.InRange (0, newState.range - 1)) {
                    if (oldState[item]) {
                        success.Assert (newState.Count == oldState.Count);
                        success.Assert (newState.Version == oldState.Version);
                    }
                    else {
                        success.Assert (newState.Count == oldState.Count + 1);
                        success.Assert (newState.Version != oldState.Version);
                    }
                    success.Assert (newState[item]);
                }
                else {
                    if (oldState[item]) {
                        success.Assert (newState.Count == oldState.Count - 1);
                        success.Assert (newState.Version != oldState.Version);
                    }
                    else {
                        success.Assert (newState.Count == oldState.Count);
                        success.Assert (newState.Version == oldState.Version);
                    }
                    success.Assert (!newState[item]);
                }
                return success;
            }

            [Pure]
            public static bool Get (BitSetArray self, int item, bool getValue) {
                Success success = true;

                success.Assert (item.InRange (0, self.range - 1));
                success.Assert (getValue == ((self.array[item / 64] & (1L << (item % 64))) != 0));
                success.Assert (getValue == self[item]);

                return success;
            }

            [Pure]
            public static bool Set (BitSetArray oldState, int item, bool setValue, BitSetArray newState) {
                Success success = true;

                success.Assert (setValue == setValue.Bool ());
                success.Assert (item.InRange (0, newState.range - 1));

                if (oldState[item] == setValue) {
                    success.Assert (newState.count == oldState.count);
                    success.Assert (newState.version == oldState.version);
                    success.Assert (oldState[item] == setValue);
                    success.Assert (newState[item] == setValue);
                }
                else if (setValue) {
                    success.Assert (newState.count == oldState.count + 1);
                    success.Assert (newState.version != oldState.version);
                    success.Assert (!oldState[item]);
                    success.Assert (newState[item]);
                }
                else {
                    success.Assert (newState.count == oldState.count - 1);
                    success.Assert (newState.version != oldState.version);
                    success.Assert (oldState[item]);
                    success.Assert (!newState[item]);
                }

                return success;
            }

            [Pure]
            public static bool SetAll (BitSetArray oldState, bool allValue, BitSetArray newState) {
                Success success = true;

                success.Assert (allValue == allValue.Bool ());
                success.Assert (oldState.range == newState.range);
                success.Assert (oldState.SequenceEqual (newState) == (oldState.version == newState.version));

                if (allValue) {
                    success.Assert (newState.count == newState.range);
                    if (oldState.count != oldState.range) {
                        success.Assert (oldState.version != newState.version);
                    }
                    else {
                        success.Assert (oldState.version == newState.version);
                    }
                }
                else {
                    success.Assert (newState.count == 0);
                    if (oldState.count != 0) {
                        success.Assert (oldState.version != newState.version);
                    }
                    else {
                        success.Assert (oldState.version == newState.version);
                    }
                }

                return success;
            }

            [Pure]
            public static bool TrimExcess (BitSetArray oldState, BitSetArray newState) {
                Success success = true;

                success.Assert (newState.count == oldState.count);
                success.Assert (oldState.SequenceEqual (newState));
                success.Assert (oldState.version == newState.version);
                success.Assert (newState.range <= oldState.range);
                success.Assert (newState.array.Length == BitSetArrayLength (newState.range));
                if (oldState.count == 0) {
                    success.Assert (newState.array.Length == 0);
                    success.Assert (newState.Last == null);
                    success.Assert (oldState.Last == null);
                    success.Assert (newState.range == 0);
                }
                else {
                    success.Assert (newState[newState.range - 1]);
                    success.Assert (newState.Last.IsNot (null));
                    success.Assert (oldState.Last.IsNot (null));
                    success.Assert ((int)newState.Last == newState.range - 1);
                    success.Assert ((int)oldState.Last == newState.range - 1);
                }

                return success;
            }

            #endregion

            #region ICollection

            [Pure]
            public static bool ICollectionAdd (BitSetArray oldState, int addItem, BitSetArray newState) {
                Success success = true;

                success.Assert (IsValidMember (addItem));
                success.Assert (newState[addItem]);
                if (addItem >= oldState.range) {
                    success.Assert (newState.range == (addItem + 1));
                }

                if (oldState[addItem]) {
                    success.Assert (newState.count == oldState.count);
                    success.Assert (newState.version == oldState.version);
                }
                else {
                    success.Assert (newState.count == (oldState.count + 1));
                    success.Assert (newState.version != oldState.version);
                }

                return success;
            }

            [Pure]
            public static bool Remove (BitSetArray oldState, int item, BitSetArray newState, bool isRemoved) {
                Success success = true;

                success.Assert (!newState[item]);

                if (oldState[item]) {
                    success.Assert (newState.count == (oldState.count - 1));
                    success.Assert (newState.version != oldState.version);
                    success.Assert (isRemoved);
                }
                else {
                    success.Assert (newState.count == oldState.count);
                    success.Assert (newState.version == oldState.version);
                    success.Assert (isRemoved == false);
                }

                return success;
            }

            [Pure]
            public static bool Clear (BitSetArray oldState, BitSetArray newState) {
                Success success = true;

                success.Assert (newState.Count == 0);
                success.Assert (newState.Length == oldState.Length);
                if (oldState.Count == 0) {
                    success.Assert (newState.version == oldState.version);
                }
                else {
                    success.Assert (newState.version != oldState.version);
                }

                return success;
            }

            [Pure]
            public static bool Contains (BitSetArray self, bool contains, int item) {
                Success success = true;

                success.Assert (contains == self[item]);

                return success;
            }

            [Pure]
            public static bool CopyToArrayOfObj (BitSetArray self, Array array, int index) {
                Success success = true;

                success.Assert (array.IsNot (null));
                success.Assert (array.Rank == 1);
                success.Assert (array is int[]);
                success.Assert (index >= 0);
                success.Assert (index <= (array.Length - self.Count));

                IEnumerator<int> e = self.GetEnumerator ();
                int counter = 0;
                while (e.MoveNext ()) {
                    success.Assert (e.Current == (int)array.GetValue (index + counter));
                    ++counter;
                }
                success.Assert (counter == self.count);

                return success;
            }

            [Pure]
            public static bool CopyToArrayOfInt (BitSetArray self, int[] array, int index) {
                Success success = true;

                success.Assert (array.IsNot (null));
                success.Assert (array.Rank == 1);
                success.Assert (array is int[]);
                success.Assert (index >= 0);
                success.Assert (index <= (array.Length - self.Count));

                IEnumerator<int> e = self.GetEnumerator ();
                int counter = 0;
                while (e.MoveNext ()) {
                    success.Assert (e.Current == array[index + counter]);
                    ++counter;
                }
                success.Assert (counter == self.count);

                return success;
            }

            #endregion

            #region ISet

            #region Operations

            [Pure]
            public static bool Add (BitSetArray oldState, int item, BitSetArray newState, bool isAdded) {
                Success success = true;

                success.Assert (oldState.SequenceEqual (newState) == (oldState.version == newState.version));

                if (IsValidMember (item)) {
                    success.Assert (newState[item]);

                    if (item >= oldState.range) {
                        success.Assert (newState.range == (item + 1));
                    }

                    if (oldState[item]) {
                        success.Assert (newState.count == oldState.count);
                        success.Assert (newState.version == oldState.version);
                        success.Assert (!isAdded);
                    }
                    else {
                        success.Assert (newState.count == (oldState.count + 1));
                        success.Assert (newState.version != oldState.version);
                        success.Assert (isAdded);
                    }
                }
                else {
                    success.Assert (!isAdded);
                }

                return success;
            }

            [Pure]
            public static bool ExceptWith (BitSetArray oldState, IEnumerable<int> argument, BitSetArray newState) {
                Success success = true;

                success.Assert (newState.range == oldState.range);
                success.Assert (oldState.SequenceEqual (newState) == (oldState.version == newState.version));

                if (newState.Is (argument)) {
                    // must test prior to arg.IsNullOrEmpty() test because "ensures" is invoked at exit
                    // cleared
                    // A.Not(A) == ∅
                    success.Assert (newState.count == 0);
                }
                else if (argument.IsNullOrEmpty ()) {
                    // no changes
                    // A.Not(∅) == A
                    success.Assert (newState.version == oldState.version);
                }
                else if (oldState.count == 0) {
                    // no changes
                    // A∅.Not(B) == A∅
                    success.Assert (newState.version == oldState.version);
                }
                else {
                    // A.Not(B) == C
                    success.Assert (newState.SequenceEqual (oldState.Except (argument).OrderBy (item => (item))));
                }

                return success;
            }

            [Pure]
            public static bool IntersectWith (BitSetArray oldState, IEnumerable<int> argument, BitSetArray newState) {
                Success success = true;

                success.Assert (newState.range == oldState.range);
                success.Assert (oldState.SequenceEqual (newState) == (oldState.version == newState.version));

                if (newState.Is (argument)) {
                    // no changes
                    // A.And(A) == A
                    success.Assert (newState.version == oldState.version);
                }
                else if (argument.IsNullOrEmpty ()) {
                    // cleared
                    // A.And(∅) == ∅
                    success.Assert (newState.count == 0);
                }
                else if (oldState.count == 0) {
                    // no changes
                    // A∅.And(B) == ∅ == A∅
                    success.Assert (newState.version == oldState.version);
                }
                else {
                    // A.And(B) == C
                    success.Assert (newState.SequenceEqual (oldState.Intersect (argument).OrderBy (item => (item))));
                }

                return success;
            }

            [Pure]
            public static bool SymmetricExceptWith (BitSetArray oldState, IEnumerable<int> argument, BitSetArray newState) {
                Success success = true;

                success.Assert (oldState.SequenceEqual (newState) == (oldState.version == newState.version));

                if (newState.Is (argument)) {
                    // cleared
                    // A.Xor(A) == ∅
                    success.Assert (newState.count == 0);
                }
                else if (argument.IsNullOrEmpty ()) {
                    // no changes
                    // A.Xor(∅) == A
                    success.Assert (newState.version == oldState.version);
                }
                else if (oldState.count == 0) {
                    // changed to B
                    // A∅.Xor(B) == B
                    success.Assert (newState.SequenceEqual (argument.Where (item => IsValidMember (item)).OrderBy (item => (item))));
                }
                else {
                    // A.Xor(B) == C
                    success.Assert (newState.SequenceEqual (oldState.Union (argument.Where (item => IsValidMember (item))).
                                                          Except (oldState.Intersect (argument)).
                                                          OrderBy (item => (item))));
                }

                return success;
            }

            [Pure]
            public static bool UnionWith (BitSetArray oldState, IEnumerable<int> argument, BitSetArray newState) {
                Success success = true;

                success.Assert (oldState.SequenceEqual (newState) == (oldState.version == newState.version));

                if (newState.Is (argument)) {
                    // no changes
                    // A.Or(A) == A
                    success.Assert (newState.version == oldState.version);
                }
                else if (argument.IsNullOrEmpty ()) {
                    // no changes
                    // A.Or(∅) == A
                    success.Assert (newState.version == oldState.version);
                }
                else if (oldState.count == 0) {
                    // changed to B
                    // A∅.Or(B) == B
                    success.Assert (newState.SequenceEqual (argument.Where (item => IsValidMember (item)).OrderBy (item => (item))));
                }
                else {
                    // A.Xor(B) == C
                    success.Assert (newState.SequenceEqual (oldState.Union (argument.Where (item => IsValidMember (item))).
                                                          OrderBy (item => (item))));
                }

                return success;
            }

            #endregion

            #region Relations

            [Pure]
            public static bool SetEquals (BitSetArray self, bool isSetEqualTo, IEnumerable<int> argument) {
                Success success = true;
                if (argument.IsNull ()) {
                    success.Assert (isSetEqualTo == (self.Count == 0));
                }
                else {
                    success.Assert (isSetEqualTo == self.SequenceEqual (argument.Distinct ().OrderBy (item => (item))));
                }

                return success;
            }

            [Pure]
            public static bool Overlaps (BitSetArray self, bool overlaps, IEnumerable<int> argument) {
                Success success = true;

                if (argument.IsNull ()) {
                    success.Assert (overlaps == false);
                }
                else {
                    success.Assert (overlaps == (self.Intersect (argument).Count () != 0));
                }

                return success;
            }

            [Pure]
            public static bool IsSupersetOf (BitSetArray self, bool isSupersetOf, IEnumerable<int> argument) {
                Success success = true;

                if (argument.IsNull ()) {
                    success.Assert (isSupersetOf == false);
                }
                else {
                    success.Assert (isSupersetOf == (argument.Any () && ((self.Intersect (argument)).Count () == argument.Count ())));
                }

                return success;
            }

            [Pure]
            public static bool IsProperSupersetOf (BitSetArray self, bool isProperSupersetOf, IEnumerable<int> argument) {
                Success success = true;

                if (argument.IsNull ()) {
                    success.Assert (isProperSupersetOf == false);
                }
                else {
                    success.Assert (isProperSupersetOf == (argument.Any () && (self.Intersect (argument).Count () == argument.Count ()) && (self.Count () > argument.Count ())));
                }

                return success;
            }

            [Pure]
            public static bool IsSubsetOf (BitSetArray self, bool isSubsetOf, IEnumerable<int> argument) {
                Success success = true;

                if (argument.IsNull ()) {
                    success.Assert (isSubsetOf == false);
                }
                else {
                    success.Assert (isSubsetOf == ((self.Count != 0) && argument.Any () && ((self.Intersect (argument)).Count () == self.Count ())));
                }

                return success;
            }

            [Pure]
            public static bool IsProperSubsetOf (BitSetArray self, bool isProperSubsetOf, IEnumerable<int> argument) {
                Success success = true;

                if (argument.IsNull ()) {
                    success.Assert (isProperSubsetOf == false);
                }
                else {
                    success.Assert (isProperSubsetOf == ((self.Count != 0) && argument.Any () && (self.Intersect (argument).Count () == self.Count ()) && (self.Count () < argument.Count ())));
                }

                return success;
            }

            #endregion

            #endregion

            #region Properties

            [Pure]
            public static bool LengthGet (BitSetArray self, int getValue) {
                Success success = true;

                success.Assert (IsValidLength (getValue));
                success.Assert (getValue == self.range);

                return success;
            }

            [Pure]
            public static bool LengthSet (BitSetArray oldState, int setValue, BitSetArray newState) {
                Success success = true;

                success.Assert (IsValidLength (setValue));

                success.Assert (oldState.SequenceEqual (newState) == (oldState.version == newState.version));
                success.Assert (newState.range == setValue);
                success.Assert (newState.Length == newState.range);
                success.Assert (newState.Capacity >= newState.Length);
                if (newState.range > oldState.range) {
                    success.Assert (newState.Capacity >= oldState.Capacity);
                }
                else {
                    success.Assert (newState.Capacity == oldState.Capacity);
                }

                return success;
            }

            [Pure]
            public static bool CapacityGet (BitSetArray self, int getValue) {
                Success success = true;

                if ((self.array.LongLength * 64) <= int.MaxValue) {
                    success.Assert (getValue == (self.array.LongLength * 64));
                }
                else {
                    success.Assert (getValue == int.MaxValue);
                }

                return success;
            }

            [Pure]
            public static bool FirstGet (BitSetArray self, int? getValue) {
                Success success = true;

                if (self.count == 0) {
                    success.Assert (getValue == null);
                }
                else {
                    success.Assert (getValue.IsNot (null));
                    success.Assert ((int)self.startVersion == self.version);
                    success.Assert (self.startMemoize.Equals (getValue));
                    //					foreach (var item in self) {
                    //						success.Assert((int)getValue == item);
                    //						break;
                    //					}
                }

                return success;
            }

            [Pure]
            public static bool FirstSet (BitSetArray self, int? setValue) {
                Success success = true;

                success.Assert ((int)self.startVersion == self.version);
                success.Assert (self.startMemoize.Equals (setValue));
                success.Assert (setValue.IsNot (null));
                success.Assert (self.count != 0);
                //				foreach (var item in self) {
                //					success.Assert((int)setValue == item);
                //					break;
                //				}

                return success;
            }

            [Pure]
            public static bool LastGet (BitSetArray self, int? getValue) {
                Success success = true;

                if (self.count == 0) {
                    success.Assert (getValue == null);
                }
                else {
                    success.Assert (getValue.IsNot (null));
                    success.Assert ((int)self.finalVersion == self.version);
                    success.Assert (self.finalMemoize.Equals (getValue));
                    foreach (var item in self.Reverse ()) {
                        success.Assert ((int)getValue == item);
                        break;
                    }
                }

                return success;
            }

            [Pure]
            public static bool LastSet (BitSetArray self, int? setValue) {
                Success success = true;

                success.Assert ((int)self.finalVersion == self.version);
                success.Assert (self.finalMemoize.Equals (setValue));
                success.Assert (setValue.IsNot (null));
                success.Assert (self.count != 0);
                foreach (var item in self.Reverse ()) {
                    success.Assert ((int)setValue == item);
                    break;
                }

                return success;
            }

            #endregion

            #region Local

            [Pure]
            public static bool IsTailCleared (BitSetArray self) {
                Success success = true;

                int rangeLength = BitSetArrayLength (self.range);

                // range does not match block end?
                if ((self.range & 0x3F) != 0) {
                    // bits of last block above range cleared?
                    success.Assert ((self.array[rangeLength - 1] & (-1L << (self.range & 0x3F))) == 0);
                }

                // if exist tail of long blocks, check if it is cleared.
                Contract.Assume (rangeLength >= 0);
                if (rangeLength < self.array.Length) {
                    for (int i = rangeLength; i < self.array.Length; i++) {
                        success.Assert (self.array[i] == 0, "Uncleared bits at " + i.ToString ());
                    }
                }

                return success;
            }

            #endregion
        }
    }
}
