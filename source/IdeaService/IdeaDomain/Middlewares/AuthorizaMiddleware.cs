using Microsoft.AspNetCore.Http;

namespace IdeaDomain.Middlewares;

public class AuthorizaMiddleware
{
    private readonly RequestDelegate _next;

    public AuthorizaMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        
        await _next(context);
    }
}