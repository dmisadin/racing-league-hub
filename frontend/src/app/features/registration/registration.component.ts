import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent {

  getRegistrationData(data: NgForm) {
    //"data" argument is getting only .value property, as it is defined in registration.component.html
    //Note: Component is named 'RegistrationComponent', but route is '/register'
    console.log(data);
  }
}
