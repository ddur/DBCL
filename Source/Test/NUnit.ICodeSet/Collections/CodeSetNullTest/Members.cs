// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Linq;
using NUnit.Framework;

using DD.Enumerables;

namespace DD.Collections.CodeSetNullTest
{
    [TestFixture]
    public class Members
    {
        [Test]
        public void AllMembers()
        {
            CodeSetNull csn = CodeSetNull.Singleton;

            Assert.Throws<InvalidOperationException> (delegate{Code c = csn.First;});
            Assert.Throws<InvalidOperationException> (delegate{Code c = csn.Last;});

            Assert.True (csn.Count == 0);
            Assert.True (csn.Length == 0);
            Assert.True (csn.SequenceEqual(new Code[0]));
            
            Random r = new Random();
            foreach (var item in 10.Times()) {
                Assert.False (csn[r.Next(Code.MinValue, Code.MaxValue)]);
            }

        }
    }
}
