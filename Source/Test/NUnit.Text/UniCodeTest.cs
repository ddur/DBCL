// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------


using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

using NUnit.Framework;

using DD.Collections.ICodeSet;

namespace DD.Text.UniCode
{
    [TestFixture]
    public class UniCodeTest
    {
        IEnumerable <PropertyInfo> BlockProperties {
            get {
                var properties = typeof (Block).GetProperties();
                foreach (var property in properties) {
                    yield return property;
                }
            }
        }

        IEnumerable <PropertyInfo> CategoryProperties {
            get {
                var properties = typeof (Category).GetProperties();
                foreach (var property in properties) {
                    yield return property;
                }
            }
        }

        [Test, TestCaseSource ("BlockProperties")]
        public void BlockPropertiesGet (PropertyInfo property)
        {
            // construct cached value
            property.GetValue (null, null);
            // get cached value
            property.GetValue (null, null);
        }

        [Test, TestCaseSource ("CategoryProperties")]
        public void CategoryPropertiesGet (PropertyInfo property)
        {
            // construct cached value
            property.GetValue (null, null);
            // get cached value
            property.GetValue (null, null);
        }

    }
}
