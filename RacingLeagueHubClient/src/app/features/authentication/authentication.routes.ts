import { Routes } from "@angular/router";

export const AUTHENTICATION_ROUTES: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: 'login', loadComponent: () => import('./login/login.component').then(c => c.LoginComponent) },
    { path: 'register', loadComponent: () => import('./registration/registration.component').then(c => c.RegistrationComponent) }
];