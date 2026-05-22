using RacingLeagueHub.Application.Dtos.Resource;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Entities.Resources;
using RacingLeagueHub.Domain.Services.Interfaces;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.DtoFactories;

public class ResourceDtoFactory(IStorageService storageService) : DtoMapperBase<Resource, ResourceDto>
{

    public override bool FromDto(Resource entity, ResourceDto dto)
    {
        throw new NotImplementedException();
    }

    public override Expression<Func<Resource, ResourceDto>> ToDtoExpression()
    {
        var baseUrl = storageService.GetBaseUrl();

        return resource => new ResourceDto
        {
            Id = new EncryptedId(resource.Id),
            StorageId = resource.StorageId,
            FileName = resource.FileName,
            Extension = resource.Extension,
            MimeType = resource.MimeType,
            SizeInBytes = resource.SizeInBytes,
            CreatedAt = resource.CreatedAt,
            IsThumbnail = resource.IsThumbnail,
            FileUrl = $"{baseUrl}/uploads/{resource.StorageId}.{resource.Extension}"
        };
    }
}
