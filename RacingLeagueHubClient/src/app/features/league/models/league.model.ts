import { BaseDto } from "../../../shared/models/dtos";
import { ResourceBaseDto } from "../../../shared/models/resource";

export interface LeagueDto extends BaseDto {
    region: Region;
    name: string;
    abbreviation: string;
    description: string | null;
    timezone: string;
    slug: string;
    logoResourceId: string;
    logo: ResourceBaseDto | null;
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