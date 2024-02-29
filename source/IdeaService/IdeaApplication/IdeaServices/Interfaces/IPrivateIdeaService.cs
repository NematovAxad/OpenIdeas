using GeneralDomain.Responses;
using IdeaDomain.CodeModels.Requests;

namespace IdeaApplication.IdeaServices.Interfaces;

public interface IPrivateIdeaService
{
    Task<Response<bool>> AddSharedUser(AddSharedUserRequest request, int userId);
    
    Task<Response<bool>> DeleteSharedUser(DeleteSharedUserRequest request, int userId);
}