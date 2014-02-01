// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace DD.Collections.BitSetArrayTest.Interfaces {

    [TestFixture]
    public class AsICloneable {

        public static IEnumerable<BitSetArray> SetValueSource {
            get {
                yield return new BitSetArray ();
                yield return BitSetArray.From (1);
                yield return BitSetArray.From (1, new int[0]);
                yield return BitSetArray.From (1, new int[] { 3, 4, 5 });
                yield return BitSetArray.From (2, 3, 4, 5, 6, 7, 8, 65);
                yield return BitSetArray.From (2, 3, 4, 5, 6, 7, 8, 1028);
                yield return BitSetArray.From (1, 2, 3, 4, 5, 0, 64, 65, 127, 128);
            }
        }

        [Test, TestCaseSource ("SetValueSource")]
        public void Clone (BitSetArray me) {
            var clone = (BitSetArray)me.Clone ();
            Assert.That (clone.Count == me.Count);
            Assert.That (clone.Length == me.Length);
            Assert.That (clone.SetEquals (me));
        }

    }
}
