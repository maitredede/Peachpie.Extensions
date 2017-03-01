using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Pchp.Core;
using System.Data.Common;

namespace Peachpie.PDO.MySQL
{
    public sealed class PDOMySQLDriver : PDODriver
    {
        public PDOMySQLDriver() : base("mysql", MySqlClientFactory.Instance)
        {

        }

        protected override string BuildConnectionString(string dsn, string user, string password, PhpArray options)
        {
            //TODO mysql pdo parameters to dotnet connectionstring
            var csb = new MySqlConnectionStringBuilder(dsn);
            csb.UserID = user;
            csb.Password = password;
            return csb.ConnectionString;
        }

        public override Dictionary<string, ExtensionMethodDelegate> GetPDObjectExtensionMethods()
        {
            return new Dictionary<string, ExtensionMethodDelegate>();
        }

        public override string GetLastInsertId(PDO pdo, string name)
        {
            using (var cmd = pdo.CreateCommand("SELECT LAST_INSERT_ID()"))
            {
                object value = cmd.ExecuteScalar();
                return value?.ToString();
            }
        }
    }
}
