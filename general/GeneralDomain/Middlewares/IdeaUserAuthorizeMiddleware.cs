using Microsoft.AspNetCore.Http;

namespace GeneralDomain.Middlewares;

public class IdeaUserAuthorizeMiddleware
{
    private readonly RequestDelegate _next;

    public IdeaUserAuthorizeMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        if (!context.User.HasClaim(c => c.Type == "userKey" && c.Value == "OpenIdeasuserSecretKey12#"))
        {
            context.Response.StatusCode = 403; // Forbidden
            await context.Response.WriteAsync("You do not have permission to access this resource.");
            return;
        }

        await _next(context);
    }
}