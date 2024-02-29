using System.Net;
using GeneralApplication.Extensions;
using GeneralApplication.Interfaces;
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
    private readonly IGetByIdGlobalService _globalService;

    public IdeaService(DataContext dbContext, IGetByIdGlobalService globalService)
    {
        _dbContext = dbContext;
        _globalService = globalService;
    }
    public async Task<Response<bool>> AddNewIdea(IdeaAddRequest request, int userId)
    {
        var user = _globalService.User(userId);

        Idea newIdea = new Idea
        {
            UserId = user.Result.Id,
            CreateDate = DateTime.Now,
            UpdateDate = DateTime.Now,
            Title = request.Title,
            Body = request.Body,
            Hashtags = request.Hashtags!,
            IsPrivate = (bool)request.IsPrivate!
        };


        _dbContext.Add(newIdea);
        await _dbContext.SaveChangesAsync();

        if(request.Files != null && request.Files.Any())
            await AddIdeaFiles(request.Files, newIdea.Id);
        
        if (request.IsPrivate != null && request is { IsPrivate: true, SharedUsersId.Count: > 0 })
            await AddSharedUsers(request.SharedUsersId, newIdea.Id);
        
        
        return true;
    }

    public async Task<Response<bool>> EditIdea(IdeaEditRequest request, int userId)
    {
        var user = _globalService.User(userId);

        var idea = _globalService.UserIdea(user.Result.Id, request.IdeaId);

        if (!String.IsNullOrEmpty(request.Title))
            idea.Result.Title = request.Title;

        if (!String.IsNullOrEmpty(request.Body))
            idea.Result.Body = request.Body;
        
        if (!String.IsNullOrEmpty(request.Hashtags))
            idea.Result.Hashtags = request.Hashtags;

        
        if (request.IsPrivate != null)
        {
            idea.Result.IsPrivate = (bool)request.IsPrivate;
            if (!(bool)request.IsPrivate)
                await DeleteSharedUsers(idea.Result.Id);
        }
        
        idea.Result.UpdateDate = DateTime.Now;

        _dbContext.Idea.Update(idea.Result);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<Response<bool>> DeleteIdea(int ideaId, int userId)
    {
        var user = _globalService.User(userId);

        var idea = _globalService.UserIdea(user.Result.Id, ideaId);

        _dbContext.Idea.Remove(idea.Result);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<Response<bool>> MarkIdea(IdeaRateRequest request, int userId)
    {
        var user = _globalService.User(userId);

        var idea = _globalService.Idea(request.IdeaId);

        if (user.Result.IdeaRates.Any(i => i.IdeaId == idea.Result.Id))
        {
            var ideaRate = user.Result.IdeaRates.FirstOrDefault(i => i.IdeaId == idea.Result.Id);
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
                IdeaId = idea.Result.Id,
                UserId = user.Result.Id,
                IdeaMark = request.Mark,
                RateDate = DateTime.Now
            };

            _dbContext.IdeaRates.Add(newRate);
        }

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

    public async Task AddSharedUsers(List<int> usersId, int ideaId)
    {
        List<SharedIdeas> listToAdd = new List<SharedIdeas>();
        foreach (var userId in usersId)
        {
            var user = _globalService.User(userId);

            SharedIdeas shared = new SharedIdeas()
            {
                IdeaId = ideaId,
                UserId = user.Result.Id,
                SharedDate = DateTime.Now
            };
            
            listToAdd.Add(shared);
        }

        _dbContext.SharedIdeas.AddRange(listToAdd);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteSharedUsers(int ideaId)
    {
        var sharedUsers = _dbContext.SharedIdeas.Where(i => i.IdeaId == ideaId);
        
        _dbContext.SharedIdeas.RemoveRange(sharedUsers);
        await _dbContext.SaveChangesAsync();
    }
}