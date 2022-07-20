using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace FocusOn.Framework.Endpoint.HttpProxy.Dynamic;
public class DynamicHttpProxyOptions
{
    public DynamicHttpProxyOptions()
    {

    }

    public string? Name { get; set; } = Options.DefaultName;
}
