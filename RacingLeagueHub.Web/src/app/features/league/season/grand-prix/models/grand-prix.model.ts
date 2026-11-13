import { BaseDto } from "../../../../../shared/models/dtos";

export interface GrandPrixDto extends BaseDto {
    trackLayoutId: number;
    seasonId: number;
    seasonDivisionId: number;
    divisionSlug: string;
    divisionName: string;
    leagueId: number;
    name: string;
    startingAt: string;
    vodUrl: string | null;
    slug: string;
    trackName: string | null;
    trackCity: string | null;
    trackCountryCode: string | null;
}

/**
 * API path of a season's Grands Prix, optionally narrowed to one division's calendar:
 * `/leagues/{l}/seasons/{s}[/division/{d}]/grands-prix[/{gp}]`.
 */
export function grandPrixApiPath(
    leagueSlug: string | null,
    seasonSlug: string | null,
    divisionSlug?: string | null,
    grandPrixSlug?: string | null
): string {
    let path = `/leagues/${leagueSlug}/seasons/${seasonSlug}`;

    if (divisionSlug)
        path += `/division/${divisionSlug}`;

    path += '/grands-prix';

    if (grandPrixSlug)
        path += `/${grandPrixSlug}`;

    return path;
}
