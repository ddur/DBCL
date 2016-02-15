// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Linq;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.QuickWrapTest
{
    [TestFixture]
    public class ConstructionAndMembers
    {
        [Test]
        public void Null()
        {
            Assert.Throws ( typeof(ArgumentNullException),
                    delegate {
                               QuickWrap.Unsafe ((BitSetArray)null);
                    });
        }

        [Test]
        public void Empty()
        {
            Assert.Throws ( typeof(ArgumentException),
                    delegate {
                               QuickWrap.Unsafe (BitSetArray.Empty());
                    });
        }


        [Test]
        public void Invalid()
        {
            Assert.Throws ( typeof(ArgumentException),
                    delegate {
                               QuickWrap.Unsafe (BitSetArray.From (1114112));
                    });
        }

        [Test]
        public void Valid()
        {
            var quick = QuickWrap.Unsafe (BitSetArray.From (1, 5, 7));

            Assert.True (quick[1] && quick[5] && quick[7]);
            Assert.False (quick[2] | quick[4] | quick[6]);
            Assert.True (quick[(Code)1] && quick[(Code)5] && quick[(Code)7]);
            Assert.False (quick[(Code)2] | quick[(Code)4] | quick[(Code)6]);
            Assert.True (quick.Count == 3);
            Assert.True (quick.First == 1);
            Assert.True (quick.Last == 7);
            Assert.True (quick.Length == 7);
            Assert.False (quick.IsReduced);
            Assert.False (quick.IsEmpty);
            Assert.True (quick.SequenceEqual (new Code[] { 1, 5, 7 }));
            Assert.True (quick.ToBitSetArray().SequenceEqual (BitSetArray.From (1, 5, 7)));
            Assert.True (quick.ToBitSetArray().SetEquals (BitSetArray.From (1, 5, 7)));
            Assert.True (quick.ToBitSetArray().Equals (BitSetArray.From (1, 5, 7)));
        }
    }
}
