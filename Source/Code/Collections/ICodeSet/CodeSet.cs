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

namespace DD.Collections
{
    /// <summary>Abstract base class for ICodeSet implementations
    /// <remarks>Implements ICodeSet inherited interfaces for derived classes</remarks>
    /// </summary>
    [ContractClass(typeof(CodeSetContractClass))]
    public abstract class CodeSet : ICodeSet
    {
        #region Ctor

        protected internal CodeSet () {}

        #endregion

        #region ICodeSet

        [Pure] public abstract bool this [Code code] { get; }
        
        [Pure] public abstract Code First { get; }
        
        [Pure] public abstract Code Last { get; }
        
        [Pure] public virtual int Length {
            get {
                return (this.Last - this.First + 1);
            }
        }
        
        [Pure] IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }
        
        [Pure] public abstract IEnumerator<Code> GetEnumerator();
        
        [Pure] public virtual bool Equals (ICodeSet that)
        {
            return ICodeSetService.Equals (this, that);
        }

        [Pure] public int CompareTo (ICodeSet that) {
            return ICodeSetService.CompareTo (this, that);
        }

        #endregion

        #region Equals<object> & GetHashCode()

        [Pure] public override bool Equals(object obj)
        {
            ICodeSet that = obj as ICodeSet;
            if (that.IsNull()) return false;
            return ICodeSetService.Equals(this, that);
        }
        
        [Pure] public override int GetHashCode()
        {
            return ICodeSetService.GetHashCode(this);
        }
        
        [Pure] public static bool operator ==(CodeSet lhs, CodeSet rhs)
        {
            return lhs.Equals(rhs);
        }
        
        [Pure] public static bool operator !=(CodeSet lhs, CodeSet rhs)
        {
            return !lhs.Equals(rhs);
        }

        #endregion

        #region IEqualityComparer<ICodeSet>

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [Pure] public bool Equals(ICodeSet a, ICodeSet b) {
            return ICodeSetService.Equals(a, b);
        }
        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [Pure] public int GetHashCode(ICodeSet c) {
            return ICodeSetService.GetHashCode(c);
        }

        #endregion

        #region ICollection
        
        [Pure] public abstract int Count { get; }
        
        [Pure] public bool IsReadOnly {
            get {
                Contract.Ensures (Contract.Result<bool>() == true);
                return true;
            }
        }

        /// <summary>Returns True if collection Contains (IsSupersetOf) specified code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>bool</returns>
        //[ContractOption("contract", "inheritance", false)]
        //[SuppressMessage("Microsoft.Contracts", "CC1033", Justification = "Contract inheritance option disabled. Ensures is just stupid")]
        [Pure] public bool Contains(Code code) {
            Contract.Ensures (Contract.Result<bool>() == this[code]);
            return this[code];
        }

        /// <summary>Copies members from this collection into Code[]
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrIndex"></param>
        //[ContractOption("contract", "inheritance", false)]
        [SuppressMessage("Microsoft.Contracts", "CC1033", Justification = "Contract inheritance option disabled")]
        [Pure] public void CopyTo(Code[] array, int arrayIndex) {
            Contract.Requires<ArgumentNullException>(!array.Is(null));
            Contract.Requires<ArgumentOutOfRangeException>(arrayIndex >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(arrayIndex <= (array.Length - this.Count));
            foreach (Code code in this) {
                array[arrayIndex] = code;
                ++arrayIndex;
            }
        }

        /// <summary>Explicit interface implementation.<para>Operations not supported on Read-Only Collection</para>
        /// </summary>
        /// <param name="code"></param>
        [Pure] void ICollection<Code>.Add(Code code) { throw new NotSupportedException(); }

        /// <summary>Explicit interface implementation.<para>Operations not supported on Read-Only Collection</para>
        /// </summary>
        [Pure] void ICollection<Code>.Clear() { throw new NotSupportedException(); }

        /// <summary>Explicit interface implementation.<para>Operations not supported on Read-Only Collection</para>
        /// </summary>
        [Pure] bool ICollection<Code>.Remove(Code code) { throw new NotSupportedException(); }

        #endregion

        /// <summary>Private ContractClass for abstract methods not defined in ICodeSetInterface
        /// </summary>
        [ContractClassFor(typeof(CodeSet))]
        private abstract class CodeSetContractClass : CodeSet {
    
            private CodeSetContractClass() {}
    
            //[ContractOption("contract", "inheritance", false)]
            [Pure] public override int Count {
                get {
                    Contract.Ensures (Contract.Result<int> () == ((IEnumerable<Code>)this).Count ());
                    return default(int);
                }
            }
        }
    }

}
