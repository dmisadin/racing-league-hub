import { BaseDto } from "../../../../shared/models/dtos";
import { Game } from "../../../../shared/models/enums";
import { ResourceBaseDto } from "../../../../shared/models/resource";

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
    logo: ResourceBaseDto | null;
}

export interface GameTeamFormModel extends GameTeamDto {
    readonly removable: boolean;
}