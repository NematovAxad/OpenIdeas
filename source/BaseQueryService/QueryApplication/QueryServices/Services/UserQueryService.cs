using System.Net;
using GeneralDomain.EntityModels;
using GeneralDomain.Responses;
using GeneralInfrastructure.DbContext;
using QueryApplication.QueryServices.Interfaces;
using QueryDomain.CodeModels.Responses.UserQueryResponses;

namespace QueryApplication.QueryServices.Services;

public class UserQueryService:IUserQueryService
{
    private readonly DataContext _dbContext;

    public UserQueryService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Response<UserQueryResponse>> GetUser(int id)
    {
        UserQueryResponse response = new UserQueryResponse();
        
        if(id==0)
            return new ErrorResponse(HttpStatusCode.NotAcceptable, "id shouldn't be zero");

        var user = _dbContext.User.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return new ErrorResponse(HttpStatusCode.NotFound, "User not found");

        response.Id = user.Id;
        response.FirstName = user.FirstName;
        response.LastName = user.LastName;
        response.PhotoPath = user.PhotoPath;
        response.Email = user.Email;

        return response;
    }
}