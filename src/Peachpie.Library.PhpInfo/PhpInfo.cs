using Pchp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Peachpie.Library.PhpInfo
{
    [PhpExtension]
    public static class PhpInfo
    {
        public static void phpinfo(Context ctx)
        {
            ctx.Echo(@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""DTD/xhtml1-transitional.dtd"">");
            using (var html = ctx.Tag("html", new { xmlns = "http://www.w3.org/1999/xhtml" }))
            {
                using (var head = html.Tag("head"))
                {
                    using (var style = head.Tag("style", new { type = "text/css" }))
                    {
                        style.EchoRaw(Resource.Style);
                    }
                    head.EchoTag("title", "phpinfo()");
                    head.EchoTagSelf("meta", new { name = "ROBOTS", content = "NOINDEX,NOFOLLOW,NOARCHIVE" });
                }
                using (var body = html.Tag("body"))
                using (var center = body.Tag("div", new { @class = "center" }))
                {
                    PageTitle(center);
                    Heading(center);
                    center.EchoTagSelf("hr");
                    center.EchoTag("h1", "Configuration");
                    Configuration(center);
                    Env(center);
                    Variables(center);
                    center.EchoTagSelf("hr");
                    center.EchoTag("h1", "Credits");
                    Credits(center);
                }
            }
        }

        private static void PageTitle(HtmlTagWriter container)
        {
            using (var table = container.Tag("table"))
            using (var tr = table.Tag("tr", new { @class = "h" }))
            using (var td = tr.Tag("td"))
            {
                using (var a = td.Tag("a", new { href = Resource.LogoHref, target = "_blank" }))
                {
                    a.EchoTagSelf("img", new { border = "0", src = Resource.LogoSrc, alt = Resource.LogoAlt });
                }
                using (var title = td.Tag("h1", new { @class = "p" }))
                {
                    title.EchoEscaped("PeachPie Version " + typeof(Context).GetTypeInfo().Assembly.GetName().Version);
                }
            }
        }

        private static void Heading(HtmlTagWriter container)
        {
            using (var table = container.Tag("table"))
            {
                Action<string, string> Line = (name, value) =>
                {
                    using (var tr = table.Tag("tr"))
                    {
                        tr.EchoTag("td", name, new { @class = "e" });
                        tr.EchoTag("td", value, new { @class = "v" });
                    }
                };

                Line("System", $"{GetOsName()} {Environment.MachineName} {RuntimeInformation.OSDescription} {RuntimeInformation.OSArchitecture}");
                Line("Architecture", RuntimeInformation.ProcessArchitecture.ToString());
                Line("Debug build",
#if DEBUG
                    true
#else
                    false
#endif
                    ? "yes":"no");
                Line("IPv6 Support", System.Net.Sockets.Socket.OSSupportsIPv6 ? "yes" : "no");
            }
        }

        private static string GetOsName()
        {
            foreach (var osDesc in typeof(OSPlatform).GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Static).Where(p => p.PropertyType == typeof(OSPlatform)).Select(p => new { Prop = p, Val = (OSPlatform)p.GetValue(null) }))
            {
                if (RuntimeInformation.IsOSPlatform(osDesc.Val))
                {
                    return osDesc.Val.ToString();
                }
            }
            return "Unknown";
        }

        private static void Configuration(HtmlTagWriter container)
        {
            //TODO : get extensions and dump configuration
        }

        private static void Env(HtmlTagWriter container)
        {
            container.EchoTag("h2", "Environment");
            using (var table = container.Tag("table"))
            {
                using (var tr = table.Tag("tr", new { @class = "h" }))
                {
                    tr.EchoTag("th", "Variable");
                    tr.EchoTag("th", "Value");
                }

                Action<string, string> Line = (name, value) =>
                {
                    using (var tr = table.Tag("tr", new { @class = "h" }))
                    {
                        tr.EchoTag("td", name, new { @class = "e" });
                        using (var td = tr.Tag("td", new { @class = "v" }))
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                td.EchoRaw("&nbsp;");
                            }
                            else
                            {
                                td.EchoEscaped(value);
                            }
                        }
                    }
                };

                foreach (var entry in container.Context.Env.Keys)
                {
                    Line(entry.ToString(), container.Context.Env[entry].ToStringOrNull());
                }
            }
        }

        private static void Variables(HtmlTagWriter container)
        {
            container.EchoTag("h2", "PHP Variables");
            using (var table = container.Tag("table"))
            {
                using (var tr = table.Tag("tr", new { @class = "h" }))
                {
                    tr.EchoTag("th", "Variable");
                    tr.EchoTag("th", "Value");
                }

                Action<string, string> Line = (name, value) =>
                {
                    using (var tr = table.Tag("tr", new { @class = "h" }))
                    {
                        tr.EchoTag("td", name, new { @class = "e" });
                        using (var td = tr.Tag("td", new { @class = "v" }))
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                td.EchoRaw("&nbsp;");
                            }
                            else
                            {
                                td.EchoEscaped(value);
                            }
                        }
                    }
                };

                Action<PhpArray, string> DumpArray = (arr, name) =>
                {
                    foreach (var entry in arr.Keys)
                    {
                        Line($"{name}[{entry}]", arr[entry].ToStringOrNull());
                    }
                };

                DumpArray(container.Context.Cookie, "_COOKIE");
                DumpArray(container.Context.Server, "_SERVER");
            }
        }

        private static void Credits(HtmlTagWriter container)
        {
            //TODO creditz
        }
    }
}
