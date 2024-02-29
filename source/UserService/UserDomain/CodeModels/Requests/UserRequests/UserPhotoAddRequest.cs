using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace UserDomain.CodeModels.Requests.UserRequests;

public class UserPhotoAddRequest
{
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public int UserId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public IFormFile Photo { get; set; }
}