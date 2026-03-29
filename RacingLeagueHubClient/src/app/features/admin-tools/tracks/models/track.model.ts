import { CountryLean } from "../../../../shared/models/country";
import { BaseDto } from "../../../../shared/models/dtos";
import { Game } from "../../../../shared/models/enums";

export interface TrackDto extends BaseDto {
    name: string;
    country?: CountryLean;
    countryAlpha2: string;
    city: string;
    shortName: string;

    trackLayouts?: TrackLayoutDto[];
}

export interface TrackLayoutDto extends BaseDto {
    trackId: string;
    name: string;
    pitStopDuration: number | null;
    cornersTotal: number;
    cornersLeft: number;
    lapsGrandPrix: number;
    length: number;
    elevationChange: number;
    telemetryId: number;

    trackLayoutGames: Game[];
}