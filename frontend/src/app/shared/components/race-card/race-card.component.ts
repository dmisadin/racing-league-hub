import { Component, Input } from '@angular/core';
import { faPlay } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-race-card',
  templateUrl: './race-card.component.html',
  styleUrls: ['./race-card.component.scss'],
})
export class RaceCardComponent {
  @Input() trackPhoto: string = '../../../../assets/images/track/photo/17.jpg';

  linearGrad =
    'linear-gradient(0deg, rgba(0, 0, 0, 0.7), rgba(0, 0, 0, .1) 50%)';
  faPlay = faPlay;
}
