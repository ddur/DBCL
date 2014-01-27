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

using DD.Diagnostics;

namespace DD.Collections {

    /// <summary>CodeSet interface
    /// <remarks>
    /// <para>ICodeSet implementation requires sorted/ordered ISet&lt;Code&gt;/IEnumerable&lt;Code&gt;</para>
    /// <para>HashSet is not sorted! Contract will fail on HashSet based ICodeSet implementation</para>
    /// </remarks>
    /// </summary>
    [ContractClass (typeof (ICodeSetContractClass))]
    public interface ICodeSet : 
        ICollection<Code>, IEnumerable<Code>, IEquatable<ICodeSet>, 
        IEqualityComparer<ICodeSet>, IComparable<ICodeSet> {

        bool this[Code code] {
            get;
        }
        int Length {
            get;
        }
        Code First {
            get;
        }
        Code Last {
            get;
        }

    }

    [ContractClassFor (typeof (ICodeSet))]
    internal abstract class ICodeSetContractClass : ICodeSet {

        #region Ctor

        // make sure this contract class cannot create instance's
        private ICodeSetContractClass () {
        }

        #endregion

        #region Contract ICodeSet

        [Pure]
        public bool this[Code code] {
            get {
                Contract.Ensures (Contract.Result<bool> () == ((IEnumerable<Code>)this).Contains (code));
                return default (bool);
            }
        }

        [Pure]
        public int Length {
            get {
                Contract.Ensures (Contract.Result<int> ().InRange (Code.MinCount, Code.MaxCount));
                Contract.Ensures ((this.Count == 0 && this.Length == 0) ||
                                  (this.Count > 0 && this.Length >= this.Count && this.Length == this.Last - this.First + 1));
                return default (int);
            }
        }

        [Pure]
        public Code First {
            get {
                Contract.Ensures ((Contract.Result<Code> () <= this.Last) && this[Contract.Result<Code> ()]);
                Contract.EnsuresOnThrow<InvalidOperationException> (this.Count == 0);
                return default (Code);
            }
        }

        [Pure]
        public Code Last {
            get {
                Contract.Ensures ((Contract.Result<Code> () >= this.First) && this[Contract.Result<Code> ()]);
                Contract.EnsuresOnThrow<InvalidOperationException> (this.Count == 0);
                return default (Code);
            }
        }

        #endregion

        #region Contract Invariant

        [ContractInvariantMethod]
        private void Invariant () {
            Contract.Invariant (Theory.Invariant (this));
        }

        #endregion

        #region Inherited Interfaces

        [Pure]
        [SuppressMessage ("Microsoft.Contracts", "CC1076", Justification = "Noted")]
        IEnumerator IEnumerable.GetEnumerator () {
            return this.GetEnumerator ();
        }
        [Pure]
        [SuppressMessage ("Microsoft.Contracts", "CC1076", Justification = "Noted")]
        public abstract IEnumerator<Code> GetEnumerator ();
        [Pure]
        [SuppressMessage ("Microsoft.Contracts", "CC1076", Justification = "Noted")]
        public abstract bool Equals (ICodeSet that);
        [Pure]
        [SuppressMessage ("Microsoft.Contracts", "CC1076", Justification = "Noted")]
        public abstract override bool Equals (object obj);
        [Pure]
        [SuppressMessage ("Microsoft.Contracts", "CC1076", Justification = "Noted")]
        public abstract override int GetHashCode ();
        [Pure]
        [SuppressMessage ("Microsoft.Contracts", "CC1076", Justification = "Noted")]
        public abstract bool Equals (ICodeSet a, ICodeSet b);
        [Pure]
        [SuppressMessage ("Microsoft.Contracts", "CC1076", Justification = "Noted")]
        public abstract int GetHashCode (ICodeSet c);
        [Pure]
        [SuppressMessage ("Microsoft.Contracts", "CC1076", Justification = "Noted")]
        public abstract int CompareTo (ICodeSet c);

        #region ICollection<Code>
        [Pure]
        [SuppressMessage ("Microsoft.Contracts", "CC1076", Justification = "Noted")]
        public abstract bool IsReadOnly {
            get;
        }
        [Pure]
        [SuppressMessage ("Microsoft.Contracts", "CC1076", Justification = "Noted")]
        public abstract bool Contains (Code code);
        [Pure]
        [SuppressMessage ("Microsoft.Contracts", "CC1076", Justification = "Noted")]
        public abstract int Count {
            get;
        }
        [Pure]
        [SuppressMessage ("Microsoft.Contracts", "CC1076", Justification = "Noted")]
        public abstract void CopyTo (Code[] array, int arrayIndex);
        [Pure]
        [SuppressMessage ("Microsoft.Contracts", "CC1076", Justification = "Noted")]
        public abstract void Add (Code code);
        [Pure]
        [SuppressMessage ("Microsoft.Contracts", "CC1076", Justification = "Noted")]
        public abstract bool Remove (Code code);
        [Pure]
        [SuppressMessage ("Microsoft.Contracts", "CC1076", Justification = "Noted")]
        public abstract void Clear ();
        #endregion

        #endregion

        #region Theory

        private static class Theory {

            [Pure] public static bool Invariant (ICodeSet self) {
                Success success = true;
                    
                success.Assert (self.IsNot(null));
                success.Assert (self.IsReadOnly);
                success.Assert (self.Count.InRange (0, Code.MaxCount));

                if (self.Count > 0) {
                    
                    //success.Assert (!(self is CodeSetNull));
                    success.Assert (self.Count.InRange (1, self.Length));
                    success.Assert (self.Count == ((IEnumerable<Code>)self).Count ());
                    success.Assert (self.Length == self.Last - self.First + 1);
                    success.Assert (self[self.First]);
                    success.Assert (self[self.Last]);

                    // interface implementation must be sorted(ordered) set
                    success.Assert (((IEnumerable<Code>)self).SequenceEqual (((IEnumerable<Code>)self).OrderBy (item => (item))));
                    
                }
                else { // self.Count == 0
                    //success.Assert (self is CodeSetNull || self is CodeSetBits);
                    success.Assert (self.Length == self.Count);
                }

                return success;
            }
        }

        #endregion

    }
}
