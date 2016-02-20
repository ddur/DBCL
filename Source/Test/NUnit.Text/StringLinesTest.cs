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

            // assert per line
            Assert.True (source.Count == count);
            Assert.True (source.GetLine(0) == string.Empty);
            for (int i = 1; i <= count; i++) {
                Assert.True (source.GetLine(i) == expected [i-1]);
            }
            Assert.True (source.GetLine(count + 1) == string.Empty);

            // assert per char
            Assert.True (source.GetLineAt(0) == string.Empty);
            if (!string.IsNullOrEmpty (input)) {
                int expectedOffset = 0;
                int expectedIndex = 0;
                for (int position = 1; position <= input.Length; position++) {
                    Assert.True (source.GetLineAt(position) != string.Empty);
                    if ((position - expectedOffset) > expected [expectedIndex].Length) {
                        expectedOffset += expected [expectedIndex].Length;
                        expectedIndex += 1;
                    }
                    Assert.True (source.GetLineAt(position) == expected [expectedIndex]);
                }
                Assert.True (source.GetLineAt(input.Length + 1) == string.Empty);
            } else {
                Assert.True (source.GetLineAt(1) == string.Empty);
            }

            // act
            var result = new List<string>();
            foreach (var item in (IEnumerable)source) {
                result.Add ((string)item);
            }

            var e = input.GetLineEnumerator();
            var compare = new List<string>();
            while (e.MoveNext()) {
                compare.Add (e.Current);
            }

            // assert
            Assert.True (result.SequenceEqual (compare));
            Assert.True (result.SequenceEqual (expected));
            if (string.IsNullOrEmpty (input)) {
                Assert.True (source.StringSource == string.Empty);
            } else {
                Assert.True (source.StringSource == input);
            }
        }
    }
}
