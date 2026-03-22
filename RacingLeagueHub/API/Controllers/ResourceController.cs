using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.API.Dtos.Resource;
using RacingLeagueHub.BLL.Services.Interfaces;

namespace RacingLeagueHub.API.Controllers;

[ApiController]
[Route("api/resource")]
public class ResourcesController(IResourceService resourceService) : ControllerBase
{
    [HttpGet("get-all")]
    public async Task<ActionResult<IReadOnlyList<ResourceDto>>> GetAll(CancellationToken ct)
    {
        var resources = await resourceService.GetAllAsync(ct);
        return Ok(resources);
    }

    [HttpPost("upload")]
    [RequestSizeLimit(5 * 1024 * 1024)]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<ResourceDto>> Upload(
        [FromForm] UploadResourceRequest request,
        CancellationToken ct)
    {
        if (request.File.Length == 0)
            return BadRequest("File is empty.");

        var result = await resourceService.UploadAsync(request.File, request.IsThumbnail, ct);
        return Ok(result);
    }

    [HttpDelete("delete/{uid:guid}")]
    public async Task<IActionResult> Delete(Guid uid, CancellationToken ct)
    {
        await resourceService.DeleteAsync(uid, ct);
        return NoContent();
    }
}
