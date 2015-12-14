// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DD.Collections.ICodeSet {

    /// <summary>Empty ICodeSet, private constructor
    /// <remarks>Allways empty, singleton</remarks>
    /// </summary>
    public sealed class CodeSetNone : CodeSet {

        #region Ctor

        private CodeSetNone () { }

        #endregion

        #region Fields

        public static readonly CodeSetNone Singleton = new CodeSetNone ();

        #endregion

        #region ICodeSet

        [Pure]
        public override bool this[Code code] {
            get {
                Contract.Ensures ( Contract.Result<bool> () == false );
                return false;
            }
        }

        [Pure]
        public override int Count {
            get {
                Contract.Ensures ( Contract.Result<int> () == 0 );
                return 0;
            }
        }

        [Pure]
        public override int Length {
            get {
                Contract.Ensures ( Contract.Result<int> () == 0 );
                return 0;
            }
        }

        /// <summary>Throws InvalidOperationException
        /// </summary>
        [Pure]
        public override Code First {
            get { throw new InvalidOperationException (); }
        }

        /// <summary>Throws InvalidOperationException
        /// </summary>
        [Pure]
        public override Code Last {
            get { throw new InvalidOperationException (); }
        }

        [Pure]
        public override IEnumerator<Code> GetEnumerator () {
            yield break;
        }

        #endregion

        #region Invariant

        [ContractInvariantMethod]
        private void Invariant () {
            // constraints
            Contract.Invariant ( this.Count == 0 );
            Contract.Invariant ( this.Length == 0 );
        }

        #endregion
    }
}