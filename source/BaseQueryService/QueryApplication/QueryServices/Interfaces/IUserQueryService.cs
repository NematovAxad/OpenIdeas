using GeneralDomain.Responses;
using QueryDomain.CodeModels.Responses.UserQueryResponses;

namespace QueryApplication.QueryServices.Interfaces;

public interface IUserQueryService
{
    Task<Response<UserQueryResponse>> GetUser(int id);
}