import { Game } from "../Game";
import { Platform } from "../Platform";

export class SeasonInLeague {
    id: number = 1;
    game!: Game;
    platform!: Platform;
    name: string = '';
    imagePath: any;
    startTime: string = '';
    endTime: string = '';
}