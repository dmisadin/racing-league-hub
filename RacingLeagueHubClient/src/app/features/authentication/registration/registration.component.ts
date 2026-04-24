import { Component, inject } from '@angular/core';
import { RestService } from '../../../core/services/rest.service';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
    selector: 'app-registration',
    imports: [ReactiveFormsModule, RouterLink],
    templateUrl: './registration.component.html',
})
export class RegistrationComponent {
    private readonly authService = inject(AuthService);
    private readonly restService = inject(RestService);
    private readonly router = inject(Router);
    formGroup: FormGroup;

    constructor(private fb: FormBuilder) {
        this.formGroup = this.fb.group({
            username: ["", Validators.required],
            email: ["", [Validators.required, Validators.email]],
            password: ["", Validators.required],
        });
    }

    onSubmit() {
        console.log(this.formGroup.value)
        if (this.formGroup.invalid)
            return;
        
        this.authService.register(this.formGroup.value).subscribe();
    }
}