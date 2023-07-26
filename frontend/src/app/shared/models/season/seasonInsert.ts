import { Points } from "./Points";
import { assist } from "./assist";
import { lobbySetting } from "./lobbySetting";

export class seasonInsert{
  leagueId : number = 1;
  gameId : number = 1;
  platformId : number = 1;
  name : string = '';
  imagePath? : string | null = '';
  lapsRequiredPercentage? : number | null = 90;
  racePointsDto! : Points[];
  lobbySettingDto! : lobbySetting;
  assistDto! : assist;
  qualPointsDto? : Points[];
  sprintPointsDto? : Points[];
  fastestLapPointDto! : Points;
}
