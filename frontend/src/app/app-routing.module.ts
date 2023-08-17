import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { LoginComponent } from './features/login/login.component';
import { GrandPrixComponent } from './features/grandprix/grandprix.component';
import { DriverComponent } from './features/driver/driver.component';
import { RegistrationComponent } from './features/registration/registration.component';
import { loginGuard } from './core/guards/login.guard';
import { GrandPrixFormsComponent } from './features/grandprix/grandprix-forms/grandprix-forms.component';
import { NotFoundComponent } from './features/not-found/not-found.component';

const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'login', component: LoginComponent, canActivate: [loginGuard] },
    { path: 'register', component: RegistrationComponent },
    { 
        path: 'leagues', 
        loadChildren: () => import('./features/league/league.module').then(m => m.LeagueModule), 
    },
    { path: 'grandprix/edit', component: GrandPrixFormsComponent },
    { path: 'grandprix/:id', component: GrandPrixComponent },
    { path: 'driver', component: DriverComponent },
    { path: '**', component: NotFoundComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
    providers: [],
})
export class AppRoutingModule { }


/*

    { path: 'season/edit', component: SeasonFormsComponent },
    { path: 'leagues/:id/season/:id', title: 'Season',component: SeasonComponent },
    { path: 'leagues/:id/season/:id/grandprix/:id', title: 'Grand Prix', component: GrandPrixComponent },
*/