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
	/// <summary>Wraps BitSetArray</summary>
	public sealed class CodeSetWrap : CodeSet
	{
		#region Ctor

		internal CodeSetWrap() : this (BitSetArray.Size())
		{
			Contract.Ensures (Theory.Construct(this));
		}

		/// <summary>Quick shallow clone</summary>
		/// <param name="wrap">CodeSetWrap</param>
		internal CodeSetWrap(CodeSetWrap wrap) : this (wrap.sorted)
		{
			Contract.Requires<ArgumentNullException>(wrap.IsNot(null));
			Contract.Ensures (Theory.Construct(wrap, this));
		}

		internal CodeSetWrap(IEnumerable<Code> codes) : this (BitSetArray.From (codes.ToValues()))
		{
			Contract.Requires<ArgumentNullException>(codes.IsNot(null));
			Contract.Ensures (Theory.Construct(codes, this));
		}

		/// <summary>Quick shallow ICodeSet wrapper around BitSetArray
		/// <remarks>Do not mutate BitSetArray!</remarks>
		/// </summary>
		/// <param name="bits">BitSetArray is used as reference, do not mutate!</param>
		internal CodeSetWrap(BitSetArray bits)
		{
			Contract.Requires<ArgumentNullException>(bits.IsNot(null));
			Contract.Requires<ArgumentOutOfRangeException>(bits.Count == 0 || bits.Length.IsCodesCount() || bits.Last.HasCodeValue());

			Contract.Ensures (Theory.Construct(bits, this));
			
 			version = bits.Version; // store to detect changes
			sorted = bits; // external reference, do not change!
		}

		#endregion

		#region Fields

		readonly BitSetArray sorted;
		readonly int version;

		#endregion

		#region ICodeSet

		[Pure] public override bool this[Code code] {
			get {
				Contract.Assert (sorted.Version == version);
				return sorted.Count != 0 && sorted[code.Value];
			}
		}

		[Pure] public override int Count {
			get {
				Contract.Assert (sorted.Version == version);
				return sorted.Count;
			}
		}

		[Pure] public override int Length {
			get {
				Contract.Assert (sorted.Version == version);
				return sorted.Span();
			}
		}

		[Pure] public override Code First {
			get {
				Contract.Assert (sorted.Version == version);
				if (sorted.Count != 0) return (int)sorted.First;
				throw new InvalidOperationException();
			}
		}

		[Pure] public override Code Last {
			get {
				Contract.Assert (sorted.Version == version);
				if (sorted.Count != 0) return (int)sorted.Last;
				throw new InvalidOperationException();
			}
		}

		[Pure] public override IEnumerator<Code> GetEnumerator () {
			foreach ( var code in sorted ) {
				Contract.Assert (sorted.Version == version);
				yield return (Code)code;
			}
		}

		#endregion

		#region Invariant

		[ContractInvariantMethod]
		private void Invariant () {
			Contract.Invariant (Theory.Invariant (this));
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
			
			[Pure] public static bool Construct (CodeSetWrap wrap, CodeSetWrap self) {

				// disable once ConvertToConstant.Local
				Success success = true;

				success.Assert (!wrap.IsNull());
				success.Assert (!wrap.sorted.IsNull());
				success.Assert (!self.sorted.IsNull());

				success.Assert (self.sorted.Is(wrap.sorted));

				return success;
			}
			
			[Pure] public static bool Construct (BitSetArray bits, CodeSetWrap self) {

				// disable once ConvertToConstant.Local
				Success success = true;

				success.Assert (!bits.IsNull());
				success.Assert (!self.sorted.IsNull());
				success.Assert (self.sorted.Is(bits));

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
					self.sorted.Length <= Code.MaxCount
				);
	
				if (self.sorted.Count != 0) {
					success.Assert (self.sorted.First.HasCodeValue ());
					success.Assert (self.sorted.Last.HasCodeValue ());
				}
				else {
					success.Assert (self.sorted.First == null);
					success.Assert (self.sorted.Last == null);
				}
				
				// public <- private
				success.Assert (self.Length == self.sorted.Span());
				success.Assert (self.Count == self.sorted.Count);
				if (self.Count != 0) {
					success.Assert (self.First == (int)self.sorted.First);
					success.Assert (self.Last == (int)self.sorted.Last);
				}
				
				return success;
			}
		}

		#endregion
	}
}
