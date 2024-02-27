using GeneralApplication.Extensions;
using GeneralDomain.Attributes;
using GeneralDomain.Responses;
using IdeaApplication.IdeaServices.Interfaces;
using IdeaDomain.CodeModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdeaWebService.Controllers;
[Route("idea_service/api/v1/comment/")]
[ApiController]
public class CommentController : Controller
{
    private readonly IIdeaService _ideaService;

    public CommentController(IIdeaService ideaService)
    {
        _ideaService = ideaService;
    }
    //[ClaimRequirement("Permission", "CanAddIdeaCommand")]
    [Authorize]
    [HttpPost("add_idea_comment")]
    public async Task<Response<bool>> AddIdeaComment([FromBody] AddIdeaCommentRequest model)
    {
        return   await _ideaService.AddComment(model, this.UserId());
    }
    
    [Authorize]
    [HttpDelete("delete_idea_comment")]
    public async Task<Response<bool>> DeleteIdeaComment([FromQuery] int commentId)
    {
        return   await _ideaService.DeleteComment(commentId, this.UserId());
    }
}