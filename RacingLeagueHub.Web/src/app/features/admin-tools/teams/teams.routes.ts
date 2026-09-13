import { Routes } from '@angular/router';

export const ADMIN_TOOL_TEAM_ROUTES: Routes = [
    {
        path: '',
        loadComponent: () => import('./components/team-list/team-list.component').then(c => c.TeamListComponent),
        children: [
            {
                path: 'add',
                loadComponent: () => import('./components/team-form/team-form-modal/team-form-modal.component').then(c => c.TeamFormModalComponent)
            }
        ]
    },
    {
        path: ':teamId',
        loadComponent: () => import('./components/team-details/team-details.component').then(c => c.TeamDetailsComponent),
        children: [
            {
                path: 'add-game-team',
                loadComponent: () => import('./components/game-team-form/modal/game-team-form-modal.component').then(c => c.GameTeamFormModalComponent)
            }
        ]
    }
];
