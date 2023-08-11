import { Component, Input } from '@angular/core';

type Directions = 'row' | 'column';

@Component({
    selector: 'app-section-flex',
    templateUrl: './section-flex.component.html',
    styleUrls: ['./section-flex.component.scss'],
})
export class SectionFlexComponent {
    @Input() width: string = '100%';
    @Input() direction: Directions = 'column';
    @Input() gap: string = '8px';
    @Input() justify: string = 'flex-start';
    @Input() title: string = '';
    @Input() titleM: string = '';
}
