using GeneralDomain.Enums;
using GeneralDomain.Responses;
using IdeaDomain.CodeModels.Requests;

namespace IdeaApplication.IdeaServices.Interfaces;

public interface IIdeaService
{
    Task<Response<bool>> AddNewIdea(IdeaAddRequest request, int userId);
    
    Task<Response<bool>> EditIdea(IdeaEditRequest request, int userId);
    
    Task<Response<bool>> DeleteIdea(int ideaId, int userId);

    Task<Response<bool>> MarkIdea(IdeaRateRequest request, int userId);
}