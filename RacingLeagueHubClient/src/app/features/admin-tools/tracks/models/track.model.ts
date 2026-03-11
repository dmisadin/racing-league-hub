import { BaseDto } from "../../../../shared/models/dtos";

export interface TrackDto extends BaseDto{
    name: string;
    country: string;
    city: string;
    elevation?: number;
    shortName: string;
}