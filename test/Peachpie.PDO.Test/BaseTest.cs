using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Peachpie.PDO.Test
{
    public abstract class BaseTest
    {
        protected string GetTestScriptContent(string name)
        {
            var input = this.GetType().GetTypeInfo().Assembly.GetManifestResourceStream(name);
            input.Should().NotBeNull("Resource name '{0}' not found", name);

            using (input)
            using (StreamReader sr = new StreamReader(input))
            {
                return sr.ReadToEnd();
            }
        }

        protected void RunTest(string name)
        {
            var script = this.GetTestScriptContent(name);
            script.Should().NotBeNullOrWhiteSpace("Script resource '{0}' is null or empty", name);
            if (string.IsNullOrWhiteSpace(script))
                throw new System.Exception("Invalid or empty script");

            using (var ctx = new TestPhpContext())
            {
                this.RunTest(ctx, script);
            }
        }

        protected virtual void RunTest(TestPhpContext ctx, string script)
        {
            ctx.Eval(script);

            var response = ctx.GetResponse();

            response.Should().ShouldBeEquivalentTo("ok", "Test script should echo 'ok' when passed");
        }
    }
}
