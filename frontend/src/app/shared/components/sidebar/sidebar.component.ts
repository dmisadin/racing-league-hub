import { Component } from '@angular/core';
import { GrandPrixLiveService, GrandPrixService } from 'app/core/services/grand-prix.service';
import { RaceRow } from 'app/shared/models/homepage/RaceRow';

@Component({
	selector: 'app-sidebar',
	templateUrl: './sidebar.component.html',
	styleUrls: ['./sidebar.component.scss'],
})
export class SidebarComponent {

    grandPrixLive: RaceRow[] = new Array();
    grandPrixStartingSoon: RaceRow[] = new Array();

    constructor( private grandPrixService: GrandPrixService, private gpLiveService: GrandPrixLiveService ) { }

    ngOnInit(): void {
        this.grandPrixService.getAll().subscribe(data => {
            this.grandPrixStartingSoon = data;
            console.log(data)
        })

        this.gpLiveService.getAll().subscribe(data => {
            this.grandPrixLive = data;
            console.log(data)
        })
    }
}
