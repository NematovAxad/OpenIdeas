using System.Net;
using GeneralApplication.Interfaces;
using GeneralDomain.EntityModels;
using GeneralDomain.Responses;
using GeneralInfrastructure.DbContext;
using IdeaApplication.IdeaServices.Interfaces;
using IdeaDomain.CodeModels.Requests;

namespace IdeaApplication.IdeaServices.Services;

public class PrivateIdeaService:IPrivateIdeaService
{
    private readonly DataContext _dbContext;
    private readonly IGetByIdGlobalService _globalService;

    public PrivateIdeaService(DataContext dbContext, IGetByIdGlobalService globalService)
    {
        _dbContext = dbContext;
        _globalService = globalService;
    }
    
    public async Task<Response<bool>> AddSharedUser(AddSharedUserRequest request, int userId)
    {
        var user = _globalService.User(userId);

        var idea = _globalService.UserIdea(user.Result.Id, request.IdeaId);

        List<SharedIdeas> listToAdd = new List<SharedIdeas>();
        foreach (var id in request.UsersId)
        {
            var sharedUser = _globalService.User(id);

            SharedIdeas shared = new SharedIdeas()
            {
                IdeaId = idea.Result.Id,
                UserId = sharedUser.Result.Id,
                SharedDate = DateTime.Now
            };
            
            listToAdd.Add(shared);
        }

        _dbContext.SharedIdeas.AddRange(listToAdd);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<Response<bool>> DeleteSharedUser(DeleteSharedUserRequest request, int userId)
    {
        var user = _globalService.User(userId);

        var idea = _globalService.UserIdea(user.Result.Id, request.IdeaId);

        var sharedUser =
            _dbContext.SharedIdeas.FirstOrDefault(s => s.UserId == request.SharedUserId && s.IdeaId == idea.Result.Id);
        
        if(sharedUser==null)
            return new ErrorResponse(HttpStatusCode.NotFound, "UserNotFound");

        _dbContext.SharedIdeas.Remove(sharedUser);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}