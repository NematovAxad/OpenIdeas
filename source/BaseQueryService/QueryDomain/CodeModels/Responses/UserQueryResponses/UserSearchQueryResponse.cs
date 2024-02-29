namespace QueryDomain.CodeModels.Responses.UserQueryResponses;

public class UserSearchQueryResponse
{
    public List<UserSearchResultModel>? Users { get; set; }
}

public class UserSearchResultModel
{
    public int Id { get; set; }
    
    public required string Username { get; set; }
}