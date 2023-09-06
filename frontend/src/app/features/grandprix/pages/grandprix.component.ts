import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { faPlay } from '@fortawesome/free-solid-svg-icons';
import { GrandprixDataService } from 'app/features/grandprix/services/grandprix-data.service';
import { GrandPrix } from 'app/shared/models/grandprix/GrandPrix';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-grandprix',
    templateUrl: './grandprix.component.html',
    styleUrls: ['./grandprix.component.scss'],
})
export class GrandPrixComponent {
    faPlay = faPlay;
    gpItem$!: Subscription;
    gpItem = new GrandPrix();
    gpId: number = 0;
    seasonId: number = 0;
    isDataLoaded: boolean = false;
    fastestLap = {driverName: '', teamId: 0, countryIso: 'hr',  lapTimeInMs: 0}
    constructor(private gpDataService: GrandprixDataService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.gpItem$ = this.route.params.subscribe(params => {
            this.gpId = params['grandPrixId'];
            //this.seasonId = params['seasonId'];
        })
        if (this.gpId) {
            this.gpDataService.getOne(this.gpId).subscribe((data) => {
                console.log(data)
                this.gpItem = data;
                this.isDataLoaded = true;

                if(data.fastestDriverId){
                    const raceEntry = data.race.find(r => r.driverId === data.fastestDriverId)
                    if(raceEntry) {
                        const driver = data.drivers!.find(d => d.id === data.fastestDriverId);
                        this.fastestLap = {
                            driverName: driver!.name,
                            teamId: raceEntry.teamId, 
                            countryIso: driver!.countryIso,  
                            lapTimeInMs: raceEntry.fastestLapInMs as number,
                        }
                    }
                }
            })
        }
    }

    goToYoutube() {
        window.open(this.gpItem.youtubeUrl, '_blank');
    }
}
