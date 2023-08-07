import { Component, Input } from '@angular/core';
import { SessionResult } from 'app/shared/models/grandprix/GrandPrix';
import { Driver, Team } from 'app/shared/models/season/Season';
import { resultStatus } from 'app/shared/models/resultStatus';
import { QualifyingResult } from 'app/shared/models/grandprix/Results';


@Component({
    selector: 'app-table-qualifying-result',
    templateUrl: './table-qualifying-result.component.html',
    styleUrls: ['./table-qualifying-result.component.scss'],
})
export class TableQualifyingResultComponent {
    @Input() session: QualifyingResult[] = [];
    @Input() drivers: Driver[] = [];
    resultStatus = resultStatus;

    qualifying: any[] = [];
 
    ngOnChanges() {
        this.session.forEach(entry => {
            const driver = this.drivers.find(d => d.id === entry.driverId);
            this.qualifying.push({
                ...entry,
                driverName: driver?.name,
                countryIso: driver?.countryIso,
            });
        });
    }
}
