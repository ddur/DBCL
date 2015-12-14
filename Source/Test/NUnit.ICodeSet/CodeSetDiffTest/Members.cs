// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Linq;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetDiffTest {

    [TestFixture]
    public class Members {
        private static readonly CodeSetDiff csd1 = CodeSetDiff.From ( CodeSetFull.From ( Code.MinValue, Code.MaxValue ), new Code ( Code.MaxValue / 2 ) );
        private static readonly CodeSetDiff csd2 = CodeSetDiff.From ( CodeSetFull.From ( 90, 203 ), CodeSetFull.From ( 100, 200 ) );

        [Test]
        public void AsEnumerable () {
            Assert.True ( csd2.SequenceEqual ( new Code[] { 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 201, 202, 203 } ) );
        }

        [Test]
        public void Properties () {
            Assert.True ( csd1.Count == Code.MaxCount - 1 );
            Assert.True ( csd1.Length == Code.MaxCount );
            Assert.True ( csd1.First.Value == Code.MinValue );
            Assert.True ( csd1.Last.Value == Code.MaxValue );

            Assert.True ( csd2.Count == 13 );
            Assert.True ( csd2.Length == 203 - 90 + 1 );
            Assert.True ( csd2.First.Value == 90 );
            Assert.True ( csd2.Last.Value == 203 );
        }

        [Test]
        public void Indexer () {
            var codes = new Code[] { 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 201, 202, 203 };
            foreach (var code in codes) {
                Assert.True ( csd2[code] );
            }
            codes = new Code[] { 0, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 120, 130, 140, 150, 160, 170, 180, 190, 199, 200 };
            foreach (var code in codes) {
                Assert.False ( csd2[code] );
            }
        }
    }
}