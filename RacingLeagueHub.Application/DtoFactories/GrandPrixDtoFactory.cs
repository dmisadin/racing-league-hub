using RacingLeagueHub.Application.Dtos.GrandPrix;
using RacingLeagueHub.Application.Models;
using RacingLeagueHub.Domain.Entities.GrandsPrix;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RacingLeagueHub.Application.DtoFactories;

public class GrandPrixDtoFactory : DtoFactoryBase<GrandPrix, GrandPrixDto>
{
    public override bool FromDto(GrandPrix entity, GrandPrixDto dto)
    {
        throw new NotImplementedException();
    }

    public override Expression<Func<GrandPrix, GrandPrixDto>> ToDtoExpression()
    {
        return gp => new GrandPrixDto
        {
            Id = new EncryptedId(gp.Id),
            TrackLayoutId = new EncryptedId(gp.TrackLayoutId),
            SeasonId = new EncryptedId(gp.SeasonId),
            Name = gp.Name,
            StartingAt = gp.StartingAt,
            VodUrl = gp.VodUrl,
            Slug = gp.Slug
        };
    }
}
