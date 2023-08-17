import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { LeagueDataService } from './league-data.service';

@Injectable()
export class LeagueResolver implements Resolve<any> {
    constructor(private dataService: LeagueDataService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<any> {

        const id = parseInt(route.paramMap.get("id") || '0');
        return this.dataService.getById(id);
    }
}