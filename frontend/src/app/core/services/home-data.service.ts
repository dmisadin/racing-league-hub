import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { grandPrix } from 'app/shared/models/grandPrix';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HomeDataService {
  baseUrl = 'https://localhost:44347/api/';
  constructor(private http: HttpClient) {}

  public fetchData(): Observable<grandPrix[]> {
    return this.http.get<grandPrix[]>(this.baseUrl + 'GrandPrix/homepage');
  }
}
