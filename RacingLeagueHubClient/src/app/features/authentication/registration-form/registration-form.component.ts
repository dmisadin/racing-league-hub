import { Component, inject, signal } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { InputTextComponent } from "../../../shared/components/input-fields/input-text/input-text.component";
import { passwordMatchValidator } from '../../../shared/validators/password-match.validator';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastService } from '../../../core/services/toast.service';
import { RouteService } from '../../../core/services/route.service';

@Component({
    selector: 'registration-form',
    imports: [ReactiveFormsModule, RouterLink, InputTextComponent],
    providers: [RouteService],
    templateUrl: './registration-form.component.html',
})
export class RegistrationFormComponent {
    private readonly authService = inject(AuthService);
    private readonly toastService = inject(ToastService);
    private readonly routeService = inject(RouteService);
    
    isLoading = signal(false);
    
    form: FormGroup;

    constructor(private fb: FormBuilder) {
        this.form = this.fb.group({
            username: ['', Validators.required],
            email: ['', [Validators.required, Validators.email]],
            password: ['', [Validators.required, Validators.minLength(8)]],
            repeatPassword: ['', Validators.required],
        }, { validators: passwordMatchValidator });
    }

    get passwordMismatch(): boolean {
        return this.form.hasError('passwordMismatch') &&
            this.form.get('repeatPassword')?.touched === true;
    }

    onSubmit() {
        if (this.form.invalid)
            return;

        this.authService.register(this.form.value).subscribe({
            next: () => this.onSuccess(),
            error: (err: HttpErrorResponse) => this.onError(err.error.title),
            complete: () => this.isLoading.set(false)
        });
    }

    onSuccess() {
        this.isLoading.set(false);
        this.toastService.showSuccess("Login successful.");
        this.routeService.navigateToRoot();
    }

    onError(errorMessage?: string) {
        this.isLoading.set(false);
        this.toastService.showError(errorMessage ?? "Something went wrong")
    }
}
