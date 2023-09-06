import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SeasonDataService } from 'app/features/season/services/season-data.service';
import { Season } from 'app/shared/models/season/Season';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-season-page',
    templateUrl: './season-page.component.html',
    styleUrls: ['./season-page.component.scss']
})
export class SeasonPageComponent {
    seasonItem$!: Subscription;
    seasonItem = new Season();
    isDataLoaded: boolean = false;
    seasonId: number = 0;
    assists: any;
    lobbySettings: any;

    constructor(private seasonDataService: SeasonDataService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.seasonItem$ = this.route.params.subscribe(params => {
            this.seasonId = params['seasonId'];
        })
        if (this.seasonId) {
            this.seasonDataService.getOne(this.seasonId).subscribe((data) => {
                this.isDataLoaded = true;
                this.seasonItem = data;
                // WARNING: Transfer to template using "keyvalue" pipe. boleanToOnOff() can become a custom pipe.
                this.assists = this.seasonItem.assists;
                this.assists = Object.entries(this.assists).map(([key, value]) => ({
                    icon: key.toLowerCase(),
                    name: this.camelCaseToWords(key),
                    value: typeof value !== "boolean" ? value : this.booleanToOnOff(value)
                }));

                this.lobbySettings = this.seasonItem.lobbySettings;
                this.lobbySettings = Object.entries(this.lobbySettings).map(([key, value]) => ({
                    icon: key.toLowerCase(),
                    name: this.camelCaseToWords(key),
                    value: typeof value !== "boolean" ? value : this.booleanToOnOff(value)
                }));
            })
        }
    }

    ngOnDestroy() {
        this.seasonItem$.unsubscribe();
    }

    camelCaseToWords(camel: string) {
        return camel.replace(/([A-Z])/g, " $1");
    }

    booleanToOnOff(bool: boolean) {
        return bool ? "On" : "Off";
    }
}
