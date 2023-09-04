import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AbstractGetService } from './abstractGetService';
import { Season } from 'app/shared/models/season/Season';

@Injectable({
  providedIn: 'root'
})
export class SeasonListService extends AbstractGetService<Season[]> {

    constructor(http: HttpClient) { 
        super(http, 'https://localhost:44347/api/Season');
    }
}
