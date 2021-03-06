﻿// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetDiffTest {

    [TestFixture]
    public class Constructors {

        [Test]
        public void FromTwoSets () {
            CodeSetDiff csd;

            csd = CodeSetDiff.From (CodeSetFull.From (Code.MinValue, Code.MaxValue), new Code (Code.MaxValue / 2));
            csd = CodeSetDiff.From (CodeSetFull.From (Code.MinValue, Code.MaxValue), CodeSetPair.From (100, 200));
            csd = CodeSetDiff.From (CodeSetFull.From (Code.MinValue, Code.MaxValue), CodeSetFull.From (100, 200));
        }

        [Test]
        public void FromTwoSetsThrows () {
            CodeSetDiff csd;

            // requires no null
            Assert.Throws<ArgumentNullException> (
                delegate { csd = CodeSetDiff.From ((ICodeSet)null, CodeSetPair.From (0, 100)); }
            );
            Assert.Throws<ArgumentNullException> (
                delegate { csd = CodeSetDiff.From (CodeSetFull.From (0, 100), (ICodeSet)null); }
            );

            // requires no empty arguments
            Assert.Throws<ArgumentEmptyException> (
                delegate { csd = CodeSetDiff.From (CodeSetNone.Singleton, CodeSetPair.From (0, 100)); }
            );
            Assert.Throws<ArgumentEmptyException> (
                delegate { csd = CodeSetDiff.From (CodeSetFull.From (0, 100), CodeSetNone.Singleton); }
            );

            // requires a.Count = a.Length
            Assert.Throws<ArgumentException> (
                delegate { csd = CodeSetDiff.From (CodeSetMask.From (new Code[] { 0, 1, 3, 4 }), CodeSetPair.From (1, 3)); }
            );
            // requires a is CodeSetFull
            Assert.Throws<ArgumentException> (
                delegate { csd = CodeSetDiff.From (CodeSetMask.From (new Code[] { 0, 1, 2, 3, 4 }), CodeSetPair.From (1, 3)); }
            );

            // requires a.Last > b.Last
            Assert.Throws<ArgumentException> (
                delegate { csd = CodeSetDiff.From (CodeSetFull.From (0, 100), CodeSetPair.From (1, 100)); }
            );
            Assert.Throws<ArgumentException> (
                delegate { csd = CodeSetDiff.From (CodeSetFull.From (0, 100), CodeSetPair.From (1, 101)); }
            );

            // requires a.First < b.First
            Assert.Throws<ArgumentException> (
                delegate { csd = CodeSetDiff.From (CodeSetFull.From (1, 100), CodeSetPair.From (1, 99)); }
            );
            Assert.Throws<ArgumentException> (
                delegate { csd = CodeSetDiff.From (CodeSetFull.From (1, 100), CodeSetPair.From (0, 99)); }
            );

            // requires .Count > 2
            Assert.Throws<ArgumentException> (
                delegate { csd = CodeSetDiff.From (CodeSetFull.From (1, 4), CodeSetPair.From (2, 3)); }
            );
        }
    }
}
