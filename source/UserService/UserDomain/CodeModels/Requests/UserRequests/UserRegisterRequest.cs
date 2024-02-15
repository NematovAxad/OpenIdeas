using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace UserDomain.CodeModels.Requests.UserRequests;

public class UserRegisterRequest
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string UserName { get; set; }
    
    [AllowNull]
    public string? Email { get; set; }
}