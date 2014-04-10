// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace DD
{
	/// <summary>IEnumerable is Empty</summary>
	public class ArgumentEmptyException : ArgumentException
	{
		public ArgumentEmptyException() {}
		public ArgumentEmptyException(string message) : base (message) {}
		public ArgumentEmptyException(string message, Exception innerException) : base (message, innerException) {}
		public ArgumentEmptyException(string message, string paramName, Exception innerException) : base (message, paramName, innerException) {}
		public ArgumentEmptyException(string message, string paramName) : base (message, paramName) {}
		public ArgumentEmptyException(SerializationInfo info, StreamingContext context) : base (info, context) {}
	}
}
