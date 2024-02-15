using System.ComponentModel.DataAnnotations.Schema;

namespace GeneralDomain.EntityModels;

[Table("user", Schema ="user")]
public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("first_name")]
    public string FirstName { get; set; }

    [Column("last_name")]
    public string LastName { get; set; }
    
    [Column("user_name")]
    public string UserName { get; set; }
    
    [Column("passowrd")] 
    public string Password { get; set; }
    
    [Column("email")]
    public string? Email { get; set; }
    
    [Column("photo_path")]
    public string? PhotoPath { get; set; }
    
    [Column("refresh_token")]
    public string? RefreshToken { get; set; }
    
    public IReadOnlyCollection<Idea> Ideas { get; set; }
    public IReadOnlyCollection<IdeaRates> IdeaRates { get; set; }
}