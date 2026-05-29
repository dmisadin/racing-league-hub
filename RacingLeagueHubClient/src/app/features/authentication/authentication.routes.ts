import { Routes } from "@angular/router";
import { twoFactorPendingGuard } from "../../core/guards/two-factor-pending.guard";

export const AUTHENTICATION_ROUTES: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    {
        path: 'login',
        loadComponent: () => import('./login-form/login-form.component').then(c => c.LoginFormComponent)
    },
    {
        path: 'login/2fa',
        loadComponent: () => import('./login-two-factor-auth-form/login-two-factor-auth-form.component').then(c => c.LoginTwoFactorAuthFormComponent),
        canActivate: [twoFactorPendingGuard]
    },
    {
        path: 'register',
        loadComponent: () => import('./registration-form/registration-form.component').then(c => c.RegistrationFormComponent)
    },
    {
        path: 'forgot-password',
        loadComponent: () => import('./forgot-password-form/forgot-password-form.component').then(c => c.ForgotPasswordFormComponent)
    },
    {
        path: 'reset-password',
        loadComponent: () => import('./reset-password-form/reset-password-form.component').then(c => c.ResetPasswordFormComponent)
    },
];