import { Component, Input, Output, EventEmitter } from '@angular/core';
import { IconDefinition, IconLookup } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-btn-custom',
  templateUrl: './btn-custom.component.html',
  styleUrls: ['./btn-custom.component.scss'],
})
export class BtnCustomComponent {
  @Input() label: string = '';
  @Input() colorClass: string = 'light';
  @Input() faIcon: IconDefinition | IconLookup | null = null;
  @Output() OnClick = new EventEmitter<string>();

  constructor() {}

  emitEvent() {
    this.OnClick.emit();
    console.log('icon: ', this.faIcon ? true : false);
  }
}
