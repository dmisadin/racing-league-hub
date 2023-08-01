import { Game } from "../Game";
import { Platform } from "../Platform";
import { Assists } from "./Assists";
import { LobbySettings } from "./LobbySettings";
import { Point } from "./Point";
import { GrandPrix } from "../grandprix/GrandPrix";

export class Season {
    id?: number = 1;
    name: string = "F1 League";
    game: Game = { id: 5, name: "F1 23"};
    imagePath: string = "/";
    platform: Platform = { id: 1, name: "PC"};
    qualPoints?: Point[];
    sprintPoints?: Point[];
    racePoints?: Point[];
    lobbySettings?: LobbySettings;
    assists?: Assists;
    grandPrixes?: GrandPrix[];
}
