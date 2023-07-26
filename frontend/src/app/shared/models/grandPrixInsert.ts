import { DatetimeoffsetToLocalPipe } from "../pipes/datetimeoffset-to-local.pipe";

export class grandPrixInsert{
  seasonId : number = 1;
  trackId : number = 1;
  name : string = '';
  startTime? : string = '';
  hasSprint : boolean = false;
  youtubeUrl : string = '';
}
