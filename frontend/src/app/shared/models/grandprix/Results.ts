export interface SessionResult {
    id: number;
    driverId: number;
    teamId: number;
    position: number;
    pointsGained: number;
    resultStatus: number;
    isReserve: boolean;

    driverName: string;
    countryIso: string;
}

export interface QualifyingResult extends SessionResult {
    bestTimeTyre: string;
    fastestLapInMs: number | null;
}

export interface SprintResult extends SessionResult {
    gridPosition?: number;
    raceTime: number;
    lapsCompleted: number;
    usedTyres: string;
    timePenalties: number;
    tyres: string[];
}

export interface RaceResult extends SessionResult {
    gridPosition?: number;
    raceTime: number;
    lapsCompleted: number;
    usedTyres: string;
    timePenalty: number;
    postRaceTimePenalty: number;
    fastestLapInMs: number | null;
    tyres: string[];
}