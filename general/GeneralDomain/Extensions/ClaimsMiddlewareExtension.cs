using GeneralDomain.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace GeneralDomain.Extensions;

public static class ClaimsMiddlewareExtension
{
    public static IApplicationBuilder UseClaimsMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<IdeaUserAuthorizeMiddleware>();
    }
}