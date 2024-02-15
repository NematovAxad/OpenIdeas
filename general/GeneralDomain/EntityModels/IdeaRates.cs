using System.ComponentModel.DataAnnotations.Schema;
using GeneralDomain.Enums;

namespace GeneralDomain.EntityModels;

[Table("idea_rates", Schema ="idea")]
public class IdeaRates
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
    
    [Column("idea_mark")]
    public IdeaMark IdeaMark { get; set; }
    
    [Column("rate_date")]
    public DateTime RateDate { get; set; }
}