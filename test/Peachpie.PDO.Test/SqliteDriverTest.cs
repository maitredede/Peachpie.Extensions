using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Pchp.Core;

namespace Peachpie.PDO.Test
{
    /// <summary>
    /// SQLite driver tests
    /// </summary>
    /// <seealso cref="Peachpie.PDO.Test.BaseTest" />
    public class SqliteDriverTest : BaseTest
    {
        [Fact]
        public void PdoSqlite_DriverIsRegistered()
        {
            this.RunTest("Peachpie.PDO.Test.SqliteDriver.DriverIsRegistered.php");
        }

        [Fact]
        public void PdoSqlite_CanUseMemoryDSN()
        {
            this.RunTest("Peachpie.PDO.Test.SqliteDriver.CanUseMemoryDSN.php");
        }

        [Fact]
        public void PdoSqlite_ExecVacuum()
        {
            this.RunTest("Peachpie.PDO.Test.SqliteDriver.ExecVacuum.php");
        }
    }
}
