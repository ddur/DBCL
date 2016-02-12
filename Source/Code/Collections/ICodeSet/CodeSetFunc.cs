// --------------------------------------------------------------------------------
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------


using System;
using System.Linq;
using System.Diagnostics.Contracts;

namespace DD.Collections.ICodeSet
{

    /// <summary>
    /// Cannot be empty, matches quick, create&enumerate slow
    /// </summary>
    public class CodeSetFunc : CodeSet
    {
        #region ctor

        public static CodeSetFunc From (Predicate<Code> func) {
            Contract.Requires<ArgumentNullException> (func.IsNot (null));
            Contract.Requires<ArgumentEmptyException>(!func.ToCodes().IsEmpty());

            return new CodeSetFunc (func);
        }

        internal CodeSetFunc(Predicate<Code> func)
        {
            Contract.Requires<ArgumentNullException> (func.IsNot (null));
            Contract.Requires<ArgumentEmptyException>(!func.ToCodes().IsEmpty());

            this.func = func;
            foreach (var item in func.ToIntCodes()) {
                if (item < start) start = item;
                final = item;
                count += 1;
            }
        }

        #endregion

        #region Fields

        private readonly int start = Code.MaxValue;
        private readonly int final = Code.MinValue;
        private readonly int count = 0;
        private readonly Predicate<Code> func;

        #endregion

        #region implemented abstract members of CodeSet

        public override System.Collections.Generic.IEnumerator<Code> GetEnumerator()
        {
            for (int index = start; index <= final; index++) {
                if (func ((Code)index)) {
                    yield return index;
                }
            }
        }

        public override bool this[Code code] {
            get {
                return func(code);
            }
        }

        public override bool this[int value] {
            get {
                return value.HasCodeValue() && func(value);
            }
        }

        public override bool IsEmpty {
            get {
                return false;
            }
        }

        public override int Length {
            get {
                return 1 + final - start;
            }
        }

        public override Code First {
            get {
                return start;
            }
        }

        public override Code Last {
            get {
                return final;
            }
        }

        public override bool IsReduced {
            get {
                return false;
            }
        }

        public override int Count {
            get {
                return count;
            }
        }

        #endregion
    }
}
