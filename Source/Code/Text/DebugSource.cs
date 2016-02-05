// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using DD.Text;

namespace DD.Text
{
    /// <summary>
    /// Interface compatible with .pdb Source Code Points
    /// </summary>
    public interface IDebugPoint {
        string GetPoint (int startLine, int startColumn, int endLine, int endColumn);
    }

    /// <summary>Source, line parsed - read only
    /// <remarks>Line and column starts at 1.</remarks>
    /// </summary>
    public class DebugSource : LineSource, IDebugPoint
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="source"></param>
        public DebugSource(string source) : base (source) {}

        /// <summary>Return string at Line/Column/EndLine/EndColumn position
        /// <remarks>Line and Column counting starts at 1.</remarks>
        /// </summary>
        /// <param name="startLine"></param>
        /// <param name="startColumn"></param>
        /// <param name="endLine"></param>
        /// <param name="endColumn"></param>
        /// <returns></returns>
        public string GetPoint (int startLine, int startColumn, int endLine, int endColumn) {

            var debugPoint = new StringBuilder();
            string line;
            bool argOutOfRange;

            if (startLine==endLine && startLine > 0 && startLine <= lines.Length) {

                #region One-Line request
                line = GetLine(startLine);

                argOutOfRange = startColumn > endColumn || startColumn > line.Length;
                if (!argOutOfRange)
                {
                    var clippedStartColumn = (startColumn < 1) ? 1 : startColumn;
                    var clippedEndColumn = (endColumn > line.Length + 1) ? line.Length + 1 : endColumn;
                    debugPoint.Append(line.Substring(clippedStartColumn - 1, clippedEndColumn - clippedStartColumn));
                }
                #endregion

            } else if (startLine < endLine && startLine > 0 && endLine <= lines.Length) {

                #region Multi-line request

                #region First line
                line = GetLine(startLine);

                argOutOfRange = startColumn > line.Length;
                if (!argOutOfRange) {
                    var clippedStartColumn = (startColumn < 1) ? 1 : startColumn;
                    debugPoint.Append(line.Substring(clippedStartColumn - 1));
                }
                #endregion

                #region More than two lines
                for ( int lineIndex = startLine + 1; lineIndex < endLine; lineIndex++ ) {
                    debugPoint.Append ( GetLine ( lineIndex ) );
                }
                #endregion

                #region Last line
                line = GetLine(endLine);

                argOutOfRange = endColumn < 1;
                if (!argOutOfRange) {
                    var clippedEndColumn = (endColumn > line.Length + 1) ? line.Length + 1 : endColumn;
                    debugPoint.Append(line.Substring(0, clippedEndColumn - 1));
                }
                #endregion

                #endregion

            } 
            return debugPoint.ToString();
        }

    }
}
