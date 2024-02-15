using GeneralDomain.Responses;
using UserDomain.CodeModels.Requests.UserRequests;
using UserDomain.CodeModels.Responses.UserResponses;

namespace UserApplication.UserServices.Interfaces;

public interface IUserService
{
    Task<Response<bool>> RegisterUser(UserRegisterRequest request);
    
    Task<Response<LoginResponse>> Login(LoginRequest request);

    Task<Response<bool>> AddUserPhoto(UserPhotoAddRequest request);
    
    Task<Response<bool>> DeleteUserPhoto(UserPhotoDeleteRequest request);
}