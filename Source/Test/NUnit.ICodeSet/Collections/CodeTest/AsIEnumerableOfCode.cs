// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace DD.Collections.CodeTest
{
    [TestFixture]
    public class AsIEnumerableOfCode
    {
        [Test]
        public void OfObject()
        {
            Code C = 34;
            ICodeSet ics = C;
            IEnumerable ie = C;
            var e = ie.GetEnumerator();
            object[] oa = new object[ics.Length];
            int count = 0;
            while (e.MoveNext()) {
                oa[count] = e.Current;
                ++count;
            }
            Assert.True ((oa.Cast<Code>()).SequenceEqual(ics));
            
        }

        [Test]
        public void OfCode()
        {
            Code C = 60;

            var e = ((IEnumerable<Code>)C).GetEnumerator();
            Code[] ca = new Code[((IEnumerable<Code>)C).Count()];
            int count = 0;
            while (e.MoveNext()) {
                ca[count] = e.Current;
                ++count;
            }
            Assert.True (ca.SequenceEqual(C));

            // from ICodeSet
            ICodeSet ics = C;

            var eICodSet = ics.GetEnumerator();
            ca = new Code[ics.Length];
            count = 0;
            while (eICodSet.MoveNext()) {
                ca[count] = eICodSet.Current;
                ++count;
            }
            Assert.True (ca.SequenceEqual(ics));
        }
    }
}
