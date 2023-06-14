import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-result-row',
  templateUrl: './result-row.component.html',
  styleUrls: ['./result-row.component.scss']
})
export class ResultRowComponent {
  @Input() grandPrixName: string = "Grand Prix Name";
  @Input() leagueName: string = "F1 League";
  @Input() seasonName: string = "Season 11";
  @Output() OnClick = new EventEmitter<string>();

  emitEvent() {
    this.OnClick.emit();
  }
  openRace() {
    console.log("Klik na cijeli red result-row.");
  }

  imeFunkcije() {
    console.log("Klik na custom botun u result-row!");
  }
}
