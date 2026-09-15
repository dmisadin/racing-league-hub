using RacingLeagueHub.Application.Common.Mappers;
using RacingLeagueHub.Application.Resources.Dtos;
using RacingLeagueHub.Domain.Resources;
using System.Linq.Expressions;

namespace RacingLeagueHub.Application.Resources.Mappers;

public class ResourceDtoMapper(IStorageService storageService) : DtoMapperBase<Resource, ResourceDto>
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
            Id = resource.Id,
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
