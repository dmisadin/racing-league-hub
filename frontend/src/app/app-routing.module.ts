import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { LoginComponent } from './features/login/login.component';
import { LeagueComponent } from './features/league/league.component';
import { SeasonComponent } from './features/season/season.component';
import { GrandPrixComponent } from './features/grandprix/grandprix.component';
import { DriverComponent } from './features/driver/driver.component';
import { RegistrationComponent } from './features/registration/registration.component';
import { loginGuard } from './core/guards/login.guard';
import { LeagueAddEditComponent } from './features/league/league-add-edit/league-add-edit.component';
import { SeasonFormsComponent } from './features/season/season-forms/season-forms.component';
import { GrandPrixFormsComponent } from './features/grandprix/grandprix-forms/grandprix-forms.component';
import { NotFoundComponent } from './features/not-found/not-found.component';
import { LeaguesListComponent } from './features/leagues-list/leagues-list.component';

const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'login', component: LoginComponent, canActivate: [loginGuard] },
    { path: 'register', component: RegistrationComponent },
    { path: 'league/add', title: 'Add League', component: LeagueAddEditComponent },
    {
        path: 'leagues/:id', title: 'League Page', component: LeagueComponent,
        children: [
            { path: 'edit', component: LeagueAddEditComponent}
        ]
    },
    { path: 'leagues/:id/season/:id', component: SeasonComponent },
    { path: 'leagues/:id/season/:id/grandprix/:id', component: GrandPrixComponent },
    { path: 'leagues', component: LeaguesListComponent },
    { path: 'grandprix/edit', component: GrandPrixFormsComponent },
    { path: 'grandprix/:id', component: GrandPrixComponent },
    { path: 'driver', component: DriverComponent },
    { path: 'season/edit', component: SeasonFormsComponent },
    { path: '**', component: NotFoundComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule { }
