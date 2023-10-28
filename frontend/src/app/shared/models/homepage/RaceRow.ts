import { LeagueHome } from "../league/LeagueHome";
import { SeasonHome } from "../season/SeasonHome";
import { TrackHome } from "../track/TrackHome";

export class RaceRow {
    id: number = 1;
    name: string = 'Grand Prix Name';
    startTime: Date = new Date();
    youtubeUrl: string = "";
    league: LeagueHome = { id: 1, name: "League name" };
    season: SeasonHome = { id: 1, name: "Season name" };
    track: TrackHome = { id: 1, name: "Albert Park Circuit", location: "Melbourne" };
}