using GeneralDomain.Responses;
using QueryDomain.CodeModels.Responses.IdeaQueryResponses;

namespace QueryApplication.QueryServices.Interfaces;

public interface IIdeaQueryService
{
    Task<Response<IdeaQueryResponse>> GetIdeas(int userId);
    
    Task<Response<IdeaQueryResponse>> GetMyIdeas(int userId);
    
    Task<Response<IdeaQueryResponse>> GetMySharedIdeas(int userId);
}