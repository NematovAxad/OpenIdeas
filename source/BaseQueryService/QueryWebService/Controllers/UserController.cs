using GeneralApplication.Extensions;
using GeneralDomain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueryApplication.QueryServices.Interfaces;
using QueryDomain.CodeModels.Responses.UserQueryResponses;

namespace QueryWebService.Controllers;

[Route("query_service/api/v1/user/")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserQueryService _userQueryServices;

    public UserController(IUserQueryService userQueryServices)
    {
        _userQueryServices = userQueryServices;
    }
    
    [Authorize]
    [HttpPost("get_current_user")]
    public async Task<Response<UserQueryResponse>> Get()
        => await _userQueryServices.GetUser(this.UserId());
    
}