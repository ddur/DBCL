// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.ICodeSetExtendsTest.Members {

    [TestFixture]
    public class ToCodes {

        [Test]
        public void WhenArgIEnumerableIsNull () {
            var arg = ((IEnumerable<int>)null);
            Assert.Throws<ArgumentNullException> (
                delegate {
                    arg.ToCodes ().All (x => true);
                }
            );
            Assert.Throws<ArgumentNullException> (
                delegate {
                    arg.ToCodes (0).All (x => true);
                }
            );
        }

        [Test]
        public void WhenArgIEnumerableIsEmpty () {
            var arg = new int[0];
            Assert.That (
                delegate {
                    arg.ToCodes ().All (x => true);
                }, Throws.Nothing
            );
            Assert.That (
                delegate {
                    arg.ToCodes (1000).All (x => true);
                }, Throws.Nothing
            );
        }

        [Test]
        public void WhenArgIEnumerableIsValidCodes () {
            var arg = new int[] { 0, 1, 2, 3 };
            Assert.That (
                delegate {
                    arg.ToCodes ().All (x => true);
                }, Throws.Nothing
            );
            Assert.That (
                delegate {
                    arg.ToCodes (1000).All (x => true);
                }, Throws.Nothing
            );
        }

        [Test]
        public void WhenArgIEnumerableIsInvalidCodes () {
            var arg = new int[] { int.MinValue, int.MaxValue, 2, 3 };
            Assert.That (
                delegate {
                    arg.ToCodes ().All (x => true);
                }, Throws.TypeOf<InvalidCastException>()
            );
            Assert.That (
                delegate {
                    arg.ToCodes (10).All (x => true);
                }, Throws.TypeOf<InvalidCastException>()
            );


            arg = new int[] { 0, 1, 2, 3 };
            Assert.Throws<InvalidCastException> (
                delegate {
                    arg.ToCodes (int.MinValue).All (x => true);
                }
            );
            Assert.Throws<InvalidCastException> (
                delegate {
                    arg.ToCodes (int.MaxValue).All (x => true);
                }
            );
        }
    }
}
