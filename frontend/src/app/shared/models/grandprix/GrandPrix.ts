class Track {
    id: number = 0;
    name: string = "";
    location: string = "";
    imagePath: string | null = "";
    countryIso: string = "";
}

interface SessionResult {
    driverId: number
    teamId: number
    pointsGained: number
}

interface Team {
    id: number
    name: string
    imagePath: string
    colorHex: string
}

export class GrandPrix {
    id?: number = 0;
    seasonId: number = 0;
    name: string = '';
    startTime: string = '';
    hasSprint: boolean = false;
    youtubeUrl: string = 'youtube.com';
    fastestDriverId?: number;
    track!: Track;
    races: SessionResult[] = [];
    qualifications: SessionResult[] = [];
    sprints: SessionResult[] = [];
    teams: Team[] = [];
}
