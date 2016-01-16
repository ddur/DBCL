﻿// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DD.Collections.ICodeSet {

    public interface ICode {

        [Pure]
        bool this[Code code] {
            get;
        }

        [Pure]
        bool this[int code] {
            get;
        }

    }
    /// <summary>ICodeSet interface
    /// <remarks>ICodeSet implementation requires sorted/ordered IEnumerable&lt;Code&gt;</remarks>
    /// </summary>
    public interface ICodeSet :
        ICode, ICollection<Code>, IEnumerable<Code>, IEquatable<ICodeSet>,
        IEqualityComparer<ICodeSet>, IComparable<ICodeSet> {

        [Pure]
        int Length {
            get;
        }

        [Pure]
        Code First {
            get;
        }

        [Pure]
        Code Last {
            get;
        }

        [Pure]
        bool IsReduced {
        	get;
        }
    }
}
