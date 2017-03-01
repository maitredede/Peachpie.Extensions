using Pchp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peachpie.PDO
{
    partial class PDO
    {
        public const int FETCH_USE_DEFAULT = 0;

        public const int FETCH_LAZY = 1;

        public const int FETCH_ASSOC = 2;

        public const int FETCH_NUM = 3;

        public const int FETCH_BOTH = 4;

        public const int FETCH_OBJ = 5;

        public const int FETCH_BOUND = 6; /* return true/false only; rely on bound columns */

        public const int FETCH_COLUMN = 7;  /* fetch a numbered column only */

        public const int FETCH_CLASS = 8;   /* create an instance of named class, call ctor and set properties */

        public const int FETCH_INTO = 9;        /* fetch row into an existing object */

        public const int FETCH_FUNC = 10;        /* fetch into function and return its result */

        public const int FETCH_NAMED = 11;    /* like FETCH_ASSOC, but can handle duplicate names */

        public const int FETCH_KEY_PAIR = 12;    /* fetch into an array where the 1st column is a key and all subsequent columns are values */

        public const int FETCH__MAX = 13;/* must be last */
    }
}
