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
  @Input() size: string = '';
  @Input() faIcon: IconDefinition | IconLookup | null = null;
  @Input() isDisabled: boolean = false;
  @Output() OnClick = new EventEmitter<string>();

  constructor() {}

  emitEvent() {
    this.OnClick.emit();
  }
}
