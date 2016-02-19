// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
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

        public const int NullCount = 0;
        public const int UnitCount = 1;
        public const int PairCount = 2;

        // NOTE: log(16) == 4 tests worst case
        // TODO? compare speed on 32 and 64
        public const int ListMaxCount = 16;

        public const int MaskMaxSpan = 256;

        public const int NoneStart = -1;
        public const int NoneFinal = -2;

        #region Ctor

        protected CodeSet () { }

        #endregion

        #region ICode & ICodeSet

        [Pure]
        public abstract bool this[Code code] { get; }

        [Pure]
        public abstract bool this[int value] { get; }

        [Pure]
        public abstract bool IsEmpty { get; }

        [Pure]
        public abstract int Length { get; }

        [Pure]
        public abstract Code First { get; }

        [Pure]
        public abstract Code Last { get; }

        [Pure]
        public abstract bool IsReduced { get; }

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

        #region IReadOnllyCollection

        [Pure]
        public abstract int Count { get; }

        #endregion

        #region Cast
        #endregion
    }
}
