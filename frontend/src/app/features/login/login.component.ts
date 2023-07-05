import { Component } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'app/shared/models/user';
import { AuthService } from 'app/core/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {}
  
  user = new User();

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
