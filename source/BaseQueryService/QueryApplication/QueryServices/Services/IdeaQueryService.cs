using GeneralDomain.Responses;
using GeneralInfrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using QueryApplication.QueryServices.Interfaces;
using QueryDomain.CodeModels.Requests;
using QueryDomain.CodeModels.Responses;

namespace QueryApplication.QueryServices.Services;

public class IdeaQueryService:IIdeaQueryService
{
    private readonly DataContext _dbContext;

    public IdeaQueryService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Response<IdeaQueryResponse>> GetIdeas(IdeaQueryRequest request, int userId)
    {
        IdeaQueryResponse response = new IdeaQueryResponse(){ Ideas = new List<IdeaQueryResultModel>()};

        var ideas = _dbContext.Idea.Include(i=>i.Rates).Include(i=>i.Comments).ToList();

        if (request.MyIdeas && userId != 0)
            ideas = ideas.Where(i => i.UserId == userId).ToList();

        foreach (var idea in ideas)
        {
            IdeaQueryResultModel addModel = new IdeaQueryResultModel()
            {
                Id = idea.Id,
                UserId = idea.UserId,
                Title = idea.Title,
                Body = idea.Body,
                CreateDate = idea.CreateDate,
                UpdateDate = idea.UpdateDate
            };
            
            response.Ideas.Add(addModel);
        }

        return response;
    }
}