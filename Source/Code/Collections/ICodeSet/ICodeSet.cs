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

    /// <summary>ICodeSet interface
    /// <remarks>ICodeSet implementation requires sorted/ordered IEnumerable&lt;Code&gt;</remarks>
    /// </summary>
    [ContractClass (typeof (ICodeSetContractClass))]
    public interface ICodeSet :
        ICollection<Code>, IEnumerable<Code>, IEquatable<ICodeSet>,
        IEqualityComparer<ICodeSet>, IComparable<ICodeSet> {

        [Pure]
        bool this[Code code] {
            get;
        }

        [Pure]
        bool this[int code] {
            get;
        }

        [Pure]
        int Length {
            get;
        }

        [Pure]
        Code First {
            get;
        }

        [Pure]
        Code Last {
            get;
        }

        [Pure]
        bool IsReduced {
        	get;
        }
    }

    [ContractClassFor (typeof (ICodeSet))]
    public abstract class ICodeSetContractClass : ICodeSet {

        #region Ctor
        #endregion

        #region Contract ICodeSet

        [Pure]
        public bool this[Code code] {
            get {
                Contract.Ensures (Theory.Indexer (this, code.Value, Contract.Result<bool> ()));
                return default (bool);
            }
        }

        [Pure]
        public bool this[int value] {
            get {
                Contract.Ensures (Theory.Indexer (this, value, Contract.Result<bool> ()));
                return default (bool);
            }
        }

        [Pure]
        public abstract int Length { get; }

        [Pure]
        public abstract Code First { get; }

        [Pure]
        public abstract Code Last { get; }

        [Pure]
        public abstract bool IsReduced { get; }

        #endregion

        #region Contract Invariant

        [ContractInvariantMethod]
        private void Invariant () {
            Contract.Invariant (Theory.Invariant (this));
        }

        #endregion

        #region Inherited Interfaces

        IEnumerator IEnumerable.GetEnumerator () {
            return this.GetEnumerator ();
        }

        public abstract IEnumerator<Code> GetEnumerator ();

        public abstract bool Equals (ICodeSet that);

        public abstract override bool Equals (object obj);

        public abstract override int GetHashCode ();

        public abstract bool Equals (ICodeSet a, ICodeSet b);

        public abstract int GetHashCode (ICodeSet that);

        public abstract int CompareTo (ICodeSet that);

        #region ICollection<Code>

        public abstract int Count { get; }
        public abstract bool IsReadOnly { get; }

        public abstract bool Contains (Code code);

        public abstract void CopyTo (Code[] array, int arrayIndex);

        public abstract void Add (Code code);

        public abstract bool Remove (Code code);

        public abstract void Clear ();

        #endregion

        #endregion

        #region Theory

        private static class Theory {

            [Pure]
            public static bool Indexer (ICodeSet self, int value, bool result) {
                Success success = true;

                if (!value.HasCodeValue ()) {
                    success.Assert (!result);
                }
                else {
                    success.Assert (result == ((IEnumerable<Code>)self).Contains ((Code)value));
                }

                return success;
            }

            /// <summary>ICodeSet Contract Invariant</summary>
            /// <remarks>ICodeSet is ReadOnly and all methods are [Pure]</remarks>
            /// <remarks>Invariant is checked only after construction</remarks>
            /// <param name="self">ICodeSet</param>
            /// <returns>bool</returns>
            [Pure]
            public static bool Invariant (ICodeSet self) {
                Success success = true;

                success.Assert (self.IsNot (null));
                success.Assert (self.IsReadOnly);

                success.Assert (self.Count >= Code.MinCount);
                success.Assert (self.Count <= Code.MaxCount);
                success.Assert (self.Count == ((IEnumerable<Code>)self).Count ());

                success.Assert (self.Length >= Code.MinCount);
                success.Assert (self.Length <= Code.MaxCount);
                success.Assert (self.Length >= self.Count);

                if (self.Count != 0) {
                    success.Assert (!(
                        self is CodeSetNone
                    ));

                    success.Assert (self.First >= Code.MinValue);
                    success.Assert (self.First <= Code.MaxValue);
                    success.Assert (self.Last >= Code.MinValue);
                    success.Assert (self.Last <= Code.MaxValue);
                    success.Assert (self.First <= self.Last);

                    success.Assert (self[self.First]);
                    success.Assert (self[self.Last]);

                    success.Assert (self.First == ((IEnumerable<Code>)self).Min ());
                    success.Assert (self.Last == ((IEnumerable<Code>)self).Max ());
                    success.Assert (self.Length == 1 + self.Last - self.First);

                    // interface implementation must be sorted(ordered) set
                    success.Assert (((IEnumerable<Code>)self).SequenceEqual (((IEnumerable<Code>)self).OrderBy (item => (item))));
                }
                else {
                    success.Assert (self.Length == 0);
                    success.Assert (
                        self is CodeSetNone
                    );
                }

                return success;
            }
        }

        #endregion
    }
}
