using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GeneralDomain;
using GeneralDomain.Configs;
using GeneralDomain.EntityModels;
using Microsoft.IdentityModel.Tokens;

namespace GeneralApplication.Extensions;

public static class UserIdentity
{
    public static ClaimsIdentity GetIdentity(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
            new Claim("userName", user.UserName),
            new Claim("firstName", user.FirstName),
            new Claim("lastName", user.LastName)
        };
        
        if(!String.IsNullOrEmpty(user.PhotoPath))
            claims.Add(new Claim("photo", user.PhotoPath));
        
        if(!String.IsNullOrEmpty(Configs.UserKey))
            claims.Add(new Claim("userKey", Configs.UserKey));
        
        ClaimsIdentity claimsIdentity =

            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
        return claimsIdentity;
    }

    public static string AccessToken(User user)
    {
        var identity = GetIdentity(user);
        var now = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        
        return encodedJwt ?? "";
    }

    public static string RefreshToken(User user)
    {
        user.RefreshToken = Guid.NewGuid().ToString();
        
        return user.RefreshToken;
    }
}