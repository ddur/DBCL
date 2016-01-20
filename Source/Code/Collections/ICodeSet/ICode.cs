// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Diagnostics.Contracts;


namespace DD.Collections.ICodeSet
{
    /// <summary>
    /// Description of ICode.
    /// </summary>
    public interface ICode {

        [Pure]
        bool this[Code code] {
            get;
        }

        [Pure]
        bool this[int code] {
            get;
        }

        [Pure]
        bool IsEmpty {
            get;
        }
    }
}
