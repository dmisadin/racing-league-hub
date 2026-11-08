import { PagedResult } from "../../../../shared/models/models";
import { SeasonDto } from "./season.model";

export interface LeagueSeasonsOverviewDto {
    seasonCount: number;
    racesHeld: number;
    /** All running seasons, or the soonest upcoming one when nothing is running. */
    currentSeasons: SeasonSummaryDto[];
    upcomingSeasons: SeasonSummaryDto[];
    /** First page only; further pages come from `/seasons/summaries?status=Completed`. */
    completedSeasons: PagedResult<SeasonSummaryDto>;
}

export interface SeasonSummaryDto {
    season: SeasonDto;
    roundsTotal: number;
    roundsCompleted: number;
    driverCount: number;
    firstRaceAt: string | null;
    lastRaceAt: string | null;
    nextGrandPrix: SeasonNextGrandPrixDto | null;
    driverLeader: SeasonDriverStandingDto | null;
    teamLeader: SeasonTeamStandingDto | null;
}

export interface SeasonNextGrandPrixDto {
    name: string;
    slug: string;
    startingAt: string;
    trackName: string;
    trackCity: string | null;
}

export interface SeasonDriverStandingDto {
    driverId: number;
    nickname: string;
    slug: string;
    points: number;
    wins: number;
}

export interface SeasonTeamStandingDto {
    teamId: number;
    name: string;
    color: string | null;
    points: number;
}

export enum SeasonStatus {
    Upcoming,
    Ongoing,
    Completed
}

/** Derived from the calendar: no round started yet, some rounds started, or all rounds started. */
export function getSeasonStatus(summary: SeasonSummaryDto): SeasonStatus {
    if (summary.roundsTotal === 0 || summary.roundsCompleted === 0)
        return SeasonStatus.Upcoming;

    return summary.roundsCompleted < summary.roundsTotal
        ? SeasonStatus.Ongoing
        : SeasonStatus.Completed;
}

export function getSeasonProgress(summary: SeasonSummaryDto): number {
    return summary.roundsTotal === 0
        ? 0
        : Math.round(summary.roundsCompleted / summary.roundsTotal * 100);
}
