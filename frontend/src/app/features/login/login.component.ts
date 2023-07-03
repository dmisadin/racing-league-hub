import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'app/shared/models/user';
import { AuthService } from 'app/core/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {

  user = new User();
  constructor(private authService: AuthService, private router: Router){}

  getLoginData(data: NgForm) {
    //"data" argument is getting only .value property, as it is defined in login.component.html
    //Reminder: add default value 'false' for "rememberUser"
    console.log(data);

    this.user.username = data.value.email;
    this.user.password = data.value.password;

    console.log(this.user);

    this.login(this.user);

    this.authService.SetLoggedStatus(true);
    this.router.navigate(['']);
  }

  login(user: User) {
    this.authService.login(user).subscribe((token: string) => {
      localStorage.setItem('authToken', token);
    });
  }

  getMe() {
    this.authService.getMe().subscribe((name: string) => {
      console.log(name);
    })
  }
}
