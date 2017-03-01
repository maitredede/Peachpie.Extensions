using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Peachpie.Web;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace Peachpie.PDO.Test
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            var phpOptions = new PhpRequestOptions
            {
                ScriptAssembliesName = new[]
                {
                    "Peachpie.PDO.Test.Website"
                }
            };

            ////HACK to register PHP extension parts not used by scripts
            ////DependencyContext.Default.CompileLibraries
            //// Default DependencyContext is retrieved from entry assembly
            //var deps = DependencyContext.Default;
            //Console.WriteLine($"Compilation depenencies");
            //foreach (var compilationLibrary in deps.CompileLibraries)
            //{
            //    Console.WriteLine($"\tPackage {compilationLibrary} {compilationLibrary.Version}");
            //    // ResolveReferencePaths returns full paths to compilation assemblies
            //    foreach (var resolveReferencePath in compilationLibrary.ResolveReferencePaths())
            //    {
            //        Console.WriteLine($"\t\tReference path: {resolveReferencePath}");
            //    }
            //}

            //Console.WriteLine($"Runtime depenencies");
            //foreach (var compilationLibrary in deps.RuntimeLibraries)
            //{
            //    Console.WriteLine($"\tPackage {compilationLibrary} {compilationLibrary.Version}");
            //    foreach (var assembly in compilationLibrary.Assemblies)
            //    {
            //        Console.WriteLine($"\t\tReference: {assembly.Name}");
            //    }
            //}

            //PHPHelper.RegisterExtensionAssembly<MySQL.PDOMySQLDriver>();
            //PHPHelper.RegisterExtensionAssembly<Sqlite.PDOSqliteDriver>();
            //PHPHelper.RegisterExtensionAssembly<Pgsql.PDONpgsqlDriver>();

            PDOHelper.RegisterAllDrivers();

            app.UsePhp(phpOptions);
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
