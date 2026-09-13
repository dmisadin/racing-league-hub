import { Routes } from '@angular/router';
import { RouteService } from './core/services/route.service';
import { ListService } from './shared/services/list.service';
import { adminGuard, authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
    {
        path: '',
        loadComponent: () => import('./features/home/home.component').then(c => c.HomeComponent)
    },
    {
        path: 'auth', 
        loadChildren: () => import('./features/authentication/authentication.routes').then(r => r.AUTHENTICATION_ROUTES),
        // canActivate: [loggedInGuard]
    },
    {
        path: 'account',
        loadChildren: () => import('./features/account/account.routes').then(r => r.ACCOUNT_ROUTES)
    },
    {
        path: 'admin-tools',
        loadChildren: () => import('./features/admin-tools/admin-tools.routes').then(r => r.ADMIN_TOOLS_ROUTES),
        providers: [RouteService],
        canActivate: [adminGuard]
    },
    {
        path: 'leagues',
        loadChildren: () => import('./features/league/league.routes').then(r => r.LEAGUE_ROUTES),
        providers: [ListService]
    },
    {
        path: 'not-found',
        loadComponent: () => import('./core/components/not-found.component').then(c => c.NotFoundComponent)
    },
    {
        path: '**',
        redirectTo: 'not-found'
    }
];
