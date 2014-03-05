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
    /// <summary>Intermediate ICodeSet that is later optimized into other ICodeSet implementations
    /// </summary>
    public sealed class CodeSetBits : CodeSet
    {
        #region Ctor

		/// <summary>Empty CodeSetBits</summary>
		/// <remarks>Only CodeSetBits and CodeSetNull can be Empty</remarks>
        internal CodeSetBits () {
			Contract.Ensures (Theory.Construct(this));

            this.sorted = new BitSetArray();
        }

        internal CodeSetBits (IEnumerable<Code> codes) {
            Contract.Requires<ArgumentNullException> (codes.IsNot(null));
			Contract.Ensures (Theory.Construct(codes, this));

            if (!codes.IsEmpty()) {
                var codeSet = codes as ICodeSet;
				if (codeSet.IsNot(null)) {
                    this.start = codeSet.First;
                    this.final = codeSet.Last;
                }
                else {
                    this.start = int.MaxValue;
                    this.final = int.MinValue;
                    foreach ( Code code in codes ) {
                        if ( code < this.start )
                            this.start = code;
                        if ( code > this.final )
                            this.final = code;
                    }
                }

                // codes is same class?
                if (codes is CodeSetBits) {
                    // ICodeSet is ReadOnly => can share internals
                    this.sorted = ((CodeSetBits)codes).sorted;
                }
                else {
                    this.sorted = new BitSetArray (this.final - this.start + 1);
                    foreach ( Code code in codes ) {
                        this.sorted.Set (code - this.start, true);
                    }
                }
            }
            else {
                this.sorted = new BitSetArray ();
            }
        }

		/// <summary>From BitSetArray</summary>
		/// <param name="bits">BitSetArray</param>
        internal CodeSetBits (BitSetArray bits) {

            Contract.Requires<ArgumentNullException> (bits.IsNot(null));
			Contract.Requires<ArgumentException> (bits.Count == 0 || bits.Last <= Code.MaxValue);
			Contract.Ensures (Theory.Construct(bits, this));

            if (bits.Count != 0) {
                this.start = (int)bits.First;
                this.final = (int)bits.Last;
                this.sorted = new BitSetArray (this.final - this.start + 1);
                foreach ( Code code in bits ) {
                    this.sorted.Set (code - this.start, true);
                }
            }
            else {
                this.sorted = new BitSetArray ();
            }
        }

        /// <summary>From Compact BitSetArray</summary>
        /// <param name="bits">Compact BitSetArray</param>
        /// <param name="offset">int</param>
        internal CodeSetBits (BitSetArray bits, int offset) {

			Contract.Requires<ArgumentNullException> (bits.IsNot(null));
            Contract.Requires<ArgumentException> (bits.Length <= Code.MaxCount);
			Contract.Requires<ArgumentException> (bits.Count == 0 || (bits[0] && bits[bits.Length-1]));
            Contract.Requires<ArgumentException> (offset >= 0);
            Contract.Requires<ArgumentException> (offset <= Code.MaxCount - bits.Length);

			Contract.Ensures (Theory.Construct(bits, offset, this));

            this.sorted = BitSetArray.Copy(bits);

			if (bits.Count != 0) {
	            this.start = offset;
	            this.final = offset + this.sorted.Length - 1;
			}
        }

        #endregion

        #region Fields

        private readonly BitSetArray sorted;
        private readonly int start = ICodeSetService.NullStart;
        private readonly int final = ICodeSetService.NullFinal;

        #endregion

        #region ICodeSet

        [Pure] public override bool this[Code code] {
            get {
				return this.sorted.Count != 0 && sorted[code - this.start];
            }
        }

        [Pure] public override int Count {
            get {
                return this.sorted.Count;
            }
        }

        [Pure] public override Code First {
            get {
                if (this.sorted.Count != 0) return this.start;
                throw new InvalidOperationException();
            }
        }

        [Pure] public override Code Last {
            get {
                if (this.sorted.Count != 0) return this.final;
                throw new InvalidOperationException();
            }
        }

        [Pure] public override IEnumerator<Code> GetEnumerator () {
            foreach ( var code in this.sorted ) {
                yield return this.start + code;
            }
        }

        #endregion

        #region Invariant

        [ContractInvariantMethod]
        private void Invariant () {
			Contract.Invariant (Theory.Invariant(this));
        }

        #endregion

        internal BitSetArray ToCompact () {
            return BitSetArray.Copy (this.sorted);
        }
        
        internal IEnumerable<Code> Complement {
            get {
                foreach (var item in this.sorted.Complement()) {
                    yield return item + this.start;
                }
            }
        }

		#region Theory

		static class Theory {

			[Pure] public static bool Construct(CodeSetBits self) {
				return self.sorted.Count == 0;
			}
			
			[Pure] public static bool Construct (IEnumerable<Code> codes, CodeSetBits self) {

				// disable once ConvertToConstant.Local
				Success success = true;
				
	            success.Assert (codes.IsEmpty() || Contract.ForAll (codes, item => self.sorted[item-self.start]));
				if (!codes.IsNullOrEmpty()) {
		            success.Assert (self.sorted.Count == codes.Distinct().Count());
					success.Assert (self.start == codes.Min());
					success.Assert (self.final == codes.Max());
				}
				else {
		            success.Assert (self.Count == 0);
				}
				
				return success;
			}
			
			[Pure] public static bool Construct (BitSetArray bits, CodeSetBits self) {

				// disable once ConvertToConstant.Local
				Success success = true;

	            success.Assert (self.sorted.Count == bits.Count);

				if (bits.Count != 0) {
					success.Assert (self.start == (int)bits.First);
					success.Assert (self.final == (int)bits.Last);
					foreach (var item in bits) {
						success.Assert (self.sorted[item-self.start]);
					}
				}
				
				return success;
			}
			
			[Pure] public static bool Construct (BitSetArray bits, int offset, CodeSetBits self) {

				// disable once ConvertToConstant.Local
				Success success = true;
				
				// compact bits?
				if (bits.Count != 0) {
					success.Assert (bits[0]);
					success.Assert (bits[bits.Length-1]);
				}

				success.Assert (self.sorted.Count == bits.Count);
				success.Assert (self.sorted.Equals(bits));

				if (bits.Count != 0) {
					success.Assert (self.start == offset);
					success.Assert (self.final == bits.Length + offset - 1);
					foreach (var item in bits) {
						success.Assert (self.sorted[item]);
					}
				}

				return success;
			}
			
			[Pure] public static bool Invariant(CodeSetBits self) {

				// disable once ConvertToConstant.Local
				Success success = true;
				
				// private
	            success.Assert (self.sorted.IsNot (null));
	            success.Assert (self.sorted.Length <= Code.MaxCount);
	            success.Assert (self.sorted.Length == 1 + self.final - self.start);
	
	            if (self.sorted.Count != 0) {
	                success.Assert (self.start.HasCodeValue ());
	                success.Assert (self.final.HasCodeValue ());
	                success.Assert (self.sorted[0]);
	                success.Assert (self.sorted[self.sorted.Length - 1]);
	            }
	            else {
	                success.Assert (self.start == ICodeSetService.NullStart);
	                success.Assert (self.final == ICodeSetService.NullFinal);
	            }
	            
	            // public <- private
	            success.Assert (self.Length == self.sorted.Length);
	            success.Assert (self.Count == self.sorted.Count);
	            if (self.Count != 0) {
	                success.Assert (self.First == self.start);
	                success.Assert (self.Last == self.final);
	            }
				
				return success;
			}
		}

		#endregion
    }
}
