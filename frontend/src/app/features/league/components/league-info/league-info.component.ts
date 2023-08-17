import { Component, Input } from '@angular/core';
import { League } from 'app/shared/models/league/League';
import { faYoutube, faDiscord, faInstagram, faFacebook, faTwitch, faTwitter } from '@fortawesome/free-brands-svg-icons';
import { faGlobe, faPen, faCircleXmark } from '@fortawesome/free-solid-svg-icons';
import { LeagueDataService } from 'app/features/league/services/league-data.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ButtonClickService } from 'app/core/services/button-click.service';
import { Subscription } from 'rxjs';


@Component({
    selector: 'app-league-info',
    templateUrl: './league-info.component.html',
    styleUrls: ['./league-info.component.scss']
})
export class LeagueInfoComponent {
    leagueItem = new League();
    resolvedData: any;

    constructor(
        private route: ActivatedRoute,
        private router: Router) {}
        
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
    isEditMode: boolean = false;

    ngOnInit(): void {
        this.leagueItem = this.route.snapshot.data['league']; //Use of resolver in router.

        if (this.router.url.includes('edit')) {
            this.isEditMode = true;
        }
    }

    /** External URL has to contain 'https://' */
    toExternalUrl(url: string) {
        if (!/^https?:\/\//i.test(url)) {
            url = 'https://' + url;
        }
        return url;
    }

    openEditForm() {
        this.router.navigate(['edit'], { relativeTo: this.route });
        this.isEditMode = !this.isEditMode;
    }
}
