using System.Text.RegularExpressions;

namespace UserDomain.Extensions;

public static class PasswordValidator
{
    public static bool IsValid(this string password)
    {
        Regex passwordPolicyExpression = new Regex(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#!$%]).{8,20})");
        return passwordPolicyExpression.IsMatch(password);
    }
}