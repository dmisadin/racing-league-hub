import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'app/core/services/auth.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  isLoggedIn$ = this.authService.isLoggedIn$;
  constructor(public router:  Router, private authService: AuthService) {}

  ngOnInit(): void {
  }

  login() {
    console.log("Klik na Login!");
    this.router.navigate(['login']);
  }

  navHome() {
    this.router.navigate(['']);
  }
}
