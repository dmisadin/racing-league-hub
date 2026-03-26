using RacingLeagueHub.API.Dtos.Resource;
using RacingLeagueHub.BLL.Entities.Resources;
using RacingLeagueHub.BLL.Services.Interfaces;
using System.Linq.Expressions;

namespace RacingLeagueHub.BLL.Mapping.DtoFactories;

public class ResourceDtoFactory(IStorageService storageService) : DtoFactoryBase<Resource, ResourceDto>
{

    public override void FromDto(Resource entity, ResourceDto dto)
    {
        throw new NotImplementedException();
    }

    public override Expression<Func<Resource, ResourceDto>> ToDtoExpression()
    {
        var baseUrl = storageService.GetBaseUrl();

        return resource => new ResourceDto
        {
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
