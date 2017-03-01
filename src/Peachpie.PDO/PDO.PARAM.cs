﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peachpie.PDO
{
    partial class PDO
    {
        internal const uint PARAM_FLAGS = 0xFFFF0000;
        internal static uint PARAM_TYPE(uint x) => x & ~PARAM_FLAGS;

        public const int PARAM_NULL = 0;
        /// <summary>
        /// int as in long (the php native int type). If you mark a column as an int, PDO expects get_col to return a pointer to a long
        /// </summary>
        public const int PARAM_INT = 1;
        /// <summary>
        /// get_col ptr should point to start of the string buffer
        /// </summary>
        public const int PARAM_STR = 2;
        /// <summary>
        /// get_col: when len is 0 ptr should point to a php_stream *, otherwise it should behave like a string. Indicate a NULL field value by setting the ptr to NULL
        /// </summary>
        public const int PARAM_LOB = 3;
        /// <summary>
        /// get_col: will expect the ptr to point to a new PDOStatement object handle, but this isn't wired up yet
        /// </summary>
        public const int PARAM_STMT = 4;
        /// <summary>
        /// get_col ptr should point to a zend_bool
        /// </summary>
        public const int PARAM_BOOL = 5;
        /// <summary>
        /// get_col ptr should point to a zval* and the driver is responsible for adding correct type information to get_column_meta()
        /// </summary>
        public const int PARAM_ZVAL = 6;
        /// <summary>
        /// magic flag to denote a parameter as being input/output
        /// </summary>
        public const int PARAM_INPUT_OUTPUT = -1;
    }
}
