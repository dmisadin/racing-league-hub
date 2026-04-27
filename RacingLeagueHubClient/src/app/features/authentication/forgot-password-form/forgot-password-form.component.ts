import { Component, inject, signal } from '@angular/core';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { InputTextComponent } from "../../../shared/components/input-fields/input-text/input-text.component";

@Component({
    selector: 'forgot-password-form',
    imports: [ReactiveFormsModule, InputTextComponent],
    templateUrl: './forgot-password-form.component.html',
})
export class ForgotPasswordFormComponent {
    private readonly authService = inject(AuthService);
    private readonly router = inject(Router);

    isLoading = signal(false);
    email: FormControl<string | null> = new FormControl('', [Validators.required, Validators.email]) ;

    onSubmit(event: Event): void {
        event.preventDefault();

        const email = this.email.value;
        
        if (this.email.invalid || !email)
            return;

        this.isLoading.set(true);

        this.authService.forgotPassword({email: email}).subscribe({
            //next: () => this.router.navigate(['/auth/login']),
            error: () => this.isLoading.set(false),
            complete: () => this.isLoading.set(false)
        });
    }
}
