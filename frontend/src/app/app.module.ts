import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { DropdownModule } from '@coreui/angular';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { HomeComponent } from './features/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import { AlertComponent } from './shared/components/alert/alert.component';
import { AnnouncementCarouselComponent } from './shared/components/announcement-carousel/announcement-carousel.component';
import { BtnLargeComponent } from './shared/components/btn-large/btn-large.component';
import { SectionFlexComponent } from './shared/components/section-flex/section-flex.component';
import { ResultRowComponent } from './shared/components/result-row/result-row.component';
import { BtnCustomComponent } from './shared/components/btn-custom/btn-custom.component';
import { SidebarComponent } from './shared/components/sidebar/sidebar.component';
import { SidebarRaceComponent } from './shared/components/sidebar-race/sidebar-race.component';
import { LoginComponent } from './features/login/login.component';
import { UserInfoComponent } from './shared/components/user-info/user-info.component';
import { LeagueComponent } from './features/league/league.component';
import { SeasonCardComponent } from './shared/components/season-card/season-card.component';


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
    UserInfoComponent,
    LeagueComponent,
    SeasonCardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatIconModule,
    DropdownModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
