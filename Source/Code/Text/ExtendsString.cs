// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using DD.Collections.ICodeSet;

namespace DD.Text
{
    /// <summary>
    /// DD.Text namespace extensions for string type
    /// </summary>
    public static class ExtendsString {

        /// <summary>
        /// Split lines including line-end
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public static IEnumerator<string>  GetLineEnumerator (this string @string) {
            return new LineEnumerator (@string);
        }

        /// <summary>
        /// Convert string to string array of lines
        /// </summary>
        /// <param name="self"></param>
        /// <returns>string[]</returns>
        public static IEnumerable<string> ToLines (this string self) {
            var e = self.GetLineEnumerator();
            while (e.MoveNext()) {
                yield return e.Current;
            }
        }

        /// <summary>
        /// Decode utf-16 encoded string into IEnumerableOf(Code)
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public static IEnumerator<Code> GetCodeEnumerator (this string @string) {
            if (!string.IsNullOrEmpty (@string)) {
                char prev = (char)0;
                Code code = 0;
                foreach (char next in @string) {
                    code = prev.Decode (next);
                    if (!code.IsHighSurrogate ()) { 
                        yield return code; 
                    }
                    prev = next;
                }
                // Check if last character is incomplete prev/next sequence
                if (code.IsHighSurrogate ()) { throw new ArgumentException ("Incomplete sequence at the end, value (" + code + ")"); }
            } else {
                yield break;
            }
        }

        /// <summary>
        /// Convert string to array of Codes
        /// </summary>
        /// <param name="string"></param>
        /// <returns>string[]</returns>
        public static IEnumerable<Code> ToCodes (this string @string) {
            var e = @string.GetCodeEnumerator();
            while (e.MoveNext()) {
                yield return e.Current;
            }
        }

        /// <summary>
        /// Decode utf-16 encoded string into IEnumerableOf(int) UTF code points
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public static IEnumerator<int> GetIntEnumerator (this string @string) {
            var e = @string.GetCodeEnumerator();
            while (e.MoveNext()) {
                yield return e.Current;
            }
        }

        /// <summary>
        /// Convert string to array of Int32
        /// </summary>
        /// <param name="string"></param>
        /// <returns>IEnumerableOf(int)</returns>
        public static IEnumerable<int> ToIntCodes (this string @string) {
            var e = @string.GetCodeEnumerator();
            while (e.MoveNext()) {
                yield return e.Current;
            }
        }

    }
}
