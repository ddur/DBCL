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
    public class LineEnumeratorTest
    {
        private IEnumerable<TestCaseData> StringLines {
            get {
                return DataSource.StringLines;
            }
        }

        [Test, TestCaseSource ("StringLines")]
        public void AsIEnumerator(string input, int count, string[] expected) {

            // arrange
            IEnumerator enumerator = new LineEnumerator (input);

            // assert
            Assert.Throws<InvalidOperationException> ( delegate { var s = enumerator.Current; } );
            Assert.Throws<InvalidOperationException> ( delegate { var s = ((IEnumerator)enumerator).Current; } );

            // act
            var result = new List<string> ();
            while (enumerator.MoveNext()) {
                result.Add ((string)enumerator.Current);
            }

            // assert
            Assert.True (result.Count == count);
            Assert.True (result.SequenceEqual (expected));

            Assert.False (enumerator.MoveNext());
            Assert.Throws<InvalidOperationException> ( delegate { var s = enumerator.Current; } );
            Assert.Throws<InvalidOperationException> ( delegate { var s = ((IEnumerator)enumerator).Current; } );

            // arrange again
            enumerator = new LineEnumerator (input);

            // assert
            if (string.IsNullOrEmpty(input)) {
                Assert.False (enumerator.MoveNext());
            } else {
                Assert.True (enumerator.MoveNext());
            }

            // rearange
            enumerator.Reset();

            // act
            result = new List<string> ();
            while (enumerator.MoveNext()) {
                result.Add ((string)enumerator.Current);
            }

            // assert
            Assert.True (result.Count == count);
            Assert.True (result.SequenceEqual (expected));

            Assert.False (enumerator.MoveNext());
            Assert.Throws<InvalidOperationException> ( delegate { var s = enumerator.Current; } );
            Assert.Throws<InvalidOperationException> ( delegate { var s = ((IEnumerator)enumerator).Current; } );

        }

        [Test]
        public void ConstructWithNullString()
        {
            // arrange
            var enumerator = new LineEnumerator (null);

            // act
            var lines = new List<string> ();
            while (enumerator.MoveNext()) {
                lines.Add (enumerator.Current);
            }

            // assert
            Assert.True (lines.Count == 0);
        }

        [Test]
        public void ConstructWithEmptyString()
        {
            // arrange
            var enumerator = new LineEnumerator (string.Empty);

            // act
            var lines = new List<string> ();
            while (enumerator.MoveNext()) {
                lines.Add (enumerator.Current);
            }

            // assert
            Assert.True (lines.Count == 0);
            Assert.False (enumerator.MoveNext());
            Assert.Throws<InvalidOperationException> ( delegate { var s = enumerator.Current; } );
            Assert.Throws<InvalidOperationException> ( delegate { var s = ((IEnumerator)enumerator).Current; } );
        }

        [Test]
        public void ConstructWithSingleLine()
        {
            // arrange
            const string input = "single line";
            var enumerator = new LineEnumerator (input);

            // act
            var lines = new List<string> ();
            while (enumerator.MoveNext()) {
                lines.Add (enumerator.Current);
            }

            // assert
            Assert.True (lines.Count == 1);
            Assert.False (enumerator.MoveNext());
            Assert.Throws<InvalidOperationException> ( delegate { var s = enumerator.Current; } );
            Assert.Throws<InvalidOperationException> ( delegate { var s = ((IEnumerator)enumerator).Current; } );
        }

        [Test]
        public void ConstructWithTwoLines()
        {
            // arrange
            const string input = "\tfirst line\n\tsecond line\r";
            var enumerator = new LineEnumerator (input);

            // act
            var lines = new List<string> ();
            while (enumerator.MoveNext()) {
                lines.Add (enumerator.Current);
            }

            // assert
            Assert.True (lines.Count == 2);
            Assert.False (enumerator.MoveNext());
            Assert.Throws<InvalidOperationException> ( delegate { var s = enumerator.Current; } );
            Assert.Throws<InvalidOperationException> ( delegate { var s = ((IEnumerator)enumerator).Current; } );
        }

        [Test]
        public void ConstructWithTwoLinesNoCrLfAtEof()
        {
            // arrange
            const string input = "\tfirst line\r\tsecond line";
            var enumerator = new LineEnumerator (input);

            // act
            var lines = new List<string> ();
            while (enumerator.MoveNext()) {
                lines.Add (enumerator.Current);
            }

            // assert
            Assert.True (lines.Count == 2);
            Assert.False (enumerator.MoveNext());
            Assert.Throws<InvalidOperationException> ( delegate { var s = enumerator.Current; } );
            Assert.Throws<InvalidOperationException> ( delegate { var s = ((IEnumerator)enumerator).Current; } );
        }

        [Test]
        public void ConstructWithFiveLines()
        {
            // arrange
            const string input = "\tfirst line\n \n\tthird line\r\n \r   fifth line\r";
            var enumerator = new LineEnumerator (input);

            // act
            var lines = new List<string> ();
            while (enumerator.MoveNext()) {
                lines.Add (enumerator.Current);
            }

            // assert
            Assert.True (lines.Count == 5);
            Assert.False (enumerator.MoveNext());
            Assert.Throws<InvalidOperationException> ( delegate { var s = enumerator.Current; } );
            Assert.Throws<InvalidOperationException> ( delegate { var s = ((IEnumerator)enumerator).Current; } );
        }
        
        [Test]
        public void CountLinesWithLineFeed()
        {
            // arrange
            const string input = "\n\n\n\n\n\n\n";
            var enumerator = new LineEnumerator (input);

            // act
            var lines = new List<string> ();
            while (enumerator.MoveNext()) {
                lines.Add (enumerator.Current);
            }

            // assert
            Assert.True (lines.Count == 7);
            Assert.False (enumerator.MoveNext());
            Assert.Throws<InvalidOperationException> ( delegate { var s = enumerator.Current; } );
            Assert.Throws<InvalidOperationException> ( delegate { var s = ((IEnumerator)enumerator).Current; } );
        }
        
        [Test]
        public void CountLinesWithCrLf()
        {
            // arrange
            const string input = "\r\n\r\n\r\n\r\n";
            var enumerator = new LineEnumerator (input);

            // act
            var lines = new List<string> ();
            while (enumerator.MoveNext()) {
                lines.Add (enumerator.Current);
            }

            // assert
            Assert.True (lines.Count == 4);
            Assert.False (enumerator.MoveNext());
            Assert.Throws<InvalidOperationException> ( delegate { var s = enumerator.Current; } );
            Assert.Throws<InvalidOperationException> ( delegate { var s = ((IEnumerator)enumerator).Current; } );
        }

        [Test]
        public void CountLinesWithMixedLineEnd()
        {
            // arrange
            const string input = "\r\r\r\n \r\n \r\n \r \n \n\n\n\r\n\n";
            //                     1 2   3    4    5  6  7  8 910  1112
            var enumerator = new LineEnumerator (input);

            // act
            var lines = new List<string> ();
            while (enumerator.MoveNext()) {
                lines.Add (enumerator.Current);
            }

            // assert
            Assert.True (lines.Count == 12);
            Assert.False (enumerator.MoveNext());
            Assert.Throws<InvalidOperationException> ( delegate { var s = enumerator.Current; } );
            Assert.Throws<InvalidOperationException> ( delegate { var s = ((IEnumerator)enumerator).Current; } );
        }

        [Test]
        public void Dispose()
        {
            // arrange
            const string input = "\r\n\r\n\r\n\r\n";
            var enumerator = new LineEnumerator (input);

            // act
            ((IDisposable)enumerator).Dispose();

            // assert
            Assert.True (enumerator.MoveNext());
            Assert.That ( delegate { var s = enumerator.Current; }, Throws.Nothing );
        }

        [Test]
        public void Reset()
        {
            // arrange
            const string input = "\r\n\r\n\r\n\r\n";
            var enumerator = new LineEnumerator (input);

            // assert
            Assert.Throws<InvalidOperationException> ( delegate { var s = enumerator.Current; } );
            Assert.Throws<InvalidOperationException> ( delegate { var s = ((IEnumerator)enumerator).Current; } );

            // act
            var lines = new List<string> ();
            while (enumerator.MoveNext()) {
                lines.Add (enumerator.Current);
            }
            enumerator.Reset();

            // assert
            Assert.Throws<InvalidOperationException> ( delegate { var s = enumerator.Current; } );
            Assert.Throws<InvalidOperationException> ( delegate { var s = ((IEnumerator)enumerator).Current; } );

            // act
            var lines2 = new List<string> ();
            while (enumerator.MoveNext()) {
                lines2.Add (enumerator.Current);
            }

            // assert
            Assert.True (lines.Count == 4);
            Assert.True (lines2.Count == 4);
            Assert.False (enumerator.MoveNext());
            Assert.Throws<InvalidOperationException> ( delegate { var s = enumerator.Current; } );
            Assert.Throws<InvalidOperationException> ( delegate { var s = ((IEnumerator)enumerator).Current; } );
        }
    }
}
