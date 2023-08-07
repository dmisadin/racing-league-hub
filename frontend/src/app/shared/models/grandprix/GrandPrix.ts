import { Driver, Team } from "../season/Season";
import { QualifyingResult, RaceResult, SprintResult } from "./Results";

class Track {
    id: number = 0;
    name: string = "";
    location: string = "";
    imagePath: string | null = "";
    countryIso: string = "";
    countryName: string = "";
    elevation?: number = 0;
    length?: number = 0;
    cornersLeft: number = 0;
    cornersTotal: number = 0;
}

export interface SessionResult {
    id?: number;
    driverId: number;
    teamId: number;
    pointsGained: number;
    resultStatus: number;

    fastestLapInMs: number | null;
    
    isReserve: boolean;
    position: number;
    
    //Qualifying only
    bestTimeTyre?: string;

    //Sprint and Race only
    gridPosition?: number;
    raceTime?: number;
    lapsCompleted?: number;
    usedTyres: string;
    postRaceTimePenalty: number;
    timePenalties: number;
}

export class SeasonGrandPrix {
    id?: number = 0;
    seasonId: number = 0;
    name: string = '';
    startTime: string = '';
    hasSprint: boolean = false;
    youtubeUrl: string = 'youtube.com';
    track!: Track;
    qualifying: SessionResult[] = [];
    sprint: SessionResult[] = [];
    race: SessionResult[] = [];
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
    qualifying: QualifyingResult[] = [];
    sprint: SprintResult[] = [];
    race: RaceResult[] = [];
    drivers: Driver[] = [];
    teams: Team[] = [];
}
