// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

using DD.Text;
using DD.Collections;
using DD.Collections.ICodeSet;

namespace UniCodeClassGenerator
{
    /// <summary>
    /// Category Class Builder.
    /// </summary>
    public class CategoryBuilder : SourceBuilder
    {
        public void Build (string input) {
            var lines = input.ToLines();

            var regexOptions = RegexOptions.Compiled | RegexOptions.ExplicitCapture;
            var categoryRegex = new Regex(@"^# General_Category=(?<name>.+)", regexOptions);
            var dataRegex = new Regex(@"^(?<start>[0123456789ABCDEF]{4,6})(\.{2}(?<final>[0123456789ABCDEF]{4,6}))?", regexOptions);

            AddCopyright();
            WriteLine();
            Using ("System");
            Using ("DD.Collections.ICodeSet");
            WriteLine();
            OpenNamespace ("DD.Text.UniCode");
            WriteLine();
            OpenStaticClass ("Category");

            bool data = false;
            int countData = 0;

            var categoryName = string.Empty;
            var propertyName = string.Empty;
            var unionCatName = string.Empty;

            var bits = BitSetArray.Size (Code.MaxCount);
            ICodeSet codes = CodeSetNone.Singleton;

            foreach (var lineItem in lines) {
                var line = Regex.Replace (lineItem, @"[\n\r]", "");

                // Group footer
                if (line.StartsWith ("# Total code points", StringComparison.InvariantCulture)) {

                    Console.Write (" OK");

                    // TODO compare bits.Count with Total code points
                    if (bits.Count == 0) {
                        WriteLine ("CodeSetNone.Singleton;");
                    }
                    else if (bits.Count == 1) {
                        WriteLine ("(Code)" + bits.First + ";");
                    }
                    else if (bits.Count == Service.PairCount) {
                        WriteLine ("CodeSetPair.From (" + bits.First + ", " + bits.Last + ");");
                    }
                    else if (bits.Count <= Service.ListMaxCount) {
                        WriteLine ("CodeSetList.From (");
                        var firstItem = true;
                        foreach (var item in bits) {
                            if (firstItem) {
                                firstItem = false;
                                Write (24, "" + item);
                            }
                            else {
                                Write (", " + item);
                            }
                        }
                        WriteLine (");");
                    }
                    else if (bits.Count == bits.Span()) {
                        WriteLine ("CodeSetFull.From (" + bits.First + ", " + bits.Last + ");");
                    }
                    else {
                        var itemBuilder = new SourceBuilder();
                        itemBuilder.Write ("CodeSetMask.From (");
                        itemBuilder.WriteLine ();

                        var itemLineBuilder = new SourceBuilder();
                        var firstItem = true;
                        var countItem = 0;
                        foreach (var item in bits) {
                            countItem += 1;
                            if (firstItem) {
                                firstItem = false;
                                itemLineBuilder.Write ("" + item);
                            }
                            else {
                                itemLineBuilder.Write (", " + item);
                            }
                            if (itemLineBuilder.Length > 80) {
                                if (countItem < bits.Count) {
                                    itemLineBuilder.Write (","); // end of line
                                    firstItem = true; // start new line
                                }
                                itemBuilder.WriteLine (24, itemLineBuilder.ToString());
                                itemLineBuilder.Clear();
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(itemLineBuilder.ToString())) {
                            itemBuilder.WriteLine (24, itemLineBuilder.ToString()); // last item line
                            itemLineBuilder.Clear();
                        }
                        itemBuilder.WriteLine (20, ");");

                        var compactBitMask = CodeSetMask.From (bits).ToCompactBitMask();
                        var maskBuilder = new SourceBuilder();
                        maskBuilder.Write ("CodeSetMask.From (new int[] {");
                        maskBuilder.WriteLine ();

                        var maskLineBuilder = new SourceBuilder();
                        var firstMask = true;
                        var countMask = 0;
                        foreach (var item in compactBitMask.Masks) {
                            countMask += 1;
                            if (firstMask) {
                                firstMask = false;
                                maskLineBuilder.Write ("" + item);
                            }
                            else {
                                maskLineBuilder.Write (", " + item);
                            }
                            if (maskLineBuilder.Length > 80) {
                                if (countMask < compactBitMask.Masks.Count) {
                                    maskLineBuilder.Write (","); // end of line
                                    firstMask = true; // start new line
                                }
                                maskBuilder.WriteLine (24, maskLineBuilder.ToString());
                                maskLineBuilder.Clear();
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(maskLineBuilder.ToString())) {
                            maskBuilder.WriteLine (24, maskLineBuilder.ToString()); // last mask line
                            maskLineBuilder.Clear();
                        }
                        maskBuilder.WriteLine (20,"}, " + compactBitMask.Start + ", " + compactBitMask.Final + ", " + compactBitMask.Count + ");");
                        if (itemBuilder.Length < maskBuilder.Length) {
                            Write (itemBuilder.ToString());
                        } else {
                            Write (maskBuilder.ToString());
                        }
                    }

                    Console.WriteLine ();

                    data = false;
                    countData = 0;
                    WriteLine (16,         "}");
                    WriteLine (16,         "return (" + unionCatName + ");");
                    WriteLine (12,     "}");
                    WriteLine (08, "}");
                    WriteLine (08, "private static ICodeSet " + unionCatName + ";");

                    categoryName = string.Empty;
                }

                // data
                if (data && !string.IsNullOrWhiteSpace (line)) {
                    var dataMatch = dataRegex.Match (line);
                    if (dataMatch.Success) {

                        Console.Write (".");

                        var startValue = dataMatch.Groups["start"].Value;
                        var finalValue = dataMatch.Groups["final"].Value;
                        int startInt;
                        int finalInt;
                        if (Int32.TryParse (startValue, NumberStyles.HexNumber, null, out startInt)) {
                            if (!string.IsNullOrWhiteSpace (finalValue)
                                && Int32.TryParse (finalValue, NumberStyles.HexNumber, null, out finalInt)) {
                                bits._SetMembersRange (startInt, finalInt);
                            } else {
                                bits._SetMember (startInt); 
                            }
                        } else {
                            throw new ArgumentException (line);
                        }
                        countData += 1;
                    } else {
                        throw new ArgumentException ("No data match: " + line);
                    }
                }

                // group header
                if (line.StartsWith ("# General_Category", StringComparison.InvariantCulture)) {
                    data = true;
                    bits.Clear ();
                    var categoryMatch = categoryRegex.Match (line);
                    if (categoryMatch.Success) {

                        Console.Write (line + " ");

                        categoryName = categoryMatch.Groups["name"].Value;
                        propertyName = categoryName.Replace ("_", "");
                        unionCatName = "_" + propertyName.Substring (0, 1).ToLower() + propertyName.Substring (1) + "_";
                        WriteLine ();
                        WriteLine (08, "/// <summary>");
                        WriteLine (08, "/// Derived " +  categoryName.Replace ('_', ' '));
                        WriteLine (08, "/// </summary>");
                        WriteLine (08, "public static ICodeSet " + propertyName + " {");
                        WriteLine (12,     "get {");
                        WriteLine (16,         "if (" + unionCatName + " == null) {");
                        Write     (20,             unionCatName + " = ");
                    } else {
                        throw new ArgumentException ("No category match: " + line);
                    }
                }

            }

            WriteLine ();
            CloseClass();
            CloseNamespace();
        }
    }
}
