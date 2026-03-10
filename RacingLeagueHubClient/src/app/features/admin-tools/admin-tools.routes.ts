import { Routes } from '@angular/router';

export const ADMIN_TOOLS_ROUTES: Routes = [
    {
        path: '',
        loadComponent: () => import('./admin-tools.component').then(c => c.AdminToolsComponent),
        children: [
            { path: 'teams', loadChildren: () => import('./teams/teams.routes').then(r => r.ADMIN_TOOL_TEAM_ROUTES) },
            { path: 'tracks', loadChildren: () => import('./tracks/tracks.routes').then(r => r.ADMIN_TOOL_TRACK_ROUTES) },
        ]
    }
];
