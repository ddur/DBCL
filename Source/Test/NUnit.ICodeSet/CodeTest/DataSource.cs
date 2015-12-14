// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeTest {

    public static class DataSource {
        public const int MinCodeValue = 0; // Min (Uni)Code value
        public const int MaxCodeValue = ((17 * 0x10000) - 1); // ((17 code planes)*0x10000)-1=0x10FFFF

        public const int MinCodeCount = 0;
        public const int MaxCodeCount = 17 * 0x10000; // MaxCodeValue + 1

        public static IEnumerable<int> ValidByteValue {
            get {
                for (int i = 0; i <= 255; i++) {
                    yield return i;
                }
            }
        }

        public static IEnumerable<int> ValidCharValue {
            get {
                yield return char.MinValue;
                yield return char.MinValue + 1;
                yield return char.MaxValue - 1;
                yield return char.MaxValue;

                int v = 0;
                for (int i = 1; i <= 10; i++) {
                    yield return v;
                    yield return v | 0xFF;
                    v += 0x100;
                }

                v = 0x1000;
                for (int i = 1; i <= 0xF; i++) {
                    yield return v;
                    yield return v | 0xFFF;
                    v += 0x1000;
                }

                Random r = new Random ();
                for (int i = 1; i <= 100; i++) {
                    yield return r.Next (char.MinValue, char.MaxValue);
                }
            }
        }

        public static IEnumerable<int> ValidCodeValue {
            get {
                yield return char.MinValue;
                yield return char.MinValue + 1;
                yield return char.MaxValue - 1;
                yield return char.MaxValue;

                yield return Code.MinValue;
                yield return Code.MinValue + 1;
                yield return Code.MaxValue - 1;
                yield return Code.MaxValue;

                // byte values
                for (int ascii7 = 0; ascii7 <= 127; ascii7++) {
                    yield return ascii7;
                }
                for (int codepage = 128; codepage <= 255; codepage++) {
                    yield return codepage;
                }

                // permanently unassigned block FDD0-FDEF
                for (int unassigned = 0xFDD0; unassigned <= 0xFDEF; unassigned++) {
                    yield return unassigned;
                }

                // for each page 0000 and permanently unassigned xFFFE/xFFFF items
                for (int page = 0; page <= 16; page++) {
                    yield return (page << 16);
                    yield return (page << 16 | 0xFFFE);
                    yield return (page << 16 | 0xFFFF);
                }

                Random r = new Random ();

                // 10 random high surrogates 0xD800-0xDBFF
                for (int times = 1; times <= 10; times++) {
                    yield return r.Next (0xD800, 0xDBFF);
                }

                // 10 random low surrogates 0xDC00-0xDFFF
                for (int times = 1; times <= 10; times++) {
                    yield return r.Next (0xDC00, 0xDFFF);
                }

                // 10 random codes for each plane
                for (int page = 0; page <= 16; page++) {
                    for (int times = 1; times <= 10; times++) {
                        yield return (page << 16 | r.Next (char.MinValue + 1, char.MaxValue - 1));
                    }
                }

                // 100 random Codes
                for (int times = 1; times <= 100; times++) {
                    yield return r.Next (Code.MinValue, Code.MaxValue);
                }
            }
        }

        public static IEnumerable<int> InvalidCodeValue {
            get {
                Random r = new Random ();
                yield return int.MinValue;
                yield return int.MinValue + 1;
                yield return r.Next (int.MinValue + 1, Code.MinValue - 1);
                yield return Code.MinValue - 1;
                yield return Code.MaxValue + 1;
                yield return r.Next (Code.MaxValue + 1, int.MaxValue - 1);
                yield return int.MaxValue - 1;
                yield return int.MaxValue;

                yield return -1;
                yield return 0x110000;
                int times = 100;
                while (times != 0) {
                    times--;
                    yield return r.Next (int.MinValue, Code.MinValue - 1);
                    yield return r.Next (Code.MaxValue + 1, int.MaxValue);
                }
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