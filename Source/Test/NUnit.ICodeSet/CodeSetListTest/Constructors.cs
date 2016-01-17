// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Linq;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetListTest {

    [TestFixture]
    public class Constructors {

        [Test]
        public void FromIEnumerable () {
            CodeSetList csl;

            var input = new Code[] {
				1114111,
				2,
				2,
				22,
				50,
				100,
				200,
				500,
				1000,
				10000,
				100000,
				1000000,
				1,
				0,
				65536,
				128000,
				512000
			};
            csl = CodeSetList.From (input);
            Assert.True (csl.SequenceEqual (input.Distinct ().OrderBy (item => (item))));

            csl = CodeSetList.From (1114111, 2, 2, 22, 50, 100, 200, 500, 1000, 10000, 100000, 1000000, 1, 0, 65536, 128000, 512000);
            Assert.True (csl.SequenceEqual (input.Distinct ().OrderBy (item => (item))));

            var csw = CodeSetMask.From (input);
            csl = CodeSetList.From (csw);
            Assert.True (csl.SequenceEqual (input.Distinct ().OrderBy (item => (item))));
        }

        [Test]
        public void FromIEnumerableThrows () {
            CodeSetList csl;

            // null
            Assert.Throws<ArgumentNullException> (delegate {
                csl = CodeSetList.From ((Code[])null);
            });

            // requires minimum 3 members
            Assert.Throws<ArgumentEmptyException> (delegate {
                csl = CodeSetList.From (new Code[0]);
            });
            Assert.Throws<InvalidOperationException> (delegate {
                csl = CodeSetList.From (new Code[] { 1 });
            });
            Assert.Throws<InvalidOperationException> (delegate {
                csl = CodeSetList.From (new Code[] { 1, 7 });
            });
            Assert.Throws<InvalidOperationException> (delegate {
                csl = CodeSetList.From (new Code[] { 1, 7, 7 });
            });

            Assert.Throws<InvalidOperationException> (delegate {
                csl = CodeSetList.From (1);
            });
            Assert.Throws<InvalidOperationException> (delegate {
                csl = CodeSetList.From (1, 7);
            });
            Assert.Throws<InvalidOperationException> (delegate {
                csl = CodeSetList.From (1, 7, 7);
            });

            // requires no more than ICodeSetService.ListMaxCount (16) members
            Assert.Throws<InvalidOperationException> (delegate {
                csl = CodeSetList.From (new Code[] { 0, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34 });
            });
            Assert.Throws<InvalidOperationException> (delegate {
                csl = CodeSetList.From (1113000, 1113010, 1113020, 1114000, 1114002, 1114004, 1114005, 1114006, 1114007, 1114008, 1114090, 1114093, 1114094, 1114096, 1114098, 1114100, 1114110, 1114111);
            });
            Assert.Throws<InvalidOperationException> (delegate {
                csl = CodeSetList.From (0, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34);
            });

            // does not except full-range of codes
            Assert.Throws<InvalidOperationException> (delegate {
                csl = CodeSetList.From (Enumerable.Range (1, 8).Select (item => (Code)item));
            });
            Assert.Throws<InvalidOperationException> (delegate {
                csl = CodeSetList.From (10, 11, 12, 13, 14, 15);
            });

            // fails to cast int argument to Code
            Assert.Throws<InvalidCastException> (delegate {
                csl = CodeSetList.From (10, 11, 12, 13, 14, 1114112);
            });
        }
    }
}
