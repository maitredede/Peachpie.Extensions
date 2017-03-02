using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhpException = global::Exception;

namespace Peachpie.PDO
{
    /// <summary>
    /// Exception raised by PDO objects
    /// </summary>
    /// <seealso cref="Exception" />
    public class PDOException : PhpException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PDOException"/> class.
        /// </summary>
        /// <param name="message">Message décrivant l'erreur.</param>
        public PDOException(string message) : base(message)
        {
        }
    }
}
