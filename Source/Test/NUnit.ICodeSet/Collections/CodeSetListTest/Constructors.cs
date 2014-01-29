// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Linq;
using DD.Enumerables;
using NUnit.Framework;

namespace DD.Collections.CodeSetListTest
{
    [TestFixture]
    public class Constructors
    {
        [Test]
        public void FromIEnumerable()
        {
            CodeSetList csl;

            // null
            Assert.Throws<ArgumentNullException> (delegate{new CodeSetList((Code[])null);});

            // requires minimum 3 members
            Assert.Throws<ArgumentException> (delegate{new CodeSetList(new Code[0]);});
            Assert.Throws<ArgumentException> (delegate{new CodeSetList(new Code[] {1});});
            Assert.Throws<ArgumentException> (delegate{new CodeSetList(new Code[] {1,7});});

            // requires no more than ICodeSetService.ListMaxCount (16) members
            Assert.Throws<ArgumentException> (delegate{new CodeSetList(0.To(16).Select(item => (Code)item));});
            Assert.Throws<ArgumentException> (delegate{new CodeSetList((Code.MaxValue-17).To(Code.MaxValue).Select(item => (Code)item));});

            csl = new CodeSetList (2.To(15).Select(item => (Code)item));
            Assert.True (csl.SequenceEqual(2.To(15).Select(item => (Code)item)));

            csl = new CodeSetList (0.To(15).Select(item => (Code)item));
            Assert.True (csl.SequenceEqual(0.To(15).Select(item => (Code)item)));

            Code[] input = new Code[] {1114111,2,2,22,50,100,200,500,1000,10000,100000,1000000,1,0,65536,128000,512000};
            csl = new CodeSetList (input);
            Assert.True (csl.SequenceEqual(input.Distinct().OrderBy(item => (item))));

            csl = new CodeSetList (new CodeSetBits(input));
            Assert.True (csl.SequenceEqual(input.Distinct().OrderBy(item => (item))));

        }
    }
}
