using Pchp.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Peachpie.PDO.Test
{
    public class TestPhpContext : Context
    {
        private readonly MemoryStream m_output;
        private readonly TextWriter m_outputWriter;

        public TestPhpContext()
        {
            this.m_output = new MemoryStream();
            this.m_outputWriter = new StreamWriter(this.m_output, System.Text.Encoding.UTF8, 4096, true)
            {
                AutoFlush = true
            };
            this.InitOutput(this.m_output, this.m_outputWriter);
            this.InitSuperglobals();
        }

        public void Eval(string code)
        {
            throw new NotImplementedException("Eval not implemented in Peachpie. See https://github.com/iolevel/peachpie/wiki/Peachpie-Roadmap");
        }

        public string GetResponse()
        {
            using (MemoryStream ms = new MemoryStream(this.m_output.ToArray()))
            using (var sr = new StreamReader(ms, this.m_outputWriter.Encoding, true, 4096, true))
            {
                return sr.ReadToEnd();
            }
        }

        public override void Dispose()
        {
            this.m_output.Dispose();
            base.Dispose();
        }
    }
}
