import { Component, inject } from '@angular/core';
import { AuthService } from '../../../../core/services/auth.service';
import { RouterLink } from "@angular/router";

@Component({
    selector: 'user-dropdown',
    imports: [RouterLink],
    templateUrl: './user-dropdown.component.html',
})
export class UserDropdownComponent {
    readonly authService = inject(AuthService);

    logout() {
        this.authService.logout();
    }

}
