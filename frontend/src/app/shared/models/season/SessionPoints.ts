import { Point } from "./Point";

export class SessionPoints {
    racePoints!: Point[];
    qualPoints?: Point[];
    sprintPoints?: Point[];
    fastestLapPoints!: Point;
}