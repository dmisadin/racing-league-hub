import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { DropdownModule } from '@coreui/angular';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { HomeComponent } from './features/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import { AlertComponent } from './shared/components/alert/alert.component';
import { AnnouncementCarouselComponent } from './announcement-carousel/announcement-carousel.component';
import { BtnLargeComponent } from './shared/components/btn-large/btn-large.component';
import { SectionFlexComponent } from './shared/components/section-flex/section-flex.component';
import { ResultRowComponent } from './shared/components/result-row/result-row.component';
import { BtnCustomComponent } from './shared/components/btn-custom/btn-custom.component';
import { SidebarComponent } from './shared/components/sidebar/sidebar.component';
import { SidebarRaceComponent } from './shared/components/sidebar-race/sidebar-race.component';
import { LoginComponent } from './features/login/login.component';
import { UserInfoComponent } from './shared/components/user-info/user-info.component';
import { RegistrationComponent } from './features/registration/registration.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthInterceptor } from './core/services/auth.interceptor';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    AlertComponent,
    AnnouncementCarouselComponent,
    BtnLargeComponent,
    SectionFlexComponent,
    ResultRowComponent,
    BtnCustomComponent,
    SidebarComponent,
    SidebarRaceComponent,
    LoginComponent,
    RegistrationComponent,
    UserInfoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatIconModule,
    DropdownModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [    {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true,
  },],
  bootstrap: [AppComponent]
})
export class AppModule { }
