import { Component } from '@angular/core';
import { faPlay } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-grandprix',
  templateUrl: './grandprix.component.html',
  styleUrls: ['./grandprix.component.scss'],
})
export class GrandPrixComponent {
  faPlay = faPlay;
}
