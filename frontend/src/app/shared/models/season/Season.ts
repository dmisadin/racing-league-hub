import { Game } from "../Game";
import { Platform } from "../Platform";
import { Assists } from "./Assists";
import { LobbySettings } from "./LobbySettings";
import { Point } from "./Point";
import { SeasonGrandPrix } from "../grandprix/GrandPrix";

export class Season {
    id?: number = 1;
    name: string = "F1 League";
    imagePath: string | null = "/";
    lapsRequiredPercentage: number = 90;
    game: Game = { id: 5, name: "F1 23"};
    platform: Platform = { id: 1, name: "PC"};
    qualPoints?: Point[];
    sprintPoints?: Point[];
    racePoints?: Point[];
    fastestLapPoints?: Point;
    lobbySettings?: LobbySettings;
    assists?: Assists;
    grandPrixes: SeasonGrandPrix[] = [];
    drivers: Driver[] = [];
    teams: Team[] = [];
}

export interface Driver {
    id: number
    name: string
    teamId: number
    countryIso: string
    penaltyPoints: number
}

export interface Team {
    id: number
    name: string
    imagePath: string
    colorHex: string
}

export class SeasonHome {
    id: number = 1;
    name: string = "Season 1";
}