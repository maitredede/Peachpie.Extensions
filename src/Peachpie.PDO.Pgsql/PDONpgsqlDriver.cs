using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Pchp.Core;

namespace Peachpie.PDO.Pgsql
{
    public class PDONpgsqlDriver : PDODriver
    {
        public PDONpgsqlDriver() : base("pgsql", NpgsqlFactory.Instance)
        {

        }

        protected override string BuildConnectionString(string dsn, string user, string password, PhpArray options)
        {
            //TODO pgsql pdo parameters to dotnet connectionstring
            var csb = new NpgsqlConnectionStringBuilder(dsn);
            csb.Username = user;
            csb.Password = password;
            return csb.ConnectionString;
        }

        public override Dictionary<string, ExtensionMethodDelegate> GetPDObjectExtensionMethods()
        {
            return new Dictionary<string, ExtensionMethodDelegate>();
        }

        public override string GetLastInsertId(PDO pdo, string name)
        {
            throw new NotImplementedException();
        }
    }
}
