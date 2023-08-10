import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { LeagueDataService } from './league-data.service';

@Injectable()
export class DataResolver implements Resolve<any> {
  constructor(private dataService: LeagueDataService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<any> {
    return this.dataService.getById(1)
  }
}