using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.AspNetCore.Builder;
public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseFocusOn(this IApplicationBuilder app)
    {
        app.UseSwagger().UseSwaggerUI().UseCors();

        return app;
    }
}
