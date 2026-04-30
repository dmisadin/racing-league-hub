import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { passwordMatchValidator } from '../../../shared/validators/password-match.validator';
import { ToastService } from '../../../core/services/toast.service';
import { InputTextComponent } from "../../../shared/components/input-fields/input-text/input-text.component";

@Component({
    selector: 'reset-password-form',
    imports: [ReactiveFormsModule, InputTextComponent],
    templateUrl: './reset-password-form.component.html',
})
export class ResetPasswordFormComponent {
    private readonly authService = inject(AuthService);
    private readonly toastService = inject(ToastService);
    private readonly route = inject(ActivatedRoute);
    private readonly router = inject(Router);
    private readonly fb = inject(FormBuilder);

    isLoading = signal(false);
    form: FormGroup;

    constructor() {
        const token = this.route.snapshot.queryParamMap.get('token') ?? '';

        if (!token)
            this.router.navigate(['/not-found'])

        this.form = this.fb.group({
            token: [token],
            password: ['', [Validators.required, Validators.minLength(8)]],
            confirmPassword: ['', Validators.required]
        }, { validators: passwordMatchValidator });
    }

    onSubmit(): void {
        if (this.form.invalid)
            return;

        this.isLoading.set(true);

        this.authService.resetPassword(this.form.value).subscribe({
            next: () => this.onSuccess(),
            error: () => this.isLoading.set(false),
            complete: () => this.isLoading.set(false)
        });
    }

    get passwordMismatch(): boolean {
        return this.form.hasError('passwordMismatch') &&
            this.form.get('confirmPassword')?.touched === true;
    }

    onSuccess() {
        this.toastService.show("Password reset successfully.")
        this.router.navigate(['/auth/login'])
    }
}