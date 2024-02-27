using GeneralDomain.EntityModels;

namespace QueryDomain.CodeModels.Responses;

public class IdeaQueryResponse
{
    public List<IdeaQueryResultModel> Ideas { get; set; }
}

public class IdeaQueryResultModel
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
    
    public IReadOnlyCollection<IdeaCommentsQueryResulModel> Comments { get; set; }
    
    public IReadOnlyCollection<IdeaFilesQueryResulModel> Files { get; set; }
}

public class IdeaCommentsQueryResulModel
{
    public int Id { get; set; }
    
    public User CommentedUser { get; set; }
    
    public string Comment { get; set; }
    
    public DateTime CommentDate { get; set; }
}

public class IdeaFilesQueryResulModel
{
    public int Id { get; set; }
    public string FilePath { get; set; }
    public DateTime FileDate { get; set; }
}
