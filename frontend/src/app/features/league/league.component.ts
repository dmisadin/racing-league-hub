import { Component } from '@angular/core';
import { faYoutube, faDiscord, faInstagram, faFacebook, faTwitch, faTwitter } from '@fortawesome/free-brands-svg-icons';
import { IconDefinition, faGlobe, faPen, faCircleXmark } from '@fortawesome/free-solid-svg-icons';
import { ActivatedRoute, Router } from '@angular/router';
import { LeagueDataService } from 'app/core/services/league-data.service';
import { Subscription } from 'rxjs';
import { League } from 'app/shared/models/league/League';
import { ButtonClickService } from 'app/core/services/button-click.service';
import { LeagueInfoComponent } from './league-info/league-info.component';
import { LeagueAddEditComponent } from './league-add-edit/league-add-edit.component';

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
    faPen = faPen;
    faCircleXmark = faCircleXmark;

    leagueItem$!: Subscription;
    leagueItem = new League();
    leagueId: number = 0;

    isDataLoaded: boolean = true;

    constructor(private leagueDataService: LeagueDataService,
        private route: ActivatedRoute,
        private router: Router) { }

    ngOnInit(): void {
        this.leagueItem$ = this.route.params.subscribe(params => {
            this.leagueId = params['id'];
        })
        if (this.leagueId)
            this.leagueDataService.getById(this.leagueId).subscribe((data) => {
                this.leagueItem = data;
                console.log(this.leagueItem)
            })
    }

    ngOnDestroy() {
        this.leagueItem$.unsubscribe();
    }
    
    onOutletLoaded(component: LeagueInfoComponent | LeagueAddEditComponent) {
        if (component instanceof LeagueInfoComponent) {
            component.leagueItem = this.leagueItem;
        }
        else if (component instanceof LeagueAddEditComponent) {
            console.log("edit add comp")
        }
    }
}
