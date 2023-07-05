import { Component } from '@angular/core';

@Component({
  selector: 'app-table-qualifying-result',
  templateUrl: './table-qualifying-result.component.html',
  styleUrls: ['./table-qualifying-result.component.scss'],
})
export class TableQualifyingResultComponent {
  tyre = 's'.toLowerCase();

  mockDrivers = [
    { position: 1, teamid: 0 },
    { position: 2, teamid: 0 },
    { position: 3, teamid: 1 },
    { position: 4, teamid: 1 },
    { position: 5, teamid: 2 },
    { position: 6, teamid: 2 },
    { position: 7, teamid: 3 },
    { position: 8, teamid: 3 },
    { position: 9, teamid: 4 },
    { position: 10, teamid: 4 },
    { position: 11, teamid: 5 },
    { position: 12, teamid: 5 },
    { position: 13, teamid: 6 },
    { position: 14, teamid: 6 },
    { position: 15, teamid: 7 },
    { position: 16, teamid: 7 },
    { position: 17, teamid: 8 },
    { position: 18, teamid: 8 },
    { position: 19, teamid: 9 },
    { position: 20, teamid: 9 },
  ];

  mockRaces = [
    1, 2, 3, 4, 9, 10, 7, 11, 5, 6, 8, 1, 1, 1, 1, 1, 2, 2, 2, 10, 12, 9, 5,
  ];
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
}
