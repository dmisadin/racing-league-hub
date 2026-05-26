import { Component, inject, signal } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouteService } from '../../../core/services/route.service';
import { ToastService } from '../../../core/services/toast.service';
import { InputTextComponent } from "../../../shared/components/input-fields/input-text/input-text.component";

@Component({
    selector: 'login-two-factor-auth-form',
    imports: [InputTextComponent, ReactiveFormsModule],
    templateUrl: './login-two-factor-auth-form.component.html',
    providers: [RouteService]
})
export class LoginTwoFactorAuthFormComponent {
    private readonly authService = inject(AuthService);
    private readonly routeService = inject(RouteService);
    private readonly toastService = inject(ToastService);

    isLoading = signal(false);

    code = new FormControl<string>('', Validators.required);

    submitCode() {
        const code = this.code.value;

        if (this.code.invalid || !code)
            return;

        this.authService.loginWithTwoFactor(code).subscribe({
            next: () => {
                this.toastService.showSuccess("Successfully logged in.");
                this.routeService.navigateToRoot();
            },
            error: err => {
                this.toastService.showError("Failed to log in.");
            }
        });
    }
}
