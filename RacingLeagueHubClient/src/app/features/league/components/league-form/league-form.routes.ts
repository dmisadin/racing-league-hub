import { Routes } from '@angular/router';

export const NON_PUBLIC_ROUTES: Routes = [
    {
        path: 'add',
        loadComponent: () => import('./modal/league-form-modal.component').then(c => c.LeagueFormModalComponent)
    }
]