import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '', 
        loadComponent: () => import('./features/home/home.component').then(c => c.HomeComponent)
    },
    {
        path: 'admin-tools', 
        loadChildren: () => import('./features/admin-tools/admin-tools.routes').then(r => r.ADMIN_TOOLS_ROUTES)
    },
    {
        path: '**',
        loadComponent: () => import('./core/components/not-found.component').then(c => c.NotFoundComponent)
    }
];
