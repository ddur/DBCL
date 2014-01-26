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

    public struct Loop : IEnumerable<int> {

        public readonly int Times;
        public int Start;
        public int Step;
        IEnumerator<int> e;

        public Loop (int times, int start = 0, int step = 1) {
            Contract.Requires<ArgumentException> (Loop.IsValid (times, start, step));
            this.Times = times;
            this.Start = start;
            this.Step = step;
            this.e = null;
        }

        [Pure]
        public static bool IsValid (int times, int start, int step) {
            if ( times <= 0 )
                return false;
            if ( step == 0 )
                return false;
            try {
                checked {
                    int check = start + ((times - 1) * step);
                }
                return true;
            }
            catch ( OverflowException ) {
                return false;
            }
        }

        public Loop From (int start) {
            Contract.Requires<ArgumentException> (Loop.IsValid (this.Times, start, this.Step));
            this.Start = start;
            return this;
        }

        public Loop By (int step) {
            Contract.Requires<ArgumentException> (Loop.IsValid (this.Times, this.Start, step));
            this.Step = step;
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
            int count = this.Times;
            long value = Start;
            while ( count > 0 && int.MinValue <= value && value <= int.MaxValue ) {
                yield return (int)value;
                value += this.Step;
                --count;
            }
            yield break;
        }

        public static implicit operator Range (Loop loop) {
            return (new Range (loop.Start, loop.Start + ((loop.Times - 1) * loop.Step)).By (loop.Step));
        }
    }
}
