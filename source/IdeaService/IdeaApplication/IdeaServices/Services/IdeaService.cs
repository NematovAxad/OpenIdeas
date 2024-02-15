using System.Net;
using GeneralApplication.Extensions;
using GeneralDomain.EntityModels;
using GeneralDomain.Responses;
using GeneralInfrastructure.DbContext;
using IdeaApplication.IdeaServices.Interfaces;
using IdeaDomain.CodeModels.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace IdeaApplication.IdeaServices.Services;

public class IdeaService:IIdeaService
{
    private readonly DataContext _dbContext;

    public IdeaService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Response<bool>> AddNewIdea(IdeaAddRequest request, int userId)
    {
        var user = _dbContext.User.FirstOrDefault(u => u.Id == userId);

        if (user == null)
            return new ErrorResponse(HttpStatusCode.Unauthorized, "User not found!");

        Idea newIdea = new Idea()
        {
            UserId = userId,
            CreateDate = DateTime.Now,
            UpdateDate = DateTime.Now
        };
        if(String.IsNullOrEmpty(request.Body) || String.IsNullOrEmpty(request.Title))
            return new ErrorResponse(HttpStatusCode.BadRequest, "Title or Body is empty");
        newIdea.Title = request.Title;
        newIdea.Body = request.Body;
        _dbContext.Add(newIdea);
        await _dbContext.SaveChangesAsync();

        if(request.Files != null && request.Files.Any())
            await AddIdeaFiles(request.Files, newIdea.Id);
        
        return true;
    }

    public async Task<Response<bool>> EditIdea(IdeaEditRequest request, int userId)
    {
        var user = _dbContext.User.FirstOrDefault(u => u.Id == userId);

        if (user == null)
            return new ErrorResponse(HttpStatusCode.Unauthorized, "User not found");

        var idea = _dbContext.Idea.FirstOrDefault(i => i.Id == request.IdeaId && i.UserId == userId);
        if (idea == null)
            return new ErrorResponse(HttpStatusCode.NotFound, "Idea not found");

        if (!String.IsNullOrEmpty(request.Title))
            idea.Title = request.Title;

        if (!String.IsNullOrEmpty(request.Body))
            idea.Body = request.Body;
        
        idea.UpdateDate = DateTime.Now;

        _dbContext.Idea.Update(idea);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<Response<bool>> DeleteIdea(int ideaId, int userId)
    {
        var user = _dbContext.User.FirstOrDefault(u => u.Id == userId);

        if (user == null)
            return new ErrorResponse(HttpStatusCode.Unauthorized, "User not found");

        var idea = _dbContext.Idea.FirstOrDefault(i => i.Id == ideaId && i.UserId == userId);
        if (idea == null)
            return new ErrorResponse(HttpStatusCode.NotFound, "Idea not found");

        _dbContext.Idea.Remove(idea);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<Response<bool>> MarkIdea(IdeaRateRequest request, int userId)
    {
        var user = _dbContext.User.Where(u => u.Id == userId).Include(mbox=>mbox.IdeaRates).FirstOrDefault();

        if (user == null)
            return new ErrorResponse(HttpStatusCode.Unauthorized, "User not found");
        
        var idea = _dbContext.Idea.FirstOrDefault(i => i.Id == request.IdeaId);
        if (idea == null)
            return new ErrorResponse(HttpStatusCode.NotFound, "Idea not found");

        if (user.IdeaRates.Any(i => i.IdeaId == request.IdeaId))
        {
            var ideaRate = user.IdeaRates.FirstOrDefault(i => i.IdeaId == request.IdeaId);
            if (ideaRate != null)
            {
                ideaRate.IdeaMark = request.Mark;
                _dbContext.IdeaRates.Update(ideaRate);   
            }
        }
        else
        {
            IdeaRates newRate = new IdeaRates()
            {
                IdeaId = idea.Id,
                UserId = user.Id,
                IdeaMark = request.Mark,
                RateDate = DateTime.Now
            };

            _dbContext.IdeaRates.Add(newRate);
        }

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<Response<bool>> AddComment(AddIdeaCommentRequest request, int userId)
    {
        var user = _dbContext.User.FirstOrDefault(u => u.Id == userId);

        if (user == null)
            return new ErrorResponse(HttpStatusCode.Unauthorized, "User not found");
        
        var idea = _dbContext.Idea.FirstOrDefault(i => i.Id == request.IdeaId);
        if (idea == null)
            return new ErrorResponse(HttpStatusCode.NotFound, "Idea not found");

        IdeaComments newComment = new IdeaComments()
        {
            IdeaId = request.IdeaId,
            UserId = user.Id,
            Comment = request.Comment ?? string.Empty,
            CommentDate = DateTime.Now
        };

        _dbContext.IdeaComments.Add(newComment);
        await _dbContext.SaveChangesAsync();

        return true;
    }
    
    public async Task<Response<bool>> DeleteComment(int commentId, int userId)
    {
        var user = _dbContext.User.FirstOrDefault(u => u.Id == userId);

        if (user == null)
            return new ErrorResponse(HttpStatusCode.Unauthorized, "User not found");
        
        var comment = _dbContext.IdeaComments.FirstOrDefault(c => c.Id == commentId && c.UserId == userId);
        if (comment == null)
            return new ErrorResponse(HttpStatusCode.NotFound, "Comment not found");

        _dbContext.IdeaComments.Remove(comment);
        await _dbContext.SaveChangesAsync();

        return true;
    }


    public async Task AddIdeaFiles(List<IFormFile> files, int ideaId)
    {
        foreach (var file in files)
        { 
            string? path = FileSaver.AddFile(file, "ideaFiles");

            if (!String.IsNullOrEmpty(path))
            {
                IdeaFiles newFile = new IdeaFiles()
                {
                    IdeaId = ideaId,
                    FilePath = path,
                    FileDate = DateTime.Now
                };
                
                _dbContext.Add(newFile); 
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}