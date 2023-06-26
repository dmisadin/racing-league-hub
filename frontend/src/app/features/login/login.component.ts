import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {

  getLoginData(data: NgForm) {
    //"data" argument is getting only .value property, as it is defined in login.component.html
    //Reminder: add default value 'false' for "rememberUser"
    console.log(data);
  }
}
