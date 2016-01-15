/*
 * Created by SharpDevelop.
 * User: ddur
 * Date: 15.1.2016.
 * Time: 12:43
 * 
 */
using System;
using System.Linq;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetUniqueFactoryTest
{
    [TestFixture]
    public class QuickWrapSubClass
    {
        [Test]
        public void CoverAll()
        {
            var quick = new Distinct.QuickWrap (BitSetArray.From (1, 5, 7));

            Assert.True (quick[1] && quick[5] && quick[7]);
            Assert.False (quick[2] | quick[4] | quick[6]);
            Assert.True (quick.Count == 3);
            Assert.True (quick.First == 1);
            Assert.True (quick.Last == 7);
            Assert.True (quick.Length == 7);
            Assert.False (quick.IsReduced);
            Assert.True (quick.SequenceEqual (new Code[] { 1, 5, 7 }));
            Assert.True (quick.ToBitSetArray().SequenceEqual (BitSetArray.From (1, 5, 7)));
            Assert.True (quick.ToBitSetArray().SetEquals (BitSetArray.From (1, 5, 7)));
            Assert.True (quick.ToBitSetArray().Equals (BitSetArray.From (1, 5, 7)));
        }
    }
}
