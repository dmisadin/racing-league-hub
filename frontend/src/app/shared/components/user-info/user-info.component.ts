import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'app/core/services/auth.service';
import { first, firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss'],
})
export class UserInfoComponent {
  username$ = this.authService.username$;
  name : string = 'John Doe';

  constructor(private authService: AuthService) {
    this.username$.subscribe((username) => this.name = username);
  }

  logout() {
    localStorage.removeItem('authToken');
    localStorage.removeItem('username')
    this.authService.setLoggedStatus(false);
    this.authService.setUsername('John Doe');
  }

}
