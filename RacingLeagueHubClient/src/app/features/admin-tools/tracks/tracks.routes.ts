import { Routes } from '@angular/router';

export const ADMIN_TOOL_TRACK_ROUTES: Routes = [
    {
        path: '',
        loadComponent: () => import('./components/track-list/track-list.component').then(c => c.TrackListComponent),
        children: [
            { path: 'add', loadComponent: () => import('./components/track-add-modal/track-add-modal.component').then(c => c.TrackAddModalComponent) }
        ]
    },
    {
        path: ':trackId',
        loadComponent: () => import('./components/track-details/track-details.component').then(c => c.TrackDetailsComponent),
        children: [
            {
                path: 'add-layout',
                loadComponent: () => import('./components/track-layout-form/modal/track-layout-modal.component').then(c => c.TrackAddModalComponent)
            }
        ]
    }
];
