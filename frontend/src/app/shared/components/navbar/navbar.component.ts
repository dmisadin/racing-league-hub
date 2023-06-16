import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {

  constructor(public router:  Router) {}

  login() {
    console.log("Klik na Login!");
    this.router.navigate(['login']);
  }

  navHome() {
    this.router.navigate(['']);
  }
}
