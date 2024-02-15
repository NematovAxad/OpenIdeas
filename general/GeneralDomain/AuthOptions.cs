using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GeneralDomain;

public class AuthOptions
{
    public const string Issuer = "MyAuthServer"; // издатель токена
    public const string Audience = "MyAuthClient"; // потребитель токена
    private const string Key = "my_super_super_secret_secret_key!123";   // ключ для шифрации
    public const int Lifetime = 9999; // время жизни токена - 1 минута
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
    public const string FilePath = "";
}