using GeneralDomain.Responses;
using IdeaDomain.CodeModels.Requests;

namespace IdeaApplication.IdeaServices.Interfaces;

public interface IIdeaCommentService
{
    Task<Response<bool>> AddComment(AddIdeaCommentRequest request, int userId);
    
    Task<Response<bool>> DeleteComment(int commentId, int userId);
}