export class League {
    regionId: number = 1;
    socialMediaId: number = 1;
    name: string = "League";
    imagePath: string | null = '';
    description: string = '';
    colorHex: string = '#000'
    leagueUsers: any[] = [];
    region: any = null;
    seasons: any[] = [];
    socialMedia: any = null;
    id: number = 1;
}