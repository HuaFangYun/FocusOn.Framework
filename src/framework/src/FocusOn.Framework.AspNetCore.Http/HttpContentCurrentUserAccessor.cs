using System.Security.Claims;
using FocusOn.Framework.Modules;
using Microsoft.AspNetCore.Http;
using FocusOn.Framework.Security;

namespace FocusOn.Framework.AspNetCore.Http;
internal class HttpContentCurrentUserAccessor : ThreadCurrentPrincipalAccessor
{
    private readonly IHttpContextAccessor context;

    public HttpContentCurrentUserAccessor(IHttpContextAccessor context)
    {
        this.context = context;
    }

    protected override ClaimsPrincipal GetClaimsPrincipal()
    {
        return context.HttpContext?.User ?? base.GetClaimsPrincipal();
    }
}
