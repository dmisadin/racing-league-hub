import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { passwordMatchValidator } from '../../../shared/validators/password-match.validator';

@Component({
    selector: 'reset-password-form',
    imports: [ReactiveFormsModule],
    templateUrl: './reset-password-form.component.html',
})
export class ResetPasswordFormComponent {
    private readonly authService = inject(AuthService);
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
            token: [{value: token, disabled: true}],
            newPassword: ['', [Validators.required, Validators.minLength(8)]],
            confirmPassword: ['', Validators.required]
        }, { validators: passwordMatchValidator });
    }

    onSubmit(): void {
        if (this.form.invalid)
            return;

        this.isLoading.set(true);

        this.authService.resetPassword(this.form.value).subscribe({
            next: () => this.router.navigate(['/auth/login']),
            error: () => this.isLoading.set(false),
            complete: () => this.isLoading.set(false)
        });
    }
    
    get passwordMismatch(): boolean {
        return this.form.hasError('passwordMismatch') &&
            this.form.get('repeatPassword')?.touched === true;
    }
}