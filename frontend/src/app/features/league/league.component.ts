import { Component } from '@angular/core';
import { faYoutube, faDiscord, faInstagram, faFacebook, faTwitch, faTwitter } from '@fortawesome/free-brands-svg-icons';
import { IconDefinition, faGlobe } from '@fortawesome/free-solid-svg-icons';
import { ActivatedRoute } from '@angular/router';
import { LeagueDataService } from 'app/core/services/league-data.service';
import { Subscription } from 'rxjs';
import { League } from 'app/shared/models/league/League';

@Component({
    selector: 'app-league',
    templateUrl: './league.component.html',
    styleUrls: ['./league.component.scss']
})
export class LeagueComponent {
    socialMediaIcons = {
        discord: faDiscord,
        youtube: faYoutube,
        instagram: faInstagram,
        facebook: faFacebook,
        twitch: faTwitch,
        twitter: faTwitter,
        website: faGlobe,
    }

    leagueItem$!: Subscription;
    leagueItem = new League();

    isDataLoaded: boolean = true;
    leagueId: number = 0;

    constructor(private leagueDataService: LeagueDataService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.leagueItem$ = this.route.params.subscribe(params => {
            this.leagueId = params['id'];
        })
        if (this.leagueId)
            this.leagueDataService.fetchData(this.leagueId).subscribe((data) => {
                this.leagueItem = data;
                console.log(this.leagueItem)
            })
    }

    ngOnDestroy() {
        this.leagueItem$.unsubscribe();
    }

    /** External URL has to contain 'https://' */
    toExternalUrl(url: string) {
        if (!/^https?:\/\//i.test(url)) {
            url = 'https://' + url;
        }
        return url;
    }
}
