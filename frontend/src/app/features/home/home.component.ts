import { Component, OnInit } from '@angular/core';
import {
  faPlus,
  faSearch,
  faStopwatch, faPlayCircle, faCalendarDays,
} from '@fortawesome/free-solid-svg-icons';
import { HomeDataService } from 'app/core/services/home-data.service';
import { GrandPrix } from 'app/shared/models/GrandPrix';
import { BehaviorSubject, Observable, firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  faPlus = faPlus;
  faSearch = faSearch;
  faStopwatch = faStopwatch;
  faPlayCircle = faPlayCircle;
  faCalendarDays = faCalendarDays;

  isDataLoaded: boolean = true;
  grandPrixData: Array<GrandPrix> = new Array();
  popularLeagues: Array<GrandPrix> = new Array();

  constructor(private homeDataService: HomeDataService) {}

  ngOnInit(): void {
    this.homeDataService.fetchData().subscribe((data) => {
      this.grandPrixData = data;
      this.popularLeagues = this.grandPrixData.slice(0,3);
    })
  }

  openRace() {
    console.log('Klik na cijeli red result-row.');
  }

}
