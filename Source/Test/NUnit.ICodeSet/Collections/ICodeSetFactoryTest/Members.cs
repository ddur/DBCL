// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DD.Collections.ICodeSetTest
{
    [TestFixture]
    public class Factory
    {
        [Test]
        public void CreateUnique()
        {
            ICodeSetFactory.OutputDictionary = new ICodeSetDictionary();
            ICodeSetFactory.InputDictionary = ICodeSetFactory.OutputDictionary;
            
            ICodeSet aCode;
            ICodeSet bCode;
            ICodeSet cCode;

            // prove identical a/b (same)
            aCode = ICodeSetFactory.From ('a');
            bCode = ICodeSetFactory.From ('a');
            Assert.True (aCode is Code);
            Assert.True (aCode == bCode);
            Assert.True (aCode.Equals(bCode));
            Assert.True (aCode.Is(bCode));
            Assert.True (ICodeSetFactory.OutputDictionary.ContainsKey(aCode));
            Assert.True (ICodeSetFactory.OutputDictionary.ContainsKey(bCode));
            Assert.True (ICodeSetFactory.OutputDictionary.Count == 1);
            Assert.True (ICodeSetFactory.OutputDictionary[aCode] == 0);

            // c is different
            cCode = ICodeSetFactory.From ('c');
            Assert.True (ICodeSetFactory.OutputDictionary.ContainsKey(cCode));
            Assert.True (ICodeSetFactory.OutputDictionary.Count == 2);
            Assert.True (ICodeSetFactory.OutputDictionary[cCode] == 1);
            
            // a/b is same set
			aCode = "az".From();
			bCode = "za".From();
            Assert.True (aCode is CodeSetPair);
            Assert.True (aCode == bCode);
            Assert.True (aCode.Equals(bCode));
            Assert.True (aCode.Is(bCode));
            Assert.True (ICodeSetFactory.OutputDictionary.ContainsKey(aCode));
            Assert.True (ICodeSetFactory.OutputDictionary.ContainsKey(bCode));
            Assert.True (ICodeSetFactory.OutputDictionary.Count == 3);
            Assert.True (ICodeSetFactory.OutputDictionary[aCode] == 2);

            // c is different
			cCode = "09".From();
            Assert.True (aCode is CodeSetPair);
            Assert.True (aCode != cCode);
            Assert.True (!aCode.Equals(cCode));
            Assert.True (!aCode.Is(cCode));
            Assert.True (ICodeSetFactory.OutputDictionary.ContainsKey(cCode));
            Assert.True (ICodeSetFactory.OutputDictionary.Count == 4);
            Assert.True (ICodeSetFactory.OutputDictionary[cCode] == 3);

            // c is same as a/b
            cCode = ICodeSetFactory.From ('z', 'a');
            Assert.True (aCode == cCode);
            Assert.True (aCode.Equals(cCode));
            Assert.True (aCode.Is(cCode));

            Assert.True (bCode == cCode);
            Assert.True (bCode.Equals(cCode));
            Assert.True (bCode.Is(cCode));

            // no new dictionary members
            Assert.True (ICodeSetFactory.OutputDictionary.ContainsKey(cCode));
            Assert.True (ICodeSetFactory.OutputDictionary.Count == 4);
            Assert.True (ICodeSetFactory.OutputDictionary[cCode] == 
                         ICodeSetFactory.OutputDictionary[aCode]);


            // a/b is same
			aCode = "Aabcdef".From();
			bCode = "fedAcbaAf".From();
            Assert.True (aCode == bCode);
            Assert.True (aCode.Equals(bCode));
            Assert.True (aCode.Is(bCode));
            Assert.True (ICodeSetFactory.OutputDictionary.ContainsKey(aCode));
            Assert.True (ICodeSetFactory.OutputDictionary.ContainsKey(bCode));
            Assert.True (ICodeSetFactory.OutputDictionary.Count == 5);
            Assert.True (ICodeSetFactory.OutputDictionary[aCode] ==
                         ICodeSetFactory.OutputDictionary[bCode]);

            // c is same as a/b
			cCode = ("Aabcdef" as IEnumerable<char>).From();
            Assert.True (aCode == cCode);
            Assert.True (aCode.Equals(cCode));
            Assert.True (aCode.Is(cCode));
            Assert.True (ICodeSetFactory.OutputDictionary.ContainsKey(aCode));
            Assert.True (ICodeSetFactory.OutputDictionary.ContainsKey(cCode));
            Assert.True (ICodeSetFactory.OutputDictionary.Count == 5);
            Assert.True (ICodeSetFactory.OutputDictionary[aCode] ==
                         ICodeSetFactory.OutputDictionary[cCode]);


            ICodeSetFactory.OutputDictionary = null;
            ICodeSetFactory.InputDictionary = null;
        }
    }
}
