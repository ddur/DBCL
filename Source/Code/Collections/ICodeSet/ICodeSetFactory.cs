// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DD.Collections.Specialized
{
    /// <summary>Produces ICodeSet's
    /// <para>If OutputDictionary is not null, methods in this class never return duplicate ICodeSet</para>  
    /// <para>(for all factored ICodeSet =&gt; ReferenceEquals=&gt;(Value)Equals=&gt;SetEquals=&gt;SequenceEqual)</para>
    /// <para>If InputDictionary is not null, set arguments must be members of that dictionary</para>  
    /// <para>OutputDictionary and InputDictionary can be same instance</para>
    /// </summary>
    public static class ICodeSetFactory
    {
        private const bool ThisMethodHandlesNull = true;
        public static ICodeSetDictionary OutputDictionary = null;
        public static ICodeSetDictionary InputDictionary = null;
        
        #region ToICodeSet Factory

        public static ICodeSet ToICodeSet (this string coded) {
            Contract.Requires<ArgumentNullException> (!coded.Is(null));
            Contract.Requires<ArgumentException> (coded != string.Empty);

            Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
            Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

            return ToICodeSet (coded.Decode());
        }

        public static ICodeSet ToICodeSet (char req, params char[] opt) {
            Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

            Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
            Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

            List<char> charList;
            if (opt.Length > 0) { // keyword "params" is never null
                charList = new List<char>(1 + opt.Length);
                charList.Add (req);
                charList.AddRange (opt);
            } else { // type char is never null
                charList = new List<char>(1);
                charList.Add (req);
            }
            return ToICodeSet (charList);
        }

        public static ICodeSet ToICodeSet (this IEnumerable<char> chars) {
            Contract.Requires<ArgumentNullException> (!chars.Is(null));
            Contract.Requires<ArgumentException> (!chars.IsEmpty());

            Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
            Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

            return ToICodeSet (chars.Cast<Code>());
        }

        public static ICodeSet ToICodeSet (Code req, params Code[] opt) {
            Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

            Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
            Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

            List<Code> codeList;
            if (opt.Length > 0) { // keyword "params" is never null
                codeList = new List<Code>(1 + opt.Length);
                codeList.Add (req);
                codeList.AddRange (opt);
            } else { // type Code is never null
                codeList = new List<Code>(1);
                codeList.Add (req);
            }
            return ToICodeSet (codeList);
        }

        public static ICodeSet ToICodeSet (IEnumerable<Code> codes) {
            Contract.Requires<ArgumentNullException> (!codes.Is(null));
            Contract.Requires<ArgumentException> (!codes.IsEmpty());
            Contract.Requires<ArgumentOutOfRangeException> (InputDictionary.Is(null) || !(codes is ICodeSet) || InputDictionary.ContainsKey((ICodeSet)codes));

            Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
            Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

            return ToICodeSet (new CodeSetBits (codes));
        }

        public static ICodeSet ToICodeSet (this BitSetArray bits) {
            Contract.Requires<ArgumentNullException> (!bits.Is(null));
            Contract.Requires<ArgumentException> (bits.Count != 0);
            Contract.Requires<ArgumentException> (bits.Last <= Code.MaxCount);

            Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
            Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

            return ToICodeSet (new CodeSetBits(bits));;
        }

        private static ICodeSet ToICodeSet (CodeSetBits codeSet) {
            Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);

            Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
            Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

            if (OutputDictionary.Is(null)) {
                //return codeSet.Optimal();
            }
            ICodeSet key = codeSet;
            if (!OutputDictionary.Find(ref key)) {
                //key = codeSet.Optimal();
                OutputDictionary.Add (key);
            }
            return key;
        }

        #endregion

        #region Optimization

//        [Pure] private static ICodeSet OptimalPartOne (this CodeSetBits bitSet) {
//            Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);
//            
//            Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
//
//            #region Null
//            if (bitSet.Is(null) || bitSet.Count == 0) {
//                return CodeSetNull.Singleton;
//            }
//            #endregion
//
//            #region Unit
//            else if (bitSet.Count == UnitCount) {
//                return new Code (bitSet.First);
//            }
//            #endregion
//
//            #region Pair
//            else if (bitSet.Count == PairCount) {
//                return new CodeSetPair (bitSet.First, bitSet.Last);
//            }
//            #endregion
//
//            #region Full
//            else if (bitSet.Count == bitSet.Length) {
//                return new CodeSetFull (bitSet.First, bitSet.Last);
//            }
//            #endregion
//
//            #region List
//            else if (bitSet.Count < ListMaxCount) {
//                // List space less than Bits/4 space (bitSet.Length/8)*4
//                if ((bitSet.Count * sizeof(int)) < (bitSet.Length/2)) {
//                    return new CodeSetList (bitSet);
//                }
//            }
//            #endregion
//            
//            return bitSet;
//
//        }
//
//        [Pure] private static ICodeSet OptimalPartTwo(this CodeSetBits bitSet) {
//            Contract.Requires<ArgumentNullException> (!bitSet.Is(null));
//
//            Contract.Ensures (!(Contract.Result<ICodeSet>() is CodeSetBits));
//
//            if (bitSet.First.UnicodePlane == bitSet.Last.UnicodePlane) {
//                return new CodeSetPage(bitSet);
//            } else {
//                Contract.Assert(bitSet.First.UnicodePlane != bitSet.Last.UnicodePlane);
//                return new CodeSetWide(bitSet);
//            }
//        }
//        
//        [Pure] internal static ICodeSet Optimal (this CodeSetBits bitSet) {
//            Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);
//
//            Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
//            Contract.Ensures (!(Contract.Result<ICodeSet>() is CodeSetBits));
//            
//            ICodeSet optimal = OptimalPartOne (bitSet);
//
//            if (!(optimal is CodeSetBits)) {
//                return optimal;
//            }
//            
//            Contract.Assert ((bitSet.Length - bitSet.Count) > 0 ); // has complement items
//            
//            if (((bitSet.Length - bitSet.Count) < (bitSet.Length / 4))) {
//                // possible more than 3/4 space saving
//                
//                // get complement and offset
//                BitSetArray bitsComplement = new BitSetArray (bitSet.Length);
//                int offset = bitSet.Complement.First();
//                foreach (var item in bitSet.Complement) {
//                    bitsComplement.Set (item - offset);
//                }
//                bitsComplement.TrimExcess();
//                if ((bitsComplement.Length < (bitSet.Length / 4)) ||
//                    (bitsComplement.Count <= ListMaxCount)) {
//                    // Can save at least 3/4 of space
//                    CodeSetFull fullSet = new CodeSetFull(bitSet.First, bitSet.Last);
//                    ICodeSet diffSet = OptimalPartOne (new CodeSetBits (FromCompact (bitsComplement, offset)));
//                    if (diffSet is CodeSetBits) diffSet = OptimalPartTwo (diffSet as CodeSetBits);
//                    return new CodeSetDiff (fullSet, diffSet);
//                }
//            }
//            // final choice
//            return OptimalPartTwo(bitSet);
//        }
//
        #endregion

        #region Operations Factory

        // ICodeSet operation names differ from ISet<T> names because
        // ICodeSet operation does not mutate any operand and returns new set,
        // unlike ISet<T> operations that mutate and return left operand.
        
        #region Union or(a,b,c...)

        public static ICodeSet Union (this ICodeSet req, params ICodeSet[] opt) {
            Contract.Requires<ArgumentNullException> (!req.Is(null));
            Contract.Requires<ArgumentNullException> (Contract.ForAll(opt, item => !item.Is(null)));

            Contract.Requires<ArgumentOutOfRangeException> (InputDictionary.Is(null) || InputDictionary.ContainsKey(req));
            Contract.Requires<ArgumentOutOfRangeException> (Contract.ForAll(opt, item => InputDictionary.Is(null) || InputDictionary.ContainsKey(item)));

            Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
            Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

            List<ICodeSet> setList;
            if (opt.Length > 0) {
                setList = new List<ICodeSet>(1 + opt.Length);
                setList.Add (req);
                setList.AddRange (opt);
            } else {
                setList = new List<ICodeSet>(1);
                setList.Add (req);
            }
            return Union (setList);
        }

        public static ICodeSet Union (this IEnumerable<ICodeSet> sets) {
            Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);
            Contract.Requires<ArgumentOutOfRangeException> 
                (sets.Is(null) || Contract.ForAll(sets, item => InputDictionary.Is(null) || item.Is(null) || InputDictionary.ContainsKey(item)));

            Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
            Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

            if (sets.Is(null) || sets.IsEmpty()) return Empty;
            int maxLength = int.MinValue;
            foreach (ICodeSet item in sets) {
                if (item.Is(null) || item.Count == 0) { continue; }
                if (item.Last > maxLength) { maxLength = item.Last; }
            }
            if (maxLength == int.MinValue) { return Empty; }
            ++maxLength;
            BitSetArray bits = new BitSetArray(maxLength);
            // TEST: foreach union takes same time as time to create another BitSetArray b (and then a.Or(b))
            foreach (ICodeSet item in sets) {
                if (item.Is(null) || item.IsEmpty()) { continue; }
                foreach (Code code in item) {
                    bits.Set (code, true);
                }
            }
            return ToICodeSet (bits);
        }

        #endregion
        
        #region Intersection and(((a,b),c),d...)
        
        public static ICodeSet Intersection (this ICodeSet req, params ICodeSet[] opt) {
            Contract.Requires<ArgumentNullException> (!req.Is(null));
            Contract.Requires<ArgumentNullException> (Contract.ForAll(opt, item => !item.Is(null)));

            Contract.Requires<ArgumentOutOfRangeException> (InputDictionary.Is(null) || InputDictionary.ContainsKey(req));
            Contract.Requires<ArgumentOutOfRangeException> (Contract.ForAll(opt, item => InputDictionary.Is(null) || InputDictionary.ContainsKey(item)));

            Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
            Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

            List<ICodeSet> setList;
            if (opt.Length > 0) { 
                setList = new List<ICodeSet> (1 + opt.Length);
                setList.Add (req);
                setList.AddRange (opt);
            } else {
                setList = new List<ICodeSet> (1);
                setList.Add (req);
            }
            return Intersection (setList);
        }

        public static ICodeSet Intersection (this IEnumerable<ICodeSet> sets) {
            Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);
            Contract.Requires<ArgumentOutOfRangeException> 
                (sets.Is(null) || Contract.ForAll(sets, item => InputDictionary.Is(null) || item.Is(null) || InputDictionary.ContainsKey(item)));

            Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
            Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

            if (sets.Is(null) || sets.IsEmpty()) { return Empty; }

            int maxLength = int.MinValue;
            foreach (ICodeSet codeSet in sets) {
                if (codeSet.Is(null) || codeSet.Count == 0) { return Empty; }
                if (codeSet.Last > maxLength) { maxLength = codeSet.Last; }
            }
            Contract.Assert (maxLength != int.MinValue);
            ++maxLength;

            BitSetArray a = new BitSetArray (maxLength);
            BitSetArray b = new BitSetArray (maxLength);
            bool first = true;
            foreach (ICodeSet codeSet in sets) {
                Contract.Assert (codeSet.Count != 0);
                if (first) {
                    first = false;
                    foreach (Code code in codeSet) {
                        a.Set (code, true);
                    }
                }
                else {
                    b.Clear();
                    foreach (Code code in codeSet) {
                        b.Set (code, false);
                    }
                    a.IntersectWith(b);
                    if (a.Count == 0) { return Empty; }
                }
            }
            return ToICodeSet (a);
        }

        #endregion
        
        #region Disjunction xor(((a,b),c),d...)

        public static ICodeSet Disjunction (this ICodeSet req, params ICodeSet[] opt) {
            Contract.Requires<ArgumentNullException> (!req.Is(null));
            Contract.Requires<ArgumentNullException> (Contract.ForAll(opt, item => !item.Is(null)));

            Contract.Requires<ArgumentOutOfRangeException> (InputDictionary.Is(null) || InputDictionary.ContainsKey(req));
            Contract.Requires<ArgumentOutOfRangeException> (Contract.ForAll(opt, item => InputDictionary.Is(null) || InputDictionary.ContainsKey(item)));

            Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
            Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

            List<ICodeSet> setList;
            if (opt.Length > 0) { 
                setList = new List<ICodeSet> (1 + opt.Length);
                setList.Add (req);
                setList.AddRange (opt);
            } else {
                setList = new List<ICodeSet> (1);
                setList.Add (req);
            }
            return Disjunction (setList);
        }

        public static ICodeSet Disjunction (this IEnumerable<ICodeSet> sets) {
            Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);
            Contract.Requires<ArgumentOutOfRangeException> 
                (sets.Is(null) || Contract.ForAll(sets, item => InputDictionary.Is(null) || item.Is(null) || InputDictionary.ContainsKey(item)));

            Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
            Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

            if (sets.Is(null) || sets.IsEmpty()) { return Empty; }
            int maxLength = int.MinValue;
            foreach (ICodeSet codeSet in sets) {
                if (codeSet.Is(null) || codeSet.Count == 0) { continue; }
                if (codeSet.Last > maxLength) { maxLength = codeSet.Last; }
            }
            ++maxLength;
            BitSetArray a = new BitSetArray (maxLength);
            BitSetArray b = new BitSetArray (maxLength);
            bool first = true;
            foreach (ICodeSet codeSet in sets) {
                if (first) {
                    first = false;
                    foreach (Code code in codeSet) {
                        a.Set (code, true);
                    }
                }
                else {
                    b.Clear();
                    foreach (Code code in codeSet) {
                        b.Set (code, false);
                    }
                    a.SymmetricExceptWith(b);
                }
            }
            return ToICodeSet (a);
        }

        #endregion

        #region Difference (((a-b)-c)-d...)

        public static ICodeSet Difference (this ICodeSet req, params ICodeSet[] opt) {
            Contract.Requires<ArgumentNullException> (!req.Is(null));
            Contract.Requires<ArgumentNullException> (Contract.ForAll(opt, item => !item.Is(null)));

            Contract.Requires<ArgumentOutOfRangeException> (InputDictionary.Is(null) || InputDictionary.ContainsKey(req));
            Contract.Requires<ArgumentOutOfRangeException> (Contract.ForAll(opt, item => InputDictionary.Is(null) || InputDictionary.ContainsKey(item)));

            Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
            Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

            List<ICodeSet> setList;
            if (opt.Length > 0) { 
                setList = new List<ICodeSet> (1 + opt.Length);
                setList.Add (req);
                setList.AddRange (opt);
            } else {
                setList = new List<ICodeSet> (1);
                setList.Add (req);
            }
            return Difference (setList);
        }

        public static ICodeSet Difference (this IEnumerable<ICodeSet> sets) {
            Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);
            Contract.Requires<ArgumentOutOfRangeException> 
                (sets.Is(null) || Contract.ForAll(sets, item => InputDictionary.Is(null) || item.Is(null) || InputDictionary.ContainsKey(item)));

            Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
            Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

            if (sets.Is(null) || sets.IsEmpty()) { return Empty; }
            if (sets.First().Is(null) || sets.First().Count == 0) { return Empty; }
            
            int maxLength = int.MinValue;
            foreach (ICodeSet codeSet in sets) {
                if (codeSet.Last > maxLength) { maxLength = codeSet.Last; }
            }
            ++maxLength;
            BitSetArray bits = new BitSetArray (maxLength);
            bool first = true;
            // TEST: foreach item difference takes same time as time to create BitSetArray b (and then a.Not(b))
            foreach (ICodeSet codeSet in sets) {
                if (first) {
                    first = false;
                    foreach (Code code in codeSet) {
                        bits.Set (code, true);
                    }
                }
                else {
                    foreach (Code code in codeSet) {
                        bits.Set (code, false);
                        if (bits.Count == 0) { return Empty; }
                    }
                }
            }
            return ToICodeSet (bits.Cast<Code>());
        }

        #endregion
        
        #region Complement
        
        public static ICodeSet Complement (this ICodeSet req) {
            Contract.Requires<ArgumentNullException> (ThisMethodHandlesNull);
            Contract.Requires<ArgumentOutOfRangeException> (InputDictionary.Is(null) || InputDictionary.ContainsKey(req));

            Contract.Ensures (Contract.Result<ICodeSet>().IsNot(null));
            Contract.Ensures (OutputDictionary.Is(null) || OutputDictionary.ContainsKey(Contract.Result<ICodeSet>()));

            if (req.Is(null) || ((req.Length - req.Count) == 0)) return Empty;

            BitSetArray compact = req.ToCompact();
            compact.Not();
            compact.TrimExcess();
            Contract.Assert (compact.Count != 0);
            return ToICodeSet (compact.FromCompact(req.First));
        }
        
        #endregion
        
        #endregion
        
        #region Empty ICodeSet

        [Pure] public static ICodeSet Empty {
            get {
                return CodeSetNull.Singleton;
            }
        }

        #endregion

    }
}
