import { Component } from '@angular/core';
import { faTwitter, faInstagram, faYoutube } from '@fortawesome/free-brands-svg-icons';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent {
    icons = { faTwitter, faInstagram, faYoutube };
}
