import { Component, inject, signal } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
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
    private readonly fb = inject(FormBuilder);
    private readonly authService = inject(AuthService);
    private readonly routeService = inject(RouteService);
    private readonly toastService = inject(ToastService);

    mode = signal<TwoFactorMode>('totp');
    isLoading = signal(false);
    error = signal<string | null>(null);

    form = this.fb.group({
        code: ['', Validators.required]
    });

    switchToRecoveryCode(): void {
        this.mode.set('recovery');
        this.form.reset();
        this.error.set(null);
    }

    switchToTotp(): void {
        this.mode.set('totp');
        this.form.reset();
        this.error.set(null);
    }

    submit(): void {
        if (this.form.invalid) {
            this.form.markAllAsTouched();
            return;
        }

        const code = this.form.controls.code.value ?? '';
        const isRecoveryCode = this.mode() === 'recovery';

        this.isLoading.set(true);
        this.error.set(null);

        this.authService.loginWithTwoFactor(code, isRecoveryCode).subscribe({
            next: () => {
                this.isLoading.set(false);
                this.toastService.showSuccess('Login successful.');
                this.routeService.navigateToRoot();
            },
            error: err => {
                this.isLoading.set(false);
                this.error.set(
                    isRecoveryCode
                        ? 'Invalid recovery code.'
                        : 'Invalid authenticator code.'
                );
            }
        });
    }
}

type TwoFactorMode = 'totp' | 'recovery';