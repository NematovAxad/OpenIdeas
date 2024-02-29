using System.Net;
using GeneralApplication.Extensions;
using GeneralApplication.Interfaces;
using GeneralDomain.EntityModels;
using GeneralDomain.Extensions;
using GeneralDomain.Responses;
using GeneralInfrastructure.DbContext;
using UserApplication.UserServices.Interfaces;
using UserDomain.CodeModels.Requests.UserRequests;
using UserDomain.CodeModels.Responses.UserResponses;
using UserDomain.Extensions;

namespace UserApplication.UserServices.Services;

public class UserService:IUserService
{
    private readonly DataContext _dbContext;
    private readonly IGetByIdGlobalService _globalService;

    public UserService(DataContext dbContext, IGetByIdGlobalService globalService)
    {
        _dbContext = dbContext;
        _globalService = globalService;
    }
    
    public async Task<Response<bool>> RegisterUser(UserRegisterRequest request)
    {
        var user = _dbContext.User.FirstOrDefault(u => u.UserName == request.UserName);

        if (user != null)
            return new ErrorResponse(HttpStatusCode.NotAcceptable, "Login is occupied!");
        
        if(!request.Password.IsValid())
            return new ErrorResponse(HttpStatusCode.NotAcceptable, "Password not acceptable!");
        
        if(String.IsNullOrEmpty(request.FirstName))
            return new ErrorResponse(HttpStatusCode.NotAcceptable, "FirstName not acceptable!");
        
        if(String.IsNullOrEmpty(request.LastName))
            return new ErrorResponse(HttpStatusCode.NotAcceptable, "LastName not acceptable!");
        
        
        User addUser = new User()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Password = request.Password.GetHashString(),
            Email = request.Email
        };

        _dbContext.Add(addUser);
        await _dbContext.SaveChangesAsync();

        return true;
    }
    
    public async Task<Response<LoginResponse>> Login(LoginRequest request)
    {
        LoginResponse result = new LoginResponse();
        
        if (!String.IsNullOrEmpty(request.Password) && !String.IsNullOrEmpty(request.UserName))
        {
            var user = _dbContext.User.FirstOrDefault(u => u.UserName.ToLower() == request.UserName.ToLower() && u.Password == request.Password.GetHashString());
            if(user==null)
                return new ErrorResponse(HttpStatusCode.Unauthorized, "UserNotFound");

            result.IsSuccess = true;
            result.RefreshToken = UserIdentity.RefreshToken(user);
            result.AccessToken = UserIdentity.AccessToken(user);

            user.RefreshToken = result.RefreshToken;
            _dbContext.User.Update(user);
            await _dbContext.SaveChangesAsync();

            return result;

        }
        else
        {
            return new ErrorResponse(HttpStatusCode.BadRequest, "not enough data");
        }   
    }

    public async Task<Response<bool>> AddUserPhoto(UserPhotoAddRequest request)
    {
        var user = _globalService.User(request.UserId);
        
        string? path = FileSaver.AddFile(request.Photo, "userPhotos");
        if(String.IsNullOrEmpty(path))
            return new ErrorResponse(HttpStatusCode.BadRequest, "Couldn't save file");

        user.Result.PhotoPath = path;
        _dbContext.User.Update(user.Result);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<Response<bool>> DeleteUserPhoto(UserPhotoDeleteRequest request)
    {
        var user = _globalService.User(request.UserId);
        
        user.Result.PhotoPath = string.Empty;
        
        _dbContext.User.Update(user.Result);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}