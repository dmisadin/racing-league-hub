import { Component } from '@angular/core';

@Component({
  selector: 'app-driver',
  templateUrl: './driver.component.html',
  styleUrls: ['./driver.component.scss']
})
export class DriverComponent {
  mockRaces = [
    { gpName: "VN Brazila", league: "F1 Adria Liga", season: "Sezona 5" },
  ];
}
