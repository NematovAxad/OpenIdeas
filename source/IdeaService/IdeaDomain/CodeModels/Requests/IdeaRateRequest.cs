using System.ComponentModel.DataAnnotations;
using GeneralDomain.Enums;

namespace IdeaDomain.CodeModels.Requests;

public class IdeaRateRequest
{
    [Required]
    public IdeaMark Mark { get; set; }
    
    [Required]
    public int IdeaId { get; set; }
}