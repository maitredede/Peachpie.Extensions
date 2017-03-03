using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Pchp.Core;

namespace Peachpie.Library.PDO
{
    /// <summary>
    /// PDOStatement class
    /// </summary>
    /// <seealso cref="IPDOStatement" />
    public class PDOStatement : IPDOStatement, IDisposable
    {
        private readonly PDO m_pdo;
        private readonly string m_stmt;
        private readonly PhpArray m_options;

        private DbCommand m_cmd;
        private DbDataReader m_dr;


        /// <summary>
        /// Initializes a new instance of the <see cref="PDOStatement" /> class.
        /// </summary>
        /// <param name="pdo">The pdo.</param>
        /// <param name="statement">The statement.</param>
        /// <param name="driver_options">The driver options.</param>
        internal PDOStatement(PDO pdo, string statement, PhpArray driver_options)
        {
            this.m_pdo = pdo;
            this.m_stmt = statement;
            this.m_options = driver_options ?? PhpArray.Empty;
        }

        /// <inheritDoc />
        void IDisposable.Dispose()
        {
            this.m_dr?.Dispose();
            this.m_cmd?.Dispose();
        }

        private void OpenReader()
        {
            if (this.m_cmd == null)
            {
                this.m_cmd = this.m_pdo.CreateCommand(this.m_stmt);
            }
            if (this.m_dr == null)
            {
                this.m_dr = this.m_cmd.ExecuteReader();
            }
        }

        /// <inheritDoc />
        public bool bindColumn(PhpValue colum, ref PhpValue param, int? type = default(int?), int? maxlen = default(int?), PhpValue? driverdata = default(PhpValue?))
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public bool bindParam(PhpValue parameter, ref PhpValue variable, int data_type = 2, int? length = default(int?), PhpValue? driver_options = default(PhpValue?))
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public bool bindValue(PhpValue parameter, PhpValue value, int data_type = 2)
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public bool closeCursor()
        {
            if (this.m_dr != null)
            {
                ((IDisposable)this.m_dr).Dispose();
                this.m_dr = null;
                return true;
            }
            return false;
        }

        /// <inheritDoc />
        public int columnCount()
        {
            if (this.m_dr == null)
            {
                return 0;
            }

            return this.m_dr.FieldCount;
        }

        /// <inheritDoc />
        public void debugDumpParams()
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public string errorCode()
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public PhpArray errorInfo()
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public bool execute(PhpArray input_parameters = null)
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public PhpValue fetch(int? fetch_style = default(int?), int cursor_orientation = default(int), int cursor_offet = 0)
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public PhpArray fetchAll(int? fetch_style = default(int?), PhpValue? fetch_argument = default(PhpValue?), PhpArray ctor_args = null)
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public PhpValue fetchColumn(int column_number = 0)
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public PhpValue fetchObject(string class_name = "stdClass", PhpArray ctor_args = null)
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public PhpValue getAttribute(int attribute)
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public PhpArray getColumnMeta(int column)
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public bool nextRowset()
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public int rowCount()
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public bool setAttribute(int attribute, PhpValue value)
        {
            throw new NotImplementedException();
        }

        /// <inheritDoc />
        public bool setFetchMode(int mode, PhpValue param1, PhpValue param2)
        {
            throw new NotImplementedException();
        }
    }
}
