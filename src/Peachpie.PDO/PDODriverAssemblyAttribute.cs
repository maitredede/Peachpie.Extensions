using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peachpie.PDO
{
    /// <summary>
    /// Tag the assembly as containing PDO drivers
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public sealed class PDODriverAssemblyAttribute : Attribute
    {
    }
}
