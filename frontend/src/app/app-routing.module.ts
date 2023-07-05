import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { LoginComponent } from './features/login/login.component';
import { SidebarComponent } from './shared/components/sidebar/sidebar.component';
import { LeagueComponent } from './features/league/league.component';
import { SeasonComponent } from './features/season/season.component';
import { GrandPrixComponent } from './features/grandprix/grandprix.component';
import { DriverComponent } from './features/driver/driver.component';
import { RegistrationComponent } from './features/registration/registration.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegistrationComponent },
  { path: 'league', component: LeagueComponent },
  { path: 'season', component: SeasonComponent },
  { path: 'grandprix', component: GrandPrixComponent },
  { path: 'driver', component: DriverComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
