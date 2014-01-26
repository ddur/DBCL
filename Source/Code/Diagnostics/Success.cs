// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace DD.Diagnostics {

    /// <summary>Assert/Trace class returning caller line and column
    /// </summary>
    public struct Success {

        #region Ctor
        public Success (bool value) {
            this.result = value;
        }
        #endregion

        #region Struct

        private bool result;

        #endregion

        #region Methods

        #region Public

        /// <summary>If condition is false and success is true, then set success to false.
        /// <para>If condition is false and DEBUG&TRACE enabled then show line/column where assertion is failed</para>
        /// <remarks>
        /// Why? Instead large Contract.Ensures use static method with Success.Assert's 
        /// Why? When nontrivial (multiple OR conditions) Contract.Ensures fails,
        /// it does not reveal exact condition/line of failure that breaks Contract
        /// </remarks>
        /// </summary>
        /// <param name="condition">bool (expression)</param>
        /// <param name="message">string</param>
        public bool Assert (bool condition, string message = "") {

            if ( !condition ) {
                if ( this.result ) {
                    this.result = false;
                }

#if DEBUG && TRACE
                foreach ( TraceListener tl in Trace.Listeners ) {
                    // default UI enabled listener exists?
                    if ( tl is DefaultTraceListener && ((DefaultTraceListener)tl).AssertUiEnabled ) {
                        // get call stack with file information.
                        StackTrace trace = new StackTrace (true);
                        if ( trace.FrameCount > 1 ) { // caller of this method exists?
                            // get caller StackFrame (first above this StackFrame)
                            StackFrame myFrame = trace.GetFrame (1);

                            // create "trace" message
                            string methodFullName = GetTypeName (myFrame.GetMethod ());
                            StringBuilder traceMessage = new StringBuilder (String.Empty);
                            traceMessage.AppendLine ("Method: " + methodFullName);
                            traceMessage.AppendLine ();
                            traceMessage.AppendLine ("Path: " + Path.GetDirectoryName (myFrame.GetFileName ()));
                            traceMessage.AppendLine ();
                            traceMessage.AppendLine ("File: " + Path.GetFileName (myFrame.GetFileName ()));
                            traceMessage.AppendLine ("Line: " + myFrame.GetFileLineNumber ());
                            traceMessage.AppendLine ("Col.: " + myFrame.GetFileColumnNumber ());

                            // offer to abort(exit), retry(debug) or ignore(continue)
                            switch ( System.Windows.Forms.MessageBox.Show (traceMessage.ToString (), 
                                String.IsNullOrEmpty (message) ? "Assertion message IsNullOrEmpty!" : message, 
                                System.Windows.Forms.MessageBoxButtons.AbortRetryIgnore) ) {
                                case System.Windows.Forms.DialogResult.Abort:
                                    Environment.Exit (0);
                                    break;
                                case System.Windows.Forms.DialogResult.Retry:
                                    Debugger.Launch ();
                                    break;
                            }
                        }
                        break;
                    }
                }
#endif
            }
            return this.result;
        }

        #endregion

        #region Private

        private static string GetTypeName (Type dtype) {
            if ( dtype.DeclaringType != null ) {
                return GetTypeName (dtype.DeclaringType) + "." + dtype.Name;
            }
            else {
                return dtype.Name;
            }
        }

        private static string GetTypeName (MethodBase method) {
            MethodInfo info = method as MethodInfo;
            if ( info != null ) {
                return info.ReturnType + " " + GetTypeName (info.DeclaringType) + "." + method.Name;
            }
            else {
                return GetTypeName (method.DeclaringType) + "." + method.Name;
            }
        }

        #endregion

        #endregion

        #region Cast Operators

        public static implicit operator bool (Success s) {
            return s.result;
        }
        public static implicit operator Success (bool b) {
            return new Success (b);
        }

        #endregion
    }
}