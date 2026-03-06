import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '**',
        loadComponent: () => import('./core/components/not-found.component').then(c => c.NotFoundComponent)
    }
];
