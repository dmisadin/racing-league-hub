import { Component } from '@angular/core';

@Component({
  selector: 'app-season',
  templateUrl: './season.component.html',
  styleUrls: ['./season.component.scss']
})
export class SeasonComponent {
  mockPoints = [15, 12, 10, 8, 6, 5, 4, 3, 2, 1];
}
