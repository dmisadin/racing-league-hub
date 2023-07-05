import { Component, HostBinding, Input } from '@angular/core';
import { IconDefinition, IconLookup } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-icon-label',
  templateUrl: './icon-label.component.html',
  styleUrls: ['./icon-label.component.scss'],
})
export class IconLabelComponent {
  @Input() faIcon: IconDefinition | IconLookup | null = null;
  @Input() iconPath: string = "";
  @Input() imageLink: string = "";
  @Input() name: string = "";
  @Input() width: string = "fit-content";

  @HostBinding('style.width') get inputWidth() { return this.width }

  constructor() { }

}