import { Component } from '@angular/core';
import { faXmarkCircle, faInfoCircle } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss']
})
export class AlertComponent {
  faXmarkCircle = faXmarkCircle;
  faInfoCircle = faInfoCircle;
}
