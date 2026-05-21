import { Routes } from '@angular/router';
import { leagueEditorGuard } from '../../core/guards/league-editor.guard';

export const LEAGUE_ROUTES: Routes = [
    {
        path: '',
        loadComponent: () => import('./components/league-list/league-list.component').then(c => c.LeagueListComponent),
        children: [
            {
                path: 'add',
                loadComponent: () => import('./components/league-form/modal/league-form-modal.component').then(c => c.LeagueFormModalComponent)
            }
        ]
    },
    {
        path: ':leagueSlug',
        loadComponent: () => import('./components/league-details/league-details.component').then(c => c.LeagueDetailsComponent),
        children: [
            {
                path: 'add-season',
                loadComponent: () => import('./season/components/season-form/modal/season-form-modal.component').then(c => c.SeasonFormModalComponent)
            }
        ]
    },
    {
        path: ':leagueSlug/edit',
        loadComponent: () => import('./components/league-form/league-form.component').then(c => c.LeagueFormComponent),
        canActivate: [leagueEditorGuard]
    },
    {
        path: ':leagueSlug/:seasonSlug',
        loadComponent: () => import('./season/components/season-details/season-details.component').then(c => c.SeasonDetailsComponent),
        children: [
            {
                path: 'add-grand-prix',
                loadComponent: () => import('./season/grand-prix/components/grand-prix-form/modal/grand-prix-form-modal.component').then(c => c.GrandPrixFormModalComponent)
            }
        ]
    },
    {
        path: ':leagueSlug/:seasonSlug/edit',
        loadComponent: () => import('./season/components/season-form/season-form.component').then(c => c.SeasonFormComponent),
        canActivate: [leagueEditorGuard]
    },
    {
        path: ':leagueSlug/:seasonSlug/:grandPrixSlug',
        loadComponent: () => import('./season/grand-prix/components/grand-prix-details/grand-prix-details.component').then(c => c.GrandPrixDetailsComponent)
    },
    {
        path: ':leagueSlug/:seasonSlug/:grandPrixSlug/edit',
        loadComponent: () => import('./season/grand-prix/components/grand-prix-form/grand-prix-form.component').then(c => c.GrandPrixFormComponent),
        canActivate: [leagueEditorGuard]
    }
];
