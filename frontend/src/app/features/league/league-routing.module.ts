import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LeagueResolver } from './services/league.resolver';
import { LeagueAddEditComponent } from './components/league-add-edit/league-add-edit.component';
import { LeagueInfoComponent } from './components/league-info/league-info.component';
import { LeaguePageComponent } from './pages/league-page/league-page.component';
import { LeaguesListComponent } from './pages/leagues-list/leagues-list.component';

const routes: Routes = [

    {
        path: '',
        component: LeaguesListComponent
    },
    {
        path: 'add',
        title: 'Add League',
        component: LeagueAddEditComponent
    },
    {
        path: ':leagueId', 
        title: 'League', 
        component: LeaguePageComponent,
        children: [
            {
                path: '',
                title: 'League',
                component: LeagueInfoComponent,
                resolve: {
                    league: LeagueResolver
                }
            },
            {
                path: 'edit',
                title: 'Edit League',
                component: LeagueAddEditComponent
            },
        ]
    },
    {
        path: ':leagueId/season', // Future rework?
        loadChildren: () => import("../season/season.module").then(m => m.SeasonModule),
    },
];

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [RouterModule],
    providers: [
        LeagueResolver,
    ]
})
export class LeagueRoutingModule {

}