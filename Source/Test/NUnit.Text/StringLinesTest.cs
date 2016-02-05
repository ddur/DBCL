// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------


using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

namespace DD.Text
{
    [TestFixture]
    public class LineSourceTest
    {
        private IEnumerable<TestCaseData> StringLines {
            get {
                return DataSource.StringLines;
            }
        }

        [Test, TestCaseSource ("StringLines")]
        public void ConstructAndUse(string input, int count, string[] expected)
        {
            // arrange
            var source = new LineSource (input);

            // assert
            Assert.True (source.Count == count);
            Assert.True (source.GetLine(0) == string.Empty);
            for (int i = 1; i <= count; i++) {
                Assert.True (source.GetLine(i) != string.Empty);
            }
            Assert.True (source.GetLine(count + 1) == string.Empty);

            // act
            var result = new List<string>();
            foreach (var item in (IEnumerable)source) {
                result.Add ((string)item);
            }

            var te = input.GetLineEnumerator();
            var compare = new List<string>();
            foreach (var item in te) {
                compare.Add (item);
            }

            // assert
            Assert.True (result.SequenceEqual (compare));
            Assert.True (result.SequenceEqual (expected));
            if (string.IsNullOrEmpty (input)) {
                Assert.True (source.Source == string.Empty);
            } else {
                Assert.True (source.Source == input);
            }
        }
    }
}
