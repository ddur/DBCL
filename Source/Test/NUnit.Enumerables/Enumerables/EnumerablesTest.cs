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
	        //Assert.Throws<ArgumentException> (delegate {loop = new Loop(3, 0, 0);});

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
	        
	        loop = new Loop (3, int.MaxValue, int.MinValue+1);
	        Assert.True (loop.Last == int.MinValue+1);

	        loop = new Loop (3, int.MinValue, int.MaxValue);
	        Assert.True (loop.Last == int.MaxValue-1);

	        loop = 3.Times().From(int.MinValue).By(int.MaxValue);
	        Assert.True (loop.Last == int.MaxValue-1);

	        int test = 0;
	        int time = 0;
	        int step = 0;
	        int init = 0;
	        int last = 0;
	        
	        // begin test
	        test = 23;
	        init = 100;
	        last = init;
	        step = 10;
	        time = 0;
	        loop = test.Times().From(init).By(step);
	        foreach (var i in loop) {
	            Assert.True (i == last);
	            last += loop.Step;
	            ++time;
	        }
	        Assert.True (time == test);
	        Assert.True (last == init + (time * step));

	        init = 100;
	        last = init;
	        time = 0;
	        foreach (var i in loop as IEnumerable) {
	            Assert.True ((int)i == last);
	            last += loop.Step;
	            ++time;
	        }
	        Assert.True (time == test);
	        Assert.True (last == init + (time * step));

	        // loop
	        time = 0;
	        while (loop.Do) {
	            ++time;
	        }
	        Assert.True (time == test);

	        // loop again
	        time = 0;
	        while (loop.Do) {
	            ++time;
	        }
	        Assert.True (time == test);

	        time = 0;
	        var e = loop.GetEnumerator();
	        while (e.MoveNext()) {
	            ++time;
	        }
	        Assert.True (time == test);

	        init = 20;
	        last = init;
	        step = 5;
	        loop = test.Times().From(init).By(step);
	        time = 0;
	        while (loop.Do) {
	            last += step;
	            ++time;
	        }
	        Assert.True (time == loop.Times);
	        Assert.True (init == loop.First);
	        Assert.True (last == loop.First + (loop.Times * loop.Step));
	        Assert.True (step == loop.Step);

	        // Cast Range to Loop
	        loop = 20.To(10);//.By(2);
	        time = 20;
	        last = loop.First;
	        while (loop.Do) {
	            --time;
	            last += loop.Step;
	        }
	        Assert.True (loop.Times == 11);
	        Assert.True (loop.First == 20);
	        Assert.True (loop.Step == -1);
	        Assert.True (last == loop.First+(loop.Times*loop.Step));
	        Assert.True (time == 20 - loop.Times);
	        
	        // Cast Loop to Range
	        Range r = loop;
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
	        
	        int times = 0;
	        int start = 0;
	        int final = 0;
            IEnumerator e;

	        // test loop
	        start = -10;
	        final = 20;
	        range = start.To(final);
	        Assert.True (range.Step == 1);

	        times = 0;
	        final = start;
	        foreach (var item in range) {
	            Assert.True (final == item);
	            ++times;
	            final += range.Step;
	        }
            final -= range.Step;
            
	        Assert.True (times == 31);
	        Assert.True (times == 1 + Math.Abs (range.Last - range.First));
	        Assert.True (final == range.Last);
	        
	        times = 0;
	        final = start;
	        while (range.Do) {
	            ++times;
	            final += range.Step;
	        }
            final -= range.Step;
            
	        Assert.True (times == 31);
	        Assert.True (times == 1 + Math.Abs (range.Last - range.First));
	        Assert.True (final == range.Last);
            
	        // Do again
	        times = 0;
	        final = start;
	        while (range.Do) {
	            ++times;
	            final += range.Step;
	        }
            final -= range.Step;
            
	        Assert.True (times == 31);
	        Assert.True (times == 1 + Math.Abs (range.Last - range.First));
	        Assert.True (final == range.Last);

            // enumerate	        
            e = ((IEnumerable)range).GetEnumerator();
	        times = 0;
	        final = start;
	        while (e.MoveNext()) {
	            ++times;
	            final += range.Step;
	        }
            final -= range.Step;

            Assert.True (times == 31);
	        Assert.True (times == 1 + Math.Abs (range.Last - range.First));
	        Assert.True (final == range.Last);

	        // Cast Range to Loop
	        Loop loop = range;
	        Assert.True (loop.Times == 31);


            // reverse	        
	        start = 20;
	        final = -10;
	        range = start.To(final);
	        Assert.True (range.Step == -1);

	        times = 0;
	        final = start;
	        foreach (var item in range) {
	            Assert.True (final == item);
	            ++times;
	            final += range.Step;
	        }
            final -= range.Step;
            
	        Assert.True (times == 31);
	        Assert.True (times == 1 + Math.Abs (range.Last - range.First));
	        Assert.True (final == range.Last);
	        
	        times = 0;
	        final = start;
	        while (range.Do) {
	            ++times;
	            final += range.Step;
	        }
            final -= range.Step;

	        Assert.True (times == 31);
	        Assert.True (times == 1 + Math.Abs (range.Last - range.First));
	        Assert.True (final == range.Last);
            
            // enumerate	        
            e = ((IEnumerable)range).GetEnumerator();
	        times = 0;
	        final = start;
	        while (e.MoveNext()) {
	            ++times;
	            final += range.Step;
	        }
            final -= range.Step;

            Assert.True (times == 31);
	        Assert.True (times == 1 + Math.Abs (range.Last - range.First));
	        Assert.True (final == range.Last);

	        // Cast Range to Loop
	        loop = range;
	        Assert.True (loop.Times == 31);

	        // Cast Range to Loop with step outside range
	        range.Step = 32;
	        loop = range;
	        Assert.True (loop.Times == 1);
	        
	        
	    }

    }
}
