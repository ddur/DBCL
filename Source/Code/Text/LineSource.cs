// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DD.Text
{
    public class LineSource : IReadOnlyCollection<string> {

        protected readonly string @string;
        protected readonly IReadOnlyList<int> offsets;

        public LineSource (string source)
        {
            @string = string.IsNullOrEmpty(source) ? string.Empty : source;
            int vOffset = 0;
            var vOffsets = new List<int>();
            foreach (var line in @string.ToLines()) {
                vOffsets.Add (vOffset);
                vOffset += line.Length;
            }
            vOffsets.Add (vOffset);
            vOffsets.TrimExcess();
            offsets = vOffsets;
            Contract.Assert (offsets.Last() == @string.Length);
        }

        /// <summary>Return text line at lineNo</summary>
        /// <remarks>Line counting starts at 1.</remarks>
        /// <param name="lineNo"></param>
        /// <returns></returns>
        public string GetLine (int lineNo)
        {
            string returnValue = string.Empty;
            if (lineNo >= 1 && lineNo <= Count) {
                return @string.Substring(offsets[lineNo-1], offsets[lineNo] - offsets[lineNo-1]);
            }
            return returnValue;
        }

        public string GetLineAt (int position)
        {
            string returnValue = string.Empty;
            position -= 1; // convert to 0 based index position
            if (position >= 0 && position < offsets.Last()) {
                // Binary search
                int start = 0;
                int final = offsets.Count - 1;
                int guess = final / 2;
                while (final - start > 4) // narrows linear search to 4 items or less
                {
                    if (position < offsets [guess]) {
                        final = guess;
                        guess = start + ((final - start) / 2);
                    } else {
                        start = guess;
                        guess = start + ((final - start) / 2);
                    }
                }
                for (int index = start; ; index++) {
                    if (position < offsets[index]) {
                        returnValue = @string.Substring(offsets[index-1], offsets[index] - offsets[index-1]);
                        break;
                    }
                }
            }
            return returnValue;
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var line in @string.ToLines()) {
                yield return line;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Number of lines in Source
        /// </summary>
        public int Count {
            get {
                return offsets.Count - 1;
            }
        }

        /// <summary>
        /// Get Source string
        /// </summary>
        public string StringSource
        {
            get {
                return @string;
            }
        }
    }
}


