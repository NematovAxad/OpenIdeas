using System.ComponentModel.DataAnnotations.Schema;

namespace GeneralDomain.EntityModels;

[Table("shared_ideas", Schema ="user")]
public class SharedIdeas
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("idea_id")]
    [ForeignKey("Idea")]
    public int IdeaId { get; set; }
    public Idea Idea { get; set; }
    
    [Column("user_id")]
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
    
    [Column("shared_date")]
    public DateTime SharedDate { get; set; }
}