using GeneralDomain.EntityModels;

namespace QueryDomain.CodeModels.Responses.IdeaQueryResponses;

public class IdeaQueryResponse
{
    public List<IdeaQueryResultModel>? Ideas { get; set; }
}

public class IdeaQueryResultModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public required string Title { get; set; }
    
    public required string Body { get; set; }
    
    public required bool IsPrivate { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    public DateTime UpdateDate { get; set; }
    
    public int IdeaRate => (IdeaUpCount-IdeaDownCount);

    public int IdeaUpCount { get; set; }

    public int IdeaDownCount { get; set; }
    
    public ICollection<SharedUser>? SharedUsers { get; set; }
    
    public ICollection<IdeaCommentsQueryResulModel>? Comments { get; set; }
    
    public ICollection<IdeaFilesQueryResulModel>? Files { get; set; }
}

public class IdeaCommentsQueryResulModel
{
    public int Id { get; set; }
    
    public required CommentUser CommentedUser { get; set; }
    
    public required string Comment { get; set; }
    
    public DateTime CommentDate { get; set; }
}

public class IdeaFilesQueryResulModel
{
    public int Id { get; set; }
    public required string FilePath { get; set; }
    public DateTime FileDate { get; set; }
}

public class CommentUser
{
    public required int Id { get; set; }
    
    public required string Username { get; set; }
}

public class SharedUser
{
    public int Id { get; set; }
    
    public required string Username { get; set; }
}
