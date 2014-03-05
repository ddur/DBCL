// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DD.Collections {

    /// <summary>Full Range Set</summary>
    public sealed class CodeSetFull : CodeSet {

        #region Ctor

        internal CodeSetFull (Code first, Code last) {
            Contract.Requires<ArgumentException> ((first + ICodeSetService.PairCount) <= last);

            // Input -> private
            Contract.Ensures (this.start == first);
            Contract.Ensures (this.final == last);
            Contract.Ensures (this.count == 1 + this.final - this.start);

            this.start = first;
            this.final = last;
			this.count = 1 + this.final - this.start;
        }

        #endregion

        #region Fields

        private readonly int start;
        private readonly int final;
		private readonly int count;

        #endregion

        #region ICodeSet

        [Pure] public override bool this[Code code] {
            get {
                return this.start <= code && this.final >= code;
            }
        }

        [Pure] public override int Count {
            get {
                return this.count;
            }
        }

        [Pure] public override int Length {
            get {
                return this.count;
            }
        }

        [Pure] public override Code First {
            get {
				return (Code)this.start;
            }
        }

        [Pure] public override Code Last {
            get {
                return (Code)this.final;
            }
        }

        [Pure] public override IEnumerator<Code> GetEnumerator () {
            for ( int item = this.start; item <= this.final; item++ ) {
                yield return (Code)item;
            }
        }

        #endregion

        #region Invariant

        [ContractInvariantMethod]
        private void Invariant () {
            // public <- private
  	        Contract.Invariant (this.Count == this.count);
  	        Contract.Invariant (this.Length == this.count);
            Contract.Invariant (this.First == this.start);
            Contract.Invariant (this.Last == this.final);

            // constraints
            Contract.Invariant (this.Count == this.Length);
            Contract.Invariant (this.Count > ICodeSetService.PairCount);
        }

        #endregion

    }
}
