import { CountryLean } from "../../../../shared/models/country";
import { BaseDto } from "../../../../shared/models/dtos";

export interface TrackDto extends BaseDto{
    name: string;
    country?: CountryLean;
    countryAlpha2: string;
    city: string;
    elevation?: number;
    shortName: string;
}