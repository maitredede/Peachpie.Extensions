using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Peachpie.PDO
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
            string PdoLib = typeof(PDODriverAssemblyAttribute).GetTypeInfo().Assembly.GetName().Name;
            var driverTypes = new List<Type>();
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
                            if(asmTypeInfo.IsClass && !asmTypeInfo.IsAbstract && typeof(IPDODriver).IsAssignableFrom(asmType) && asmTypeInfo.GetConstructor(Type.EmptyTypes) != null)
                            {
                                driverTypes.Add(asmType);
                            }
                        }
                    }
                }
            }
            //var driverTypes =
            //    //Get all libs referencing PDO
            //    from libs in DependencyContext.Default.RuntimeLibraries
            //    where libs.Dependencies.Any(d => d.Name == PdoLib)
            //    from asmName in libs.Assemblies
            //        //Load assembly
            //    let asm = Assembly.Load(asmName.Name)
            //    //Take only PDODriver assemblies
            //    where asm.GetCustomAttribute<PDODriverAssemblyAttribute>() != null
            //    from asmType in asm.GetTypes()
            //    let asmTypeInfo = asmType.GetTypeInfo()
            //    //Get IPDODriver implementations
            //    where asmTypeInfo.IsClass && !asmTypeInfo.IsAbstract && typeof(IPDODriver).IsAssignableFrom(asmType) && asmType.GetConstructor(Type.EmptyTypes) != null
            //    select asmType;

            var method = typeof(PDOEngine).GetMethod(nameof(PDOEngine.RegisterDriver));
            foreach (var type in driverTypes)
            {
                var registerDriver = method.MakeGenericMethod(type);
                registerDriver.Invoke(null, null);
            }
        }
    }
}
