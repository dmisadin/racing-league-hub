import { Component, Input, Output, EventEmitter } from '@angular/core';
import { IconDefinition, IconLookup } from '@fortawesome/fontawesome-svg-core';
import { faCoffee } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-btn-large',
  templateUrl: './btn-large.component.html',
  styleUrls: ['./btn-large.component.scss']
})
export class BtnLargeComponent {
  @Input() label: string = "Large Button";
  //@Input() colorClass: string = "light";
  @Input() iconName: IconDefinition | IconLookup = faCoffee;
  @Output() OnClick = new EventEmitter<string>();

  emitEvent() {
    this.OnClick.emit();
    console.log("Klik na Large Button.");
  }
}
