using System.Net;
using GeneralApplication.Interfaces;
using GeneralApplication.Services;
using GeneralDomain.EntityModels;
using GeneralDomain.Responses;
using GeneralInfrastructure.DbContext;
using IdeaApplication.IdeaServices.Interfaces;
using IdeaDomain.CodeModels.Requests;

namespace IdeaApplication.IdeaServices.Services;

public class IdeaCommentService:IIdeaCommentService
{
    private readonly DataContext _dbContext;
    private readonly IGetByIdGlobalService _globalService;

    public IdeaCommentService(DataContext dbContext, IGetByIdGlobalService globalService)
    {
        _dbContext = dbContext;
        _globalService = globalService;
    }
    
    public async Task<Response<bool>> AddComment(AddIdeaCommentRequest request, int userId)
    {
        var user = _globalService.User(userId);


        var idea = _globalService.Idea(request.IdeaId);

        IdeaComments newComment = new IdeaComments()
        {
            IdeaId = idea.Result.Id,
            UserId = user.Result.Id,
            Comment = request.Comment,
            CommentDate = DateTime.Now
        };

        _dbContext.IdeaComments.Add(newComment);
        await _dbContext.SaveChangesAsync();

        return true;
    }
    
    public async Task<Response<bool>> DeleteComment(int commentId, int userId)
    {
        var user = _globalService.User(userId);

        var userComment = _globalService.UserComment(user.Result.Id, commentId);

        _dbContext.IdeaComments.Remove(userComment.Result);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}