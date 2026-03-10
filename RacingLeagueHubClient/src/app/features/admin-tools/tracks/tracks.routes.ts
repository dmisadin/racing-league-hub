import { Routes } from '@angular/router';

export const ADMIN_TOOL_TRACK_ROUTES: Routes = [
    {
        path: '',
        loadComponent: () => import('./components/track-list/track-list.component').then(c => c.TrackListComponent),
        children: [
            { path: 'add', loadComponent: () => import('./components/track-form/track-form.component').then(c => c.TrackFormComponent) },
            { path: ':id', loadComponent: () => import('./components/track-form/track-form.component').then(c => c.TrackFormComponent) }
        ]
    }
];
