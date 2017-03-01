using Pchp.Core;
using Pchp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peachpie.PDO
{
    internal sealed class PDORegistrator
    {
        public PDORegistrator()
        {
            Context.RegisterConfiguration(new PDOConfiguration());
        }

        //static PhpValue GetSet(IPhpConfigurationService config, string option, PhpValue value, StandardPhpOptions.IniAction action)
        //{
        //    var local = config.Get<PDOConfiguration>();

        //    throw new NotImplementedException();
        //}
    }
}
