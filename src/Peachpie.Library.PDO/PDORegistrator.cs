using Pchp.Core;
using Pchp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peachpie.Library.PDO
{
    internal sealed class PDORegistrator
    {
        public PDORegistrator()
        {
            Context.RegisterConfiguration(new PDOConfiguration());
        }
    }
}
