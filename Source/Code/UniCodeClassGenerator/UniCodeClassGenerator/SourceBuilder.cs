// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Text;

namespace UniCodeClassGenerator
{
    /// <summary>
    /// Emulate Text Template (*.tt) methods and more
    /// </summary>
    public class SourceBuilder
    {
        protected readonly StringBuilder output = new StringBuilder();

        public SourceBuilder()
        {
        }

        public void AddCopyright () {
            output.AppendLine ("// ================================================================================");
            output.AppendLine ("// <copyright file=\"https://github.com/ddur/DBCL/blob/master/LICENSE\" company=\"DD\">");
            output.AppendLine ("// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.");
            output.AppendLine ("// ================================================================================");
            output.AppendLine ("// GENERATED CLASS - DO NOT CHANGE");
            output.AppendLine ("// ================================================================================");
        }

        public void Using (string @namespace, params string[] namespaces) {
            Using (@namespace);
            if (namespaces != null) {
                foreach (var item in namespaces) {
                    Using (item);
                }
            }
        }

        public void Using (string @namespace) {
            if (!string.IsNullOrWhiteSpace(@namespace)) {
                output.Append ("using ");
                output.Append (@namespace);
                output.AppendLine (";");
            }
        }

        public void OpenNamespace (string @namespace) {
                output.Append ("namespace ");
                output.Append (@namespace);
                output.AppendLine (" {");
        }

        public void CloseNamespace () {
                output.AppendLine ("}");
        }

        public void OpenStaticClass (string @class) {
            output.Append ("".PadRight(4) + "public static partial class ");
            output.Append (@class);
            output.AppendLine (" {");
        }
        public void OpenClass (string @class) {
            output.Append ("".PadRight(4) + "public partial class ");
            output.Append (@class);
            output.AppendLine (" {");
        }

        public void CloseClass () {
                output.AppendLine (" ".PadRight(4) + "}");
        }

        public void OpenRegion (string @region) {
            output.AppendLine ("#region " + @region);
        }

        public void CloseRegion () {
            output.AppendLine ("#endregion");
        }

        public void Write (char @char) {
            output.Append (@char);
        }

        public void Write (string @string) {
            output.Append (@string);
        }

        public void Write (byte spaces, string @string) {
            output.Append (" ".PadRight(spaces));
            output.Append (@string);
        }

        public void WriteLine () {
            output.AppendLine ();
        }

        public void WriteLine (char @char) {
            output.Append (@char);
            output.AppendLine ();
        }

        public void WriteLine (string @string) {
            output.AppendLine (@string);
        }

        public void WriteLine (byte spaces, string @string) {
            output.Append (" ".PadRight(spaces));
            output.AppendLine (@string);
        }

        public override string ToString() {
            return output.ToString();
        }

        public int Length {
            get {
                return output.Length;
            }
        }

        public void Clear () {
            output.Clear();
        }
    }
}
