import { Component, inject } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';

@Component({
    selector: 'user-dropdown',
    imports: [],
    templateUrl: './user-dropdown.component.html',
})
export class UserDropdownComponent {
    private readonly authService = inject(AuthService);

    logout() {
        this.authService.logout();
    }

}
