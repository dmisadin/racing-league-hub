import { LeagueHome } from "../league/League";
import { SeasonHome } from "../season/Season";

export class RaceRow {
    id: number = 1;
    name: string = 'Grand Prix Name';
    league: SeasonHome = { id: 1, name: "League name" };
    season: LeagueHome = { id: 1, name: "Season name" };
}