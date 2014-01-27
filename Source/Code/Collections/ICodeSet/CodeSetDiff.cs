// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DD.Collections {

    /// <summary>Difference of two ICodeSet's under ICodeSet constrains
    /// <remarks>Space efficient, O(k)</remarks>
    /// </summary>
    public sealed class CodeSetDiff : CodeSet {

        #region Ctor

        /// <summary>Constructor
        /// Set a is full set
        /// set a.First &lt; b.First b.Last &lt; a.Last
        /// </summary>
        internal CodeSetDiff (ICodeSet a, ICodeSet b) {
            Contract.Requires<ArgumentNullException> (!a.IsNull ());
            Contract.Requires<ArgumentNullException> (!b.IsNull ());

            Contract.Requires<ArgumentException> (!a.IsEmpty ());
            Contract.Requires<ArgumentException> (!b.IsEmpty ());

            Contract.Requires<ArgumentException> ((a.Count - b.Count) > ICodeSetService.ListMaxCount);

            // ensures first & last members
            Contract.Requires<ArgumentException> (a.First < b.First);
            Contract.Requires<ArgumentException> (b.Last < a.Last);

            Contract.Ensures (this.aSet.IsNot (null));
            Contract.Ensures (this.bSet.IsNot (null));
            Contract.Ensures (this.aSet.Is (a));
            Contract.Ensures (this.bSet.Is (b));

            // Input -> Output
            Contract.Ensures (this.Count > ICodeSetService.ListMaxCount);
            Contract.Ensures (this.Count == a.Count - b.Count);
            Contract.Ensures (Contract.ForAll (a, item => this[item] == this.aSet[item] && !this.bSet[item]));
            Contract.Ensures (Contract.ForAll (b, item => !this[item]));
            Contract.Ensures (this.First == this.aSet.First);
            Contract.Ensures (this.Last == this.aSet.Last);

            this.aSet = a;
            this.bSet = b;
        }

        #endregion

        #region Fields

        private readonly ICodeSet aSet;
        private readonly ICodeSet bSet;

        #endregion

        #region ICodeSet

        [Pure] public override bool this[Code code] {
            get {
                return this.aSet[code] && !this.bSet[code];
            }
        }

        [Pure] public override int Count {
            get {
                return aSet.Count - bSet.Count;
            }
        }

        [Pure] public override Code First {
            get {
                return this.aSet.First;
            }
        }

        [Pure] public override Code Last {
            get {
                return this.aSet.Last;
            }
        }

        [Pure] public override IEnumerator<Code> GetEnumerator () {
            foreach ( Code code in this.aSet ) {
                if ( !this.bSet[code] ) {
                    yield return code;
                }
            }
        }

        #endregion

        #region Invariant

        [ContractInvariantMethod]
        private void Invariant () {
            // private
            Contract.Invariant (this.aSet.IsNot (null));
            Contract.Invariant (this.bSet.IsNot (null));
            
            // public <- private
            Contract.Invariant (this.Count == aSet.Count - bSet.Count);
            Contract.Invariant (this.First == aSet.First);
            Contract.Invariant (this.Last == aSet.Last);

            // public
            Contract.Invariant (this.Count > ICodeSetService.ListMaxCount);
            Contract.Invariant (this.Count < Code.MaxCount);
        }

        #endregion
        
    }
}
