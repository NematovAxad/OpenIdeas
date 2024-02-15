using System.ComponentModel.DataAnnotations.Schema;

namespace GeneralDomain.EntityModels;

[Table("idea_files", Schema ="idea")]
public class IdeaFiles
{
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("idea_id")]
    [ForeignKey("Idea")]
    public int IdeaId { get; set; }
    public Idea Idea { get; set; }
    
    [Column("file_path")]
    public string FilePath { get; set; }
    
    [Column("file_date")]
    public DateTime FileDate { get; set; }
    
}