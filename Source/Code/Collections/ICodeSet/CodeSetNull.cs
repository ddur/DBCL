// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DD.Collections
{
    /// <summary>Empty ICodeSet, private constructor
    /// <remarks>Singleton</remarks>
    /// </summary>
    public sealed class CodeSetNull : CodeSet {
        
        #region Ctor

        private CodeSetNull() {
        }

        #endregion
        
        #region Fields

        public static readonly CodeSetNull Singleton = new CodeSetNull();

        #endregion

        #region ICodeSet

        [Pure] public override bool this [Code code] {
            get { return false; }
        }

        [Pure] public override int Count {
	        get { return 0; }
        }

        /// <summary>Throws InvalidOperationException
	    /// </summary>
        [Pure] public override Code First {
	        get { throw new InvalidOperationException(); }
        }

	    /// <summary>Throws InvalidOperationException
	    /// </summary>
        [Pure] public override Code Last {
	        get { throw new InvalidOperationException(); }
        }

	    [Pure] public override IEnumerator<Code> GetEnumerator() {
            yield break;
        }

        #endregion

        #region Invariant

        [ContractInvariantMethod]
        private void Invariant () {
            // public
            Contract.Invariant (this.Count == 0);
            Contract.Invariant (this.Length == 0);
        }

        #endregion

    }
}
