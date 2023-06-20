import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { LoginComponent } from './features/login/login.component';
import { SidebarComponent } from './shared/components/sidebar/sidebar.component';
import { LeagueComponent } from './features/league/league.component';

const routes: Routes = [
  { path:'', component: HomeComponent },
  { path:'login', component: LoginComponent },
  { path:'league', component: LeagueComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
