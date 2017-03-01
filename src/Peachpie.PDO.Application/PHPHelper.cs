using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Peachpie.PDO
{
    public static class PHPHelper
    {
        public static void RegisterExtensionAssembly<T>()
        {
            var extentionTableType = typeof(Pchp.Core.Context).GetTypeInfo().Assembly.GetType("Pchp.Core.Reflection.ExtensionsTable", true);
            var addAssemblyMethod = extentionTableType.GetMethod("AddAssembly", new[] { typeof(Assembly) });
            var extensionsAppContextType = typeof(Pchp.Core.Context).GetTypeInfo().Assembly.GetType("Pchp.Core.Reflection.ExtensionsAppContext", true);
            var extensionsTableField = extensionsAppContextType.GetField("ExtensionsTable", BindingFlags.Public | BindingFlags.Static);
            var extensionsTableInstance = extensionsTableField.GetValue(null);
            addAssemblyMethod.Invoke(extensionsTableInstance, new object[] { typeof(T).GetTypeInfo().Assembly });

        }
    }
}
