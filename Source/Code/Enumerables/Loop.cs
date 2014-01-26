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
        public int First;
        public int Step;
        IEnumerator<int> e;
        
        public int Last {
            get {
                if (this.Times == 1) {
                    return this.First;
                }
                return (int)((long)this.First + (((long)this.Times - 1) * (long)this.Step));
            }
        }

        public Loop (int times, int start = 0, int step = 1) {
            Contract.Requires<ArgumentException> (Loop.IsValid (times, start, step));
            this.Times = times;
            this.First = start;
            this.Step = step;
            this.e = null;
        }

        [Pure]
        public static bool IsValid (int times, int start, int step) {
            if ( times <= 0 )
                return false;
            if ( times == 1 )
                return true;
            long last = (long)start + (((long)times-1) * (long)step);
            if (int.MinValue <= last && last <= int.MaxValue) {
                return true;
            }
            return false;
        }

        public Loop From (int start) {
            Contract.Requires<ArgumentException> (Loop.IsValid (this.Times, start, this.Step));
            this.First = start;
            return this;
        }

        public Loop By (int step) {
            Contract.Requires<ArgumentException> (Loop.IsValid (this.Times, this.First, step));
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
            long value = First;
            while (count > 0) {
                yield return (int)value;
                value += this.Step;
                --count;
            }
            yield break;
        }

        public static implicit operator Range (Loop loop) {
            long times = loop.Times;
            long start = loop.First;
            long step  = loop.Step;

            long final = start + ((times - 1) * step);
            Contract.Assert (int.MinValue <= final && final <= int.MaxValue);

            return (new Range (loop.First, (int)final).By (loop.Step));
        }
    }
}
