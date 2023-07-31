import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Platform } from 'app/shared/models/Platform'
import { Season } from 'app/shared/models/Season';
import { AbstractGetService } from './abstractGetService';

@Injectable({
  providedIn: 'root'
})
export class SeasonDataService  extends AbstractGetService<Season> {
    constructor(http: HttpClient) {
        super(http, 'https://localhost:44347/api/Season/display/');
    }
}