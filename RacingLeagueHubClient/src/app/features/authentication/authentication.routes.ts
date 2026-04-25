import { Routes } from "@angular/router";

export const AUTHENTICATION_ROUTES: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: 'login', loadComponent: () => import('./login-form/login-form.component').then(c => c.LoginFormComponent) },
    { path: 'register', loadComponent: () => import('./registration-form/registration-form.component').then(c => c.RegistrationFormComponent) }
];