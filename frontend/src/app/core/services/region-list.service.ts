import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Region } from 'app/shared/models/Region';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegionListService {
    isDataLoaded: boolean = true;
    
    private baseUrl = 'https://localhost:44347/api/Region';

    constructor(private http: HttpClient) { }

    public fetchData(): Observable<Region[]> {
        return this.http.get<Region[]>(this.baseUrl);
    }
}
