using System.Net;
using GeneralApplication.Interfaces;
using GeneralDomain.EntityModels;
using GeneralDomain.Responses;
using GeneralInfrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace GeneralApplication.Services;

public class GetByIdGlobalService:IGetByIdGlobalService
{
    private readonly DataContext _dbContext;

    public GetByIdGlobalService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Response<User> User(int id)
    {
        var user = _dbContext.User.Where(u => u.Id == id)
            .Include(u => u.IdeaRates)
            .Include(u => u.Ideas)
            .FirstOrDefault();

        if(user==null)
            return new ErrorResponse(HttpStatusCode.NotFound, "UserNotFound");
        
        return user;
    }

    public Response<Idea> Idea(int id)
    {
        var idea = _dbContext.Idea.FirstOrDefault(i => i.Id == id);

        if(idea==null)
            return new ErrorResponse(HttpStatusCode.NotFound, "IdeaNotFound");
        
        return idea;
    }

    public Response<Idea> UserIdea(int userId, int ideaId)
    {
        var idea = _dbContext.Idea.FirstOrDefault(i => i.Id == ideaId && i.UserId == userId);

        if (idea == null)
            return new ErrorResponse(HttpStatusCode.NotFound, "IdeaNotFound");
        
        return idea;
    }
    
    public Response<IdeaComments> Comment(int id)
    {
        var comment = _dbContext.IdeaComments.FirstOrDefault(c => c.Id == id);
        
        if(comment==null)
            return new ErrorResponse(HttpStatusCode.NotFound, "CommentNotFound");
        
        return comment;
    }

    public Response<IdeaComments> UserComment(int userId, int commentId)
    {
        var userComment = _dbContext.IdeaComments.FirstOrDefault(c => c.Id == commentId && c.UserId == userId);
        
        if(userComment==null)
            return new ErrorResponse(HttpStatusCode.NotFound, "UserCommentNotFound");
        
        return userComment;
    }
}