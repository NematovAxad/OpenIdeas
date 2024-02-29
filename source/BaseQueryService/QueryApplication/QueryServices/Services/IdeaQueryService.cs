using GeneralApplication.Interfaces;
using GeneralDomain.Enums;
using GeneralDomain.Responses;
using GeneralInfrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using QueryApplication.QueryServices.Interfaces;
using QueryDomain.CodeModels.Responses.IdeaQueryResponses;

namespace QueryApplication.QueryServices.Services;

public class IdeaQueryService:IIdeaQueryService
{
    private readonly DataContext _dbContext;
    private readonly IGetByIdGlobalService _globalService;
    
    public IdeaQueryService(DataContext dbContext, IGetByIdGlobalService globalService)
    {
        _dbContext = dbContext;
        _globalService = globalService;
    }
    
    public Task<Response<IdeaQueryResponse>> GetIdeas(int userId)
    {
        _globalService.User(userId);
        
        IdeaQueryResponse response = new IdeaQueryResponse(){ Ideas = new List<IdeaQueryResultModel>()};

        var ideas = _dbContext.Idea.Where(i => !i.IsPrivate)
            .Include(i => i.Rates)
            .Include(i=>i.Files)
            .Include(i => i.Comments)
            .ThenInclude(c => c.User);
        
        foreach (var idea in ideas)
        {
            IdeaQueryResultModel addModel = new IdeaQueryResultModel()
            {
                Id = idea.Id,
                UserId = idea.UserId,
                Title = idea.Title,
                Body = idea.Body,
                CreateDate = idea.CreateDate,
                UpdateDate = idea.UpdateDate,
                //IdeaRate = idea.Rates.Count(i => i.IdeaMark == IdeaMark.Up) - idea.Rates.Count(i => i.IdeaMark == IdeaMark.Up),
                IdeaUpCount = idea.Rates.Count(i => i.IdeaMark == IdeaMark.Up),
                IdeaDownCount = idea.Rates.Count(i => i.IdeaMark == IdeaMark.Down),
                Comments = new List<IdeaCommentsQueryResulModel>(),
                Files = new List<IdeaFilesQueryResulModel>()
            };

            foreach (var comment in idea.Comments)
            {
                IdeaCommentsQueryResulModel resultComments = new IdeaCommentsQueryResulModel()
                {
                    Id = comment.Id,
                    Comment = comment.Comment,
                    CommentDate = comment.CommentDate,
                    CommentedUser = new CommentUser()
                        { Id = comment.User.Id, Username = comment.User.UserName }
                };
                
                addModel.Comments.Add(resultComments);
            }

            foreach (var files in idea.Files)
            {
                IdeaFilesQueryResulModel resultFiles = new IdeaFilesQueryResulModel()
                {
                    Id = files.Id,
                    FilePath = files.FilePath,
                    FileDate = files.FileDate
                };
                addModel.Files.Add(resultFiles);
            }
            
            response.Ideas.Add(addModel);
        }

        return Task.FromResult<Response<IdeaQueryResponse>>(response);
    }

    public Task<Response<IdeaQueryResponse>> GetMyIdeas(int userId)
    {
        var user = _globalService.User(userId);
        
        IdeaQueryResponse response = new IdeaQueryResponse(){ Ideas = new List<IdeaQueryResultModel>()};

        var ideas = _dbContext.Idea.Where(i => i.UserId == user.Result.Id)
            .Include(i => i.Rates)
            .Include(i=>i.Files)
            .Include(i => i.Comments)
            .ThenInclude(c => c.User);
        
        foreach (var idea in ideas)
        {
            IdeaQueryResultModel addModel = new IdeaQueryResultModel()
            {
                Id = idea.Id,
                UserId = idea.UserId,
                Title = idea.Title,
                Body = idea.Body,
                CreateDate = idea.CreateDate,
                UpdateDate = idea.UpdateDate,
                IdeaUpCount = idea.Rates.Count(i => i.IdeaMark == IdeaMark.Up),
                IdeaDownCount = idea.Rates.Count(i => i.IdeaMark == IdeaMark.Down),
                Comments = new List<IdeaCommentsQueryResulModel>(),
                Files = new List<IdeaFilesQueryResulModel>()
            };

            foreach (var comment in idea.Comments)
            {
                IdeaCommentsQueryResulModel resultComments = new IdeaCommentsQueryResulModel()
                {
                    Id = comment.Id,
                    Comment = comment.Comment,
                    CommentDate = comment.CommentDate,
                    CommentedUser = new CommentUser()
                        { Id = comment.User.Id, Username = comment.User.UserName }
                };
                
                addModel.Comments.Add(resultComments);
            }

            foreach (var files in idea.Files)
            {
                IdeaFilesQueryResulModel resultFiles = new IdeaFilesQueryResulModel()
                {
                    Id = files.Id,
                    FilePath = files.FilePath,
                    FileDate = files.FileDate
                };
                addModel.Files.Add(resultFiles);
            }
            
            response.Ideas.Add(addModel);
        }

        return Task.FromResult<Response<IdeaQueryResponse>>(response);
    }
    public Task<Response<IdeaQueryResponse>> GetMySharedIdeas(int userId)
    {
        var user = _globalService.User(userId);
        
        IdeaQueryResponse response = new IdeaQueryResponse(){ Ideas = new List<IdeaQueryResultModel>()};

        var sharedIdeasIdList = _dbContext.SharedIdeas.Where(i => i.UserId == user.Result.Id).Select(i => i.Id).ToList();

        var ideas = _dbContext.Idea.Where(i => sharedIdeasIdList.Any(sharedIdeaId => sharedIdeaId == i.Id))
            .Include(i => i.Rates)
            .Include(i => i.Files)
            .Include(i => i.Comments)
            .ThenInclude(c => c.User);
        
        foreach (var idea in ideas)
        {
            IdeaQueryResultModel addModel = new IdeaQueryResultModel()
            {
                Id = idea.Id,
                UserId = idea.UserId,
                Title = idea.Title,
                Body = idea.Body,
                CreateDate = idea.CreateDate,
                UpdateDate = idea.UpdateDate,
                IdeaUpCount = idea.Rates.Count(i => i.IdeaMark == IdeaMark.Up),
                IdeaDownCount = idea.Rates.Count(i => i.IdeaMark == IdeaMark.Down),
                Comments = new List<IdeaCommentsQueryResulModel>(),
                Files = new List<IdeaFilesQueryResulModel>()
            };

            foreach (var comment in idea.Comments)
            {
                IdeaCommentsQueryResulModel resultComments = new IdeaCommentsQueryResulModel()
                {
                    Id = comment.Id,
                    Comment = comment.Comment,
                    CommentDate = comment.CommentDate,
                    CommentedUser = new CommentUser()
                        { Id = comment.User.Id, Username = comment.User.UserName }
                };
                
                addModel.Comments.Add(resultComments);
            }

            foreach (var files in idea.Files)
            {
                IdeaFilesQueryResulModel resultFiles = new IdeaFilesQueryResulModel()
                {
                    Id = files.Id,
                    FilePath = files.FilePath,
                    FileDate = files.FileDate
                };
                addModel.Files.Add(resultFiles);
            }
            
            response.Ideas.Add(addModel);
        }

        return Task.FromResult<Response<IdeaQueryResponse>>(response);
    }
}