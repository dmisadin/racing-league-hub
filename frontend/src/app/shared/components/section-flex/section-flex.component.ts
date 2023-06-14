import { Component, Input } from '@angular/core';

type Directions = 'row' | 'column';

@Component({
  selector: 'app-section-flex',
  templateUrl: './section-flex.component.html',
  styleUrls: ['./section-flex.component.scss']
})
export class SectionFlexComponent {
  @Input() direction: Directions = 'column';
  @Input() title: string = '';

}
