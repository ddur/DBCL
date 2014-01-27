// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

//#define COMPACT
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DD.Collections
{

    public struct Code : ICodeSet, IEquatable<Code>, IEqualityComparer<Code>, IComparable<Code> {

        #region Ctor

        public Code (byte value) {
            #if COMPACT
            this.b0 = value;
            this.b1 = 0;
            this.b2 = 0;
            #else
            this.val = value;
            #endif
        }

        public Code (char value) {
            #if COMPACT
            this.b0 = (byte)(value&0xFF);
            this.b1 = (byte)((value>>8)&0xFF);
            this.b2 = 0;
            #else
            this.val = value;
            #endif
        }

        public Code (int value) {
            Contract.Requires<ArgumentOutOfRangeException>(value.InRange(Code.MinValue, Code.MaxValue));

            #if COMPACT
            this.b0 = (byte)(value&0xFF);
            this.b1 = (byte)((value>>8)&0xFF);
            this.b2 = (byte)((value>>16)&0xFF);
            #else
            this.val = value;
            #endif
        }

        #endregion

        #region Const

        public const int MinValue = 0;
		public const int MaxValue = 0x10FFFF; // Max code-point value

		public const int MinCount = 0;
		public const int MaxCount = 1 + MaxValue; // MinValue.Count + [1..maxValue].Count; ie.Sequence[0..3].Count=4

        #endregion

        #region Fields

        // ATTN! do not add fields to Code structure! Keep it as smal as possible 

        #if COMPACT
        private readonly byte b0;
        private readonly byte b1;
        private readonly byte b2;
        #else
        private readonly int val;
        #endif
        
        #endregion

        #region Interfaces

        #region Code Interfaces

        #region IEquatable<Code>
        
        public override bool Equals(object obj)
        {
            return (obj is Code) && Equals((Code)obj);
        }
        
        public bool Equals(Code that)
        {
            return this.Value == that.Value;
        }
        
        public static bool operator ==(Code lhs, Code rhs)
        {
            return lhs.Equals(rhs);
        }
        
        public static bool operator !=(Code lhs, Code rhs)
        {
            return !(lhs == rhs);
        }

        
        #endregion

        #region IEqualityComparer<Code>
        
        [Pure] public bool Equals(Code a, Code b) {
            return a.Value == b.Value;
        }

        [Pure] public int GetHashCode(Code c) {
            return this.Value.GetHashCode();
        }
        
        #endregion

        #region IComparable<Code>
        
        public int CompareTo (Code that) {
            return this.Value.CompareTo(that.Value);
        }
        
        #endregion

        #endregion

        #region ICodeSet Interfaces

        #region ICodeSet Interface

        [Pure] bool ICodeSet.this [Code code] {
            get { return this.Value == code.Value; }
        }

        [Pure] int ICodeSet.Length {
            get { return ICodeSetService.UnitCount; }
        }

        [Pure] Code ICodeSet.First {
            get { return (Code)this; }
        }

        [Pure] Code ICodeSet.Last {
            get { return (Code)this; }
        }

        #endregion

        #region ICodeSet Inherited 

        #region ICollection<Code>
        
        [Pure] 
        bool ICollection<Code>.IsReadOnly {
            get { return true; }
        }
        [Pure] 
        int ICollection<Code>.Count {
            get { return ICodeSetService.UnitCount; }
        }

        /// <summary> Returns True if collection Contains (IsSupersetOf) specified code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>bool</returns>
        //[ContractOption("contract", "inheritance", false)]
        //[SuppressMessage("Microsoft.Contracts", "CC1033", Justification = "Contract inheritance option disabled. Ensures is just stupid")]
        [Pure] 
        bool ICollection<Code>.Contains(Code code) {
            return ((ICodeSet)this)[code];
        }

        /// <summary>Copies members from this collection into Code[]
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrIndex"></param>
        //[ContractOption("contract", "inheritance", false)]
        [SuppressMessage("Microsoft.Contracts", "CC1033", Justification = "Contract inheritance option disabled")]
        [Pure] 
        void ICollection<Code>.CopyTo(Code[] array, int arrayIndex) {
            Contract.Requires<ArgumentNullException>(!array.Is(null));
            Contract.Requires<ArgumentOutOfRangeException>(arrayIndex >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(arrayIndex <= (array.Length - 1));
            array[arrayIndex] = this;
        }

        /// <summary>Explicit interface implementation.<para>Operations not supported on Read-Only Collection</para>
        /// </summary>
        /// <param name="code"></param>
        [Pure] 
        void ICollection<Code>.Add(Code code) { throw new NotSupportedException(); }

        /// <summary>Explicit interface implementation.<para>Operations not supported on Read-Only Collection</para>
        /// </summary>
        [Pure] 
        void ICollection<Code>.Clear() { throw new NotSupportedException(); }

        /// <summary>Explicit interface implementation.<para>Operations not supported on Read-Only Collection</para>
        /// </summary>
        [Pure]
        bool ICollection<Code>.Remove(Code code) { throw new NotSupportedException(); }
        
        #endregion
        
        #region IEnumerable<Code>

        [Pure] IEnumerator IEnumerable.GetEnumerator () {
            return ((IEnumerable<Code>)this).GetEnumerator();
        }

        [Pure] IEnumerator<Code> IEnumerable<Code>.GetEnumerator() {
            yield return this;
        }
        
        #endregion
        
        #region IEquatable<ICodeSet>

        [Pure] public bool Equals (ICodeSet that) {
            return ICodeSetService.Equals (this, that);
        }

        [Pure] public override int GetHashCode() {
            return ICodeSetService.GetHashCode(this);
        }
        
        #endregion

        #region IEqualityComparer<ICodeSet>

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool Equals(ICodeSet a, ICodeSet b) {
            return ICodeSetService.Equals(a, b);
        }
        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int GetHashCode(ICodeSet c) {
            return ICodeSetService.GetHashCode(c);
        }

        #endregion
        
        #region IComparable<ICodeSet>
        
        public int CompareTo (ICodeSet that) {
            return ICodeSetService.CompareTo (this, that);
        }
        
        #endregion

        #endregion

        #endregion

        #endregion

        #region Invariant
        
        [ContractInvariantMethod]
        private void Invariant () {
            Contract.Invariant (((ICodeSet)this).Count == ICodeSetService.UnitCount);
            Contract.Invariant (((ICodeSet)this).Count == ((ICodeSet)this).Length);
            Contract.Invariant (((ICodeSet)this).First == ((ICodeSet)this).Last);
        }

        #endregion

        #region Methods

        [Pure]
		public string Encode ()
		{
		    if (this.HasCharValue) { return this.IsSurrogate? "" + (char)0xFFFD : "" + (char)this; }
			int v = Value - 0x10000;
			return "" + (char)((v>>10)|0xD800) + (char)((v&0x3FF)|0xDC00);
		}

        [Pure]
        public override string ToString()
        {
		    if ((Value&0xFF) == Value) {
		        char c = (char)Value;
		        if (char.IsControl(c)) {
		            return "\\x" + Value.ToString("X");
		        }
		        return "" + c;
		    }
            return "\\x" + Value.ToString("X");
        }

		[Pure]
		public string ToXmlEntity() {
		    if ((Value&0xFF) == Value) {
		        switch (Value) { // XML predefined entities
		            case ((int)'>'):
    				    return "&gt;";
    				case ((int)'<'):
    					return "&lt;";
    	            case ((int)'&'):
    					return "&amp;";
                    case ((int)'\''):
    					return "&apos;";
                    case ((int)'"'):
    					return "&quot;";
    			}
		    }
			return "&#x" + Value.ToString("X") + ";";
		}

        #endregion

        #region Properties

        [Pure]
        public int Value {
            get {
                #if COMPACT
                int v = this.b0;
                v |= (this.b1<<8);
                v |= (this.b2<<16);
                return v;
                #else
                return this.val;
                #endif
            }
        }

        [Pure]
        public bool HasCharValue {
            get {
                Contract.Ensures (Contract.Result<bool> () == (Value.InRange (0, 0xFFFF)));

    			return Value.HasCharValue();
            }
        }

        [Pure]
        public bool IsSurrogate {
            get {
                Contract.Ensures (Contract.Result<bool> () == (Value.InRange (0xD800, 0xDFFF)));

    		    return (this.HasCharValue && char.IsSurrogate((char)Value));
            }
        }

        [Pure]
        public bool IsHighSurrogate {
            get {
                Contract.Ensures (Contract.Result<bool> () == (Value.InRange (0xD800, 0xDBFF)));

    		    return (this.HasCharValue && char.IsHighSurrogate((char)Value));
            }
        }

        [Pure]
        public bool IsLowSurrogate {
            get {
    		    Contract.Ensures (Contract.Result<bool>() == (( 0xDC00 <= Value ) && ( Value <= 0xDFFF )));
    
    		    return (this.HasCharValue && char.IsLowSurrogate((char)Value));
            }
        }

        [Pure]
        public bool IsPermanentlyUndefined {
            get {
    		    Contract.Ensures (Contract.Result<bool>() == 
                                  (Value.InRange(0xFDD0, 0xFDDF)) || 
                                  (Value > 0xFF && ((Value&0xFFFF) == 0xFFFE || (Value&0xFFFF) == 0xFFFF))
                                 );
                return (Value.InRange(0xFDD0, 0xFDDF) ||
                        (Value > 0xFF &&
                         ((Value&0xFFFF) == 0xFFFE || (Value&0xFFFF) == 0xFFFF)));
            }
        }
        
        [Pure]
        public int UnicodePlane {
            get {
                return Value >> 16;
            }
        }

        /// <summary>http://www.w3.org/TR/2008/REC-xml-20081126/#charsets</summary>
        [Pure]
        public bool IsXml10Char {
            get {
                return (Value == 0x9 || Value == 0xA || Value == 0xD ||   
                         Value.InRange(0x20, 0xD7FF) ||
                         Value.InRange(0xE000, 0xFFFD) || 
                         Value.InRange(0x10000, 0x10FFFF));
            }
        }
        
        /// <summary>http://www.w3.org/TR/2008/REC-xml-20081126/#charsets</summary>
        [Pure]
        public bool IsXml10Discouraged {
            get {
                return ( Value.InRange(0x7F, 0x84) ||
                         Value.InRange(0x86, 0x9F) ||
                         this.IsPermanentlyUndefined
                        );
            }
        }
        
        /// <summary>http://www.w3.org/TR/2006/REC-xml11-20060816/#charsets</summary>
        [Pure]
        public bool IsXml11Char {
            get {
                return ((Value.InRange(0x1, 0xD7FF) ||
                         Value.InRange(0xE000, 0xFFFD) || 
                         Value.InRange(0x10000, 0x10FFFF)));
            }
        }
        
        /// <summary>http://www.w3.org/TR/2006/REC-xml11-20060816/#charsets</summary>
        [Pure]
        public bool IsXml11Restricted {
            get {
                return (Value.InRange(0x1, 0x8) || 
                        Value.InRange(0xB, 0xC) || 
                        Value.InRange(0xE, 0x1F) || 
                        Value.InRange(0x7F, 0x84) || 
                        Value.InRange(0x86, 0x9F));
            }
        }

        /// <summary>http://www.w3.org/TR/2006/REC-xml11-20060816/#charsets</summary>
        [Pure]
        public bool IsXml11Discouraged {
            get {
                return (this.IsPermanentlyUndefined || this.IsXml11Restricted);
            }
        }

        #endregion

        #region Cast Operators
        
        public static implicit operator int (Code code) {
            return code.Value;
        }
        public static implicit operator char[] (Code code) {
            return code.Encode().ToCharArray();
        }
        public static implicit operator Code (byte value) {
            return new Code(value);
        }
        public static implicit operator Code (char value) {
            return new Code(value);
        }
        public static implicit operator Code (int value) {
            Contract.Requires<InvalidCastException>(value.InRange(Code.MinValue, Code.MaxValue));

            return new Code(value);
        }

        #endregion
        
    }
}
