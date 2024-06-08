export interface SessionResult {
    id: number;
    driverId: number;
    teamId: number;
    position: number;
    pointsGained: number;
    resultStatus: number;
    isReserve: boolean;
    fastestLapInMs: number | null;

    driverName: string;
    countryIso: string;

    selectedForDeletion: boolean;
}

export interface QualifyingResult extends SessionResult {
    bestTimeTyre: string;
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
    tyres: string[];
}