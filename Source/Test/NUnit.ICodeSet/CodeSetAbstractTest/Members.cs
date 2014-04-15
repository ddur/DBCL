// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetAbstractTest
{
	[TestFixture]
	public class Members
	{
		[Test]
		public void CompareTo_EQ()
		{
			CodeSet acset = new CodeSetList(new Code[] {1,5,7});
			ICodeSet icset = new CodeSetPage(new Code[] {1,5,7});
			Assert.True (acset.CompareTo(icset) == 0);

			acset = CodeSetNull.Singleton;
			icset = CodeSetNull.Singleton;
			Assert.True (acset.CompareTo(icset) == 0);

			icset = null;
			Assert.True (acset.CompareTo(icset) == 0);
		}

		[Test]
		public void CompareTo_LT()
		{
			CodeSet acset = new CodeSetPage(new Code[] {1,5,7});
			ICodeSet icset = new Code(11);
			Assert.True (acset.CompareTo(icset) < 0);

			icset = new CodeSetPage(new Code[] {0,1,5,7});
			Assert.True (acset.CompareTo(icset) < 0);

			acset = CodeSetNull.Singleton;
			Assert.True (acset.CompareTo(icset) < 0);
		}

		[Test]
		public void CompareTo_GT()
		{
			CodeSet acset = new CodeSetPage(new Code[] {1,5,7});
			ICodeSet icset = new Code(5);
			Assert.True (acset.CompareTo(icset) > 0);

			icset = CodeSetNull.Singleton;
			Assert.True (acset.CompareTo(icset) > 0);

			icset = null;
			Assert.True (acset.CompareTo(icset) > 0);
		}
		
		[Test]
		public void Contains_Code() {
			CodeSet acset = new CodeSetList(new Code[] {1,5,7});

			Assert.True (acset.Contains(1));
			Assert.True (acset.Contains(5));
			Assert.True (acset.Contains(7));

			Assert.False (acset.Contains(0));
			Assert.False (acset.Contains(3));
			Assert.False (acset.Contains(11));
		}
	
		[Test]
		public void CopyTo_Codes() {
			CodeSet acset = new CodeSetList(new Code[] {0,1,5,7});
			var array = new Code[4];

			acset.CopyTo(array, 0);
			Assert.True (array.SequenceEqual(acset));
			
			Assert.Throws<ArgumentOutOfRangeException> (
				delegate {
					acset.CopyTo(array,-1);
				}
			);
			Assert.Throws<ArgumentOutOfRangeException> (
				delegate {
					acset.CopyTo(array,int.MinValue);
				}
			);
			Assert.Throws<ArgumentOutOfRangeException> (
				delegate {
					acset.CopyTo(array,1);
				}
			);
			Assert.Throws<ArgumentOutOfRangeException> (
				delegate {
					acset.CopyTo(array,int.MaxValue);
				}
			);

			array = null;
			Assert.Throws<ArgumentNullException> (
				delegate {
					acset.CopyTo(array,0);
				}
			);
		}
		
		[Test]
		public void Equals_IEqualityComparerOfICodeSet() {

			CodeSet cset_a = new CodeSetList(new Code[] {0,1,5,7});
			CodeSet cset_b = new CodeSetPage(new Code[] {0,1,5,7});
			CodeSet cset_c = new CodeSetList(new Code[] {1,5,7});
			
			Assert.True (cset_a.Equals(cset_a, cset_b));
			Assert.True (cset_b.Equals(cset_a, cset_b));
			Assert.True (cset_c.Equals(cset_a, cset_b));

			Assert.False (cset_a.Equals(cset_a, cset_c));
			Assert.False (cset_b.Equals(cset_c, cset_a));
			Assert.False (cset_c.Equals(cset_c, cset_a));

			Assert.False (cset_a.Equals(cset_b, cset_c));
			Assert.False (cset_b.Equals(cset_b, cset_c));
			Assert.False (cset_c.Equals(cset_c, cset_b));

			cset_c = null;
			Assert.False (cset_a.Equals(cset_b, cset_c));
			Assert.False (cset_b.Equals(cset_c, cset_b));

			cset_b = null;
			Assert.True (cset_a.Equals(cset_b, cset_c));
			Assert.True (cset_a.Equals(cset_c, cset_b));
		}

		[Test]
		public void Equals_ObjectOverride() {

			CodeSet cset_a = new CodeSetList(new Code[] {0,1,5,7});
			CodeSet cset_b = new CodeSetPage(new Code[] {0,1,5,7});
			CodeSet cset_c = new CodeSetList(new Code[] {1,5,7});
			
			Assert.True (cset_a.Equals((object)cset_a));
			Assert.True (cset_b.Equals((object)cset_b));
			Assert.True (cset_c.Equals((object)cset_c));

			Assert.True (cset_a.Equals((object)cset_b));
			Assert.True (cset_b.Equals((object)cset_a));

			Assert.False (cset_a.Equals((object)cset_c));
			Assert.False (cset_b.Equals((object)cset_c));

			Assert.False (cset_a.Equals((object)null));
			Assert.False (cset_b.Equals((object)null));
			Assert.False (cset_c.Equals((object)null));

			Assert.False (cset_a.Equals((object)BitSetArray.From(0,1,5,7)));
			Assert.False (cset_b.Equals((object)new Code[]{0,1,5,7}));
			Assert.False (cset_c.Equals(new object()));
		}

		[Test]
		public void GetHashCode_ObjectOverride() {
			CodeSet cset_a = new CodeSetList(new Code[] {0,1,5,7});
			CodeSet cset_b = new CodeSetPage(new Code[] {0,1,5,7});
			
			Assert.True (cset_a.GetHashCode() == cset_b.GetHashCode());
		}

		[Test]
		public void GetHashCode_IEqualityComparerOfICodeSet() {
			CodeSet cset_a = new CodeSetList(new Code[] {0,1,5,7});
			CodeSet cset_b = new CodeSetPage(new Code[] {0,1,5,7});
			
			Assert.True (cset_a.GetHashCode(cset_a) == cset_a.GetHashCode(cset_b));
			Assert.True (cset_b.GetHashCode(cset_a) == cset_b.GetHashCode(cset_b));
		}

		[Test]
		public void IsReadOnly_Get() {
			CodeSet cset_a = new CodeSetList(new Code[] {0,1,5,7});
			CodeSet cset_b = new CodeSetPage(new Code[] {0,1,5,7});
			CodeSet cset_c = CodeSetNull.Singleton;

			Assert.True (cset_a.IsReadOnly);
			Assert.True (cset_b.IsReadOnly);
			Assert.True (cset_c.IsReadOnly);
		}

		[Test]
		public void Item_Get() {
			CodeSet cset_a = new CodeSetList(new Code[] {0,1,5,7});

			Assert.True (cset_a[0]);
			Assert.True (cset_a[1]);
			Assert.True (cset_a[5]);
			Assert.True (cset_a[7]);

			Assert.False (cset_a[int.MinValue]);
			Assert.False (cset_a[-1]);
			Assert.False (cset_a[3]);
			Assert.False (cset_a[11]);
			Assert.False (cset_a[int.MaxValue]);
		}

		[Test]
		public void op_Equality() {
			CodeSet cset_a = new CodeSetList(new Code[] {0,1,5,7});
			CodeSet cset_b = new CodeSetPage(new Code[] {0,1,5,7});

			Assert.True (cset_a == cset_b);
			Assert.True (cset_b == cset_a);
			
			cset_a = CodeSetNull.Singleton;
			cset_b = CodeSetNull.Singleton;

			Assert.True (cset_a == cset_b);
			Assert.True (cset_b == cset_a);
			
			cset_a = null;

			Assert.True (cset_a == cset_b);
			Assert.True (cset_b == cset_a);
			
			cset_b = null;

			Assert.True (cset_a == cset_b);
			Assert.True (cset_b == cset_a);
		}

		[Test]
		public void op_Inequality() {
			CodeSet cset_a = new CodeSetList(new Code[] {0,1,5,7});
			CodeSet cset_b = new CodeSetPage(new Code[] {0,1,3,7});

			Assert.True (cset_a != cset_b);
			Assert.True (cset_b != cset_a);
			
			cset_a = CodeSetNull.Singleton;

			Assert.True (cset_a != cset_b);
			Assert.True (cset_b != cset_a);
			
			cset_a = null;

			Assert.True (cset_a != cset_b);
			Assert.True (cset_b != cset_a);
		}

		[Test]
		public void _ICollectionAdd_Throws() {
			ICollection<Code> cset_a = new CodeSetList(new Code[] {0,1,5,7});
			Assert.Throws<NotSupportedException> (
				delegate {
					cset_a.Add(new Code(11));
				}
			);
		}

		[Test]
		public void _ICollectionClear_Throws() {
			ICollection<Code> cset_a = new CodeSetList(new Code[] {0,1,5,7});
			Assert.Throws<NotSupportedException> (
				delegate {
					cset_a.Clear();
				}
			);
		}

		[Test]
		public void _ICollectionRemove_Throws() {
			ICollection<Code> cset_a = new CodeSetList(new Code[] {0,1,5,7});
			Assert.Throws<NotSupportedException> (
				delegate {
					cset_a.Remove(new Code(5));
				}
			);
		}

		[Test]
		public void _IEnumerableGetEnumerator() {
			IEnumerable cset_a = new CodeSetList(new Code[] {0,1,5,7});
			var enumerator = cset_a.GetEnumerator();
			Assert.True (enumerator.MoveNext());
			Assert.True (((Code)enumerator.Current).Value == 0);
			Assert.True (enumerator.MoveNext());
			Assert.True (((Code)enumerator.Current).Value == 1);
			Assert.True (enumerator.MoveNext());
			Assert.True (((Code)enumerator.Current).Value == 5);
			Assert.True (enumerator.MoveNext());
			Assert.True (((Code)enumerator.Current).Value == 7);
			Assert.False (enumerator.MoveNext());
		}
	}
}
