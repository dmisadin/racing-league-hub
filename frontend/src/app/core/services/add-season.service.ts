import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SeasonInsert } from 'app/shared/models/season/SeasonInsert';

@Injectable({
  providedIn: 'root'
})
export class AddSeasonService {
  private baseUrl = 'https://localhost:44347/api/Season';

  constructor(private http: HttpClient) { }

  public addSeason(seasonInsert: SeasonInsert): any {
    let result = this.http.post<any>(this.baseUrl + '/create', seasonInsert);
    return result;
  }
}
