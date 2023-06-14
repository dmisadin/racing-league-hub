import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-sidebar-race',
  templateUrl: './sidebar-race.component.html',
  styleUrls: ['./sidebar-race.component.scss']
})
export class SidebarRaceComponent {
  @Input() leagueName: string = "";
  @Input() grandPrixName: string = "";
  @Input() timeRemainingM: number = 720;

  @Input() buttonIcon: string = "";
  @Input() buttonIconColor: string = "";


  sidebarButton() {
    console.log("Sidebar button");
  }

  minutesToHours(minutes: number): any {
    let hours = minutes / 60;
    return hours < 1 ? "<1" : Math.round(hours);
  }
}


