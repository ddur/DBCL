// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace DD.Enumerables {

    public struct Range : IEnumerable<int> {

        public readonly int Start;
        public readonly int Final;
        public int Step;
        IEnumerator<int> e;

        public Range (int start, int final) {
            this.Start = start;
            this.Final = final;
            if ( this.Start <= this.Final ) {
                this.Step = 1;
            }
            else {
                this.Step = -1;
            }
            this.e = null;
        }

        public Range By (int step) {
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
            long value = this.Start;
            if ( this.Step == 0 ) {
                yield return (int)value;
            }
            else {
                while ( int.MinValue <= value && value <= int.MaxValue
                       && ((int)value).InRange (this.Start, this.Final) ) {
                    yield return (int)value;
                    value += this.Step;
                }
            }
            yield break;
        }

        public static implicit operator Loop (Range range) {
            if ( range.Start <= range.Final ) {
                return new Loop ((1 + range.Final - range.Start) / range.Step, range.Start, range.Step);
            }
            else {
                return new Loop ((1 + range.Start - range.Final) / Math.Abs (range.Step), range.Start, range.Step);
            }
        }
    }
}
