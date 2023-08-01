import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { League } from 'app/shared/models/league/League';

import { Observable } from 'rxjs';
@Injectable({
    providedIn: 'root'
})
export class LeagueDataService {
    isDataLoaded: boolean = true;
    
    private baseUrl = 'https://localhost:44347/api/League/display';

    constructor(private http: HttpClient) { }

    public fetchData(id: number): Observable<League> {
        return this.http.get<League>(this.baseUrl + '/' + id);
    }
}
