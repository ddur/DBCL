// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DD.Text
{
	public class LineSource : IReadOnlyCollection<string>
	{
		public struct LineInfo
		{
			public int Offset;
			public int Length;
		}

		protected readonly string @string;

		protected readonly LineInfo[] lines;

		public LineSource (string text)
		{
			@string = string.IsNullOrEmpty(text) ? string.Empty : text;
			int offset = 0;
			var tmpLines = new List<LineInfo>();
			foreach (var line in @string.GetLineEnumerator()) {
				tmpLines.Add(new LineInfo {
					Offset = offset,
					Length = line.Length
				});
				offset += line.Length;
			}
			lines = tmpLines.ToArray();
		}

		/// <summary>Return text line at lineNo</summary>
		/// <remarks>Line counting starts at 1.</remarks>
		/// <param name="lineNo"></param>
		/// <returns></returns>
		public string GetLine(int lineNo)
		{
			string returnValue = string.Empty;
			if (lineNo > 0 && lineNo <= lines.Length) {
				LineInfo line = lines[lineNo - 1];
				returnValue = @string.Substring(line.Offset, line.Length);
			}
			return returnValue;
		}

		public IEnumerator<string> GetEnumerator()
		{
			foreach (var line in lines) {
				yield return @string.Substring(line.Offset, line.Length);
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Number of lines in Source
		/// </summary>
		public int Count {
			get {
				return lines.Length;
			}
		}

		/// <summary>
        /// Get Source string
        /// </summary>
        public string Source {
            get {
                return @string;
            }
        }
	}
}


