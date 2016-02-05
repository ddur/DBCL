// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------


using System;
using System.Collections;
using System.Collections.Generic;

namespace DD.Text
{
    /// <summary>
    /// Environment.NewLine (cr, lf, crlf) independent string line enumearator
    /// </summary>
    public class LineEnumerator : IEnumerator<string> {

        private const ushort carriageReturn = 0xD;
        private const ushort lineFeed = 0xA;

        private bool doNext = false;
        private bool invalid = true;

        private bool cr = false;
        private bool lf = false;

        private int offset = 0;
        private int index = 0;

        private readonly string enumerated;
        private string current;

        public LineEnumerator (string @string) {
            enumerated = @string;
            doNext = !string.IsNullOrEmpty(enumerated);
        }

        public bool MoveNext()
        {
            if (doNext) {
                if (index < enumerated.Length) {
                    for (; index < enumerated.Length; index++) {
                        if (IsEndOfLine(enumerated[index])) { // New line
                            current = enumerated.Substring(offset, index - offset);
                            offset = index;
                            ++index;
                            if (invalid) {
                                invalid = false;
                            } 
                            return true;
                        }
                    }
                }
                // Last line
                doNext = false;
                current = enumerated.Substring(offset, index - offset);
                if (invalid) {
                    invalid = false;
                }
                return true;
            }
            if (!invalid) {
                invalid = true;
            }
            return false;
        }

        public void Reset()
        {
            doNext = !string.IsNullOrEmpty(enumerated);
            invalid = true;
            offset = 0;
            index = 0;
            cr = false;
            lf = false;
        }

        public string Current {
            get {
                if (!this.invalid) {
                    return (current);
                }
                throw new InvalidOperationException ("The enumerator is not positioned within collection.");
            }
        }

        object IEnumerator.Current {
            get {
                return Current;
            }
        }

        void IDisposable.Dispose() {}

        #if DEBUG
        internal
        #else
        private
        #endif
        bool IsEndOfLine (ushort @char) {
            switch (@char) {
                case carriageReturn:
                    if (lf || cr) {
                        lf = false; // cr after cr|lf
                        return true;
                    }
                    cr = true; // cr found
                    break;
                case lineFeed:
                    if (lf) { // lf after lf
                        return true;
                    }
                    lf = true; // lf found
                    break;
                default:
                    if (cr || lf) { // any non-line-end char after any line-end
                        cr = false;
                        lf = false;
                        return true;
                    }
                    break;
            }
            return false;
        }
    }

}
