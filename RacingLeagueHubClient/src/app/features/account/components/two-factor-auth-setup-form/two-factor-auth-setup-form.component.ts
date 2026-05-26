import { Component, computed, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { TwoFactorSetupResponse } from '../../models/2fa.model';
import { AccountSecurityService } from '../../services/account-security.service';
import { InputTextComponent } from '../../../../shared/components/input-fields/input-text/input-text.component';
import { QRCodeComponent } from 'angularx-qrcode';

@Component({
    selector: 'two-factor-auth-setup-form',
    imports: [ReactiveFormsModule, QRCodeComponent, InputTextComponent],
    templateUrl: './two-factor-auth-setup-form.component.html',
})
export class TwoFactorAuthSetupFormComponent {
    private readonly fb = inject(FormBuilder);
    private readonly accountSecurityService = inject(AccountSecurityService);

    readonly setupResult = signal<TwoFactorSetupResponse | null>(null);
    readonly loading = signal(false);
    readonly confirming = signal(false);
    readonly success = signal(false);
    readonly error = signal<string | null>(null);

    readonly hasSetupStarted = computed(() => this.setupResult() !== null);

    readonly form = this.fb.group({
        code: [
            '',
            [
                Validators.required,
                Validators.minLength(6),
                Validators.maxLength(6),
                Validators.pattern(/^\d{6}$/)
            ]
        ]
    });

    startSetup(): void {
        this.loading.set(true);
        this.error.set(null);
        this.success.set(false);

        this.accountSecurityService.setupTwoFactor().subscribe({
            next: res => {
                this.setupResult.set(res);
                this.loading.set(false);
            },
            error: err => {
                this.error.set(err?.error?.message ?? 'Failed to start two-factor setup.');
                this.loading.set(false);
            }
        });
    }

    confirm(): void {
        if (this.form.invalid) {
            this.form.markAllAsTouched();
            return;
        }

        const code = this.form.controls.code.value ?? '';

        this.confirming.set(true);
        this.error.set(null);

        this.accountSecurityService.confirmTwoFactor({ code }).subscribe({
            next: () => {
                this.success.set(true);
                this.confirming.set(false);
                this.form.reset();
            },
            error: err => {
                this.error.set(err?.error?.message ?? 'Invalid authentication code.');
                this.confirming.set(false);
            }
        });
    }
}
