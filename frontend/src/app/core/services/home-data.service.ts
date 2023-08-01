import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RaceRow } from 'app/shared/models/homepage/RaceRow';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HomeDataService {
  private baseUrl = 'https://localhost:44347/api/GrandPrix';

  constructor(private http: HttpClient) {}

  public fetchData(): Observable<RaceRow[]> {
    return this.http.get<RaceRow[]>(this.baseUrl + '/homepage');
  }
}
