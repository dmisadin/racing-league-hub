import { Routes } from '@angular/router';

export const ADMIN_TOOL_TEAM_ROUTES: Routes = [
    {
        path: '',
        loadComponent: () => import('./components/team-list/team-list.component').then(c => c.TeamListComponent),
        children: [
            { path: 'add', loadComponent: () => import('./components/team-form/team-form.component').then(c => c.TeamFormComponent) },
            { path: ':id', loadComponent: () => import('./components/team-form/team-form.component').then(c => c.TeamFormComponent) }
        ]
    }
];
