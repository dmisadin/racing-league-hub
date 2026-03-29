using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.API.Dtos.Resource;
using RacingLeagueHub.BLL.Models;
using RacingLeagueHub.BLL.Services.Interfaces;

namespace RacingLeagueHub.API.Controllers;

[ApiController]
[Route("api/resource")]
public class ResourcesController(IResourceService resourceService) : ControllerBase
{

    [HttpGet("get-by-id/{id}")]
    public async Task<ActionResult<ResourceDto>> GetById([FromRoute] EncryptedId id, CancellationToken ct)
    {
        var file = await resourceService.GetByIdAsync(id.RawId, ct);

        if (file == null)
            NotFound();

        return Ok(file);
    }

    [HttpGet("get-file-url/{id}")]
    public async Task<ActionResult<IReadOnlyList<ResourceDto>>> GetFileUrl([FromRoute] EncryptedId id, CancellationToken ct)
    {
        var fileUrl = await resourceService.GetFileUrl(id.RawId, ct);

        if (fileUrl == null)
            NotFound();

        return Ok(fileUrl);
    }

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

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] EncryptedId id, CancellationToken ct)
    {
        await resourceService.DeleteAsync(id.RawId, ct);
        return NoContent();
    }
}
