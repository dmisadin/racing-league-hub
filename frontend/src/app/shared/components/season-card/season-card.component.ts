import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-season-card',
  templateUrl: './season-card.component.html',
  styleUrls: ['./season-card.component.scss'],
})
export class SeasonCardComponent {
    @Input() logoPath: string = '../../../assets/images/league/league-logo.png';
    @Input() gameName: string = 'F124';
    @Input() text: string = 'Sezona X';
    @Input() subtext: string = 'Start date - End date';
}
