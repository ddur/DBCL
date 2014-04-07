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

namespace DD.Collections
{
	/// <summary>CodeSet covering blocks of more than one unicode plane</summary>
	public sealed class CodeSetWide : CodeSet, ICodeSet
	{
		
		#region Ctor
		
		internal CodeSetWide(IEnumerable<Code> codes)
		{
			Contract.Requires<ArgumentNullException> (!codes.Is(null));
			Contract.Requires<ArgumentException> (codes.Distinct().Count() > ICodeSetService.PairCount);
			Contract.Requires<ArgumentException> (codes.Distinct().Count() < (codes.Max() - codes.Min()));
			Contract.Requires<ArgumentException> (codes.Min().UnicodePlane() != codes.Max().UnicodePlane());

			Contract.Ensures (Theory.Construct (codes, this));

			var iCodeSet = codes as ICodeSet;
			if (iCodeSet.IsNot(null)) {
				this.start = iCodeSet.First;
				this.final = iCodeSet.Last;
			}
			else {
				foreach (Code code in codes) {
					if (this.start > code) this.start = code;
					if (this.final < code) this.final = code;
				}
			}
			this.init (ref this.startPlane, ref this.finalPlane, ref this.planes);
			this.init (codes);
			foreach (ICodeSet codeSet in this.planes) {
				this.count += codeSet.Count;
			}

		}

		internal CodeSetWide(BitSetArray bits, int offset = 0)
		{
			Contract.Requires<ArgumentNullException> (!bits.Is(null));
			Contract.Requires<ArgumentOutOfRangeException> (bits.Length <= Code.MaxCount || bits.Last <= Code.MaxValue);
			Contract.Requires<ArgumentException> (((int)bits.First + offset).HasCodeValue() && ((int)bits.Last + offset).HasCodeValue());
			Contract.Requires<ArgumentException> (bits.Count > ICodeSetService.PairCount);
			Contract.Requires<ArgumentException> (bits.Count < (bits.Last - bits.First));
			Contract.Requires<ArgumentException> (((Code)(bits.First + offset)).UnicodePlane() != ((Code)(bits.Last + offset)).UnicodePlane());

			Contract.Ensures (Theory.Construct (bits, offset, this));
			
			this.start = (Code)(bits.First + offset);
			this.final = (Code)(bits.Last + offset);

			this.init (ref this.startPlane, ref this.finalPlane, ref this.planes);
			this.init (bits, offset);
			foreach (ICodeSet codeSet in this.planes) {
				this.count += codeSet.Count;
			}
		}

		BitSetArray[] getBitPlanes() {
			var bitPlanes = new BitSetArray[this.planes.Length];
			for (int i = 0; i < this.planes.Length; i++) {
				bitPlanes[i] = new BitSetArray(char.MaxValue + 1);
			}
			return bitPlanes;
		}

		void init (ref int thisStartPlane, ref int thisFinalPlane, ref ICodeSet[] thisPlanes) {
			thisStartPlane = this.start.UnicodePlane();
			thisFinalPlane = this.final.UnicodePlane();
			thisPlanes = new ICodeSet[1 + thisFinalPlane - thisStartPlane];
		}

		void init (IEnumerable<int> bits, int offset) {
			var bitPlanes = getBitPlanes();
			foreach (var item in bits) {
				bitPlanes[(item >> 16) - this.startPlane].Set(item & 0xFFFF);
			}
			initPlanes(bitPlanes, offset);
		}

		void init (IEnumerable<Code> codes) {
			var bitPlanes = getBitPlanes();
			foreach (Code code in codes) {
				bitPlanes[(code >> 16) - this.startPlane].Set(code & 0xFFFF);
			}
			initPlanes(bitPlanes);
		}

		void initPlanes (BitSetArray[] bitPlanes, int offset = 0) {
			int index = 0;
			foreach (var bitPlane in bitPlanes) {
				this.planes[index] = bitPlane.Reduce(offset + ((index + this.startPlane) << 16));
				++index;
			}
		}

		#endregion

		#region Fields
		
		private readonly ICodeSet[] planes;
		private readonly Code start = Code.MaxValue;
		private readonly Code final = Code.MinValue;
		private readonly int startPlane = 0;
		private readonly int finalPlane = 0;
		private readonly int count = 0;
		
		#endregion

		#region ICodeSet

		[Pure] public override bool this[Code code] {
			get {
				int currentPlane = code.UnicodePlane();
				return currentPlane.InRange(this.startPlane, this.finalPlane) 
					&& planes[currentPlane - this.startPlane][code];
			}
		}

		[Pure] public override int Count {
			get {
				return this.count;
			}
		}

		[Pure] public override int Length {
			get {
				return 1 + this.final - this.start;
			}
		}

		[Pure] public override Code First {
			get {
				return this.start;
			}
		}

		[Pure] public override Code Last {
			get {
				return this.final;
			}
		}

		[Pure] public override IEnumerator<Code> GetEnumerator () {
			foreach (ICodeSet codeSet in this.planes) {
				foreach ( Code code in codeSet ) {
					yield return code;
				}
			}
		}

		#endregion

		#region Invariant

		[ContractInvariantMethod]
		private void Invariant () {
			Contract.Invariant (Theory.Invariant (this));
		}

		#endregion

		private static class Theory {
			
			[Pure] public static bool Construct (IEnumerable<Code> codes, CodeSetWide self) {
				
				// disable once ConvertToConstant.Local
				Success success = true;

				// input -> private
				success.Assert (codes.Distinct().Count() == self.count);
				var e = self.GetEnumerator();
				foreach (var item in codes.Distinct().OrderBy(code => (code))) {
					e.MoveNext();
					success.Assert (item == e.Current); // => SequenceEqual
					success.Assert (self.planes[item.UnicodePlane()][item]);
				}
			
				return success;
			}

			[Pure] public static bool Construct (BitSetArray bits, int offset, CodeSetWide self) {
				
				// disable once ConvertToConstant.Local
				Success success = true;
				
				// input -> private
				success.Assert (bits.Count == self.Count);
				var e = self.GetEnumerator();
				foreach (Code item in bits) {
					e.MoveNext();
					success.Assert ((item.Value + offset) == e.Current); // => SequenceEqual
					success.Assert (self.planes[item.UnicodePlane()][item + offset]);
				}
			
				return success;
			}

			[Pure] public static bool Invariant (CodeSetWide self) {

				// disable once ConvertToConstant.Local
				Success success = true;

				// private
				success.Assert (self.planes.IsNot (null));
				success.Assert (self.planes.Length > 1);
				success.Assert (self.planes.Length <= 1 + ((Code)Code.MaxValue).UnicodePlane());
				foreach (var plane in self.planes) {
					success.Assert (plane.IsNot (null));
				}

				var startPlane = self.planes.FirstOrDefault();
				var finalPlane = self.planes.LastOrDefault();
				success.Assert (startPlane.Count != 0);
				success.Assert (finalPlane.Count != 0);
				success.Assert (startPlane[self.start]);
				success.Assert (finalPlane[self.final]);

				int counter = 0; 
				foreach (ICodeSet iCodeSet in self.planes) {
					counter += iCodeSet.Count;
				}
				success.Assert (self.count == counter);
				success.Assert ((self.start) == startPlane.First);
				success.Assert ((self.final) == finalPlane.Last);
				success.Assert (self.count > ICodeSetService.PairCount);// not Null-Pair
				success.Assert (self.count < (self.final - self.start));// not Full-Pair
				
				// public <- private
				success.Assert (self.Count == self.count);
				success.Assert (self.First == self.start);
				success.Assert (self.Last == self.final);
				
				// constraints
				success.Assert (self.Count > ICodeSetService.PairCount);// not Null-Pair
				success.Assert (self.Count < (self.Length - 1)); 		// not Full-Pair
				success.Assert (self.First.UnicodePlane() != self.Last.UnicodePlane()); // spans over single unicode plane
				
				return success;
			}
		}
	}
}
