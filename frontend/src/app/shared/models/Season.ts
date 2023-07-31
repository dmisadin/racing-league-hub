import { Game } from "./Game";
import { Platform } from "./Platform";
import { Assists } from "./season/Assists";
import { LobbySettings } from "./season/LobbySettings";
import { Point } from "./season/Point";
import { GrandPrix } from "./GrandPrix";

export class Season {
    id?: number = 1;
    name: string = "F1 League";
    game: Game = { id: 5, name: "F1 23"};
    imagePath: string = "/";
    platform: Platform = { id: 1, name: "PC"};
    qualPoints?: Point[];
    sprintPoints?: Point[];
    racePoints?: Point[];
    lobbySetting?: LobbySettings;
    assist?: Assists;
    grandPrixes?: GrandPrix[];
}