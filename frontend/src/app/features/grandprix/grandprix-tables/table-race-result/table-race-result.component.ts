import { Component, Input } from '@angular/core';
import { SessionResult } from 'app/shared/models/grandprix/GrandPrix';
import { RaceResult } from 'app/shared/models/grandprix/Results';
import { resultStatus } from 'app/shared/models/resultStatus';
import { Driver, Team } from 'app/shared/models/season/Season';
@Component({
    selector: 'app-table-race-result',
    templateUrl: './table-race-result.component.html',
    styleUrls: ['./table-race-result.component.scss'],
})


export class TableRaceResultComponent {
    @Input() session: RaceResult[] = [];
    @Input() drivers: Driver[] = [];
    @Input() teams: Team[] = [];
    resultStatus = resultStatus;

    race: RaceResult[] = [];

    ngOnChanges() {
        this.session.forEach(entry => {
            const driver = this.drivers.find(d => d.id === entry.driverId);
            this.race.push({
                ...entry,
                driverName: driver?.name || '',
                countryIso: driver?.countryIso || '',
                tyres: Array.from(entry.usedTyres),
            });
        });
        console.log(this.race)
    }

    findTeam(teamId: number) {
        return this.teams.find(t => t.id === teamId);
    }
}
