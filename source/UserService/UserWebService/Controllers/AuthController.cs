using GeneralApplication.Extensions;
using GeneralDomain.CodeModels;
using GeneralDomain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApplication.UserServices.Interfaces;
using UserDomain.CodeModels.Requests.UserRequests;
using UserDomain.CodeModels.Responses.UserResponses;

namespace UserWebService.Controllers;

[Route("user_service/api/v1/auth/")]
[ApiController]
public class AuthController : Controller
{
    private readonly IUserService _userServices;

    public AuthController(IUserService userService)
    {
        _userServices = userService;
    }
    [HttpPost("Register")]
    public async Task<Response<bool>> Register([FromBody] UserRegisterRequest model)
        => await _userServices.RegisterUser(model);
    
    [HttpPost("Login")]
    public async Task<Response<LoginResponse>> Login([FromBody] LoginRequest model)
        => await _userServices.Login(model);

    [Authorize]
    [HttpPut("AddPhoto")]
    public async Task<Response<bool>> AddPhoto([FromForm] FileModel fileModel )
    {
        UserPhotoAddRequest request = new UserPhotoAddRequest()
        {
            UserId = this.UserId(),
            Photo = fileModel.File
        };
           
        return await _userServices.AddUserPhoto(request);
    }
    
    [Authorize]
    [HttpPost("DeletePhoto")]
    public async Task<Response<bool>> DeletePhoto()
    {
        UserPhotoDeleteRequest request = new UserPhotoDeleteRequest()
        {
            UserId = this.UserId()
        };
           
        return await _userServices.DeleteUserPhoto(request);
    }
}