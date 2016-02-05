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
        public static IEnumerable<string>  GetLineEnumerator (this string @string) {
            var lineEnumerator = new LineEnumerator (@string);
            while (lineEnumerator.MoveNext()) {
                yield return lineEnumerator.Current;
            }
        }

        /// <summary>
        /// Decode utf-16 encoded string into IEnumerableOf(Code)
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public static IEnumerable<Code> GetCodeEnumerator (this string @string) {
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
        /// Decode utf-16 encoded string into IEnumerableOf(int) UTF code points
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetIntEnumerator (this string @string) {
            foreach (Code c in @string.GetCodeEnumerator()) {
                yield return c;
            }
        }
    }
}
