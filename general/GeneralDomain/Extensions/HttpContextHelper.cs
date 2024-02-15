using Microsoft.AspNetCore.Http;

namespace GeneralDomain.Extensions;

public class HttpContextHelper
{
    public static IHttpContextAccessor Accessor;
    
    public static HttpContext Current => Accessor?.HttpContext;
}