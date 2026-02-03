import { Point } from "./Point";
import { Assists } from "./Assists";
import { LobbySettings } from "./LobbySettings";

export class SeasonInsert {
    leagueId: number = 1;
    game: number = 1;
    platform: number = 1;
    name: string = '';
    imagePath?: string | null = '';
    lapsRequiredPercentage?: number | null = 90;
    lobbySettings?: LobbySettings;
    assists!: Assists;
    seasonPoints?: Point[];
}
