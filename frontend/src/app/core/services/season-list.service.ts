import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Game } from 'app/shared/models/Game';
import { Observable } from 'rxjs';
import { AbstractGetService } from './abstractGetService';

@Injectable({
  providedIn: 'root'
})
export class GameListService extends AbstractGetService<Game> {

    constructor(http: HttpClient) { 
        super(http, 'https://localhost:44347/api/Game');
    }
}
