// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetServiceTest.Members {

    [TestFixture]
    public class ToCompact {

        [Test]
        public void FromNullOrEmpty () {
            ICodeSet a = null;

            Tuple <BitSetArray, int> compact = null;
            Assert.IsTrue (!compact.IsCompact ());

            compact = a.ToCompact ();
            Assert.IsTrue (compact.IsNot (null));
            Assert.IsTrue (compact.IsCompact ());
            Assert.IsTrue (compact.Item1.Count == 0);
            Assert.IsTrue (compact.Item1.IsCompact ());
            Assert.IsTrue (compact.Item2 == 0);

            a = CodeSetNone.Singleton;
            compact = a.ToCompact ();
            Assert.IsTrue (compact.IsNot (null));
            Assert.IsTrue (compact.IsCompact ());
            Assert.IsTrue (compact.Item1.Count == 0);
            Assert.IsTrue (compact.Item1.IsCompact ());
            Assert.IsTrue (compact.Item2 == 0);
        }

        [Test]
        public void FromCodeSetMask () {
            ICodeSet a = CodeSetMask.From (22, 65, 77);

            var compact = a.ToCompact ();
            Assert.IsTrue (compact.IsNot (null));
            Assert.IsTrue (compact.IsCompact ());
            Assert.IsTrue (compact.Item1.Count == 3);
            Assert.IsTrue (compact.Item1.IsCompact ());
            Assert.IsTrue (compact.Item2 == 22);

            a = CodeSetMask.From (20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 33);
            compact = a.ToCompact ();
            Assert.IsTrue (compact.IsNot (null));
            Assert.IsTrue (compact.IsCompact ());
            Assert.IsTrue (compact.Item1.Count == 11);
            Assert.IsTrue (compact.Item1.IsCompact ());
            Assert.IsTrue (compact.Item2 == 20);
        }

        [Test]
        public void FromICodeSet () {
            ICodeSet a = new Code (20);

            var compact = a.ToCompact ();
            Assert.IsTrue (compact.IsNot (null));
            Assert.IsTrue (compact.IsCompact ());
            Assert.IsTrue (compact.Item1.Count == 1);
            Assert.IsTrue (compact.Item1.IsCompact ());
            Assert.IsTrue (compact.Item2 == 20);

            a = CodeSetPair.From (22, 65);
            compact = a.ToCompact ();
            Assert.IsTrue (compact.IsNot (null));
            Assert.IsTrue (compact.IsCompact ());
            Assert.IsTrue (compact.Item1.Count == 2);
            Assert.IsTrue (compact.Item1.IsCompact ());
            Assert.IsTrue (compact.Item2 == 22);

            a = CodeSetList.From (22, 65, 77);
            compact = a.ToCompact ();
            Assert.IsTrue (compact.IsNot (null));
            Assert.IsTrue (compact.IsCompact ());
            Assert.IsTrue (compact.Item1.Count == 3);
            Assert.IsTrue (compact.Item1.IsCompact ());
            Assert.IsTrue (compact.Item2 == 22);

            a = CodeSetFull.From (20, 29);
            compact = a.ToCompact ();
            Assert.IsTrue (compact.IsNot (null));
            Assert.IsTrue (compact.IsCompact ());
            Assert.IsTrue (compact.Item1.Count == 10);
            Assert.IsTrue (compact.Item1.IsCompact ());
            Assert.IsTrue (compact.Item2 == 20);
        }
    }
}
