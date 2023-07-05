import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'app/shared/models/user';
import { AuthService } from 'app/core/services/auth.service';
import { Observable, firstValueFrom } from 'rxjs';
import { grandPrix } from 'app/shared/models/grandPrix';
import { HomeDataService } from 'app/core/services/home-data.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  user = new User();

  constructor(
    private authService: AuthService,
    private router: Router,
    private homeDataService: HomeDataService
  ) {}

  getLoginData(data: NgForm) {
    //"data" argument is getting only .value property, as it is defined in login.component.html
    //Reminder: add default value 'false' for "rememberUser"

    this.user.username = data.value.email;
    this.user.password = data.value.password;

    this.login(this.user);

    this.router.navigate(['']);
  }

  async login(user: User): Promise<void> {
    const token = await firstValueFrom(this.authService.login(user));
    localStorage.setItem('authToken', token);

    if (localStorage.getItem('authToken'))
      this.authService.setLoggedStatus(true);

    const username = await firstValueFrom(this.authService.getMe());
    localStorage.setItem('username', username);
    this.authService.setUsername(username);

  }
}
