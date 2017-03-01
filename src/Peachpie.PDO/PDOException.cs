using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhpException = global::Exception;

namespace Peachpie.PDO
{
    public class PDOException : PhpException
    {
        public PDOException(string message) : base(message)
        {
        }
    }
}
