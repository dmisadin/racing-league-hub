import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AbstractGetService } from './abstractGetService';
import { RaceRow } from 'app/shared/models/homepage/RaceRow';
import { ApiUrls } from './apiUrls';
import { Observable } from 'rxjs';
import { SessionResult } from 'app/shared/models/grandprix/Results';

@Injectable({
    providedIn: 'root'
})
export class GrandPrixService extends AbstractGetService<RaceRow> {

    constructor(http: HttpClient) {
        super(http, `${ApiUrls.DOMAIN}${ApiUrls.GrandPrixStartingSoon}`);
    }

    public InsertGrandPrixResults(results: any[])  {
        console.log(`${ApiUrls.DOMAIN}${ApiUrls.InsertGrandPrixResults}`);
        this.http.post(`${ApiUrls.DOMAIN}${ApiUrls.InsertGrandPrixResults}`, results).subscribe(data => console.log("post", data));
    }
}

@Injectable({
    providedIn: 'root'
})
export class GrandPrixLiveService extends AbstractGetService<RaceRow> {

    constructor(http: HttpClient) {
        super(http, 'https://localhost:44347/api/GrandPrix/live');
    }
}