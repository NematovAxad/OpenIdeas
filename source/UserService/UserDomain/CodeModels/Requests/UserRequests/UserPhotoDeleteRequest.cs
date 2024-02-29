using System.ComponentModel.DataAnnotations;

namespace UserDomain.CodeModels.Requests.UserRequests;

public class UserPhotoDeleteRequest
{
    [Required]
    public int UserId { get; set; }
}