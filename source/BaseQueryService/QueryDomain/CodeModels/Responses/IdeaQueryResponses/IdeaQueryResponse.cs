using GeneralDomain.EntityModels;

namespace QueryDomain.CodeModels.Responses;

public class IdeaQueryResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    
    public string Body { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    public DateTime UpdateDate { get; set; }
    
    public int IdeaRate { get; set; }
    
    public int IdeaUpCount { get; set; }
    
    public int IdeaDownCount { get; set; }
    
    public IReadOnlyCollection<IdeaCommentsQueryModel> Comments { get; set; }
    
    public IReadOnlyCollection<IdeaFilesQueryModel> Files { get; set; }
}

public class IdeaCommentsQueryModel
{
    public int Id { get; set; }
    
    public User CommentedUser { get; set; }
    
    public string Comment { get; set; }
    
    public DateTime CommentDate { get; set; }
}

public class IdeaFilesQueryModel
{
    public int Id { get; set; }
    public string FilePath { get; set; }
    public DateTime FileDate { get; set; }
}
