import { Component, Input, Output, EventEmitter } from '@angular/core';
import { IconDefinition, IconLookup } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-race-row',
  templateUrl: './race-row.component.html',
  styleUrls: ['./race-row.component.scss']
})
export class RaceRowComponent {
  @Input() first: string = "";
  @Input() second: string = "";
  @Input() third: string = "";
  @Input() faIcon: IconDefinition | IconLookup | null = null;
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
