using Microsoft.AspNetCore.Mvc;
using RacingLeagueHub.Api.Models.Resource;
using RacingLeagueHub.Application.Dtos.Resource;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Application.Models.Resource;
using RacingLeagueHub.Application.Services;

namespace RacingLeagueHub.Api.Controllers;

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
    public async Task<ActionResult<ResourceDto>> Upload([FromForm] UploadResourceRequest request, CancellationToken ct)
    {
        if (request.File.Length == 0)
            return BadRequest("File is empty.");

        await using var stream = request.File.OpenReadStream();

        var fileUpload = new FileUploadRequest(
            FileName: request.File.FileName,
            ContentType: request.File.ContentType,
            SizeInBytes: request.File.Length,
            Content: stream
        );

        var result = await resourceService.UploadAsync(fileUpload, request.IsThumbnail, ct);
        return Ok(result);
    }
}
