import { Component } from '@angular/core';

@Component({
  selector: 'app-main-result',
  templateUrl: './main-result.component.html',
  styleUrls: ['./main-result.component.scss']
})
export class MainResultComponent {
  imeFunkcije() {
    console.log("Klik na custom botun!");
  }
}
