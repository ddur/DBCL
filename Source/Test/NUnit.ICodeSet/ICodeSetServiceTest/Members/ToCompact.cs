﻿// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetServiceTest.Members {

    [TestFixture]
    public class ToCompact {

        [Test]
        public void FromNullOrEmpty () {
            BitSetArray compact;
            ICodeSet a = null;

            compact = a.ToCompact ();
            Assert.IsTrue (compact.Count == 0);
            Assert.IsTrue (compact.IsCompact ());

            a = CodeSetNone.Singleton;
            compact = a.ToCompact ();
            Assert.IsTrue (compact.Count == 0);
            Assert.IsTrue (compact.IsCompact ());
        }

        [Test]
        public void FromCodeSetPage () {
            BitSetArray compact;
            ICodeSet a = CodeSetPage.From (22, 65, 77);

            compact = a.ToCompact ();
            Assert.IsTrue (compact.Count == 3);
            Assert.IsTrue (compact.IsCompact ());

            a = CodeSetPage.From (20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 33);
            compact = a.ToCompact ();
            Assert.IsTrue (compact.Count == 11);
            Assert.IsTrue (compact.IsCompact ());
        }

        [Test]
        public void FromICodeSet () {
            BitSetArray compact;
            ICodeSet a = new Code (20);

            compact = a.ToCompact ();
            Assert.IsTrue (compact.Count == 1);
            Assert.IsTrue (compact.IsCompact ());

            a = CodeSetPair.From (22, 65);
            compact = a.ToCompact ();
            Assert.IsTrue (compact.Count == 2);
            Assert.IsTrue (compact.IsCompact ());

            a = CodeSetList.From (22, 65, 77);
            compact = a.ToCompact ();
            Assert.IsTrue (compact.Count == 3);
            Assert.IsTrue (compact.IsCompact ());

            a = CodeSetFull.From (20, 29);
            compact = a.ToCompact ();
            Assert.IsTrue (compact.Count == 10);
            Assert.IsTrue (compact.IsCompact ());
        }
    }
}