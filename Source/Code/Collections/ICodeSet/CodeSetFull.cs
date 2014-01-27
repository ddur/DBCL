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

    /// <summary>Range Set
    /// <remarks>Space efficient, O(k)</remarks>
    /// </summary>
    public sealed class CodeSetFull : CodeSet {

        #region Ctor

        internal CodeSetFull (Code first, Code last) {
            Contract.Requires<ArgumentException> ((first + ICodeSetService.PairCount) < last);

            // Input -> Output
            Contract.Ensures (this.First == first);
            Contract.Ensures (this.Last == last);
            Contract.Ensures (this.Count > ICodeSetService.PairCount);
            Contract.Ensures (this.Count == this.Length);

            this.start = first;
            this.final = last;
        }

        #endregion

        #region Fields

        private readonly Code start;
        private readonly Code final;

        #endregion

        #region ICodeSet

        [Pure] public override bool this[Code code] {
            get {
                return this.start <= code && code <= this.final;
            }
        }

        [Pure] public override int Count {
            get {
                return this.Length;
            }
        }

        [Pure] public override Code First {
            get {
                return this.start;
            }
        }

        [Pure] public override Code Last {
            get {
                return this.final;
            }
        }

        [Pure] public override IEnumerator<Code> GetEnumerator () {
            for ( Code code = this.start; code <= this.final; code++ ) {
                yield return code;
            }
        }

        #endregion

        #region Invariant

        [ContractInvariantMethod]
        private void Invariant () {
            // public <- private
            Contract.Invariant (this.First == this.start);
            Contract.Invariant (this.Last == this.final);

            // public
            Contract.Invariant (this.Count > ICodeSetService.PairCount);
            Contract.Invariant (this.Count == this.Length);
        }

        #endregion

    }
}
