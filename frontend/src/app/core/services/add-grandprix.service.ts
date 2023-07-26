import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { grandPrixInsert } from 'app/shared/models/grandPrixInsert';

@Injectable({
  providedIn: 'root'
})
export class AddGrandprixService {

  private baseUrl = 'https://localhost:44347/api/GrandPrix';

  constructor(private http: HttpClient) { }

  public addGrandPrix(grandPrixInserts: Array<grandPrixInsert>): any {
    let result = this.http.post<any>(this.baseUrl + '/create', grandPrixInserts);
    return result;
  }
}
