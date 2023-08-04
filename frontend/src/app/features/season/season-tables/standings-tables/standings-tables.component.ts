import { Component, Input } from '@angular/core';
import { GrandPrix } from 'app/shared/models/grandprix/GrandPrix';
import { Driver, Team } from 'app/shared/models/season/Season';

@Component({
    selector: 'app-standings-tables',
    templateUrl: './standings-tables.component.html',
    styleUrls: ['./standings-tables.component.scss'],
})
export class StandingsTablesComponent {
    @Input() grandPrixes: GrandPrix[] = [];
    @Input() drivers: Driver[] = [];
    @Input() teams: Team[] = [];


    driverPoints: any[] = [];
    teamPoints: any[] = [];

    ngOnChanges() {
        // Add functionality to count Qualifying and Sprint points.
        // Check if the points are even given out for those sessions.

        this.drivers.forEach(driver => {
            this.driverPoints.push({
                pointsSum: 0,
                points: new Array(this.grandPrixes.length),
                resultStatus: new Array(this.grandPrixes.length),
                ...driver
            });
        });
        this.teams.forEach(team => {
            this.teamPoints.push({
                pointsSum: 0,
                points: new Array<number>(this.grandPrixes.length).fill(0),
                ...team
            })
        });

        this.grandPrixes.forEach((gp, i) => {
            gp.race.forEach(race => {
                const dp = this.driverPoints.find(d => d.id === race.driverId);
                dp.pointsSum += race.pointsGained;
                dp.points[i] = race.pointsGained;
                dp.resultStatus[i] = race.resultStatus;

                const tp = this.teamPoints.find(team => team.id === race.teamId);
                tp.pointsSum += race.pointsGained;
                tp.points[i] += race.pointsGained;
            });
        });
        this.driverPoints.sort((a, b) => b.pointsSum - a.pointsSum);
        this.teamPoints.sort((a, b) => b.pointsSum - a.pointsSum);
    }

    resultStatus: { [key: number]: string } = {
        0: 'invalid',
        1: 'inactive',
        2: 'active',
        3: 'finished',
        4: 'DNF',
        5: 'DSQ',
        6: 'NC',
        7: 'Ret'
    }

}
