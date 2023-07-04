import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-race-row',
  templateUrl: './race-row.component.html',
  styleUrls: ['./race-row.component.scss'],
})
export class RaceRowComponent {
  @Input() grandPrixName: string = 'Grand Prix Name';
  @Input() leagueName: string = 'F1 League';
  @Input() seasonName: string = 'Season 11';
  @Input() timeUntilOrFrom: string = 'Starts in 12hrs';
  @Output() OnClick = new EventEmitter<string>();

  emitEvent() {
    this.OnClick.emit();
  }
  openRace() {
    console.log('Klik na cijeli red result-row.');
  }

  imeFunkcije() {
    console.log('Klik na custom botun u result-row!');
  }
}
