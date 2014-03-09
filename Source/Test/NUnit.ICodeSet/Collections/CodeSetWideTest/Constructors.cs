/*
 * Created by SharpDevelop.
 * User: ddur
 * Date: 06/03/2014
 * Time: 20:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DD.Collections.CodeSetWideTest
{
	[TestFixture]
	public class Constructors
	{
		[Test]
		public void FromCodesThrows()
		{
			CodeSetWide csw;

			// requires not null
			Assert.Throws<ArgumentNullException> (
				delegate { csw = new CodeSetWide((IEnumerable<Code>) null);}
			);

			// requres more than ICodeSetService.ListMaxCount members
			Assert.Throws<ArgumentException> (
				delegate { csw = new CodeSetWide(new List<Code> () {
					0,70000});}
			);
			
			// requires more than ICodeSetService.PairCount NOT members
			Assert.Throws<ArgumentException> (
				delegate { csw = new CodeSetWide(new List<Code> () {
					65525,65526,65527,65528,
					65529,65530,65531,65532,
					65533,65534,65535,65536,
					65537,65538,65539,65540,65542});}
			);
			
			// requires to span over single unicode page
			Assert.Throws<ArgumentException> (
				delegate { csw = new CodeSetWide(new List<Code> () {
					0,1,2,3,
					4,5,6,7,
					8,9,10,11,
					12,13,14,15,
					60000});}
			);
		}

		[Test]
		public void FromBitsThrows()
		{
			CodeSetWide csw;

			// requires not null
			Assert.Throws<ArgumentNullException> (
				delegate { csw = new CodeSetWide((BitSetArray) null);}
			);

			// requres more than ICodeSetService.PairCount members
			Assert.Throws<ArgumentException> (
				delegate { csw = new CodeSetWide(new BitSetArray () {
					0,70000});}
			);
			
			// requires more than ICodeSetService.PairCount NOT members
			Assert.Throws<ArgumentException> (
				delegate { csw = new CodeSetWide(new BitSetArray () {
					65525,65526,65527,65528,
					65529,65530,65531,65532,
					65533,65534,65535,65536,
					65537,65538,65539,65540,65542});}
			);
			
			// requires to span over single unicode page
			Assert.Throws<ArgumentException> (
				delegate { csw = new CodeSetWide(new BitSetArray () {
					0,1,2,3,
					4,5,6,7,
					8,9,10,11,
					12,13,14,15,
					60000});}
			);

			// requires valid codes
			Assert.Throws<ArgumentOutOfRangeException> (
				delegate { csw = new CodeSetWide(new BitSetArray () {
					0,1,2,3,
					4,5,6,7,
					8,9,10,11,
					12,13,14,15,
					66000, Code.MaxValue+1});}
			);
		}
	}
}
