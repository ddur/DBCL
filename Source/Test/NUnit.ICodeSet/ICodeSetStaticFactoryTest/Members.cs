// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Factory = DD.Collections.ICodeSet.Factory;

namespace DD.Collections.ICodeSet.ICodeSetStaticFactoryTest {
    //    [TestFixture]
    //    public class Members
    //    {
    //        [Test]
    //        public void CreateUnique()
    //        {
    //            Factory.OutputDictionary = new ICodeSetDictionary();
    //
    //            ICodeSet aCode;
    //            ICodeSet bCode;
    //            ICodeSet cCode;
    //
    //            // prove identical a/b (same)
    //            aCode = Factory.From ('a');
    //            bCode = Factory.From ('a');
    //            Assert.True (aCode is Code);
    //            Assert.True (aCode == bCode);
    //            Assert.True (aCode.Equals(bCode));
    //            Assert.True (aCode.Is(bCode));
    //            Assert.True (Factory.OutputDictionary.ContainsKey(aCode));
    //            Assert.True (Factory.OutputDictionary.ContainsKey(bCode));
    //            Assert.True (Factory.OutputDictionary.Count == 1);
    //            Assert.True (Factory.OutputDictionary[aCode] == 0);
    //
    //            // c is different
    //            cCode = Factory.From ('c');
    //            Assert.True (Factory.OutputDictionary.ContainsKey(cCode));
    //            Assert.True (Factory.OutputDictionary.Count == 2);
    //            Assert.True (Factory.OutputDictionary[cCode] == 1);
    //
    //            // a/b is same set
    //			aCode = "az".From();
    //			bCode = "za".From();
    //            Assert.True (aCode is CodeSetPair);
    //            Assert.True (aCode == bCode);
    //            Assert.True (aCode.Equals(bCode));
    //            Assert.True (aCode.Is(bCode));
    //            Assert.True (Factory.OutputDictionary.ContainsKey(aCode));
    //            Assert.True (Factory.OutputDictionary.ContainsKey(bCode));
    //            Assert.True (Factory.OutputDictionary.Count == 3);
    //            Assert.True (Factory.OutputDictionary[aCode] == 2);
    //
    //            // c is different
    //			cCode = "09".From();
    //            Assert.True (aCode is CodeSetPair);
    //            Assert.True (aCode != cCode);
    //            Assert.True (!aCode.Equals(cCode));
    //            Assert.True (!aCode.Is(cCode));
    //            Assert.True (Factory.OutputDictionary.ContainsKey(cCode));
    //            Assert.True (Factory.OutputDictionary.Count == 4);
    //            Assert.True (Factory.OutputDictionary[cCode] == 3);
    //
    //            // c is same as a/b
    //            cCode = Factory.From ('z', 'a');
    //            Assert.True (aCode == cCode);
    //            Assert.True (aCode.Equals(cCode));
    //            Assert.True (aCode.Is(cCode));
    //
    //            Assert.True (bCode == cCode);
    //            Assert.True (bCode.Equals(cCode));
    //            Assert.True (bCode.Is(cCode));
    //
    //            // no new dictionary members
    //            Assert.True (Factory.OutputDictionary.ContainsKey(cCode));
    //            Assert.True (Factory.OutputDictionary.Count == 4);
    //            Assert.True (Factory.OutputDictionary[cCode] ==
    //                         Factory.OutputDictionary[aCode]);
    //
    //
    //            // a/b is same
    //			aCode = "Aabcdef".From();
    //			bCode = "fedAcbaAf".From();
    //            Assert.True (aCode == bCode);
    //            Assert.True (aCode.Equals(bCode));
    //            Assert.True (aCode.Is(bCode));
    //            Assert.True (Factory.OutputDictionary.ContainsKey(aCode));
    //            Assert.True (Factory.OutputDictionary.ContainsKey(bCode));
    //            Assert.True (Factory.OutputDictionary.Count == 5);
    //            Assert.True (Factory.OutputDictionary[aCode] ==
    //                         Factory.OutputDictionary[bCode]);
    //
    //            // c is same as a/b
    //			cCode = ("Aabcdef" as IEnumerable<char>).From();
    //            Assert.True (aCode == cCode);
    //            Assert.True (aCode.Equals(cCode));
    //            Assert.True (aCode.Is(cCode));
    //            Assert.True (Factory.OutputDictionary.ContainsKey(aCode));
    //            Assert.True (Factory.OutputDictionary.ContainsKey(cCode));
    //            Assert.True (Factory.OutputDictionary.Count == 5);
    //            Assert.True (Factory.OutputDictionary[aCode] ==
    //                         Factory.OutputDictionary[cCode]);
    //
    //            Factory.OutputDictionary = null;
    //        }
    //    }
}
