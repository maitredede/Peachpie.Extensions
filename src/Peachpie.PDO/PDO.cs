using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pchp.Core;
using Pchp.Library;
using System.Data.Common;

namespace Peachpie.PDO
{

    /// <summary>
    /// Represents a connection between PHP and a database server
    /// </summary>
    /// <seealso cref="Pchp.Core.PhpResource" />
    [PhpType("PDO")]
    public partial class PDO : IDisposable, IPDO
    {
        private readonly Context m_ctx;
        private IPDODriver m_driver;
        private DbConnection m_con;
        private DbTransaction m_tx;
        private readonly Dictionary<int, object> m_attributes = new Dictionary<int, object>();
        private Dictionary<string, ExtensionMethodDelegate> m_extensionMethods;

        internal DbTransaction CurrentTransaction { get { return this.m_tx; } }

        /// <summary>
        /// Gets the native connection instance
        /// </summary>
        public DbConnection Connection { get { return this.m_con; } }

        private PDO(Context ctx)
        {
            this.m_ctx = ctx;
        }

        /// <summary>
        /// Creates a <see cref="PDO"/> instance to represent a connection to the requested database.
        /// </summary>
        /// <param name="ctx">The php context.</param>
        /// <param name="dsn">The Data Source Name.</param>
        /// <param name="username">The user name for the DSN string.</param>
        /// <param name="password">The password for the DSN string.</param>
        /// <param name="options">A key=&gt;value array of driver-specific connection options.</param>
        public PDO(Context ctx, string dsn, string username = null, string password = null, PhpArray options = null) : this(ctx)
        {
            this.__construct(dsn, username, password, options);
        }

        /// <inheritDoc />
        public void __construct(string dsn, string username = null, string password = null, PhpArray options = null)
        {
            this.SetDefaultAttributes();

            string[] items = dsn.Split(new[] { ':' }, 2);

            if (items[0].Equals("uri", StringComparison.Ordinal))
            {
                //Uri mode
                Uri uri;
                if (Uri.TryCreate(items[1], UriKind.Absolute, out uri))
                {
                    if (uri.Scheme.Equals("file", StringComparison.Ordinal))
                    {
                        throw new NotImplementedException("PDO uri DSN not implemented");
                        //return
                    }
                    else
                    {
                        throw new PDOException("PDO DSN as URI does not support other schemes than 'file'");
                    }
                }
                else
                {
                    throw new PDOException("Invalid uri in DSN");
                }
            }

            if (items.Length == 1)
            {
                //Alias mode
                throw new NotImplementedException("PDO DSN alias not implemented");
                //replace DSN alias with value
            }

            //DSN mode
            this.m_driver = PDOEngine.GetDriver(items[0]);
            if (this.m_driver == null)
            {
                throw new PDOException(string.Format("Driver '{0}' not found", items[0]));
            }

            this.m_extensionMethods = this.m_driver.GetPDObjectExtensionMethods();

            this.m_con = this.m_driver.OpenConnection(items[1], username, password, options);
            this.m_attributes.Set(ATTR_SERVER_VERSION, this.m_con.ServerVersion);
            this.m_attributes.Set(ATTR_DRIVER_NAME, this.m_driver.Name);
        }

        /// <inheritDoc />
        public bool inTransaction() => this.m_tx != null;

        /// <inheritDoc />
        public PhpValue __call(string name, PhpArray arguments)
        {
            if (this.m_extensionMethods.ContainsKey(name))
            {
                var method = this.m_extensionMethods[name];
                return method.Invoke(this, arguments);
            }
            throw new PDOException("Method not found");
        }

        /// <inheritDoc />
        void IDisposable.Dispose()
        {
            this.m_con.Dispose();
        }

        /// <summary>
        /// This function returns all currently available PDO drivers which can be used in DSN parameter of <see cref="PDO"/> constructor.
        /// </summary>
        /// <param name="ctx">The php context.</param>
        /// <returns></returns>
        public static PhpArray getAvailableDrivers()
        {
            return PDOStatic.pdo_drivers();
        }


        /// <summary>
        /// Creates a DbCommand object.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <returns></returns>
        [PhpHidden]
        public DbCommand CreateCommand(string statement)
        {
            var dbCommand = this.m_con.CreateCommand();
            dbCommand.CommandText = statement;
            dbCommand.Transaction = this.m_tx;
            dbCommand.CommandTimeout = (int)(this.m_attributes[ATTR_TIMEOUT]) * 1000;
            return dbCommand;
        }

        /// <inheritDoc />
        public PhpValue exec(string statement)
        {
            this.ClearError();

            var dbCommand = this.CreateCommand(statement);
            try
            {
                int affected = dbCommand.ExecuteNonQuery();
                return PhpValue.Create(affected);
            }
            catch (System.Exception ex)
            {
                //TODO err
                this.HandleError(ex);
                return PhpValue.False;
            }
        }

        /// <inheritDoc />
        public bool beginTransaction()
        {
            if (this.m_tx != null)
                throw new PDOException("Transaction already active");
            //TODO DbTransaction isolation level
            this.m_tx = this.m_con.BeginTransaction();
            return true;
        }

        /// <inheritDoc />
        public bool commit()
        {
            if (this.m_tx == null)
                throw new PDOException("No active transaction");
            this.m_tx.Commit();
            this.m_tx = null;
            return true;
        }

        /// <inheritDoc />
        public bool rollback()
        {
            if (this.m_tx == null)
                throw new PDOException("No active transaction");
            this.m_tx.Rollback();
            this.m_tx = null;
            return true;
        }

        /// <inheritDoc />
        public PhpValue getAttribute(int attribute)
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public string lastInsertId(string name = null)
        {
            return this.m_driver.GetLastInsertId(this, name);
        }

        /// <inheritDoc />
        public PDOStatement prepare(string statement, PhpArray driver_options = null)
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public PDOStatement query(string statement, params PhpValue[] args)
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public string quote(string str, int parameter_type = default(int))
        {
            throw new NotImplementedException();
        }

        private void SetDefaultAttributes()
        {
            this.m_attributes.Set(ATTR_AUTOCOMMIT, true);
            this.m_attributes.Set(ATTR_PREFETCH, 0);
            this.m_attributes.Set(ATTR_TIMEOUT, 30);
            this.m_attributes.Set(ATTR_ERRMODE, ERRMODE_SILENT);
            this.m_attributes.Set(ATTR_SERVER_VERSION, null);
            this.m_attributes.Set(ATTR_CLIENT_VERSION, null);
            this.m_attributes.Set(ATTR_SERVER_INFO, null);
            this.m_attributes.Set(ATTR_CONNECTION_STATUS, null);
            this.m_attributes.Set(ATTR_CASE, null);
            this.m_attributes.Set(ATTR_CURSOR_NAME, null);
            this.m_attributes.Set(ATTR_CURSOR, null);
            this.m_attributes.Set(ATTR_DRIVER_NAME, "");
            this.m_attributes.Set(ATTR_ORACLE_NULLS, null);
            this.m_attributes.Set(ATTR_PERSISTENT, false);
            this.m_attributes.Set(ATTR_STATEMENT_CLASS, null);
            this.m_attributes.Set(ATTR_FETCH_CATALOG_NAMES, null);
            this.m_attributes.Set(ATTR_FETCH_TABLE_NAMES, null);
            this.m_attributes.Set(ATTR_STRINGIFY_FETCHES, null);
            this.m_attributes.Set(ATTR_MAX_COLUMN_LEN, null);
            this.m_attributes.Set(ATTR_DEFAULT_FETCH_MODE, FETCH_USE_DEFAULT);
            this.m_attributes.Set(ATTR_EMULATE_PREPARES, false);
        }

        /// <inheritDoc />
        public bool setAttribute(int attribute, PhpValue value)
        {
            try
            {
                switch (attribute)
                {
                    case ATTR_AUTOCOMMIT:
                        this.m_attributes.Set(attribute, value.ToBoolean());
                        return true;
                    case ATTR_PREFETCH:
                        int prefetchValue = (int)value.ToLong();
                        if (prefetchValue <= 0)
                            throw new ArgumentOutOfRangeException();
                        this.m_attributes.Set(attribute, prefetchValue);
                        return true;
                    case ATTR_TIMEOUT:
                        int timeoutValue = (int)value.ToLong();
                        if (timeoutValue < 0)
                            throw new ArgumentOutOfRangeException();
                        this.m_attributes.Set(attribute, timeoutValue);
                        return true;
                    case ATTR_ERRMODE:
                        int errmodeValue = (int)value.ToLong();
                        if (!new[] { ERRMODE_EXCEPTION, ERRMODE_SILENT, ERRMODE_WARNING }.Contains(errmodeValue))
                            throw new ArgumentOutOfRangeException();
                        this.m_attributes.Set(attribute, errmodeValue);
                        return true;
                }
                if (attribute >= ATTR_DRIVER_SPECIFIC)
                {
                    return this.m_driver.TrySetAttribute(this.m_attributes, attribute, value);
                }
                throw new NotImplementedException();
            }
            catch (System.Exception ex)
            {
                this.HandleError(ex);
                return false;
            }
        }

        #region Interface artifacts
        /// <inheritDoc />
        IPDOStatement IPDO.prepare(string statement, PhpArray driver_options)
        {
            return this.prepare(statement, driver_options);
        }

        /// <inheritDoc />
        IPDOStatement IPDO.query(string statement, params PhpValue[] args)
        {
            return this.query(statement, args);
        }
        #endregion
    }
}
