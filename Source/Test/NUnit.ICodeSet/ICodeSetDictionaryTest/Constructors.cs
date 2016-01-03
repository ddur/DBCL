// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetDictionaryTest {

    [TestFixture]
    public class Constructors {

        [Test]
        public void Construct () {
            var icsDict = new ICodeSetDictionary ();
            var icsDictTyped = new Dictionary<Code> ();
        }
    }
}
