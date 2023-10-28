import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AbstractGetService } from './abstractGetService';
import { RaceRow } from 'app/shared/models/homepage/RaceRow';

@Injectable({
  providedIn: 'root'
})
export class GrandPrixStartingSoonService extends AbstractGetService<RaceRow> {

    constructor(http: HttpClient) { 
        super(http, 'https://localhost:44347/api/GrandPrix/startingsoon');
    }
}

@Injectable({
  providedIn: 'root'
})
export class GrandPrixLiveService extends AbstractGetService<RaceRow> {

  constructor(http: HttpClient) { 
      super(http, 'https://localhost:44347/api/GrandPrix/live');
  }
}