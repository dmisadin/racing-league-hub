import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { LoginComponent } from './features/login/login.component';
import { SidebarComponent } from './shared/components/sidebar/sidebar.component';
import { RegistrationComponent } from './features/registration/registration.component';
import { loginGuard } from './guards/login.guard';

const routes: Routes = [
  { path:'', component: HomeComponent },
  { path:'login', component: LoginComponent, canActivate: [loginGuard] },
  { path:'register', component: RegistrationComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
