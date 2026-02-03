import { Component, Input } from '@angular/core';
import { RaceResult } from 'app/shared/models/grandprix/Results';
import { ResultStatus } from 'app/shared/models/enums/Enumerations';
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
    lapDiff: number[] = [];

    races: RaceResult[] = [];
    ResultStatus = ResultStatus;

    ngOnChanges() {
        this.session.forEach(entry => {
            const driver = this.drivers.find(d => d.id === entry.driverId);

            this.lapDiff.push(this.session[0].lapsCompleted - entry.lapsCompleted)

            this.races.push({
                ...entry,
                driverName: driver?.name || '',
                countryIso: driver?.countryIso || '',
                tyres: Array.from(entry.usedTyres),
            });
        });
    }

    findTeam(teamId: number) {
        return this.teams.find(t => t.id === teamId);
    }
}
