import { Component } from '@angular/core';
import {
  faPlus,
  faSearch,
  faStopwatch,
} from '@fortawesome/free-solid-svg-icons';
import { HomeDataService } from 'app/core/services/home-data.service';
import { grandPrix } from 'app/shared/models/grandPrix';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  faPlus = faPlus;
  faSearch = faSearch;
  faStopwatch = faStopwatch;

  isDataLoaded: boolean = false;
  grandPrixes: Array<grandPrix> = new Array<grandPrix>();

  constructor(private homeDataService: HomeDataService) {
    this.homeDataService.fetchData().subscribe((data) => {
      this.grandPrixes = data;
      this.isDataLoaded = true;
    });
  }
  openRace() {
    console.log('Klik na cijeli red result-row.');
  }
}
