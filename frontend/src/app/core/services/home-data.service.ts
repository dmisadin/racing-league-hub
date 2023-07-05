import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { grandPrix } from 'app/shared/models/grandPrix';
import { BehaviorSubject, Observable, firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HomeDataService {
  private baseUrl = 'https://localhost:44347/api/GrandPrix';

  constructor(private http: HttpClient) {}

  public fetchData(): Observable<grandPrix[]> {
    return this.http.get<grandPrix[]>(this.baseUrl + '/homepage');
  }
}
