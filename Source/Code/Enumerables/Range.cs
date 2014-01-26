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

        public readonly int First;
        public readonly int Last;
        private int step;
        private IEnumerator<int> e;

        public int Step {
            get {
                return this.step;
            }
            set {
                Contract.Requires<ArgumentException> (value != 0);
                this.step = value;
            }
        }
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

        public Range By (int value) {
            Contract.Requires<ArgumentException> (value != 0);
            
            this.step = value;
            return this;
        }

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

            Contract.Assume (range.Step != 0);

            int span = 1 + (range.First <= range.Last? range.Last - range.First : range.First - range.Last);
            int step = Math.Abs (range.Step);

            if (step.InRange (1, span)) {
                return new Loop ( span/step, range.First, range.Step);
            }
            return new Loop (1, range.First, range.Step);
        }
    }
}
