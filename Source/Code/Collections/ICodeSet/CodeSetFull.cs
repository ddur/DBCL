// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DD.Collections.ICodeSet {

    /// <summary>Full Range Set</summary>
    /// <remarks>Cannot be empty, allways full, contains at least 3, up to <see cref="Code.MaxCodeCount">Code.MaxCodeCount</see> codes</remarks>
    [Serializable]
    public sealed class CodeSetFull : CodeSet {

        #region Ctor

        public static CodeSetFull From (Code first, Code last) {
            Contract.Requires<InvalidOperationException> ((first + ICodeSetService.PairCount) <= last);
            Contract.Ensures (Contract.Result<CodeSetFull> ().IsNot (null));

            return new CodeSetFull (first, last);
        }

        internal CodeSetFull (Code first, Code last) {
            Contract.Requires<InvalidOperationException> ((first + ICodeSetService.PairCount) <= last);

            // Input -> private
            Contract.Ensures (this.start == first);
            Contract.Ensures (this.final == last);

            this.start = first;
            this.final = last;
            this.count = 1 + this.final - this.start;
        }

        #endregion

        #region Fields

        private readonly Code start;
        private readonly Code final;
        private readonly int count;

        #endregion

        #region ICodeSet

        [Pure]
        public override bool this[Code code] {
            get {
                return this.start <= code && this.final >= code;
            }
        }

        [Pure]
        public override int Count {
            get {
                return this.count;
            }
        }

        [Pure]
        public override int Length {
            get {
                return this.count;
            }
        }

        [Pure]
        public override Code First {
            get {
                return (Code)this.start;
            }
        }

        [Pure]
        public override Code Last {
            get {
                return (Code)this.final;
            }
        }

        [Pure]
        public override IEnumerator<Code> GetEnumerator () {
            for (int item = this.start; item <= this.final; item++) {
                yield return (Code)item;
            }
        }

        #endregion

        #region Invariant

        [ContractInvariantMethod]
        private void Invariant () {
            // private
            Contract.Invariant (this.start < this.final);
            Contract.Invariant (this.final - this.start >= ICodeSetService.PairCount);
            Contract.Invariant (this.count == 1 + this.final - this.start);
            Contract.Invariant (this.count.IsCodesCount ());

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