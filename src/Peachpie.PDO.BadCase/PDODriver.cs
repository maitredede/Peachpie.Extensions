using Pchp.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Peachpie.PDO
{
    [PhpHidden]
    public abstract class PDODriver : IPDODriver
    {
        public string Name { get; }
        public DbProviderFactory DbFactory { get; }

        public PDODriver(string name, DbProviderFactory dbFactory)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            if (dbFactory == null)
                throw new ArgumentNullException(nameof(dbFactory));

            this.Name = name;
            this.DbFactory = dbFactory;
        }

        protected abstract string BuildConnectionString(string dsn, string user, string password, PhpArray options);

        public virtual DbConnection OpenConnection(string dsn, string user, string password, PhpArray options)
        {
            string connectionString = this.BuildConnectionString(dsn, user, password, options);
            var connection = this.DbFactory.CreateConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            return connection;
        }

        public abstract Dictionary<string, ExtensionMethodDelegate> GetPDObjectExtensionMethods();

        public abstract string GetLastInsertId(PDO pdo, string name);

        public virtual bool TrySetAttribute(Dictionary<int, object> attributes, int attribute, PhpValue value)
        {
            return false;
        }
    }
}
