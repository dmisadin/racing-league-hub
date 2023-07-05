import { Component, DoCheck, OnInit } from '@angular/core';
import { ActivatedRoute, Data } from '@angular/router';
import {
  faPlus,
  faSearch,
  faStopwatch,
} from '@fortawesome/free-solid-svg-icons';
import { HomeDataService } from 'app/core/services/home-data.service';
import { grandPrix } from 'app/shared/models/grandPrix';
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

  isDataLoaded: boolean = true;
  grandPrixData: Array<grandPrix> = new Array();
  popularLeagues: Array<grandPrix> = new Array();

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
