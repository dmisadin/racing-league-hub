import { Component } from '@angular/core';
import { AuthService } from 'app/core/services/auth.service';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent {

  constructor(private authService: AuthService){}

  logout(){
    localStorage.removeItem('authToken');
    this.authService.SetLoggedStatus(false);
  }
}
