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
                    ICodeSetService.ToCodes ((IEnumerable<int>)null);
                }
            );
            
        }

        [Test]
        public void WhenArgIEnumerableIsValidCodes () {
            ICodeSetService.ToCodes (new int[]{0,1,2,3});
            ICodeSetService.ToCodes (new int[]{0,1,2,3}, 1000);
        }

        [Test]
        public void WhenArgIEnumerableIsInvalidCodes () {
            Assert.Throws<ArgumentException> (
                delegate {
                    ICodeSetService.ToCodes (new int[]{int.MinValue, int.MaxValue});
                }
            );
            Assert.Throws<ArgumentException> (
                delegate {
                    ICodeSetService.ToCodes (new int[]{0,1,2,3}, int.MinValue);
                }
            );
            Assert.Throws<ArgumentException> (
                delegate {
                    ICodeSetService.ToCodes (new int[]{0,1,2,3}, int.MaxValue);
                }
            );

            //ICodeSetService.ToCodes (new int[]{int.MinValue, int.MaxValue});
            //ICodeSetService.ToCodes (new int[]{0,1,2,3}, int.MinValue);
            //ICodeSetService.ToCodes (new int[]{0,1,2,3}, int.MaxValue);
        }
    }
}
