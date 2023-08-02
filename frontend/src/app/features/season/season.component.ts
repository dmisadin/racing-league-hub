import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SeasonDataService } from 'app/core/services/season-data.service';
import { Season } from 'app/shared/models/season/Season';
import { Assists } from 'app/shared/models/season/Assists';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-season',
    templateUrl: './season.component.html',
    styleUrls: ['./season.component.scss']
})
export class SeasonComponent {
    seasonItem$!: Subscription;
    seasonItem = new Season();
    isDataLoaded: boolean = true;
    seasonId: number = 0;
    assists: any;
    lobbySettings: any;

    constructor(private seasonDataService: SeasonDataService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.seasonItem$ = this.route.params.subscribe(params => {
            this.seasonId = params['id'];
        })
        if (this.seasonId) {
            this.seasonDataService.getOne(this.seasonId).subscribe((data) => {
                this.seasonItem = data;
                console.log(data)

                this.assists = this.seasonItem.assists;
                this.assists = Object.entries(this.assists).map(([key, value]) => ({
                    icon: key.toLowerCase(),
                    name: this.camelCaseToWords(key),
                    value: typeof value !== "boolean" ? value : this.booleanToOnOff(value)
                }));
                console.log(this.assists)
                this.lobbySettings = this.seasonItem.lobbySettings;
                this.lobbySettings = Object.entries(this.lobbySettings).map(([key, value]) => ({
                    icon: key.toLowerCase(),
                    name: this.camelCaseToWords(key),
                    value: typeof value !== "boolean" ? value : this.booleanToOnOff(value)
                }));
                console.log(this.lobbySettings)


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
