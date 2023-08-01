import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { leagueInsert } from 'app/shared/models/league/LeagueInsert';

@Injectable({
  providedIn: 'root'
})
export class AddLeagueService {
  private baseUrl = 'https://localhost:44347/api/League';

  constructor(private http: HttpClient) { }

  public addLeague(leagueInsert: leagueInsert): any {
    let result = this.http.post<any>(this.baseUrl + '/create', leagueInsert);
    return result;
  }
}
