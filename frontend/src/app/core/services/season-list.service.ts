import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AbstractGetService } from './abstractGetService';
import { Season } from 'app/shared/models/season/Season';
import { SessionPoints } from 'app/shared/models/season/SessionPoints';

@Injectable({
  providedIn: 'root'
})
export class SeasonListService extends AbstractGetService<Season[]> {

    constructor(http: HttpClient) { 
        super(http, 'https://localhost:44347/api/Season');
    }
}


@Injectable({
    providedIn: 'root'
  })
  export class SessionPointsService extends AbstractGetService<SessionPoints> {
  
      constructor(http: HttpClient) { 
          super(http, 'https://localhost:44347/api/Season/SessionPoints/');
      }
  }
  