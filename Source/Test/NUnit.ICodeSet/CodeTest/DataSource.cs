// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
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
    }
}
