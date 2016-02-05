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
            var result = new List<Code>();
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
            var compare = new List<int> ();
            foreach (var item in code) {
                compare.Add (item);
            }

            // assert
            Assert.True (result.SequenceEqual(compare));
        }
    }
}
