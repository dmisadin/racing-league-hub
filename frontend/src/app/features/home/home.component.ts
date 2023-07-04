import { Component } from '@angular/core';
import { faPlus, faSearch, faStopwatch, faPlayCircle, faCalendarDays } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  faPlus = faPlus;
  faSearch = faSearch;
  faStopwatch = faStopwatch;
  faPlayCircle = faPlayCircle;
  faCalendarDays = faCalendarDays;
  
  openRace() {
    console.log("Klik na cijeli red result-row.");
  }
}
