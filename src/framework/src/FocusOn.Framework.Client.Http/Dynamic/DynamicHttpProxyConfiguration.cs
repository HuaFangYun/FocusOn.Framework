using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace FocusOn.Framework.Client.Http.Dynamic;
public class DynamicHttpProxyConfiguration
{
    public DynamicHttpProxyConfiguration()
    {

    }
    public readonly static string Default = "FocusOn.DynamicHttpProxyName";
    public string Name { get; set; } = Options.DefaultName;

    public string BaseAddress { get; set; }
}
