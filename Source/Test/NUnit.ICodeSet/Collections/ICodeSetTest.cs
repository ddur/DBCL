// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace DD.Collections
{
    [TestFixture]
    public class ICodeSetTest
    {
        [Test]
        public void CreateUnique()
        {
            ICodeSetFactory.OutputDictionary = new ICodeSetDictionary();
            ICodeSetFactory.InputDictionary = ICodeSetFactory.OutputDictionary;
            
            ICodeSet aCode;
            ICodeSet bCode;
            ICodeSet cCode;

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

            cCode = ICodeSetFactory.From ('c');
            Assert.True (ICodeSetFactory.OutputDictionary.ContainsKey(cCode));
            Assert.True (ICodeSetFactory.OutputDictionary.Count == 2);
            Assert.True (ICodeSetFactory.OutputDictionary[cCode] == 1);
            
            aCode = ICodeSetFactory.From ("az");
            bCode = ICodeSetFactory.From ("za");
            Assert.True (aCode is CodeSetPair);
            Assert.True (aCode == bCode);
            Assert.True (aCode.Equals(bCode));
            Assert.True (aCode.Is(bCode));
            Assert.True (ICodeSetFactory.OutputDictionary.ContainsKey(aCode));
            Assert.True (ICodeSetFactory.OutputDictionary.ContainsKey(bCode));
            Assert.True (ICodeSetFactory.OutputDictionary.Count == 3);
            Assert.True (ICodeSetFactory.OutputDictionary[aCode] == 2);

            cCode = ICodeSetFactory.From ("09");
            Assert.True (aCode is CodeSetPair);
            Assert.True (aCode != cCode);
            Assert.True (!aCode.Equals(cCode));
            Assert.True (!aCode.Is(cCode));
            Assert.True (ICodeSetFactory.OutputDictionary.ContainsKey(cCode));
            Assert.True (ICodeSetFactory.OutputDictionary.Count == 4);
            Assert.True (ICodeSetFactory.OutputDictionary[cCode] == 3);

            cCode = ICodeSetFactory.From ('z', 'a');
            Assert.True (aCode is CodeSetPair);
            Assert.True (aCode == cCode);
            Assert.True (aCode.Equals(cCode));
            Assert.True (aCode.Is(cCode));
            Assert.True (ICodeSetFactory.OutputDictionary.ContainsKey(cCode));
            Assert.True (ICodeSetFactory.OutputDictionary.Count == 4);
            Assert.True (ICodeSetFactory.OutputDictionary[cCode] == 
                         ICodeSetFactory.OutputDictionary[aCode]);


            aCode = ICodeSetFactory.From ("Aabcdef");
            bCode = ICodeSetFactory.From ("fedAcbaAf");
            System.Windows.Forms.MessageBox.Show (aCode.GetType().Name);
            Assert.True (aCode is CodeSetPage);
            Assert.True (aCode == bCode);
            Assert.True (aCode.Equals(bCode));
            Assert.True (aCode.Is(bCode));
            Assert.True (ICodeSetFactory.OutputDictionary.ContainsKey(aCode));
            Assert.True (ICodeSetFactory.OutputDictionary.ContainsKey(bCode));
            Assert.True (ICodeSetFactory.OutputDictionary.Count == 5);
            Assert.True (ICodeSetFactory.OutputDictionary[aCode] == 4);

        }
    }
}
