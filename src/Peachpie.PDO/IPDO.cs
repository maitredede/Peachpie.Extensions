using Pchp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peachpie.PDO
{
    /// <summary>
    /// Interface of PDO class
    /// </summary>
    public interface IPDO
    {
        bool beginTransaction();
        bool commit();

        void __construct(string dsn, string username = null, string password = null, PhpArray options = null);
        PhpValue errorCode();
        PhpValue errorInfo();
        PhpValue exec(string statement);
        PhpValue getAttribute(int attribute);
        bool inTransaction();
        string lastInsertId(string name = null);
        IPDOStatement prepare(string statement, PhpArray driver_options = null);
        IPDOStatement query(string statement, params PhpValue[] args);
        string quote(string str, int parameter_type = PDO.PARAM_STR);
        bool rollback();
        bool setAttribute(int attribute, PhpValue value);

    }
}
