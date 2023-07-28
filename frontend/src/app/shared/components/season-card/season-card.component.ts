import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-season-card',
  templateUrl: './season-card.component.html',
  styleUrls: ['./season-card.component.scss'],
})
export class SeasonCardComponent {
    @Input() logoPath: string = '../../../assets/images/league/league-logo.png';
    @Input() gameName: string = 'F1 22';
    @Input() text: string = 'Sezona X';
    @Input() subtext: string = 'Start date - End date';

    ngOnInit() {
        this.gameName = this.gameName.replace(' ', '');
    }
    ngOnChange() {
        this.gameName = this.gameName.replace(' ', '');
    }
}
