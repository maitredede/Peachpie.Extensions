using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Peachpie.Library.PDO;

namespace Peachpie.PDO.Test
{
    /// <summary>
    /// Base class for tests
    /// </summary>
    public abstract class BaseTest
    {
        /// <summary>
        /// Initializes the <see cref="BaseTest"/> class.
        /// </summary>
        static BaseTest()
        {
            PDOHelper.RegisterAllDrivers();
        }

        /// <summary>
        /// Gets the content of the test script.
        /// </summary>
        /// <param name="name">The resource name containing the php script test.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Runs the test.
        /// </summary>
        /// <param name="name">The resource name containing the php script test.</param>
        /// <exception cref="System.Exception">Invalid or empty script</exception>
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

        /// <summary>
        /// Runs the test.
        /// </summary>
        /// <param name="ctx">The php context.</param>
        /// <param name="script">The script content.</param>
        protected virtual void RunTest(TestPhpContext ctx, string script)
        {
            ctx.Eval(script);

            var response = ctx.GetResponse();

            response.Should().ShouldBeEquivalentTo("ok", "Test script should echo 'ok' when passed");
        }
    }
}
