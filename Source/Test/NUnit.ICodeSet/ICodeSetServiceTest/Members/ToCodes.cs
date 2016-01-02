// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetServiceTest.Members {

    [TestFixture]
    public class ToCodes {

        [Test]
        public void WhenArgIEnumerableIsNull () {
            var arg = ((IEnumerable<int>)null);
            Assert.Throws<ArgumentNullException> (
                delegate {
                    arg.ToCodes();
                }
            );
            
        }

        [Test]
        public void WhenArgIEnumerableIsValidCodes () {
            var arg = new int[] {0, 1, 2, 3};
            Assert.That (
                delegate {
                    arg.ToCodes();
                }, Throws.Nothing
            );
            Assert.That (
                delegate {
                    arg.ToCodes(1000);
                }, Throws.Nothing
            );
        }

        [Test]
        public void WhenArgIEnumerableIsInvalidCodes () {
            var arg = new int[] {int.MinValue, int.MaxValue, 2, 3};
            Assert.Throws<ArgumentException> (
                delegate {
                    arg.ToCodes();
                }
            );

            arg = new int[] {0, 1, 2, 3};
            Assert.Throws<ArgumentException> (
                delegate {
                    arg.ToCodes(int.MinValue);
                }
            );
            Assert.Throws<ArgumentException> (
                delegate {
                    arg.ToCodes(int.MaxValue);
                }
            );
        }
    }
}
