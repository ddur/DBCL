// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DD.Enumerables {

    public struct Range : IEnumerable<int> {

        /// <summary>First item in (IEnumerable&lt;int&gt;)this
        /// </summary>
        public readonly int First;

        /// <summary>Last item in (IEnumerable&lt;int&gt;)this
        /// </summary>
        public readonly int Last;

        private int step;
        private IEnumerator<int> e;

        /// <summary>Get/Set Step&lt;int&gt;
        /// <remarks>If First+Step is not inside Range (IEnumerable&lt;int&gt;)this will return only one item, First item</remarks>
        /// </summary>
        public int Step {
            get {
                return this.step;
            }
            set {
                Contract.Requires<ArgumentException> (value != 0);
                this.step = value;
            }
        }
        
        /// <summary>Creates IEnumerable&lt;int&gt; Range(start, final) inclusive
        /// <remarks>Default Step is 1/-1 depending on start&amp;final</remarks>
        /// </summary>
        /// <param name="start">First item in IEnumerable&lt;int&gt;</param>
        /// <param name="final">Last item in IEnumerable&lt;int&gt;</param>
        public Range (int start, int final) {
            this.First = start;
            this.Last = final;
            if ( this.First <= this.Last ) {
                this.step = 1;
            }
            else {
                this.step = -1;
            }
            this.e = null;
        }

        /// <summary>Fluent set Step
        /// </summary>
        /// <param name="step">-&gt; Step</param>
        /// <returns>this</returns>
        public Range By (int step) {
            Contract.Requires<ArgumentException> (step != 0);
            
            this.step = step;
            return this;
        }

        /// <summary>Returns true 1+(Last-First/First-Last) Times
        /// <para><code>ie: while(range.Do) {}</code></para> 
        /// </summary>
        public bool Do {
            get {
                if ( this.e.IsNull () ) {
                    this.e = this.GetEnumerator ();
                }
                if ( this.e.MoveNext () ) {
                    return true;
                }
                else {
                    this.e.Dispose ();
                    this.e = null;
                    return false;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator () {
            return this.GetEnumerator ();
        }

        public IEnumerator<int> GetEnumerator () {
            long value = this.First;
            int start = this.First <= this.Last? this.First : this.Last;
            int final = this.First <= this.Last? this.Last : this.First;

            while ( start <= value && value <= final ) {
                    yield return (int)value;
                    value += this.Step;
            }
            yield break;
        }

        public static implicit operator Loop (Range range) {

            Contract.Assume (range.step != 0);

            int span = 1 + (range.First <= range.Last? range.Last - range.First : range.First - range.Last);
            int step = Math.Abs (range.Step);

            if (step.InRange (1, span)) {
                return new Loop ( span/step, range.First, range.Step);
            }
            return new Loop (1, range.First, range.Step);
        }
    }
}
