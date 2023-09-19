import { Season } from "../season/Season";
import { Region } from "../Region";
import { SocialMedia } from "../SocialMedia";
import { LeagueUser } from "./LeagueUser";
import { SeasonInLeague } from "../season/SeasonInLeague";
import { Platform } from "../Platform";
import { Game } from "../Game";

export class League {
    id: number = 1;
    name: string = "League";
    description: string = '';
    colorHex: string = '#000'
    imagePath: string | null = '';
    socialMedia?: SocialMedia;
    region?: Region;
    seasonsInLeague?: SeasonInLeague[] = [];
    platform?: Platform;
    game?: Game;
    //leagueUsers?: LeagueUser[] = [];
}