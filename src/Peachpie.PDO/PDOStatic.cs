using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pchp.Core;

namespace Peachpie.PDO
{
    [PhpExtension]
    public static class PDOStatic
    {
        public static PhpArray pdo_drivers(Context ctx)
        {
            var phpNames = PDOEngine.GetDriverNames().Select(d => PhpValue.Create(d)).ToArray();
            return PhpArray.New(phpNames);
        }
    }
}
