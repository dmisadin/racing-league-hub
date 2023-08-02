import { Component } from '@angular/core';

@Component({
  selector: 'app-table-team-standings',
  templateUrl: './table-team-standings.component.html',
  styleUrls: ['./table-team-standings.component.scss'],
})
export class TableTeamStandingsComponent {
  mockCountries = [
    'hr',
    'rs',
    'ba',
    'me',
    'xk',
    'al',
    'mk',
    'si',
    'bh',
    'au',
    'at',
    'be',
  ];
  mockTeams = [
    'Mercedes',
    'Ferrari',
    'Red Bull Racing',
    'Williams',
    'Aston Martin',
    'Alpine',
    'Alpha Tauri',
    'Haas',
    'McLaren',
    'Alfa Romeo',
  ];
}
