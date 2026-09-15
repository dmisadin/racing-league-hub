import { BaseDto } from "../../../../../shared/models/dtos";

export interface GrandPrixDto extends BaseDto {
    trackLayoutId: number;
    seasonId: number;
    leagueId: number;
    name: string;
    startingAt: string;
    vodUrl: string | null;
    slug: string;
}