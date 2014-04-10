// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetStaticFactoryTest
{
    [TestFixture]
    public class Members
    {
        [Test]
        public void CreateUnique()
        {
            ICodeSetStaticFactory.OutputDictionary = new ICodeSetDictionary();
            
            ICodeSet aCode;
            ICodeSet bCode;
            ICodeSet cCode;

            // prove identical a/b (same)
            aCode = ICodeSetStaticFactory.From ('a');
            bCode = ICodeSetStaticFactory.From ('a');
            Assert.True (aCode is Code);
            Assert.True (aCode == bCode);
            Assert.True (aCode.Equals(bCode));
            Assert.True (aCode.Is(bCode));
            Assert.True (ICodeSetStaticFactory.OutputDictionary.ContainsKey(aCode));
            Assert.True (ICodeSetStaticFactory.OutputDictionary.ContainsKey(bCode));
            Assert.True (ICodeSetStaticFactory.OutputDictionary.Count == 1);
            Assert.True (ICodeSetStaticFactory.OutputDictionary[aCode] == 0);

            // c is different
            cCode = ICodeSetStaticFactory.From ('c');
            Assert.True (ICodeSetStaticFactory.OutputDictionary.ContainsKey(cCode));
            Assert.True (ICodeSetStaticFactory.OutputDictionary.Count == 2);
            Assert.True (ICodeSetStaticFactory.OutputDictionary[cCode] == 1);
            
            // a/b is same set
			aCode = "az".From();
			bCode = "za".From();
            Assert.True (aCode is CodeSetPair);
            Assert.True (aCode == bCode);
            Assert.True (aCode.Equals(bCode));
            Assert.True (aCode.Is(bCode));
            Assert.True (ICodeSetStaticFactory.OutputDictionary.ContainsKey(aCode));
            Assert.True (ICodeSetStaticFactory.OutputDictionary.ContainsKey(bCode));
            Assert.True (ICodeSetStaticFactory.OutputDictionary.Count == 3);
            Assert.True (ICodeSetStaticFactory.OutputDictionary[aCode] == 2);

            // c is different
			cCode = "09".From();
            Assert.True (aCode is CodeSetPair);
            Assert.True (aCode != cCode);
            Assert.True (!aCode.Equals(cCode));
            Assert.True (!aCode.Is(cCode));
            Assert.True (ICodeSetStaticFactory.OutputDictionary.ContainsKey(cCode));
            Assert.True (ICodeSetStaticFactory.OutputDictionary.Count == 4);
            Assert.True (ICodeSetStaticFactory.OutputDictionary[cCode] == 3);

            // c is same as a/b
            cCode = ICodeSetStaticFactory.From ('z', 'a');
            Assert.True (aCode == cCode);
            Assert.True (aCode.Equals(cCode));
            Assert.True (aCode.Is(cCode));

            Assert.True (bCode == cCode);
            Assert.True (bCode.Equals(cCode));
            Assert.True (bCode.Is(cCode));

            // no new dictionary members
            Assert.True (ICodeSetStaticFactory.OutputDictionary.ContainsKey(cCode));
            Assert.True (ICodeSetStaticFactory.OutputDictionary.Count == 4);
            Assert.True (ICodeSetStaticFactory.OutputDictionary[cCode] == 
                         ICodeSetStaticFactory.OutputDictionary[aCode]);


            // a/b is same
			aCode = "Aabcdef".From();
			bCode = "fedAcbaAf".From();
            Assert.True (aCode == bCode);
            Assert.True (aCode.Equals(bCode));
            Assert.True (aCode.Is(bCode));
            Assert.True (ICodeSetStaticFactory.OutputDictionary.ContainsKey(aCode));
            Assert.True (ICodeSetStaticFactory.OutputDictionary.ContainsKey(bCode));
            Assert.True (ICodeSetStaticFactory.OutputDictionary.Count == 5);
            Assert.True (ICodeSetStaticFactory.OutputDictionary[aCode] ==
                         ICodeSetStaticFactory.OutputDictionary[bCode]);

            // c is same as a/b
			cCode = ("Aabcdef" as IEnumerable<char>).From();
            Assert.True (aCode == cCode);
            Assert.True (aCode.Equals(cCode));
            Assert.True (aCode.Is(cCode));
            Assert.True (ICodeSetStaticFactory.OutputDictionary.ContainsKey(aCode));
            Assert.True (ICodeSetStaticFactory.OutputDictionary.ContainsKey(cCode));
            Assert.True (ICodeSetStaticFactory.OutputDictionary.Count == 5);
            Assert.True (ICodeSetStaticFactory.OutputDictionary[aCode] ==
                         ICodeSetStaticFactory.OutputDictionary[cCode]);

            ICodeSetStaticFactory.OutputDictionary = null;
        }
    }
}
