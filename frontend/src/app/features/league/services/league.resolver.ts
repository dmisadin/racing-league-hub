import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { LeagueDataService } from './league-data.service';
import { League } from 'app/shared/models/league/League';

@Injectable()
export class LeagueResolver implements Resolve<League> {
    constructor(private dataService: LeagueDataService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<League> {

        const id = parseInt(route.paramMap.get("leagueId") || '0');
        return this.dataService.getById(id);
    }
}