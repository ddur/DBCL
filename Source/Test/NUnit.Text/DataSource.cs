// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using DD.Collections.ICodeSet;

using NUnit.Framework;

namespace DD.Text {

    public static class DataSource {

        public static IEnumerable<TestCaseData> StringLines {
            get {
                yield return new TestCaseData ((string)null, 0, new string[0]);
                yield return new TestCaseData (string.Empty, 0, new string[0]);
                yield return new TestCaseData ("single line", 1, new [] {"single line"});
                yield return new TestCaseData ("\tfirst line\n\tsecond line\r", 2, new [] {"\tfirst line\n", "\tsecond line\r"});
                yield return new TestCaseData ("\tfirst line\r\tsecond line", 2, new [] {"\tfirst line\r", "\tsecond line"});
                yield return new TestCaseData ("\tfirst line\n \n\tthird line\r\n \r   fifth line\r\n", 5, new [] {"\tfirst line\n", " \n", "\tthird line\r\n", " \r", "   fifth line\r\n"});
                yield return new TestCaseData ("\n\n\n\n\n\n\n", 7, new [] {"\n", "\n", "\n", "\n", "\n", "\n", "\n"});
                yield return new TestCaseData ("\r\n\r\n\r\n\r\n", 4, new [] {"\r\n", "\r\n", "\r\n", "\r\n"});
                yield return new TestCaseData ("\r\r\r\n \r\n \r\n \r \n \n\n\n\r\n\n", 12, new [] 
                   {"\r", "\r", "\r\n", " \r\n", " \r\n", " \r", " \n", " \n", "\n", "\n", "\r\n", "\n"});
                yield return new TestCaseData ("\r\r\r\n \r\n \r\n \r \n \n\n\n\r\n\r", 12, new [] 
                   {"\r", "\r", "\r\n", " \r\n", " \r\n", " \r", " \n", " \n", "\n", "\n", "\r\n", "\r"});
                yield return new TestCaseData ("\r\r\r\n \r\n \r\n \r \n \n\n\n\r\n\r\n", 12, new [] 
                   {"\r", "\r", "\r\n", " \r\n", " \r\n", " \r", " \n", " \n", "\n", "\n", "\r\n", "\r\n"});
                yield return new TestCaseData ("\r\r\r\n \r\n \r\n \r \n \n\n\n\r\n ", 12, new [] 
                   {"\r", "\r", "\r\n", " \r\n", " \r\n", " \r", " \n", " \n", "\n", "\n", "\r\n", " "});
            }
        }

        public static IEnumerable<string> InvalidEncodedString {
            get {
                yield return "abc\uD801";
                yield return "abc\uD801def";
                yield return "\uD801def";
                yield return "\uD801";
                yield return "abc\uF922\uDC00\uD800";
                yield return "abc\uF922\uDBFF\uE000";
                yield return "abc\uDC00\uD800def";
                yield return "\uDC00\uD800def";
            }
        }

        public static IEnumerable<TestCaseData> ValidEncodedString {
            get {
                yield return new TestCaseData ((string)null, new List<Code> ());
                yield return new TestCaseData (string.Empty, new List<Code> ());
                yield return new TestCaseData ("", new List<Code> ());
                yield return new TestCaseData ("a", new List<Code> () { 'a' });
                yield return new TestCaseData ("a\uD800\uDC00", new List<Code> { 'a', 0x10000 });
                yield return new TestCaseData ("ab\uD800\uDC01", new List<Code> { 'a', 'b', 0x10001 });
                yield return new TestCaseData ("abc\uD800\uDC02", new List<Code> { 'a', 'b', 'c', 0x10002 });
                yield return new TestCaseData ("abc\uF922\uD800\uDC10", new List<Code> { 'a', 'b', 'c', 0xF922, 0x10010 });
                yield return new TestCaseData ("abc\uFFFF\u0000\uD801\uDC01def", new List<Code> { 'a', 'b', 'c', 0xFFFF, 0x0000, 0x10401, 'd', 'e', 'f' });
                yield return new TestCaseData ("\uF922\uD802\uDC00def", new List<Code> { 0xF922, 0x10800, 'd', 'e', 'f' });
                yield return new TestCaseData ("\uD802\uDC03def", new List<Code> { 0x10803, 'd', 'e', 'f' });
                yield return new TestCaseData ("\uD802\uDC04de", new List<Code> { 0x10804, 'd', 'e' });
                yield return new TestCaseData ("\uD800\uDC07d", new List<Code> { 0x10007, 'd' });
                yield return new TestCaseData ("\uD800\uDC08", new List<Code> { 0x10008 });
            }
        }

        public static IEnumerable<TestCaseData> TwoCharsToBool {
            get {
                // high surrogate, low surrogate
                yield return new TestCaseData ((char)0xD800, (char)0xDC00).Returns (true);
                // high surrogate, no surrogate
                yield return new TestCaseData ((char)0xD800, (char)0x0C00).Returns (false);
                // high surrogate, high surrogate
                yield return new TestCaseData ((char)0xD800, (char)0xD800).Returns (false);

                // low surrogate, high surrogate
                yield return new TestCaseData ((char)0xDC00, (char)0xD800).Returns (true);
                // low surrogate, no surrogate
                yield return new TestCaseData ((char)0xDC00, (char)0x0800).Returns (true);
                // low surrogate, low surrogate
                yield return new TestCaseData ((char)0xDC00, (char)0xDC00).Returns (false);

                // no surrogate, high surrogate
                yield return new TestCaseData ((char)0x0800, (char)0xD800).Returns (true);
                // no surrogate, low surrogate
                yield return new TestCaseData ((char)0x0800, (char)0xDC00).Returns (false);
                // no surrogate, no surrogate
                yield return new TestCaseData ((char)0x0800, (char)0x0800).Returns (true);

                // no surrogate, no surrogate
                yield return new TestCaseData ((char)0, (char)0).Returns (true);
                yield return new TestCaseData (char.MinValue, char.MaxValue).Returns (true);
                yield return new TestCaseData (char.MinValue, char.MinValue).Returns (true);
                yield return new TestCaseData (char.MaxValue, char.MaxValue).Returns (true);
                yield return new TestCaseData (char.MaxValue, char.MinValue).Returns (true);
            }
        }

        public static IEnumerable<TestCaseData> TwoCharsToCodeOrThrow {
            get {
                // high surrogate, low surrogate
                yield return new TestCaseData ((char)0xD800, (char)0xDC00).Returns (char.ConvertToUtf32 ((char)0xD800, (char)0xDC00));
                // high surrogate, no surrogate
                yield return new TestCaseData ((char)0xD800, (char)0x0C00).Throws (typeof (ArgumentException));
                // high surrogate, high surrogate
                yield return new TestCaseData ((char)0xD800, (char)0xD800).Throws (typeof (ArgumentException));

                // low surrogate, high surrogate
                yield return new TestCaseData ((char)0xDC00, (char)0xD800).Returns (0xD800);
                // low surrogate, no surrogate
                yield return new TestCaseData ((char)0xDC00, (char)0x0800).Returns (0x0800);
                // low surrogate, low surrogate
                yield return new TestCaseData ((char)0xDC00, (char)0xDC00).Throws (typeof (ArgumentException));

                // no surrogate, high surrogate
                yield return new TestCaseData ((char)0x0800, (char)0xD800).Returns (0xD800);
                // no surrogate, low surrogate
                yield return new TestCaseData ((char)0x0800, (char)0xDC00).Throws (typeof (ArgumentException));
                // no surrogate, no surrogate
                yield return new TestCaseData ((char)0x0800, (char)0x0800).Returns (0x0800);

                // no surrogate, no surrogate
                yield return new TestCaseData ((char)0, (char)0).Returns (0);
                yield return new TestCaseData (char.MinValue, char.MaxValue).Returns (0xFFFF);
                yield return new TestCaseData (char.MinValue, char.MinValue).Returns (0x0000);
                yield return new TestCaseData (char.MaxValue, char.MaxValue).Returns (0xFFFF);
                yield return new TestCaseData (char.MaxValue, char.MinValue).Returns (0x0000);
            }
        }

        public static IEnumerable<TestCaseData> CodeToStringOrThrow {
            get {
                yield return new TestCaseData (int.MinValue).Throws (typeof (IndexOutOfRangeException));
                yield return new TestCaseData (int.MaxValue).Throws (typeof (IndexOutOfRangeException));
                yield return new TestCaseData (Code.MinValue - 1).Throws (typeof (IndexOutOfRangeException));
                yield return new TestCaseData (Code.MaxValue + 1).Throws (typeof (IndexOutOfRangeException));

                yield return new TestCaseData (8).Returns ("\u0008");
                yield return new TestCaseData (9).Returns ("\u0009");
                yield return new TestCaseData (10).Returns ("\u000A");
                yield return new TestCaseData (11).Returns ("\u000B");
                yield return new TestCaseData (12).Returns ("\u000C");
                yield return new TestCaseData (13).Returns ("\u000D");

                yield return new TestCaseData ((int)'&').Returns ("&");
                yield return new TestCaseData ((int)'\'').Returns ("'");
                yield return new TestCaseData ((int)'>').Returns (">");
                yield return new TestCaseData ((int)'<').Returns ("<");
                yield return new TestCaseData ((int)'"').Returns ("\"");
                yield return new TestCaseData ((int)'~').Returns ("~");

                int testCode = char.MinValue;
                yield return new TestCaseData (testCode).Returns ("\u0000");
                Random r = new Random ();
                for (int i = 0; i < 100; i++) {
                    testCode = r.Next (char.MinValue, char.MaxValue);
                    if (char.IsSurrogate ((char)testCode)) {
                        yield return new TestCaseData (testCode).Returns ("" + (char)0xFFFD);
                    }
                    else {
                        yield return new TestCaseData (testCode).Returns ("" + (char)testCode);
                    }
                }
                testCode = char.MaxValue;
                yield return new TestCaseData (testCode).Returns ("" + (char)testCode);
                testCode = char.MaxValue + 1;
                yield return new TestCaseData (testCode).Returns (char.ConvertFromUtf32 (testCode));
                for (int i = 0; i < 100; i++) {
                    testCode = r.Next (char.MaxValue + 1, Code.MaxValue);
                    yield return new TestCaseData (testCode).Returns (char.ConvertFromUtf32 (testCode));
                }
                testCode = Code.MaxValue;
                yield return new TestCaseData (testCode).Returns (char.ConvertFromUtf32 (testCode));
            }
        }
    }
}
