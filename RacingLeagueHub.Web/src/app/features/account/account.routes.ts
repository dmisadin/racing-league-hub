import { Routes } from "@angular/router";
import { twoFactorPendingGuard } from "../../core/guards/two-factor-pending.guard";

export const ACCOUNT_ROUTES: Routes = [
    {
        path: '2fa',
        loadComponent: () => import('./components/two-factor-auth-setup-form/two-factor-auth-setup-form.component').then(c => c.TwoFactorAuthSetupFormComponent)
    }
];