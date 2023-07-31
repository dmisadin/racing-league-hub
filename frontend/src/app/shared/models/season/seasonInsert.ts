import { Point } from "./Point";
import { Assists } from "./Assists";
import { LobbySettings } from "./LobbySettings";

export class seasonInsert{
  leagueId : number = 1;
  gameId : number = 1;
  platformId : number = 1;
  name : string = '';
  imagePath? : string | null = '';
  lapsRequiredPercentage? : number | null = 90;
  racePointsDto! : Point[];
  lobbySettingDto! : LobbySettings;
  assistDto! : Assists;
  qualPointsDto? : Point[];
  sprintPointsDto? : Point[];
  fastestLapPointDto! : Point;
}
