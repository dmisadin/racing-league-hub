import { Component } from '@angular/core';
import { LeaguesListService } from 'app/core/services/leagues-list.service';
import { League } from 'app/shared/models/League';

@Component({
    selector: 'app-leagues-list',
    templateUrl: './leagues-list.component.html',
    styleUrls: ['./leagues-list.component.scss']
})
export class LeaguesListComponent {

    isDataLoaded: boolean = true;
    leagueList: Array<League> = new Array();

    constructor(private leagueListService: LeaguesListService) { }

    ngOnInit(): void {
        this.leagueListService.fetchData().subscribe((data) => {
            this.leagueList = data;
            console.log(this.leagueList)
        })
    }
}
