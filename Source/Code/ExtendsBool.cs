// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Diagnostics.Contracts;

namespace DD {

    public static class ExtendsBool {

        /// <summary>Fix possible invalid boolean
        /// <remarks>Apply on bool argument to fix Pex/CIL generated/injected bug into C#
        /// <para>Ie. to prevent program logic failures,</para>
        /// <para>before using "value" set "value = value.Bool();".</para>
        /// <para>http://social.msdn.microsoft.com/Forums/en-US/50858997-8784-417d-b3f1-d0ada448b79b/pexsafehelpersbytetoboolean</para>
        /// <para>http://connect.microsoft.com/VisualStudio/feedback/details/455553/boolean-comparison-in-net#details</para>
        /// <para>http://social.msdn.microsoft.com/Forums/en-US/47e69fa8-fb33-4b67-966f-b4ad82a241dd/pexforfun-unsolvable-duels-due-to-crazy-bugs-in-pex</para>
        /// <para>http://blog.dotnetwiki.org/default,date,2009-06-02.aspx</para>
        /// </remarks>
        /// </summary>
        /// <param name="self">this bool</param>
        /// <returns>bool</returns>
        [Pure]
        public static bool Bool (this bool self) {
            return Convert.ToInt32 (self) != 0;
        }
    }
}