using Pchp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peachpie.Library.PhpInfo
{
    internal static class HtmlExtensions
    {
        public static HtmlTagWriter Tag(this Context ctx, string tag, object attributes = null)
        {
            return new HtmlTagWriter(ctx, tag, attributes);
        }

        public static HtmlTagWriter Tag(this HtmlTagWriter tagWriter, string tag, object attributes = null)
        {
            return new HtmlTagWriter(tagWriter.Context, tag, attributes);
        }
    }
}
