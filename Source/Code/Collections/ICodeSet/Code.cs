﻿// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

//#define COMPACT
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DD.Collections.ICodeSet {

    [Serializable]
    public struct Code : ICode, ICodeSet, IEquatable<Code>, IEqualityComparer<Code>, IComparable<Code> {

        #region Ctor

        public Code (byte value) {
#if COMPACT
			this.b0 = value;
			this.b1 = 0;
			this.b2 = 0;
#else
            this.val = value;
#endif
        }

        public Code (char value) {
#if COMPACT
			this.b0 = (byte)(value&0xFF);
			this.b1 = (byte)((value>>8)&0xFF);
			this.b2 = 0;
#else
            this.val = value;
#endif
        }

        public Code (int value) {
            Contract.Requires<InvalidCastException> (value.InRange (Code.MinValue, Code.MaxValue));

#if COMPACT
			this.b0 = (byte)(value&0xFF);
			this.b1 = (byte)((value>>8)&0xFF);
			this.b2 = (byte)((value>>16)&0xFF);
#else
            this.val = value;
#endif
        }

        #endregion

        #region Const

        /// <summary>
        /// Min code-point value: 0
        /// </summary>
        public const int MinValue = 0;

        /// <summary>
        /// Max code-point value: 0x10FFFF (1114111)
        /// </summary>
        public const int MaxValue = 0x10FFFF;

        internal const int MinCount = 0;

        /// <summary>
        /// Max code-points count: 0x110000 (1114112)
        /// </summary>
        public const int MaxCount = 1 + MaxValue; // MinValue.Count + [1..maxValue].Count; ie.Sequence[0..3].Count=4

        #endregion

        #region Fields

        // ATTN! do not add fields to Code structure! Keep it as smal as possible

#if COMPACT
		private readonly byte b0;
		private readonly byte b1;
		private readonly byte b2;
#else
        private readonly int val;
#endif

        #endregion

        #region Interfaces

        #region Code Interfaces

        #region IEquatable<Code>

        [Pure]
        public bool Equals (Code that) {
            return this.Value == that.Value;
        }

        #endregion

        #region IEqualityComparer<Code>

        [Pure]
        public bool Equals (Code a, Code b) {
            return a.Value == b.Value;
        }

        [Pure]
        public int GetHashCode (Code c) {
            return c.GetHashCode ();
        }

        #endregion

        #region IComparable<Code>

        public int CompareTo (Code that) {
            return this.Value.CompareTo (that.Value);
        }

        #endregion

        #endregion

        #region ICodeSet Interfaces

        #region ICodeSet Interface

        [Pure]
        bool ICode.this[Code code] {
            get { return this.Value == code.Value; }
        }

        [Pure]
        bool ICode.this[int value] {
            get { return this.Value == value; }
        }

        [Pure]
        int ICodeSet.Length {
            get { return Service.UnitCount; }
        }

        [Pure]
        Code ICodeSet.First {
            get { return this; }
        }

        [Pure]
        Code ICodeSet.Last {
            get { return this; }
        }

        [Pure]
        public bool IsReduced {
            get {
        		return true;
            }
        }

        #endregion

        #region ICodeSet Inherited

        #region ICollection<Code>

        [Pure]
        bool ICollection<Code>.IsReadOnly {
            get { return true; }
        }

        [Pure]
        int ICollection<Code>.Count {
            get { return Service.UnitCount; }
        }

        /// <summary> Returns True if collection Contains (IsSupersetOf) specified code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>bool</returns>
        [Pure]
        bool ICollection<Code>.Contains (Code code) {
            return this.Value == code.Value;
        }

        /// <summary>Copies members from this collection into Code[]
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        [Pure]
        [SuppressMessage ("Microsoft.Contracts", "CC1033", Justification = "Debug/Release exceptions not same")]
        void ICollection<Code>.CopyTo (Code[] array, int arrayIndex) {
            Contract.Requires<ArgumentNullException> (array.IsNot (null));
            Contract.Requires<IndexOutOfRangeException> (arrayIndex >= 0);
            Contract.Requires<IndexOutOfRangeException> (arrayIndex <= (array.Length - 1));
            array[arrayIndex] = this;
        }

        /// <summary>Explicit interface implementation.<para>Operation not supported on Read-Only Collection</para>
        /// </summary>
        /// <param name="code"></param>
        /// <exception cref="T:System.NotSupportedException"></exception>
        [Pure]
        void ICollection<Code>.Add (Code code) { throw new NotSupportedException (); }

        /// <summary>Explicit interface implementation.<para>Operation not supported on Read-Only Collection</para>
        /// </summary>
        /// <exception cref="T:System.NotSupportedException"></exception>
        [Pure]
        void ICollection<Code>.Clear () { throw new NotSupportedException (); }

        /// <summary>Explicit interface implementation.<para>Operation not supported on Read-Only Collection</para>
        /// </summary>
        /// <param name="code"></param>
        /// <exception cref="T:System.NotSupportedException"></exception>
        [Pure]
        bool ICollection<Code>.Remove (Code code) { throw new NotSupportedException (); }

        #endregion

        #region IEnumerable<Code>

        [Pure]
        IEnumerator IEnumerable.GetEnumerator () {
            return ((IEnumerable<Code>)this).GetEnumerator ();
        }

        [Pure]
        IEnumerator<Code> IEnumerable<Code>.GetEnumerator () {
            yield return this;
        }

        #endregion

        #region IEquatable<ICodeSet>

        [Pure]
        public override bool Equals (object obj) {
            Contract.Ensures (Contract.Result<bool> () == ((obj is ICodeSet) && this.SetEquals ((ICodeSet)obj)));
            return (obj is ICodeSet) && this.SetEquals ((ICodeSet)obj);
        }

        [Pure]
        public bool Equals (ICodeSet that) {
            Contract.Ensures (Contract.Result<bool> () == this.SetEquals (that));
            return this.SetEquals (that);
        }

        [Pure]
        public override int GetHashCode () {
            Contract.Ensures (Contract.Result<int> () == this.HashCode ());
            return this.HashCode ();
        }

        #endregion

        #region IEqualityComparer<ICodeSet>

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [Pure]
        public bool Equals (ICodeSet a, ICodeSet b) {
            // disable once InvokeAsExtensionMethod
            Contract.Ensures (Contract.Result<bool> () == a.SetEquals (b));
            // disable once InvokeAsExtensionMethod
            return a.SetEquals (b);
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="that"></param>
        /// <returns></returns>
        [Pure]
        public int GetHashCode (ICodeSet that) {
            // disable once InvokeAsExtensionMethod
            Contract.Ensures (Contract.Result<int> () == that.HashCode ());
            return that.HashCode ();
        }

        #endregion

        #region IComparable<ICodeSet>

        public int CompareTo (ICodeSet that) {
            Contract.Ensures (Contract.Result<int> () == this.Compare (that));
            return this.Compare (that);
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region Invariant

        [ContractInvariantMethod]
        private void Invariant () {
            Contract.Invariant (((ICodeSet)this).Count == Service.UnitCount);
            Contract.Invariant (((ICodeSet)this).Count == ((ICodeSet)this).Length);
            Contract.Invariant (((ICodeSet)this).First == ((ICodeSet)this).Last);
        }

        #endregion

        #region Methods

        [Pure]
        public override string ToString () {
            if ((Value & 0xFF) == Value) {
                char c = (char)Value;
                if (char.IsControl (c)) {
                    return "\\x" + Value.ToString ("X");
                }
                return "" + c;
            }
            return "\\x" + Value.ToString ("X");
        }

        #endregion

        #region Properties

        [Pure]
        public int Value {
            get {
#if COMPACT
				int v = this.b0;
				v |= (this.b1<<8);
				v |= (this.b2<<16);
				return v;
#else
                return this.val;
#endif
            }
        }

        #endregion

        #region Operators

        #region Equality Operators

        // Cast operator will redirect to int equality operators

        #endregion

        #region Cast Operators

        public static implicit operator int (Code code) {
            return code.Value;
        }

        public static implicit operator Code (byte value) {
            return new Code (value);
        }

        public static implicit operator Code (char value) {
            return new Code (value);
        }

        public static implicit operator Code (int value) {
            Contract.Requires<InvalidCastException> (value.InRange (Code.MinValue, Code.MaxValue));

            return new Code (value);
        }

        #endregion

        #region Comparators

        // Cast operator will redirect to int comparators

        #endregion

        #endregion
    }
}
