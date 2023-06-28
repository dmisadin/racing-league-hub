import { Component, HostBinding, Input } from '@angular/core';

@Component({
  selector: 'app-icon-label',
  templateUrl: './icon-label.component.html',
  styleUrls: ['./icon-label.component.scss'],
})
export class IconLabelComponent {
  @Input() iconPath: string = "assists/tractioncontrol.png";
  @Input() name: string = "";
  @Input() width: string = "fit-content";

  @HostBinding('style.width') get inputWidth() { return this.width }

  constructor() { }

}