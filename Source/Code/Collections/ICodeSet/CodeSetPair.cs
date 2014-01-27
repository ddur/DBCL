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
    /// <summary>Set of two items (codes)
    /// <remarks>Space efficient, O(k)</remarks>
    /// </summary>
    public sealed class CodeSetPair : CodeSet {

        #region Ctor

        internal CodeSetPair (Code first, Code last) {
            Contract.Requires<ArgumentException> (first < last);

            // Input -> Output
            Contract.Ensures (this.Count == ICodeSetService.PairCount);
            Contract.Ensures (this[first] && this[last]);

            this.one = first;
            this.two = last;
        }

        #endregion

        #region Fields

        private readonly Code one;
	    private readonly Code two;

	    #endregion

	    #region ICodeSet

	    [Pure] public override bool this [Code code] {
            get { return code == this.one || code == this.two; }
        }

	    [Pure] public override int Count {
	        get { return ICodeSetService.PairCount; }
        }

	    [Pure] public override Code First {
            get { return this.one; }
        }

	    [Pure] public override Code Last {
            get { return this.two; }
        }

	    [Pure] public override IEnumerator<Code> GetEnumerator() {
            yield return this.one;
            yield return this.two;
        }

        #endregion
        
        #region Invariant
        
        [ContractInvariantMethod]
        private void Invariant () {
            // private
            Contract.Invariant (this.one != this.two);

            // public <- private
            Contract.Invariant (this.First == this.one);
            Contract.Invariant (this.Last == this.two);
            
            // public
            Contract.Invariant (this.Count == ICodeSetService.PairCount);
            Contract.Invariant (this.First != this.Last);
        }

        #endregion
        
	}
}
