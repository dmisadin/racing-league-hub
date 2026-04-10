import { BaseDto } from "../../../shared/models/dtos";

export interface LeagueDto extends BaseDto {
    region: Region;
    name: string;
    abbreviation: string;
    description: string | null;
    timezone: string;
    slug: string;
    logoResourceId: string;
    logoUrl: string | null;
}

export enum Region {
    Adria,
    Europe,
    NorthAmerica,
    SouthAmerica,
    Asia,
    Oceania,
    Africa
}