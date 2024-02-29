using GeneralApplication.Extensions;
using GeneralDomain.Responses;
using IdeaApplication.IdeaServices.Interfaces;
using IdeaDomain.CodeModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdeaWebService.Controllers;

[Route("idea_service/api/v1/private_idea/")]
[ApiController]
public class PrivateIdeaController : Controller
{
    
    private readonly IPrivateIdeaService _privateIdeaServices;

    public PrivateIdeaController(IPrivateIdeaService privateIdeaServices)
    {
        _privateIdeaServices = privateIdeaServices;
    }
    
    [Authorize]
    [HttpPut("add_shared_user")]
    public async Task<Response<bool>> AddSharedIdea([FromBody] AddSharedUserRequest request)
    {
        return   await _privateIdeaServices.AddSharedUser(request, this.UserId());
    }
    
    [Authorize]
    [HttpPut("delete_shared_user")]
    public async Task<Response<bool>> DeleteSharedIdea([FromBody] DeleteSharedUserRequest request)
    {
        return   await _privateIdeaServices.DeleteSharedUser(request, this.UserId());
    }
}