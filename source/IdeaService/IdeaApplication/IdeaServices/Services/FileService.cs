using System.Net;
using GeneralApplication.Extensions;
using GeneralApplication.Interfaces;
using GeneralApplication.Services;
using GeneralDomain.EntityModels;
using GeneralDomain.Responses;
using GeneralInfrastructure.DbContext;
using IdeaApplication.IdeaServices.Interfaces;
using IdeaDomain.CodeModels.Requests;
using IdeaDomain.CodeModels.Responses;
using Microsoft.EntityFrameworkCore;

namespace IdeaApplication.IdeaServices.Services;

public class FileService:IFileService
{
    private readonly DataContext _dbContext;
    private readonly IGetByIdGlobalService _globalService;

    public FileService(DataContext dbContext, IGetByIdGlobalService globalService)
    {
        _dbContext = dbContext;
        _globalService = globalService;
    }
    public async Task<Response<AddIdeaFileResponse>> AddIdeaFile(AddIdeaFileRequest request, int userId)
    {
        AddIdeaFileResponse result = new AddIdeaFileResponse();

        var user = _globalService.User(userId);

        var idea = _globalService.UserIdea(user.Result.Id, request.IdeaId);
        
        string? path = FileSaver.AddFile(request.File, "ideaFiles");

        if (String.IsNullOrEmpty(path))
            return new ErrorResponse(HttpStatusCode.BadRequest, "File cannot save");
        
        IdeaFiles newFile = new IdeaFiles()
        {
            IdeaId = idea.Result.Id,
            FilePath = path,
            FileDate = DateTime.Now
        };
            
        _dbContext.Add(newFile); 
        await _dbContext.SaveChangesAsync();

        result.FilePath = path;
        
        return result;
    }

    public async Task<Response<bool>> DeleteIdeaFile(int fileId, int userId)
    {
        var ideaFile = _dbContext.IdeaFiles.Where(f => f.Id == fileId).Include(mbox => mbox.Idea).FirstOrDefault();
        
        if(ideaFile==null || ideaFile.Idea.UserId!=userId)
            return new ErrorResponse(HttpStatusCode.NotFound, "File not found");

        _dbContext.IdeaFiles.Remove(ideaFile);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}