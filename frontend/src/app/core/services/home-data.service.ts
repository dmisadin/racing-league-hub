import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GrandPrix } from 'app/shared/models/GrandPrix';
import { BehaviorSubject, Observable, firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HomeDataService {
  private baseUrl = 'https://localhost:44347/api/GrandPrix';

  constructor(private http: HttpClient) {}

  public fetchData(): Observable<GrandPrix[]> {
    return this.http.get<GrandPrix[]>(this.baseUrl + '/homepage');
  }
}
