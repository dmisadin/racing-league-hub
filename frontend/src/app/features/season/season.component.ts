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

    mockAssists = [
        { iconPath: 'assists/tractioncontrol.png', name: 'Traction Control', value: 'Medium' },
        { iconPath: 'assists/abs.png', name: 'ABS', value: 'Off' },
        { iconPath: 'assists/racingline.png', name: 'Racing Line', value: 'Corners only' },
        { iconPath: 'assists/gearbox.png', name: 'gearbox', value: 'Manual' },
    ];

    mockLobby = [
        { iconPath: 'lobby/qualifying.png', name: 'Qualifying', value: 'Short' },
        { iconPath: 'lobby/formationlap.png', name: 'Formation Lap', value: 'On' },
        { iconPath: 'lobby/equalcarperformance.png', name: 'Race Distance', value: '50%' },
        { iconPath: 'lobby/weather.png', name: 'Weather', value: 'Dynamic' },
        { iconPath: 'lobby/cornercutting.png', name: 'Corner cutting', value: 'strict' },
        { iconPath: 'lobby/damage.png', name: 'Damage', value: 'Standard' },
        { iconPath: 'lobby/parcferme.png', name: 'Parc Ferme', value: 'On' },
        { iconPath: 'lobby/equalcarperformance.png', name: 'Engine', value: 'Equal' },
        { iconPath: 'lobby/safetycar.png', name: 'Safety Car', value: 'Reduced' },
        { iconPath: 'lobby/start.png', name: 'Starts', value: 'Manual' },
        { iconPath: 'lobby/collisions.png', name: 'Collisions', value: 'On' },
        { iconPath: 'lobby/ghosting.png', name: 'Ghosting', value: 'Off' },
    ]

    mockPoints = [15, 12, 10, 8, 6, 5, 4, 3, 2, 1];

    camelCaseToWords(camel: string) {
        return camel.replace(/([A-Z])/g, " $1");
    }
    booleanToOnOff(bool: boolean) {
        return bool ? "On" : "Off";
    }
}
