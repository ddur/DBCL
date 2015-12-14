// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

namespace DD {

    [TestFixture]
    public class ExtensionsTest {

        [Test]
        public void IntInRange () {
            Int_InRangeExtension (-100);
            Int_InRangeExtension (0);
            Int_InRangeExtension (100);

            Assert.That (int.MinValue.InRange (int.MinValue, int.MinValue + 1), Is.True);
            Assert.That (int.MaxValue.InRange (int.MaxValue - 1, int.MaxValue), Is.True);
        }

        public static void Int_InRangeExtension (int testVal) {
            Assert.That (testVal.InRange (testVal - 1, testVal + 1), Is.True);
            Assert.That (testVal.InRange (testVal + 1, testVal - 1), Is.False);

            Assert.That (testVal.InRange (testVal, testVal), Is.True);
            Assert.That (testVal.InRange (testVal, testVal - 1), Is.False);

            Assert.That (testVal.InRange (testVal + 1, testVal + 2), Is.False);
            Assert.That (testVal.InRange (testVal - 2, testVal - 1), Is.False);
        }

        [Test]
        public void LongInRange () {
            Assert.True (((long)int.MinValue).InRange (int.MinValue, int.MaxValue));
            Assert.True (((long)int.MaxValue).InRange (int.MinValue, int.MaxValue));
            Assert.False (((long)int.MinValue - 1L).InRange (int.MinValue, int.MaxValue));
            Assert.False (((long)int.MaxValue + 1L).InRange (int.MinValue, int.MaxValue));
            Assert.False ((long.MinValue).InRange (int.MinValue, int.MaxValue));
            Assert.False ((long.MaxValue).InRange (int.MinValue, int.MaxValue));
        }

        // user defined enumerable class that is not collection<T> nor array<T>
        public class TestEnumClass<T> : IEnumerable<T> {
            private IEnumerable<T> items = new T[0];

            public TestEnumClass (IEnumerable<T> items) {
                this.items = items;
            }

            IEnumerator IEnumerable.GetEnumerator () {
                return this.GetEnumerator ();
            }

            public IEnumerator<T> GetEnumerator () {
                foreach (var item in this.items) {
                    yield return item;
                }
            }
        }

        [Test]
        public void IEnumIsEmpty () {
            List<string> testEmpty = null;
            Assert.Throws<ArgumentNullException> (delegate {
                testEmpty.IsEmpty ();
            });
            Assert.True (testEmpty.IsNullOrEmpty ());

            Assert.Throws<ArgumentNullException> (delegate {
                ((IEnumerable)testEmpty).IsEmpty ();
            });
            Assert.True (((IEnumerable)testEmpty).IsNullOrEmpty ());

            testEmpty = new List<string> ();
            Assert.True (testEmpty.IsEmpty ());
            Assert.True (((IEnumerable)testEmpty).IsEmpty ());
            Assert.True ((testEmpty.ToArray ()).IsEmpty ());
            Assert.True ((new TestEnumClass<string> (testEmpty)).IsEmpty ());
            Assert.True (((IEnumerable)new TestEnumClass<string> (testEmpty)).IsEmpty ());

            Assert.True (testEmpty.IsNullOrEmpty ());
            Assert.True (((IEnumerable)testEmpty).IsNullOrEmpty ());
            Assert.True ((testEmpty.ToArray ()).IsNullOrEmpty ());
            Assert.True ((new TestEnumClass<string> (testEmpty)).IsNullOrEmpty ());
            Assert.True (((IEnumerable)new TestEnumClass<string> (testEmpty)).IsNullOrEmpty ());

            List<string> testNotEmptyNulls = new List<string> { null, null, null };
            Assert.False (testNotEmptyNulls.IsEmpty ());
            Assert.False (((IEnumerable)testNotEmptyNulls).IsEmpty ());
            Assert.False ((testNotEmptyNulls.ToArray ()).IsEmpty ());
            Assert.False ((new TestEnumClass<string> (testNotEmptyNulls)).IsEmpty ());
            Assert.False (((IEnumerable)new TestEnumClass<string> (testNotEmptyNulls)).IsEmpty ());

            Assert.False (testNotEmptyNulls.IsNullOrEmpty ());
            Assert.False (((IEnumerable)testNotEmptyNulls).IsNullOrEmpty ());
            Assert.False ((testNotEmptyNulls.ToArray ()).IsNullOrEmpty ());
            Assert.False ((new TestEnumClass<string> (testNotEmptyNulls)).IsNullOrEmpty ());
            Assert.False (((IEnumerable)new TestEnumClass<string> (testNotEmptyNulls)).IsNullOrEmpty ());

            List<string> testNotEmpty = new List<string> { "a", "b", "c", "d" };
            Assert.False (testNotEmpty.IsEmpty ());
            Assert.False (((IEnumerable)testNotEmpty).IsEmpty ());
            Assert.False ((testNotEmpty.ToArray ()).IsEmpty ());
            Assert.False ((new TestEnumClass<string> (testNotEmpty)).IsEmpty ());
            Assert.False (((IEnumerable)new TestEnumClass<string> (testNotEmpty)).IsEmpty ());

            Assert.False (testNotEmpty.IsNullOrEmpty ());
            Assert.False (((IEnumerable)testNotEmpty).IsNullOrEmpty ());
            Assert.False ((testNotEmpty.ToArray ()).IsNullOrEmpty ());
            Assert.False ((new TestEnumClass<string> (testNotEmpty)).IsNullOrEmpty ());
            Assert.False (((IEnumerable)new TestEnumClass<string> (testNotEmpty)).IsNullOrEmpty ());
        }

        [Test]
        public void ObjIs () {
            List<string> testObj = new List<string> { "", "", "" };
            List<string> testObj2 = testObj;

            Assert.That (!testObj.Is (null));
            Assert.That (testObj.Is (testObj));
            Assert.That (testObj.Is (testObj2));
            Assert.That (((object)null).Is (null));
        }

        [Test]
        public void BoolExtension () {
            // cannot reproduce bug in CLR with C#
            // Pex uncovered bug in bool by setting true value from byte 0x80 or 0x20
            // http://social.msdn.microsoft.com/Forums/en-US/50858997-8784-417d-b3f1-d0ada448b79b/pexsafehelpersbytetoboolean
            // http://connect.microsoft.com/VisualStudio/feedback/details/455553/boolean-comparison-in-net#details
            // http://social.msdn.microsoft.com/Forums/en-US/47e69fa8-fb33-4b67-966f-b4ad82a241dd/pexforfun-unsolvable-duels-due-to-crazy-bugs-in-pex
            // http://blog.dotnetwiki.org/default,date,2009-06-02.aspx
            bool x;
            x = true;
            Assert.That (x.Bool (), Is.True);
            x = false;
            Assert.That (x.Bool (), Is.False);
        }
    }
}