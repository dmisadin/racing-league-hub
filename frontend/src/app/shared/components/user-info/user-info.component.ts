import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'app/core/services/auth.service';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss'],
})
export class UserInfoComponent {
  userName: string = 'Hello';

  constructor(private authService: AuthService) {
    this.setName();
  }

  logout() {
    localStorage.removeItem('authToken');
    localStorage.removeItem('username')
    this.authService.SetLoggedStatus(false);
  }

  setName() {
    const name = localStorage.getItem('username');
    if(!name)
      return;
    this.userName = name;
  }
}
