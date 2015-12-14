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
                foreach (var input in SetInputSource) {
                    yield return BitSetArray.From (input);
                }
            }
        }

        [Test]
        public void EnumerableThrows ([ValueSource ("SetValueSource")] BitSetArray me) {
            var generic_enumerator_forward = me.GetEnumerator ();
            Assert.That (delegate {
                generic_enumerator_forward.Reset ();
            }, Throws.TypeOf<NotSupportedException> ());
            Assert.That (delegate {
                int i = generic_enumerator_forward.Current;
            }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("The enumerator is not positioned within collection."));
            if (generic_enumerator_forward.MoveNext ()) {
                me[generic_enumerator_forward.Current] = false;
                Assert.That (delegate {
                    int i = generic_enumerator_forward.Current;
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
                Assert.That (delegate {
                    generic_enumerator_forward.MoveNext ();
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
            }

            var object_enumerator_forward = ((IEnumerable)me).GetEnumerator ();
            Assert.That (delegate {
                object_enumerator_forward.Reset ();
            }, Throws.TypeOf<NotSupportedException> ());
            Assert.That (delegate {
                int i = (int)object_enumerator_forward.Current;
            }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("The enumerator is not positioned within collection."));
            if (object_enumerator_forward.MoveNext ()) {
                me[(int)object_enumerator_forward.Current] = false;
                Assert.That (delegate {
                    int i = (int)object_enumerator_forward.Current;
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
                Assert.That (delegate {
                    object_enumerator_forward.MoveNext ();
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
            }

            var generic_enumerator_complement = me.GetEnumeratorComplement ();
            Assert.That (delegate {
                generic_enumerator_complement.Reset ();
            }, Throws.TypeOf<NotSupportedException> ());
            Assert.That (delegate {
                int i = generic_enumerator_complement.Current;
            }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("The enumerator is not positioned within collection."));
            if (generic_enumerator_complement.MoveNext ()) {
                me[generic_enumerator_complement.Current] = true;
                Assert.That (delegate {
                    int i = generic_enumerator_complement.Current;
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
                Assert.That (delegate {
                    generic_enumerator_complement.MoveNext ();
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
            }

            var object_enumerator_complement = (IEnumerator)(me.GetEnumeratorComplement ());
            Assert.That (delegate {
                object_enumerator_complement.Reset ();
            }, Throws.TypeOf<NotSupportedException> ());
            Assert.That (delegate {
                int i = (int)object_enumerator_complement.Current;
            }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("The enumerator is not positioned within collection."));
            if (object_enumerator_complement.MoveNext ()) {
                me[(int)object_enumerator_complement.Current] = true;
                Assert.That (delegate {
                    int i = (int)object_enumerator_complement.Current;
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
                Assert.That (delegate {
                    object_enumerator_complement.MoveNext ();
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
            }

            var generic_enumerator_reverse = me.GetEnumeratorReverse ();
            Assert.That (delegate {
                generic_enumerator_reverse.Reset ();
            }, Throws.TypeOf<NotSupportedException> ());
            Assert.That (delegate {
                int i = generic_enumerator_reverse.Current;
            }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("The enumerator is not positioned within collection."));
            if (generic_enumerator_reverse.MoveNext ()) {
                me[generic_enumerator_reverse.Current] = false;
                Assert.That (delegate {
                    int i = generic_enumerator_reverse.Current;
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
                Assert.That (delegate {
                    generic_enumerator_reverse.MoveNext ();
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
            }

            var object_enumerator_reverse = (IEnumerator)(me.GetEnumeratorReverse ());
            Assert.That (delegate {
                object_enumerator_reverse.Reset ();
            }, Throws.TypeOf<NotSupportedException> ());
            Assert.That (delegate {
                int i = (int)object_enumerator_reverse.Current;
            }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("The enumerator is not positioned within collection."));
            if (object_enumerator_reverse.MoveNext ()) {
                me[(int)object_enumerator_reverse.Current] = false;
                Assert.That (delegate {
                    int i = (int)object_enumerator_reverse.Current;
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
                Assert.That (delegate {
                    object_enumerator_reverse.MoveNext ();
                }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("Collection was modified; enumeration operation may not execute."));
            }
        }

        [Test, Sequential]
        public void ForEach ([ValueSource ("SetValueSource")] BitSetArray me, [ValueSource ("SetInputSource")] int[] input) {
            List<int> output;

            #region Forward enumerators

            // default - synchronised enumerator
            output = new List<int> ();
            foreach (var item in me) {
                me[item] = true; // setting member allready set will not change version and/or trigger exception
                output.Add (item);
            }
            Assert.That (output.Count == input.Distinct ().Count ());
            if (output.Count > 0) {
                Assert.That (output.SequenceEqual (input.Distinct ().OrderBy (item => (item))));
            }

            // unsynchronised enumerator
            output = new List<int> ();
            var forwardEnumerator = new BitSetArray.EnumeratorForward (me);
            while (forwardEnumerator.MoveNext ()) {
                me[forwardEnumerator.Current] = true; // setting member allready set will not change version and/or trigger exception
                output.Add (forwardEnumerator.Current);
            }
            Assert.That (output.Count == input.Distinct ().Count ());
            if (output.Count > 0) {
                Assert.That (output.SequenceEqual (input.Distinct ().OrderBy (item => (item))));
            }

            // readonly enumerator
            output = new List<int> ();
            var forwardReadOnlyEnumerator = new BitSetArray.EnumeratorForwardReadOnly (me);
            while (forwardReadOnlyEnumerator.MoveNext ()) {
                me[forwardReadOnlyEnumerator.Current] = false; // clearing member of original will not affect copy
                me[forwardReadOnlyEnumerator.Current] = true;  // reset value to compare result
                output.Add (forwardReadOnlyEnumerator.Current);
            }
            Assert.That (output.Count == input.Distinct ().Count ());
            if (output.Count > 0) {
                Assert.That (output.SequenceEqual (input.Distinct ().OrderBy (item => (item))));
            }

            #endregion

            #region Reverse Enumerators

            // default synchronized reverse enumerator
            output = new List<int> ();
            foreach (var item in me.Reverse ()) {
                me[item] = true; // setting member allready set will not change version and/or trigger exception
                output.Add (item);
            }
            Assert.That (output.Count == input.Distinct ().Count ());
            if (output.Count > 0) {
                Assert.That (output.SequenceEqual (input.Distinct ().OrderBy (item => (0 - item))));
            }

            // unsynchronized reverse enumerator
            output = new List<int> ();
            var reverseEnumerator = new BitSetArray.EnumeratorReverse (me);
            while (reverseEnumerator.MoveNext ()) {
                me[reverseEnumerator.Current] = true; // setting member allready set will not change version and/or trigger exception
                output.Add (reverseEnumerator.Current);
            }
            Assert.That (output.Count == input.Distinct ().Count ());
            if (output.Count > 0) {
                Assert.That (output.SequenceEqual (input.Distinct ().OrderBy (item => (0 - item))));
            }

            // readonly reverse enumerator
            output = new List<int> ();
            var reverseReadOnlyEnumerator = new BitSetArray.EnumeratorReverseReadOnly (me);
            while (reverseReadOnlyEnumerator.MoveNext ()) {
                me[reverseReadOnlyEnumerator.Current] = false; // clearing member of original will not affect copy
                me[reverseReadOnlyEnumerator.Current] = true;  // reset value to compare result
                output.Add (reverseReadOnlyEnumerator.Current);
            }
            Assert.That (output.Count == input.Distinct ().Count ());
            if (output.Count > 0) {
                Assert.That (output.SequenceEqual (input.Distinct ().OrderBy (item => (0 - item))));
            }

            #endregion

            #region Complement enumerators

            // default synchronised complement enumerator
            output = new List<int> ();
            foreach (var item in me.Complement ()) {
                me[item] = false; // resetting member allready un-set will not change version and/or trigger exception
                output.Add (item);
            }
            if (output.Count > 0) {
                Assert.That (input.Length != 0); // fails in Visual NUnit - old NUnit version?
                Assert.That (output.Count == (input.Max () + 1 - input.Distinct ().Count ()));
                Assert.That (output.SequenceEqual ((BitSetArray.Copy (me)).Not ()));
            }

            // unsynchronized complement enumerator
            output = new List<int> ();
            var complementEnumerator = new BitSetArray.EnumeratorComplement (me);
            while (complementEnumerator.MoveNext ()) {
                me[complementEnumerator.Current] = false; // resetting member allready un-set will not change version and/or trigger exception
                output.Add (complementEnumerator.Current);
            }
            if (output.Count > 0) {
                Assert.That (input.Length != 0);
                Assert.That (output.Count == (input.Max () + 1 - input.Distinct ().Count ()));
                Assert.That (output.SequenceEqual ((BitSetArray.Copy (me)).Not ()));
            }

            // readonly complement enumerator
            output = new List<int> ();
            var complementReadOnlyEnumerator = new BitSetArray.EnumeratorComplementReadOnly (me);
            while (complementReadOnlyEnumerator.MoveNext ()) {
                me[complementReadOnlyEnumerator.Current] = true; // setting member of original will not affect copy
                me[complementReadOnlyEnumerator.Current] = false; // reset value to compare result
                output.Add (complementReadOnlyEnumerator.Current);
            }
            if (output.Count > 0) {
                Assert.That (input.Length != 0);
                Assert.That (output.Count == (input.Max () + 1 - input.Distinct ().Count ()));
                Assert.That (output.SequenceEqual ((BitSetArray.Copy (me)).Not ()));
            }

            #endregion
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
