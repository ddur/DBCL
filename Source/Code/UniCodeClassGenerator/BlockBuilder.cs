// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.</copyright>
// --------------------------------------------------------------------------------


using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using DD.Text;

namespace UniCodeClassGenerator
{
    /// <summary>
    /// Description of BlockClassBuilder.
    /// </summary>
    public class BlockBuilder : SourceBuilder
    {
        public void Build (string input) {

            var lines = input.ToLines();
            var blocks = new List<Tuple<string, string, string, string>>();

            var regexOptions = RegexOptions.Compiled | RegexOptions.ExplicitCapture;
            var rangeRegex = new Regex(@"^(?<start>[0123456789ABCDEF]{4,6})\.{2}(?<final>[0123456789ABCDEF]{4,6});\s*(?<label>.+)$", regexOptions);

            AddCopyright();
            WriteLine();
            Using ("System");
            Using ("DD.Collections.ICodeSet");
            WriteLine();
            OpenNamespace ("DD.Text.UniCode");
            WriteLine();
            OpenStaticClass ("Block");
            foreach (var line in lines) {
                var match = rangeRegex.Match(line);
                if (match.Success) {
                    var start = "";
                    var final = "";
                    var label = "";
                    foreach (var groupName in rangeRegex.GetGroupNames()) {
                        switch (groupName) {
                            case "start":
                                start = match.Groups[groupName].Value;
                                break;
                            case "final":
                                final = match.Groups[groupName].Value;
                                break;
                            case "label":
                                label = match.Groups[groupName].Value;
                                break;
                        }
                    }
                    label = Regex.Replace (label, @"[^\w\d]", "");
                    blocks.Add (new Tuple<string, string, string, string> (Regex.Replace (line, @"[\n\r]", ""), start, final, label));
                }
            }

            foreach (var tuple in blocks ) {
                var title = tuple.Item1;
                var start = tuple.Item2;
                var final = tuple.Item3;
                var label = tuple.Item4;
                var cache = "_" + label.Substring (0, 1).ToLower() + label.Substring (1) + "_";
                WriteLine ();
                WriteLine ( 8, "/// <summary>");
                WriteLine ( 8, "/// " +  title);
                WriteLine ( 8, "/// </summary>");
                WriteLine ( 8, "public static ICodeSet " + label + " {");
                WriteLine (12, "get {");
                WriteLine (16,     "if (" + cache + " == null) {");
                WriteLine (20,         cache + " = CodeSetFull.From (0x" + start + ", 0x" + final + ");");
                WriteLine (16,     "}");
                WriteLine (16,     "return " + cache + ";");
                WriteLine (12, "}");
                WriteLine ( 8, "}");
                WriteLine ( 8, "private static ICodeSet " + cache + ";");
            }
            WriteLine ();
            CloseClass();
            CloseNamespace();
        }
    }
}
