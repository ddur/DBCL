// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using DD.Diagnostics;
using DD.Enumerables;

namespace DD.Collections
{
	/// <summary>Set covering more than one unicode plane
	/// Description of CodeSetWide.
	/// </summary>
	public class CodeSetWide : CodeSet, ICodeSet
	{
		
		#region Ctor
		
		internal CodeSetWide(IEnumerable<Code> codes)
		{
			Contract.Requires<ArgumentNullException> (!codes.Is(null));
			Contract.Requires<ArgumentException> (codes.Distinct().Count() > ICodeSetService.ListMaxCount);
			Contract.Requires<ArgumentException> (codes.Min().UnicodePlane() != codes.Max().UnicodePlane());

			Contract.Ensures (Theory.Construct (codes, this));

			var iCodeSet = codes as ICodeSet;
			if (codes.IsNot(null)) {
				this.start = iCodeSet.First;
				this.final = iCodeSet.Last;
			}
			else {
				foreach (Code code in codes) {
					if (this.start > code) this.start = code;
					if (this.final < code) this.final = code;
				}
			}
			this.Init (ref this.startPlane, ref this.finalPlane, ref this.planes);

			var planesList = Enumerable.Repeat(new List<Code>(),this.planes.Length).ToArray();
			foreach (Code code in codes) {
				planesList[code.UnicodePlane() - this.startPlane].Add(code);
			}
			this.Init (planesList);
			foreach (ICodeSet codeSet in this.planes) {
				this.count += codeSet.Count;
			}

		}

		internal CodeSetWide(BitSetArray codes)
		{
			Contract.Requires<ArgumentNullException> (!codes.Is(null));
			Contract.Requires<ArgumentException> (codes.Count > ICodeSetService.ListMaxCount);
			Contract.Requires<ArgumentOutOfRangeException> (codes.Last <= Code.MaxValue);
			Contract.Requires<ArgumentException> (((Code)codes.First).UnicodePlane() != ((Code)codes.Last).UnicodePlane());

			Contract.Ensures (Theory.Construct (codes, this));
			
			this.start = (Code)codes.First;
			this.final = (Code)codes.Last;

			this.Init (ref this.startPlane, ref this.finalPlane, ref this.planes);

			var planesList = Enumerable.Repeat(new List<Code>(),this.planes.Length).ToArray();
			foreach (Code code in codes) {
				planesList[code.UnicodePlane() - this.startPlane].Add(code);
			}
			this.Init (planesList);
			foreach (ICodeSet codeSet in this.planes) {
				this.count += codeSet.Count;
			}
		}

		void Init (ref int thisStartPlane, ref int thisFinalPlane, ref ICodeSet[] thisPlanes) {
			thisStartPlane = this.start.UnicodePlane();
			thisFinalPlane = this.final.UnicodePlane();
			thisPlanes = new ICodeSet[1 + thisFinalPlane - thisStartPlane];
		}
		
		void Init (IEnumerable<List<Code>> planesList) {
			int plane = 0;
			foreach (List<Code> codeList in planesList) {
				if (codeList.Count == 0) {
					this.planes[plane] = CodeSetNull.Singleton;
				} else {
					this.planes[plane] = new CodeSetBits(codeList).Optimal();
				}
				++plane;
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
				if (code.UnicodePlane().InRange (this.startPlane, this.finalPlane)) {
					return planes[code.UnicodePlane()-this.startPlane][code];
				}
				return false;
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
				foreach (var item in codes) {
					success.Assert (self.planes[item.UnicodePlane()][item]);
				}
			
				return success;
			}

			[Pure] public static bool Construct (BitSetArray codes, CodeSetWide self) {
				
				// disable once ConvertToConstant.Local
				Success success = true;
				
				// input -> private
				success.Assert (codes.Count == self.Count);
				foreach (Code item in codes) {
					success.Assert (self.planes[item.UnicodePlane()][item]);
				}
			
				return success;
			}

			[Pure] public static bool Invariant (CodeSetWide self) {

				// disable once ConvertToConstant.Local
				Success success = true;

				// private
				success.Assert (self.planes.IsNot (null));
				success.Assert (self.planes.Length.InRange (2, 1 + ((Code)Code.MaxValue).UnicodePlane()));
				foreach (var plane in self.planes) {
					success.Assert (plane.IsNot (null));
				}
				int counter = 0; foreach (ICodeSet iCodeSet in self.planes) { counter += iCodeSet.Count; }
				success.Assert (self.count == counter);
				success.Assert (self.start == self.planes[0].First);
				success.Assert (self.final == self.planes[self.planes.Length-1].Last);
				
				// public <- private
				success.Assert (self.Count == self.count);
				success.Assert (self.First == self.start);
				success.Assert (self.Last == self.final);
				
				// constraints
				success.Assert (self.Count > ICodeSetService.ListMaxCount);
				success.Assert (self.Length > 1 + char.MaxValue);
				success.Assert (self.First.UnicodePlane() != self.Last.UnicodePlane());
				
				return success;
			}
		}
	}
}
