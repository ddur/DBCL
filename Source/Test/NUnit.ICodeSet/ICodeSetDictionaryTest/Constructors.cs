﻿// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;

using DD.Collections.Generic;

using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetUniqueTest {

    [TestFixture]
    public class Constructors {

        [Test]
        public void Construct () {
            var icsDict = new ICodeSetUnique ();
            var icsDictTyped = new HashDictionary<ICodeSet, Code> ();
        }
    }
}
