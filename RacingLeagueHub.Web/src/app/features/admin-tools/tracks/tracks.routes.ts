import { Routes } from '@angular/router';

export const ADMIN_TOOL_TRACK_ROUTES: Routes = [
    {
        path: '',
        loadComponent: () => import('./components/track-list/track-list.component').then(c => c.TrackListComponent),
        children: [
            { path: 'add', loadComponent: () => import('./components/track-form/track-form-modal/track-form-modal.component').then(c => c.TrackFormModalComponent) }
        ]
    },
    {
        path: ':trackId',
        loadComponent: () => import('./components/track-details/track-details.component').then(c => c.TrackDetailsComponent),
        children: [
            {
                path: 'add-layout',
                loadComponent: () => import('./components/track-layout-form/modal/track-layout-form-modal.component').then(c => c.TrackLayoutFormModalComponent)
            }
        ]
    }
];
