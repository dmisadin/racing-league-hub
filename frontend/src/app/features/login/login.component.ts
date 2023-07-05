import { Component } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
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
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    ) {}

  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
    rememberUser: false,
  })

  onSubmit(): void {
    console.log("Submitted form: ", this.loginForm.value, this.loginForm.valid);

    this.user.username = this.loginForm.value.email || '';
    this.user.password = this.loginForm.value.password || '';

    this.login(this.user);
  }

  async login(user: User): Promise<void> {
    const token = await firstValueFrom(this.authService.login(user));
    localStorage.setItem('authToken', token);

    if (localStorage.getItem('authToken')){
      this.authService.setLoggedStatus(true);
      this.router.navigate(['']);
    }


    const username = await firstValueFrom(this.authService.getMe());
    localStorage.setItem('username', username);
    this.authService.setUsername(username);
  }
}
