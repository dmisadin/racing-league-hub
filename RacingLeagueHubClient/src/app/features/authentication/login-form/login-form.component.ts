import { Component, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { RouterLink } from "@angular/router";
import { ToastService } from '../../../core/services/toast.service';
import { RouteService } from '../../../core/services/route.service';
import { HttpErrorResponse } from '@angular/common/http';
import { InputTextComponent } from '../../../shared/components/input-fields/input-text/input-text.component';

@Component({
    selector: 'login-form',
    imports: [ReactiveFormsModule, RouterLink, InputTextComponent],
    providers: [RouteService],
    templateUrl: './login-form.component.html',
})
export class LoginFormComponent {
    private readonly authService = inject(AuthService);
    private readonly toastService = inject(ToastService);
    private readonly routeService = inject(RouteService);

    isLoading = signal(false);

    form: FormGroup;

    constructor(private fb: FormBuilder) {
        this.form = this.fb.group({
            email: ["", [Validators.required, Validators.email]],
            password: ["", Validators.required]
        });
    }

    onSubmit() {
        if (this.form.invalid)
            return;

        this.isLoading.set(true);
        this.authService.login(this.form.value).subscribe({
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
