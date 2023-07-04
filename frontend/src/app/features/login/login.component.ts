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
  constructor(private authService: AuthService, private router: Router) {}

  getLoginData(data: NgForm) {
    //"data" argument is getting only .value property, as it is defined in login.component.html
    //Reminder: add default value 'false' for "rememberUser"

    this.user.username = data.value.email;
    this.user.password = data.value.password;

    this.login(this.user);

    this.router.navigate(['']);
  }

  login(user: User) {
    this.authService.login(user).subscribe((token: string) => {
      localStorage.setItem('authToken', token);

      if(localStorage.getItem('authToken'))
        this.authService.setLoggedStatus(true);

      this.authService
        .getMe()
        .subscribe((name) => {
          localStorage.setItem('username', name);
          this.authService.setUsername(name);
        });
    });
  }
}
