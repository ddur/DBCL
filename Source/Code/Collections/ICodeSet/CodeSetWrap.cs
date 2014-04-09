// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using DD.Diagnostics;

namespace DD.Collections.ICodeSet
{
	/// <summary>
	/// Description of CodeBitsSet.
	/// </summary>
	public sealed class CodeSetWrap : CodeSet
	{

		#region Ctor

		internal CodeSetWrap()
		{
			Contract.Ensures (Theory.Construct(this));

			sorted = BitSetArray.Size();
		}

		internal CodeSetWrap(CodeSetWrap wrap)
		{
			Contract.Requires<ArgumentNullException>(wrap.IsNot(null));

			Contract.Ensures (Theory.Construct(wrap, this));

			sorted = wrap.sorted;
		}

		internal CodeSetWrap(BitSetArray bits)
		{
			Contract.Requires<ArgumentNullException>(bits.IsNot(null));
			Contract.Requires<ArgumentException>(bits.Length <= Code.MaxCount || bits.Last <= Code.MaxValue);

			Contract.Ensures (Theory.Construct(bits, this));
			
			sorted = BitSetArray.Copy(bits);
		}

		internal CodeSetWrap(IEnumerable<Code> codes)
		{
			Contract.Requires<ArgumentNullException>(codes.IsNot(null));

			Contract.Ensures (Theory.Construct(codes, this));
			
			sorted = BitSetArray.From (codes.ToValues());
		}

		#endregion

		#region Fields

		readonly BitSetArray sorted;

		#endregion

		#region ICodeSet

		[Pure] public override bool this[Code code] {
			get {
				return sorted.Count != 0 && sorted[code.Value];
			}
		}

		[Pure] public override int Count {
			get {
				return sorted.Count;
			}
		}

		[Pure] public override int Length {
			get {
				return sorted.Span();
			}
		}

		[Pure] public override Code First {
			get {
				if (sorted.Count != 0) return (int)sorted.First;
				throw new InvalidOperationException();
			}
		}

		[Pure] public override Code Last {
			get {
				if (sorted.Count != 0) return (int)sorted.Last;
				throw new InvalidOperationException();
			}
		}

		[Pure] public override IEnumerator<Code> GetEnumerator () {
			foreach ( var code in sorted ) {
				yield return (Code)code;
			}
		}

		#endregion

		#region Theory

		static class Theory {

			[Pure] public static bool Construct(CodeSetWrap self) {

				// disable once ConvertToConstant.Local
				Success success = true;

				success.Assert (!self.sorted.IsNull());
				success.Assert (self.sorted.Count == 0);

				return success;
			}
			
			[Pure] public static bool Construct (CodeSetWrap bits, CodeSetWrap self) {

				// disable once ConvertToConstant.Local
				Success success = true;

				success.Assert (!bits.IsNull());
				success.Assert (!self.sorted.IsNull());

				success.Assert (self.sorted.Is(bits.sorted));

				return success;
			}
			
			[Pure] public static bool Construct (BitSetArray bits, CodeSetWrap self) {

				// disable once ConvertToConstant.Local
				Success success = true;

				success.Assert (self.sorted.IsNot (null));
				success.Assert (self.sorted.Count == bits.Count);

				if (bits.Count != 0) {
					foreach (var item in bits) {
						success.Assert (self.sorted[item]);
					}
				}
				else {
					success.Assert (Construct(self));
				}
				
				return success;
			}
			
			[Pure] public static bool Construct (IEnumerable<Code> codes, CodeSetWrap self) {

				// disable once ConvertToConstant.Local
				Success success = true;
				
				success.Assert (self.sorted.IsNot (null));

				if (!codes.IsNullOrEmpty()) {
					success.Assert (self.sorted.Count == codes.Distinct().Count());
					foreach (var item in codes) {
						success.Assert (self.sorted[item]);
					}
				}
				else {
					success.Assert (Construct(self));
				}
				
				return success;
			}
			
			[Pure] public static bool Invariant(CodeSetWrap self) {

				// disable once ConvertToConstant.Local
				Success success = true;
				
				// private
				success.Assert (!self.sorted.IsNull());

				success.Assert (
					self.sorted.Length <= Code.MaxCount ||
					self.sorted.Last <= Code.MaxValue
				);
	
				if (self.sorted.Count != 0) {
					success.Assert (((int)self.sorted.First).HasCodeValue ());
					success.Assert (((int)self.sorted.Last).HasCodeValue ());
				}
				else {
					success.Assert (self.sorted.First == null);
					success.Assert (self.sorted.Last == null);
				}
				
				// public <- private
				success.Assert (self.Length == self.sorted.Span());
				success.Assert (self.Count == self.sorted.Count);
				if (self.Count != 0) {
					success.Assert (self.First == self.sorted.First);
					success.Assert (self.Last == self.sorted.Last);
				}
				
				return success;
			}
		}

		#endregion
	}
}
