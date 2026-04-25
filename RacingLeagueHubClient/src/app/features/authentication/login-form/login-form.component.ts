import { Component, inject } from '@angular/core';
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

        this.authService.login(this.form.value).subscribe({
            next: () => this.onSuccess(),
            error: (err: HttpErrorResponse) => {console.log("test", err.error); this.toastService.showError(err.error.title)}
        });
    }

    onSuccess() {
        this.toastService.showSuccess("Login successful.");
        this.routeService.navigateToRoot();
    }
}
