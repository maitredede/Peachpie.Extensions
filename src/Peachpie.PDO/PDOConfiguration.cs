using Pchp.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace Peachpie.PDO
{
    public class PDOConfiguration : IPhpConfiguration
    {
        public IPhpConfiguration Copy() => (PDOConfiguration)this.MemberwiseClone();

        //public NameValueCollection Alias { get; set; } = new NameValueCollection();

    }
}
