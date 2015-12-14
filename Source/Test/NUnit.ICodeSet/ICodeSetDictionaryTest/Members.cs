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

namespace DD.Collections.ICodeSet.ICodeSetDictionaryTest {

    [TestFixture]
    public class Members {
        private ICodeSetDictionary icsDict;
        private ICodeSetDictionary<long> icsDictLong;

        [Test]
        public void Add_Key () {
            icsDict = new ICodeSetDictionary ();
            icsDict.Add (CodeSetFull.From (0, 5));
            icsDict.Add (CodeSetList.From (new Code[] { 0, 2, 4, 6, 8, 10 }));
            Assert.True (icsDict[CodeSetFull.From (0, 5)] == 0);
            Assert.True (icsDict[CodeSetList.From (new Code[] { 0, 2, 4, 6, 8, 10 })] == 1);
            Assert.True (icsDict[CodeSetPage.From (new Code[] { 0, 2, 4, 6, 8, 10 })] == 1);

            // add duplicate
            Assert.Throws<ArgumentException> (
                delegate {
                    icsDict.Add (CodeSetFull.From (0, 5));
                }
            );
            Assert.Throws<ArgumentException> (
                delegate {
                    icsDict.Add (CodeSetPage.From (new Code[] { 0, 2, 4, 6, 8, 10 }));
                }
            );
        }

        [Test]
        public void Add_KeyAndValue_NotSupported () {
            icsDict = new ICodeSetDictionary ();
            var icsIDictAsIDictionary = icsDict as IDictionary<ICodeSet, int>;
            Assert.NotNull (icsIDictAsIDictionary);
            Assert.Throws<NotSupportedException> (
                delegate {
                    icsIDictAsIDictionary.Add (CodeSetFull.From (0, 5), 8);
                }
            );
        }

        [Test]
        public void Add_KeyAndValue_Typed () {
            icsDictLong = new ICodeSetDictionary<long> ();
            Assert.That (
                delegate {
                    icsDictLong.Add (CodeSetFull.From (0, 5), 8);
                },
                Throws.Nothing
            );
            Assert.That (
                delegate {
                    icsDictLong.Add (CodeSetFull.From (0, 5), 8);
                },
                Throws.TypeOf<ArgumentException> ()
            );
        }

        [Test]
        public void Add_KeyValuePair_NotSupported () {
            icsDict = new ICodeSetDictionary ();
            var icsDictAsKvpCollection = (ICollection<KeyValuePair<ICodeSet, int>>)icsDict;
            Assert.NotNull (icsDictAsKvpCollection);
            Assert.Throws<NotSupportedException> (
                delegate { icsDictAsKvpCollection.Add (new KeyValuePair<ICodeSet, int> (CodeSetFull.From (0, 5), 8)); }
            );
        }

        [Test]
        public void Add_KeyValuePair_Typed () {
            icsDictLong = new ICodeSetDictionary<long> ();
            Assert.That (
                delegate {
                    icsDictLong.Add (new KeyValuePair<ICodeSet, long> (CodeSetFull.From (0, 5), 8));
                },
                Throws.Nothing
            );
            Assert.That (
                delegate {
                    icsDictLong.Add (new KeyValuePair<ICodeSet, long> (CodeSetFull.From (0, 5), 8));
                },
                Throws.TypeOf<ArgumentException> ()
            );
        }

        [Test]
        public void Clear () {
            icsDict = new ICodeSetDictionary ();
            Assert.True (icsDict.Count == 0);
            icsDict.Add (CodeSetFull.From (0, 5));
            Assert.True (icsDict.Count == 1);
            icsDict.Add (CodeSetFull.From (0, 4));
            Assert.True (icsDict.Count == 2);
            icsDict.Clear ();
            Assert.True (icsDict.Count == 0);
        }

        [Test]
        public void ContainsKey () {
            icsDict = new ICodeSetDictionary ();
            icsDict.Add (CodeSetFull.From (0, 5));
            Assert.True (icsDict.ContainsKey (CodeSetFull.From (0, 5)));
        }

        [Test]
        public void Count () {
            icsDict = new ICodeSetDictionary ();
            Assert.True (icsDict.Count == 0);
            icsDict.Add (CodeSetFull.From (0, 5));
            Assert.True (icsDict.Count == 1);
            icsDict.Add (CodeSetFull.From (0, 4));
            Assert.True (icsDict.Count == 2);
            icsDict.Remove (CodeSetFull.From (0, 5));
            Assert.True (icsDict.Count == 1);
            icsDict.Clear ();
            Assert.True (icsDict.Count == 0);
        }

        [Test]
        public void Find () {
            icsDict = new ICodeSetDictionary ();
            icsDict.Add (CodeSetFull.From (0, 5));
            icsDict.Add (CodeSetFull.From (0, 4));
            ICodeSet findSet = CodeSetFull.From (0, 5);
            Assert.True (icsDict.Find (ref findSet));
        }

        [Test]
        public void IsReadOnly () {
            icsDict = new ICodeSetDictionary ();
            Assert.False (icsDict.IsReadOnly);
        }

        [Test]
        public void Item_Get () {
            icsDict = new ICodeSetDictionary ();
            icsDict.Add (CodeSetFull.From (0, 4));
            icsDict.Add (CodeSetFull.From (0, 5));
            Assert.True (icsDict[CodeSetFull.From (0, 4)] == 0);
            Assert.True (icsDict[CodeSetFull.From (0, 5)] == 1);
        }

        [Test]
        public void Item_Set_NotSupported () {
            icsDict = new ICodeSetDictionary ();
            icsDict.Add (CodeSetFull.From (0, 4));
            Assert.Throws<NotSupportedException> (
                delegate {
                    icsDict[CodeSetFull.From (0, 4)] = 5;
                }
            );
        }

        [Test]
        public void Item_Set_Typed () {
            icsDictLong = new ICodeSetDictionary<long> ();
            Assert.That (
                delegate {
                    icsDictLong.Add (new KeyValuePair<ICodeSet, long> (CodeSetFull.From (0, 5), 8));
                },
                Throws.Nothing
            );
            Assert.AreEqual (icsDictLong[CodeSetFull.From (0, 5)], 8);
            icsDictLong[CodeSetFull.From (0, 5)] = 9;
            Assert.AreEqual (icsDictLong[CodeSetFull.From (0, 5)], 9);
        }

        [Test]
        public void Keys_Get () {
            icsDict = new ICodeSetDictionary ();
            ICodeSet set1 = CodeSetFull.From (0, 4);
            ICodeSet set2 = CodeSetFull.From (0, 5);
            icsDict.Add (set1);
            icsDict.Add (set2);
            ICollection<ICodeSet> keys = icsDict.Keys;
            Assert.True (keys.Count == 2);
            Assert.True (keys.Contains (set1));
            Assert.True (keys.Contains (set2));
        }

        [Test]
        public void Remove_Key () {
            icsDict = new ICodeSetDictionary ();
            icsDict.Add (CodeSetFull.From (0, 5));
            icsDict.Add (CodeSetFull.From (0, 4));
            Assert.False (icsDict.Remove (CodeSetFull.From (0, 6)));
            Assert.True (icsDict.Remove (CodeSetFull.From (0, 4)));
            Assert.True (icsDict.Remove (CodeSetFull.From (0, 5)));
        }

        [Test]
        public void Remove_KeyValuePair_NotSupported () {
            icsDict = new ICodeSetDictionary ();
            icsDict.Add (CodeSetFull.From (0, 5));
            var kvp = new KeyValuePair<ICodeSet, int> (CodeSetFull.From (0, 5), 0);
            Assert.Throws<NotSupportedException> (
                delegate {
                    ((ICollection<KeyValuePair<ICodeSet, int>>)icsDict).Remove (kvp);
                }
            );
        }

        [Test]
        public void Remove_KeyValuePair_Typed () {
            icsDictLong = new ICodeSetDictionary<long> ();
            Assert.That (
                delegate {
                    icsDictLong.Add (new KeyValuePair<ICodeSet, long> (CodeSetFull.From (0, 5), 8));
                },
                Throws.Nothing
            );
            bool result = false;

            Assert.That (
                delegate {
                    result = icsDictLong.Remove (new KeyValuePair<ICodeSet, long> (CodeSetFull.From (1, 5), 8));
                },
                Throws.Nothing
            );
            Assert.False (result);

            Assert.That (
                delegate {
                    result = icsDictLong.Remove (new KeyValuePair<ICodeSet, long> (CodeSetFull.From (0, 5), 7));
                },
                Throws.Nothing
            );
            Assert.False (result);

            Assert.That (
                delegate {
                    result = icsDictLong.Remove (new KeyValuePair<ICodeSet, long> (CodeSetFull.From (0, 5), 8));
                },
                Throws.Nothing
            );
            Assert.True (result);
        }

        [Test]
        public void Contains () {
            icsDict = new ICodeSetDictionary ();
            icsDict.Add (CodeSetFull.From (0, 5));
            var icsDictAsKvpCollection = (ICollection<KeyValuePair<ICodeSet, int>>)icsDict;
            Assert.NotNull (icsDictAsKvpCollection);
            var kvp = new KeyValuePair<ICodeSet, int> (CodeSetFull.From (0, 5), 0);
            Assert.True (icsDictAsKvpCollection.Contains (kvp));
        }

        [Test]
        public void CopyTo () {
            icsDict = new ICodeSetDictionary ();
            ICodeSet a = CodeSetFull.From (0, 5);
            icsDict.Add (a);
            var icsDictAsKvpCollection = (ICollection<KeyValuePair<ICodeSet, int>>)icsDict;
            Assert.NotNull (icsDictAsKvpCollection);
            var array = new KeyValuePair<ICodeSet, int>[1];
            icsDictAsKvpCollection.CopyTo (array, 0);

            Assert.That (array[0].Key.Is (a));
            Assert.That (array[0].Value == 0);

            Assert.That (
                delegate {
                    icsDictAsKvpCollection.CopyTo (array, 1);
                },
                !Throws.Nothing
            );

            ICodeSet b = CodeSetFull.From (2, 5);
            icsDict.Add (b);

            Assert.That (
                delegate {
                    icsDictAsKvpCollection.CopyTo (array, 0);
                },
                !Throws.Nothing
            );
        }

        [Test]
        public void GetEnumerator_Of_KeyValuePairs () {
            icsDict = new ICodeSetDictionary ();
            icsDict.Add (CodeSetFull.From (0, 5));
            var icsDictAsKvpCollection = (ICollection<KeyValuePair<ICodeSet, int>>)icsDict;
            Assert.NotNull (icsDictAsKvpCollection);
            var enumerator = icsDictAsKvpCollection.GetEnumerator ();
            Assert.NotNull (enumerator);
            Assert.True (enumerator.MoveNext ());
            Assert.True (enumerator.Current.Key.Equals (CodeSetFull.From (0, 5)));
            Assert.True (enumerator.Current.Value == 0);
        }

        [Test]
        public void GetEnumerator_Of_Objects () {
            icsDict = new ICodeSetDictionary ();
            icsDict.Add (CodeSetFull.From (0, 5));
            var icsDictAsIEnumerable = (IEnumerable)icsDict;
            Assert.NotNull (icsDictAsIEnumerable);
            var enumerator = icsDictAsIEnumerable.GetEnumerator ();
            Assert.NotNull (enumerator);
            Assert.True (enumerator.MoveNext ());
            Assert.NotNull ((KeyValuePair<ICodeSet, int>)enumerator.Current);
            Assert.True (((KeyValuePair<ICodeSet, int>)enumerator.Current).Key.Equals (CodeSetFull.From (0, 5)));
            Assert.True (((KeyValuePair<ICodeSet, int>)enumerator.Current).Value == 0);
        }

        [Test]
        public void TryGetValue () {
            icsDict = new ICodeSetDictionary ();
            icsDict.Add (CodeSetFull.From (0, 5));
            icsDict.Add (CodeSetFull.From (0, 4));
            int value = int.MinValue;
            Assert.True (
                icsDict.TryGetValue (CodeSetFull.From (0, 4), out value)
            );
            Assert.True (value == 1);

            Assert.True (
                icsDict.TryGetValue (CodeSetFull.From (0, 5), out value)
            );
            Assert.True (value == 0);

            Assert.True (
                icsDict.TryGetValue (CodeSetFull.From (0, 4), out value)
            );
            Assert.True (value == 1);

            Assert.True (
                icsDict.TryGetValue (CodeSetFull.From (0, 5), out value)
            );
            Assert.True (value == 0);

            Assert.False (
                icsDict.TryGetValue (CodeSetFull.From (0, 6), out value)
            );
        }

        [Test]
        public void Values_Get () {
            icsDict = new ICodeSetDictionary ();
            icsDict.Add (CodeSetFull.From (0, 5));
            icsDict.Add (CodeSetFull.From (0, 4));
            icsDict.Add (CodeSetFull.From (0, 6));
            Assert.True (icsDict.Values.Distinct ().Count () == 3);
            Assert.True (icsDict.Values.Contains (0));
            Assert.True (icsDict.Values.Contains (1));
            Assert.True (icsDict.Values.Contains (2));
        }
    }
}