// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetWideTest {

    [TestFixture]
    public class Constructors {

        [Test]
        public void FromBits () {
            CodeSetWide csw;

            csw = CodeSetWide.From (new Code[] { 0, 1, 1114111 });
            csw = CodeSetWide.From (BitSetArray.From (0, 1, 1114111));
        }

        [Test]
        public void FromBitsThrows () {
            CodeSetWide csw;

            // requires not null
            Assert.Throws<ArgumentNullException> (
                delegate {
                    csw = CodeSetWide.From ((BitSetArray)null);
                }
            );

            // requres more than ICodeSetService.PairCount members
            Assert.Throws<InvalidOperationException> (
                delegate {
                    csw = CodeSetWide.From (BitSetArray.From (0, 70000));
                }
            );

            // requires at least one NOT member
            Assert.Throws<InvalidOperationException> (
                delegate {
                    csw = CodeSetWide.From (BitSetArray.From (
                        65525, 65526, 65527, 65528,
                        65529, 65530, 65531, 65532,
                        65533, 65534, 65535, 65536,
                        65537, 65538, 65539, 65540));
                }
            );

            // requires to span over two or more unicode pages
            Assert.Throws<InvalidOperationException> (
                delegate {
                    csw = CodeSetWide.From (BitSetArray.From (
                        0, 1, 2, 3,
                        4, 5, 6, 7,
                        8, 9, 10, 11,
                        12, 13, 14, 15,
                        60000));
                }
            );

            // requires valid codes
            Assert.Throws<IndexOutOfRangeException> (
                delegate {
                    csw = CodeSetWide.From (BitSetArray.From (
                        0, 1, 2, 3,
                        4, 5, 6, 7,
                        8, 9, 10, 11,
                        12, 13, 14, 15,
                        66000, Code.MaxValue + 1));
                }
            );
        }

        [Test]
        public void FromCodes () {
            CodeSetWide csw;

            csw = CodeSetWide.From (new Code[] { 0, 1, 65536 });
            csw = CodeSetWide.From (CodeSetList.From (new Code[] { 0, 1, 140000 }));
        }

        [Test]
        public void FromCodesThrows () {
            CodeSetWide csw;

            // requires not null
            Assert.Throws<ArgumentNullException> (
                delegate {
                    csw = CodeSetWide.From ((IEnumerable<Code>)null);
                }
            );

            // requres more than ICodeSetService.ListMaxCount members
            Assert.Throws<InvalidOperationException> (
                delegate {
                    csw = CodeSetWide.From (new List<Code> () {
						0, 70000
					});
                }
            );

            // requires at least one NOT member
            Assert.Throws<InvalidOperationException> (
                delegate {
                    csw = CodeSetWide.From (new List<Code> () {
						65525, 65526, 65527, 65528,
						65529, 65530, 65531, 65532,
						65533, 65534, 65535, 65536,
						65537, 65538, 65539, 65540, 65541
					});
                }
            );

            // requires to span over two or more unicode pages
            Assert.Throws<InvalidOperationException> (
                delegate {
                    csw = CodeSetWide.From (new List<Code> () {
						0, 1, 2, 3,
						4, 5, 6, 7,
						8, 9, 10, 11,
						12, 13, 14, 15,
						60000
					});
                }
            );
        }
    }
}