using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pchp.Core;

namespace Peachpie.PDO
{
    /// <summary>
    /// PDO static functions
    /// </summary>
    [PhpExtension]
    public static class PDOStatic
    {
        /// <summary>
        /// Get the known PDO drivers
        /// </summary>
        /// <param name="ctx">The php context.</param>
        /// <returns></returns>
        public static PhpArray pdo_drivers(Context ctx)
        {
            var phpNames = PDOEngine.GetDriverNames().Select(d => PhpValue.Create(d)).ToArray();
            return PhpArray.New(phpNames);
        }
    }
}
