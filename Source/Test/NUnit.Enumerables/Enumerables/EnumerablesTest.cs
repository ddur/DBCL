// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using DD.Enumerables;
using NUnit.Framework;

namespace DD.Enumerables
{
    [TestFixture]
    public class EnumerablesTest
    {
	    [Test]
	    public void LoopTest() {

	        Loop loop;

	        Assert.Throws<ArgumentException> (delegate {loop = new Loop(-1);});
	        Assert.Throws<ArgumentException> (delegate {loop = new Loop(0);});
	        Assert.Throws<ArgumentException> (delegate {loop = new Loop(2, int.MaxValue, int.MaxValue);});
	        Assert.Throws<ArgumentException> (delegate {loop = new Loop(3, 0, int.MaxValue);});
	        Assert.Throws<ArgumentException> (delegate {loop = new Loop(3, 0, int.MinValue);});

	        // One time loop cannot fail
	        loop = new Loop(1, int.MaxValue);
	        Assert.True (loop.Last == int.MaxValue);

	        loop = new Loop(1, int.MinValue);
	        Assert.True (loop.Last == int.MinValue);

	        loop = new Loop(1, int.MaxValue, int.MaxValue);
	        Assert.True (loop.Last == int.MaxValue);

	        loop = new Loop(1, int.MinValue, int.MinValue);
	        Assert.True (loop.Last == int.MinValue);

	        loop = 1.Times().From(int.MaxValue).By(int.MaxValue);
	        Assert.True (loop.Last == int.MaxValue);

	        loop = 1.Times().From(int.MinValue).By(int.MinValue);
	        Assert.True (loop.Last == int.MinValue);
	        
	        // two times
	        loop = 2.Times().From(0).By(int.MaxValue);
	        Assert.True (loop.Last == int.MaxValue);

	        loop = 2.Times().By(int.MaxValue).From(0);
	        Assert.True (loop.Last == int.MaxValue);

	        loop = 2.Times().By(int.MinValue);
	        Assert.True (loop.Last == int.MinValue);

	        loop = 2.Times().By(int.MinValue);
	        Assert.True (loop.Last == int.MinValue);

	        loop = new Loop (2, int.MaxValue, int.MinValue);
	        Assert.True (loop.Last == -1);

	        loop = 2.Times().By(int.MinValue).From(int.MaxValue);
	        Assert.True (loop.Last == -1);

	        loop = new Loop (2, int.MinValue, int.MaxValue);
	        Assert.True (loop.Last == -1);

	        loop = 2.Times().From(int.MinValue).By(int.MaxValue);
	        Assert.True (loop.Last == -1);

	        // 3 times
	        loop = new Loop (3, int.MaxValue, int.MinValue+1);
	        Assert.True (loop.Last == int.MinValue+1);

	        loop = new Loop (3, int.MinValue, int.MaxValue);
	        Assert.True (loop.Last == int.MaxValue-1);

	        loop = 3.Times().From(int.MinValue).By(int.MaxValue);
	        Assert.True (loop.Last == int.MaxValue-1);

	        // prepare
	        int timesLoop = 0;
	        int timesCount = 0;
	        int loopStep = 0;
	        int loopStart = 0;
	        int loopFinal = 0;
	        
	        // begin test
	        timesLoop = 23;
	        loopStart = 100;
	        loopFinal = loopStart;
	        loopStep = 10;
	        timesCount = 0;
	        loop = timesLoop.Times().From(loopStart).By(loopStep);
	        foreach (var i in loop) {
	            Assert.True (i == loopFinal);
	            loopFinal += loop.Step;
	            ++timesCount;
	        }
            loopFinal -= loop.Step;
	        Assert.True (timesCount == timesLoop);
	        Assert.True (loopFinal == loopStart + ((timesCount-1) * loopStep));

	        loopStart = 100;
	        loopFinal = loopStart;
	        timesCount = 0;
	        foreach (var i in loop as IEnumerable) {
	            Assert.True ((int)i == loopFinal);
	            loopFinal += loop.Step;
	            ++timesCount;
	        }
            loopFinal -= loop.Step;
	        Assert.True (timesCount == timesLoop);
	        Assert.True (loopFinal == loopStart + ((timesCount-1) * loopStep));

	        // loop
	        timesCount = 0;
	        while (loop.Do) {
	            ++timesCount;
	        }
	        Assert.True (timesCount == timesLoop);

	        // loop again
	        timesCount = 0;
	        while (loop.Do) {
	            ++timesCount;
	        }
	        Assert.True (timesCount == timesLoop);

	        timesCount = 0;
	        var e = loop.GetEnumerator();
	        while (e.MoveNext()) {
	            ++timesCount;
	        }
	        Assert.True (timesCount == timesLoop);

	        loopStart = 20;
	        loopFinal = loopStart;
	        loopStep = 5;
	        loop = timesLoop.Times().From(loopStart).By(loopStep);
	        timesCount = 0;
	        while (loop.Do) {
	            loopFinal += loopStep;
	            ++timesCount;
	        }
            loopFinal -= loop.Step;
	        Assert.True (timesCount == timesLoop);
	        Assert.True (timesCount == loop.Times);
	        Assert.True (loopStart == loop.First);
	        Assert.True (loopFinal == loopStart + ((timesCount-1) * loopStep));
	        Assert.True (loopFinal == loop.First + ((loop.Times-1) * loop.Step));
	        Assert.True (loopStep == loop.Step);

	        // Cover Set properties 
	        loop.First = loopStart;
	        loop.Step = loopStep;

	        // Cast Range to Loop
	        loop = 20.To(10);//.By(2);
	        timesCount = 20;
	        loopFinal = loop.First;
	        while (loop.Do) {
	            --timesCount;
	            loopFinal += loop.Step;
	        }
            loopFinal -= loop.Step;

            Assert.True (loop.Times == 11);
	        Assert.True (loop.First == 20);
	        Assert.True (loop.Step == -1);
	        Assert.True (loopFinal == loop.First+((loop.Times-1)*loop.Step));
	        Assert.True (timesCount == 20 - loop.Times);
	        
	        // Cast Loop to Range
	        Range r = loop;
	        Assert.True (r.First == loop.First);
	        Assert.True (r.Last == loop.First+((loop.Times-1)*loop.Step));
	        Assert.True (r.Step == loop.Step);

	        // Cast Range to Loop to Range
	        loop = 20.To(-10);
	        r = loop;
	        Assert.True (r.First == loop.First);
	        Assert.True (r.Last == loop.First+((loop.Times-1)*loop.Step));
	        Assert.True (r.Step == loop.Step);
	        
	    }
	    
	    [Test]
	    public void RangeTest() {
	        Range range = new Range();

	        Assert.Throws<ArgumentException> (delegate {range.Step = 0;});
	        Assert.Throws<ArgumentException> (delegate {range.By (0);});

	        range.Step = int.MinValue;
	        range.Step = -1;
	        range.Step = 1;
	        range.Step = int.MaxValue;
	        
	        range.By (int.MinValue);
	        range.By (-1);
	        range.By(1);
	        range.By(int.MaxValue);
	        
	        // Construct
	        range = new Range (int.MinValue, int.MaxValue);
	        range = new Range (int.MaxValue, int.MinValue);

	        range = new Range (int.MaxValue, int.MaxValue);
	        range = new Range (int.MinValue, int.MinValue);

	        range = new Range (0, int.MaxValue);
	        range = new Range (0, int.MinValue);

	        range = new Range (int.MaxValue, 0);
	        range = new Range (int.MinValue, 0);
	        
	        int timesCount = 0;
	        int rangeStart = 0;
	        int rangeFinal = 0;
            IEnumerator e;

	        // test loop
	        rangeStart = -10;
	        rangeFinal = 20;
	        range = rangeStart.To(rangeFinal);
	        Assert.True (range.Step == 1);

	        timesCount = 0;
	        rangeFinal = rangeStart;
	        foreach (var item in range) {
	            Assert.True (rangeFinal == item);
	            ++timesCount;
	            rangeFinal += range.Step;
	        }
            rangeFinal -= range.Step;
            
	        Assert.True (timesCount == 31);
	        Assert.True (timesCount == 1 + Math.Abs (range.Last - range.First));
	        Assert.True (rangeFinal == range.Last);
	        
	        timesCount = 0;
	        rangeFinal = rangeStart;
	        while (range.Do) {
	            ++timesCount;
	            rangeFinal += range.Step;
	        }
            rangeFinal -= range.Step;
            
	        Assert.True (timesCount == 31);
	        Assert.True (timesCount == 1 + Math.Abs (range.Last - range.First));
	        Assert.True (rangeFinal == range.Last);
            
	        // Do again
	        timesCount = 0;
	        rangeFinal = rangeStart;
	        while (range.Do) {
	            ++timesCount;
	            rangeFinal += range.Step;
	        }
            rangeFinal -= range.Step;
            
	        Assert.True (timesCount == 31);
	        Assert.True (timesCount == 1 + Math.Abs (range.Last - range.First));
	        Assert.True (rangeFinal == range.Last);

            // enumerate	        
            e = ((IEnumerable)range).GetEnumerator();
	        timesCount = 0;
	        rangeFinal = rangeStart;
	        while (e.MoveNext()) {
	            ++timesCount;
	            rangeFinal += range.Step;
	        }
            rangeFinal -= range.Step;

            Assert.True (timesCount == 31);
	        Assert.True (timesCount == 1 + Math.Abs (range.Last - range.First));
	        Assert.True (rangeFinal == range.Last);

	        // Cast Range to Loop
	        Loop loop = range;
	        Assert.True (loop.Times == 31);


            // reverse	        
	        rangeStart = 20;
	        rangeFinal = -10;
	        range = rangeStart.To(rangeFinal);
	        Assert.True (range.Step == -1);

	        timesCount = 0;
	        rangeFinal = rangeStart;
	        foreach (var item in range) {
	            Assert.True (rangeFinal == item);
	            ++timesCount;
	            rangeFinal += range.Step;
	        }
            rangeFinal -= range.Step;
            
	        Assert.True (timesCount == 31);
	        Assert.True (timesCount == 1 + Math.Abs (range.Last - range.First));
	        Assert.True (rangeFinal == range.Last);
	        
	        timesCount = 0;
	        rangeFinal = rangeStart;
	        while (range.Do) {
	            ++timesCount;
	            rangeFinal += range.Step;
	        }
            rangeFinal -= range.Step;

	        Assert.True (timesCount == 31);
	        Assert.True (timesCount == 1 + Math.Abs (range.Last - range.First));
	        Assert.True (rangeFinal == range.Last);
            
            // enumerate	        
            e = ((IEnumerable)range).GetEnumerator();
	        timesCount = 0;
	        rangeFinal = rangeStart;
	        while (e.MoveNext()) {
	            ++timesCount;
	            rangeFinal += range.Step;
	        }
            rangeFinal -= range.Step;

            Assert.True (timesCount == 31);
	        Assert.True (timesCount == 1 + Math.Abs (range.Last - range.First));
	        Assert.True (rangeFinal == range.Last);

	        // Cast Range to Loop
	        loop = range;
	        Assert.True (loop.Times == 31);

	        // Cast Range to Loop with step outside range
	        range.Step = 32;
	        Assert.True (range.Last == range.First);
	        loop = range;
	        Assert.True (loop.Times == 1);
	        
	        
	    }

    }
}
