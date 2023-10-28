import { Component } from '@angular/core';
import { GrandPrixLiveService, GrandPrixStartingSoonService } from 'app/core/services/grand-prix-starting-soon.service';
import { RaceRow } from 'app/shared/models/homepage/RaceRow';

@Component({
	selector: 'app-sidebar',
	templateUrl: './sidebar.component.html',
	styleUrls: ['./sidebar.component.scss'],
})
export class SidebarComponent {

    grandPrixLive: RaceRow[] = new Array();
    grandPrixStartingSoon: RaceRow[] = new Array();

    constructor( private gpStartingSoonService: GrandPrixStartingSoonService, private gpLiveService: GrandPrixLiveService ) { }

    ngOnInit(): void {
        this.gpStartingSoonService.getAll().subscribe(data => {
            this.grandPrixStartingSoon = data;
            console.log(data)
        })

        this.gpLiveService.getAll().subscribe(data => {
            this.grandPrixLive = data;
            console.log(data)
        })
    }
}
