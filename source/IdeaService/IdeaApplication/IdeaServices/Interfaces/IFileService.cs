using GeneralDomain.Responses;
using IdeaDomain.CodeModels.Requests;
using IdeaDomain.CodeModels.Responses;

namespace IdeaApplication.IdeaServices.Interfaces;

public interface IFileService
{
    Task<Response<AddIdeaFileResponse>> AddIdeaFile(AddIdeaFileRequest request, int userId);
    
    Task<Response<bool>> DeleteIdeaFile(int fileId, int userId);
}