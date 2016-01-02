/*
 * Created by SharpDevelop.
 * User: ddur
 * Date: 1.1.2016.
 * Time: 15:53
 * 
 */
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DD.Collections.ICodeSet.CodeSetMaskTest
{
	[TestFixture]
	public class Construction
	{

		#region From ParamsCode
		[Test]
		public void FromParamsCode_Null()
		{
			
            Assert.That (
                delegate {
					CodeSetMask.From ((Code[])null);
				}, Throws.TypeOf<ArgumentNullException>()
            );
		}

		[Test]
		public void FromParamsCode_Empty()
		{
            Assert.That (
                delegate {
					var x = CodeSetMask.From (new Code[0]);
				}, Throws.TypeOf<ArgumentEmptyException>()
            );
		}

		[Test]
		public void FromParamsCode_ValidArray()
		{
            Assert.That (
                delegate {
					CodeSetMask.From (new Code[] {0,1,2,3});
				}, Throws.Nothing
            );
		}

		[Test]
		public void FromParamsCode_ValidParams()
		{
            Assert.That (
                delegate {
					CodeSetMask.From ( 0, 1, 2, 3, 50);
				}, Throws.Nothing
            );
		}

		#endregion

		#region From IEumerable<Code>

		[Test]
		public void FromIEnumerableCode_Null()
		{
			
            Assert.That (
                delegate {
					CodeSetMask.From ((IEnumerable<Code>)null);
				}, Throws.TypeOf<ArgumentNullException>()
            );
		}

		[Test]
		public void FromIEnumerableCode_Empty()
		{
            Assert.That (
                delegate {
					var x = CodeSetMask.From ((IEnumerable<Code>)new Code[0]);
				}, Throws.TypeOf<ArgumentEmptyException>()
            );
		}

		[Test]
		public void FromIEnumerableCode_Valid()
		{
            Assert.That (
                delegate {
					CodeSetMask.From ((IEnumerable<Code>)new Code[] {0,1,3,3});
				}, Throws.Nothing
            );
		}

		[Test]
		public void FromIEnumerableCode_CastICodeSet()
		{
            Assert.That (
                delegate {
					CodeSetMask.From ((IEnumerable<Code>)CodeSetList.From(0,3,254,255,256));
				}, Throws.Nothing
            );
		}

		#endregion

		#region From int[]

		[Test]
		public void FromArrayInt_Null()
		{
			int[] arg = null;
            Assert.That (
                delegate {
					CodeSetMask.From (arg);
				}, Throws.TypeOf<ArgumentNullException>()
            );
		}

		[Test]
		public void FromArrayInt_Empty()
		{
			var arg = new int[0];
            Assert.That (
                delegate {
					var x = CodeSetMask.From (arg);
				}, Throws.TypeOf<ArgumentEmptyException>()
            );
		}

		[Test]
		public void FromArrayInt_InvalidNoBits()
		{
			var arg = new int[]{0};
            Assert.That (
                delegate {
					CodeSetMask.From (arg);
				}, Throws.TypeOf<ArgumentException>()
            );
		}

		[Test]
		public void FromArrayInt_InvalidFirstBit()
		{
			var arg = new int[]{2};
            Assert.That (
                delegate {
					CodeSetMask.From (arg);
				}, Throws.TypeOf<ArgumentException>()
            );
		}

		[Test]
		public void FromArrayInt_InvalidLastInt()
		{
			var arg = new int[]{1,1,0};
            Assert.That (
                delegate {
					CodeSetMask.From (arg);
				}, Throws.TypeOf<ArgumentException>()
            );
		}

		[Test]
		public void FromArrayInt_InvalidLastBit()
		{
			var arg = new int[(Code.MaxValue>>5)+2];
			arg[0] = 1;
			arg[arg.Length-1] = 1; // 1114112
            Assert.That (
                delegate {
					CodeSetMask.From (arg);
				}, Throws.TypeOf<ArgumentException>()
            );
		}

		[Test]
		public void FromArrayInt_InvalidOffset()
		{
			var arg = new int[]{1};
            Assert.That (
                delegate {
					CodeSetMask.From (arg, Code.MinValue-1);
				}, Throws.TypeOf<ArgumentException>()
            );
            Assert.That (
                delegate {
					CodeSetMask.From (arg, Code.MaxValue+1);
				}, Throws.TypeOf<ArgumentException>()
            );
            Assert.That (
                delegate {
					CodeSetMask.From (arg, int.MinValue);
				}, Throws.TypeOf<ArgumentException>()
            );
            Assert.That (
                delegate {
					CodeSetMask.From (arg, int.MaxValue);
				}, Throws.TypeOf<ArgumentException>()
            );
		}


		[Test]
		public void FromArrayInt_InvalidBitsPlusOffset()
		{
			var arg = new int[(Code.MaxValue>>5)+1];
			arg[0] = 1;
			arg[arg.Length-1] = -1;
            Assert.That (
                delegate {
					CodeSetMask.From (arg, 1); // 1114112
				}, Throws.TypeOf<ArgumentException>()
            );

			arg = new int[]{3};
            Assert.That (
                delegate {
					CodeSetMask.From (arg, Code.MaxValue);
				}, Throws.TypeOf<ArgumentException>()
            );

		}

		[Test]
		public void FromArrayInt_Valid()
		{
			var arg = new int[(Code.MaxValue>>5)+1];
			arg[0] = 1;
			arg[arg.Length-1] = -1;
            Assert.That (
                delegate {
					CodeSetMask.From (arg);
				}, Throws.Nothing
            );

			arg = new int[]{1};
            Assert.That (
                delegate {
					CodeSetMask.From (arg);
				}, Throws.Nothing
            );

		}

		#endregion

		#region From CodeSetMask

		[Test]
		public void FromCodeSetMask_Null()
		{
			CodeSetMask arg = null;
            Assert.That (
                delegate {
					CodeSetMask.From (arg);
				}, Throws.TypeOf<ArgumentNullException>()
            );
		}

		[Test]
		public void FromCodeSetMask_InvalidOffset()
		{
			var arg = CodeSetMask.From (new int[]{1});
            Assert.That (
                delegate {
					CodeSetMask.From (arg, int.MinValue);
				}, Throws.TypeOf<ArgumentException>()
            );
            Assert.That (
                delegate {
					CodeSetMask.From (arg, Code.MinValue - 1);
				}, Throws.TypeOf<ArgumentException>()
            );
            Assert.That (
                delegate {
					CodeSetMask.From (arg, Code.MaxValue + 1);
				}, Throws.TypeOf<ArgumentException>()
            );
            Assert.That (
                delegate {
					CodeSetMask.From (arg, int.MaxValue);
				}, Throws.TypeOf<ArgumentException>()
            );

			arg = CodeSetMask.From (CodeSetPair.From (1, 1114111));
			Assert.That (
                delegate {
					CodeSetMask.From (arg, -1);
				}, Throws.TypeOf<ArgumentException>()
            );
			Assert.That (
                delegate {
					CodeSetMask.From (arg, 1);
				}, Throws.TypeOf<ArgumentException>()
            );
		}


		[Test]
		public void FromCodeSetMask_Valid()
		{
			CodeSetMask arg = null;
			arg = CodeSetMask.From (CodeSetPair.From (1, 1114111));
			Assert.That (
                delegate {
					CodeSetMask.From (arg);
				}, Throws.Nothing
            );
		}

		#endregion

	}
}
