// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using DD.Collections.ICodeSet;

namespace DD.Text
{
    [TestFixture]
    public class StringExtensionsTest
    {
        [Test]
        public void GetLineEnumeratorTest()
        {
            // arrange & act
            var e = "\r\r\r\n \r\n \r\n \r \n \n\n\n\r\n\n".GetLineEnumerator();
            var lines = new List<string> ();
            foreach (var item in e) {
                lines.Add (item);
            }

            // assert
            Assert.True (lines.Count == 12);
        }

        private IEnumerable<TestCaseData> ValidString {
            get {
                return DataSource.ValidEncodedString;
            }
        }

        private IEnumerable<string> InvalidString {
            get {
                return DataSource.InvalidEncodedString;
            }
        }

        [Test, TestCaseSource ("ValidString")]
        public void GetCodeEnumeratorValidTest (string data, IEnumerable<Code> code)
        {
            // arrange & act
            var result = new List<Code>();
            var e = data.GetCodeEnumerator();
            foreach (var item in e) {
                result.Add (item);
            }

            // assert
            Assert.True (result.SequenceEqual (code));
        }

        [Test, TestCaseSource ("InvalidString")]
        public void GetCodeEnumeratorInvalidTest (string data)
        {
            // arrange
            var e = data.GetCodeEnumerator();

            // act & assert
            Assert.That (
                delegate {
                    foreach (var item in e) {}
                }, Throws.ArgumentException);
        }

        [Test, TestCaseSource ("ValidString")]
        public void GetIntEnumeratorValidTest (string data, IEnumerable<Code> code)
        {
            // arrange & act
            var result = new List<int>();
            var e = data.GetIntEnumerator();
            foreach (var item in e) {
                result.Add (item);
            }

            // assert
            int index = 0;
            foreach (var item in code) {
                Assert.True (result[index] == item.Value);
                index += 1;
            }
        }

        [Test, TestCaseSource ("InvalidString")]
        public void ToCodesInvalidTest (string data)
        {
            // arrange & act & assert
            Assert.That (
                delegate {
                    data.ToCodes();
                }, Throws.ArgumentException);
        }

        [Test, TestCaseSource ("ValidString")]
        public void ToCodesValidTest (string data, IEnumerable<Code> code)
        {
            // arrange & act
            var result = data.ToCodes();

            // assert
            Assert.True (result.SequenceEqual(code));
        }

        [Test, TestCaseSource ("InvalidString")]
        public void ToIntCodesInvalidTest (string data)
        {
            // arrange & act & assert
            Assert.That (
                delegate {
                    data.ToIntCodes();
                }, Throws.ArgumentException);
        }

        [Test, TestCaseSource ("ValidString")]
        public void ToIntCodesValidTest (string data, IEnumerable<Code> code)
        {
            // arrange & act
            int[] result = data.ToIntCodes();

            // assert
            int index = 0;
            foreach (var item in code) {
                Assert.True (result[index] == item.Value);
                index += 1;
            }
        }

        [Test]
        public void ToLinesTest()
        {
            // arrange & act
            const string input = "\r\r\r\n \r\n \r\n \r \n \n\n\n\r\n\n";
            var lines = input.ToLines();

            // assert
            Assert.True (lines.Length == 12);
        }
    }
}
