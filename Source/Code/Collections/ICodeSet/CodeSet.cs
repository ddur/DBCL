// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DD.Collections.ICodeSet {

    /// <summary>Abstract base class for ICodeSet implementations
    /// <remarks>Implements ICodeSet inherited interfaces for derived classes</remarks>
    /// </summary>
    public abstract class CodeSet : ICodeSet {

        #region Ctor

        protected CodeSet () { }

        #endregion

        #region ICodeSet

        [Pure]
        public abstract bool this[Code code] { get; }

        [Pure]
        public bool this[int value] {
            get {
                return value.HasCodeValue () && this[(Code)value];
            }
        }

        [Pure]
        public abstract int Length { get; }

        [Pure]
        public abstract Code First { get; }

        [Pure]
        public abstract Code Last { get; }

        [Pure]
        IEnumerator IEnumerable.GetEnumerator () { return this.GetEnumerator (); }

        [Pure]
        public abstract IEnumerator<Code> GetEnumerator ();

        [Pure]
        public virtual bool Equals (ICodeSet that) {
            Contract.Ensures (Contract.Result<bool> () == this.SetEquals (that));
            return this.SetEquals (that);
        }

        [Pure]
        public int CompareTo (ICodeSet that) {
            Contract.Ensures (Contract.Result<int> () == this.Compare (that));
            return this.Compare (that);
        }

        #endregion

        #region Equals<object> & GetHashCode()

        [Pure]
        public override bool Equals (object obj) {
            Contract.Ensures (Contract.Result<bool> () == ((obj is ICodeSet) && this.SetEquals ((ICodeSet)obj)));
            return (obj is ICodeSet) && this.SetEquals ((ICodeSet)obj);
        }

        [Pure]
        public override int GetHashCode () {
            Contract.Ensures (Contract.Result<int> () == this.HashCode ());
            return this.HashCode ();
        }

        [Pure]
        public static bool operator == (CodeSet lhs, CodeSet rhs) {
            return lhs.Is (null) ? rhs.IsNullOrEmpty () : lhs.Equals (rhs);
        }

        [Pure]
        public static bool operator != (CodeSet lhs, CodeSet rhs) {
            return lhs.Is (null) ? !rhs.IsNullOrEmpty () : !lhs.Equals (rhs);
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
            Contract.Ensures (Contract.Result<bool> () == a.SetEquals (b));
            return a.SetEquals (b);
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="that"></param>
        /// <returns></returns>
        [Pure]
        public int GetHashCode (ICodeSet that) {
            Contract.Ensures (Contract.Result<int> () == that.HashCode ());
            return that.HashCode ();
        }

        #endregion

        #region ICollection

        [Pure]
        public abstract int Count { get; }

        [Pure]
        public bool IsReadOnly {
            get {
                return true;
            }
        }

        /// <summary>Returns True if collection Contains (IsSupersetOf) specified code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>bool</returns>
        [Pure]
        public bool Contains (Code code) {
            Contract.Ensures (Contract.Result<bool> () == this[code]);
            return this[code];
        }

        /// <summary>Copies members from this collection into Code[]
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        [Pure]
        [SuppressMessage ("Microsoft.Contracts", "CC1033", Justification = "Debug/Release exceptions not same")]
        public void CopyTo (Code[] array, int arrayIndex) {
            Contract.Requires<ArgumentNullException> (array.IsNot (null));
            Contract.Requires<IndexOutOfRangeException> (arrayIndex >= 0);
            Contract.Requires<IndexOutOfRangeException> (arrayIndex <= (array.Length - this.Count));
            foreach (Code code in this) {
                array[arrayIndex] = code;
                ++arrayIndex;
            }
        }

        /// <summary>Explicit interface implementation.<para>Operations not supported on Read-Only Collection</para>
        /// </summary>
        /// <param name="code"></param>
        [Pure]
        void ICollection<Code>.Add (Code code) { throw new NotSupportedException (); }

        /// <summary>Explicit interface implementation.<para>Operations not supported on Read-Only Collection</para>
        /// </summary>
        [Pure]
        void ICollection<Code>.Clear () { throw new NotSupportedException (); }

        /// <summary>Explicit interface implementation.<para>Operations not supported on Read-Only Collection</para>
        /// </summary>
        [Pure]
        bool ICollection<Code>.Remove (Code code) { throw new NotSupportedException (); }

        #endregion
    }
}