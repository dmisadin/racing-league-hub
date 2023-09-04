import { Component, Input, Output, EventEmitter } from '@angular/core';
import { IconDefinition, IconLookup } from '@fortawesome/free-solid-svg-icons';
import { faPlayCircle } from '@fortawesome/free-solid-svg-icons';

@Component({
    selector: 'app-race-row',
    templateUrl: './race-row.component.html',
    styleUrls: ['./race-row.component.scss'],
})
export class RaceRowComponent {
    @Input() first: string = "";
    @Input() second: string = "";
    @Input() third: string = "";
    @Input() firstDescription: string = "";
    @Input() secondDescription: string = "";
    @Input() thirdDescription: string = "";
    @Input() faIcon: IconDefinition | IconLookup | null = null;
    @Output() OnClick = new EventEmitter<string>();

    faPlayCircle = faPlayCircle;

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
