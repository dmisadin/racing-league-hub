import { Component, OnInit } from '@angular/core';
import {
    faPlus,
    faSearch,
    faStopwatch, faPlayCircle, faCalendarDays,
} from '@fortawesome/free-solid-svg-icons';
import { HomeDataService } from 'app/core/services/home-data.service';
import { RaceRow } from 'app/shared/models/homepage/RaceRow';

import { BehaviorSubject, Observable, firstValueFrom } from 'rxjs';
import { LeaguesListService } from '../league/services/leagues-list.service';
import { League } from 'app/shared/models/league/League';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
    faPlus = faPlus;
    faSearch = faSearch;
    faStopwatch = faStopwatch;
    faPlayCircle = faPlayCircle;
    faCalendarDays = faCalendarDays;

    isDataLoaded: boolean = true;
    grandPrixData: Array<RaceRow> = new Array();
    popularLeagues: Array<League> = new Array();

    constructor(private homeDataService: HomeDataService, private leagueListService: LeaguesListService) { }

    ngOnInit(): void {
        this.homeDataService.fetchData().subscribe(data => {
            this.grandPrixData = data;
            console.log(data)
        })
        this.leagueListService.fetchData().subscribe(data => {
            this.popularLeagues = data.slice(0, 5);
            console.log(data)
        })
    }

    openRace() {
        console.log('Klik na cijeli red result-row.');
    }

}
