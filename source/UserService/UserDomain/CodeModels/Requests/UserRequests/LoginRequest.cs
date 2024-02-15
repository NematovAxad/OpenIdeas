using System.ComponentModel.DataAnnotations;

namespace UserDomain.CodeModels.Requests.UserRequests;

public class LoginRequest
{
    [Required]
    public required string UserName { get; set; }
    
    [Required]
    public required string Password { get; set; }
}