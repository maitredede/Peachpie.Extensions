using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peachpie.PDO
{
    partial class PDO
    {
        public const int ATTR_AUTOCOMMIT = 0;
        public const int ATTR_PREFETCH = 1;
        public const int ATTR_TIMEOUT = 2;
        public const int ATTR_ERRMODE = 3;
        public const int ATTR_SERVER_VERSION = 4;
        public const int ATTR_CLIENT_VERSION = 5;
        public const int ATTR_SERVER_INFO = 6;
        public const int ATTR_CONNECTION_STATUS = 7;
        public const int ATTR_CASE = 8;
        public const int ATTR_CURSOR_NAME = 9;
        public const int ATTR_CURSOR = 10;
        public const int ATTR_DRIVER_NAME = 11;
        public const int ATTR_ORACLE_NULLS = 12;
        public const int ATTR_PERSISTENT = 13;
        public const int ATTR_STATEMENT_CLASS = 14;
        public const int ATTR_FETCH_CATALOG_NAMES = 15;
        public const int ATTR_FETCH_TABLE_NAMES = 16;
        public const int ATTR_STRINGIFY_FETCHES = 17;
        public const int ATTR_MAX_COLUMN_LEN = 18;
        public const int ATTR_DEFAULT_FETCH_MODE = 19;
        public const int ATTR_EMULATE_PREPARES = 20;

        public const int ATTR_DRIVER_SPECIFIC = 1000;
    }
}
