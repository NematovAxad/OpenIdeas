using Microsoft.AspNetCore.Mvc;

namespace GeneralApplication.Extensions;

public static class UserTokenData
{
    public static int UserId(this ControllerBase cBase)
    {
        var value = cBase.User.FindFirst(m => m.Type.ToLower() == "id")?.Value;
        if (string.IsNullOrEmpty(value))
        {
            return 0;
        }
        return int.Parse(value);
    }
    
    public static string UserName(this ControllerBase cBase)
    {
        var value = cBase.User.FindFirst(m => m.Type.ToLower() == "userName")?.Value;
        if (string.IsNullOrEmpty(value)) { return String.Empty; }
        return value;
    }
}