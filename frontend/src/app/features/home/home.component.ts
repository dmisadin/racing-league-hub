import { Component } from '@angular/core';
import { faPlus, faSearch, faStopwatch } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  faPlus = faPlus;
  faSearch = faSearch;
  faStopwatch = faStopwatch;

  openRace() {
    console.log("Klik na cijeli red result-row.");
  }
}
