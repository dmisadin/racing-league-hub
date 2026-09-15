import { BaseDto } from "../../../../shared/models/dtos";
import { Game } from "../../../../shared/models/enums";

export interface TeamDto extends BaseDto {
    name: string;
    color: string;
    gameSpecificTeams: GameTeamDto[];
}

export interface GameTeamDto extends BaseDto {
    game: Game;
    teamId: number;
    name: string;
    shortName: string;
    abbreviation: string;
    color: string;
    telemetryId: number;
    logoResourceId: number | null;
    logoUrl: string | null;
}
