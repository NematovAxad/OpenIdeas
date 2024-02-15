using System.ComponentModel.DataAnnotations.Schema;

namespace GeneralDomain.EntityModels;

[Table("idea_comments", Schema ="idea")]
public class IdeaComments
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
    
    [Column("comment")]
    public string Comment { get; set; }
    
    [Column("comment_date")]
    public DateTime CommentDate { get; set; }
}