// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DD.Collections
{
	/// <summary>Abstract base class for ICodeSet implementations
	/// <remarks>Implements ICodeSet inherited interfaces for derived classes</remarks>
	/// </summary>
	public abstract class CodeSet : ICodeSet
	{
		#region Ctor

		protected internal CodeSet () {}

		#endregion

		#region ICodeSet

		[Pure] public abstract bool this [Code code] { get; }
		
		[Pure] public bool this [int value] {
			get {
				return value.HasCodeValue() && this[(Code)value];
			}
		}
		
		[Pure] public abstract Code First { get; }
		
		[Pure] public abstract Code Last { get; }
		
		[Pure] public virtual int Length {
			get {
				if (this.Count == 0) {
					return 0;
				} else {
					return (1 + this.Last - this.First);
				}
			}
		}
		
		[Pure] IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }
		
		[Pure] public abstract IEnumerator<Code> GetEnumerator();
		
		[Pure] public virtual bool Equals (ICodeSet that) {
			// disable once InvokeAsExtensionMethod
			Contract.Ensures (Contract.Result<bool>() == ICodeSetService.Equals(this,that));
			// disable once InvokeAsExtensionMethod
			return ICodeSetService.Equals (this, that);
		}

		[Pure] public int CompareTo (ICodeSet that) {
			// disable once InvokeAsExtensionMethod
			Contract.Ensures (Contract.Result<int>() == ICodeSetService.CompareTo(this, that));
			// disable once InvokeAsExtensionMethod
			return ICodeSetService.CompareTo (this, that);
		}

		#endregion

		#region Equals<object> & GetHashCode()

		[Pure] public override bool Equals(object obj)
		{
			// disable once InvokeAsExtensionMethod
			Contract.Ensures (Contract.Result<bool>() == ((obj is ICodeSet) && ICodeSetService.Equals(this, (ICodeSet)obj)));
			// disable once InvokeAsExtensionMethod
			return (obj is ICodeSet) && ICodeSetService.Equals(this, (ICodeSet)obj);
		}
		
		[Pure] public override int GetHashCode()
		{
			// disable once InvokeAsExtensionMethod
			Contract.Ensures (Contract.Result<int>() == ICodeSetService.GetHashCode(this));
			return ICodeSetService.GetHashCode(this);
		}
		
		[Pure] public static bool operator ==(CodeSet lhs, CodeSet rhs)
		{
			return lhs.Equals(rhs);
		}
		
		[Pure] public static bool operator !=(CodeSet lhs, CodeSet rhs)
		{
			return !lhs.Equals(rhs);
		}

		#endregion

		#region IEqualityComparer<ICodeSet>

		/// <summary>
		/// Determines whether the specified objects are equal.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		[Pure] public bool Equals(ICodeSet a, ICodeSet b) {
			// disable once InvokeAsExtensionMethod
			Contract.Ensures (Contract.Result<bool>() == ICodeSetService.Equals(a, b));
			// disable once InvokeAsExtensionMethod
			return ICodeSetService.Equals(a, b);
		}
		/// <summary>
		/// Returns a hash code for the specified object.
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		[Pure] public int GetHashCode(ICodeSet that) {
			// disable once InvokeAsExtensionMethod
			Contract.Ensures (Contract.Result<int>() == ICodeSetService.GetHashCode(that));
			return ICodeSetService.GetHashCode(that);
		}

		#endregion

		#region ICollection
		
		[Pure] public abstract int Count { get; }
		
		[Pure] public bool IsReadOnly {
			get {
				return true;
			}
		}

		/// <summary>Returns True if collection Contains (IsSupersetOf) specified code
		/// </summary>
		/// <param name="code"></param>
		/// <returns>bool</returns>
		[Pure] public bool Contains(Code code) {
			Contract.Ensures (Contract.Result<bool>() == this[code]);
			return this[code];
		}

		/// <summary>Copies members from this collection into Code[]
		/// </summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		[SuppressMessage("Microsoft.Contracts", "CC1033", Justification = "Not same exceptions")]
		[Pure] public void CopyTo(Code[] array, int arrayIndex) {
			Contract.Requires<ArgumentNullException>(!array.Is(null));
			Contract.Requires<ArgumentOutOfRangeException>(arrayIndex >= 0);
			Contract.Requires<ArgumentOutOfRangeException>(arrayIndex <= (array.Length - this.Count));
			foreach (Code code in this) {
				array[arrayIndex] = code;
				++arrayIndex;
			}
		}

		/// <summary>Explicit interface implementation.<para>Operations not supported on Read-Only Collection</para>
		/// </summary>
		/// <param name="code"></param>
		[Pure] void ICollection<Code>.Add(Code code) { throw new NotSupportedException(); }

		/// <summary>Explicit interface implementation.<para>Operations not supported on Read-Only Collection</para>
		/// </summary>
		[Pure] void ICollection<Code>.Clear() { throw new NotSupportedException(); }

		/// <summary>Explicit interface implementation.<para>Operations not supported on Read-Only Collection</para>
		/// </summary>
		[Pure] bool ICollection<Code>.Remove(Code code) { throw new NotSupportedException(); }

		#endregion

	}

}
