import { BaseDto } from "../../../../shared/models/dtos";
import { Game } from "../../../../shared/models/enums";

export interface SeasonDto extends BaseDto {
    leagueId: number;
    name: string;
    platform: Platform;
    game: Game;
    lapPercentageRequired: number;
    slug: string;
    logoResourceId: number | null;
    logoUrl: string | null;
}

export enum Platform {
    Steam = 1,
    PlayStation = 2,
    Xbox = 3,
    Origin = 4,
    Crossplay = 5,
}