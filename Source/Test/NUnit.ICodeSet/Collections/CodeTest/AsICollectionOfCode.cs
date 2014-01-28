// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DD.Collections.CodeTest
{
    [TestFixture]
    public class AsICollectionOfCode
    {
        
        [Test]
        public void NotSupported() {
            
            Random r = new Random();
            Code C = r.Next(Code.MinValue, Code.MaxValue);
            Code D = r.Next(Code.MinValue, Code.MaxValue);
            ICollection<Code> iC = C;
                        
            Assert.Throws<NotSupportedException> (delegate { iC.Add (C); });
            Assert.Throws<NotSupportedException> (delegate { iC.Add (D); });
            Assert.Throws<NotSupportedException> (delegate { iC.Remove (C); });
            Assert.Throws<NotSupportedException> (delegate { iC.Remove (D); });
            Assert.Throws<NotSupportedException> (delegate { iC.Clear(); });

        }

        [Test]
        public void ReadOnlyIsTrue() {

            Random r = new Random();
            Code C = r.Next(Code.MinValue, Code.MaxValue);
            ICollection<Code> iC = C;
            
            // allways read only
            Assert.True (iC.IsReadOnly);
            
            // always 1 member
            Assert.True (iC.Count == 1);
        }
            
        [Test]
        public void CountIsOne() {

            Random r = new Random();
            Code C = r.Next(Code.MinValue, Code.MaxValue);
            ICollection<Code> iC = C;
            
            // always 1 member
            Assert.True (iC.Count == 1);
        }
            
        [Test]
        public void ContainsOneCode() {

            Random r = new Random();
            Code C = r.Next(Code.MinValue, Code.MaxValue);
            Code D = r.Next(Code.MinValue, Code.MaxValue);
            ICollection<Code> iC = C;
            
            // Contains
            Assert.True (iC.Contains(D) == (C == D));
        }
            
        [Test]
        public void CopyToArray() {

            Random r = new Random();
            Code C = r.Next(Code.MinValue, Code.MaxValue);

            ICollection<Code> iC = C;
            
            // CopyTo
            Code[] arrayC = new Code[2];
            iC.CopyTo(arrayC, 0);
            Assert.True (arrayC[0] == C);
            iC.CopyTo(arrayC, 1);
            Assert.True (arrayC[1] == C);
            
            Assert.Throws<ArgumentOutOfRangeException> (delegate {iC.CopyTo(arrayC, -1);});
            Assert.Throws<ArgumentOutOfRangeException> (delegate {iC.CopyTo(arrayC, 2);});

            arrayC = new Code[0];
            Assert.Throws<ArgumentOutOfRangeException> (delegate {iC.CopyTo(arrayC, -1);});
            Assert.Throws<ArgumentOutOfRangeException> (delegate {iC.CopyTo(arrayC, 0);});
            Assert.Throws<ArgumentOutOfRangeException> (delegate {iC.CopyTo(arrayC, 1);});
            Assert.Throws<ArgumentOutOfRangeException> (delegate {iC.CopyTo(arrayC, 2);});

            arrayC = null;
            Assert.Throws<ArgumentNullException> (delegate {iC.CopyTo(arrayC, 0);});

        }
            
    }
}
