using System.Net;
using GeneralApplication.Interfaces;
using GeneralDomain.EntityModels;
using GeneralDomain.Responses;
using GeneralInfrastructure.DbContext;
using QueryApplication.QueryServices.Interfaces;
using QueryDomain.CodeModels.Responses.UserQueryResponses;

namespace QueryApplication.QueryServices.Services;

public class UserQueryService:IUserQueryService
{
    private readonly DataContext _dbContext;
    private readonly IGetByIdGlobalService _globalService;

    public UserQueryService(DataContext dbContext, IGetByIdGlobalService globalService)
    {
        _dbContext = dbContext;
        _globalService = globalService;
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

    public async Task<Response<UserSearchQueryResponse>> SearchUser(int userId, string text)
    {
        UserSearchQueryResponse result = new UserSearchQueryResponse() { Users = new List<UserSearchResultModel>()};
        
        var searchingUser = _globalService.User(userId);

        var users = _dbContext.User
            .Where(u => u.UserName.ToLower().Contains(text.ToLower()) && u.Id != searchingUser.Result.Id).ToList();

        foreach (var user in users)
        {
            UserSearchResultModel foundUser = new UserSearchResultModel()
            {
                Id = user.Id,
                Username = user.UserName
            };
            
            result.Users.Add(foundUser);
        }

        return result;
    }
}