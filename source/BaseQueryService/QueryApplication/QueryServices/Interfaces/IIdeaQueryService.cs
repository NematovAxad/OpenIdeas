using GeneralDomain.Responses;
using QueryDomain.CodeModels.Requests;
using QueryDomain.CodeModels.Responses;
using QueryDomain.CodeModels.Responses.UserQueryResponses;

namespace QueryApplication.QueryServices.Interfaces;

public interface IIdeaQueryService
{
    Task<Response<IdeaQueryResponse>> GetIdeas(IdeaQueryRequest request, int userId);
}