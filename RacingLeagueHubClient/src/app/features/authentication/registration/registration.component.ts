import { Component, inject } from '@angular/core';
import { RestService } from '../../../core/services/rest.service';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { InputTextComponent } from "../../../shared/components/input-fields/input-text/input-text.component";
import { passwordMatchValidator } from '../../../shared/validators/password-match.validator';

@Component({
    selector: 'registration-form',
    imports: [ReactiveFormsModule, RouterLink, InputTextComponent],
    templateUrl: './registration.component.html',
})
export class RegistrationComponent {
    private readonly authService = inject(AuthService);
    private readonly restService = inject(RestService);
    private readonly router = inject(Router);
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
        console.log(this.form.value)
        if (this.form.invalid)
            return;

        this.authService.register(this.form.value).subscribe();
    }
}