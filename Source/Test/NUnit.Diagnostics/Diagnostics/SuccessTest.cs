// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;

using NUnit.Framework;

namespace DD.Diagnostics {

    [TestFixture]
    public class SuccessTest {

        [Test]
        public void AssertFailCoverage () {

            Success success = true;

#if DEBUG && TRACE
            // store an remove listeners
            List<TraceListener> tListeners = new List<TraceListener> ();
            foreach ( TraceListener tl in Trace.Listeners ) {
                tListeners.Add (tl);
            }
            Trace.Listeners.Clear ();
#endif

            Assert.True (success);
            success.Assert (true, "Press ignore"); // no message
            Assert.True (success);
            success.Assert (false, "Press ignore"); // no message
            Assert.False (success);
            success.Assert (true, "Press ignore"); // no message
            Assert.False (success);
            success.Assert (false, "Press ignore"); // no message
            Assert.False (success);

#if DEBUG && TRACE
            // restore listeners
            foreach ( TraceListener li in tListeners ) {
                Trace.Listeners.Add (li);
            }
            tListeners.Clear ();

            Assert.False (success);
            success.Assert (true, "Press ignore"); // no message
            Assert.False (success);

            // shows message pointing at line below
            success.Assert (false, "line == 53, Press ignore"); // shows message pointing at this line
            Assert.False (success);

            success = true;
            Assert.True (success);
            success.Assert (true, "Press ignore"); // no message
            Assert.True (success);

            // shows message pointing at line below
            success.Assert (false, "line == 62, Press ignore"); // shows message pointing at this line
            Assert.False (success);
#endif
        }

    }

}