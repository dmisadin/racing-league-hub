import { BaseDto } from "../../../../shared/models/dtos";
import { Game } from "../../../../shared/models/enums";
import { SeasonNextGrandPrixDto, SeasonStatus } from "./season-summary.model";
import {
    CarDamage, CarDamageRate, CornerCuttingStringency, Gearbox, QualifyingType,
    RaceDistance, RaceStart, RacingLine, SafetyCar, TractionControl, Weather
} from "./season-settings.model";

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

export interface SeasonDetailsDto {
    season: SeasonDto;
    assists: SeasonAssistsDto | null;
    lobbySettings: SeasonLobbySettingsDto | null;
    divisions: SeasonDivisionSummaryDto[];
}

export interface DivisionDto extends BaseDto {
    leagueId: number;
    name: string;
    slug: string;
    tier: number;
}

export interface SeasonDivisionSummaryDto {
    seasonDivisionId: number;
    division: DivisionDto;
    status: SeasonStatus;
    progressPercent: number;
    roundsTotal: number;
    roundsCompleted: number;
    firstRaceAt: string | null;
    lastRaceAt: string | null;
    nextGrandPrix: SeasonNextGrandPrixDto | null;
}

export interface SeasonAssistsDto extends BaseDto {
    seasonId: number;
    racingLine: RacingLine;
    gearbox: Gearbox;
    tractionControl: TractionControl;
    abs: boolean;
}

export interface SeasonLobbySettingsDto extends BaseDto {
    seasonId: number;
    qualifyingType: QualifyingType;
    raceDistancePercentage: RaceDistance;
    formationLap: boolean;
    weather: Weather;
    cornerCutting: CornerCuttingStringency;
    carDamage: CarDamage;
    carDamageRate: CarDamageRate;
    parcFerme: boolean;
    equalCarPerformance: boolean;
    safetyCar: SafetyCar;
    collisions: boolean;
    ghosting: boolean;
    raceStart: RaceStart;
}
