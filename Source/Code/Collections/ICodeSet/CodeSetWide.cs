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
using DD.Enumerables;

namespace DD.Collections
{
	/// <summary>Set covering more than one unicode plane
	/// Description of CodeSetWide.
	/// </summary>
	public class CodeSetWide : CodeSet, ICodeSet
	{
		
		#region Ctor
		private CodeSetWide() {
			
		}
		
		internal CodeSetWide(IEnumerable<Code> codes)
		{
			Contract.Requires<ArgumentNullException> (!codes.Is(null));
			Contract.Requires<ArgumentException> (codes.Distinct().Count() > ICodeSetService.ListMaxCount);
			Contract.Requires<ArgumentException> (codes.Min().UnicodePlane() != codes.Max().UnicodePlane());
			
			Contract.Ensures (this.planes.IsNot(null));
			Contract.Ensures (this.planes.Length > 1);
			Contract.Ensures (this.Count > ICodeSetService.ListMaxCount);
			Contract.Ensures (this.First.UnicodePlane() != this.Last.UnicodePlane());

			// Input -> Output
			Contract.Ensures (codes.Distinct().Count() == this.Count);
			Contract.Ensures (Contract.ForAll (codes, item => this[item]));
			
			if (codes is ICodeSet) {
				var iCodeSet = codes as ICodeSet;
				this.start = iCodeSet.First;
				this.final = iCodeSet.Last;
			}
			else {
				foreach (Code code in codes) {
					if (this.start > code) this.start = code;
					if (this.final < code) this.final = code;
				}
			}
			this.Init (codes, ref this.startPlane, ref this.finalPlane, ref this.planes, ref this.count);

		}

		internal CodeSetWide(BitSetArray bits)
		{
			Contract.Requires<ArgumentNullException> (!bits.Is(null));
			Contract.Requires<ArgumentOutOfRangeException> (bits.Last <= Code.MaxValue);
			Contract.Requires<ArgumentException> (bits.Count > ICodeSetService.ListMaxCount);
			Contract.Requires<ArgumentException> (((Code)bits.First).UnicodePlane() != ((Code)bits.Last).UnicodePlane());

			Contract.Ensures (this.Count > 0);
			Contract.Ensures (this.Length > char.MaxValue + 1);
			Contract.Ensures (this.planes.Length > 1);

			// Input -> Output
			Contract.Ensures (this.Count == bits.Count);
			Contract.Ensures (Contract.ForAll (bits, item => this[item]));
			
			this.start = (Code)bits.First;
			this.final = (Code)bits.Last;
			
			this.Init (bits.Cast<Code>(), ref this.startPlane, ref this.finalPlane, ref this.planes, ref this.count);
		}

		private void Init (IEnumerable<Code> codes, ref int startPlane, ref int finalPlane, ref ICodeSet[] thisPlanes, ref int thisCount) {

			startPlane = this.start.UnicodePlane();
			finalPlane = this.final.UnicodePlane();
			thisPlanes = new ICodeSet[1 + finalPlane - startPlane];

			int counter = 0;
			var planesList = new List<Code>[this.planes.Length];
			var loop = thisPlanes.Length.Times();
			while (loop.Do) {
				planesList[counter] = new List<Code>();
				++counter;
			}
			foreach (Code code in codes) {
				planesList[code.UnicodePlane() - startPlane].Add(code);
			}
			int plane = 0;
			foreach (List<Code> codeList in planesList) {
				if (codeList.Count == 0) {
					thisPlanes[plane] = CodeSetNull.Singleton;
				} else {
					thisPlanes[plane] = new CodeSetBits(codeList).Optimal();
				}
				++plane;
			}

			thisCount = 0;
			foreach (ICodeSet codeSet in this.planes) {
				thisCount += codeSet.Count;
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
			// private
			Contract.Invariant (this.planes.IsNot (null));
			Contract.Invariant (this.planes.Length.InRange (2, 1 + ((Code)Code.MaxValue).UnicodePlane()));
			Contract.Invariant (Contract.ForAll (this.planes, plane => plane.IsNot (null)));
			
			// public <- private
			Contract.Invariant (this.First == this.planes[0].First);
			Contract.Invariant (this.Last == this.planes.Last().Last);
			int count = 0; foreach (ICodeSet iCodeSet in this.planes) { count += iCodeSet.Count; }
			Contract.Invariant (this.Count == count);
			
			// public
			Contract.Invariant (this.Count > ICodeSetService.ListMaxCount);
			Contract.Invariant (this.Length > 1 + char.MaxValue);
			Contract.Invariant (this.First.UnicodePlane() != this.Last.UnicodePlane());
		}

		#endregion

	}
}
