using Pchp.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Peachpie.PDO
{
    /// <summary>
    /// Interface of a PDO driver
    /// </summary>
    public interface IPDODriver
    {
        /// <summary>
        /// Gets the driver name (used in DSN)
        /// </summary>
        string Name { get; }
      
        /// <summary>
        /// Gets the methods added to the PDO instance when this driver is used.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, ExtensionMethodDelegate> GetPDObjectExtensionMethods();

        /// <summary>
        /// Opens a new database connection.
        /// </summary>
        /// <param name="dsn">The DSN.</param>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        DbConnection OpenConnection(string dsn, string user, string password, PhpArray options);
       
        /// <summary>
        /// Gets the last insert identifier.
        /// </summary>
        /// <param name="pDO">The p do.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        string GetLastInsertId(PDO pDO, string name);

        /// <summary>
        /// Tries to set a driver specific attribute value.
        /// </summary>
        /// <param name="attributes">The current attributes collection.</param>
        /// <param name="attribute">The attribute to set.</param>
        /// <param name="value">The value.</param>
        /// <returns>true if value is valid, or false if value can't be set.</returns>
        bool TrySetAttribute(Dictionary<int, object> attributes, int attribute, PhpValue value);
    }
}
