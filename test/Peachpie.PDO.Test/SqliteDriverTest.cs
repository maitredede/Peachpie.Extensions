using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Pchp.Core;

namespace Peachpie.PDO.Test
{
    public class SqliteDriverTest : BaseTest
    {
        [Fact]
        public void PdoSqlite_CanUseMemoryDSN()
        {
            this.RunTest("Peachpie.PDO.Test.SqliteDriver.CanUseMemoryDSN.php");
        }
    }

    //public class TestPhpContext : Context, IHttpPhpContext
    //{
    //    public void EvalScript(string phpScript)
    //    {
    //        ScriptInfo info  =new ScriptInfo()
    //        {

    //        }
    //    }
    //}
}
