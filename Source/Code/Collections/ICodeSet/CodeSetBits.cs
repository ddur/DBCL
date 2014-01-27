// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DD.Collections
{
    /// <summary>Intermediate ICodeSet that is later optimized into other space efficient ICodeSet implementations
    /// <remarks>Can .Count 0 to 1114112 Code members</remarks>
    /// </summary>
    internal class CodeSetBits : CodeSet
    {
        #region Ctor

        internal CodeSetBits (BitSetArray bits) {

            Contract.Ensures (this.sorted.IsNot (null));

            // Input -> Output
            Contract.Ensures (bits.Is(null) && this.sorted.Count == 0);
            Contract.Ensures (bits.Is(null) || this.sorted.Count == bits.Count);
            Contract.Ensures (bits.Is(null) || Contract.ForAll (bits, item => this[item]));

            if (bits.IsNot(null) && bits.Count != 0) {
                this.start = (int)bits.First;
                this.final = (int)bits.Last;
                this.sorted = new BitSetArray (this.final - this.start + 1);
                foreach ( Code code in bits ) {
                    this.sorted.Set (code - this.start, true);
                }
            }
            else {
                this.sorted = new BitSetArray (this.final - this.start + 1);
                Contract.Assert (this.sorted.Length == 0);
            }
        }

        internal CodeSetBits (IEnumerable<Code> codes) {

            Contract.Ensures (this.sorted.IsNot (null));
            Contract.Ensures (codes.Is(null) || this.sorted.Count != 0);
            Contract.Ensures (codes.Is(null) || this.sorted.Count == codes.Distinct ().Count ());
            Contract.Ensures (codes.Is(null) || Contract.ForAll (codes, item => this[item]));

            if (codes.IsNot(null) && !codes.IsEmpty()) {
                if (codes is ICodeSet) {
                    ICodeSet codeSet = codes as ICodeSet;
                    this.start = codeSet.First;
                    this.final = codeSet.Last;
                }
                else {
                    this.start = int.MaxValue;
                    this.final = int.MinValue;
                    foreach ( Code code in codes ) {
                        if ( code < this.start )
                            this.start = code;
                        if ( code > this.final )
                            this.final = code;
                    }
                }

                // codes is same class or class based on BitSetArray?
                if (codes is CodeSetBits) {
                    this.sorted = ((CodeSetBits)codes).sorted;
                }
                else if (codes is CodeSetPage) {
                    this.sorted = ((CodeSetPage)codes).ToCompact();
                }
                else {
                    this.sorted = new BitSetArray (this.final - this.start + 1);
                    foreach ( Code code in codes ) {
                        this.sorted.Set (code - this.start, true);
                    }
                }
            }
            else {
                this.sorted = new BitSetArray (this.final - this.start + 1);
                Contract.Assert (this.sorted.Count == 0);
                Contract.Assert (this.sorted.Length == 0);
            }
        }

        #endregion

        #region Fields

        private readonly BitSetArray sorted;
        private readonly int start = ICodeSetService.NullStart;
        private readonly int final = ICodeSetService.NullFinal;

        #endregion

        #region ICodeSet

        [Pure] public override bool this[Code code] {
            get {
                return this.Count != 0 ? sorted[code - this.start] : false ;
            }
        }

        [Pure] public override int Count {
            get {
                return sorted.Count;
            }
        }

        [Pure] public override Code First {
            get {
                if (this.Count != 0) return this.start;
                throw new InvalidOperationException();
            }
        }

        [Pure] public override Code Last {
            get {
                if (this.Count != 0) return this.final;
                throw new InvalidOperationException();
            }
        }

        [Pure] public override IEnumerator<Code> GetEnumerator () {
            foreach ( Code code in this.sorted ) {
                yield return this.start + code;
            }
        }

        #endregion

        #region Invariant

        [ContractInvariantMethod]
        private void Invariant () {

            // TODO! move all this to Theory.Method
            // ATTN! Contract ignores branches and checks all Invariants
//            Contract.Invariant (this.sorted.IsNot (null));
//            Contract.Invariant (this.sorted.Length <= Code.MaxCount);
//            Contract.Invariant (this.sorted.Length == 1 + this.final - this.start);
//            Contract.Invariant (this.sorted.Count.InRange (0, this.sorted.Length));
//
//            if (this.sorted.Count > 0) {
//                Contract.Invariant (this.start.HasCodeValue ());
//                Contract.Invariant (this.final.HasCodeValue ());
//                Contract.Invariant (this.sorted[0]);
//                Contract.Invariant (this.sorted[this.final - this.start]);
//            }
//            else {
//                Contract.Invariant (this.start == ICodeSetService.NullStart);
//                Contract.Invariant (this.final == ICodeSetService.NullFinal);
//            }
//            
//            // public <- private
//            Contract.Invariant (this.Length == this.sorted.Length);
//            Contract.Invariant (this.Count == this.sorted.Count);
//            if (this.Count != 0) {
//                Contract.Invariant (this.First == this.start);
//                Contract.Invariant (this.Last == this.start);
//            }
            

        }

        #endregion

        internal BitSetArray ToCompact () {
            return BitSetArray.Copy (this.sorted);
        }
        
        internal IEnumerable<Code> Complement {
            get {
                foreach (int item in this.sorted.Complement()) {
                    yield return item + this.start;
                }
            }
        }

    }
}
