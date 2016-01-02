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
            Assert.Throws<ArgumentNullException> (
                delegate {
                    ((IEnumerable<int>)null).ToCodes();
                }
            );
            
        }

        [Test]
        public void WhenArgIEnumerableIsValidCodes () {
            new int[] {
                0,
                1,
                2,
                3
            }.ToCodes();
            new int[] {
                0,
                1,
                2,
                3
            }.ToCodes(1000);
        }

        [Test]
        public void WhenArgIEnumerableIsInvalidCodes () {
            Assert.That (
                delegate {
                    new int[] {
                        int.MinValue,
                        int.MaxValue
                    }.ToCodes();
                }, Throws.TypeOf<ArgumentException>
            );
            Assert.That (
                delegate {
                    new int[] {
                        0,
                        1,
                        2,
                        3
                    }.ToCodes(int.MinValue);
                }, Throws.TypeOf<ArgumentException>
            );
            Assert.That (
                delegate {
                    new int[] {
                        0,
                        1,
                        2,
                        3
                    }.ToCodes(int.MaxValue);
                }, Throws.TypeOf<ArgumentException>
            );
        }
    }
}
