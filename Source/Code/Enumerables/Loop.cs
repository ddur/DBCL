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

    /// <summary>foreach in Loop IEnumerable&lt;int&gt; returns N items where N &gt; 0
    /// </summary>
    public struct Loop : IEnumerable<int> {

        /// <summary>Number of: Times to loop.Do / Items in IEnumerable&lt;int&gt; 
        /// </summary>
        public readonly int Times;

        private int start;
        private int step;
        private IEnumerator<int> e;

        /// <summary>Get/Set First&lt;int&gt;
        /// </summary>
        public int First {
            get {
                return this.start;
            }
            set {
                Contract.Requires<ArgumentException> (Loop.IsValid (this.Times, value, this.Step));
                this.start = value;
            }
        }
        
        /// <summary>Get Last&lt;int&gt; item
        /// </summary>
        public int Last {
            get {
                if (this.Times == 1) {
                    return this.First;
                }
                return (int)((long)this.First + (((long)this.Times - 1) * (long)this.Step));
            }
        }

        /// <summary>Get/Set Step&lt;int&gt;
        /// </summary>
        public int Step {
            get {
                return this.step;
            }
            set {
                Contract.Requires<ArgumentException> (Loop.IsValid (this.Times, this.First, value));
                this.step = value;
            }
        }

        [ContractInvariantMethod]
        private void Invariant() {
        	// TODO Theory
            Contract.Invariant (this.Times > 0);
            Contract.Invariant (((long)this.First + (((long)this.Times-1) * (long)this.Step)).InRange(int.MinValue, int.MaxValue));
            Contract.Invariant (((long)this.First + (((long)this.Times-1) * (long)this.Step)) == this.Last);
        }

        /// <summary>Creates IEnumerable&lt;int&gt; Loop with "times" items 
        /// </summary>
        /// <param name="times">-&gt; Times</param>
        /// <param name="start">(default 0) -&gt; First item</param>
        /// <param name="step">(default 1) -&gt; Next item == Prev. item + step ...</param>
        public Loop (int times, int start = 0, int step = 1) {
            Contract.Requires<ArgumentException> (Loop.IsValid (times, start, step));
            this.Times = times;
            this.start = start;
            this.step = step;
            this.e = null;
        }

        /// <summary>Validate Loop arguments 
        /// </summary>
        /// <param name="times">-&gt; Times</param>
        /// <param name="start">-&gt; First item</param>
        /// <param name="step">-&gt; Next item == Prev. item + step ...</param>
        /// <returns>true for valid arguments/fields</returns>
        [Pure]
        public static bool IsValid (int times, int start, int step) {
            if ( times > 0 ) {
                long last = (long)start + (((long)times-1) * (long)step);
                if (int.MinValue <= last && last <= int.MaxValue) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Fluent set First
        /// </summary>
        /// <param name="start">-&gt; First item</param>
        /// <returns>this</returns>
        public Loop From (int start) {
            Contract.Requires<ArgumentException> (Loop.IsValid (this.Times, start, this.Step));
            this.start = start;
            return this;
        }

        /// <summary>Fluent set Step
        /// </summary>
        /// <param name="step">-&gt; Step</param>
        /// <returns>this</returns>
        public Loop By (int step) {
            Contract.Requires<ArgumentException> (Loop.IsValid (this.Times, this.First, step));
            this.step = step;
            return this;
        }

        /// <summary>Returns true N Times
        /// <para><code>ie: while(loop.Do) {}</code></para> 
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
            int count = this.Times;
            long value = First;
            while (count > 0) {
                yield return (int)value;
                value += this.Step;
                --count;
            }
        }

        public static implicit operator Range (Loop loop) {

            return (new Range (loop.First, loop.Last).By (loop.Step));
        }
    }
}
