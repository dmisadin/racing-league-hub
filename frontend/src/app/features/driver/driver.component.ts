import { Component } from '@angular/core';
import { faCalendarDays } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-driver',
  templateUrl: './driver.component.html',
  styleUrls: ['./driver.component.scss'],
})
export class DriverComponent {
  faCalendarDays = faCalendarDays;
  mockRaces = [
    { gpName: 'VN Brazila', league: 'F1 Adria Liga', season: 'Sezona 5' },
  ];


  mockStats = [
    {stat:"Grand Prix Entries", value: 124, lastAchieved: "PSGL Season 10", daysSince: 5},
    {stat:"Grand Prix Wins", value: 10, lastAchieved: "F1 Adria Liga Sezona 5", daysSince: 65},
    {stat:"Pole Positions", value: 6, lastAchieved: "OKTAN F1 Sezona 4", daysSince: 712},
    {stat:"Podiums", value: 25, lastAchieved: "OKTAN F1 Sezona 4", daysSince: 13},
    {stat:"DNFs", value: 5, lastAchieved: "WOR Season 11", daysSince: 42},

  ];

  mockAvg = [
    {stat:"Average finishing position", value: 5.34},
    {stat:"Average starting position", value: 4.2},
    {stat:"Average points per race", value: 14.6},
    {stat:"DNF rate", value: "12%"},
  ];
}
