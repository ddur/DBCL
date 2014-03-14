// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using DD.Collections;
using DD.Diagnostics;

namespace DD.Text
{
	public static class CodeEncoding {

		#region Decode String
		
		/// <summary>
		/// Decode UTF-16 string or sequence of characters to List&lt;int&gt; of code points
		/// </summary>
		/// <param name="utf16"></param>
		/// <returns>IList&lt;int&gt;</returns>
		/// <exception cref="ArgumentNullException">When Argument is null</exception>
		/// <exception cref="ArgumentException">When Argument contains invalid sequence of characters</exception>
		/// <remarks>
		/// <para></para>
		/// </remarks>
		public static List<Code> Decode (this string utf16)
		{
			Contract.Ensures (Theory.Decode(utf16, Contract.Result<List<Code>>()));

			if (string.IsNullOrEmpty (utf16)) { return new List<Code> (); }
			var codes = new List<Code>(utf16.Length);
			char prev = (char)0;
			Code code = 0;
			int index = 0; // Maintain position for exception message
			try
			{
				foreach (char next in utf16)
				{
					code = Decode(prev, next);
					if(!code.IsHighSurrogate()) { codes.Add(code); }
					prev = next; ++index;
				}
				// Check if last character is incomplete prev/next sequence
				if (code.IsHighSurrogate()) { throw new ArgumentException("Incomplete sequence at the end, value (" + code + ")"); }
			} 
			catch (ArgumentException e)
			{
				throw new ArgumentException(e.Message + ", at(" + index + ")");
			}
			return codes;
		}
	
		/// <summary>Try to Decode UTF-16 string to out List&lt;Code&gt; (no exceptions thrown)
		/// </summary>
		/// <param name="utf16">to decode</param>
		/// <param name="codes">decoded</param>
		/// <returns>Success/Failure</returns>
		public static bool TryDecode (this string utf16, out List<Code> codes)
		{
			Contract.Ensures(
				(Contract.Result<bool>() == false
				 && Contract.ValueAtReturn(out codes).IsNull()
				)
				||
				(Contract.Result<bool>() == true
				 && Contract.ValueAtReturn(out codes).IsNot(null)
				 && Theory.Decode(utf16, Contract.ValueAtReturn(out codes))
				)
			   );

			try
			{
				codes = utf16.Decode();
				return true;
			}
			catch (ArgumentException)
			{
				codes = null;
				return false;
			}
		}
	
		public static bool CanDecode (this string chars)
		{
			if (chars.IsNull()) return true;
			char prev = (char)0;
			char next = (char)0;
			using ( IEnumerator<char> e = chars.GetEnumerator() ) {
				while(e.MoveNext()) {
						next = e.Current;
						if (CanDecode(prev, next)) prev = next;
						else return false;
				}   }
			// check if last character is incomplete surrogate sequence
			if (char.IsHighSurrogate(next)) return false;
			else return true;
		}
		
		[Pure]
		public static Code Decode (this char prev, char next) {
	
			Contract.Ensures (Theory.Decode(prev, next, Contract.Result<Code>()));
			Contract.EnsuresOnThrow<ArgumentException>(Theory.CanDecode(prev, next, false) );
	
			/* sample values and masks
			 * 
			 * D800			 00000000 00000000 11011000 00000000
			 * DBFF			 00000000 00000000 11011011 11111111
			 * DFFF			 00000000 00000000 11011111 11111111
			 * MASK  FFFFF800   11111111 11111111 11111000 00000000 == D800 => within surrogate area
			 * MASK  FFFFFC00   11111111 11111111 11111100 00000000 == D800 => within low surrogate area
			 * MASK  FFFFFC00   11111111 11111111 11111100 00000000 == DC00 => within high surrogate area
			 * 
			 */
	
			// rfc 2781
			if (char.IsHighSurrogate(prev)) // Expecting next value in low surrogate area
			{
				// Next value in low surrogate area?
				if (char.IsLowSurrogate(next)) 
				{   // Next is within low surrogate area
					// Encode surrogate-pair
					int decode = prev & 0x3FF;	// use lower 10 bits of W1
					decode <<= 10;				// shift 10 up
					decode |= next & 0x3FF;		// combine with lower 10 bits of W2
					decode += 0x10000;			// shift above BMP (0-FFFFF to 10000-10FFFF)
					return (Code)decode;		// prev&next decoded -> success
				}
				// !next.IsLowSurrogate -> invalid sequence -> failure
				throw new ArgumentException("Invalid sequence end (not low surrogate), value(" + ((int)next).ToString("X") + ")");
	
			} else { // Prev is Not high surrogate here
		
					// Next is not surrogate -> no sequence -> success
					if (char.IsSurrogate(next) == false) return (Code)next;
					// Next is high surrogate -> sequence start -> success
					if (char.IsHighSurrogate(next) == true) return (Code)next;
					// Next is low surrogate -> invalid sequence -> failure
					throw new ArgumentException("Invalid sequence start (low surrogate), value(" + ((int)next).ToString("X") + ")");
			}
		}
	
		[Pure]
		public static bool CanDecode (this char prev, char next)
		{
			Contract.Ensures (Theory.CanDecode(prev, next, Contract.Result<bool>()));
			return ( char.IsHighSurrogate(prev) && char.IsLowSurrogate(next) ||
					!char.IsHighSurrogate(prev) && !char.IsLowSurrogate(next) );
	
		}
	
		#endregion
	
		#region Encode ToString
		
		[Pure]
		public static string Encode (this Code self)
		{
			if (self.HasCharValue()) { 
				return self.IsSurrogate()? "" + (char)0xFFFD : "" + (char)self; 
			}
			int value = self.Value - 0x10000;
			return "" + (char)((value>>10)|0xD800) + (char)((value&0x3FF)|0xDC00);
		}

		[Pure]
		public static string Encode (this IEnumerable<Code> codes) {
			Contract.Ensures(Theory.Encode(codes, Contract.Result<string>()));
	
			if (codes == null || codes.IsEmpty()) return string.Empty;
			var sb = new StringBuilder(codes.Count());
			foreach (var code in codes) {
				sb.Append(code.Encode());
			}
			return sb.ToString();
		}
	
		#endregion

		#region Encoding Theory
	
		private static class Theory {
	
			[Pure]
			public static bool Encode (IEnumerable<Code> input, string result) {
				Success success = true;
				success.Assert (!result.IsNull());
				if (input.IsNull()) {
					success.Assert (result.Length == 0);
				}
				else {
					var codes = new int[input.Count()];
					int index = 0;
					foreach (int item in input) {
						codes[index] = item;
						++index;
					}
						var bytes = new byte[codes.Length*4];
						Buffer.BlockCopy (codes, 0, bytes, 0, bytes.Length);
						string encoded = Encoding.UTF32.GetString(bytes);
						var r = result.GetEnumerator();
						success.Assert(result.SequenceEqual(encoded),result.Length.ToString());
				}
				return success;
			}
			
			[Pure]
			public static bool Decode (string input, List<Code> result) {
				Success success = true;
				success.Assert (!result.IsNull());
				if (string.IsNullOrEmpty(input)) {
					success.Assert(result.Count == 0);
				}
				else {
					success.Assert(result.Count != 0);
					byte[] bytes = Encoding.UTF32.GetBytes(input);
					success.Assert(bytes.Length != 0);
					success.Assert(bytes.Length % 4 == 0);
	   				success.Assert(bytes.Length / 4 == result.Count);
		   			var utf32 = new int[bytes.Length/4];
		   			Buffer.BlockCopy (bytes, 0, utf32, 0, bytes.Length);
					int index = 0;
					foreach ( int item in result ) {
						success.Assert (utf32[index] == item);
						index++;
					}
					success.Assert (index == utf32.Length);
				} 
				return success;
			}
			
			[Pure]
			public static bool CanDecode (char prev, char next, bool result) {
				Success success = true;

				success.Assert( result ==
					( char.IsHighSurrogate(prev) && char.IsLowSurrogate(next) ||
					  !char.IsHighSurrogate(prev) && !char.IsLowSurrogate(next) ));
				return success;
			}
	
			[Pure]
			public static bool Decode (char prev, char next, Code result) {
				Success success = true;

				success.Assert (result.IsNot(null));
				success.Assert (CanDecode (prev, next, true));
	
				if (char.IsHighSurrogate(prev) && char.IsLowSurrogate(next)) {
	   					success.Assert(result == char.ConvertToUtf32(prev, next));
		  			} else {
		   				success.Assert(result == next);
		   			}
				return success;
			}
		}
	
		#endregion
	
	}
}
