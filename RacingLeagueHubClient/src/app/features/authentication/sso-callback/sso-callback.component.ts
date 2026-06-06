import { Component, OnInit, inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { AuthService } from "../../../core/services/auth.service";
import { ToastService } from "../../../core/services/toast.service";

@Component({
    selector: 'sso-callback',
    standalone: true,
    template: `
    <div class="min-h-screen flex items-center justify-center">
      <span class="loading loading-spinner loading-lg"></span>
    </div>
  `
})
export class SsoCallbackComponent implements OnInit {
    private readonly route = inject(ActivatedRoute);
    private readonly router = inject(Router);
    private readonly authService = inject(AuthService);
    private readonly toastService = inject(ToastService);

    ngOnInit(): void {
        const success = this.route.snapshot.queryParamMap.get('success');

        if (success !== 'true') {
            this.toastService.showError('Google sign-in failed.');
            this.router.navigate(['/auth/login']);
            return;
        }

        this.authService.refreshSession().subscribe({
            next: () => {
                this.toastService.showSuccess('Login successful.');
                this.router.navigate(['/']);
            },
            error: () => {
                this.toastService.showError('Could not complete Google sign-in.');
                this.router.navigate(['/auth/login']);
            }
        });
    }
}