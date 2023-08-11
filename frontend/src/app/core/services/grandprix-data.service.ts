import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AbstractGetService } from './abstractGetService';
import { GrandPrix } from 'app/shared/models/grandprix/GrandPrix';
@Injectable({
  providedIn: 'root'
})
export class GrandprixDataService extends AbstractGetService<GrandPrix> {
    constructor(http: HttpClient) {
        super(http, 'https://localhost:44347/api/GrandPrix/display/');
    }
}
