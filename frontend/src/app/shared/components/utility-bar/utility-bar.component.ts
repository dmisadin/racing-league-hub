import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'app/core/services/auth.service';

@Component({
    selector: 'app-utility-bar',
    templateUrl: './utility-bar.component.html',
    styleUrls: ['./utility-bar.component.scss']
})
export class UtilityBarComponent {
    isLoggedIn$ = this.authService.isLoggedIn$;
    username$ = this.authService.username$;
    name: string = 'John Doe';
    constructor(public router: Router, private authService: AuthService) {
        this.username$.subscribe((username) => (this.name = username));
    }

    ngOnInit(): void { }

    login() {
        console.log('Klik na Login!');
        this.router.navigate(['login']);
    }

    logout() {
        localStorage.removeItem('authToken');
        localStorage.removeItem('username');
        this.authService.setLoggedStatus(false);
        this.authService.setUsername('John Doe');
    }
}
