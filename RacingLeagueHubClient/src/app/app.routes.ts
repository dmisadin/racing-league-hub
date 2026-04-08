import { Routes } from '@angular/router';
import { RouteService } from './core/services/route.service';

export const routes: Routes = [
    {
        path: '', 
        loadComponent: () => import('./features/home/home.component').then(c => c.HomeComponent)
    },
    {
        path: 'admin-tools', 
        loadChildren: () => import('./features/admin-tools/admin-tools.routes').then(r => r.ADMIN_TOOLS_ROUTES),
        providers: [RouteService]
    },
    {
        path: 'leagues',
        loadChildren: () => import('./features/league/league.routes').then(r => r.LEAGUE_ROUTES)
    },
    {   path: 'not-found', 
        loadComponent: () => import('./core/components/not-found.component').then(c => c.NotFoundComponent) 
    },
    {
        path: '**',
        redirectTo: 'not-found'
    }
];
