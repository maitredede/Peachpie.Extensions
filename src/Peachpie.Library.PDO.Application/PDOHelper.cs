using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Peachpie.Library.PDO
{
    /// <summary>
    /// Helper class
    /// </summary>
    public static class PDOHelper
    {
        /// <summary>
        /// Registers all referenced PDO drivers.
        /// </summary>
        public static void RegisterAllDrivers()
        {
            //Find all assemblies referencing the PDO library and tagged with PDODriverAssemblyAttribute
            //Drivers must implement IPDODriver
            string PdoLib = typeof(PDODriverAssemblyAttribute).GetTypeInfo().Assembly.GetName().Name;
            Type iDriver = typeof(IPDODriver);
            var driverTypes = new List<Type>();

            //Seach in all assemblies
            foreach (var lib in DependencyContext.Default.RuntimeLibraries)
            {
                if (lib.Dependencies.Any(d => d.Name == PdoLib))
                {
                    var asm = Assembly.Load(new AssemblyName(lib.Name));
                    if (asm.GetCustomAttribute<PDODriverAssemblyAttribute>() != null)
                    {
                        foreach (var asmType in asm.GetTypes())
                        {
                            var asmTypeInfo = asmType.GetTypeInfo();
                            if(asmTypeInfo.IsClass && !asmTypeInfo.IsAbstract && iDriver.IsAssignableFrom(asmType) && asmTypeInfo.GetConstructor(Type.EmptyTypes) != null)
                            {
                                driverTypes.Add(asmType);
                            }
                        }
                    }
                }
            }

            //Register the found drivers
            var method = typeof(PDOEngine).GetMethod(nameof(PDOEngine.RegisterDriver));
            foreach (var type in driverTypes)
            {
                var registerDriver = method.MakeGenericMethod(type);
                registerDriver.Invoke(null, null);
            }
        }
    }
}
