import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { RouterLink } from "@angular/router";

@Component({
    selector: 'login',
    imports: [ReactiveFormsModule, RouterLink],
    templateUrl: './login.component.html',
})
export class LoginComponent {
    private readonly authService = inject(AuthService);
    formGroup: FormGroup;

    constructor(private fb: FormBuilder) {
        this.formGroup = this.fb.group({
            email: ["", Validators.required],
            password: ["", Validators.required]
        });
    }

    onSubmit() {
        if (this.formGroup.valid)
            this.authService.login(this.formGroup.value).subscribe();
    }
}
