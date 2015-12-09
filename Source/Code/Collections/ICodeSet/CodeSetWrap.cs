// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2014 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using DD.Diagnostics;

namespace DD.Collections.ICodeSet
{
    /// <summary>Wraps over (and converts to) cloned BitSetArray
    /// <remarks>Can be empty, can be full, can contain up to <see cref="Code.MaxCodeCount">Code.MaxCodeCount-1</see> codes</remarks>
    /// </summary>
    [Serializable]
    public sealed class CodeSetWrap : CodeSet
    {
        #region Ctor

        public static CodeSetWrap From()
        {
            Contract.Ensures(Contract.Result<CodeSetWrap>() != null);

            var bits = BitSetArray.Empty();
            Contract.Assume(bits.Count == 0);
            return new CodeSetWrap(bits);
        }

        public static CodeSetWrap From(CodeSetWrap wrap)
        {
            Contract.Requires<ArgumentNullException>(wrap != null);
            Contract.Ensures(Contract.Result<CodeSetWrap>() != null);

            return new CodeSetWrap(wrap);
        }

        public static CodeSetWrap From(params Code[] codes)
        {
            Contract.Requires<ArgumentNullException>(codes != null);
            Contract.Ensures(Contract.Result<CodeSetWrap>() != null);

            return new CodeSetWrap(codes as IEnumerable<Code>);
        }

        public static CodeSetWrap From(IEnumerable<Code> codes)
        {
            Contract.Requires<ArgumentNullException>(codes != null);
            Contract.Ensures(Contract.Result<CodeSetWrap>() != null);

            return new CodeSetWrap(codes);
        }

        public static CodeSetWrap From(BitSetArray bits)
        {
            Contract.Requires<ArgumentNullException>(bits != null);
            Contract.Requires<IndexOutOfRangeException>(bits.Count == 0 || bits.Length.IsCodesCount() || bits.Last.HasCodeValue());
            Contract.Ensures(Contract.Result<CodeSetWrap>() != null);

            return new CodeSetWrap(bits);
        }

        /// <summary>Quick shallow clone</summary>
        /// <param name="wrap">CodeSetWrap</param>
        private CodeSetWrap(CodeSetWrap wrap)
        {
            Contract.Requires<ArgumentNullException>(wrap != null);
            Contract.Ensures(Theory.Construct(wrap, this));

            sorted = wrap.sorted;
        }

        private CodeSetWrap(IEnumerable<Code> codes)
        {
            Contract.Requires<ArgumentNullException>(codes != null);
            Contract.Ensures(Theory.Construct(codes, this));

            sorted = BitSetArray.From(codes.ToValues());
        }

        /// <summary>Copy(bits) &amp; bits.TrimExcess()</summary>
        /// <param name="bits">BitSetArray</param>
        private CodeSetWrap(BitSetArray bits)
        {
            Contract.Requires<ArgumentNullException>(bits != null);
            Contract.Requires<IndexOutOfRangeException>(bits.Count == 0 || bits.Length.IsCodesCount() || bits.Last.HasCodeValue());
            Contract.Ensures(Theory.Construct(bits, this));

            sorted = BitSetArray.Copy(bits);
            sorted.TrimExcess();
        }

        #endregion

        #region Fields

        readonly BitSetArray sorted;

        #endregion

        #region ICodeSet

        [Pure]
        public override bool this[Code code]
        {
            get
            {
                return sorted.Count != 0 && sorted[code.Value];
            }
        }

        [Pure]
        public override int Count
        {
            get
            {
                return sorted.Count;
            }
        }

        [Pure]
        public override int Length
        {
            get
            {
                return sorted.Span();
            }
        }

        [Pure]
        public override Code First
        {
            get
            {
                if (sorted.Count != 0)
                {
                    Contract.Assume(sorted.First.HasValue);
                    return (int)sorted.First;
                }
                throw new InvalidOperationException();
            }
        }

        [Pure]
        public override Code Last
        {
            get
            {
                if (sorted.Count != 0)
                {
                    Contract.Assume(sorted.Last.HasValue);
                    return (int)sorted.Last;
                }
                throw new InvalidOperationException();
            }
        }

        [Pure]
        public override IEnumerator<Code> GetEnumerator()
        {
            foreach (var code in sorted)
            {
                yield return (Code)code;
            }
        }

        #endregion

        #region Extended

        public BitSetArray ToBitSetArray()
        {
            Contract.Ensures(Contract.Result<BitSetArray>() != null);

            return BitSetArray.Copy(this.sorted);
        }

        public IEnumerable<Code> Complement
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<Code>>() != null);
                foreach (var item in this.sorted.Complement())
                {
                    yield return item;
                }
            }
        }

        #endregion

        #region Invariant

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(Theory.Invariant(this));
        }

        #endregion

        #region Theory

        static class Theory
        {

            [Pure]
            public static bool Construct(CodeSetWrap self)
            {
                Success success = true;

                success.Assert(!self.sorted.IsNull());
                success.Assert(self.sorted.Count == 0);

                return success;
            }

            [Pure]
            public static bool Construct(CodeSetWrap wrap, CodeSetWrap self)
            {
                Success success = true;

                success.Assert(!wrap.IsNull());
                success.Assert(!wrap.sorted.IsNull());
                success.Assert(!self.sorted.IsNull());

                success.Assert(self.sorted.Is(wrap.sorted));

                return success;
            }

            [Pure]
            public static bool Construct(BitSetArray bits, CodeSetWrap self)
            {
                Success success = true;

                success.Assert(!bits.IsNull());
                success.Assert(!self.sorted.IsNull());
                success.Assert(!self.sorted.Is(bits));
                success.Assert(self.sorted.SetEquals(bits));

                if (bits.Count != 0)
                {
                    foreach (var item in bits)
                    {
                        success.Assert(self.sorted[item]);
                    }
                }
                else
                {
                    success.Assert(self.sorted.Count == 0);
                }

                return success;
            }

            [Pure]
            public static bool Construct(IEnumerable<Code> codes, CodeSetWrap self)
            {
                Success success = true;

                success.Assert(self.sorted.IsNot(null));

                if (!codes.IsNullOrEmpty())
                {
                    success.Assert(self.sorted.Count == codes.Distinct().Count());
                    foreach (var item in codes)
                    {
                        success.Assert(self.sorted[item]);
                    }
                }
                else
                {
                    success.Assert(Construct(self));
                }

                return success;
            }

            [Pure]
            public static bool Invariant(CodeSetWrap self)
            {
                Success success = true;

                // private
                success.Assert(!self.sorted.IsNull());

                success.Assert(self.sorted.Length <= Code.MaxCount);

                if (self.sorted.Count != 0)
                {
                    success.Assert(self.sorted.First.HasCodeValue());
                    success.Assert(self.sorted.Last.HasCodeValue());
                }
                else
                {
                    success.Assert(self.sorted.First == null);
                    success.Assert(self.sorted.Last == null);
                }

                // public <- private
                success.Assert(self.Length == self.sorted.Span());
                success.Assert(self.Count == self.sorted.Count);
                if (self.Count != 0)
                {
                    Contract.Assume(self.sorted.First.HasValue);
                    Contract.Assume(self.sorted.Last.HasValue);
                    success.Assert(self.First == (Code)self.sorted.First);
                    success.Assert(self.Last == (Code)self.sorted.Last);
                }

                return success;
            }
        }

        #endregion
    }
}
