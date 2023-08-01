import { Season } from "../season/Season";
import { Region } from "../Region";
import { SocialMedia } from "../SocialMedia";
import { LeagueUser } from "./LeagueUser";

export class League {
    id: number = 1;
    name: string = "League";
    description: string = '';
    imagePath: string | null = '';
    colorHex: string = '#000'
    regionId: number = 1;
    socialMediaId: number = 1;
    leagueUsers?: LeagueUser[] = [];
    region?: Region;
    seasonsInLeague?: Season[] = [];
    socialMedia?: SocialMedia;
}
