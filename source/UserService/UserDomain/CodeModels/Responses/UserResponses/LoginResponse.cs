namespace UserDomain.CodeModels.Responses.UserResponses;

public class LoginResponse
{
    public bool IsSuccess { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string AccessToken { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string RefreshToken { get; set; }
}