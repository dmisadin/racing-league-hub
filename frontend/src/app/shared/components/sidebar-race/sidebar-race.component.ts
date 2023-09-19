import { Component, Input } from '@angular/core';
import {
	IconDefinition,
	IconLookup,
	faCircle,
	faPlay,
} from '@fortawesome/free-solid-svg-icons';
import { DateDiffFromNowPipe } from 'app/shared/pipes/date-diff-from-now.pipe';

@Component({
	selector: 'app-sidebar-race',
	templateUrl: './sidebar-race.component.html',
	styleUrls: ['./sidebar-race.component.scss'],
})
export class SidebarRaceComponent {
	@Input() leagueName: string = '';
	@Input() grandPrixName: string = '';
	@Input() startTime: Date = new Date();
	@Input() streamUrl: string = "youtube.com";

	@Input() isLive: boolean = false;
	@Input() buttonIconColor: string = '';

	faCircle = faCircle;
	faPlay = faPlay;
	timeRemaining: number = 0;

	constructor() {}

	ngOnInit() {
		this.timeRemaining = new Date(this.startTime).getTime() - new Date().getTime();
		console.log("time rem ", this.timeRemaining)
	}

	sidebarButton() {
		console.log('Sidebar button');
	}

	goToExternal(url: string, event: Event) {
        window.open(url, '_blank');
        event.stopPropagation(); // opens a link, but prevents opening Grand Prix page
    }

	minutesToHours(minutes: number): any {
		let hours = minutes / 60;
		return hours < 1 ? '<1' : Math.round(hours);
	}

}
