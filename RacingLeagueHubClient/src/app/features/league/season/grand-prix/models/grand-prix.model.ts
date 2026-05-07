import { BaseDto } from "../../../../../shared/models/dtos";

export interface GrandPrixDto extends BaseDto {
    trackLayoutId: string;
    seasonId: string;
    leagueId: string;
    name: string;
    startingAt: string;
    vodUrl: string | null;
    slug: string;
}