import { Point } from "./Point";
import { Assists } from "./Assists";
import { LobbySettings } from "./LobbySettings";

export class SeasonInsert{
  leagueId : number = 1;
  gameId : number = 1;
  platformId : number = 1;
  name : string = '';
  imagePath? : string | null = '';
  lapsRequiredPercentage? : number | null = 90;
  lobbySettingsDto! : LobbySettings;
  assistsDto! : Assists;
  racePointsDto! : Point[];
  qualPointsDto? : Point[];
  sprintPointsDto? : Point[];
  fastestLapPointDto! : Point;
}
