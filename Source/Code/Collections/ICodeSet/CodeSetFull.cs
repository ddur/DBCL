﻿// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
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
            Contract.Requires<ArgumentException> ((first + CodeSet.PairCount) <= last);
            Contract.Ensures (Contract.Result<CodeSetFull> ().IsNot (null));

            return new CodeSetFull (first, last);
        }

        internal CodeSetFull (Code first, Code last) {
            Contract.Requires<ArgumentException> ((first + CodeSet.PairCount) <= last);

            // Input -> private
            Contract.Ensures (this.start == first);
            Contract.Ensures (this.final == last);

            this.start = first.Value;
            this.final = last.Value;
            this.count = 1 + this.final - this.start;
        }

        #endregion

        #region Fields

        private readonly int start;
        private readonly int final;
        private readonly int count;

        #endregion

        #region ICodeSet

        [Pure]
        public override bool this[Code code] {
            get {
                return this.start <= code.Value && this.final >= code.Value;
            }
        }

        [Pure]
        public override bool this[int value] {
            get {
                return this.start <= value && this.final >= value;
            }
        }

        [Pure]
        public override bool IsEmpty {
            get {
                return false;
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
        public override bool IsReduced {
            get {
                return Count > CodeSet.PairCount;
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
            Contract.Invariant (this.final - this.start >= CodeSet.PairCount);
            Contract.Invariant (this.count == 1 + this.final - this.start);
            Contract.Invariant (this.count.IsCodesCount ());

            // public <- private
            Contract.Invariant (this.Count == this.count);
            Contract.Invariant (this.Length == this.count);
            Contract.Invariant (this.First == this.start);
            Contract.Invariant (this.Last == this.final);

            // constraints
            Contract.Invariant (this.Count == this.Length);
            Contract.Invariant (this.Count > CodeSet.PairCount);
        }

        #endregion
    }
}
