using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Pchp.Core;

namespace Peachpie.Library.PDO.SqlSrv
{
    /// <summary>
    /// PDO driver for Microsoft SqlServer
    /// </summary>
    /// <seealso cref="Peachpie.Library.PDO.PDODriver" />
    public class PDOSqlServerDriver : PDODriver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PDOSqlServerDriver"/> class.
        /// </summary>
        public PDOSqlServerDriver() : base("sqlsrv", SqlClientFactory.Instance)
        {
        }

        /// <inheritDoc />
        public override string GetLastInsertId(PDO pdo, string name)
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public override Dictionary<string, ExtensionMethodDelegate> GetPDObjectExtensionMethods()
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        protected override string BuildConnectionString(string dsn, string user, string password, PhpArray options)
        {
            throw new NotImplementedException();
        }
    }
}
