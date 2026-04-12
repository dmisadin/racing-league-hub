import { BaseDto } from "../../../../shared/models/dtos";
import { Game } from "../../../../shared/models/enums";

export interface SeasonDto extends BaseDto {
    name: string;
    platform: Platform;
    game: Game;
    LapPercentageRequired: number;
    slug: string;
    logoResourceId: string | null;
    logoUrl: string | null;
}

export enum Platform {
    Steam = 1,
    PlayStation = 2,
    Xbox = 3,
    Origin = 4,
    Crossplay = 5,
}