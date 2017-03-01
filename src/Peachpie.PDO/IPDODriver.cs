using Pchp.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Peachpie.PDO
{
    public interface IPDODriver
    {
        string Name { get; }
        Dictionary<string, ExtensionMethodDelegate> GetPDObjectExtensionMethods();

        DbConnection OpenConnection(string dsn, string user, string password, PhpArray options);
        string GetLastInsertId(PDO pDO, string name);
        bool TrySetAttribute(Dictionary<int, object> attributes, int attribute, PhpValue value);
    }
}
