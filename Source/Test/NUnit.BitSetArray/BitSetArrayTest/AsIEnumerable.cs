// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace DD.Collections.BitSetArrayTest.Interfaces {

    [TestFixture]
    public class AsIEnumerable {
        public static IEnumerable<int[]> SetInputSource {
            get {
                yield return new int[0];
                yield return new int[] { 1, 1 };
                yield return new int[] { 2, 3, 4, 5, 6, 7, 8, 65 };
                yield return new int[] { 2, 3, 4, 5, 6, 7, 8, 1028 };
                yield return new int[] { 1, 2, 3, 4, 5, 0, 0, 64, 65, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175, 176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 192, 193, 194, 195, 196, 197, 198, 199, 200, 255, 256, 511, 512 };
            }
        }

        public static IEnumerable<BitSetArray> SetValueSource {
            get {
                foreach ( var input in SetInputSource ) {
                    yield return BitSetArray.From (input);
                }
            }
        }

        [Test]
        public void EnumerableThrows ([ValueSource ("SetValueSource")] BitSetArray me) {

            IEnumerator<int> e = me.GetEnumerator ();
            Assert.That (delegate {
                e.Reset ();
            }, Throws.TypeOf<NotSupportedException> ());
            Assert.That (delegate {
                int i = e.Current;
            }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("The enumerator is not positioned within collection."));
            if ( e.MoveNext () ) {
                me[e.Current] = false;
                Assert.That (delegate {
                    int i = e.Current;
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
                Assert.That (delegate {
                    e.MoveNext ();
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
            }

            IEnumerator oe = ((IEnumerable)me).GetEnumerator ();
            Assert.That (delegate {
                oe.Reset ();
            }, Throws.TypeOf<NotSupportedException> ());
            Assert.That (delegate {
                int i = (int)oe.Current;
            }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("The enumerator is not positioned within collection."));
            if ( oe.MoveNext () ) {
                me[(int)oe.Current] = false;
                Assert.That (delegate {
                    int i = (int)oe.Current;
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
                Assert.That (delegate {
                    oe.MoveNext ();
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
            }

            IEnumerator<int> ec = me.GetEnumeratorComplement ();
            Assert.That (delegate {
                ec.Reset ();
            }, Throws.TypeOf<NotSupportedException> ());
            Assert.That (delegate {
                int i = ec.Current;
            }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("The enumerator is not positioned within collection."));
            if ( ec.MoveNext () ) {
                me[ec.Current] = true;
                Assert.That (delegate {
                    int i = ec.Current;
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
                Assert.That (delegate {
                    ec.MoveNext ();
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
            }

            IEnumerator oec = (IEnumerator)(me.GetEnumeratorComplement ());
            Assert.That (delegate {
                oec.Reset ();
            }, Throws.TypeOf<NotSupportedException> ());
            Assert.That (delegate {
                int i = (int)oec.Current;
            }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("The enumerator is not positioned within collection."));
            if ( oec.MoveNext () ) {
                me[(int)oec.Current] = true;
                Assert.That (delegate {
                    int i = (int)oec.Current;
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
                Assert.That (delegate {
                    oec.MoveNext ();
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
            }

            IEnumerator<int> er = me.GetEnumeratorReverse ();
            Assert.That (delegate {
                er.Reset ();
            }, Throws.TypeOf<NotSupportedException> ());
            Assert.That (delegate {
                int i = er.Current;
            }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("The enumerator is not positioned within collection."));
            if ( er.MoveNext () ) {
                me[er.Current] = false;
                Assert.That (delegate {
                    int i = er.Current;
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
                Assert.That (delegate {
                    er.MoveNext ();
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
            }

            IEnumerator oer = (IEnumerator)(me.GetEnumeratorReverse ());
            Assert.That (delegate {
                oer.Reset ();
            }, Throws.TypeOf<NotSupportedException> ());
            Assert.That (delegate {
                int i = (int)oer.Current;
            }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("The enumerator is not positioned within collection."));
            if ( oer.MoveNext () ) {
                me[(int)oer.Current] = false;
                Assert.That (delegate {
                    int i = (int)oer.Current;
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
                Assert.That (delegate {
                    oer.MoveNext ();
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
            }
        }

        [Test, Sequential]
        public void ForEach ([ValueSource ("SetValueSource")] BitSetArray me, [ValueSource ("SetInputSource")] int[] input) {
            List<int> output = new List<int> ();

            foreach ( var item in me ) {
                me[item] = true; // setting member allready set will not change version and/or trigger exception
                output.Add (item);
            }
            Assert.That (output.Count == input.Distinct ().Count ());
            if ( output.Count > 0 ) {
                Assert.That (output.SequenceEqual (input.Distinct ().OrderBy (item => (item))));
            }

            output = new List<int> ();
            foreach ( var item in me.Reverse () ) {
                me[item] = true; // setting member allready set will not change version and/or trigger exception
                output.Add (item);
            }
            Assert.That (output.Count == input.Distinct ().Count ());
            if ( output.Count > 0 ) {
                Assert.That (output.SequenceEqual (input.Distinct ().OrderBy (item => (0 - item))));
            }

            output = new List<int> ();
            foreach ( var item in me.Complement () ) {
                me[item] = false; // resetting member allready un-set will not change version and/or trigger exception
                output.Add (item);
            }
            if ( output.Count > 0 ) {
                Assert.That (input.Length != 0); // fails in Visual NUnit - old NUnit version?
                Assert.That (output.Count == (input.Max () + 1 - input.Distinct ().Count ()));
                Assert.That (output.SequenceEqual ((BitSetArray.Copy (me)).Not ()));
            }

            me.Length = 32;
            me.Length = 1024;
            foreach ( var item in me ) {
            }
            foreach ( var item in me.Reverse () ) {
            }
            foreach ( var item in me.Complement () ) {
            }
        }

        [TestFixtureSetUp]
        public void Init () {
            GC.Collect ();
        }

        [TestFixtureTearDown]
        public void Dispose () {
            GC.Collect ();
        }
    }
}
