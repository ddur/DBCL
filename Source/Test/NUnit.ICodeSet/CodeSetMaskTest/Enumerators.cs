// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

using DD.Collections;

namespace DD.Collections.ICodeSet.CodeSetMaskTest
{
    [TestFixture]
    public class Enumerators
    {
        readonly CodeSetMask csm = CodeSetMask.From (new Code[] {
            1114111,
            2,
            2,
            22,
            50,
            100,
            200,
            500,
            1000,
            10000,
            100000,
            1000000,
            1,
            0,
            65536,
            128000,
            512000
        });

        [Test]
        public void AsIEnumerableOfCode()
        {
            var e = csm.GetEnumerator();
            Assert.That (delegate {
                e.Reset ();
            }, Throws.TypeOf<NotSupportedException> ());

            Assert.That (delegate {
                var i = e.Current;
            }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("The enumerator is not positioned within collection."));

            while (e.MoveNext()) {
                var x = e.Current;
            }
            Assert.False (e.MoveNext());
        }


        [Test]
        public void AsIEnumerableOfObject()
        {
            var oe = ((IEnumerable)csm).GetEnumerator();

            Assert.That (delegate {
                oe.Reset ();
            }, Throws.TypeOf<NotSupportedException> ());

            Assert.That (delegate {
                var i = (Code)oe.Current;
            }, Throws.TypeOf<InvalidOperationException> ().With.Message.EqualTo ("The enumerator is not positioned within collection."));

            while (oe.MoveNext()) {
                var x = oe.Current;
            }
            Assert.False (oe.MoveNext());
        }


    }
}
