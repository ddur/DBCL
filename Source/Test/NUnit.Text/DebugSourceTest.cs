// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------


using System;
using NUnit.Framework;

namespace DD.Text
{
    [TestFixture]
    public class DebugSourceTest
    {
        [Test]
        public void ConstructAndUse()
        {
            // arrange
            var pdbSource = new DebugSource ("    var items = new []\r\n\r\n    {\"a\", \"b\"}" );

            // assert with invalid point (line < 1 or line > lines or start-line > end-line)
            Assert.True (pdbSource.GetPoint (0,1,0,2) == ""); // invalid line index
            Assert.True (pdbSource.GetPoint (0,10,1,2) == ""); // invalid line index
            Assert.True (pdbSource.GetPoint (10,1,11,2) == ""); // invalid line index
            Assert.True (pdbSource.GetPoint (1,10,11,12) == ""); // invalid line index
            Assert.True (pdbSource.GetPoint (3,1,1,1) == ""); // invalid line index

            Assert.True (pdbSource.GetPoint (1,30,1,31) == ""); // one line - start column > line-length
            Assert.True (pdbSource.GetPoint (1,5,1,1) == "");   // one line - start column > end column

            Assert.True (pdbSource.GetPoint (1,30,2,-1) == "");     // two lines - start column > line-length & end column < 1
            Assert.True (pdbSource.GetPoint (1,30,2,30) == "\r\n"); // two lines - start column > line-length & end column > line-length

            // assert with valid point
            Assert.False (pdbSource.GetPoint (1,5,1,13) == "var items");
            Assert.True (pdbSource.GetPoint (1,5,1,14) == "var items");
            Assert.True (pdbSource.GetPoint (1,5,1,15) == "var items ");
            Assert.True (pdbSource.GetPoint (1,5,1,16) == "var items =");

            Assert.False (pdbSource.GetPoint (2,1,2,2) == "\r\n");
            Assert.True (pdbSource.GetPoint (2,1,2,3) == "\r\n");
            Assert.True (pdbSource.GetPoint (2,1,2,4) == "\r\n");
            Assert.True (pdbSource.GetPoint (2,0,2,4) == "\r\n");
            Assert.True (pdbSource.GetPoint (2,-1,2,4) == "\r\n");

            Assert.False (pdbSource.GetPoint (3,5,3,14) == "{\"a\", \"b\"}");
            Assert.True (pdbSource.GetPoint (3,5,3,15) == "{\"a\", \"b\"}");
            Assert.True (pdbSource.GetPoint (3,5,3,16) == "{\"a\", \"b\"}");

            Assert.True (pdbSource.GetPoint (1,5,3,15) == "var items = new []\r\n\r\n    {\"a\", \"b\"}");

            Assert.True (pdbSource.GetPoint (1,1,3,16) == "    var items = new []\r\n\r\n    {\"a\", \"b\"}");
            Assert.True (pdbSource.GetPoint (1,-1,3,16) == "    var items = new []\r\n\r\n    {\"a\", \"b\"}");
        }
    }
}
