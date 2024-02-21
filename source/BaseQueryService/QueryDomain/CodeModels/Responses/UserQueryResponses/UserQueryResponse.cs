namespace QueryDomain.CodeModels.Responses.UserQueryResponses;

public class UserQueryResponse
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string? Email { get; set; }
    
    public string? PhotoPath { get; set; }
}