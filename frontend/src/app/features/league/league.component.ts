import { Component } from '@angular/core';
import { faYoutube, faDiscord, faInstagram, faFacebook, faTwitch } from '@fortawesome/free-brands-svg-icons';
import { faGlobe } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-league',
  templateUrl: './league.component.html',
  styleUrls: ['./league.component.scss']
})
export class LeagueComponent {
  faYoutube = faYoutube;
  faDiscord = faDiscord;
  faInstagram = faInstagram;
  faFacebook = faFacebook;
  faTwitch = faTwitch;
  faGlobe = faGlobe;
}
