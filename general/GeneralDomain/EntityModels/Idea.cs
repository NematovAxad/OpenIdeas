using System.ComponentModel.DataAnnotations.Schema;

namespace GeneralDomain.EntityModels;

[Table("ideas", Schema ="idea")]
public class Idea
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("user_id")]
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
    
    [Column("is_private")]
    public bool IsPrivate { get; set; }
    
    [Column("title")]
    public string Title { get; set; }
    
    [Column("body")]
    public string Body { get; set; }
    
    [Column("hashtags")]
    public string Hashtags { get; set; }
    
    [Column("create_date")]
    public DateTime CreateDate { get; set; }
    
    [Column("update_date")]
    public DateTime UpdateDate { get; set; }
    
    public IReadOnlyCollection<IdeaComments> Comments { get; set; }
    
    public IReadOnlyCollection<IdeaFiles> Files { get; set; }
    
    public IReadOnlyCollection<IdeaRates> Rates { get; set; }
}