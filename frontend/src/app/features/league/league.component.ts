import { Component } from '@angular/core';
import { faYoutube, faDiscord, faInstagram, faFacebook, faTwitch } from '@fortawesome/free-brands-svg-icons';
import { faGlobe } from '@fortawesome/free-solid-svg-icons';
import { ActivatedRoute } from '@angular/router';
import { LeagueDataService } from 'app/core/services/league-data.service';
import { Subscription } from 'rxjs';
import { League } from 'app/shared/models/League';
@Component({
    selector: 'app-league',
    templateUrl: './league.component.html',
    styleUrls: ['./league.component.scss']
})
export class LeagueComponent {
    faYoutube = faYoutube;
    faDiscord = faDiscord;
    faInstagram = faInstagram;
    faFacebook = faFacebook;
    faTwitch = faTwitch;
    faGlobe = faGlobe;

    leagueItem$!: Subscription;
    leagueItem = new League();

    isDataLoaded: boolean = true;
    leagueId: number = 0;

    constructor(private leagueDataService: LeagueDataService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.leagueItem$ = this.route.params.subscribe(params => {
            this.leagueId = params['id'];
        })
        if(this.leagueId)
            this.leagueDataService.fetchData(this.leagueId).subscribe((data) => {
                this.leagueItem = data;
                console.log(this.leagueItem)
            })
    }

    ngOnDestroy() {
        this.leagueItem$.unsubscribe();
    }
}
